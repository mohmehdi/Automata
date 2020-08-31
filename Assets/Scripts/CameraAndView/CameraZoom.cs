using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private Camera cam;
    private float scrollAmount = 0;

    private void Update()
    {

        scrollAmount = Input.mouseScrollDelta.y;
        cam.orthographicSize += scrollAmount;
        if (cam.orthographicSize <= 0)
            cam.orthographicSize = 0.1f ;

    }
}
