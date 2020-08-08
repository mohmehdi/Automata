using System;
using System.Runtime.CompilerServices;
using DefaultNamespace;
using UnityEngine;

public class StateCreationEvents : MonoBehaviour
{
    public static StateCreationEvents Instance { get; private set; }

    public event Action OnStateCreated;
    private void Awake()
    {
        Instance = this;
    }
    public void CreateState()
    {
        OnStateCreated?.Invoke();
        AutomataManager.CurrentStateId++;
        Debug.Log("manager id"+AutomataManager.CurrentStateId);
    }
}