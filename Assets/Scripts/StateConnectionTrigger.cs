using System;
using DefaultNamespace;
using UnityEngine;

public class StateConnectionTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask statesLayer;

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
    }
    private void SelectFirst()
    {
        var first = Detect();
        if (!first) return;
        var first_id = first.GetComponent<StateID>();
        if (!first_id)
        {
            Debug.Log("selected state dosent have StateID component");
            return;
        }

        StateConnectionEvents.Instance.firstStateID = first_id;
        StateConnectionEvents.Instance.FirstStateSelected();
        _isFirstSelected = true;
        Debug.Log("first state selected");
    }
    private void SelectSecond()
    {
        if (!_isFirstSelected) return;

        var second = Detect();
        if (!second)
        {
            StateConnectionEvents.Instance.SecondStateSelectionCanceled();
            Debug.Log("second state selection canceled");
            _isFirstSelected = false;
            return;
        }
        var second_id = second.GetComponent<StateID>();
        if (!second_id)
        {
            Debug.Log("selected state dosent have StateID component");
            _isFirstSelected = false;
            return;
        }
        StateConnectionEvents.Instance.secondStateID = second_id;
        StateConnectionEvents.Instance.SecondStateSelected();
        _isFirstSelected = false;
        Debug.Log("second selected");
    }
    private GameObject Detect()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector3(mousePos.x, mousePos.y, 1);
        Debug.DrawRay(-Vector3.forward, mousePos);
        RaycastHit2D hit = Physics2D.Raycast(-Vector3.forward, mousePos, statesLayer);
        if (!hit) return null;
        return hit.collider.gameObject;
    }
}
