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
}
