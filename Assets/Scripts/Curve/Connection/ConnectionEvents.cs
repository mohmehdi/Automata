﻿using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ConnectionEvents : MonoBehaviour
{
    public static ConnectionEvents Instance { get; private set; }
    public StateObject firstStateID;
    public StateObject secondStateID;

    private bool _isEditMode = false;

    public event Action<bool> OnEditMode;
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
    public void ActiveEditMode()
    {
        OnEditMode?.Invoke(_isEditMode);
        _isEditMode = !_isEditMode;
    }
}