﻿using System;
using UnityEngine;
using UnityEngine.UI;

public class StateObject : MonoBehaviour
{
    [SerializeField] private Color[] colors = null;
    [SerializeField] private RectTransform stateName=null; 

    [HideInInspector]
    public int ID;

    private SpriteRenderer _spriteRenderer;
    private void Start()
    {

        ID = AutomataManager.CurrentStateId - 1;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (colors.Length <3)
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
        if (status ==Status.START && _spriteRenderer.color == colors[0])
            _spriteRenderer.color = colors[1];

        if (id != ID) return;

        if (status == Status.NORMAL)
        {
            _spriteRenderer.color = colors[1];
        }
        else if (status == Status.START)
        {
            _spriteRenderer.color = colors[0];
        }
        else if (status == Status.FINAL)
        {
            _spriteRenderer.color = colors[2];
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