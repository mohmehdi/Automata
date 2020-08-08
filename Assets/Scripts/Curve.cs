using System;
using DefaultNamespace;
using UnityEngine;

public class Curve:MonoBehaviour
{
    [SerializeField] //just for now to see it works 
    private int _startStateID;
    private int _lastStateID;

    //TODO: add some id
    /// <summary>
    ///some id that gets from Automata get somthing method like for DFA: DFA.GetLabel() -> a
    /// or for PDA : PDA.get() -> a/b/z
    /// </summary>
    private void Start()
    {
        _startStateID = StateConnectionEvents.Instance.firstStateID.stateID;
    }
}
