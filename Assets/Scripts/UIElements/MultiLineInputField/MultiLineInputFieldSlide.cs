using System.Collections;
using UnityEngine;


public class MultiLineInputFieldSlide : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    [SerializeField] RectTransform buttonPos=null; //position of Kesho button 
    [SerializeField] RectTransform button=null;
    [SerializeField] private bool isOn = true;
    [SerializeField] KeyCode switchKey=KeyCode.None;

    private RectTransform _rectTransform;

    private Coroutine slide;
    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(switchKey))
        {
            DoSlide();
        }
    }
    public void DoSlide()
    {
        if (slide == null)
        {
            slide = StartCoroutine(SlideMove());
        }
    }
    private IEnumerator SlideMove()
    {
        float size = _rectTransform.localScale.x;
        bool flag = isOn ? size >= 0.01f : size <= 0.99f;
        while (flag)
        {
            flag = isOn ? size >= 0.01f : size <= 0.99f;
            size += isOn ? -speed : speed;
            _rectTransform.localScale = new Vector3(size, 1, 1);

            button.position = buttonPos.position;
            yield return null;
        }

        _rectTransform.localScale = isOn ? new Vector3(0, 1, 1) : new Vector3(1, 1, 1);
        button.position = buttonPos.position;

        isOn = !isOn;

        StopCoroutine(slide);
        slide = null;
    }
}
