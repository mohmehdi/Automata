using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class Curve:MonoBehaviour
{
    [SerializeField] private Transform[] control;

    private Camera _camera;//TODO: this shit should be removed cz used alot in every class

    private LineRenderer _curve;
    private StateObjectID _startState = null;
    private StateObjectID _lastState = null;
    private string _tag = "a";

    //TODO: add some id
    /// <summary>
    ///some id that gets from Automata get somthing method like for DFA: DFA.GetLabel() -> a
    /// or for PDA : PDA.get() -> a/b/z
    /// </summary>
    private void Start()
    {
        _camera = Camera.main;
        _curve = GetComponent<LineRenderer>();
        for (int i = 0; i < control.Length; i++)
        {
            if (control[i] == null)
            {
                Debug.Log("Set Curve Control points "+ i.ToString());
            }
        }
        ConnectionEvents.Instance.OnDeleteLastConnection += OnDeleteThis;
    }
    private void Update()
    {
        if (!_lastState)
        {
            control[4].position = GetMousePosition();
            SetMiddle();
        }
        RenderCurve();
    }
    private void RenderCurve()
    {
        CurveLineRenderer.SetCurvePositions(_curve, control);
    }

    public void SetFrom()
    {
        _startState = ConnectionEvents.Instance.firstStateID;
        control[0].position = _startState.gameObject.transform.position;
        control[0].SetParent(_startState.transform);
    }
    private void SetMiddle()
    {
        control[2].position = control[4].position + (control[0].position - control[4].position) / 2;
    }
    public void SetTo()
    {
        _lastState = ConnectionEvents.Instance.secondStateID;
        control[4].position = _lastState.gameObject.transform.position;
        control[4].SetParent(_lastState.transform);
    }
    private Vector3 GetMousePosition() //TODO: Remove this ASAP ASSHOLE
    {
        var mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector3(mousePos.x, mousePos.y, 0);
        return mousePos;
    }
    
    private void OnDeleteThis(State from,ConnectionData label)
    {
        Debug.Log(gameObject);
        Debug.Log(CurveCreator.CurrentCurveHash);
         if(gameObject.GetHashCode() == CurveCreator.CurrentCurveHash) return;

        if (from.StateID == _startState.ID &&
            label.Tag == _tag &&
            label.To.StateID == _lastState.ID )
        {

            Destroy(control[0].gameObject);
            Destroy(control[4].gameObject);
            Destroy(gameObject);
        }
    }
    public void OnDestroy()
    {
        if (!ConnectionEvents.Instance) return;
        ConnectionEvents.Instance.OnDeleteLastConnection -= OnDeleteThis;
    }
}
