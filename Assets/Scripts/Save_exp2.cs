using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;

using System;   //for datetime


public class Save_exp2: MonoBehaviour
{
    public GameObject dcanvas;
    public List<GameObject> points;
    public GameObject hcanvas;

    private List<GameObject> Areas;

    private Manager manager;
    private UndoRedo ud;

    private string hpath = "C:/Users/yahoo/cyberlab/thesis/exp2/Record/";
    private string lpath;

    private void Reset()
    {
        foreach (GameObject point in points)
        {
            point.SetActive(false);
        }
        foreach (GameObject go in Areas)
        {
            Destroy(go);
        }
        Areas.Clear();
        hcanvas.SetActive(true);
        manager.ShowElectrodes(manager.Option());
        ud.ResetInstances();
    }

    /*
    private IEnumerator DelayMethod(float waitTime, Action action)
    {
        yield return new WaitForSeconds(waitTime);
        action();
    }
    */
    private void Capture(string filePath)
    {
        ScreenCapture.CaptureScreenshot(filePath);
    }
    private void Clean(bool flag)
    {
        dcanvas.SetActive(!flag);
    }
    //access instances
    public void AddToArea(GameObject obj)
    {
        Areas.Add(obj);
    }



    public async void OnClickSave()
    {
        //name of file
        string subject = manager.InputBox();
        // DateTime dt = DateTime.Now;
        // string result = dt.ToString("MM_dd_");
        int current = manager.CurrentNum();
        string pair1 = manager.Pair1();
        // string pair2 = manager.Pair2();
        bool practice = (pair1 == "P");
        string path;
        if (practice)
        {
            path = hpath + "subject" + subject + "/practice" + "_" + current.ToString();
        }
        else
        {
            path = hpath + "subject" + subject + "/" + pair1 +  "_" + current.ToString();
        }
        string filename;
        Areas = ud.AreasAll();

        if (subject != "0" || practice)
        {
            if (Areas.Count < 3) return;
            foreach (GameObject point in points) if (!point.activeSelf) return;
        }

        if (current < 4)
        {
            manager.UpdateCurrentNum(current + 1);
        }
        else
        {
            manager.UpdateCurrentNum(0);
            manager.ResetPair1();//Pairの表示をPにリセットする(fail safe)
        }

        Debug.Log(path);


        //record point coordinate
        StreamWriter file;
        if (current == 0) file = new StreamWriter(hpath + "subject" + subject + "/" + pair1 + "_result.csv", false, Encoding.UTF8);
        else file = new StreamWriter(hpath + "subject" + subject + "/" + pair1 + "_result.csv", true, Encoding.UTF8);

        file.WriteLine((current + 1).ToString());
        for (int i = 0; i < 3; i++)
        {
            Vector3 tmp = points[i].transform.position;
            file.WriteLine(i.ToString() + "," + tmp.x.ToString() + "," + tmp.y.ToString());
        }
        file.Close();


        Clean(true);
        //capture all
        filename = "_all.png";
        Capture(path + filename);
        await Task.Delay(100);

        //capture area + hand
        foreach (GameObject point in points) point.SetActive(false);
        filename = "_area.png";
        Capture(path + filename);
        await Task.Delay(100);

        //capture just area
        hcanvas.SetActive(false);
        filename = "_areaonly.png";
        Capture(path + filename);
        await Task.Delay(100);


        Clean(false);
        Reset();

    }

    // Start is called before the first frame update
    void Start()
    {
        ud = GetComponent<UndoRedo>();
        Areas = ud.AreasAll();
        manager = GetComponent<Manager>();


    }

    // Update is called once per frame
    void Update()
    {

    }
}
