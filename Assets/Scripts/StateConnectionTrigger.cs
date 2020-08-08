using System;
using DefaultNamespace;
using UnityEngine;

public class StateConnectionTrigger : MonoBehaviour
{
    [SerializeField]
    private LayerMask statesLayer;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos = new Vector3(mousePos.x, mousePos.y, 1);
            Debug.DrawRay(-Vector3.forward, mousePos);
            RaycastHit2D hit = Physics2D.Raycast(-Vector3.forward, mousePos,statesLayer);
            if (!hit) return;
                StateConnectionEvents.Instance.firstStateID = hit.collider.GetComponent<StateID>();
                StateConnectionEvents.Instance.SelectFirstState();
                Debug.Log("hit");
        }
    }
}
