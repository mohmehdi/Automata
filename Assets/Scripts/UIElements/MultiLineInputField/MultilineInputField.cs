using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MultilineInputField : MonoBehaviour
{
    private const int ITEM_HEIGHT = 35;

    [SerializeField] GameObject inputFieldPrefab;
    [SerializeField] RectTransform addButtonRect;
    private ScrollRect _scrollView;
    private List<RectTransform> _itemsTransform;
    public List<string> _inputs { get; private set; }

    public Action<int> OnDeleteItem;

    private void Start()
    {
        _scrollView = GetComponentInChildren<ScrollRect>();
        _itemsTransform = new List<RectTransform>();
        _inputs = new List<string>();
    }

    public void AddItem()
    {
        var ItemRect = Instantiate(inputFieldPrefab,_scrollView.content).GetComponent<RectTransform>();
        ItemRect.GetComponent<InputFieldIndex>().index = _itemsTransform.Count;


        //seting new item position
        ItemRect.localPosition += Vector3.down * ITEM_HEIGHT * _itemsTransform.Count ;
        _itemsTransform.Add(ItemRect);
        string s = "";
        _inputs.Add(s);

        //set addbutton transform
        addButtonRect.localPosition += Vector3.down * ITEM_HEIGHT;

        //set Content size
        _scrollView.content.sizeDelta = new Vector2(0,(_itemsTransform.Count + 1) * ITEM_HEIGHT);
    }


    public void DeleteItem(int index)
    {
        Debug.Log($"Item  {index} Deleted ");
        Destroy(_itemsTransform[index].gameObject);
        int i;
        for (i = index; i < _itemsTransform.Count - 1; i++)
        {
            _inputs[i] = _inputs[i + 1];
            _itemsTransform[i] = _itemsTransform[i + 1];
            _itemsTransform[i].localPosition += Vector3.up * ITEM_HEIGHT ;
        }
        _itemsTransform.RemoveAt(i);
        _inputs.RemoveAt(i);

        addButtonRect.localPosition += Vector3.up * ITEM_HEIGHT;
        OnDeleteItem?.Invoke(index);
    }

    public void ChangeInput(InputField input,int index)
    {
        _inputs[index] = input.text;
    }
}
