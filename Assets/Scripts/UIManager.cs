using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private InputField alphabet = null;
    private string[] _alphabet;
    private void Start()
    {
        Instance = this;
        MousePosition m = new MousePosition();
    }
    public string[] get_alphabet_from_field()
    {
        _alphabet = alphabet.text.Split(',');
        return _alphabet;
    }
}
