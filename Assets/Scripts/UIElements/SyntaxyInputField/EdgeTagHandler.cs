using System.Linq;
using UnityEngine;
using UnityEngine.UI;


class EdgeTagHandler : MonoBehaviour
{
    [SerializeField] private Color correct=Color.white;
    [SerializeField] private Color wrong=Color.white;
    [SerializeField] private Color syntaxerror= Color.white;

    private int _from, _to;
    private InputField _inputField = null;
    private RectTransform _inputRect=null;
    private Vector2 _size;
    private AutomataType _type ;
    private static IInputProcessor _inputCheck = null;
    private void Start()
    {
        _type = AutomataManager.automataType;
        _inputField = GetComponent<InputField>();
        _inputRect = GetComponent<RectTransform>();
        _size = _inputRect.sizeDelta;
        _inputRect.localScale = Vector3.one;

        if (_inputCheck == null)
        {
            if (_type == AutomataType.dfa)
            {
                _inputCheck = new SingleTagProcessor();
            }
            else if (_type == AutomataType.DPDA || _type == AutomataType.Turing)
            {
                _inputCheck = new TripletTagProcessor();
            }
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
        if (_type == AutomataType.DPDA)
        {
            _inputField.text=_inputField.text.Replace('.', 'λ');
        }
        if (_type == AutomataType.Turing)
        {
            _inputField.text = _inputField.text.Replace('.', '□');
        }
        string[] lines = _inputField.text.Split('\n');
        int max = lines.Max(str => str.Length)/2;
        max = Mathf.Clamp(max, 4, 20);
        _inputRect.sizeDelta = new Vector2(_size.x* max, _size.y * lines.Length/2);
    }
    public void CheckEnteredEdgeTag()
    {
        bool connectionResult = true;

        bool syntaxResult = _inputCheck.SyntaxCheck(_inputField.text);
        Debug.Log("syntax result : "+syntaxResult);
        if (syntaxResult)
        {
            AutomataManager.Instance.RemoveConnections(_from, _to);
            var tags = _inputCheck.GetTags(_inputField.text);
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
