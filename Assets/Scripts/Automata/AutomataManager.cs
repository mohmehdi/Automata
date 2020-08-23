using System;
using UnityEngine;

    public class AutomataManager : MonoBehaviour
    {
        public static int CurrentStateId;
        private Automata _machine;


        private void Start()
        {
        }
        public void setAlphabet()
        {
            string[] alphabet = UIManager.Instance.get_alphabet_from_field() ;
            _machine = new DFA(alphabet);            
        }
    }