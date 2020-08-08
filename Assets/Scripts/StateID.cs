using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class StateID:MonoBehaviour
    {
        [HideInInspector]
        public int stateID;

        private void Start()
        {
            stateID = AutomataManager.CurrentStateId-1;
            Debug.Log("obj id"+(stateID));
        }
    }
}