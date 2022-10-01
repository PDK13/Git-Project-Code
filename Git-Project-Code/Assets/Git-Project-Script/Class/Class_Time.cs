using UnityEngine;

public class Class_Time
{
    public static void Set_Time_Scale(float f_Time_Scale)
    {
        Time.timeScale = f_Time_Scale;
    }

    public static int Get_Time_Compare(Class_Time_Value cs_Time_From, Class_Time_Value cs_Time_To)
    {
        if (cs_Time_From.Get_Time_Hour() > cs_Time_To.Get_Time_Hour())
        {
            return 1;
        }
        else
        if (cs_Time_From.Get_Time_Hour() < cs_Time_To.Get_Time_Hour())
        {
            return -1;
        }

        if (cs_Time_From.Get_Time_Minute() > cs_Time_To.Get_Time_Minute())
        {
            return 1;
        }
        else
        if (cs_Time_From.Get_Time_Minute() < cs_Time_To.Get_Time_Minute())
        {
            return -1;
        }

        if (cs_Time_From.Get_Time_Second() > cs_Time_To.Get_Time_Second())
        {
            return 1;
        }
        else
        if (cs_Time_From.Get_Time_Second() < cs_Time_To.Get_Time_Second())
        {
            return -1;
        }

        return 0;
    }
}

[System.Serializable]
public class Class_Time_Value
{
    [SerializeField] private int i_Hour = 0;

    [SerializeField] private int i_Minute = 0;

    [SerializeField] private int i_Second = 0;

    public Class_Time_Value()
    {
        i_Hour = 0;
        i_Minute = 0;
        i_Second = 0;
    }

    public Class_Time_Value(Class_Time_Value cs_Class_Time_Value)
    {
        i_Hour = cs_Class_Time_Value.Get_Time_Hour();
        i_Minute = cs_Class_Time_Value.Get_Time_Minute();
        i_Second = cs_Class_Time_Value.Get_Time_Second();
    }

    public bool Get_Time_Zero()
    {
        return Get_Time_Second() == 0 && Get_Time_Minute() == 0 && Get_Time_Hour() == 0;
    }

    #region Hour

    public void Set_Time_Hour(int i_Hour)
    {
        if (i_Hour < 0)
        {
            this.i_Hour = 0;
        }
        else
        {
            this.i_Hour = i_Hour;
        }
    }

    public void Set_Time_Hour_Chance(int i_Hour_Chance)
    {
        if (i_Hour + i_Hour_Chance < 0)
        {
            i_Hour = 0;
        }
        else
        {
            i_Hour += i_Hour_Chance;
        }
    }

    public int Get_Time_Hour()
    {
        return i_Hour;
    }

    #endregion

    #region Minute

    public void Set_Time_Minute(int i_Minute)
    {
        if (i_Minute < 0)
        {
            this.i_Minute = 0;
        }
        else
        {
            this.i_Minute = i_Minute;
        }
    }

    public void Set_Time_Minute_Chance(int i_Minute_Chance)
    {
        if (i_Minute + i_Minute_Chance < 0)
        {
            i_Minute = 0;
        }
        else
        {
            i_Minute += i_Minute_Chance;
        }
    }

    public int Get_Time_Minute()
    {
        return i_Minute;
    }

    #endregion

    #region Second

    public void Set_Time_Second(int i_Second)
    {
        if (i_Second < 0)
        {
            this.i_Second = 0;
        }
        else
        {
            this.i_Second = i_Second;
        }
    }

    public void Set_Time_Second_Chance(int i_Second_Chance)
    {
        if (i_Second + i_Second_Chance < 0)
        {
            i_Second = 0;
        }
        else
        {
            i_Second += i_Second_Chance;
        }
    }

    public int Get_Time_Second()
    {
        return i_Second;
    }

    #endregion
}