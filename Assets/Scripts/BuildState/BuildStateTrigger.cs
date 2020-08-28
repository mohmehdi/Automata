using System;
using UnityEngine;

public class BuildStateTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask stateLayer = 0;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            BuildStateEvents.Instance.CreateState();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            var detected = StateDetector.DetectStateObject(stateLayer);
            if (!detected) return;

            var id = detected.GetComponent<StateObject>().ID;
            BuildStateEvents.Instance.DeleteState(id);
        }
    }
}