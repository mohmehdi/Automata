using System.Collections;
using UnityEngine;


public class MultiLinePanelSlide : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    [Tooltip("position in child that button will move with it")]
    [SerializeField] RectTransform destenation=null;
    [SerializeField] RectTransform slideButton=null;
    [SerializeField] private bool isOn = true;
    [SerializeField] KeyCode switchKey=KeyCode.None;

    private RectTransform _rectTransform;
    private Coroutine _slideMove;
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
        if (_slideMove == null)
        {
            _slideMove = StartCoroutine(SlideMove());
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

            slideButton.position = destenation.position;
            yield return null;
        }

        _rectTransform.localScale = isOn ? new Vector3(0, 1, 1) : new Vector3(1, 1, 1);
        slideButton.position = destenation.position;

        isOn = !isOn;
        _slideMove = null;
    }
}
