using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private MultiLinePanel Acceptable=null;
    [SerializeField] private MultiLinePanel NotAcceptable=null;
    [SerializeField] private InputField languageAlphabet = null;
    [SerializeField] private InputField machineAlphabet = null;
    [SerializeField] private Dropdown typeSelection = null;
    [SerializeField] private Text alphabetView = null;
    [SerializeField] private GameObject panel = null;
    [SerializeField] private GameObject startButton = null;

    private char[] _alphabet;
    private int _stage = 0;
    private void Start()
    {
        Instance = this;
        MousePosition m = new MousePosition();
    }
    public List<string> GetInputs(bool isAcceptsList) => isAcceptsList ? Acceptable.inputs : NotAcceptable.inputs;
    private void SetStage(int stage)
    {
        typeSelection.gameObject.SetActive(0 == stage);
        languageAlphabet.gameObject.SetActive(1 == stage);
        machineAlphabet.gameObject.SetActive(2 == stage);
        startButton.gameObject.SetActive(3 == stage);
        panel.SetActive(!(4 == stage));
    }
    public void NextStage()
    {
        _stage++;
        if (AutomataManager.automataType == AutomataType.dfa && _stage == 2)
        {
            _stage++;
        }
        if (_stage > 3)
        {
            _stage = 3;
        }
        SetStage(_stage);
    }
    public void PrevtStage()
    {
        _stage--;
        if (AutomataManager.automataType == AutomataType.dfa && _stage == 2)
        {
            _stage--;
        }
        if (_stage < 0)
        {
            _stage = 0;
        }
        SetStage(_stage);
    }

    public void SelectType()
    {
        AutomataManager.automataType = (AutomataType)typeSelection.value;
    }
    /// <summary>
    /// used by Start Button to set alphabet
    /// </summary>
    /// <returns></returns>
    public char[] GetLanguageAlphabet()
    {
        _alphabet = languageAlphabet.text.ToCharArray();
        string s = "";
        for (int i = 0; i < _alphabet.Length; i++)
        {
            s+= _alphabet[i].ToString()+(i==_alphabet.Length-1? "" :  " , ");
        }
        alphabetView.text = "Σ = { " + s +" } ";
        if (languageAlphabet.text.Length < 1)
            return null;

        SetStage(4);
        return _alphabet;
    }
    public char[] GetMachineAlphabet()
    {
        SetStage(4);
        return machineAlphabet.text.ToCharArray();
    }
}
