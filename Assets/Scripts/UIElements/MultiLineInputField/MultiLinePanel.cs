using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MultiLinePanel : MonoBehaviour
{
    private const int ITEM_HEIGHT = 35; //maybe not best way but its effisient and clean a little and 35 is ok 

    [SerializeField] bool IsAcceptList = true;
    [SerializeField] GameObject itemPrefab=null;
    [SerializeField] RectTransform addButtonRect=null;

    private ScrollRect _scrollView;
    private List<RectTransform> _itemsTransform;
    public List<string> inputs { get; private set; }

    public Action<int> OnDeleteItem;

    private void Start()
    {
        _itemsTransform = new List<RectTransform>();
        inputs = new List<string>();
        _scrollView = GetComponentInChildren<ScrollRect>();
    }

    public void AddItem()
    {
        var ItemRect = Instantiate(itemPrefab,_scrollView.content).GetComponent<RectTransform>();
        ItemRect.GetComponent<InputFieldItem>().Initialize(_itemsTransform.Count,IsAcceptList); // here i set index used in item class
        
        //seting new item position
        ItemRect.localPosition += Vector3.down * ITEM_HEIGHT * _itemsTransform.Count ;
        _itemsTransform.Add(ItemRect);
        string s = "";
        inputs.Add(s);

        //set addbutton transform
        addButtonRect.localPosition += Vector3.down * ITEM_HEIGHT;

        //set Content size
        _scrollView.content.sizeDelta = new Vector2(0,(_itemsTransform.Count + 1) * ITEM_HEIGHT);
    }

    public void DeleteItem(int index)
    {
        //Debug.Log($"Item  {index} Deleted ");
        Destroy(_itemsTransform[index].gameObject);
        ShiftUpItems(index);
        OnDeleteItem?.Invoke(index);
    }

    /// <summary>
    ///shift every thin up from index to end of list and also addButton
    /// </summary>
    private void ShiftUpItems(int index)
    {
        int i;
        for (i = index; i < _itemsTransform.Count - 1; i++) 
        {
            inputs[i] = inputs[i + 1];
            _itemsTransform[i] = _itemsTransform[i + 1];
            _itemsTransform[i].localPosition += Vector3.up * ITEM_HEIGHT;
        }
        _itemsTransform.RemoveAt(i);
        inputs.RemoveAt(i);

        addButtonRect.localPosition += Vector3.up * ITEM_HEIGHT;
    }

    public void ChangeInput(InputField input,int index)
    {
        inputs[index] = input.text;
    }
}
