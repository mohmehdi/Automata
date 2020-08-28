﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Curve:MonoBehaviour
{
    [SerializeField] private Transform[] control;
    [SerializeField] private GameObject dropDownPrefab;
    [SerializeField] private LineRenderer line;
    private DropDownSetup dropDownSetup;

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
        dropDownSetup = GetComponent<DropDownSetup>();
        _curve = GetComponent<LineRenderer>();
        for (int i = 0; i < control.Length; i++)
        {
            if (control[i] == null)
            {
                Debug.Log("Set Curve Control points " + i.ToString());
            }
        }

        dropDownSetup.Initialize(dropDownPrefab, UIManager.Instance.transform);
    }
    private void Update()
    {
        if (!_toID) //if second state still not selected . set second position to mousePos
        {
            control[4].position = MousePosition.GetMousePosition();
            SetMiddleInBetween();
        }
        RenderCurve();
        dropDownSetup.SetPosition(MousePosition.GetCamera().WorldToScreenPoint( GetMiddlePoint()));

        line.SetPosition(0, control[1].position);
        line.SetPosition(1, control[3].position);
    }
    private void RenderCurve()
    {
        CurveLineRenderer.SetCurvePositions(_curve, control);
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
        dropDownSetup.SetDropDownFromAndTo(_fromID.ID, _toID.ID);
    }
    
    private Vector2 GetMiddlePoint()
    {
        return _curve.GetPosition(_curve.positionCount / 2);
    }
}
