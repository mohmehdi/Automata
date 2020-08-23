using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ConnectionEvents : MonoBehaviour
{
    public static ConnectionEvents Instance { get; private set; }
    public StateObjectID firstStateID;
    public StateObjectID secondStateID;

    public event Action OnFirstStateSelected;
    public event Action OnSecondStateSelected;
    public event Action OnSecondStateSelectionCanceled;
    public event Action<State,ConnectionData> OnDeleteLastConnection;

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
    public void DeleteLastConnection(State from,ConnectionData label)
    {
        OnDeleteLastConnection?.Invoke(from,label);
    }
}