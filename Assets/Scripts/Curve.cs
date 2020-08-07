using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Curve
{
    public State From { get; set; }
    public State To { get; set; }
    public string Label { get; set; }
}
public class CurveUI
{
    private Text _text;
    private Image _controlPointImg;
    public CurveUI(Text text)
    {
        _text = text;
    }
    private void SetControlPointImage()
    {
       // _controlPointImg.transform.position = mainCam.WorldToScreenPoint(line.GetPosition(quality / 2));
    }
    private void EnableControlPoints(bool enable)
    {
        //_controlPointImg.enabled = enable;
    }

}
public class ControlPoint
{
    private Transform[] _controlPoint = new Transform[3];

    public void OnSetFrom(Transform from)
    {
        //set position and parent
    }
    public void OnSetTo(Transform to)
    {
        //set position and parent
    }
}
public class CurveControler
{
    private Transform[] _controlPoint = new Transform[3];

    private void EditControls()
    {
    //    if (  Vector2.Distance(mainCam.ScreenToWorldPoint(Input.mousePosition), line.GetPosition(quality / 2)) < areaOfccet)
    //    {
    //        if (Input.GetMouseButtonDown(0))
    //        {
    //            selected = 1;
    //            isSelected = true;
    //        }
    //    }
    //    else if (selected != -1)
    //    {
    //        if (Input.GetMouseButton(0))
    //        {
    //            if (Input.GetKeyDown(KeyCode.X))
    //            {
    //                GameObject.FindGameObjectWithTag("DFA").GetComponent<DFAManager>().DisConnect(controlPoint[0].position, label);
    //                isSelected = false;
    //                Destroy(gameObject);
    //            }
    //            Vector3 vv = (Vector3)controlPoint[selected].position - line.GetPosition(quality / 2);
    //            controlPoint[selected].position = mainCam.ScreenToWorldPoint(new Vector2(Mathf.Clamp(Input.mousePosition.x, 0, mainCam.pixelWidth), Mathf.Clamp(Input.mousePosition.y, 0, mainCam.pixelHeight))) + vv;

    //            controlPointImg.transform.position = mainCam.WorldToScreenPoint(line.GetPosition(quality / 2));
    //        }
    //        if (Input.GetMouseButtonUp(0))
    //        {
    //            selected = -1;
    //            isSelected = false;
    //        }
    //    }
    }
}
public class CurveRenderer
{
    private LineRenderer _line;
    private int _quality;

    public CurveRenderer(LineRenderer line,int quality)
    {
        _line = line;
        _quality = quality;
    }
    private Vector2 Lerp(Vector2 a, Vector2 b, float t)
    {
        return a + (b - a) * t;
    }
    private Vector2 QuadricCurve(Vector2 a, Vector2 b, Vector2 c, float t)
    {
        Vector2 p0 = Lerp(a, b, t);
        Vector2 p1 = Lerp(b, c, t);
        return Lerp(p0, p1, t);
    }
    public void SetCurvePointsPosition(Transform[] points)
    {
        for (int i = 0; i <_quality; i++)
        {
            _line.SetPosition(i, QuadricCurve(points[0].position, points[1].position, points[2].position, i * ((float)1 / (_quality - 1))));
        }
    }
}
public class CurveManager : MonoBehaviour
{
    public static bool editMode = false;
    private static bool isSelected = false;
    
    private Camera mainCam;
    [SerializeField] private LineRenderer line;
    [SerializeField] private Image controlPointImg;
    [SerializeField] private Transform[] controlPoint = new Transform[3];
    [SerializeField] private Text text;
    [SerializeField] private int quality;

    private CurveRenderer _curveRenderer;
    public float areaOfccet = 0.4f;


    private int selected = -1;
    private void Start()
    {
        mainCam = Camera.main;
        _curveRenderer = new CurveRenderer(line, quality);
    }
    private void Update()
    {
       
    }

    public void DeleteLastCurve(Vector3 f, string l)
    {
        //if (c != this)
        //{
        //    if (controlPoint[0].position == f)
        //    {
        //        if (l == label)
        //        {
        //            Destroy(gameObject);
        //        }
        //    }
        //}
    }
}
