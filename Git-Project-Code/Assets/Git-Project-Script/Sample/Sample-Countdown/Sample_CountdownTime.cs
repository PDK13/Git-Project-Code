using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sample_CountdownTime : MonoBehaviour
{
    [SerializeField] private Text com_Text;

    [SerializeField] private List<Sample_CountdownTime_Class> l_CountdownTime = new List<Sample_CountdownTime_Class>();

    private void Start()
    {
        l_CountdownTime.Add(new Sample_CountdownTime_Class(10, "Press Enter to Start from 10"));

        if (l_CountdownTime.Count > 0)
        {
            StartCoroutine(Set_Countdown(l_CountdownTime[0]));
        }
    }

    private IEnumerator Set_Countdown(Sample_CountdownTime_Class cs_CountdownTime)
    {
        yield return null;

        com_Text.text = cs_CountdownTime.Get_Message();

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));

        do
        {
            cs_CountdownTime.Set_TimeOut();

            com_Text.text = cs_CountdownTime.Get_TimeOut_Cur() + " / " + cs_CountdownTime.Get_Time();

            yield return new WaitForSecondsRealtime(Time.fixedDeltaTime);
        }
        while (!cs_CountdownTime.Get_TimeOut());

        l_CountdownTime.Remove(cs_CountdownTime);

        if (l_CountdownTime.Count > 0)
        {
            StartCoroutine(Set_Countdown(l_CountdownTime[0]));
        }
        else
        {
            com_Text.text = "We're done!";
        }
    }
}

[System.Serializable]
public class Sample_CountdownTime_Class
{
    [SerializeField] private float f_Time = 0;

    [SerializeField] private string s_Message = "";

    [SerializeField] private float f_Time_Cur;

    public Sample_CountdownTime_Class(float f_Time)
    {
        this.f_Time = f_Time;
        this.f_Time_Cur = f_Time;
    }

    public Sample_CountdownTime_Class(float f_Time, string s_Message)
    {
        this.f_Time = f_Time;
        this.f_Time_Cur = f_Time;

        this.s_Message = s_Message;
    }

    public void Set_TimeOut()
    {
        this.f_Time_Cur -= Time.fixedDeltaTime;
    }

    public float Get_Time()
    {
        return f_Time;
    }

    public float Get_TimeOut_Cur()
    {
        return f_Time_Cur;
    }

    public bool Get_TimeOut()
    {
        return f_Time_Cur <= 0;
    }

    public string Get_Message()
    {
        return s_Message;
    }
}