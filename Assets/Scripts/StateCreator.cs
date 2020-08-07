using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class StateCreator : MonoBehaviour
    {
        [SerializeField] private GameObject statePrefab;
        private void Start()
        {
            StateCreationEvents.Instance.OnStateCreated += OnCreateStateObject;
        }

        private void OnCreateStateObject()
        {
            Vector2 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            Instantiate(statePrefab, mousePos, Quaternion.identity);
        }
    }
}