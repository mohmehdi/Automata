using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldItem : MonoBehaviour
{
    [SerializeField] InputField inputField=null;
    [SerializeField] Button removeButton=null;
    [SerializeField] Color isOk=Color.white;
    [SerializeField] Color notOk=Color.white;

    private int _index=-1;
    private bool _isAcceptInput = true;
    private MultiLinePanel _parent;

    private void Start()
    {
        _parent = GetComponentInParent<MultiLinePanel>();

        inputField.onEndEdit.AddListener(delegate { _parent.ChangeInput(inputField,_index); });
        removeButton.onClick.AddListener(delegate { _parent.DeleteItem(_index); });

        _parent.OnDeleteItem += ReduceIndex;

        AutomataManager.Instance.OnInputChecked += Check;
    }
    public void Initialize(int i , bool isAccept)
    {
        _index = i;
        _isAcceptInput = isAccept;
    }

    private void Check(int i,bool result,bool mustAccept)
    {
        if (i != _index || mustAccept != _isAcceptInput) return;

        inputField.image.color = mustAccept ? (result ? isOk : notOk) : (result ? notOk : isOk);
    }
    public void ReduceIndex(int i)
    {
        if (i<_index)
        {
            _index--;
        }
    }
    private void OnDestroy()
    {
        AutomataManager.Instance.OnInputChecked -= Check;
        _parent.OnDeleteItem -= ReduceIndex;
    }
}
