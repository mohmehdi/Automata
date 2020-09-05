using UnityEngine;
using UnityEngine.EventSystems;

public class SetStateStatus : MonoBehaviour
{
    [SerializeField] private LayerMask stateLayer=0;
    private void Update()
    {
        var detectedObject = StateDetector.DetectStateObject(stateLayer);
        if (!detectedObject) return;

        if (EventSystem.current.currentSelectedGameObject)return ;

        int id=-1;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            id = detectedObject.GetComponent<StateObject>().ID;
            BuildStateEvents.Instance.ChangeStatus(id, Status.START);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            id = detectedObject.GetComponent<StateObject>().ID;
            BuildStateEvents.Instance.ChangeStatus(id, Status.NORMAL);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            id = detectedObject.GetComponent<StateObject>().ID;
            BuildStateEvents.Instance.ChangeStatus(id, Status.FINAL);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            id = detectedObject.GetComponent<StateObject>().ID;
            BuildStateEvents.Instance.ChangeStatus(id, Status.STARTANDFINAL);
        }
    }
}