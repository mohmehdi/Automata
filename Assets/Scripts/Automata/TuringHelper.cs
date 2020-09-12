using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UIElements;

public class TuringHelper :MonoBehaviour
{
    private Coroutine _checking=null;
    public void StopChecking()
    {
        if(_checking != null)
        StopCoroutine(_checking);
        _checking = null;
    }
    public void StartChecking(string input,Turing turing)
    {
        turing.StopChechingInput();
        if (_checking == null)
        {
            _checking = StartCoroutine(DoCheck(input, turing));
        }
    }
    private IEnumerator DoCheck(string input,Turing turing)
    {
        bool result = false;
        bool isHalt = false;

        if (!turing.DeterministicCheck())
            isHalt = true;

        DState current = turing.GetStartState();
        StringBuilder tape = new StringBuilder(input);
        int head = 0;

        while (!isHalt)
        {
            if (head>=tape.Length || head<0)
            {
                if (head < 0) head = 0;
                tape.Insert(head, '□');
            }

            var next = turing.GetNextState(current , tape[head]);

            if (next == null)
            {
                isHalt = true;
                result = current.IsFinal();
            }
            else
            {
                var tagFormat = current.GetTags(tape[head])[0];
                tape[head] = tagFormat.machine;
                head += tagFormat.machineCommand.ToLower().Contains("r") ? +1 : -1;
                current = next;
            }
            yield return null;
        }

        turing.IsAnswerReady = true;
        turing.Result = result;
        _checking = null;
    }
}