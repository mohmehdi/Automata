using System;
using UnityEngine;
using UnityEngine.UI;

public class StateObject : MonoBehaviour
{
    [HideInInspector]
    public int ID;
    private void Start()
    {
        ID = AutomataManager.CurrentStateId - 1;
    }
}