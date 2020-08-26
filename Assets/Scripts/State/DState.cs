using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// D is Short for Deterministic
/// </summary>
public class DState 
{
    public Status Status { get; private set; } //this will used for Automatas classes for later

    private Dictionary<string,DState> _connections;    

    public DState()
    {
        _connections = new Dictionary<string, DState>();
        Status = Status.NORMAL;
    }
    public DState GetNextState(string tag)
    {
        return _connections[tag];
    }
    public bool TryConnect(string tag, DState to)
    {
            if (_connections.ContainsKey(tag))
            {
                Debug.Log("Current connention currently added with : " + tag +" to state : "+  _connections[tag]);
                return false;
            }
        _connections.Add(tag,to);
        return true;
    }
    public void Disconnect(string tag, DState to)
    {
        if (_connections.ContainsKey(tag))
        {
            _connections.Remove(tag);
        }
    }
}