using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class StateCreationTrigger : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StateCreationEvents.Instance.CreateState();
            }
        }
    }
}