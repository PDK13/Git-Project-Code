using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simple_AfterSceneLoad : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void SetOnStart()
    {
        Debug.Log("This is after Scene Load!");
    }
}
