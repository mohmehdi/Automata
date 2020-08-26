using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class DropDownOptionManager : MonoBehaviour
{
    private Dropdown dropDown;
    [SerializeField] Sprite selected, notSelected;
    public int _from, _to;
    private void Start()
    {
        dropDown = GetComponent<Dropdown>();
        if (dropDown == null)
        {
            Debug.Log("Set dropDown u Asshole");
        }
        SetOptions();
    }
    private void SetOptions()//can take a int to set diffrent inputs from automatas
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

        //dropDown.onValueChanged.AddListener(delegate {CheckConnection(); });
    }
    private void CheckConnection()
    {
        bool res = AutomataManager.Instance.TryConnect(_from, dropDown.options[dropDown.value].text, _to);
        Debug.Log("Checking connection --> result :  " + res);
    }
}
