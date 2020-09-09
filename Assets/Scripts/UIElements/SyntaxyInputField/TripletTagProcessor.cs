using System.Collections.Generic;
using System.Linq;
public class TripletTagProcessor : IInputProcessor
{
    public List<string> GetTags(string s)
    {
        List<string> output = new List<string>();
        string[] tags = s.Split(';');

        foreach (var word in tags)
        {
            if (word.Trim().Length == 0) continue;
            var currentTagFormat = GetCurrentTagFormat(word);
            output.Add(currentTagFormat.GetAllTogether());
        }
        return output;

    }
    public bool SyntaxCheck(string s)
    {
        string[] tags = s.Split(';');
        foreach (var word in tags)
        {
            if (word.Trim().Length == 0) continue;

            if(!CheckCurrentWord(word))return false;
        }
        return true;
    }
    private TagFormat GetCurrentTagFormat(string word)
    {
        TagFormat t = new TagFormat();
        int step = 0;
        for (int i = 0; i < word.Length; i++)
        {
            char ch = word[i];

            if (ch == '$' && step == 0)
            {
                t.input = ch;
                step++;
                continue;
            }

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
        return t;
    }
    private bool CheckCurrentWord(string word)
    {
        int step = 0;
        for (int i = 0; i < word.Length; i++)
        {
            char ch = word[i];

            if (step == 0 && ch == '$')
            {
                step++;
                continue;
            }

            if (step != 0 && ch == 'λ')
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

        return true;
    }
}