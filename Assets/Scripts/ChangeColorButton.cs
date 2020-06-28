using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColorButton : MonoBehaviour
{
    //ここでカラーを設定
    static readonly Color btnColor1 = Color.white;
    static readonly Color btnColor2 = Color.blue;

    Button abtn;
    Button pbtn;

    Manager manager;
    public GameObject Area;
    public GameObject Point;

    // Start is called before the first frame update
    void Start()
    {
        abtn = Area.GetComponent<Button>();
        abtn.image.color = btnColor1;
        pbtn = Point.GetComponent<Button>();
        pbtn.image.color = btnColor1;
        manager = GetComponent<Manager>();
    }

    public void OnClicked()
    {
        pbtn.image.color = manager.IsPoint() ? btnColor2 : btnColor1;
        abtn.image.color = manager.IsArea() ? btnColor2 : btnColor1;

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
