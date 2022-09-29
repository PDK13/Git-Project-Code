using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simple_Coroutine : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Set_Coroutne_1());
        StartCoroutine(Set_Coroutne_2());
        StartCoroutine(Set_Coroutne_3());
    }

    private IEnumerator Set_Coroutne_1()
    {
        //Skip 1 frame
        yield return null;

        Debug.LogFormat("{0}: Coroutine 1");
    }

    private IEnumerator Set_Coroutne_2()
    {
        yield return null;
        yield return null;

        Debug.LogFormat("{0}: Coroutine 2");
    }

    private IEnumerator Set_Coroutne_3()
    {
        yield return null;
        yield return null;
        yield return null;

        Debug.LogFormat("{0}: Coroutine 3");
    }
}
