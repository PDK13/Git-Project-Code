using UnityEngine;

public class ClassTime
{
    public static void SetTimeScale(float m_TimeScale)
    {
        Time.timeScale = m_TimeScale;
    }

    public static int GetTimeCompare(ClassTimeValue m_TimeFrom, ClassTimeValue m_TimeTo)
    {
        if (m_TimeFrom.GetTimeHour() > m_TimeTo.GetTimeHour())
        {
            return 1;
        }
        else
        if (m_TimeFrom.GetTimeHour() < m_TimeTo.GetTimeHour())
        {
            return -1;
        }

        if (m_TimeFrom.GetTimeMinute() > m_TimeTo.GetTimeMinute())
        {
            return 1;
        }
        else
        if (m_TimeFrom.GetTimeMinute() < m_TimeTo.GetTimeMinute())
        {
            return -1;
        }

        if (m_TimeFrom.GetTimeSecond() > m_TimeTo.GetTimeSecond())
        {
            return 1;
        }
        else
        if (m_TimeFrom.GetTimeSecond() < m_TimeTo.GetTimeSecond())
        {
            return -1;
        }

        return 0;
    }
}

[System.Serializable]
public class ClassTimeValue
{
    [SerializeField] private int m_Hour = 0;

    [SerializeField] private int m_Minute = 0;

    [SerializeField] private int m_Second = 0;

    public ClassTimeValue()
    {
        SetTimeHour(0);
        SetTimeMinute(0);
        SetTimeSecond(0);
    }

    public ClassTimeValue(int m_Hour, int m_Minute, int m_Second)
    {
        SetTimeHour(m_Hour);
        SetTimeMinute(m_Minute);
        SetTimeSecond(m_Second);
    }

    public ClassTimeValue(ClassTimeValue m_ClassTimeValue)
    {
        SetTimeHour(m_ClassTimeValue.GetTimeHour());
        SetTimeMinute(m_ClassTimeValue.GetTimeMinute());
        SetTimeSecond(m_ClassTimeValue.GetTimeSecond());
    }

    public bool GetTimeZero()
    {
        return GetTimeSecond() == 0 && GetTimeMinute() == 0 && GetTimeHour() == 0;
    }

    #region Hour

    public void SetTimeHour(int m_Hour)
    {
        if (m_Hour < 0)
        {
            this.m_Hour = 0;
        }
        else
        {
            this.m_Hour = m_Hour;
        }
    }

    public void SetTimeHourChance(int m_HourChance)
    {
        if (m_Hour + m_HourChance < 0)
        {
            this.m_Hour = 0;
        }
        else
        {
            this.m_Hour += m_HourChance;
        }
    }

    public int GetTimeHour()
    {
        return m_Hour;
    }

    #endregion

    #region Minute

    public void SetTimeMinute(int m_Minute)
    {
        if (m_Minute < 0)
        {
            m_Minute = 0;
        }
        else
        {
            this.m_Minute = m_Minute;
        }
    }

    public void SetTimeMinuteChance(int m_MinuteChance)
    {
        if (m_Minute + m_MinuteChance < 0)
        {
            m_Minute = 0;
        }
        else
        {
            m_Minute += m_MinuteChance;
        }
    }

    public int GetTimeMinute()
    {
        return m_Minute;
    }

    #endregion

    #region Second

    public void SetTimeSecond(int m_Second)
    {
        if (m_Second < 0)
        {
            this.m_Second = 0;
        }
        else
        {
            this.m_Second = m_Second;
        }
    }

    public void SetTimeSecondChance(int m_SecondChance)
    {
        if (m_Second + m_SecondChance < 0)
        {
            m_Second = 0;
        }
        else
        {
            m_Second += m_SecondChance;
        }
    }

    public int GetTimeSecond()
    {
        return m_Second;
    }

    #endregion
}