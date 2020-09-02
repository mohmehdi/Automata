﻿using System;
using UnityEditor;
using UnityEngine;
public class ControlPointMovement : MonoBehaviour
{
    [SerializeField] private GameObject controlPointsParent = null;

    public static bool viewMode = false;
    private Vector3 _offset;

    private void Start()
    {
        ConnectionEvents.Instance.OnEditMode += OnActiveEditMode;
    }
    private void OnActiveEditMode(bool flag)
    {
        controlPointsParent.SetActive(flag);
    }
    private void OnMouseDown()
    {
        _offset = transform.position-MousePosition.GetMousePosition();
    }

    private void OnMouseDrag()
    {
        transform.position =MousePosition.GetMousePosition()+_offset;
    }
    private void OnDestroy()
    {
        ConnectionEvents.Instance.OnEditMode -= OnActiveEditMode;
    }
}