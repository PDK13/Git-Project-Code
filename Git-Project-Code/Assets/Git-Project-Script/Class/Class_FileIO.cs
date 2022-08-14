using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// Working on IO FILE
/// </summary>
public class Class_FileIO
{
    public const string s_ExampleLink = @"D:\Class_FileIO.txt";

    /// <summary>
    /// Working on IO FILE
    /// </summary>
    public Class_FileIO()
    {
        Set_Data_Write_Clear();
        Set_Data_Read_Clear();
    }

    #region Primary Path 

    public static string Get_Path_Disk(char c_Path_Disk)
    {
        return c_Path_Disk + @":\";
    }

    public static string Get_Path_File(string s_Name, string s_Type, string s_FileExtend)
    {
        return s_Name + ((s_Type != null) ? "_" : "") + s_Type + "." + s_FileExtend;
    }

    #region Application Data Path 

    public static string Get_Path_Application()
    {
        return Application.dataPath + @"\";
    }

    #endregion

    #region Resource Data Path 

    public static string Get_Path_Application_ProjectData()
    {
        return Get_Path_Application() + @"Project_Data\";
    }

    public static string Get_Path_Application_Resource()
    {
        return Get_Path_Application() + @"resources\";
    }

    public static string Get_Path_Application_Resources_File(string s_Path_Folder, string s_File_Name)
    {
        return Get_Path_Application_Resource() + s_Path_Folder + (s_Path_Folder.Equals("") ? "" : (@"\")) + s_File_Name + ".txt";
    }

    #endregion

    #region Persistent Data Path 

    public static string Get_Path_Application_Persistent()
    {
        return Application.persistentDataPath + @"\";
    }

    public static string Get_Path_Application_Persistent_File(string s_Path_Folder, string s_File_Name)
    {
        return Get_Path_Application_Persistent() + s_Path_Folder + (s_Path_Folder.Equals("") ? "" : (@"\")) + s_File_Name + ".txt";
    }

    #endregion

    #endregion

    #region Check File Exist

    /// <summary>
    /// Check Path of File Exist
    /// </summary>
    /// <param name="s_Path"></param>
    /// <returns></returns>
    public static bool Get_File_isExist(string s_Path)
    {
        return File.Exists(s_Path);
    }

    public static bool Get_File_Application_Persistent_isExist(string s_Path_Folder, string s_File_Name)
    {
        //Debug.Log("Get_File_Application_Persistent_isExist: " + Get_Path_Application_Persistent_File(s_Path_Folder, s_File_Name));

        return Get_File_isExist(Get_Path_Application_Persistent_File(s_Path_Folder, s_File_Name));
    }

    /// <summary>
    /// Check Path of File Exist
    /// </summary>
    /// <param name="s_Path_Folder"></param>
    /// <param name="s_File_Name"></param>
    /// <returns></returns>
    public static bool Get_File_Application_Resources_isExist(string s_Path_Folder, string s_File_Name)
    {
        return Get_File_isExist(Get_Path_Application_Resources_File(s_Path_Folder, s_File_Name));
    }

    #endregion

    #region Write File 

    private string s_TextWrite = "";

    public void Set_Data_Write_Clear()
    {
        s_TextWrite = "";
    }

    #region Write File Start

    public void Set_Data_Write_Start(string s_Path)
    {
        //Debug.Log("Set_Data_Write_Start: " + s_Path);

        using (FileStream myFile = File.Create(s_Path))
        {
            try
            {
                byte[] b_Info = new UTF8Encoding(true).GetBytes(s_TextWrite);
                myFile.Write(b_Info, 0, b_Info.Length);
            }
            catch
            {
                Debug.LogError("Set_Data_Write_Start(" + s_Path + ")");
            }
        }
    }

    public void Set_Data_Write_Persistent_Start(string s_Path_Folder, string s_File_Name)
    {
        Set_Data_Write_Start(Get_Path_Application_Persistent_File(s_Path_Folder, s_File_Name));
    }

    public void Set_Data_Write_Resource_Start(string s_Path_Folder, string s_File_Name)
    {
        Set_Data_Write_Start(Get_Path_Application_Resources_File(s_Path_Folder, s_File_Name));
    }

    #endregion

    #region Write File Set Data

    public void Set_Data_Write_Add(string s_Add)
    {
        if (s_TextWrite.Length != 0)
            s_TextWrite += "\n";
        s_TextWrite += s_Add;
    }

    public void Set_Data_Write_Add(int s_Add)
    {
        if (s_TextWrite.Length != 0)
            s_TextWrite += "\n";
        s_TextWrite += s_Add.ToString();
    }

    public void Set_Data_Write_Add(float s_Add)
    {
        if (s_TextWrite.Length != 0)
            s_TextWrite += "\n";
        s_TextWrite += s_Add.ToString();
    }

    public void Set_Data_Write_Add(double s_Add)
    {
        if (s_TextWrite.Length != 0)
            s_TextWrite += "\n";
        s_TextWrite += s_Add.ToString();
    }

    public void Set_Data_Write_Add(bool b_Add)
    {
        if (s_TextWrite.Length != 0)
            s_TextWrite += "\n";
        s_TextWrite += ((b_Add) ? "True" : "False");
    }

    public void Set_Data_Write_Add(Vector2 v2_Add, char c_Key)
    {
        Set_Data_Write_Add(Class_String.Get_Data_Vector2_Encypt(v2_Add, c_Key));
    }

    public void Set_Data_Write_Add(Vector2Int v2_Add, char c_Key)
    {
        Set_Data_Write_Add(Class_String.Get_Data_Vector2Int_Encypt(v2_Add, c_Key));
    }

    public void Set_Data_Write_Add(Vector3 v3_Add, char c_Key)
    {
        Set_Data_Write_Add(Class_String.Get_Data_Vector3_Encypt(v3_Add, c_Key));
    }

    public void Set_Data_Write_Add(Vector3Int v3_Add, char c_Key)
    {
        Set_Data_Write_Add(Class_String.Get_Data_Vector3Int_Encypt(v3_Add, c_Key));
    }

    #endregion

    #region Write File Get String

    public string Get_Data_Write_String()
    {
        return s_TextWrite;
    }

    #endregion

    #endregion

    #region Read File 

    private List<string> ls_TextRead = new List<string>();
    private int i_ReadRun = -1;

    public void Set_Data_Read_Clear()
    {
        ls_TextRead = new List<string>();
        i_ReadRun = -1;
    }

    #region Read File Start

    public void Set_Data_Read_Start(string s_Path)
    {
        //Debug.Log("Set_Data_Read_Start: " + s_Path);

        try
        {
            // Open the stream and read it back.
            using (StreamReader sr = File.OpenText(s_Path))
            {
                string s_ReadRun = "";
                while ((s_ReadRun = sr.ReadLine()) != null)
                {
                    ls_TextRead.Add(s_ReadRun);
                }
            }
        }
        catch
        {
            Debug.LogError("Set_Data_Read_Start(" + s_Path + ")");
        }
    }

    public void Set_Data_Read_Persistent_Start(string s_Path_Folder, string s_File_Name)
    {
        Set_Data_Read_Start(Get_Path_Application_Persistent_File(s_Path_Folder, s_File_Name));
    }

    public void Set_Data_Read_Resource_Start(string s_Path_Folder, string s_File_Name)
    {
        //Debug.Log("Set_Data_Read_Start: " + s_Path_Folder + (s_Path_Folder.Equals("") ? "" : (@"\")) + s_File_Name);

        TextAsset t_TextFile = (TextAsset)Resources.Load(
            s_Path_Folder + (s_Path_Folder.Equals("") ? "" : (@"\")) + s_File_Name,
            typeof(TextAsset));

        ls_TextRead = Class_String.Get_String_Split_List(t_TextFile.text, '\n');
    }

    #endregion

    #region Read File Get Data

    public string Get_Data_Read_Auto_String()
    {
        i_ReadRun++;
        return ls_TextRead[i_ReadRun];
    }

    public int Get_Data_Read_Auto_Int()
    {
        i_ReadRun++;
        return int.Parse(ls_TextRead[i_ReadRun]);
    }

    public float Get_Data_Read_Auto_Float()
    {
        i_ReadRun++;
        return float.Parse(ls_TextRead[i_ReadRun]);
    }

    public double Get_Data_Read_Auto_Double()
    {
        i_ReadRun++;
        return double.Parse(ls_TextRead[i_ReadRun]);
    }

    public bool Get_Data_Read_Auto_Bool()
    {
        i_ReadRun++;
        return ls_TextRead[i_ReadRun] == "True";
    }

    public Vector2 Get_Data_Read_Auto_Vector2(char c_Key)
    {
        i_ReadRun++;
        return Class_String.Get_Data_Vector2_Dencypt(ls_TextRead[i_ReadRun], c_Key);
    }

    public Vector2Int Get_Data_Read_Auto_Vector2Int(char c_Key)
    {
        i_ReadRun++;
        return Class_String.Get_Data_Vector2Int_Dencypt(ls_TextRead[i_ReadRun], c_Key);
    }

    public Vector3 Get_Data_Read_Auto_Vector3(char c_Key)
    {
        i_ReadRun++;
        return Class_String.Get_Data_Vector3_Dencypt(ls_TextRead[i_ReadRun], c_Key);
    }

    public Vector3Int Get_Data_Read_Auto_Vector3Int(char c_Key)
    {
        i_ReadRun++;
        return Class_String.Get_Data_Vector3Int_Dencypt(ls_TextRead[i_ReadRun], c_Key);
    }

    public int Get_Data_Read_Auto_Current()
    {
        return i_ReadRun;
    }

    public bool Get_Data_Read_Auto_isEnd()
    {
        return Get_Data_Read_Auto_Current() >= Get_Data_Read_List().Count - 1;
    }

    #endregion

    #region Read File Get List

    public List<string> Get_Data_Read_List()
    {
        return ls_TextRead;
    }

    #endregion

    #endregion
}