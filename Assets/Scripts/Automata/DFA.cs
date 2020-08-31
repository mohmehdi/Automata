using System;
using System.Collections.Generic;
using UnityEngine;
class DFA : Automata
{
    private readonly Dictionary<int,DState> _states;
    private DState _start = null;
    //private DState _start;
    public DFA()
    {
        _states = new Dictionary<int, DState>();
        BuildStateEvents.Instance.OnCreateState += OnAddState;
        BuildStateEvents.Instance.OnDeleteState += OnDeleteState;
        BuildStateEvents.Instance.OnChangeStatus += ChangeStatus;
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
        foreach (var s in _states)
        {
            s.Value.DisConnect(_states[id]);
        }
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
    public override bool TryDisConnect(int from, string tag)
    {
        return _states[from].DisConnect(tag);
    }    
    public override void ChangeStatus(int id, Status status)
    {
        if (status == Status.START || status == Status.STARTANDFINAL)
        {
            _start = _states[id];
            foreach (var s in _states)
            {
                if (s.Value.Status == Status.START)
                {
                    s.Value.Status = Status.NORMAL;
                    break;
                }
                if (s.Value.Status == Status.STARTANDFINAL)
                {
                    s.Value.Status = Status.FINAL;
                    break;
                }
            }
        }
        _states[id].Status = status;
    }
    public bool CheckInput(string inp)
    {
        DState current = _start;
        for (int i = 0; i < inp.Length; i++)
        {
            current = current.GetNextState(inp[i].ToString());
        }
        return current.Status == Status.FINAL ? true : current.Status == Status.STARTANDFINAL ? true : false;
    }
    public void CheckForComplete()
    {

        bool hasStart = false;
        var alphabet = AutomataManager.Alphabet;
        foreach (var s in _states)
        {
            if (s.Value.Status==Status.START)
            {
                hasStart = true;
            }
            for (int i = 0; i < alphabet.Length; i++)
            {
                if (!s.Value.ContainTag(alphabet[i]))
                {
                    Debug.LogWarning("state :: " + s.Key + " :: doesnt contain :: " + alphabet[i]+" ::");
                }
            }
        }
        if(!hasStart)
        Debug.LogWarning("DFA needs a 'Start' state");

    }
}