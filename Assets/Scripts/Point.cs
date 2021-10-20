using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Point : MonoBehaviour
{
    public GameObject circlea;
    public GameObject circleb;
    public GameObject circlec;

    public GameObject cLineHorizontal;
    public GameObject cLineVertical;

    public GameObject mng;

    public Manager manager;

    private bool toucha;
    private bool touchb;
    private bool touchc;

    private float local_x;
    private float local_y;
    private float local_canvas_y;

    private Vector3 worldPosition;
    // Start is called before the first frame update
    void Start()
    {
        circlea.SetActive(false);
        circleb.SetActive(false);
        circlec.SetActive(false);
        cLineVertical.SetActive(false);
        cLineHorizontal.SetActive(false);
        manager = mng.GetComponent<Manager>();
        toucha = false;
        touchb = false;
        touchc = false;
        local_x = manager.local_x;
        local_y = manager.local_y;
        local_canvas_y = manager.local_canvas_y;
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
                if (mp.x < local_x && mp.y > local_y && mp.y < local_canvas_y)   //左上
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
                if (toucha)
                {
                    toucha = false;
                    cLineVertical.SetActive(false);
                    cLineHorizontal.SetActive(false);
                }
                if (touchb)
                {
                    touchb = false;
                    cLineVertical.SetActive(false);
                    cLineHorizontal.SetActive(false);
                }
                if (touchc)
                {
                    touchc = false;
                    cLineVertical.SetActive(false);
                    cLineHorizontal.SetActive(false);
                }
            }

            
            if (toucha) //もし左上がタッチされていたら
            {
                Vector3 mp = Input.mousePosition;
                mp.z = Camera.main.nearClipPlane + 1.0f;
                Vector3 worldMp = Camera.main.ScreenToWorldPoint(mp);
                
                if (mp.x < local_x && mp.y > local_y) circlea.GetComponent<Transform>().position = worldMp;
                else circlea.SetActive(false);

                if(circleb.activeSelf) //側面図が選択済みなら側面図を動かす。
                {
                    Vector3 bwp = circleb.GetComponent<Transform>().position;
                    bwp.x = worldMp.x;
                    circleb.GetComponent<Transform>().position = bwp;
                    cLineVertical.SetActive(true);
                    cLineVertical.GetComponent<Transform>().position = new Vector3(worldMp.x,0, 0);
                }
            }
            if (touchb) //もし左下がたっちされていたら
            {
                Vector3 mp = Input.mousePosition;
                mp.z = Camera.main.nearClipPlane + 1.0f;
                Vector3 worldMp = Camera.main.ScreenToWorldPoint(mp);

                if (mp.x < local_x && mp.y < local_y)
                {
                    
                    if (circlea.activeSelf) //上面点が選択済みなら補助線をひく
                    {
                        cLineVertical.SetActive(true);
                        float ax = circlea.GetComponent<Transform>().position.x;
                        cLineVertical.GetComponent<Transform>().position = new Vector3 (ax,0,0);
                        worldMp.x = ax;
                    }
                    circleb.GetComponent<Transform>().position = worldMp;
                }
                    
                else circleb.SetActive(false);

                if (circlec.activeSelf) //正面図が選択済みなら側面図を動かす。
                {
                    Vector3 cwp = circlec.GetComponent<Transform>().position;
                    cwp.y = worldMp.y;
                    circlec.GetComponent<Transform>().position = cwp;
                    cLineHorizontal.SetActive(true);
                    cLineHorizontal.GetComponent<Transform>().position = new Vector3(0, worldMp.y, 0);
                }
            }
            if (touchc)
            {
                Vector3 mp = Input.mousePosition;
                mp.z = Camera.main.nearClipPlane + 1.0f;
                Vector3 worldMp = Camera.main.ScreenToWorldPoint(mp);

                if (mp.x > local_x && mp.y < local_y)
                {
                    if (circleb.activeSelf) //側面点が選択済みなら補助線をひく
                    {
                        cLineHorizontal.SetActive(true);
                        float by = circleb.GetComponent<Transform>().position.y;
                        cLineHorizontal.GetComponent<Transform>().position = new Vector3(0, by, 0);
                        worldMp.y = by;
                    }
                    circlec.GetComponent<Transform>().position = worldMp;
                }
                else circlec.SetActive(false);
            }
        }
    }
}
