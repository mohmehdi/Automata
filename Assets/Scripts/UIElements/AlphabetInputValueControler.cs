using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AlphabetInputValueControler : MonoBehaviour
{
    [SerializeField] InputField InputField;
    public void CheckAlpabetInput()
    {
        string input ="";
        foreach (var ch in InputField.text)
        {
            if ((char.IsDigit(ch) || char.IsLetter(ch)) && !input.Contains(ch.ToString()))
            {
                input += ch;
            }
        }
        InputField.text = input;
    }
   
}
