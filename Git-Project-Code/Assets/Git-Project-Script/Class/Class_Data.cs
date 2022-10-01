using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Working on Data and List
/// </summary>
public class Clasm_Data
{
    public bool m_Debug = false;

    /// <summary>
    /// Working on Data & List
    /// </summary>
    public Clasm_Data()
    {

    }

    /// <summary>
    /// Working on Data & List
    /// </summary>
    public Clasm_Data(bool m_Debug)
    {
        this.m_Debug = m_Debug;
    }

    #region Convert

    /// <summary>
    /// Convert OBJECT to INT
    /// </summary>
    /// <param name="m_Value"></param>
    /// <returns>If Convert fail, return 0</returns>
    public int GetConvert_Int(object m_Value)
    {
        string m_ValueCheck = m_Value.ToString();

        if (m_ValueCheck == GetString_Data_NotFound() || m_Value == null || m_ValueCheck == "")
        {
            if (m_Debug)
            {
                Debug.LogError("GetExchance_Int: \"m_Value\" To (INT)\"0\"");
            }

            return 0;
        }
        return int.Parse(m_ValueCheck);
    }

    /// <summary>
    /// Convert OBJECT to FLOAT
    /// </summary>
    /// <param name="m_Value"></param>
    /// <returns>If Convert fail, return 0.0</returns>
    public float GetConvert_Float(object m_Value)
    {
        string m_ValueCheck = m_Value.ToString();

        if (m_ValueCheck == GetString_Data_NotFound() || m_Value == null || m_ValueCheck == "")
        {
            if (m_Debug)
            {
                Debug.LogError("GetExchance_Float: \"m_Value\" To (FLOAT)\"0.0\"");
            }

            return 0.0f;
        }
        return float.Parse(m_ValueCheck);
    }

    /// <summary>
    /// Convert OBJECT to BOOL
    /// </summary>
    /// <param name="m_Value"></param>
    /// <returns>If Convert fail, return FALSE</returns>
    public bool GetConvert_Bool(object m_Value)
    {
        string m_ValueCheck = m_Value.ToString();

        if (m_ValueCheck == GetString_Data_NotFound() || m_Value == null || m_ValueCheck == "")
        {
            if (m_Debug)
            {
                Debug.LogError("GetExchance_Bool: \"m_Value\" To (BOOL)\"FALSE\"");
            }

            return false;
        }
        return bool.Parse(m_ValueCheck);
    }

    /// <summary>
    /// Convert OBJECT to STRING
    /// </summary>
    /// <param name="m_Value"></param>
    /// <returns>If Convert fail, return NULL</returns>
    public string GetConvert_String(object m_Value)
    {
        string m_ValueCheck = m_Value.ToString();
        return m_ValueCheck.ToString();
    }

    #endregion

    #region Data

    /// <summary>
    /// Data Name to Access
    /// </summary>
    private readonly List<string> l_DataName = new List<string>();

    /// <summary>
    /// Data Value (INT, FLOAT, BOOL, STRING) saved at STRING
    /// </summary>
    private readonly List<object> l_Data_Value = new List<object>();

    /// <summary>
    /// Get List of Data Name
    /// </summary>
    /// <returns></returns>
    public List<string> GetListName()
    {
        return l_DataName;
    }

    /// <summary>
    /// Get List of Data Value
    /// </summary>
    /// <returns></returns>
    public List<object> GetList_Value()
    {
        return l_Data_Value;
    }

    private int m_Index_Auto = -1;

    /// <summary>
    /// Set Auto Index to "-1" before start use "SetIndex_Plus()"
    /// </summary>
    public void SetIndex_Restart()
    {
        m_Index_Auto = -1;
    }

    /// <summary>
    /// Set Auto Index "+1" each time called
    /// </summary>
    public void SetIndex_Plus()
    {
        m_Index_Auto++;
    }

    /// <summary>
    /// Get Auto Index
    /// </summary>
    /// <returns></returns>
    public int GetIndex()
    {
        return m_Index_Auto;
    }

    #endregion

    #region List (Single Value)

    /// <summary>
    /// Get Index of Exist Data Name
    /// </summary>
    /// <param name="m_DataName"></param>
    /// <returns></returns>
    public int GetIndex_DataIsExist(string m_DataName)
    {
        for (int i = 0; i < l_DataName.Count; i++)
        {
            if (l_DataName[i] == m_DataName)
            {
                return i;
            }
        }
        return -1;
    }

