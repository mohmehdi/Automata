using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class DropDownOptionManager : MonoBehaviour
{
    [SerializeField] private Dropdown dropDown;

    private void Start()
    {
        SetOptions();
    }
    private void SetOptions()//can take a int to set diffrent alphabets from automatas
    {
        var alphabet = AutomataManager.Alphabet;
        List<Dropdown.OptionData> data = new List<Dropdown.OptionData>();
        for (int i = 0; i < alphabet.Length; i++)
        {
            Debug.Log(alphabet[i]);
            Dropdown.OptionData option = new Dropdown.OptionData(alphabet[i]);
            data.Add(option);
        }
        dropDown.ClearOptions();
        dropDown.AddOptions(data);
        dropDown.onValueChanged.AddListener(delegate { DropDownValueChanged(); });
    }
    public void DropDownValueChanged()
    {
        Debug.Log(dropDown.value);
        AutomataManager.ChangeTag(dropDown.value);
    }

}
