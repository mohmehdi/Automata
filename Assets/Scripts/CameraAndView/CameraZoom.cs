using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float speed = 1;
    [SerializeField] private float mult = 1;


    private float scrollAmount = 0;


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

            Vector3 diffrence = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.position = Vector3.Lerp(transform.position, (transform.position - diffrence * (-Mathf.Abs(scrollAmount)* mult)), speed);
            transform.position = new Vector3(transform.position.x, transform.position.y, -10);

            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, cam.orthographicSize + Mathf.Sign(scrollAmount)*mult, speed);
            if (cam.orthographicSize <= 0)
                cam.orthographicSize = 0.2f;

        }
    }
}
