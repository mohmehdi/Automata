using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ConnectionEvents : MonoBehaviour
{
    public static ConnectionEvents Instance { get; private set; }
    public StateObjectID firstStateID;
    public StateObjectID secondStateID;
    public string curveTag;

    public event Action OnFirstStateSelected;
    public event Action OnSecondStateSelected;
    public event Action OnSecondStateSelectionCanceled;
    public event Action<DState,ConnectionData> OnDeleteLastConnection;

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
    public void DeleteLastConnection(DState from,ConnectionData label)
    {
        OnDeleteLastConnection?.Invoke(from,label);
    }
}