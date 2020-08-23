using System;
using UnityEngine;

    public class StateObjectID:MonoBehaviour
    {
        [HideInInspector]
        public int ID;

        private void Start()
        {
            ID = AutomataManager.CurrentStateId-1;
        }
    }