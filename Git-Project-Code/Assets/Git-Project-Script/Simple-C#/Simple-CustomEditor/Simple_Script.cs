using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simple_Script : MonoBehaviour
{
    public Option m_VaribleA = Option.Option1;

    [SerializeField] private string m_VaribleB;
    [SerializeField] private string m_VaribleC;

    [SerializeField] private Vector3[] m_VariblePath;

    public enum Option
    {
        Option1 = 1,
        Option2 = 2,
        Option3 = 3,
    }

    public void SetOptionDebug()
    {
        Debug.LogFormat("{0}: Option {1} was sellected!", this.name, m_VaribleA);
    }

    public Vector3[] GetPath()
    {
        return m_VariblePath;
    }

    public void SetPath(Vector3 value, int index)
    {
        m_VariblePath[index] = value;
    }
}