using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/// <summary>
/// D is Short for Deterministic
/// </summary>
public class DState 
{
    public Status Status { get;  set; }

    private Dictionary<string,DState> _connections;    
    public DState()
    {
        _connections = new Dictionary<string, DState>();
        Status = Status.NORMAL;
    }
    public DState GetNextState(string tag)
    {
        if (_connections.ContainsKey(tag))
        {
            return _connections[tag];
        }
        return null;
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
    public void RemoveConnectionsTo(DState to)
    {
        foreach (var item in _connections.Where(kvp => kvp.Value == to).ToList())
        {
            _connections.Remove(item.Key);
        }
    }
    public bool ContainTag(string tag)
    {
        return _connections.ContainsKey(tag);
    }
}