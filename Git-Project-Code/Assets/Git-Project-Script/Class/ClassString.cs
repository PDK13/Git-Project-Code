using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// Working on String
/// </summary>
public class ClassString
{
    #region String Command

    #region String Count

    public static int GetStringArrayCount(string[] m_StringArray)
    {
        return m_StringArray.Length;
    }

    public static int GetStringListCount(List<string> m_StringList)
    {
        return m_StringList.Count;
    }

    #endregion

    #region String Exist

    public static bool GetStringIsExist(string m_FatherString, string m_ChildString)
    {
        return m_FatherString.Contains(m_ChildString);
    }

    #endregion

    #region String Replace

    public static string GetStringReplace(string m_FatherString, string m_CheckString, string m_ReplaceString)
    {
        return m_FatherString.Replace(m_CheckString, m_ReplaceString);
    }

    public static string GetStringReplaceClone(string m_CloneName)
    {
        return GetStringReplace(m_CloneName, "(Clone)", "");
    }

    public static string GetStringReplace_Resources(string m_Resourcem_Path)
    {
        return GetStringReplace(m_Resourcem_Path, "Assets/resources/", "");
    }

    #endregion

    #region String Split

    public static string[] GetString_Split_Array(string m_FatherString, char m_Key)
    {
        return m_FatherString.Split(m_Key);
    }

    public static List<string> GetString_Split_List(string m_FatherString, char m_Key)
    {
        string[] m_SplitArray = GetString_Split_Array(m_FatherString, m_Key);

        List<string> m_SplitString = new List<string>();

        m_SplitString.AddRange(m_SplitArray);

        return m_SplitString;
    }

    #endregion

    #endregion

    #region String Data

    #region String Data Main

    #region String Data Main Encypt

    public static string GetDataEncypt(List<string> m_DataList, char m_Key)
    {
        string m_Data = "";

        for (int i = 0; i < m_DataList.Count; i++)
        {
            //m_Data += (m_DataList[i]);

            //if (i < m_DataList.Count - 1)
            //{
            //    m_Data += m_Key;
            //}

            m_Data = GetDataEncyptAdd(m_Data, m_DataList[i], m_Key);
        }

        return m_Data;
    }

    public static string GetDataEncypt(List<int> m_DataList, char m_Key)
    {
        string m_Data = "";

        for (int i = 0; i < m_DataList.Count; i++)
        {
            //m_Data += (m_DataList[i].ToString());

            //if (i < m_DataList.Count - 1)
            //{
            //    m_Data += m_Key;
            //}

            m_Data = GetDataEncyptAdd(m_Data, m_DataList[i], m_Key);
        }

        return m_Data;
    }

    public static string GetDataEncypt(List<float> m_DataList, char m_Key)
    {
        string m_Data = "";

        for (int i = 0; i < m_DataList.Count; i++)
        {
            //m_Data += (m_DataList[i].ToString());

            //if (i < m_DataList.Count - 1)
            //{
            //    m_Data += m_Key;
            //}

            m_Data = GetDataEncyptAdd(m_Data, m_DataList[i], m_Key);
        }

        return m_Data;
    }

    public static string GetDataEncypt(List<bool> m_DataList, char m_Key)
    {
        string m_Data = "";

        for (int i = 0; i < m_DataList.Count; i++)
        {
            //m_Data += ((m_DataList[i]) ? "1" : "0");

            //if (i < m_DataList.Count - 1)
            //{
            //    m_Data += m_Key;
            //}

            m_Data = GetDataEncyptAdd(m_Data, m_DataList[i], m_Key);
        }

        return m_Data;
    }

    #endregion

    #region String Data Main Add Encypt

    public static string GetDataEncyptAdd(string m_Data, string m_DataAdd, char m_Key)
    {
        return m_Data + ((m_Data.Length != 0) ? m_Key.ToString() : "") + m_DataAdd;
    }

    public static string GetDataEncyptAdd(string m_Data, int m_DataAdd, char m_Key)
    {
        return m_Data + ((m_Data.Length != 0) ? m_Key.ToString() : "") + m_DataAdd.ToString();
    }

    public static string GetDataEncyptAdd(string m_Data, float m_DataAdd, char m_Key)
    {
        return m_Data + ((m_Data.Length != 0) ? m_Key.ToString() : "") + m_DataAdd.ToString();
    }

