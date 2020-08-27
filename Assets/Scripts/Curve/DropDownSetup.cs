using System;
using UnityEngine;
using UnityEngine.UI;


class DropDownSetup : MonoBehaviour
{
    private MyDropDown _dropDown;
    private RectTransform _dropDownTransform;
    public void Initialize(GameObject dropDown , Transform canvas)
    {
        CreateDropDowns(dropDown,canvas);
    }

    private void CreateDropDowns(GameObject dropDown ,Transform canvas)
    {
        GameObject drop = null;
        if (AutomataManager.automataType == AutomataType.dfa)
        {
            drop = Instantiate(dropDown);
            drop.transform.SetParent(canvas);
            _dropDownTransform = drop.GetComponent<RectTransform>();
        }
        _dropDown = drop.GetComponent<MyDropDown>();
        if (_dropDown==null)
        {
            Debug.Log("Why ?");
        }
    }
    public void SetDropDownFromAndTo(int from,int to)
    {
        _dropDown._from = from;
        _dropDown._to = to;
    }
    public void SetPosition(Vector2 position)
    {
        _dropDownTransform.transform.position = position;
    }

}
