﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MultiDropDown : MonoBehaviour
{
    [SerializeField] GameObject togglePrefab = null;
    [SerializeField] GameObject optionsMenu = null;
    [SerializeField] Transform content = null;
    private Button Button;
    private float toggleHeight;


    private void Start()
    {
        string[] ass = { "a", "b", "c", "d" };
        Button = GetComponent<Button>();

        toggleHeight = togglePrefab.GetComponent<RectTransform>().rect.height;
        RectTransform rect = content.GetComponent<RectTransform>();
        rect.rect.Set(rect.rect.x, rect.rect.y, rect.rect.width, ass.Length * toggleHeight);
        for (int i = 0; i < ass.Length; i++)
        {
            GameObject t = Instantiate(togglePrefab);
            t.transform.SetParent(content);
            t.GetComponent<RectTransform>().localPosition = new Vector3(0, -i * toggleHeight);
            t.GetComponentInChildren<Text>().text = ass[i];

            // t.GetComponent<RectTransform>().rect.Set(0, i * toggleHeight,100,toggleHeight);
        }
    }
    public void TurnMenuOnOff()
    {
        optionsMenu.SetActive(!optionsMenu.activeSelf);
    }

}
