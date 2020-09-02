using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldIndex : MonoBehaviour
{
    [SerializeField] InputField inputField=null;
    [SerializeField] Button removeButton=null;
    [SerializeField] Text textField=null;
    [SerializeField] Color isOk=Color.white;
    [SerializeField] Color notOk=Color.white;


    private MultilineInputField parent;
    public int index=-1;
    public bool isAcceptInput = true;
    private void Start()
    {
        parent = GetComponentInParent<MultilineInputField>();

        inputField.onEndEdit.AddListener(delegate { parent.ChangeInput(inputField,index); });
        removeButton.onClick.AddListener(delegate { parent.DeleteItem(index); });

        parent.OnDeleteItem += ReduceIndex;
        AutomataManager.Instance.OnCheckInput += Check;
    }
    public void Initialize(int i , bool isAccept)
    {
        index = i;
        isAcceptInput = isAccept;
    }

    private void Check(int i,bool result,bool mustAccept)
    {
        if (i != index || mustAccept != isAcceptInput) return;

        inputField.image.color = mustAccept ? (result ? isOk : notOk) : (result ? notOk : isOk);
    }
    public void ReduceIndex(int i)
    {
        if (i<index)
        {
            index--;
        }
    }
    private void OnDestroy()
    {
        AutomataManager.Instance.OnCheckInput -= Check;
        parent.OnDeleteItem -= ReduceIndex;
    }
}
