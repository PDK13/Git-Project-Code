using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Working on Data and List
/// </summary>
public class Class_Data
{
    public bool m_Debug = false;

    /// <summary>
    /// Working on Data & List
    /// </summary>
    public Class_Data()
    {

    }

    /// <summary>
    /// Working on Data & List
    /// </summary>
    public Class_Data(bool m_Debug)
    {
        this.m_Debug = m_Debug;
    }

    #region Convert

    /// <summary>
    /// Convert OBJECT to INT
    /// </summary>
    /// <param name="s_Value"></param>
    /// <returns>If Convert fail, return 0</returns>
    public int GetConvert_Int(object s_Value)
    {
        string s_ValueCheck = s_Value.ToString();

        if (s_ValueCheck == GetString_Data_NotFound() || s_Value == null || s_ValueCheck == "")
        {
            if (m_Debug)
            {
                Debug.LogError("GetExchance_Int: \"s_Value\" To (INT)\"0\"");
            }

            return 0;
        }
        return int.Parse(s_ValueCheck);
    }

    /// <summary>
    /// Convert OBJECT to FLOAT
    /// </summary>
    /// <param name="s_Value"></param>
    /// <returns>If Convert fail, return 0.0</returns>
    public float GetConvert_Float(object s_Value)
    {
        string s_ValueCheck = s_Value.ToString();

        if (s_ValueCheck == GetString_Data_NotFound() || s_Value == null || s_ValueCheck == "")
        {
            if (m_Debug)
            {
                Debug.LogError("GetExchance_Float: \"s_Value\" To (FLOAT)\"0.0\"");
            }

            return 0.0f;
        }
        return float.Parse(s_ValueCheck);
    }

    /// <summary>
    /// Convert OBJECT to BOOL
    /// </summary>
    /// <param name="s_Value"></param>
    /// <returns>If Convert fail, return FALSE</returns>
    public bool GetConvert_Bool(object s_Value)
    {
        string s_ValueCheck = s_Value.ToString();

        if (s_ValueCheck == GetString_Data_NotFound() || s_Value == null || s_ValueCheck == "")
        {
            if (m_Debug)
            {
                Debug.LogError("GetExchance_Bool: \"s_Value\" To (BOOL)\"FALSE\"");
            }

            return false;
        }
        return bool.Parse(s_ValueCheck);
    }

    /// <summary>
    /// Convert OBJECT to STRING
    /// </summary>
    /// <param name="s_Value"></param>
    /// <returns>If Convert fail, return NULL</returns>
    public string GetConvert_String(object s_Value)
    {
        string s_ValueCheck = s_Value.ToString();
        return s_ValueCheck.ToString();
    }

    #endregion

    #region Data

    /// <summary>
    /// Data Name to Access
    /// </summary>
    private readonly List<string> l_Data_Name = new List<string>();

    /// <summary>
    /// Data Value (INT, FLOAT, BOOL, STRING) saved at STRING
    /// </summary>
    private readonly List<object> l_Data_Value = new List<object>();

    /// <summary>
    /// Get List of Data Name
    /// </summary>
    /// <returns></returns>
    public List<string> GetList_Name()
    {
        return l_Data_Name;
    }

    /// <summary>
    /// Get List of Data Value
    /// </summary>
    /// <returns></returns>
    public List<object> GetList_Value()
    {
        return l_Data_Value;
    }

    private int i_Index_Auto = -1;

    /// <summary>
    /// Set Auto Index to "-1" before start use "Set_Index_Plus()"
    /// </summary>
    public void Set_Index_Restart()
    {
        i_Index_Auto = -1;
    }

    /// <summary>
    /// Set Auto Index "+1" each time called
    /// </summary>
    public void Set_Index_Plus()
    {
        i_Index_Auto++;
    }

    /// <summary>
    /// Get Auto Index
    /// </summary>
    /// <returns></returns>
    public int GetIndex()
    {
        return i_Index_Auto;
    }

    #endregion

    #region List (Single Value)

    /// <summary>
    /// Get Index of Exist Data Name
    /// </summary>
    /// <param name="s_DataName"></param>
    /// <returns></returns>
    public int GetIndex_DataIsExist(string s_DataName)
    {
        for (int i = 0; i < l_Data_Name.Count; i++)
        {
            if (l_Data_Name[i] == s_DataName)
            {
                return i;
            }
        }
        return -1;
    }

