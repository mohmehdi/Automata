using System;
using UnityEngine;

public enum AutomataType
{
    dfa,nfa,pda
}
public class AutomataManager : MonoBehaviour
{
    public static AutomataType automataType;
    public static string[] Alphabet;
    public static int CurrentStateId;
    private Automata _machine;


    private void Start()
    {
        automataType = AutomataType.dfa;
    }
    public void setAlphabet()
    {
        string[] alphabet = UIManager.Instance.get_alphabet_from_field();
        _machine = new DFA(alphabet);
        Alphabet = alphabet;
    }
    public static void ChangeTag(int index)
    {
        ConnectionEvents.Instance.curveTag = Alphabet[index];
    }
}