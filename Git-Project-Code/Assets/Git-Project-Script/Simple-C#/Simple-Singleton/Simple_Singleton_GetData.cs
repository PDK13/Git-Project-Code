using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simple_Singleton_GetData : MonoBehaviour
{
    private void Start()
    {
        Debug.LogFormat("{0}: Get {1}", name, Simple_Singleton.Get_Instance().i_Number_Public);

        Debug.LogFormat("{0}: Get {1}", name, Simple_Singleton.Get_Number());

        Simple_Singleton.Set_Number(3);

        Debug.LogFormat("{0}: Get {1}", name, Simple_Singleton.Get_Number());
    }
}