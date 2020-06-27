using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Point : MonoBehaviour
{
    public GameObject circlea;
    public GameObject circleb;
    public GameObject circlec;
    public GameObject mng;

    public Manager manager;

    private bool toucha;
    private bool touchb;
    private bool touchc;

    private float local_x;
    private float local_y;

    private Vector3 worldPosition;
    // Start is called before the first frame update
    void Start()
    {
        circlea.SetActive(false);
        circleb.SetActive(false);
        circlec.SetActive(false);
        manager = mng.GetComponent<Manager>();
        toucha = false;
        touchb = false;
        touchc = false;
        local_x = manager.local_x;
        local_y = manager.local_y;
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.IsPoint())
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 mp = Input.mousePosition;
                mp.z = Camera.main.nearClipPlane + 1.0f;
                Vector3 worldMp = Camera.main.ScreenToWorldPoint(mp);
                /*
                UnityEngine.Debug.Log(mp);
                UnityEngine.Debug.Log(local_x) ;
                UnityEngine.Debug.Log(local_y) ;
                */
                if (mp.x < local_x && mp.y > local_y)   //左上
                {
                    circlea.SetActive(true);
                    circlea.GetComponent<Transform>().position = worldMp;
                    toucha = true;
                    //UnityEngine.Debug.Log("in2");
                }
                if (mp.x < local_x && mp.y < local_y)   //左下
                {
                    circleb.SetActive(true);
                    circleb.GetComponent<Transform>().position = worldMp;
                    touchb = true;
                    //UnityEngine.Debug.Log("in2");
                }
                if (mp.x > local_x && mp.y < local_y)   //右下
                {
                    circlec.SetActive(true);
                    circlec.GetComponent<Transform>().position = worldMp;
                    touchc = true;
                    //UnityEngine.Debug.Log("in2");
                }



                //UnityEngine.Debug.Log(mp.x);
                //UnityEngine.Debug.Log(mp.y);
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (toucha) toucha = false;
                if (touchb) touchb = false;
                if (touchc) touchc = false;
            }

            
            if (toucha)
            {
                Vector3 mp = Input.mousePosition;
                mp.z = Camera.main.nearClipPlane + 1.0f;
                Vector3 worldMp = Camera.main.ScreenToWorldPoint(mp);
                
                if (mp.x < local_x && mp.y > local_y) circlea.GetComponent<Transform>().position = worldMp;
                else circlea.SetActive(false);
            }
            if (touchb)
            {
                Vector3 mp = Input.mousePosition;
                mp.z = Camera.main.nearClipPlane + 1.0f;
                Vector3 worldMp = Camera.main.ScreenToWorldPoint(mp);

                if (mp.x < local_x && mp.y < local_y) circleb.GetComponent<Transform>().position = worldMp;
                else circleb.SetActive(false);
            }
            if (touchc)
            {
                Vector3 mp = Input.mousePosition;
                mp.z = Camera.main.nearClipPlane + 1.0f;
                Vector3 worldMp = Camera.main.ScreenToWorldPoint(mp);

                if (mp.x > local_x && mp.y < local_y) circlec.GetComponent<Transform>().position = worldMp;
                else circlec.SetActive(false);
            }
        }
    }
}
