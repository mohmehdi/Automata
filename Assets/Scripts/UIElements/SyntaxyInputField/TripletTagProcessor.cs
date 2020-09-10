using System.Collections.Generic;
using System.Linq;
using UnityEngine.Rendering;

public class TripletTagProcessor : IInputProcessor
{
    private AutomataType _automataType;


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
            if (!CheckCurrentWord(word)) return false;
        }
        return true;
    }

    private bool CheckCurrentWord(string word)
    {
        int step = 0;
        for (int i = 0; i < word.Length; i++)
        {
            char ch = word[i];
            if(step<2 && ch == '□' && AutomataManager.automataType == AutomataType.Turing)
            {
                step++;
                continue;
            }
            if (!char.IsLetterOrDigit(ch)) continue;

            if (step == 0 && !AutomataManager.inputAlphabet.Contains(ch)) return false;

            if(step ==2 && AutomataManager.automataType == AutomataType.Turing)
            {
                if (char.ToLower(ch) != 'l'  && char.ToLower(ch) != 'r')
                {
                    return false;
                }
            }
            else if (step > 0 && !AutomataManager.machineAlphabet.Contains(ch)) return false;

            step++;
        }
        if (step > 3 && AutomataManager.automataType == AutomataType.Turing) return false;
        if (step < 3) return false;

        return true;
    }

    private TagFormat GetCurrentTagFormat(string word)
    {
        TagFormat t = new TagFormat();
        int step = 0;
        for (int i = 0; i < word.Length; i++)
        {
            char ch = word[i];

            if (ch == '$' && step == 0 && AutomataManager.automataType == AutomataType.DPDA)
            {
                t.input = ch;
                step++;
                continue;
            }

            if (step < 2 && ch == '□' && AutomataManager.automataType == AutomataType.Turing)
            {
                t.input = ch;
                step++;
                continue;
            }

            if (!char.IsLetterOrDigit(ch)) continue;

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
}