    /// <summary>
    /// Set Data Single
    /// </summary>
    /// <param name="s_DataName"></param>
    /// <param name="o_DataValue"></param>
    public void Set_Data(string s_DataName, object o_DataValue)
    {
        int Index = GetIndex_DataIsExist(s_DataName);

        if (Index != -1)
        {
            if (m_Debug)
            {
                Debug.Log("Set_Data: " + "\"" + s_DataName + "\"" + " Updated " + "\"" + o_DataValue + "\"");
            }

            if (o_DataValue == null)
            {
                l_Data_Value[Index] = GetString_Data_NULL();
            }
            else
            if (o_DataValue.GetType() == typeof(int))
            {
                l_Data_Value[Index] = o_DataValue.ToString();
            }
            else
            if (o_DataValue.GetType() == typeof(float))
            {
                l_Data_Value[Index] = o_DataValue.ToString();
            }
            else
            if (o_DataValue.GetType() == typeof(bool))
            {
                l_Data_Value[Index] = o_DataValue.ToString();
            }
            else
            if (o_DataValue.GetType() == typeof(string))
            {
                l_Data_Value[Index] = o_DataValue.ToString();
            }
            else
            if (m_Debug)
            {
                Debug.LogError("Set_Data: " + s_DataName + " not support Updated TYPE");
            }
        }
        else
        {
            if (m_Debug)
            {
                Debug.Log("Set_Data: " + "\"" + s_DataName + "\"" + " Added " + "\"" + o_DataValue + "\"");
            }

            if (o_DataValue == null)
            {
                l_Data_Name.Add(s_DataName);
                l_Data_Value.Add(GetString_Data_NULL());
            }
            else
            if (o_DataValue.GetType() == typeof(int))
            {
                l_Data_Name.Add(s_DataName);
                l_Data_Value.Add(o_DataValue.ToString());
            }
            else
            if (o_DataValue.GetType() == typeof(float))
            {
                l_Data_Name.Add(s_DataName);
                l_Data_Value.Add(o_DataValue.ToString());
            }
            else
            if (o_DataValue.GetType() == typeof(bool))
            {
                l_Data_Name.Add(s_DataName);
                l_Data_Value.Add(o_DataValue.ToString());
            }
            else
            if (o_DataValue.GetType() == typeof(string))
            {
                l_Data_Name.Add(s_DataName);
                l_Data_Value.Add(o_DataValue.ToString());
            }
            else
            if (m_Debug)
            {
                Debug.LogError("Set_Data: " + s_DataName + " not support Added TYPE");
            }
        }
    }

    /// <summary>
    /// Get Data Single
    /// </summary>
    /// <param name="s_DataName"></param>
    /// <returns>If not found Data, return "@NotFound"</returns>
    public object GetData(string s_DataName)
    {
        for (int i = 0; i < l_Data_Name.Count; i++)
        {
            if (l_Data_Name[i] == s_DataName)
            {
                return l_Data_Value[i];
            }
        }
        if (m_Debug)
        {
            Debug.LogError("GetData: " + "\"" + s_DataName + "\" Not found");
        }

        return "@NotFound";
    }

    /// <summary>
    /// Use to Get "@NotFound" value for "GetData_String()"
    /// </summary>
    /// <returns></returns>
    public string GetString_Data_NotFound()
    {
        return "@NotFound";
    }

    /// <summary>
    /// Use to Get "@Null" value for "GetData_String()"
    /// </summary>
    /// <returns></returns>
    public string GetString_Data_NULL()
    {
        return "@Null";
    }

    #endregion

    #region List (Muti Value)

    /// <summary>
    /// Set Data Muti
    /// </summary>
    /// <param name="s_DataName"></param>
    /// <param name="i_Index"></param>
    /// <param name="o_DataValue"></param>
    public void Set_Data(string s_DataName, int i_Index, object o_DataValue)
    {
        string s_DataCheck = s_DataName + "_" + i_Index.ToString();
        Set_Data(s_DataCheck, o_DataValue);
    }

    /// <summary>
    /// Set Data Muti Count
    /// </summary>
    /// <param name="s_DataName"></param>
    /// <param name="iCount"></param>
    public void Set_DataCount(string s_DataName, int iCount)
    {
        string s_DataCheck = s_DataName + "Count";
        Set_Data(s_DataCheck, iCount);
    }

    /// <summary>
    /// Get Data Muti
    /// </summary>
    /// <param name="s_DataName"></param>
    /// <param name="i_Index"></param>
    /// <returns></returns>
    public object GetObject_Data(string s_DataName, int i_Index)
    {
        string s_DataCheck = s_DataName + "_" + i_Index.ToString();
        return GetData(s_DataCheck);
    }

    /// <summary>
    /// Get Data Muti Count
    /// </summary>
    /// <param name="s_DataName"></param>
    /// <returns></returns>
    public int GetInt_DataCount(string s_DataName)
    {
        string s_DataCheck = s_DataName + "Count";
        if (GetConvert_String(GetData(s_DataCheck)) == GetString_Data_NotFound())
        {
            return -1;
        }

        return GetConvert_Int(GetData(s_DataCheck).ToString());
    }

    /// <summary>
    /// Get own Index Name Data
    /// </summary>
    /// <param name="s_DataName"></param>
    /// <param name="i_Index"></param>
    /// <returns></returns>
    public string GetString_GetConvert_NameIndex(string s_DataName, int i_Index)
    {
        return s_DataName + "_" + i_Index.ToString();
    }

    /// <summary>
    /// Get own Count Name Data
    /// </summary>
    /// <param name="s_DataName"></param>
    /// <param name="iCount"></param>
    /// <returns></returns>
    public string GetString_GetConvert_NameCount(string s_DataName)
    {
        return s_DataName + "Count";
    }

    #endregion
}
