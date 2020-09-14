using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DPDA : Automata
{
    public DPDA()
    {
        _states = new Dictionary<int, DState>();
        SubscribeEvents();
        IsAnswerReady = false;
    }
    public override void StartCheckingInput(string input)
    {
        IsAnswerReady = false;
        Result = CheckInput(input);
        IsAnswerReady = true;
    }
    
    public bool CheckInput(string input)
    {
        input += "$";
        if (!DeterministicCheck()) return false;

        Stack<char> _stack = new Stack<char>();
        _stack.Push('λ');
        _stack.Push('z');

        DState current = _start;
        for (int i = 0; i < input.Length; i++)
        {
            if (current == null) return false;

            char inputChar = input[i];
            List<TagFormat> tagFormats = null;

            tagFormats = current.GetTags(inputChar);
            if(tagFormats == null)
            {
                var list = current.GetTags('λ');
                if (list != null)
                {
                    i--;
                    tagFormats = list;
                }
            }
            if (tagFormats == null) return false;
            foreach (var currentTagFormat in tagFormats.Where(t => t.machine == _stack.Peek()))
            {
                if (_stack.Peek() != 'λ')
                    _stack.Pop();

                PushToStack(_stack, currentTagFormat);

                current = current.GetNextState(currentTagFormat.GetAllTogether());
                break;
            }
        }
        return current.IsFinal();

    }
    private static void PushToStack(Stack<char> _stack, TagFormat t)
    {
        for (int j = t.machineCommand.Length - 1; j >= 0; j--)
        {
            if (t.machineCommand[j] == 'λ')
            {
                break;
            }
            _stack.Push(t.machineCommand[j]);
        }
    }

    public bool DeterministicCheck()
    {
        if (_start == null)
        {
            Debug.LogWarning("DPDA needs a 'Start' state");
            return false;
        }
        foreach (var s in _states)
        {
            var lists = s.Value.GetTags('λ');
            if (lists != null)
            {
                if(s.Value.GetNumberOfConnections() > lists.Count)
                {
                    Debug.LogWarning("in DPDA if there is a λ move there must not be any other input moves");
                    return false;
                }
            }
        }
        return true;
    }
}