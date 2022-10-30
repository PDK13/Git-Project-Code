using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Simple_ButtonLock : MonoBehaviour
{
    [SerializeField] private Button m_BtnLockPressMe;

    [SerializeField] private Button m_BtnPressMe;

    private bool m_LockPressMe = false;

    private int m_PressMe = 0;

    private void Start()
    {
        string m_BtnLockPressMeStatus = (m_LockPressMe) ? "NOW UNLOCK" : "NOW LOCK";
        m_BtnLockPressMe.transform.Find("Text").GetComponent<Text>().text = m_BtnLockPressMeStatus;

        m_BtnPressMe.interactable = m_LockPressMe;

        m_BtnPressMe.transform.Find("Text").GetComponent<Text>().text = "PRESS ME!";
    }

    public void BtnLockPressMe()
    {
        m_LockPressMe = !m_LockPressMe;

        string m_BtnLockPressMeStatus = (m_LockPressMe) ? "NOW UNLOCK" : "NOW LOCK";
        m_BtnLockPressMe.transform.Find("Text").GetComponent<Text>().text = m_BtnLockPressMeStatus;

        m_BtnPressMe.interactable = m_LockPressMe;
    }

    public void BtnPressMe()
    {
        m_PressMe++;
        m_BtnPressMe.transform.Find("Text").GetComponent<Text>().text = m_PressMe.ToString();
    }
}
