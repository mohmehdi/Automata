using System;
using UnityEngine;
    class MousePosition
    {
        private static Camera camera
        {
            get 
            {
                if (camera == null)
                {
                    camera = Camera.main;
                }
                return camera;
            }
            set 
            {
                camera = value;   
            }
        }
        public static Vector3 GetMousePosition()
        {
            var mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
            mousePos = new Vector3(mousePos.x, mousePos.y, 0);
            return mousePos;
        }
    }
