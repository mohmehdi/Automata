using System;
using System.Collections.Generic;
using UnityEngine;

public enum AutomataType
{
    dfa,nfa,pda
}
public class AutomataManager : MonoBehaviour
{
    //'ƛ'
    public static AutomataManager Instance; 
    public static AutomataType automataType;
    public static char[] inputAlphabet;
    public static char[] machineAlphabet;
    public static int CurrentStateId;

    private Automata _machine;
    DFA d;

    /// <summary>
    /// index of input string from two lists
    /// result of checking that string
    /// is that string from accept list of not
    /// </summary>
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
        if (Input.GetKeyDown(KeyCode.Y))
        {
            List<string> vs = UIManager.Instance.GetInputs(true);
            for (int i = 0; i < vs.Count; i++)
            {
                CheckInput(i, d.CheckInput(vs[i]), true);
            }
            vs = UIManager.Instance.GetInputs(false);
            for (int i = 0; i < vs.Count; i++)
            {
                CheckInput(i, d.CheckInput(vs[i]), false);
                Debug.Log(vs[i]);
            }
        }
    }

    public void CheckInput(int index,bool result,bool mustAccept)
    {
        OnCheckInput?.Invoke(index,result, mustAccept);
    }

    public void setAlphabet()
    {
        char[] alphabet = UIManager.Instance.GetLanguageAlphabet();
        inputAlphabet = alphabet;
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