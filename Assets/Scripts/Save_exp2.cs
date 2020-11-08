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

    private string hpath = "C:/Users/yahoo/cyberlab/thesis/exp2/pre/";
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
        string lpath = manager.InputBox();
        DateTime dt = DateTime.Now;
        string result = dt.ToString("MM_dd_");
        string path = hpath + result + lpath;
        string filename;
        Areas = ud.AreasAll();
        
        Debug.Log(path);

        Clean(true);
        //capture all
        filename = ".png";
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
