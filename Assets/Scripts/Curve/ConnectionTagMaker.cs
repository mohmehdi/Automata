using System;
using UnityEngine;

class ConnectionTagMaker : MonoBehaviour
{
    private RectTransform _dropDownTransform;
    public void Initialize(GameObject dropDown , Transform canvas)
    {
        CreateDropDowns(dropDown,canvas);
    }

    private void CreateDropDowns(GameObject dropDown ,Transform canvas)
    {
        if (AutomataManager.automataType == AutomataType.dfa)
        {
            GameObject drop = Instantiate(dropDown);
            drop.transform.SetParent(canvas);
            _dropDownTransform = drop.GetComponent<RectTransform>();
        }
    }
    public void SetPosition(Vector2 position)
    {
        _dropDownTransform.transform.position = position;
    }
}
