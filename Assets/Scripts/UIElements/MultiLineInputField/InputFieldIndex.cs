using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldIndex : MonoBehaviour
{
    [SerializeField] InputField inputField;
    [SerializeField] Button removeButton;
    [SerializeField] Text textField;
    [SerializeField] Color isOk;
    [SerializeField] Color notOk;


    private MultilineInputField parent;
    public int index=-1;
    private void Start()
    {
        parent = GetComponentInParent<MultilineInputField>();

        inputField.onEndEdit.AddListener(delegate { parent.ChangeInput(inputField,index); });
        removeButton.onClick.AddListener(delegate { parent.DeleteItem(index); });

        parent.OnDeleteItem += ReduceIndex;
        AutomataManager.Instance.OnCheckInput += Check;
    }

    private void Check(int i,bool result,bool mustAccept)
    {
        if (i != index) return;

        inputField.image.color = mustAccept ? result ? isOk : notOk : result ? notOk : isOk;
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
