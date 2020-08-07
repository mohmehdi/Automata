using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class StateMovement : MonoBehaviour
    {
        private void OnMouseDrag()
        {
            Vector3 mousePos = new Vector3(Input.mousePosition.x,Input.mousePosition.y,1);
            transform.position = Camera.main.ScreenToWorldPoint(mousePos);
        }
    }
}