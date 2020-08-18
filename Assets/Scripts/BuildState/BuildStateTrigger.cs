using System;
using UnityEngine;

public class BuildStateTrigger : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            BuildStateEvents.Instance.CreateState();
        }
    }
}