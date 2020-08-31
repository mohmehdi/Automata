using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class MultiLineInputFieldSlide : MonoBehaviour
{
    [SerializeField] private float speed;
    private RectTransform _rectTransform;
    private Coroutine slide;
    [SerializeField]
    private bool isOn = true;
    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void DoSlide()
    {
        //if (slide == null)
        {
            slide = StartCoroutine(SlideMove());
        }
    }
    private IEnumerator SlideMove()
    {
        float xpos = _rectTransform.localPosition.x +(isOn? - 200:+200);
        bool b = true ;
        Vector3 vec = (isOn ? Vector3.left : Vector3.right);
        while (b)
        {
            b = isOn ? _rectTransform.localPosition.x > xpos : _rectTransform.localPosition.x < xpos;
            Debug.Log(_rectTransform.localPosition.x);
            _rectTransform.localPosition +=vec * speed;
            yield return null;
        }
        isOn = !isOn;
    }
}
