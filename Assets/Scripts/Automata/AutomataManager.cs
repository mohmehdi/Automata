using System;
using System.Collections;
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

    private List<Vector2Int> _connections;
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
        _connections = new List<Vector2Int>();
        Instance = this;
        automataType = AutomataType.dfa;
        _machine = new DFA();
         d = (DFA)_machine;

        ConnectionEvents.Instance.OnSecondStateSelected += UpdateLocalConnections;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
            d.DeterministicCheck();
    }

    public void CheckStrings()
    {
        d.DeterministicCheck();

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
    private void UpdateLocalConnections()
    {
        _connections.Add(new Vector2Int( ConnectionEvents.Instance.firstStateID.ID, ConnectionEvents.Instance.secondStateID.ID));
    }
    public bool IsConnectionPossible(int from,int to) //this if just for Deterministics
    {
        foreach (var vec in _connections)
        {
            if (vec.x ==from && vec.y == to)
            {
                return false;
            }
        }
        return true;
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
    public void RemoveConnections(int from,int to)
    {
         _machine.RemoveConnections(from,to);
    }

}