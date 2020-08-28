using System;
using System.Collections.Generic;
using UnityEngine;
class DFA : Automata
{
    private readonly Dictionary<int,DState> _states;
    //private DState _start;
    public DFA()
    {
        _states = new Dictionary<int, DState>();
        BuildStateEvents.Instance.OnCreateState += OnAddState;
        BuildStateEvents.Instance.OnDeleteState += OnDeleteState;
    }
    protected override void OnAddState()
    {
        int id = AutomataManager.CurrentStateId;
        DState newState = new DState();
        _states.Add(id,newState);
        Debug.Log("State " + id + " added");
    }
    protected override void OnDeleteState(int id)
    {
        _states.Remove(id);
    }

    public override bool TryConnect(int from , string tag ,int to)
    {
        bool res = _states[from].TryConnect(tag, _states[to]);
        if (res)
        {
            Debug.Log(("Sucsesfuly connected : " + from + " with :" + tag + " to : " + to));
        }
        else
        {
            Debug.LogError(("Cannot connec : " + from + " with :" + tag + " to : " + to));
        }
        return res;
    }
    public override bool TryDisConnect(int from, string tag, int to)
    {
        return _states[from].Disconnect(tag, _states[to]);
    }    

    public override void ChangeStatus(int id, Status status)
    {
        if (status == Status.START)
        {
            foreach (var s in _states)
            {
                if (s.Value.Status == Status.START)
                {
                    s.Value.Status = Status.NORMAL;
                    break;
                }
            }
        }
        _states[id].Status = status;
    }
}