using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simple_ScriptableObject_GetData : MonoBehaviour
{
    [SerializeField] private Simple_ScriptableObject cs_Simple_ScriptableObject;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        Debug.LogFormat("{0}: Data: {1}", name, cs_Simple_ScriptableObject.Get_MyString());

        cs_Simple_ScriptableObject.Set_MyString("Good bye!");

        Debug.LogFormat("{0}: Data after chance: {1}", name, cs_Simple_ScriptableObject.Get_MyString());
    }
}
