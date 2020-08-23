using System.Collections.Generic;
using UnityEngine;
public class State
{
    public Status Status { get; private set; } //this will used for Automatas classes for later
    public int StateID { get;private set; }

    private List<ConnectionData> _connections;
   
    //dont README . im confusing
    //there is always just one connection from A with "tag" to B .keep it in mind
    //but in some automatas like dfa there is just one connection from A with "tag" ,because its deterministic
    

    public State(int id)
    {
        _connections = new List<ConnectionData>();
        Status = Status.NORMAL;
        StateID = id;
    }
    public List<State> GetNextStates(string tag)
    {
        var states = new List<State>();
        foreach (var c in _connections)
        {
            if (c.Tag == tag)
            {
                states.Add(c.To);
            }
        }
        return states;
    }
    public bool Connect(string tag, State to)
    {
        foreach (var c in _connections)
        {
            if (c.Tag == tag && c.To == to)
            {
                Debug.Log("Current connention currently added with : " + tag +" to state : "+  to.StateID );
                return false;
            }
        }
        _connections.Add(new ConnectionData( tag, to));
        return true;
    }
    public void Disconnect(string tag, State to)
    {
        foreach (var c in _connections)
        {
            if (c.Tag == tag && c.To == to)
            {
                _connections.Remove(c);
                //Debug.Log(tag + "remove sucseed bro. well done. im so proud of u.");
                return;
            }
        }
    }
}