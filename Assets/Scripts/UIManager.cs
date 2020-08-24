using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private InputField alphabet;
    [SerializeField] private Text currentTag;
    [SerializeField] private GameObject view;
    [SerializeField] private UnityEngine.UI.Dropdown dropdown;
    [SerializeField] private UnityEngine.UI.Button setbtn;
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
    public void set_alphabet_buttons()
    {
        setbtn.enabled = false;
        string[] n = alphabet.text.Split(',');
        List<Dropdown.OptionData> data = new List<Dropdown.OptionData>();
        for (int i = 0; i < n.Length; i++)
        {
            //GameObject b = Instantiate(btn);

            //b.transform.SetParent(view.transform, false);
            //RectTransform r = b.GetComponent<RectTransform>();
            //r.transform.localPosition = new Vector3(r.transform.localPosition.x, -i * r.rect.height, r.transform.localPosition.z);
            //Debug.Log("i from set btns "+ i);
            //b.GetComponent<Button>().onClick.AddListener(delegate { getLetter(i); }) ;
            Dropdown.OptionData option = new Dropdown.OptionData(_alphabet[i]);
            data.Add(option);
        }
            dropdown.AddOptions(data);
    }
    public void getLetter()
    {
        ConnectionEvents.Instance.curveTag = _alphabet[dropdown.value];
    }
}
