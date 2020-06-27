using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security;
using System.Security.Cryptography;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private bool area;
    private bool point;
    public float x_line;
    public float y_line;
    public float local_x;
    public float local_y;
    public GameObject linex;
    public GameObject liney;


    public bool IsArea()
    {
        return area;
    }
    public bool IsPoint()
    {
        return point;
    }

    public void OnClickArea()
    {
        area = true;
        point = false;
    }
    public void OnClickPoint()
    {
        UnityEngine.Debug.Log("point");
        area = false;
        point = true;
    }
    // Start is called before the first frame update
    void Awake()
    {
        area = false;
        point = false;
        local_x = Screen.width * x_line;
        local_y = Screen.height* y_line;

        Vector3 lpx = new Vector3 (0, local_y, Camera.main.nearClipPlane + 3.0f);
        Vector3 lpy = new Vector3(local_x , 0, Camera.main.nearClipPlane + 1.0f);
        Vector3 wpx = Camera.main.ScreenToWorldPoint(lpx);
        Vector3 wpy = Camera.main.ScreenToWorldPoint(lpy);
        wpx.x = 0;
        wpy.y = 0;
        linex.GetComponent<Transform>().position = wpx;
        liney.GetComponent<Transform>().position = wpy;
        //UnityEngine.Debug.Log(Camera.main.ScreenToWorldPoint(lpx));
        //UnityEngine.Debug.Log(Camera.main.ScreenToWorldPoint(lpy));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
