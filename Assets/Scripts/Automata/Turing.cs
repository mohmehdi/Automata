using System.Collections.Generic;

public class Turing : Automata
{
    public Turing()
    {
        _states = new Dictionary<int, DState>();
        SubscribeEvents();
    }
    public override bool CheckInput(string input)
    {
        return false;
    }
}
