using System;
using UnityEngine;
using UnityEngine.UI;

public class StateObject : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites = null;
    [SerializeField] private RectTransform stateName=null; 

    [HideInInspector]
    public int ID;

    private Status _status=Status.NORMAL;
    private SpriteRenderer _spriteRenderer;
    private void Start()
    {

        ID = AutomataManager.CurrentStateId - 1;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (sprites.Length <4)
        {
            Debug.LogError("Set State Colors");
        }

        stateName.SetParent(UIManager.Instance.transform);
        stateName.position = MousePosition.GetCamera().WorldToScreenPoint(transform.position);
        BuildStateEvents.Instance.OnChangeStatus += SetColor;
        BuildStateEvents.Instance.OnDeleteState += DestroyThis;
    }
    public void SetColor(int id, Status status)
    {
        if ((status ==Status.START|| status == Status.STARTANDFINAL) && _status == Status.START )
            _spriteRenderer.sprite = sprites[1];

        if ((status == Status.START || status == Status.STARTANDFINAL) && _status == Status.STARTANDFINAL)
            _spriteRenderer.sprite = sprites[2];

        if (id != ID) return;

        if (status == Status.NORMAL)
        {
            _spriteRenderer.sprite = sprites[1];
        }
        else if (status == Status.START)
        {
            _spriteRenderer.sprite = sprites[0];
        }
        else if (status == Status.FINAL)
        {
            _spriteRenderer.sprite = sprites[2];
        }
        else if (status == Status.STARTANDFINAL)
        {
            _spriteRenderer.sprite = sprites[3];
        }
    }
    private void DestroyThis(int id)
    {
        if (id == ID)
        {
            Destroy(stateName.gameObject);
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        BuildStateEvents.Instance.OnChangeStatus -= SetColor;
        BuildStateEvents.Instance.OnDeleteState -= DestroyThis;
    }
}