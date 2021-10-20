using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using System.Diagnostics;
using System.Collections.Specialized;

public class Draw : MonoBehaviour
{

    /// <summary>
    /// 描く線のコンポーネントリスト
    /// </summary>
    private List<LineRenderer> lineRendererList;

    /// <summary>
    /// 描く線のマテリアル
    /// </summary>
    public Material lineMaterial;

    /// <summary>
    /// 描く線の色
    /// </summary>
    public Color lineColor;

    /// <summary>
    /// 描く線の太さ
    /// </summary>
    [Range(0, 1)] public float lineWidth;

    public GameObject slider;
    public GameObject mng;

    private Manager manager;
    private UndoRedo undoredo;

    private bool touch = false;

    private int width;
    private int height;
    private float slider_x;
    private float slider_y;
    private float canvas_y;
    private Vector3 worldPosition;
    void Start()
    {
        lineRendererList = new List<LineRenderer>();
        lineWidth = 0.05f;
        width = Screen.width;
        height = Screen.height;
        manager = mng.GetComponent<Manager>();
        undoredo = mng.GetComponent<UndoRedo>();
        slider_x = manager.local_x;
        slider_y = manager.local_y;
        canvas_y = manager.local_canvas_y;
        //UnityEngine.Debug.Log(width);
        //UnityEngine.Debug.Log(height);
       
        worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    
    public void OnChangedSlider()
    {
        lineWidth = slider.GetComponent<Slider>().value * 0.3f + 0.05f;
    }

    void Update()
    {
        
        // ボタンが押された時に線オブジェクトの追加を行う
        if (Input.GetMouseButtonDown(0) && manager.IsArea())
        {
            float x = Input.mousePosition.x;
            float y = Input.mousePosition.y;
            int a = x > slider_x ? 1 : 0;
            int b = y > slider_y ? 1 : 0;

            bool OnCanvas = y > canvas_y ? false : true;
            int select = a + 2 * b;//どこに触っているかを判断、四分割
            
            if (select != 3 && OnCanvas)
            {
                this.AddLineObject(select);
                touch = true;
            }
            worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            UnityEngine.Debug.Log(worldPosition.x);
            UnityEngine.Debug.Log(worldPosition.y);
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (touch) touch = false;
        }

        // ボタンが押されている時、LineRendererに位置データの設定を指定していく
        if (touch)
        {
            this.AddPositionDataToLineRendererList();
        }
    }

    /// <summary>
    /// 線オブジェクトの追加を行うメソッド
    /// </summary>
    private void AddLineObject(int pos)
    {

        // 追加するオブジェクトをインスタンス
        GameObject lineObject = new GameObject();

        //描画されているものを格納
        undoredo.AddObject(lineObject);
        
        //分別
        switch (pos)
        {
            case 0:
                undoredo.AddToSide(lineObject);
                break;
            case 1:
                undoredo.AddToFront(lineObject);
                break;
            case 2:
                undoredo.AddToTop(lineObject);
                break;
            default:
                break;
        }
        // オブジェクトにLineRendererを取り付ける
        lineObject.AddComponent<LineRenderer>();

        // 描く線のコンポーネントリストに追加する
        lineRendererList.Add(lineObject.GetComponent<LineRenderer>());

        // 線と線をつなぐ点の数を0に初期化
        lineRendererList.Last().positionCount = 0;

        // マテリアルを初期化
        lineRendererList.Last().material = this.lineMaterial;

        // 線の色を初期化
        lineRendererList.Last().material.color = this.lineColor;

        // 線の太さを初期化
        lineRendererList.Last().startWidth = this.lineWidth;
        lineRendererList.Last().endWidth = this.lineWidth;
    }

    /// <summary>
    /// 描く線のコンポーネントリストに位置情報を登録していく
    /// </summary>
    private void AddPositionDataToLineRendererList()
    {

        // 座標の変換を行いマウス位置を取得
        Vector3 screenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane + 100.0f);
        var mousePosition = Camera.main.ScreenToWorldPoint(screenPosition);

        // 線と線をつなぐ点の数を更新
        lineRendererList.Last().positionCount += 1;

        // 描く線のコンポーネントリストを更新
        lineRendererList.Last().SetPosition(lineRendererList.Last().positionCount - 1, mousePosition);
    }
}