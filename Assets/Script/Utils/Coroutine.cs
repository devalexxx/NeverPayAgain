using System;
using System.Collections;

public static class CoroutineUtils
{

    // Runs a coroutine and executes the output action once it completes.
    public static IEnumerator Run<T>(IEnumerator target, Action<T> output)
    {
        object result = null;
        while (target.MoveNext())
        {
            result = target.Current;
            yield return result;
        }
        output((T)result);
    }
    
}
