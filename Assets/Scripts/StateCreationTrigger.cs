using System;
using UnityEngine;

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