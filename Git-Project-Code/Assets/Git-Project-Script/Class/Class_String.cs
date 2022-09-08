using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

/// <summary>
/// Working on String
/// </summary>
public class Class_String
{
    /// <summary>
    /// Working on String
    /// </summary>
    public Class_String()
    {

    }

    #region String Command

    #region String Count

    public static int Get_StringArray_Count(string[] s_StringArray)
    {
        return s_StringArray.Length;
    }

    public static int Get_StringList_Count(List<string> l_StringList)
    {
        return l_StringList.Count;
    }

    #endregion

    #region String Exist

    public static bool Get_String_Exist(string s_FatherString, string s_ChildString)
    {
        return s_FatherString.Contains(s_ChildString);
    }

    #endregion

    #region String Replace

    public static string Get_String_Replace(string s_FatherString, string s_CheckString, string s_ReplaceString)
    {
        return s_FatherString.Replace(s_CheckString, s_ReplaceString);
    }

    public static string Get_String_Replace_Clone(string s_Clone_Name)
    {
        return Get_String_Replace(s_Clone_Name, "(Clone)", "");
    }

    public static string Get_String_Replace_Resources(string s_Resources_Path)
    {
        return Get_String_Replace(s_Resources_Path, "Assets/resources/", "");
    }

    #endregion

    #region String Split

    public static string[] Get_String_Split_Array(string s_FatherString, char c_Key)
    {
        return s_FatherString.Split(c_Key);
    }

    public static List<string> Get_String_Split_List(string s_FatherString, char c_Key)
    {
        string[] s_SplitString = Get_String_Split_Array(s_FatherString, c_Key);

        List<string> l_SplitString = new List<string>();

        l_SplitString.AddRange(s_SplitString);

        return l_SplitString;
    }

    #endregion

    #endregion

    #region String Data

    #region String Data Main

    #region String Data Main Encypt

    public static string Get_Data_Encypt(List<string> l_DataList, char c_Key)
    {
        string s_Data = "";

        for (int i = 0; i < l_DataList.Count; i++)
        {
            //s_Data += (l_DataList[i]);

            //if (i < l_DataList.Count - 1)
            //{
            //    s_Data += c_Key;
            //}

            s_Data = Get_Data_Encypt_Add(s_Data, l_DataList[i], c_Key);
        }

        return s_Data;
    }

    public static string Get_Data_Encypt(List<int> l_DataList, char c_Key)
    {
        string s_Data = "";

        for (int i = 0; i < l_DataList.Count; i++)
        {
            //s_Data += (l_DataList[i].ToString());

            //if (i < l_DataList.Count - 1)
            //{
            //    s_Data += c_Key;
            //}

            s_Data = Get_Data_Encypt_Add(s_Data, l_DataList[i], c_Key);
        }

        return s_Data;
    }

    public static string Get_Data_Encypt(List<float> l_DataList, char c_Key)
    {
        string s_Data = "";

        for (int i = 0; i < l_DataList.Count; i++)
        {
            //s_Data += (l_DataList[i].ToString());

            //if (i < l_DataList.Count - 1)
            //{
            //    s_Data += c_Key;
            //}

            s_Data = Get_Data_Encypt_Add(s_Data, l_DataList[i], c_Key);
        }

        return s_Data;
    }

    public static string Get_Data_Encypt(List<bool> l_DataList, char c_Key)
    {
        string s_Data = "";

        for (int i = 0; i < l_DataList.Count; i++)
        {
            //s_Data += ((l_DataList[i]) ? "1" : "0");

            //if (i < l_DataList.Count - 1)
            //{
            //    s_Data += c_Key;
            //}

            s_Data = Get_Data_Encypt_Add(s_Data, l_DataList[i], c_Key);
        }

        return s_Data;
    }

    #endregion

    #region String Data Main Add Encypt

    public static string Get_Data_Encypt_Add(string s_Data, string s_Data_Add, char c_Key)
    {
        return s_Data + ((s_Data.Length != 0) ? c_Key.ToString() : "") + s_Data_Add;
    }

    public static string Get_Data_Encypt_Add(string s_Data, int i_Data_Add, char c_Key)
    {
        return s_Data + ((s_Data.Length != 0) ? c_Key.ToString() : "") + i_Data_Add.ToString();
    }

    public static string Get_Data_Encypt_Add(string s_Data, float f_Data_Add, char c_Key)
    {
        return s_Data + ((s_Data.Length != 0) ? c_Key.ToString() : "") + f_Data_Add.ToString();
    }

