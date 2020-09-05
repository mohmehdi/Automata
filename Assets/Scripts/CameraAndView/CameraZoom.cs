using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    [SerializeField] private float multiplier = 1;

    private Camera _camera;
    private float scrollAmount = 0;

    private void Start()
    {
        _camera = GetComponent<Camera>();
    }
    private void Update()
    {
        if (Mathf.Abs(Input.mouseScrollDelta.y)>0)
        {
            scrollAmount = -Input.mouseScrollDelta.y;
        }
        scrollAmount *= 0.95f;

        if (Mathf.Abs(scrollAmount)<0.05f)
        {
            scrollAmount = 0;
        }
        if (Mathf.Abs(scrollAmount) > 0)
        {
            Vector3 diffrence = _camera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.position = Vector3.Lerp(transform.position, transform.position - diffrence * (-Mathf.Abs(scrollAmount)* multiplier), speed);
            transform.position = new Vector3(transform.position.x, transform.position.y, -10);

            _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, _camera.orthographicSize + Mathf.Sign(scrollAmount)*multiplier, speed);
            if (_camera.orthographicSize <= 0)
                _camera.orthographicSize = 0.2f;
        }
    }
}
