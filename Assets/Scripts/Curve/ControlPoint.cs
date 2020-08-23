using System;
using UnityEditor;
using UnityEngine;
public class ControlPoint : MonoBehaviour
{
    [SerializeField] private LineRenderer line;
    [SerializeField] private Transform[] helpers;
    private Vector3 _offset;
    private void OnMouseDown()
    {
        _offset = transform.position-MousePosition.GetMousePosition();
    }

    private void OnMouseDrag()
    {
        transform.position =MousePosition.GetMousePosition()+_offset;
        line.SetPosition(0, helpers[0].position);
        line.SetPosition(1, helpers[1].position);
    }
}