using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class StateCreationEvents : MonoBehaviour
{
    public static StateCreationEvents Instance;

    public event Action OnStateCreated;
    private void Awake()
    {
        Instance = this;
    }
    public void CreateState()
    {
        if (OnStateCreated!=null)
        {
            OnStateCreated();
        }
    }
}