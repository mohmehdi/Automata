using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ConnectionEvents : MonoBehaviour
{
    public static ConnectionEvents Instance { get; private set; }
    public StateObject firstStateID;
    public StateObject secondStateID;
    private bool isEditMode = false;

    public event Action<bool,bool> OnEditMode;
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
    public void ActiveEditMode(bool view)
    {
        OnEditMode?.Invoke(isEditMode,view);
        if(view)
            isEditMode = !isEditMode;
    }
}