using System.Collections.Generic;
using UnityEngine;

class MyDropDown : MonoBehaviour
{
    [SerializeField] GameObject togglePrefab;
    [SerializeField] GameObject optionsMenu;
    [SerializeField] Transform content;
    public int _from, _to;
    private float toggleHeight;
    private void Start()
    {
        SetOptions();
        ConnectionEvents.Instance.OnEditMode += OnActiveEditMode;
        gameObject.SetActive(false);
    }
    private void OnActiveEditMode(bool flag)
    {
        gameObject.SetActive(!flag);
    }
    private void OnDestroy()
    {
        ConnectionEvents.Instance.OnEditMode -= OnActiveEditMode;
    }
    private void SetOptions()//can take a int to set diffrent inputs from automatas
    {
        var alphabet = AutomataManager.Alphabet;

        toggleHeight = togglePrefab.GetComponent<RectTransform>().rect.height;
        RectTransform rect = content.GetComponent<RectTransform>();
        rect.rect.Set(rect.rect.x, rect.rect.y, rect.rect.width, alphabet.Length * toggleHeight);
        for (int i = 0; i < alphabet.Length; i++)
        {
            GameObject toggleObj = Instantiate(togglePrefab);
            toggleObj.transform.SetParent(content);
            toggleObj.GetComponent<RectTransform>().localPosition = new Vector3(0, -i * toggleHeight);
            toggleObj.GetComponent<MyToggle>().Set(this, alphabet[i]) ;
        }
    }
    public void TurnMenuOnOff()
    {
        optionsMenu.SetActive(!optionsMenu.activeSelf);
    }

}
