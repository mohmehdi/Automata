using System;
using System.Runtime.CompilerServices;
using DefaultNamespace;
using UnityEngine;

public class StateConnectionEvents : MonoBehaviour
{
    public static StateConnectionEvents Instance { get; private set; }
    public StateID firstStateID;

    public event Action OnFirstStateSelected;
    private void Awake()
    {
        Instance = this;
    }
    public void SelectFirstState()
    {
        OnFirstStateSelected?.Invoke();
    }
}