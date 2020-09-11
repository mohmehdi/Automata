using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputStringCheck :MonoBehaviour
{
    List<string> mustAccept;
    List<string> mustNotAccept;
    Coroutine checkingCoroutine=null;
    public void StopChecking()
    {
        if (checkingCoroutine != null)
        StopCoroutine(checkingCoroutine);
        checkingCoroutine = null;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StopChecking();
        }
    }
    public void StartCheck(List<string> must, List<string> mustNot,Automata machine)
    {
        mustAccept = must;
        mustNotAccept = mustNot;

        if(checkingCoroutine != null)
        {
            StopCoroutine(checkingCoroutine);
            checkingCoroutine = null;
        }
        if (checkingCoroutine == null)
        {
            checkingCoroutine = StartCoroutine(CheckStrings(machine));
        }
    }
    public IEnumerator CheckStrings(Automata machine)
    {
        int readyForNext = 0;
        for (int i = 0; i < mustAccept.Count;)
        {
            if (readyForNext == i)
            {
                machine.StartCheckingInput(mustAccept[i]);
                readyForNext++;
            }
            if (machine.IsAnswerReady)
            {
                AutomataManager.Instance.ApplyInputCheck(i, machine.Result, true);
                i++;
            }
            yield return null;
        }
        readyForNext = 0;
        for (int i = 0; i < mustNotAccept.Count;)
        {
            if (readyForNext == i)
            {
                machine.StartCheckingInput(mustNotAccept[i]);
                readyForNext++;
            }
            if (machine.IsAnswerReady)
            {
                AutomataManager.Instance.ApplyInputCheck(i, machine.Result, false);
                i++;
            }
            yield return null;
        }
        checkingCoroutine = null;
    }
}