using System;
using UnityEngine;
using UnityEngine.UI;

public class StateObject : MonoBehaviour
{
    [Tooltip("Start\nNormal\nFinal\nStart&Final")]
    [SerializeField] private Sprite[] sprites = null;
    [SerializeField] private RectTransform stateName=null; 

    [HideInInspector]
    public int ID;

    private Status _status=Status.NORMAL;
    private SpriteRenderer _spriteRenderer;
    private void Start()
    {
        /// when a state created 
        /// automata class makes one state with {AutomataManager.CurrentStateId} 
        /// then in next frame Start method of this object calls 
        /// so -1 turn it back to it should be
        ID = AutomataManager.CurrentStateId - 1; 

        stateName.SetParent(UIManager.Instance.transform,false);
        stateName.position = MousePosition.GetCamera().WorldToScreenPoint(transform.position);

        _spriteRenderer = GetComponent<SpriteRenderer>();

        if (sprites.Length <4)
        {
            Debug.LogError("Set State Colors");
        }

        BuildStateEvents.Instance.OnChangeStatus += SetSprite;
        BuildStateEvents.Instance.OnDeleteState += DestroyThis;
    }
    public void SetSprite(int id, Status status)
    {
        // setting sprite to normal or final if current status is a kind of Start status (it can be start or Start&final)
        if (status == Status.START || status == Status.STARTANDFINAL)
        {
            if (_status == Status.START)
            {
                _spriteRenderer.sprite = sprites[1];
                _status = Status.NORMAL;
            }

            if (_status == Status.STARTANDFINAL)
            {
                _spriteRenderer.sprite = sprites[2];
                _status = Status.FINAL;
            }
        }

        if (id != ID) return;
        
        _status = status;

        if (status == Status.NORMAL)
        _spriteRenderer.sprite = sprites[1];

        else if (status == Status.START)
        _spriteRenderer.sprite = sprites[0];

        else if (status == Status.FINAL)
        _spriteRenderer.sprite = sprites[2];

        else if (status == Status.STARTANDFINAL)
        _spriteRenderer.sprite = sprites[3];
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
        BuildStateEvents.Instance.OnChangeStatus -= SetSprite;
        BuildStateEvents.Instance.OnDeleteState -= DestroyThis;
    }
}