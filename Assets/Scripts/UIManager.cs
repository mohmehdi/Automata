using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.XR.WSA.Input;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private InputField alphabet;
    [SerializeField] private Text currentTag;
    [SerializeField] private GameObject view;
    [SerializeField] private GameObject btn;

    private void Start()
    {
        Instance = this;
        MousePosition m = new MousePosition();
    }
    public string[] get_alphabet_from_field()
    {
        return alphabet.text.Split(',');
    }
    public void set_alphabet_buttons()
    {
        int n = alphabet.text.Split(',').Length;
        for (int i = 0; i < n; i++)
        {
            GameObject b = Instantiate(btn);
            b.transform.SetParent(view.transform, false);
            RectTransform r = b.GetComponent<RectTransform>();
            r.transform.localPosition = new Vector3(r.transform.localPosition.x, -i * r.rect.height, r.transform.localPosition.z);
        }
    }
}
