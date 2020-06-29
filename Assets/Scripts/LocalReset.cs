using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class LocalReset : MonoBehaviour
{

    private List<GameObject> topArea;
    private List<GameObject> sideArea;
    private List<GameObject> frontArea;

    // Start is called before the first frame update
    public void AddToTop(GameObject obj)
    {
        topArea.Add(obj);
    }
    public void AddToSide(GameObject obj)
    {
        sideArea.Add(obj);
    }
    public void AddToFront(GameObject obj)
    {
        frontArea.Add(obj);
    }

    void Start()
    {
        topArea = new List<GameObject>();
        sideArea = new List<GameObject>();
        frontArea = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
