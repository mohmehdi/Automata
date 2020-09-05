using System;
using UnityEngine;
using UnityEngine.UI;


class SyntaxyInputField : MonoBehaviour
{
    [SerializeField] private Color correct=Color.white;
    [SerializeField] private Color wrong=Color.white;
    [SerializeField] private Color syntaxerror= Color.white;

    private int _from, _to;
    private InputField _inputField = null;
    private RectTransform _inputRect=null;
    private Vector2 _size;
    private AutomataType _type ;
    private void Start()
    {
        _type = AutomataManager.automataType;
        _inputField = GetComponent<InputField>();
        _inputRect = GetComponent<RectTransform>();
        _size = _inputRect.sizeDelta;
        _inputRect.localScale = Vector3.one;
        if (_inputField==null)
        {
            Debug.Log("Null refrence error");
        }
        gameObject.SetActive(false);

        ConnectionEvents.Instance.OnEditMode += OnActiveEditMode;
        BuildStateEvents.Instance.OnDeleteState += DestroyThisWhenStateDeleted;

    }

    private void OnActiveEditMode(bool flag)
    {
        gameObject.SetActive(!flag);
    }
    private void OnDestroy()
    {
        ConnectionEvents.Instance.OnEditMode -= OnActiveEditMode;
        BuildStateEvents.Instance.OnDeleteState -= DestroyThisWhenStateDeleted;
    }
    private void DestroyThisWhenStateDeleted(int id)
    {
        if (_from == id || _to == id)
        {
            Destroy(gameObject);
        }
    }
    public void SetOptions(int from,int to)
    {
        _from = from;
        _to = to;
    }

    public void ChangeFieldSize()
    {
        _inputRect.sizeDelta = new Vector2(_size.x, _size.y * _inputField.text.Split('\n').Length/2);
    }
    public void CheckInput()
    {
        bool connectionResult = true;

        IInputProcessor inputCheck=null;
        if (_type == AutomataType.dfa)
        {
            inputCheck = new DFAInput();
        }
        bool syntaxResult = inputCheck.SyntaxCheck(_inputField.text);
        Debug.Log("syntax result : "+syntaxResult);
        if (syntaxResult)
        {
            AutomataManager.Instance.RemoveConnections(_from, _to);
            var tags = inputCheck.GetTags(_inputField.text);
            foreach (var t in tags)
            {
               bool res = AutomataManager.Instance.TryConnect(_from, t, _to);
                if (connectionResult)
                {
                    connectionResult = res;
                }
            }
        }

        ChangeSkin(syntaxResult,connectionResult);
    }

    private void ChangeSkin(bool synRes,bool conRes)
    {
        if (!synRes)
        {
            _inputField.image.color = syntaxerror;
            return;
        }
        _inputField.image.color = conRes ? correct : wrong;
    }

    
}
