using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldIndex : MonoBehaviour
{
    [SerializeField] InputField inputField;
    [SerializeField] Button removeButton;

    private MultilineInputField parent;
    public int index=-1;
    private void Start()
    {
        parent = GetComponentInParent<MultilineInputField>();

        inputField.onEndEdit.AddListener(delegate { parent.ChangeInput(inputField); });
        removeButton.onClick.AddListener(delegate { parent.DeleteItem(index); });

        parent.OnDeleteItem += ReduceIndex;
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
        parent.OnDeleteItem -= ReduceIndex;
    }
}
