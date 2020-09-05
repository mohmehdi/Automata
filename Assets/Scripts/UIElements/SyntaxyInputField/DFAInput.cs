using System.Collections.Generic;
using System.Linq;

public class DFAInput : IInputProcessor
{
    public bool SyntaxCheck(string s)
    {
        foreach (var ch in s)
        {
            if (char.IsLetter(ch) || char.IsDigit(ch))
                if (!AutomataManager.inputAlphabet.Contains(ch))
                {
                    return false;
                }
        }
        return true;
    }

    public List<string> GetTags(string s)
    {
        string input = "";
        var output = new List<string>();
        foreach (var ch in s)
        {
            if ((char.IsDigit(ch) || char.IsLetter(ch)) && !input.Contains(ch.ToString()))
            {
                input += ch;
                output.Add(ch.ToString());
            }
        }
        return output;
    }
}
