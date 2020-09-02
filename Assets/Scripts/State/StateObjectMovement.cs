using System;
using System.Collections;
using UnityEngine;

public class StateObjectMovement : MonoBehaviour
{
    [SerializeField]private RectTransform stateName = null;
    private Vector3 _offset; //offset from where we click to objects center

    private void OnMouseDown()
    {
        stateName.position = MousePosition.GetCamera().WorldToScreenPoint(transform.position);
        _offset = transform.position- MousePosition.GetMousePosition();
    }

    private void OnMouseDrag()
    {
        transform.position = MousePosition.GetMousePosition()+_offset;
    }
}