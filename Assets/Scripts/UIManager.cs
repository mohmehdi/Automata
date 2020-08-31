using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private MultilineInputField MultiField_Accepts;
    [SerializeField] private MultilineInputField MultiField_NotAccepts;
    [SerializeField] private InputField alphabet = null;
    [SerializeField] private GameObject panel = null;
    private string[] _alphabet;
    private void Start()
    {
        Instance = this;
        MousePosition m = new MousePosition();
    }
    public List<string> GetInputs(bool isAccepts) => isAccepts ? MultiField_Accepts._inputs : MultiField_NotAccepts._inputs;
    public string[] get_alphabet_from_field()
    {
        _alphabet = alphabet.text.Split(',');

        panel.SetActive(false);
        return _alphabet;
    }
}
