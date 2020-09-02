using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AlphabetInputValueControler : MonoBehaviour
{
    [SerializeField] InputField InputField;
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Tab))
    //    {
    //        if (EventSystem.current.currentSelectedGameObject == InputField.gameObject)
    //            InputField.text += "ƛ";
    //    }
    //}
    public void CheckAlpabetInput()
    {
        string input ="";
        foreach (var ch in InputField.text)
        {
            if (char.IsLetter(ch) && !input.Contains(ch.ToString()))
            {
                input += ch;
            }
        }
        InputField.text = input;
    }
    public void CheckInputSyntax()
    {
        AutomataType type = AutomataManager.automataType;

        if (type == AutomataType.dfa) //Syntax for DFA is a Character
        {

        }
        if (type == AutomataType.pda)
        {

        }
    }
}
