using System.Diagnostics;

public abstract class Automata
{
    protected string[] Alphabet;

    protected abstract void OnAddState();
    protected abstract void OnDeleteState();
    protected abstract string GetTag();
}