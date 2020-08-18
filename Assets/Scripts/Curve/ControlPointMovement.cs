using System;
using UnityEngine;
public class ControlPointMovement : MonoBehaviour
{
    private Camera _camera; //TODO: Remove this

    private Vector3 _offset;
    private void Start()
    {
        _camera = Camera.main;
    }

    private void OnMouseDown()
    {
        _offset = transform.position-GetMousePosition();
    }

    private void OnMouseDrag()
    {
        transform.position = GetMousePosition()+_offset;
    }

    private Vector3 GetMousePosition()//TODO: Remove this
    {
         var mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
         mousePos = new Vector3(mousePos.x,mousePos.y,0);
         return mousePos;
    }
}