using System.Collections.Generic;
using System.Linq;

public class DPDAInput : IInputProcessor
{
    public List<string> GetTags(string s)
    {
        List<string> output = new List<string>();
        string[] tags = s.Split(';');

        foreach (var word in tags)
        {
            TagFormat t = new TagFormat();
            int step = 0;
            for (int i = 0; i < word.Length; i++)
            {
                char ch = word[i];

                if (!char.IsLetter(ch) && !char.IsDigit(ch)) continue;

                if (step == 0)
                {
                    t.input = ch;
                }

                if (step == 1)
                {
                    t.machine = ch;
                }

                if (step > 1)
                {
                    t.machineCommand += ch;
                }

                step++;
            }
            output.Add(t.GetAllTogether());
        }
        return output;

    }

    public bool SyntaxCheck(string s)
    {
        string[] tags = s.Split(';');
        int step = 0;
        foreach (var word in tags)
        {
            for (int i = 0; i < word.Length; i++)
            {
                char ch = word[i];

                if (ch != 'λ')
                {
                    step++;
                    continue;
                }
                    if (!char.IsLetter(ch) && !char.IsDigit(ch)) continue;

                if (step == 0 && !AutomataManager.inputAlphabet.Contains(ch)) return false;

                if (step > 0 && !AutomataManager.machineAlphabet.Contains(ch)) return false;

                step++;
            }
            if (step < 3) return false;
        }
        return true;
    }
}