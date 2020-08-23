using System;
using UnityEditor;
using UnityEngine;
public class ControlPoint : MonoBehaviour
{
    private Vector3 _offset;
    private void OnMouseDown()
    {
        _offset = transform.position-MousePosition.GetMousePosition();
    }

    private void OnMouseDrag()
    {
        transform.position =MousePosition.GetMousePosition()+_offset;
    }
}