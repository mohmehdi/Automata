﻿using System;
using UnityEngine;
using UnityEngine.UI;

class MyToggle : MonoBehaviour
{
    private MyDropDown _myDrop;
    private string _tag;
    private Text _Text;
    private Toggle _Toggle;
    private void Start()
    {
        _Text = GetComponentInChildren<Text>();
        _Toggle = GetComponent<Toggle>();
    }
    public void Set(MyDropDown drop, string tag)
    {
        _myDrop = drop;
        _tag = tag;
        GetComponentInChildren<Text>().text = _tag ;

    }
    public void CheckConnect(bool flag)
    {
        Debug.Log(flag + _myDrop._from.ToString() + _myDrop._to.ToString());
        bool res= false;
        if (flag)
        {
             res = AutomataManager.Instance.TryConnect(_myDrop._from, _tag, _myDrop._to);
        }
        else
        {
            res = AutomataManager.Instance.TryDisConnect(_myDrop._from, _tag, _myDrop._to);
        }
        ChangeSkin(res);
    }

    private void ChangeSkin(bool result)
    {
        ColorBlock c = _Toggle.colors;
        if (result)
        {

            c.normalColor = Color.green;
            c.selectedColor = Color.green;
            _Text.color = Color.green;
        }
        else
        {
            c.normalColor = Color.red;
            c.selectedColor = Color.red;
            _Text.color = Color.red;
        }
        _Toggle.colors = c;
    }
}
