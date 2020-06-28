using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndoRedo:MonoBehaviour
{
    private List<GameObject> undoList;
    private List<GameObject> redoList;

    
    // Start is called before the first frame update
    void Start()
    {
        undoList = new List<GameObject>();
        redoList = new List<GameObject>();

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
    }

    public void AddObject(GameObject obj)
    {
        undoList.Add(obj);
        foreach (GameObject go in redoList)
        {
            Destroy(go);
        }
        redoList.Clear();   //何か書かれた後は、もうredoできない
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
