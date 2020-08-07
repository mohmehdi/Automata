using System.Diagnostics;

public abstract class Automata
{
    protected char[] Alphabet;

    protected abstract void OnAddState();
    protected abstract void OnDeleteState();
}