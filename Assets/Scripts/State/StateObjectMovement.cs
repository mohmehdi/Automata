using System;
using System.Collections;
using UnityEngine;

public class StateObjectMovement : MonoBehaviour
{
    [SerializeField]private RectTransform stateName = null;
    private Vector3 _offset; //offset from where we click to objects center

    private void OnMouseDown()
    {
        _offset = transform.position- MousePosition.GetMousePosition();
    }

    private void OnMouseDrag()
    {
        transform.position = MousePosition.GetMousePosition()+_offset;
        stateName.position = MousePosition.GetCamera().WorldToScreenPoint(transform.position);
    }
}