using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndoRedo:MonoBehaviour
{
    private List<GameObject> undoList;
    private List<GameObject> redoList;
    private List<GameObject> areasAll;
    private List<GameObject> topArea;
    private List<GameObject> sideArea;
    private List<GameObject> frontArea;

    // Start is called before the first frame update
    void Start()
    {
        undoList = new List<GameObject>();
        redoList = new List<GameObject>();
        areasAll = new List<GameObject>();
        topArea = new List<GameObject>();
        sideArea = new List<GameObject>();
        frontArea = new List<GameObject>();
    }

    //access instances
    public void ResetInstances()
    {
        undoList = new List<GameObject>();
        redoList = new List<GameObject>();
        areasAll = new List<GameObject>();
        topArea = new List<GameObject>();
        sideArea = new List<GameObject>();
        frontArea = new List<GameObject>();
    }
    public List<GameObject> AreasAll()
    {
        return areasAll;
    }

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
    public void OnResetTop()
    {
        List<GameObject> tmp = new List<GameObject>();
        foreach (GameObject area in areasAll)
        {
            if (topArea.Contains(area))
            {
                redoList.Add(area);
                area.SetActive(false);
                tmp.Add(area);
            }
        }
        foreach (GameObject area in tmp) areasAll.Remove(area);
        tmp = new List<GameObject>();
        foreach (GameObject area in undoList)
        {
            if (topArea.Contains(area))
            {
                tmp.Add(area);
            }
        }
        foreach (GameObject area in tmp) undoList.Remove(area);
    }
    public void OnResetSide()
    {
        List<GameObject> tmp = new List<GameObject>();
        foreach (GameObject area in areasAll)
        {
            if (sideArea.Contains(area))
            {
                redoList.Add(area);
                area.SetActive(false);
                tmp.Add(area);
            }
        }
        foreach (GameObject area in tmp) areasAll.Remove(area);
        tmp = new List<GameObject>();
        foreach (GameObject area in undoList)
        {
            if (sideArea.Contains(area))
            {
                tmp.Add(area);
            }
        }
        foreach (GameObject area in tmp) undoList.Remove(area);
    }
    public void OnResetFront()
    {
        List<GameObject> tmp = new List<GameObject>();
        foreach (GameObject area in areasAll)
        {
            if (frontArea.Contains(area))
            {
                redoList.Add(area);
                area.SetActive(false);
                tmp.Add(area);
            }
        }
        foreach (GameObject area in tmp) areasAll.Remove(area);
        tmp = new List<GameObject>();
        foreach (GameObject area in undoList)
        {
            if (frontArea.Contains(area))
            {
                tmp.Add(area);
            }
        }
        foreach (GameObject area in tmp) undoList.Remove(area);
    }

    public void OnClickUndo()
    {
        int undoLength = undoList.Count;
        GameObject last;
        if (undoLength == 0) return;
        last = undoList[undoLength - 1];
        last.SetActive(false);
        undoList.Remove(last);
        redoList.Add(last);
        areasAll.Remove(last);
    }

    public void OnClickRedo()
    {
        int redoLength = redoList.Count;
        GameObject last;
        if (redoLength == 0) return;
        last = redoList[redoLength - 1];
        last.SetActive(true);
        redoList.Remove(last);
        undoList.Add(last);
        areasAll.Add(last);
    }

    public void AddObject(GameObject obj)
    {
        undoList.Add(obj);
        foreach (GameObject go in redoList)
        {
            Destroy(go);
        }
        redoList.Clear();   //何か書かれた後は、もうredoできない
        areasAll.Add(obj);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
