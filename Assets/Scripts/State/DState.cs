using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// D is Short for Deterministic
/// </summary>
public class DState 
{
    public Status Status { get; private set; } //this will used for Automatas classes for later
    public int StateID { get;private set; }

    private Dictionary<string,DState> _connections;    

    public DState(int id)
    {
        _connections = new Dictionary<string, DState>();
        Status = Status.NORMAL;
        StateID = id;
    }
    public DState GetNextState(string tag)
    {
        return _connections[tag];
    }
    public bool Connect(string tag, DState to)
    {
            if (_connections.ContainsKey(tag))
            {
                Debug.Log("Current connention currently added with : " + tag +" to state : "+  _connections[tag].StateID);
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