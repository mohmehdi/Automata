using System;
using DefaultNamespace;
using UnityEngine;

public class Curve:MonoBehaviour
{
    [SerializeField] private Transform from;
    [SerializeField] private Transform to;

    private LineRenderer _curve;
    private StateID _startState;
    private StateID _lastState;



    //TODO: add some id
    /// <summary>
    ///some id that gets from Automata get somthing method like for DFA: DFA.GetLabel() -> a
    /// or for PDA : PDA.get() -> a/b/z
    /// </summary>
    private void Start()
    {
        _curve = GetComponent<LineRenderer>();
        if (!from || !to)
        {
            Debug.Log("Set from and to transforms in Curve Prefab");
            return;
        }
    }
    public void SetFrom()
    {
        _startState = StateConnectionEvents.Instance.firstStateID;
        from.position = _startState.gameObject.transform.position;
        from.SetParent(_startState.transform);
    }
    public void SetTo()
    {
        _lastState = StateConnectionEvents.Instance.secondStateID;
        to.position = _lastState.gameObject.transform.position;
        to.SetParent(_lastState.transform);
    }

}
