using System;
using UnityEngine;

public class ConnectionTrigger : MonoBehaviour
{
    [SerializeField] private LayerMask statesLayer;//TODO: this shit dosent working 

    private Camera _camera;
    private bool _isFirstSelected=false;

    private void Start()
    {
        _camera = Camera.main;
        if (!_camera)
        {
            Debug.Log("There is no active Camera with Main Tag on it u dumb idiot");
        }
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            SelectFirst();
        }
        if (Input.GetMouseButtonUp(1))
        {
            SelectSecond();
        }
    }
    private void SelectFirst()
    {
        var first = DetectStateObject();
        if (!first) return;
        var first_id = first.GetComponent<StateObjectID>();
        if (!first_id)
        {
            Debug.Log("selected state dosent have StateID component");
            return;
        }

        ConnectionEvents.Instance.firstStateID = first_id;
        ConnectionEvents.Instance.FirstStateSelected();
        _isFirstSelected = true;
        Debug.Log("first state selected");
    }
    private void SelectSecond()
    {
        if (!_isFirstSelected) return;

        var second = DetectStateObject();
        if (!second)
        {
            ConnectionEvents.Instance.SecondStateSelectionCanceled();
            Debug.Log("second state selection canceled");
            _isFirstSelected = false;
            return;
        }
        Debug.Log(second.name);
        var second_id = second.GetComponent<StateObjectID>();
        if (!second_id)
        {
            ConnectionEvents.Instance.SecondStateSelectionCanceled();
            Debug.Log("selected state dosent have StateID component");
            _isFirstSelected = false;
            return;
        }
        ConnectionEvents.Instance.secondStateID = second_id;
        ConnectionEvents.Instance.SecondStateSelected();
        _isFirstSelected = false;
        Debug.Log("second selected");
    }

    /// <summary>
    /// cast a ray 1 unit behind mousePos to 1 unit forward
    /// that state sprite will be in between
    /// </summary>
    /// <returns></returns>
    private GameObject DetectStateObject()
    {
        var mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        mousePos = new Vector3(mousePos.x, mousePos.y, 0);
        Debug.Log(statesLayer.value);
        RaycastHit2D hit = Physics2D.Raycast(mousePos + Vector3.back, mousePos + Vector3.zero,1.5f,statesLayer.value);
        if (!hit) return null;
        return hit.collider.gameObject;
    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    mousePos = new Vector3(mousePos.x, mousePos.y, 0);
    //    Gizmos.DrawWireSphere(mousePos, 1f);
    //    Gizmos.DrawLine(mousePos + Vector3.back, mousePos + Vector3.forward) ;
    //}
}
