﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPDA : Automata
{
    public DPDA()
    {
        _states = new Dictionary<int, DState>();
        SubscribeEvents();
    }

    public override bool CheckInput(string input)
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
            foreach (var t in tagFormats)
            {
                if (t.machine == _stack.Peek())
                {
                    _stack.Pop();
                    for (int j = t.machineCommand.Length - 1; j >= 0; j--)
                    {
                        if (t.machineCommand[j] == 'λ')
                        {
                            break;
                        }
                        _stack.Push(t.machineCommand[j]);
                    }
                    current = current.GetNextState(t.GetAllTogether());
                    break;
                }
            }
        }
        if (current!=null && (current.Status == Status.FINAL || current.Status == Status.STARTANDFINAL))
        {
            return true;
        }
        return false;

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