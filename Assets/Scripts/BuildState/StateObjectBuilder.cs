using System;
using UnityEngine;

public class StateObjectBuilder : MonoBehaviour
{
    [SerializeField] private GameObject statePrefab =null;
    private void Start()
    {
        BuildStateEvents.Instance.OnCreateState += OnCreateStateObject;
    }

    private void OnCreateStateObject()
    {
        Vector2 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Instantiate(statePrefab, mousePos, Quaternion.identity);
    }
}