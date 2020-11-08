using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    //public int exp_num = 1;
    private bool area;
    private bool point;
    private int subjectNum = 0;//被験者番号
    private string option = "Practice";  //刺激位置
    private int currentNum = 0;
    private string inputBoxTxt;

    public float x_line;
    public float y_line;
    public float local_x;
    public float local_y;
    public GameObject linex;
    public GameObject liney;
    public GameObject inputText;
    public GameObject optionText;
    public GameObject currentNumText;
    public List<GameObject> electrodes;
    //access instances
    public void ClearElectrodes()
    {
        foreach(GameObject el in electrodes)
        {
            el.SetActive(false);
        }
    }
    public void ShowElectrodes(string option_in)
    {
        switch (option_in)
        {
            case "ulnar_wrist":
                ClearElectrodes();
                electrodes[0].SetActive(true);
                break;
            case "ulnar_elbow":
                ClearElectrodes();
                electrodes[1].SetActive(true);
                break;
            case "ulnar_armpit":
                ClearElectrodes();
                electrodes[2].SetActive(true);
                break;
            case "median_wrist":
                ClearElectrodes();
                electrodes[3].SetActive(true);
                break;
            case "median_elbow":
                ClearElectrodes();
                electrodes[4].SetActive(true);
                break;
            case "median_armpit":
                ClearElectrodes();
                electrodes[5].SetActive(true);
                break;
            default:
                ClearElectrodes();
                break;
        }
    }
    public string Option()
    {
        return option;
    }
    public int SubjectNum()
    {
        subjectNum = Int32.Parse(inputBoxTxt);
        return subjectNum;
    }
    public int CurrentNum()
    {
        return currentNum;
    }
    public string InputBox()
    {
        return inputBoxTxt;
    }
    public void UpdateCurrentNum(int i)
    {
        currentNum = i;
        currentNumText.GetComponent<Text>().text = (currentNum+1).ToString();
    }
    public bool IsArea()
    {
        return area;
    }
    public bool IsPoint()
    {
        return point;
    }

    //Interaction w/ UI

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
    public void OnUpdateNum()
    {
        inputBoxTxt = inputText.GetComponent<Text>().text;
        //UnityEngine.Debug.Log(num);
    }

    public void OnClickUp()
    {
        if(currentNum < 4) currentNum++;
        currentNumText.GetComponent<Text>().text = (currentNum+1).ToString();
    }
    public void OnClickDown()
    {
        if (currentNum > 0) currentNum--;
        currentNumText.GetComponent<Text>().text = (currentNum+1).ToString();
    }
    public void OnChangeOption()
    {
        option = optionText.GetComponent<Text>().text;
        UpdateCurrentNum(0);
        ShowElectrodes(option);
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
