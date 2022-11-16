using System.Collections;
using UnityEngine;

public class Simple_Coroutine : MonoBehaviour
{
    private Coroutine m_Coroutine;

    private void Start()
    {
        StartCoroutine(SetCoroutine1());
        StartCoroutine(SetCoroutine2());
        StartCoroutine(SetCoroutine3());
        StartCoroutine(SetCoroutine4());

        m_Coroutine = StartCoroutine(SetCoroutineFinal());
    }

    private IEnumerator SetCoroutine1()
    {
        //Skip 1 frame
        yield return null;

        Debug.LogFormat("{0}: Coroutine 1 Done!", name);
    }

    private IEnumerator SetCoroutine2()
    {
        yield return null;
        yield return null;

        Debug.LogFormat("{0}: Coroutine 2 Done!", name);
    }

    private IEnumerator SetCoroutine3()
    {
        yield return null;
        yield return null;
        yield return null;

        Debug.LogFormat("{0}: Coroutine 3 Done!", name);

        yield return m_Coroutine;

        Debug.LogFormat("{0}: All Coroutine End!", name);
    }

    private IEnumerator SetCoroutineFinal()
    {
        yield return null;
        yield return null;
        yield return null;
        yield return null;

        for (int i = 0; i < 10; i++)
        {
            Debug.LogFormat("{0}: Coroutine Final End in {1}!", name, 10 - i);

            yield return new WaitForSeconds(1f);
        }

        //Or Use this code instead... :)
        //yield return new WaitForSeconds(10f);

        Debug.LogFormat("{0}: Coroutine Final Done!", name);
    }

    private IEnumerator SetCoroutine4()
    {
        //Stop this coroutine!
        yield break;

        Debug.LogFormat("{0}: Coroutine would not run to here!", name);
    }
}
