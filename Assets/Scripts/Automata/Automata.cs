using System.Diagnostics;

public abstract class Automata
{
    protected abstract void OnAddState();
    protected abstract void OnDeleteState();
    public abstract bool TryConnect(int from, string tag, int to);
}