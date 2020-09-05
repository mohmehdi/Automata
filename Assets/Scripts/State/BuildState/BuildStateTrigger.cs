using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildStateTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask stateLayer = 0;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject go = StateDetector.DetectStateObject(stateLayer);
            bool flag = EventSystem.current.IsPointerOverGameObject();
            if(!flag && !go)
            BuildStateEvents.Instance.CreateState();
        }
        if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Delete))
        {
            var detected = StateDetector.DetectStateObject(stateLayer);
            if (!detected) return;

            var id = detected.GetComponent<StateObject>().ID;
            BuildStateEvents.Instance.DeleteState(id);
        }
    }
}