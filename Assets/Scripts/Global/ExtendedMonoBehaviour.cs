using System;
using System.Collections;
using UnityEngine;

public class ExtendedMonoBehaviour : MonoBehaviour
{
    public void InvokeLater(Action action, float delay)
    {
        StartCoroutine(WaitAndRun(action, delay));
    }

    private IEnumerator WaitAndRun(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }
}