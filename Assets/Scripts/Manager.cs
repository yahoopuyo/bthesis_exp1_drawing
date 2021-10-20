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
    private int pair1Index = 0;
    // private int pair2Index = 0;

    private const int POS_MAX = 10;//実験２，位置条件の数(練習含めず)
    private string[] alphabets = { "P", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l" };
    
    public float x_line;
    public float y_line;
    public float y_canvas;//画面上のボタン、input fieldの識別用
    public float local_x;
    public float local_y;
    public float local_canvas_y;
    public GameObject linex;
    public GameObject liney;
    public GameObject inputText;
    public GameObject optionText;
    public GameObject currentNumText;
    public GameObject pair1Text;
    // public GameObject pair2Text;
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
    // for exp 2

    public string Pair1()
    {
        return alphabets[pair1Index];
    }
    public void ResetPair1()
    {
        pair1Index = 0;
        pair1Text.GetComponent<Text>().text = "P";
    }
    /*
    public string Pair2()
    {
        return alphabets[pair2Index];
    }
    */
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
    public void OnClickUpPair1()
    {
        if (pair1Index < POS_MAX) pair1Index++;
        pair1Text.GetComponent<Text>().text = alphabets[pair1Index];
    }
    public void OnClickDownPair1()
    {
        if (pair1Index > 0) pair1Index--;
        pair1Text.GetComponent<Text>().text = alphabets[pair1Index];
    }

    /*
    public void OnClickUpPair2()
    {
        if (pair2Index < POS_MAX) pair2Index++;
        pair2Text.GetComponent<Text>().text = alphabets[pair2Index];
    }
    public void OnClickDownPair2()
    {
        if (pair2Index > 0) pair2Index--;
        pair2Text.GetComponent<Text>().text = alphabets[pair2Index];
    }
    */

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
        local_canvas_y = Screen.height * y_canvas;

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
