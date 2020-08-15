using System;
using UnityEngine;

public class StateCreator : MonoBehaviour
{
    [SerializeField] private GameObject statePrefab;
    private void Start()
    {
        StateCreationEvents.Instance.OnCreateState += OnCreateStateObject;
    }

    private void OnCreateStateObject()
    {
        Vector2 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        GameObject state = Instantiate(statePrefab, mousePos, Quaternion.identity);
    }
}