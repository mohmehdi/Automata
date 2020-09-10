using UnityEngine;

class EdgeTagHandlerInitializer : MonoBehaviour
{
    private RectTransform _tagHandlerRect;
    public void Initialize(GameObject inputField)
    {
        CreateDropDowns(inputField);
    }

    private void CreateDropDowns(GameObject inputField )
    {
        GameObject inp = Instantiate(inputField);
        inp.transform.SetParent(UIManager.Instance.transform);
            _tagHandlerRect = inp.GetComponent<RectTransform>();
    }
    public void SetInputFieldOptions(int from,int to)
    {
        _tagHandlerRect.GetComponent<EdgeTagHandler>().SetOptions(from, to);
    }
    public void SetPosition(Vector2 position)
    {
        _tagHandlerRect.transform.position = MousePosition.GetCamera().WorldToScreenPoint(position);
    }

}
