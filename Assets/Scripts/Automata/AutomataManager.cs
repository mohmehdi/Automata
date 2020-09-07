using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum AutomataType
{
    dfa,DPDA,turing
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
    /// <summary>
    /// index of input string from two lists
    /// result of checking that string
    /// is that string from accept list of not
    /// </summary>
    public Action<int,bool, bool> OnInputChecked;

    private void Start()
    {
        _connections = new List<Vector2Int>();
        Instance = this;
        automataType = AutomataType.dfa;

        ConnectionEvents.Instance.OnSecondStateSelected += UpdateLocalConnections;
    }

    public void CheckStrings()
    {
        List<string> vs = UIManager.Instance.GetInputs(true);
        for (int i = 0; i < vs.Count; i++)
        {
            ApplyInputCheck(i, _machine.CheckInput(vs[i]), true);
        }
        vs = UIManager.Instance.GetInputs(false);
        for (int i = 0; i < vs.Count; i++)
        {
            ApplyInputCheck(i, _machine.CheckInput(vs[i]), false);
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
    public void ApplyInputCheck(int index,bool result,bool mustAccept)
    {
        OnInputChecked?.Invoke(index,result, mustAccept);
    }

    public void Initialize()
    {
        inputAlphabet = UIManager.Instance.GetLanguageAlphabet();
        switch (automataType)
        {
            case AutomataType.dfa:
                _machine = new DFA();
                break;
            case AutomataType.DPDA:
                _machine = new DPDA();
                machineAlphabet = UIManager.Instance.GetMachineAlphabet();
                var z = new char[inputAlphabet.Length + machineAlphabet.Length];
                inputAlphabet.CopyTo(z, 0);
                machineAlphabet.CopyTo(z, inputAlphabet.Length);
                machineAlphabet = z;
                break;
            case AutomataType.turing:
                _machine = new Turing();
                machineAlphabet = UIManager.Instance.GetMachineAlphabet();
                break;
        }

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