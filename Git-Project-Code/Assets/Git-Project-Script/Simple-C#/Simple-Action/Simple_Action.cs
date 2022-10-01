using System;
using UnityEngine;

public class Simple_Action : MonoBehaviour
{
    private Action act_Action_01;
    private Action<string> act_Action_02;
    private Action<string, int> act_Action_03;

    private void Start()
    {
        act_Action_01 += Set_Action_01;
        act_Action_02 += Set_Action_02;
        act_Action_03 += Set_Action_03;
    }

    private void OnDestroy()
    {
        act_Action_01 -= Set_Action_01;
        act_Action_02 -= Set_Action_02;
        act_Action_03 -= Set_Action_03;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            act_Action_01?.Invoke();
        }
        else
        if (Input.GetKeyDown(KeyCode.X))
        {
            act_Action_02?.Invoke("Hello World!");
        }
        else
        if (Input.GetKeyDown(KeyCode.C))
        {
            act_Action_03?.Invoke("Good bye!", -1);
        }
    }

    private void Set_Action_01()
    {
        Debug.LogFormat("{0}: Action 01!", name);
    }

    private void Set_Action_02(string s_MyString)
    {
        Debug.LogFormat("{0}: Action 02: {1}!", name, s_MyString);
    }

    private void Set_Action_03(string s_MyString, int m_MyInt)
    {
        Debug.LogFormat("{0}: Action 02: {1} {2}!", name, s_MyString, m_MyInt);
    }
}
