using System;
using System.Collections.Generic;
using UnityEngine;
class DFA : Automata
{
    private List<State> _states;
    private State _start;
    public DFA(string[] alphabet)
    {
        _states = new List<State>();
        Alphabet = alphabet;
        BuildStateEvents.Instance.OnCreateState += OnAddState;
        ConnectionEvents.Instance.OnSecondStateSelected += OnConnect;
    }
    protected override void OnAddState()
    {
        int id = AutomataManager.CurrentStateId;
        _states.Add(new State(id));
       // Debug.Log("DFA id"+id);
    }
    private void OnConnect()
    {
        int first = ConnectionEvents.Instance.firstStateID.ID;
        int second = ConnectionEvents.Instance.secondStateID.ID;

       Connect(first, second,   GetTag());
    }
    protected override string GetTag()//TODO : Get From alphabet and input
    {
        string tag = "a";
        return tag;
    }
    private void Connect(int first, int second, string tag)
    {
        State from = null, to = null;
        foreach (var s in _states)
        {
            if (s.StateID == first)
            {
                from = s;
            }
            if (s.StateID == second)
            {
                to = s;
            }
        }
        if (from == null || to == null)
        {
            Debug.Log("idk why this can happen yet");
            return;
        }

        var lastState = from.GetNextStates(tag);
        if (lastState.Count > 1)
        {
            Debug.LogError("HOLY FUCK WHAT THE HELL?");
            return;
        }
        else if (lastState.Count == 1)
        {
            from.Disconnect(tag, lastState[0]);
            ConnectionData label = new ConnectionData(tag, lastState[0]);
            ConnectionEvents.Instance.DeleteLastConnection(from, label);
            from.Connect(tag, to);
        }
        else
        {
            from.Connect(tag, to);
        }
    }
    
    
    protected override void OnDeleteState()
    {
        throw new NotImplementedException();
    }


}