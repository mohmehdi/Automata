using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class MyDropDown : MonoBehaviour
{
    [SerializeField] GameObject togglePrefab = null;
    [SerializeField] GameObject optionsMenu = null;
    [SerializeField] Transform content = null;
    public int _from, _to;
    private float toggleHeight;
    private void Start()
    {
        SetOptions();
        gameObject.SetActive(false);

        ConnectionEvents.Instance.OnEditMode += OnActiveEditMode;
        BuildStateEvents.Instance.OnDeleteState += DestroyThisWhenStateDeleted;

    }
    private void OnActiveEditMode(bool flag,bool view)
    {
        if (!view)
        {
            Color c = optionsMenu.GetComponent<Image>().color;
            c = new Color(c.r, c.g, c.b, 0);
            optionsMenu.GetComponent<Image>().color = c;
        }
        else
        {
            Color c = optionsMenu.GetComponent<Image>().color;
            c = new Color(c.r, c.g, c.b, 1);
            optionsMenu.GetComponent<Image>().color = c;
        }
        if(view)
        gameObject.SetActive(!flag);
    }
    private void OnDestroy()
    {
        ConnectionEvents.Instance.OnEditMode -= OnActiveEditMode;
        BuildStateEvents.Instance.OnDeleteState -= DestroyThisWhenStateDeleted;
    }
    private void SetOptions()//can take a int to set diffrent inputs from automatas
    {
        var alphabet = AutomataManager.inputAlphabet;

        toggleHeight = togglePrefab.GetComponent<RectTransform>().rect.height;
        content.GetComponent<RectTransform>().sizeDelta = new Vector2(0,toggleHeight * alphabet.Length);

       // rect.rect.Set(rect.rect.x, rect.rect.y, rect.rect.width, alphabet.Length * toggleHeight);

        for (int i = 0; i < alphabet.Length; i++)
        {
            GameObject toggleObj = Instantiate(togglePrefab,content);
            toggleObj.GetComponent<RectTransform>().localPosition = new Vector3(3, -(i * toggleHeight + 4));
            toggleObj.GetComponent<MyToggle>().Set(this, alphabet[i].ToString());
        }
    }
    public void TurnMenuOnOff()
    {
        optionsMenu.SetActive(!optionsMenu.activeSelf);
    }
    private void DestroyThisWhenStateDeleted(int id)
    {
        if (_from == id || _to == id)
        {
            Destroy(gameObject);
        }
    }
}
