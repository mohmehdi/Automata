using System;
using System.Runtime.CompilerServices;
using DefaultNamespace;
using UnityEngine;

public class StateCreationEvents : MonoBehaviour
{
    public static StateCreationEvents Instance { get; private set; }

    public event Action OnCreateState;
    private void Awake()
    {
        Instance = this;
    }
    public void CreateState()
    {
        OnCreateState?.Invoke();
        AutomataManager.CurrentStateId++;
        Debug.Log("manager id"+AutomataManager.CurrentStateId);
    }
}