using System;
using System.Runtime.CompilerServices;
using DefaultNamespace;
using UnityEngine;

public class ConnectionEvents : MonoBehaviour
{
    public static ConnectionEvents Instance { get; private set; }
    public StateID firstStateID;
    public StateID secondStateID;

    public event Action OnFirstStateSelected;
    public event Action OnSecondStateSelected;
    public event Action OnSecondStateSelectionCanceled;

    private void Awake()
    {
        Instance = this;
    }
    public void FirstStateSelected()
    {
        OnFirstStateSelected?.Invoke();
    }
    public void SecondStateSelected()
    {
        OnSecondStateSelected?.Invoke();
    }
    public void SecondStateSelectionCanceled()
    {
        OnSecondStateSelectionCanceled?.Invoke();
    }
}