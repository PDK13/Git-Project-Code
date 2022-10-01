using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

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

    #region ================================================================== File Path 

    public static string GetPath_Disk(char c_Path_Disk)
    {
        return c_Path_Disk + @":\";
    }

    public static string GetPath_File(string sName, string s_Type, string s_FileExtend)
    {
        return sName + ((s_Type != null) ? "_" : "") + s_Type + "." + s_FileExtend;
    }

    #region Application Data Path 

    public static string GetPath_Application()
    {
        return Application.dataPath + @"\";
    }

    #endregion

    #region Resource Data Path (Access Read / Write in Editor, but just Access Read in Application)

    public static string GetPath_Application_ProjectData()
    {
        return GetPath_Application() + @"Project_Data\";
    }

    public static string GetPath_Application_Resource()
    {
        return GetPath_Application() + @"resources\";
    }

    public static string GetPath_Application_Resources_File(string s_Path_Folder, string s_FileName)
    {
        return GetPath_Application_Resource() + s_Path_Folder + (s_Path_Folder.Equals("") ? "" : (@"\")) + s_FileName + ".txt";
    }

    #endregion

    #region Persistent Data Path (Access to Project or Application)

    public static string GetPath_Application_Persistent()
    {
        return Application.persistentDataPath + @"\";
    }

    public static string GetPath_Application_Persistent_File(string s_Path_Folder, string s_FileName)
    {
        return GetPath_Application_Persistent() + s_Path_Folder + (s_Path_Folder.Equals("") ? "" : (@"\")) + s_FileName + ".txt";
    }

    #endregion

    #endregion

    #region ================================================================== File Exist

    public static bool GetFileIsExist(string s_Path)
    {
        return File.Exists(s_Path);
    }

    public static bool GetFile_Application_ResourcesIsExist(string s_Path_Folder, string s_FileName)
    {
        return GetFileIsExist(GetPath_Application_Resources_File(s_Path_Folder, s_FileName));
    }

    public static bool GetFile_Application_PersistentIsExist(string s_Path_Folder, string s_FileName)
    {
        return GetFileIsExist(GetPath_Application_Persistent_File(s_Path_Folder, s_FileName));
    }

    #endregion

    #region ================================================================== File Custom

    #region File Custom Write 

    private string s_TextWrite = "";

    public void Set_Data_Write_Clear()
    {
        s_TextWrite = "";
    }

    #region File Custom Write Start

    public static void Set_Data_Write_toFile(string s_Path, string s_Data)
    {
        using (FileStream m_yFile = File.Create(s_Path))
        {
            try
            {
                byte[] m_Info = new UTF8Encoding(true).GetBytes(s_Data);
                m_yFile.Write(m_Info, 0, m_Info.Length);
            }
            catch
            {
                Debug.LogErrorFormat("[Error] File Write Fail: {0}", s_Path);
            }
        }
    }

    public void Set_Data_Write_Start(string s_Path)
    {
        Set_Data_Write_toFile(s_Path, GetData_Write_String());
    }

    public void Set_Data_Write_Resource_Start(string s_Path_Folder, string s_FileName)
    {
        Set_Data_Write_Start(GetPath_Application_Resources_File(s_Path_Folder, s_FileName));
    }

    public void Set_Data_Write_Persistent_Start(string s_Path_Folder, string s_FileName)
    {
        Set_Data_Write_Start(GetPath_Application_Persistent_File(s_Path_Folder, s_FileName));
    }

    #endregion

    #region File Custom Write Set Data

    public void Set_Data_Write_Add(string s_Add)
    {
        if (s_TextWrite.Length != 0)
        {
            s_TextWrite += "\n";
        }

        s_TextWrite += s_Add;
    }

    public void Set_Data_Write_Add(int s_Add)
    {
        if (s_TextWrite.Length != 0)
        {
            s_TextWrite += "\n";
        }

        s_TextWrite += s_Add.ToString();
    }

    public void Set_Data_Write_Add(float s_Add)
    {
        if (s_TextWrite.Length != 0)
        {
            s_TextWrite += "\n";
        }

        s_TextWrite += s_Add.ToString();
    }

    public void Set_Data_Write_Add(double s_Add)
    {
        if (s_TextWrite.Length != 0)
        {
            s_TextWrite += "\n";
        }

        s_TextWrite += s_Add.ToString();
    }

    public void Set_Data_Write_Add(bool m_Add)
    {
        if (s_TextWrite.Length != 0)
        {
            s_TextWrite += "\n";
        }

        s_TextWrite += ((m_Add) ? "True" : "False");
    }

    public void Set_Data_Write_Add(Vector2 v2_Add, char c_Key)
    {
        Set_Data_Write_Add(ClassString.GetDataVector2Encypt(v2_Add, c_Key));
    }

    public void Set_Data_Write_Add(Vector2Int v2_Add, char c_Key)
    {
        Set_Data_Write_Add(ClassString.GetDataVector2IntEncypt(v2_Add, c_Key));
    }

    public void Set_Data_Write_Add(Vector3 v3_Add, char c_Key)
    {
        Set_Data_Write_Add(ClassString.GetDataVector3Encypt(v3_Add, c_Key));
    }

    public void Set_Data_Write_Add(Vector3Int v3_Add, char c_Key)
    {
        Set_Data_Write_Add(ClassString.GetDataVector3IntEncypt(v3_Add, c_Key));
    }

    #endregion

    #region File Custom Write Get String

    public string GetData_Write_String()
    {
        return s_TextWrite;
    }

    #endregion

    #endregion

    #region File Custom Read 

    private List<string> ls_TextRead = new List<string>();
    private int m_ReadRun = -1;

    public void Set_Data_Read_Clear()
    {
        ls_TextRead = new List<string>();
        m_ReadRun = -1;
    }

    #region File Custom Read Start

    public static List<string> GetData_Read_fromFile(string s_Path)
    {
        List<string> ls_TextRead = new List<string>();

        try
        {
            using (StreamReader sr = File.OpenText(s_Path))
            {
                string s_ReadRun = "";
                while ((s_ReadRun = sr.ReadLine()) != null)
                {
                    ls_TextRead.Add(s_ReadRun);
                }
            }

            return ls_TextRead;
        }
        catch
        {
            Debug.LogErrorFormat("[Error] File Read Fail: {0}", s_Path);

            return null;
        }
    }

    public void Set_Data_Read_Start(string s_Path)
    {
        ls_TextRead = GetData_Read_fromFile(s_Path);
    }

    public void Set_Data_Read_Persistent_Start(string s_Path_Folder, string s_FileName)
    {
        Set_Data_Read_Start(GetPath_Application_Persistent_File(s_Path_Folder, s_FileName));
    }

    public void Set_Data_Read_Resource_Start(string s_Path_Folder, string s_FileName)
    {
        //Debug.Log("Set_Data_Read_Start: " + s_Path_Folder + (s_Path_Folder.Equals("") ? "" : (@"\")) + s_FileName);

        TextAsset t_TextFile = (TextAsset)Resources.Load(
            s_Path_Folder + (s_Path_Folder.Equals("") ? "" : (@"\")) + s_FileName,
            typeof(TextAsset));

        ls_TextRead = ClassString.GetString_Split_List(t_TextFile.text, '\n');
    }

    #endregion

    #region File Custom Read Get Data

    public string GetData_Read_Auto_String()
    {
        m_ReadRun++;
        return ls_TextRead[m_ReadRun];
    }

    public int GetData_Read_Auto_Int()
    {
        m_ReadRun++;
        return int.Parse(ls_TextRead[m_ReadRun]);
    }

    public float GetData_Read_Auto_Float()
    {
        m_ReadRun++;
        return float.Parse(ls_TextRead[m_ReadRun]);
    }

    public double GetData_Read_Auto_Double()
    {
        m_ReadRun++;
        return double.Parse(ls_TextRead[m_ReadRun]);
    }

    public bool GetData_Read_Auto_Bool()
    {
        m_ReadRun++;
        return ls_TextRead[m_ReadRun] == "True";
    }

    public Vector2 GetData_Read_AutoVector2(char c_Key)
    {
        m_ReadRun++;
        return ClassString.GetDataVector2Dencypt(ls_TextRead[m_ReadRun], c_Key);
    }

    public Vector2Int GetData_Read_AutoVector2Int(char c_Key)
    {
        m_ReadRun++;
        return ClassString.GetDataVector2IntDencypt(ls_TextRead[m_ReadRun], c_Key);
    }

    public Vector3 GetData_Read_AutoVector3(char c_Key)
    {
        m_ReadRun++;
        return ClassString.GetDataVector3Dencypt(ls_TextRead[m_ReadRun], c_Key);
    }

    public Vector3Int GetData_Read_AutoVector3Int(char c_Key)
    {
        m_ReadRun++;
        return ClassString.GetDataVector3IntDencypt(ls_TextRead[m_ReadRun], c_Key);
    }

    public int GetData_Read_Auto_Current()
    {
        return m_ReadRun;
    }

    public bool GetData_Read_AutoIsEnd()
    {
        return GetData_Read_Auto_Current() >= GetData_Read_List().Count - 1;
    }

    #endregion

    #region File Custom Read Get List

    public List<string> GetData_Read_List()
    {
        return ls_TextRead;
    }

    #endregion

    #endregion

    #endregion

    #region ================================================================== File JSON

    //NOTE:
    //Type "TextAsset" is a "Text Document" File or "*.txt" File

    //SAMPLE:
    //ClassData m_yData = Class_FileIO.GetData_fromJSON<ClassData>(file_JSON_Data_TextDocument);

    public static ClassData GetData_fromJSON<ClassData>(TextAsset file_JSON_Data_TextDocument)
    {
        return GetData_fromJSON<ClassData>(file_JSON_Data_TextDocument.text);
    }

    public static ClassData GetData_fromJSON<ClassData>(string s_JSON_Data)
    {
        return JsonUtility.FromJson<ClassData>(s_JSON_Data);
    }

    public static string GetData_toJSON(object obj_JSON_Data_Class)
    {
        return JsonUtility.ToJson(obj_JSON_Data_Class);
    }

    public static void Set_Data_JSON_toFile(string s_Path, object obj_JSON_Data_Class)
    {
        Set_Data_Write_toFile(s_Path, GetData_toJSON(obj_JSON_Data_Class));
    }

    #endregion
}