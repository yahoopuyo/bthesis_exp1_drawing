using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;

public class Save_exp2: MonoBehaviour
{
    public GameObject dcanvas;
    public List<GameObject> points;
    public GameObject hcanvas;

    private List<GameObject> Areas;

    private Manager manager;
    private UndoRedo ud;

    private string hpath = "C:/Users/yahoo/cyberlab/thesis/exp1_record/Record/subject";
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
        string lpath = manager.Option();
        int currentNum = manager.CurrentNum();
        string path = hpath + manager.SubjectNum().ToString() + "/" + lpath + "/";
        string filename;
        Areas = ud.AreasAll();
        //check whether valid
        if (lpath != "Practice")
        {
            if (Areas.Count < 3) return;
            foreach (GameObject point in points) if (!point.activeSelf) return;
        }

        Debug.Log(path);
        if (currentNum < 4)
        {
            manager.UpdateCurrentNum(currentNum + 1);
        }
        else
        {
            manager.UpdateCurrentNum(0);
        }

        //record point coordinate
        StreamWriter file;
        if (currentNum == 00) file = new StreamWriter(path + "result.csv", false, Encoding.UTF8);
        else file = new StreamWriter(path + "result.csv", true, Encoding.UTF8);

        file.WriteLine((currentNum + 1).ToString());
        for (int i = 0; i < 3; i++)
        {
            Vector3 tmp = points[i].transform.position;
            file.WriteLine(i.ToString() + "," + tmp.x.ToString() + "," + tmp.y.ToString());
        }
        file.Close();

        Clean(true);
        //capture all
        filename = (currentNum + 1).ToString() + "_all.png";
        Capture(path + filename);
        await Task.Delay(100);

        //capture without electrodes
        manager.ClearElectrodes();
        filename = (currentNum + 1).ToString() + "_no_elect.png";
        Capture(path + filename);
        await Task.Delay(100);

        //capture without electrodes,hand canvas
        hcanvas.SetActive(false);
        filename = (currentNum + 1).ToString() + "_no_hand.png";
        Capture(path + filename);
        await Task.Delay(100);

        //capture just the area
        foreach (GameObject point in points) point.SetActive(false);
        filename = (currentNum + 1).ToString() + "_area.png";
        Capture(path + filename);
        await Task.Delay(100);

        //capture hand + area
        hcanvas.SetActive(true);
        filename = (currentNum + 1).ToString() + "_hand_area.png";
        Capture(path + filename);
        await Task.Delay(100);

        //capture hand + point
        foreach (GameObject go in Areas) go.SetActive(false);
        foreach (GameObject point in points) point.SetActive(true);
        filename = (currentNum + 1).ToString() + "_hand_point.png";
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
