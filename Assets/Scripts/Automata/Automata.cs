using System.Diagnostics;

public abstract class Automata
{
    protected abstract void OnAddState();
    protected abstract void OnDeleteState(int id);
    public abstract bool TryConnect(int from, string tag, int to);
    public abstract bool TryDisConnect(int from, string tag, int to);
    public abstract void ChangeStatus(int id, Status status);
}