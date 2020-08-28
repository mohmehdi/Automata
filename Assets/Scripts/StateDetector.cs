using UnityEngine;

public class StateDetector
    {

        /// <summary>
        /// cast a ray 1 unit behind mousePos to 1 unit forward
        /// that state sprite will be in between
        /// </summary>
        /// <returns></returns>
        public static GameObject DetectStateObject(int layerMask)
        {
            var mousePos = MousePosition.GetCamera().ScreenToWorldPoint(Input.mousePosition);
            mousePos = new Vector3(mousePos.x, mousePos.y, 0);
            //Debug.Log(statesLayer.value);
            RaycastHit2D hit = Physics2D.Raycast(mousePos + Vector3.back, mousePos + Vector3.zero, 1.5f,layerMask);
            if (!hit) return null;
            return hit.collider.gameObject;
        }
    }