    public static string Get_Data_Encypt_Add(string s_Data, bool b_Data_Add, char c_Key)
    {
        return s_Data + ((s_Data.Length != 0) ? c_Key.ToString() : "") + ((b_Data_Add) ? "1" : "0");
    }

    #endregion

    #region String Data Main Dencypt

    public static List<string> Get_Data_Dencypt_String(string s_Data, char c_Key)
    {
        if (s_Data.Equals(""))
        {
            Debug.LogWarning("Get_Data_Dencypt_String: Emty Data!");

            return new List<string>();
        }

        return Get_String_Split_List(s_Data, c_Key);
    }

    public static List<int> Get_Data_Dencypt_Int(string s_Data, char c_Key)
    {
        if (s_Data.Equals(""))
        {
            Debug.LogWarning("Get_Data_Dencypt_Int: Emty Data!");

            return new List<int>();
        }

        List<string> ls_Data = Get_Data_Dencypt_String(s_Data, c_Key);

        List<int> l_Data = new List<int>();

        for (int i = 0; i < ls_Data.Count; i++)
        {
            l_Data.Add(int.Parse(ls_Data[i]));
        }

        return l_Data;
    }

    public static List<float> Get_Data_Dencypt_Float(string s_Data, char c_Key)
    {
        if (s_Data.Equals(""))
        {
            Debug.LogWarning("Get_Data_Dencypt_Float: Emty Data!");

            return new List<float>();
        }

        List<string> ls_Data = Get_String_Split_List(s_Data, c_Key);

        List<float> l_Data = new List<float>();

        for (int i = 0; i < ls_Data.Count; i++)
        {
            l_Data.Add(float.Parse(ls_Data[i]));
        }

        return l_Data;
    }

    public static List<bool> Get_Data_Dencypt_Bool(string s_Data, char c_Key)
    {
        if (s_Data.Equals(""))
        {
            Debug.LogWarning("Get_Data_Dencypt_Bool: Emty Data!");

            return new List<bool>();
        }

        List<string> ls_Data = Get_String_Split_List(s_Data, c_Key);

        List<bool> l_Data = new List<bool>();

        for (int i = 0; i < ls_Data.Count; i++)
        {
            string s_Bool = ls_Data[i];

            if (s_Bool == "1")
            {
                l_Data.Add(true);
            }
            else
            if (s_Bool == "0")
            {
                l_Data.Add(false);
            }
        }

        return l_Data;
    }

    #endregion

    #endregion

    #region String Data Vector

    #region String Data Vector Encypt

    public static string Get_Data_Vector2_Encypt(Vector2 v2_VectorData, char c_Key)
    {
        return v2_VectorData.x.ToString() + c_Key + v2_VectorData.y.ToString();
    }

    public static string Get_Data_Vector3_Encypt(Vector3 v3_VectorData, char c_Key)
    {
        return v3_VectorData.x.ToString() + c_Key + v3_VectorData.y.ToString() + c_Key + v3_VectorData.z.ToString();
    }

    public static string Get_Data_Vector2Int_Encypt(Vector2Int v2_VectorData, char c_Key)
    {
        return v2_VectorData.x.ToString() + c_Key + v2_VectorData.y.ToString();
    }

    public static string Get_Data_Vector3Int_Encypt(Vector3Int v3_VectorData, char c_Key)
    {
        return v3_VectorData.x.ToString() + c_Key + v3_VectorData.y.ToString() + c_Key + v3_VectorData.z.ToString();
    }

    #endregion

    #region String Data Vector Dencypt

    public static Vector2 Get_Data_Vector2_Dencypt(string s_VectorData, char c_Key)
    {
        List<float> l_Data = Get_Data_Dencypt_Float(s_VectorData, c_Key);

        return new Vector2(l_Data[0], l_Data[1]);
    }

    public static Vector3 Get_Data_Vector3_Dencypt(string s_VectorData, char c_Key)
    {
        List<float> l_Data = Get_Data_Dencypt_Float(s_VectorData, c_Key);

        return new Vector3(l_Data[0], l_Data[1], l_Data[2]);
    }

    public static Vector2Int Get_Data_Vector2Int_Dencypt(string s_VectorData, char c_Key)
    {
        List<int> l_Data = Get_Data_Dencypt_Int(s_VectorData, c_Key);

        return new Vector2Int(l_Data[0], l_Data[1]);
    }

