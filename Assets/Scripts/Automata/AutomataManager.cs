using System;
using System.Collections.Generic;
using UnityEngine;

public enum AutomataType
{
    dfa,nfa,pda
}
public class AutomataManager : MonoBehaviour
{

    public static AutomataManager Instance;
    public static AutomataType automataType;
    public static string[] Alphabet;
    public static int CurrentStateId;
    private Automata _machine;
    DFA d;

    public Action<int,bool, bool> OnCheckInput;

    private void Start()
    {
        Instance = this;
        automataType = AutomataType.dfa;
        _machine = new DFA();
         d = (DFA)_machine;

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
            d.CheckForComplete();
        if (Input.GetKeyDown(KeyCode.CapsLock))
        {
            List<string> vs = UIManager.Instance.GetInputs();
            for (int i = 0; i < vs.Count; i++)
            {
                CheckInput(i, d.CheckInput(vs[i]), true);
            }
        }

    }

    public void CheckInput(int index,bool result,bool mustAccept)
    {
        OnCheckInput?.Invoke(index,result, mustAccept);
    }

    public void setAlphabet()
    {
        string[] alphabet = UIManager.Instance.get_alphabet_from_field();
        Alphabet = alphabet;
    }
    public bool TryConnect(int from , string tag , int to) 
    {
        return _machine.TryConnect(from, tag, to);
    }
    public bool TryDisConnect(int from, string tag)
    {
        return _machine.TryDisConnect(from, tag);
    }

}