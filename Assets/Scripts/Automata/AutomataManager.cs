using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum AutomataType
{
    dfa,DPDA,Turing
}
public class AutomataManager : MonoBehaviour
{
    //'ƛ'
    public static AutomataManager Instance; 
    public static AutomataType automataType;
    public static char[] inputAlphabet;
    public static char[] machineAlphabet;
    public static int CurrentStateId;

    private InputStringCheck _inputChecker;
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
        Instance = this;
        _connections = new List<Vector2Int>();
        automataType = AutomataType.dfa;

        _inputChecker = GetComponent<InputStringCheck>();

        ConnectionEvents.Instance.OnSecondStateSelected += UpdateLocalConnections;
    }

    public void CheckStrings()
    {
        var must = UIManager.Instance.GetInputs(true);
        var mustNot = UIManager.Instance.GetInputs(false);
        _inputChecker.StartCheck(must, mustNot, _machine);
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
        string inp="";
        switch (automataType)
        {

            case AutomataType.dfa:
                _machine = new DFA();
                inputAlphabet = UIManager.Instance.GetLanguageAlphabet().ToCharArray();
                break;
        
            case AutomataType.DPDA:
                _machine = new DPDA();
                inp = UIManager.Instance.GetLanguageAlphabet() + "λ";
                inputAlphabet = (inp + "$").ToCharArray();
                machineAlphabet = (UIManager.Instance.GetMachineAlphabet()+ inp + "zZ").ToCharArray();
                break;

            case AutomataType.Turing:
                gameObject.AddComponent<TuringHelper>();
                _machine = new Turing();
                inp = UIManager.Instance.GetLanguageAlphabet()+ UIManager.Instance.GetMachineAlphabet()+"□";
                inputAlphabet = inp.ToCharArray();
                machineAlphabet = inputAlphabet;
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
