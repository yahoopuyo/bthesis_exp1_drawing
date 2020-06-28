using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.Threading.Tasks;

public class Save : MonoBehaviour
{
    public GameObject dcanvas;
    public List<GameObject> points;

    private List<GameObject> topArea;
    private List<GameObject> sideArea;
    private List<GameObject> frontArea;

    private List<GameObject> Areas;

    private Manager manager;

    private string hpath = "C:/Users/yahoo/cyberlab/thesis/exp1_record/Record/subject";
    private string lpath;

    

    private void Reset()
    {
        foreach(GameObject point in points)
        {
            point.SetActive(false);
        }
        foreach (GameObject go in Areas)
        {
            Destroy(go);
        }
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
        string filename = (currentNum+1).ToString() + "_image.png";
        Debug.Log(path);
        if (currentNum < 4)
        {
            manager.UpdateCurrentNum(currentNum + 1);
        }
        else
        {
            manager.UpdateCurrentNum(0);
        }

        Clean(true);
        Capture(path + filename);
        await Task.Delay(100);
        Reset();
        Clean(false);     
    }

    // Start is called before the first frame update
    void Start()
    {
        topArea = new List<GameObject>();
        sideArea = new List<GameObject>();
        frontArea = new List<GameObject>();
        Areas = new List<GameObject>();
        manager = GetComponent<Manager>();

       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