    public static string GetDataEncyptAdd(string m_Data, bool m_DataAdd, char m_Key)
    {
        return m_Data + ((m_Data.Length != 0) ? m_Key.ToString() : "") + ((m_DataAdd) ? "1" : "0");
    }

    #endregion

    #region String Data Main Dencypt

    public static List<string> GetDataDencyptString(string m_Data, char m_Key)
    {
        if (m_Data.Equals(""))
        {
            Debug.LogWarning("GetDataDencypt_String: Emty Data!");

            return new List<string>();
        }

        return GetString_Split_List(m_Data, m_Key);
    }

    public static List<int> GetDataDencyptInt(string m_Data, char m_Key)
    {
        if (m_Data.Equals(""))
        {
            Debug.LogWarning("GetDataDencypt_Int: Emty Data!");

            return new List<int>();
        }

        List<string> l_DataListString = GetDataDencyptString(m_Data, m_Key);

        List<int> m_DataListInt = new List<int>();

        for (int i = 0; i < l_DataListString.Count; i++)
        {
            m_DataListInt.Add(int.Parse(l_DataListString[i]));
        }

        return m_DataListInt;
    }

    public static List<float> GetDataDencyptFloat(string m_Data, char m_Key)
    {
        if (m_Data.Equals(""))
        {
            Debug.LogWarning("GetDataDencypt_Float: Emty Data!");

            return new List<float>();
        }

        List<string> l_DataListString = GetString_Split_List(m_Data, m_Key);

        List<float> m_DataListFloat = new List<float>();

        for (int i = 0; i < l_DataListString.Count; i++)
        {
            m_DataListFloat.Add(float.Parse(l_DataListString[i]));
        }

        return m_DataListFloat;
    }

    public static List<bool> GetDataDencyptBool(string m_Data, char m_Key)
    {
        if (m_Data.Equals(""))
        {
            Debug.LogWarning("GetDataDencypt_Bool: Emty Data!");

            return new List<bool>();
        }

        List<string> m_DataListString = GetString_Split_List(m_Data, m_Key);

        List<bool> m_DataListBool = new List<bool>();

        for (int i = 0; i < m_DataListString.Count; i++)
        {
            string m_Bool = m_DataListString[i];

            if (m_Bool == "1")
            {
                m_DataListBool.Add(true);
            }
            else
            if (m_Bool == "0")
            {
                m_DataListBool.Add(false);
            }
        }

        return m_DataListBool;
    }

    #endregion

    #endregion

    #region String Data Vector

    #region String Data Vector Encypt

    public static string GetDataVector2Encypt(Vector2 v2VectorData, char m_Key)
    {
        return v2VectorData.x.ToString() + m_Key + v2VectorData.y.ToString();
    }

    public static string GetDataVector3Encypt(Vector3 v3VectorData, char m_Key)
    {
        return v3VectorData.x.ToString() + m_Key + v3VectorData.y.ToString() + m_Key + v3VectorData.z.ToString();
    }

    public static string GetDataVector2IntEncypt(Vector2Int v2VectorData, char m_Key)
    {
        return v2VectorData.x.ToString() + m_Key + v2VectorData.y.ToString();
    }

    public static string GetDataVector3IntEncypt(Vector3Int v3VectorData, char m_Key)
    {
        return v3VectorData.x.ToString() + m_Key + v3VectorData.y.ToString() + m_Key + v3VectorData.z.ToString();
    }

    #endregion

    #region String Data Vector Dencypt

    public static Vector2 GetDataVector2Dencypt(string m_VectorData, char m_Key)
    {
        List<float> m_Data = GetDataDencyptFloat(m_VectorData, m_Key);

        return new Vector2(m_Data[0], m_Data[1]);
    }

    public static Vector3 GetDataVector3Dencypt(string m_VectorData, char m_Key)
    {
        List<float> m_Data = GetDataDencyptFloat(m_VectorData, m_Key);

        return new Vector3(m_Data[0], m_Data[1], m_Data[2]);
    }

    public static Vector2Int GetDataVector2IntDencypt(string m_VectorData, char m_Key)
    {
        List<int> m_Data = GetDataDencyptInt(m_VectorData, m_Key);

        return new Vector2Int(m_Data[0], m_Data[1]);
    }

