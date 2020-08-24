using System;
using UnityEngine;
class MousePosition
{
    public MousePosition()
    {
        camera = Camera.main;
    }


    private static Camera camera;
    public static Vector3 GetMousePosition()
    {
        var mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector3(mousePos.x, mousePos.y, 0);
        return mousePos;
    }
    public static Camera GetCamera()
    {
        return camera;
    }
}
