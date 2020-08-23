using UnityEngine;

public class HelperControlPointMovement : MonoBehaviour
{
    [SerializeField] private Transform other;

    private Vector3 _offset;
    private Vector3 _center;

    private void OnMouseDown()
    {
        _offset = transform.position - MousePosition.GetMousePosition();
        _center = other.position + (transform.position - other.position) / 2;
    }

    private void OnMouseDrag()
    {
        transform.position = MousePosition.GetMousePosition() + _offset;
        other.position = _center + (_center - transform.position);

    }
}