    public static Vector3Int GetDataVector3IntDencypt(string m_VectorData, char m_Key)
    {
        List<int> m_Data = GetDataDencyptInt(m_VectorData, m_Key);

        return new Vector3Int(m_Data[0], m_Data[1], m_Data[2]);
    }

    #endregion

    #endregion

    #endregion

    #region Email Check (Local-Part@Domain-Part)

    /// <summary>
    /// Check if EMAIL NOT INVAILID (NOT FALSE)
    /// </summary>
    /// <param name="m_EmailCheck"></param>
    /// <returns>If NOT INVAILID, get TRUE</returns>
    private static bool GetCheckEmaimNotInvalid(string m_EmailCheck)
    {
        //Check SPACE
        if (m_EmailCheck.Contains(" "))
        {
            return false;
        }

        //Check @
        bool m_CheckAAIsExist = false;
        for (int i = 0; i < m_EmailCheck.Length; i++)
        {
            if (!m_CheckAAIsExist && m_EmailCheck[i] == '@')
            {
                m_CheckAAIsExist = true;
            }
            else
            if (m_CheckAAIsExist && m_EmailCheck[i] == '@')
            {
                return false;
            }
        }
        if (!m_CheckAAIsExist)
        {
            return false;
        }

        //All Check Done
        return true;
    }

    /// <summary>
    /// Check if STRING is EMAIL format
    /// </summary>
    /// <param name="m_EmailCheck"></param>
    /// <returns></returns>
    public static bool GetCheckEmail(string m_EmailCheck)
    {
        //Check Not Invalid
        if (!GetCheckEmaimNotInvalid(m_EmailCheck))
        {
            return false;
        }

        //Lower MAIL
        m_EmailCheck = m_EmailCheck.ToLower();

        return
            GetCheckEmaimGmail(m_EmailCheck) &&
            GetCheckEmaimYahoo(m_EmailCheck);
    }

    /// <summary>
    /// Check if GMAIL NOT INVAILID
    /// </summary>
    /// <param name="m_EmailCheck"></param>
    /// <returns>If NOT INVAILID, get TRUE</returns>
    private static bool GetCheckEmaimGmail(string m_EmailCheck)
    {
        //Check if GMAIL
        if (m_EmailCheck.Contains("@gmail.com"))
        {
            //Get ASCII
            byte[] ba_Ascii = Encoding.ASCII.GetBytes(m_EmailCheck);

            //First Character (Just Allow '0-9' and 'a-z')
            if (ba_Ascii[0] >= 48 && ba_Ascii[0] <= 57 ||
                ba_Ascii[0] >= 97 && ba_Ascii[0] <= 122)
            {
                //Next Character (Just Allow '0-9' and 'a-z' and '.')
                for (int i = 1; i < m_EmailCheck.Length; i++)
                {
                    if (m_EmailCheck[i] == '@')
                    {
                        break;
                    }

                    if (ba_Ascii[i] >= 48 && ba_Ascii[i] <= 57 ||
                        ba_Ascii[i] >= 97 && ba_Ascii[i] <= 122 ||
                        m_EmailCheck[i] == '.')
                    {

                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }

        //All Check Done
        return true;
    }

    /// <summary>
    /// Check if YAHOO NOT INVAILID
    /// </summary>
    /// <param name="m_EmailCheck"></param>
    /// <returns>If NOT INVAILID, get TRUE</returns>
    private static bool GetCheckEmaimYahoo(string m_EmailCheck)
    {
        //Check if GMAIL
        if (m_EmailCheck.Contains("@yahoo.com"))
        {
            //Get ASCII
            byte[] ba_Ascii = Encoding.ASCII.GetBytes(m_EmailCheck);

            //First Character (Just Allow 'a-z')
            if (ba_Ascii[0] >= 97 && ba_Ascii[0] <= 122)
            {
                //Next Character (Just Allow '0-9' and 'a-z' and '.' and '_')
                for (int i = 1; i < m_EmailCheck.Length; i++)
                {
                    if (m_EmailCheck[i] == '@')
                    {
                        break;
                    }

                    if (ba_Ascii[i] >= 48 && ba_Ascii[i] <= 57 ||
                        ba_Ascii[i] >= 97 && ba_Ascii[i] <= 122 ||
                        m_EmailCheck[i] == '.' ||
                        m_EmailCheck[i] == '_')
                    {

                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }

        //All Check Done
        return true;
    }

    #endregion
}