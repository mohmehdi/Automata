using System;
using System.Collections.Generic;
using UnityEngine;
class DFA
{
    private string[] _alphabet;
    private List<State> _states;
    private State _start;
    public DFA(string[] alphabet)
    {
        _states = new List<State>();
        _alphabet = alphabet;
    }

    public void AddState()
    {
        _states.Add(new State());
    }
    public void DeleteState(int index)
    {
        _states.Remove(_states[index]);
    }
    public void Connect(int a, int b, string label)
    {
        _states[a].Connect(label, _states[b]);
    }
    public void Disconnect(int a , string label)
    {
        _states[a].Disconnect(label);
    }
    public bool Accepts(string input)
    {

        State current = _start;
        if (current == null)
        {
            Debug.LogError("DFA dosent have start");
            return false;
        }
        for (int i = 0; i < input.Length; i++)
        {
            if (current == null)
            {
                Debug.Log("Set all ");
                return false;
            }
            if (current.ContainKey(input[i].ToString()))
            {
                current = current.GetNextState(i.ToString());
            }
            else
            {
                Debug.LogError("DFA not completed");
            }
        }
        if (current != null && current.Status == Status.FINAL)
        {
            //  Debug.Log("--------------Accept----------------");
            return true;
        }
        // Debug.Log("--------------Not Accept----------------");
        return false;

    }
}