using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
class DFA : Automata
{
    private List<State> _states;
    private State _start;
    public DFA(char[] alphabet)
    {
        _states = new List<State>();
        Alphabet = alphabet;
        StateCreationEvents.Instance.OnStateCreated += OnAddState;
    }

    protected override void OnAddState()
    {
        int id = AutomataManager.CurrentStateId;
        _states.Add(new State(id));
        Debug.Log("DFA id"+id);
    }

    protected override void OnDeleteState()
    {
        throw new NotImplementedException();
    }
}