    /// <summary>
    /// Set Data Single
    /// </summary>
    /// <param name="m_DataName"></param>
    /// <param name="o_DataValue"></param>
    public void SetData(string m_DataName, object o_DataValue)
    {
        int Index = GetIndex_DataIsExist(m_DataName);

        if (Index != -1)
        {
            if (m_Debug)
            {
                Debug.Log("SetData: " + "\"" + m_DataName + "\"" + " Updated " + "\"" + o_DataValue + "\"");
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
                Debug.LogError("SetData: " + m_DataName + " not support Updated TYPE");
            }
        }
        else
        {
            if (m_Debug)
            {
                Debug.Log("SetData: " + "\"" + m_DataName + "\"" + " Added " + "\"" + o_DataValue + "\"");
            }

            if (o_DataValue == null)
            {
                l_DataName.Add(m_DataName);
                l_Data_Value.Add(GetString_Data_NULL());
            }
            else
            if (o_DataValue.GetType() == typeof(int))
            {
                l_DataName.Add(m_DataName);
                l_Data_Value.Add(o_DataValue.ToString());
            }
            else
            if (o_DataValue.GetType() == typeof(float))
            {
                l_DataName.Add(m_DataName);
                l_Data_Value.Add(o_DataValue.ToString());
            }
            else
            if (o_DataValue.GetType() == typeof(bool))
            {
                l_DataName.Add(m_DataName);
                l_Data_Value.Add(o_DataValue.ToString());
            }
            else
            if (o_DataValue.GetType() == typeof(string))
            {
                l_DataName.Add(m_DataName);
                l_Data_Value.Add(o_DataValue.ToString());
            }
            else
            if (m_Debug)
            {
                Debug.LogError("SetData: " + m_DataName + " not support Added TYPE");
            }
        }
    }

    /// <summary>
    /// Get Data Single
    /// </summary>
    /// <param name="m_DataName"></param>
    /// <returns>If not found Data, return "@NotFound"</returns>
    public object GetData(string m_DataName)
    {
        for (int i = 0; i < l_DataName.Count; i++)
        {
            if (l_DataName[i] == m_DataName)
            {
                return l_Data_Value[i];
            }
        }
        if (m_Debug)
        {
            Debug.LogError("GetData: " + "\"" + m_DataName + "\" Not found");
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
    /// <param name="m_DataName"></param>
    /// <param name="m_Index"></param>
    /// <param name="o_DataValue"></param>
    public void SetData(string m_DataName, int m_Index, object o_DataValue)
    {
        string m_DataCheck = m_DataName + "_" + m_Index.ToString();
        SetData(m_DataCheck, o_DataValue);
    }

    /// <summary>
    /// Set Data Muti Count
    /// </summary>
    /// <param name="m_DataName"></param>
    /// <param name="iCount"></param>
    public void SetDataCount(string m_DataName, int iCount)
    {
        string m_DataCheck = m_DataName + "Count";
        SetData(m_DataCheck, iCount);
    }

    /// <summary>
    /// Get Data Muti
    /// </summary>
    /// <param name="m_DataName"></param>
    /// <param name="m_Index"></param>
    /// <returns></returns>
    public object GetObject_Data(string m_DataName, int m_Index)
    {
        string m_DataCheck = m_DataName + "_" + m_Index.ToString();
        return GetData(m_DataCheck);
    }

    /// <summary>
    /// Get Data Muti Count
    /// </summary>
    /// <param name="m_DataName"></param>
    /// <returns></returns>
    public int GetInt_DataCount(string m_DataName)
    {
        string m_DataCheck = m_DataName + "Count";
        if (GetConvert_String(GetData(m_DataCheck)) == GetString_Data_NotFound())
        {
            return -1;
        }

        return GetConvert_Int(GetData(m_DataCheck).ToString());
    }

    /// <summary>
    /// Get own Index Name Data
    /// </summary>
    /// <param name="m_DataName"></param>
    /// <param name="m_Index"></param>
    /// <returns></returns>
    public string GetString_GetConvertNameIndex(string m_DataName, int m_Index)
    {
        return m_DataName + "_" + m_Index.ToString();
    }

    /// <summary>
    /// Get own Count Name Data
    /// </summary>
    /// <param name="m_DataName"></param>
    /// <param name="iCount"></param>
    /// <returns></returns>
    public string GetString_GetConvertNameCount(string m_DataName)
    {
        return m_DataName + "Count";
    }

    #endregion
}
