using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SimpleCountdownTime : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_Text;

    [SerializeField] private float m_TimePassed = 1f;

    [SerializeField] private List<SampleCountdownTimeClass> lCountdownTime = new List<SampleCountdownTimeClass>();

    private void Start()
    {
        lCountdownTime.Add(new SampleCountdownTimeClass(10, "Press Enter to Start from 10"));

        if (lCountdownTime.Count > 0)
        {
            StartCoroutine(SetCountdown(lCountdownTime[0]));
        }
    }

    private IEnumerator SetCountdown(SampleCountdownTimeClass csCountdownTime)
    {
        yield return null;

        m_Text.text = csCountdownTime.GetMessage();

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        do
        {
            csCountdownTime.SetTimeOut(m_TimePassed);

            m_Text.text = csCountdownTime.GetTimeOutCurrent() + " / " + csCountdownTime.GetTime();

            yield return new WaitForSeconds(m_TimePassed);
        }
        while (!csCountdownTime.GetTimeOut());

        lCountdownTime.Remove(csCountdownTime);

        if (lCountdownTime.Count > 0)
        {
            StartCoroutine(SetCountdown(lCountdownTime[0]));
        }
        else
        {
            m_Text.text = "We're done!";
        }
    }
}

[System.Serializable]
public class SampleCountdownTimeClass
{
    [SerializeField] private float m_Time = 0;

    [SerializeField] private string m_Message = "";

    [SerializeField] private float m_TimeCurrent;

    public SampleCountdownTimeClass(float m_Time)
    {
        this.m_Time = m_Time;
        m_TimeCurrent = m_Time;
    }

    public SampleCountdownTimeClass(float m_Time, string m_Message)
    {
        this.m_Time = m_Time;
        m_TimeCurrent = m_Time;

        this.m_Message = m_Message;
    }

    public void SetTimeOut(float m_TimePassed)
    {
        m_TimeCurrent -= m_TimePassed;
    }

    public float GetTime()
    {
        return m_Time;
    }

    public float GetTimeOutCurrent()
    {
        return m_TimeCurrent;
    }

    public bool GetTimeOut()
    {
        return m_TimeCurrent <= 0;
    }

    public string GetMessage()
    {
        return m_Message;
    }
}