    public static Vector3Int Get_Data_Vector3Int_Dencypt(string s_VectorData, char c_Key)
    {
        List<int> l_Data = Get_Data_Dencypt_Int(s_VectorData, c_Key);

        return new Vector3Int(l_Data[0], l_Data[1], l_Data[2]);
    }

    #endregion

    #endregion

    #endregion

    #region Email Check (Local-Part@Domain-Part)

    /// <summary>
    /// Check if EMAIL NOT INVAILID (NOT FALSE)
    /// </summary>
    /// <param name="s_EmailCheck"></param>
    /// <returns>If NOT INVAILID, get TRUE</returns>
    private static bool Get_CheckEmail_NotInvalid(string s_EmailCheck)
    {
        //Check SPACE
        if (s_EmailCheck.Contains(" "))
            return false;

        //Check @
        bool b_Exist_AA = false;
        for (int i = 0; i < s_EmailCheck.Length; i++)
        {
            if (!b_Exist_AA && s_EmailCheck[i] == '@')
                b_Exist_AA = true;
            else
            if (b_Exist_AA && s_EmailCheck[i] == '@')
                return false;
        }
        if (!b_Exist_AA)
            return false;

        //All Check Done
        return true;
    }

    /// <summary>
    /// Check if STRING is EMAIL format
    /// </summary>
    /// <param name="s_EmailCheck"></param>
    /// <returns></returns>
    public static bool Get_CheckEmail(string s_EmailCheck)
    {
        //Check Not Invalid
        if (!Get_CheckEmail_NotInvalid(s_EmailCheck))
            return false;

        //Lower MAIL
        s_EmailCheck = s_EmailCheck.ToLower();

        return
            Get_CheckEmail_Gmail(s_EmailCheck) &&
            Get_CheckEmail_Yahoo(s_EmailCheck);
    }

    /// <summary>
    /// Check if GMAIL NOT INVAILID
    /// </summary>
    /// <param name="s_EmailCheck"></param>
    /// <returns>If NOT INVAILID, get TRUE</returns>
    private static bool Get_CheckEmail_Gmail(string s_EmailCheck)
    {
        //Check if GMAIL
        if (s_EmailCheck.Contains("@gmail.com"))
        {
            //Get ASCII
            byte[] ba_Ascii = Encoding.ASCII.GetBytes(s_EmailCheck);

            //First Character (Just Allow '0-9' and 'a-z')
            if (ba_Ascii[0] >= 48 && ba_Ascii[0] <= 57 ||
                ba_Ascii[0] >= 97 && ba_Ascii[0] <= 122)
            {
                //Next Character (Just Allow '0-9' and 'a-z' and '.')
                for (int i = 1; i < s_EmailCheck.Length; i++)
                {
                    if (s_EmailCheck[i] == '@')
                        break;

                    if (ba_Ascii[i] >= 48 && ba_Ascii[i] <= 57 ||
                        ba_Ascii[i] >= 97 && ba_Ascii[i] <= 122 ||
                        s_EmailCheck[i] == '.')
                    {

                    }
                    else
                        return false;
                }
            }
            else
                return false;
        }

        //All Check Done
        return true;
    }

    /// <summary>
    /// Check if YAHOO NOT INVAILID
    /// </summary>
    /// <param name="s_EmailCheck"></param>
    /// <returns>If NOT INVAILID, get TRUE</returns>
    private static bool Get_CheckEmail_Yahoo(string s_EmailCheck)
    {
        //Check if GMAIL
        if (s_EmailCheck.Contains("@yahoo.com"))
        {
            //Get ASCII
            byte[] ba_Ascii = Encoding.ASCII.GetBytes(s_EmailCheck);

            //First Character (Just Allow 'a-z')
            if (ba_Ascii[0] >= 97 && ba_Ascii[0] <= 122)
            {
                //Next Character (Just Allow '0-9' and 'a-z' and '.' and '_')
                for (int i = 1; i < s_EmailCheck.Length; i++)
                {
                    if (s_EmailCheck[i] == '@')
                        break;

                    if (ba_Ascii[i] >= 48 && ba_Ascii[i] <= 57 ||
                        ba_Ascii[i] >= 97 && ba_Ascii[i] <= 122 ||
                        s_EmailCheck[i] == '.' ||
                        s_EmailCheck[i] == '_')
                    {

                    }
                    else
                        return false;
                }
            }
            else
                return false;
        }

        //All Check Done
        return true;
    }

    #endregion
}