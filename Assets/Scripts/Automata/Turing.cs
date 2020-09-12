using System.Collections.Generic;
using UnityEngine;

public class Turing : Automata
{
    private TuringHelper _helper;

    public Turing()
    {
        _helper = AutomataManager.Instance.GetComponent<TuringHelper>();
        if (_helper==null)
        {
            Debug.LogWarning("Automata manager need a TuringHelper Component");
        }
        _states = new Dictionary<int, DState>();
        SubscribeEvents();
    }
    
    public override void StartCheckingInput(string input)
    {
        IsAnswerReady = false;
        _helper.StartChecking(input,this);
    }
    public DState GetStartState()
    {
        return _start;
    }
    public DState GetNextState(DState current,char underhead)
    {
        var tags = current.GetTags(underhead);
        if (tags== null)
            return null;

        return current.GetNextState(tags[0].GetAllTogether());
    }
    public override void StopChechingInput()
    {
        _helper.StopChecking();
    }
    public bool DeterministicCheck()
    {
        if (_start == null)
        {
            Debug.LogWarning("DPDA needs a 'Start' state");
            return false;
        }
        return true;
    }
}
