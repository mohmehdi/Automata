using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateNameChanger : MonoBehaviour
{
    [SerializeField] TextMesh stateName = null;
    [SerializeField] InputField InputField = null;

    public void ChangeName()
    {
        if (InputField.text.Length == 0)
            return;
        stateName.text = InputField.text;
        InputField.text = "";
    }
}
