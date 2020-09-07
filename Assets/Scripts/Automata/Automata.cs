using System.Collections.Generic;

public abstract class Automata
{
    protected  Dictionary<int,DState> _states;
    protected DState _start = null;
   // protected abstract void OnAddState();
    //protected abstract void OnDeleteState(int id);
    //public abstract bool TryConnect(int from, string tag, int to);
    //public abstract void RemoveConnections(int from,int to);
    //public abstract void ChangeStatus(int id, Status status);
    public abstract bool CheckInput(string input);

    protected void SubscribeEvents()
    {
        BuildStateEvents.Instance.OnCreateState += OnAddState;
        BuildStateEvents.Instance.OnDeleteState += OnDeleteState;
        BuildStateEvents.Instance.OnChangeStatus += ChangeStatus;
    }

    protected void OnAddState()
    {
        int id = AutomataManager.CurrentStateId;
        _states.Add(id, new DState());
    }

    protected  void OnDeleteState(int id)
    {
        foreach (var s in _states)
        {
            s.Value.RemoveConnectionsTo(_states[id]);
        }
        _states.Remove(id);
    }

    public bool TryConnect(int from , string tag ,int to)
    {
        return _states[from].TryConnect(tag, _states[to]);
    }
    public  void RemoveConnections(int from,int to)
    {
        _states[from].RemoveConnectionsTo(_states[to]);
    }    
    public  void ChangeStatus(int id, Status status)
    {
        if (status == Status.START || status == Status.STARTANDFINAL)
        {
            _start = _states[id];
            foreach (var s in _states)
            {
                if (s.Value.Status == Status.START)
                {
                    s.Value.Status = Status.NORMAL;
                    break;
                }
                if (s.Value.Status == Status.STARTANDFINAL)
                {
                    s.Value.Status = Status.FINAL;
                    break;
                }
            }
        }
        _states[id].Status = status;
    }
}