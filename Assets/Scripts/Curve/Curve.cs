using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Curve:MonoBehaviour
{
    /// <summary>
    ///[0]fromObject , [1]helper_0 , [2]middle , [3]helper_1 , [4]toObject
    /// </summary>
    [SerializeField] private Transform[] control = null;
    [SerializeField] private GameObject syntaxyInptField =null;
    [SerializeField] private LineRenderer helpersLine =null;
    [SerializeField] private Transform arrowSprite = null;

    private EdgeTagHandlerInitializer _inputFieldInit;
    private LineRenderer _curve;
    private StateObject _fromID = null;
    private StateObject _toID = null;

    private void Start()
    {
        _inputFieldInit = GetComponent<EdgeTagHandlerInitializer>();
        _curve = GetComponent<LineRenderer>();
        for (int i = 0; i < control.Length; i++)
        {
            if (control[i] == null)
            {
                Debug.Log("Set Curve Control points " + i.ToString());
            }
        }

        BuildStateEvents.Instance.OnDeleteState += DeleteWhenStateDeleted;
    }
    private void Update()
    {
        if (!_toID) //if second state still not selected . set second position to mousePos
        {
            control[4].position = MousePosition.GetMousePosition();
            SetMiddleInBetween();
        }
        else
        {
            _inputFieldInit.SetPosition(control[2].position);
        }
        RenderCurve();

        helpersLine.SetPosition(0, control[1].position);
        helpersLine.SetPosition(1, control[3].position);
    }
    private void RenderCurve()
    {
        CurveLineRenderer.SetCurvePositions(_curve, control);

        //set arrow position
        arrowSprite.position = _curve.GetPosition(_curve.positionCount-1);

        //set arrow rotation
        Vector2 v = (_curve.GetPosition(_curve.positionCount - 1) - _curve.GetPosition(_curve.positionCount-3)).normalized;
        float degree = Mathf.Rad2Deg * Mathf.Atan(v.y / v.x);
        degree = v.x < 0 ? degree + 180 : degree;
       // Debug.Log(v.x + "  " + v.y + "  " + degree);
        arrowSprite.transform.rotation=  Quaternion.Euler(0, 0, degree); 
    }

    public void SetFrom()
    {
        _fromID = ConnectionEvents.Instance.firstStateID;
        control[0].position = _fromID.gameObject.transform.position;
        control[0].SetParent(_fromID.transform);
    }
    private void SetMiddleInBetween()
    {
        //deffrence vector between 1st point and last point is a directional vector --> " base on (0,0,0) "
        //so add it to 1st point or last point means in between position
        control[2].position = control[4].position + (control[0].position - control[4].position) / 2;
    }
    public void SetTo()
    {
        _toID = ConnectionEvents.Instance.secondStateID;

        SetOptions();
    }

    private void SetOptions()
    {
        control[4].position = _toID.gameObject.transform.position;
        control[4].SetParent(_toID.transform);
        _inputFieldInit.Initialize(syntaxyInptField);
        _inputFieldInit.SetInputFieldOptions(_fromID.ID, _toID.ID);
    }

    private void DeleteWhenStateDeleted(int id)
    {
        if (id == _fromID.ID || id == _toID.ID)
        {
            DestroyThisCurveAndChilds();
        }
    }

    private void DestroyThisCurveAndChilds()
    {
        Destroy(control[0].gameObject);
        Destroy(control[4].gameObject);
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        BuildStateEvents.Instance.OnDeleteState -= DeleteWhenStateDeleted;
    }
}
