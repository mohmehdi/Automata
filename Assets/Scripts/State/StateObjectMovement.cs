using System;
using UnityEngine;

public class StateObjectMovement : MonoBehaviour
{
    private Vector3 _offset; //offset from where we click to objects center

    private void OnMouseDown()
    {
        _offset = transform.position- MousePosition.GetMousePosition();
    }

    private void OnMouseDrag()
    {
        transform.position = MousePosition.GetMousePosition()+_offset;
    }
}