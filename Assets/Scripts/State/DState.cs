using System.Collections.Generic;
using System.Linq;
using UnityEngine;
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
            Debug.Log("Current connention currently added with : " + tag + " to state : " + _connections[tag]);
            return false;
        }
        if (AutomataManager.automataType == AutomataType.DPDA)
        {
            var allSameInputChar = GetTags(tag[0]);
            foreach (var _ in allSameInputChar.Where(item => tag[1] == item.machine).Select(item => new { }))
            {
                return false;
            }
        }
        if (AutomataManager.automataType == AutomataType.Turing)
        {
            foreach (var item in _connections)
            {
                if (item.Key[0]==tag[0])
                {
                    return false;
                }
            }
        }
        _connections.Add(tag, to);
        return true;
    }
    public void RemoveConnectionsTo(DState to)
    {
        List<KeyValuePair<string, DState>> items = _connections.Where(kvp => kvp.Value == to).ToList();
        foreach (var item in items)
        {
            _connections.Remove(item.Key);
        }
    }
    public bool ContainTag(string tag)
    {
        return _connections.ContainsKey(tag);
    }
    public List<TagFormat> GetTags(char inputChar)
    {
        var tags = new List<TagFormat>();
        foreach (var c in _connections)
        {
            if (c.Key[0] == inputChar) // first part of string is input character
            {
                string s = c.Key;
                TagFormat t = new TagFormat();
                t.input = s[0];
                t.machine= s[1];
                t.machineCommand= s.Substring(2);
                tags.Add(t);
            }
        }
        if (tags.Count == 0) return null;
        return tags;
    }
    public bool IsFinal()
    {
        if ((Status == Status.FINAL || Status == Status.STARTANDFINAL))
        {
            return true;
        }
        return false;
    }
}