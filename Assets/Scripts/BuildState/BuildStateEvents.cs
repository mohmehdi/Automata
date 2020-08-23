using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BuildStateEvents : MonoBehaviour
{
    public static BuildStateEvents Instance { get; private set; }

    public event Action OnCreateState;
    private void Awake()
    {
        Instance = this;
    }
    public void CreateState()
    {
        OnCreateState?.Invoke();
        AutomataManager.CurrentStateId++;
       // Debug.Log("manager id"+AutomataManager.CurrentStateId);
    }
}