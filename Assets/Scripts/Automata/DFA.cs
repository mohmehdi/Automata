using System.Collections.Generic;
using UnityEngine;
class DFA : Automata
{
    private readonly Dictionary<int,DState> _states;
    private DState _start = null;
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
    }
    protected override void OnDeleteState(int id)
    {
        foreach (var s in _states)
        {
            s.Value.RemoveConnectionsTo(_states[id]);
        }
        _states.Remove(id);
    }
    public override bool TryConnect(int from , string tag ,int to)
    {
        return _states[from].TryConnect(tag, _states[to]);
    }
    public override void RemoveConnections(int from,int to)
    {
        if (!_states.ContainsKey(from))
        {
            Debug.Log("Not Found");
        }
        if (!_states.ContainsKey(to))
        {
            Debug.Log("Not Found");
        }
        _states[from].RemoveConnectionsTo(_states[to]);
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
        if( DeterministicCheck() == false) return false; //DFA needs a Start and must be deterministic depends on alphabet

        DState current = _start;
        for (int i = 0; i < inp.Length; i++)
        {
            if (current == null)
            {
                Debug.Log("Current Dosent exitst during InputCheck calculation");
                return false;
            }
            current = current.GetNextState(inp[i].ToString());
        }
        return current.Status == Status.FINAL ? true : current.Status == Status.STARTANDFINAL ? true : false;
    }
    public bool DeterministicCheck()
    {
        if (!(_start != null))
        {
            Debug.LogWarning("DFA needs a 'Start' state");
            return false;
        }

        var alphabet = AutomataManager.inputAlphabet;
        foreach (var s in _states)
        {
            for (int i = 0; i < alphabet.Length; i++)
            {
                if (!s.Value.ContainTag(alphabet[i].ToString()))
                {
                    Debug.LogWarning("state :: " + s.Key + " :: doesnt contain :: " + alphabet[i]+" ::");
                    return false;
                }
            }
        }
        return true;

    }
}