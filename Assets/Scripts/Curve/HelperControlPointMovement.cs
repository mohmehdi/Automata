using UnityEngine;

public class HelperControlPointMovement : MonoBehaviour
{
    [SerializeField] private Transform other;

    private Camera _camera;//TODO: Remove this

    private Vector3 _offset;
    private Vector3 _center;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void OnMouseDown()
    {
        _offset = transform.position - GetMousePosition();
        _center = other.position + (transform.position - other.position) / 2;
    }

    private void OnMouseDrag()
    {
        transform.position = GetMousePosition() + _offset;
        other.position = _center + (_center - transform.position);

    }

    private Vector3 GetMousePosition()//TODO: Remove this
    {
        var mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector3(mousePos.x, mousePos.y, 0);
        return mousePos;
    }
}
