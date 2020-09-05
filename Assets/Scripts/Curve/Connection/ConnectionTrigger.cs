using System;
using System.Net;
using UnityEngine;

public class ConnectionTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask statesLayer=0;
    [SerializeField] private KeyCode viewKey;
    private bool _isFirstSelected=false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            SelectFirst();
        }
        if (Input.GetMouseButtonUp(1))
        {
            SelectSecond();
        }
        if (Input.GetKeyDown(viewKey))
        {
            ConnectionEvents.Instance.ActiveEditMode();
        }
    }
    private void SelectFirst()
    {
        var first = StateDetector.DetectStateObject(statesLayer.value);
        if (!first) return;
        var first_id = first.GetComponent<StateObject>();
        if (!first_id)
        {
            Debug.Log("selected state dosent have StateID component");
            return;
        }

        ConnectionEvents.Instance.firstStateID = first_id;
        ConnectionEvents.Instance.FirstStateSelected();
        _isFirstSelected = true;
        //Debug.Log("first state selected");
    }
    private void SelectSecond()
    {
        if (!_isFirstSelected) return;

        var second = StateDetector.DetectStateObject(statesLayer.value);
        if (!second)
        {
            ConnectionEvents.Instance.SecondStateSelectionCanceled();
            Debug.Log("second state selection canceled");
            _isFirstSelected = false;
            return;
        }
        //Debug.Log(second.name);
        var second_id = second.GetComponent<StateObject>();
        if (!second_id)
        {
            ConnectionEvents.Instance.SecondStateSelectionCanceled();
            Debug.Log("selected state dosent have StateID component");
            _isFirstSelected = false;
            return;
        }
        if (!AutomataManager.Instance.IsConnectionPossible(ConnectionEvents.Instance.firstStateID.ID,second_id.ID)) //TODO : this isnt working check this another way
        {
            ConnectionEvents.Instance.SecondStateSelectionCanceled();
            Debug.Log("Current Connection exists");
            _isFirstSelected = false;
            return;
        }
        ConnectionEvents.Instance.secondStateID = second_id;
        ConnectionEvents.Instance.SecondStateSelected();
        _isFirstSelected = false;
        //Debug.Log("second selected");
    }

}
