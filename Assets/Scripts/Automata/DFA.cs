using System.Collections.Generic;
using UnityEngine;
class DFA : Automata
{
    public DFA()
    {
        _states = new Dictionary<int, DState>();
        SubscribeEvents();
        IsAnswerReady = false;
    }
    public override void StartCheckingInput(string input)
    {
        IsAnswerReady = false;
        Result = CheckInput(input);
        IsAnswerReady = true;
    }
    public  bool CheckInput(string inp)
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
        if (current == null)
            return false;

        return current.IsFinal();
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