using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SimpleCountdownTime : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI com_Text;

    [SerializeField] private float m_Time_Passed = 1f;

    [SerializeField] private List<SampleCountdownTime_Class> lCountdownTime = new List<SampleCountdownTime_Class>();

    private void Start()
    {
        lCountdownTime.Add(new SampleCountdownTime_Class(10, "Press Enter to Start from 10"));

        if (lCountdownTime.Count > 0)
        {
            StartCoroutine(SetCountdown(lCountdownTime[0]));
        }
    }

    private IEnumerator SetCountdown(SampleCountdownTime_Class csCountdownTime)
    {
        yield return null;

        com_Text.text = csCountdownTime.GetMessage();

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        do
        {
            csCountdownTime.SetTimeOut(m_Time_Passed);

            com_Text.text = csCountdownTime.GetTimeOut_Cur() + " / " + csCountdownTime.GetTime();

            yield return new WaitForSeconds(m_Time_Passed);
        }
        while (!csCountdownTime.GetTimeOut());

        lCountdownTime.Remove(csCountdownTime);

        if (lCountdownTime.Count > 0)
        {
            StartCoroutine(SetCountdown(lCountdownTime[0]));
        }
        else
        {
            com_Text.text = "We're done!";
        }
    }
}

[System.Serializable]
public class SampleCountdownTime_Class
{
    [SerializeField] private float m_Time = 0;

    [SerializeField] private string m_Message = "";

    [SerializeField] private float m_Time_Cur;

    public SampleCountdownTime_Class(float m_Time)
    {
        this.m_Time = m_Time;
        m_Time_Cur = m_Time;
    }

    public SampleCountdownTime_Class(float m_Time, string m_Message)
    {
        this.m_Time = m_Time;
        m_Time_Cur = m_Time;

        this.m_Message = m_Message;
    }

    public void SetTimeOut(float m_Time_Passed)
    {
        m_Time_Cur -= m_Time_Passed;
    }

    public float GetTime()
    {
        return m_Time;
    }

    public float GetTimeOut_Cur()
    {
        return m_Time_Cur;
    }

    public bool GetTimeOut()
    {
        return m_Time_Cur <= 0;
    }

    public string GetMessage()
    {
        return m_Message;
    }
}