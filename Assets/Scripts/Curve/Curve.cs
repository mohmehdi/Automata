﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Curve:MonoBehaviour
{
    [SerializeField] private Transform[] control = null;
    [SerializeField] private GameObject syntaxyInptField =null;
    [SerializeField] private LineRenderer line =null;
    [SerializeField] private Transform direction = null;

    private SyntaxyInputFieldInitializer _inputFieldInit;
    private LineRenderer _curve;
    private StateObject _fromID = null;
    private StateObject _toID = null;

    //TODO: add some id
    /// <summary>
    ///some id that gets from Automata get somthing method like for DFA: DFA.GetLabel() -> a
    /// or for PDA : PDA.get() -> a/b/z
    /// </summary>
    private void Start()
    {
        _inputFieldInit = GetComponent<SyntaxyInputFieldInitializer>();
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
        _inputFieldInit.SetPosition( control[2].position);
        }
        RenderCurve();

        line.SetPosition(0, control[1].position);
        line.SetPosition(1, control[3].position);
    }
    private void RenderCurve()
    {
        CurveLineRenderer.SetCurvePositions(_curve, control);

        //set arrow position
        direction.position = _curve.GetPosition(_curve.positionCount-1);

        //set arrow rotation
        Vector2 v = (_curve.GetPosition(_curve.positionCount - 1) - _curve.GetPosition(_curve.positionCount-3)).normalized;
        float degree = Mathf.Rad2Deg * Mathf.Atan(v.y / v.x);
        degree = v.x < 0 ? degree + 180 : degree;
       // Debug.Log(v.x + "  " + v.y + "  " + degree);
        direction.transform.rotation=  Quaternion.Euler(0, 0, degree); 
    }

    public void SetFrom()
    {
        _fromID = ConnectionEvents.Instance.firstStateID;
        control[0].position = _fromID.gameObject.transform.position;
        control[0].SetParent(_fromID.transform);
    }
    private void SetMiddleInBetween()
    {
        control[2].position = control[4].position + (control[0].position - control[4].position) / 2;
    }
    public void SetTo()
    {
        _toID = ConnectionEvents.Instance.secondStateID;

        control[4].position = _toID.gameObject.transform.position;
        control[4].SetParent(_toID.transform);
        _inputFieldInit.Initialize(syntaxyInptField);
        _inputFieldInit.SetInputFieldOptions(_fromID.ID, _toID.ID);
    }
    
    private void DeleteWhenStateDeleted(int id)
    {
        if (id == _fromID.ID || id == _toID.ID)
        {
            DestroyThisCurve();
        }
    }

    private void DestroyThisCurve()
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
