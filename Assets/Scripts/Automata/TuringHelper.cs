using System.Collections;
using UnityEngine;

public class TuringHelper :MonoBehaviour
{
    Coroutine _checking=null;
    public void StopChecking()
    {
        if(_checking != null)
        StopCoroutine(_checking);
        _checking = null;
    }
    public void StartChecking(string input,Turing turing)
    {
        if (_checking == null)
        {
            _checking = StartCoroutine(DoCheck(input, turing));
        }
    }
    private IEnumerator DoCheck(string input,Turing turing)
    {
        bool result = false;
        int i = 0;
        while (i<10)
        {
            Debug.Log(i);
            i++;
            yield return null;
        }

        turing.IsAnswerReady = true;
        turing.Result = result;
        _checking = null;
    }
}
