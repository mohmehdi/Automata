using System.Collections.Generic;
public class State
{
    public Status Status { get; private set; }
    private Dictionary<string, State> _states;
    private int stateID;
    public State(int id)
    {
        _states = new Dictionary<string, State>();
        Status = Status.NORMAL;
        stateID = id;
    }
    public void Connect(string label, State s)
    {
        if (ContainKey(label))
            _states[label] = s;
        else
            _states.Add(label, s);
    }
    public void Disconnect(string label)
    {
        if (ContainKey(label))
            _states[label] = null;
    }
    public State GetNextState(string label)
    {
        return ContainKey(label) ? _states[label] : null;
    }
    public bool ContainKey(string label) => _states.ContainsKey(label);
}