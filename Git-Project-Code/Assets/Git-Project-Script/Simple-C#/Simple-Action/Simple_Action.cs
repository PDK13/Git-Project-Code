using System;
using UnityEngine;

public class Simple_Action : MonoBehaviour
{
    private Action act_Action_01;
    private Action<string> act_Action_02;
    private Action<string, int> act_Action_03;

    private void Start()
    {
        act_Action_01 += SetAction_01;
        act_Action_02 += SetAction_02;
        act_Action_03 += SetAction_03;
    }

    private void OnDestroy()
    {
        act_Action_01 -= SetAction_01;
        act_Action_02 -= SetAction_02;
        act_Action_03 -= SetAction_03;
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

    private void SetAction_01()
    {
        Debug.LogFormat("{0}: Action 01!", name);
    }

    private void SetAction_02(string m_MyString)
    {
        Debug.LogFormat("{0}: Action 02: {1}!", name, m_MyString);
    }

    private void SetAction_03(string m_MyString, int m_MyInt)
    {
        Debug.LogFormat("{0}: Action 02: {1} {2}!", name, m_MyString, m_MyInt);
    }
}
