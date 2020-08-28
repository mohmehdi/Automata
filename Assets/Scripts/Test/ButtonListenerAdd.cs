using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ButtonListenerAdd : MonoBehaviour
{
    [SerializeField]private Dropdown drop=null;
    // Start is called before the first frame update
    void Start()
    {
        drop.onValueChanged.AddListener(delegate { IDK(); });
    }
    public void IDK()
    {
        Debug.Log("Shit");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
