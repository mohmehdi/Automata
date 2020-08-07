using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class AutomataManager : MonoBehaviour
    {
        private Automata _machine;

        private void Start()
        {
            char[] alphabet = new[] {'a', 'b'};
            _machine= new DFA(alphabet);
        }
    }
}