using System.Collections;
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
            var tagFormats = current.GetTags(input[i]);
            foreach (var currentTagFormat in tagFormats.Where(t => t.machine == _stack.Peek()))
            {
                if (_stack.Peek() != 'λ')
                _stack.Pop();

                PushToStack(_stack, currentTagFormat);

                current = current.GetNextState(currentTagFormat.GetAllTogether());
                break;
            }
        }
        if (current!=null && (current.Status == Status.FINAL || current.Status == Status.STARTANDFINAL))
        {
            return true;
        }
        return false;

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
        return true;
    }
}