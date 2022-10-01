using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SimpleCountdownTime : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI com_Text;

    [SerializeField] private float f_Time_Passed = 1f;

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
            csCountdownTime.Set_TimeOut(f_Time_Passed);

            com_Text.text = csCountdownTime.GetTimeOut_Cur() + " / " + csCountdownTime.GetTime();

            yield return new WaitForSeconds(f_Time_Passed);
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
    [SerializeField] private float f_Time = 0;

    [SerializeField] private string s_Message = "";

    [SerializeField] private float f_Time_Cur;

    public SampleCountdownTime_Class(float f_Time)
    {
        this.f_Time = f_Time;
        f_Time_Cur = f_Time;
    }

    public SampleCountdownTime_Class(float f_Time, string s_Message)
    {
        this.f_Time = f_Time;
        f_Time_Cur = f_Time;

        this.s_Message = s_Message;
    }

    public void Set_TimeOut(float f_Time_Passed)
    {
        f_Time_Cur -= f_Time_Passed;
    }

    public float GetTime()
    {
        return f_Time;
    }

    public float GetTimeOut_Cur()
    {
        return f_Time_Cur;
    }

    public bool GetTimeOut()
    {
        return f_Time_Cur <= 0;
    }

    public string GetMessage()
    {
        return s_Message;
    }
}