using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Class_Time
{
    public static void Set_Time_Scale(float f_Time_Scale)
    {
        Time.timeScale = f_Time_Scale;
    }
}

[System.Serializable]
public class Class_Time_Value
{
    [SerializeField] private int i_Second = 0;

    [SerializeField] private int i_Minute = 0;

    [SerializeField] private int i_Hour = 0;

    public void Set_Time_Second(int i_Second)
    {
        this.i_Second = i_Second;
    }

    public int Get_Time_Second()
    {
        return i_Second;
    }

    public void Set_Time_Minute(int i_Minute)
    {
        this.i_Minute = i_Minute;
    }

    public int Get_Time_Minute()
    {
        return i_Minute;
    }

    public void Set_Time_Hour(int i_Hour)
    {
        this.i_Hour = i_Hour;
    }

    public int Get_Time_Hour()
    {
        return i_Hour;
    }
}