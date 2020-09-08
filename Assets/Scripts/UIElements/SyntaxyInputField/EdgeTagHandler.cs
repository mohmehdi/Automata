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
    private IInputProcessor _inputCheck = null;
    private void Start()
    {
        _type = AutomataManager.automataType;
        _inputField = GetComponent<InputField>();
        _inputRect = GetComponent<RectTransform>();
        _size = _inputRect.sizeDelta;
        _inputRect.localScale = Vector3.one;

        if (_type == AutomataType.dfa)
        {
            _inputCheck = new SingleTagProcessor();
        }
        else if (_type == AutomataType.DPDA)
        {
            _inputCheck = new TripletTagProcessor();
        }

        if (_inputField==null)
        {
            Debug.Log("Null refrence error");
        }
        gameObject.SetActive(false);

        ConnectionEvents.Instance.OnEditMode += OnActiveEditMode;
        BuildStateEvents.Instance.OnDeleteState += DestroyThisWhenStateDeleted;

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            _inputField.text += 'λ';
        }
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
