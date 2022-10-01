using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

/// <summary>
/// Working on IO FILE
/// </summary>
public class Clasm_FileIO
{
    public const string m_ExampleLink = @"D:\Clasm_FileIO.txt";

    /// <summary>
    /// Working on IO FILE
    /// </summary>
    public Clasm_FileIO()
    {
        SetData_Write_Clear();
        SetData_Read_Clear();
    }

    #region ================================================================== File Path 

    public static string GetPath_Disk(char c_Path_Disk)
    {
        return c_Path_Disk + @":\";
    }

    public static string GetPath_File(string sName, string m_Type, string m_FileExtend)
    {
        return sName + ((m_Type != null) ? "_" : "") + m_Type + "." + m_FileExtend;
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

    public static string GetPath_Application_Resourcem_File(string m_Path_Folder, string m_FileName)
    {
        return GetPath_Application_Resource() + m_Path_Folder + (m_Path_Folder.Equals("") ? "" : (@"\")) + m_FileName + ".txt";
    }

    #endregion

    #region Persistent Data Path (Access to Project or Application)

    public static string GetPath_Application_Persistent()
    {
        return Application.persistentDataPath + @"\";
    }

    public static string GetPath_Application_Persistent_File(string m_Path_Folder, string m_FileName)
    {
        return GetPath_Application_Persistent() + m_Path_Folder + (m_Path_Folder.Equals("") ? "" : (@"\")) + m_FileName + ".txt";
    }

    #endregion

    #endregion

    #region ================================================================== File Exist

    public static bool GetFileIsExist(string m_Path)
    {
        return File.Exists(m_Path);
    }

    public static bool GetFile_Application_ResourcesIsExist(string m_Path_Folder, string m_FileName)
    {
        return GetFileIsExist(GetPath_Application_Resourcem_File(m_Path_Folder, m_FileName));
    }

    public static bool GetFile_Application_PersistentIsExist(string m_Path_Folder, string m_FileName)
    {
        return GetFileIsExist(GetPath_Application_Persistent_File(m_Path_Folder, m_FileName));
    }

    #endregion

    #region ================================================================== File Custom

    #region File Custom Write 

    private string m_TextWrite = "";

    public void SetData_Write_Clear()
    {
        m_TextWrite = "";
    }

    #region File Custom Write Start

    public static void SetData_Write_toFile(string m_Path, string m_Data)
    {
        using (FileStream m_yFile = File.Create(m_Path))
        {
            try
            {
                byte[] m_Info = new UTF8Encoding(true).GetBytes(m_Data);
                m_yFile.Write(m_Info, 0, m_Info.Length);
            }
            catch
            {
                Debug.LogErrorFormat("[Error] File Write Fail: {0}", m_Path);
            }
        }
    }

    public void SetData_Write_Start(string m_Path)
    {
        SetData_Write_toFile(m_Path, GetData_Write_String());
    }

    public void SetData_Write_Resource_Start(string m_Path_Folder, string m_FileName)
    {
        SetData_Write_Start(GetPath_Application_Resourcem_File(m_Path_Folder, m_FileName));
    }

    public void SetData_Write_Persistent_Start(string m_Path_Folder, string m_FileName)
    {
        SetData_Write_Start(GetPath_Application_Persistent_File(m_Path_Folder, m_FileName));
    }

    #endregion

    #region File Custom Write Set Data

    public void SetData_Write_Add(string m_Add)
    {
        if (m_TextWrite.Length != 0)
        {
            m_TextWrite += "\n";
        }

        m_TextWrite += m_Add;
    }

    public void SetData_Write_Add(int m_Add)
    {
        if (m_TextWrite.Length != 0)
        {
            m_TextWrite += "\n";
        }

        m_TextWrite += m_Add.ToString();
    }

    public void SetData_Write_Add(float m_Add)
    {
        if (m_TextWrite.Length != 0)
        {
            m_TextWrite += "\n";
        }

        m_TextWrite += m_Add.ToString();
    }

    public void SetData_Write_Add(double m_Add)
    {
        if (m_TextWrite.Length != 0)
        {
            m_TextWrite += "\n";
        }

        m_TextWrite += m_Add.ToString();
    }

    public void SetData_Write_Add(bool m_Add)
    {
        if (m_TextWrite.Length != 0)
        {
            m_TextWrite += "\n";
        }

        m_TextWrite += ((m_Add) ? "True" : "False");
    }

    public void SetData_Write_Add(Vector2 v2_Add, char c_Key)
    {
        SetData_Write_Add(ClassString.GetDataVector2Encypt(v2_Add, c_Key));
    }

    public void SetData_Write_Add(Vector2Int v2_Add, char c_Key)
    {
        SetData_Write_Add(ClassString.GetDataVector2IntEncypt(v2_Add, c_Key));
    }

    public void SetData_Write_Add(Vector3 v3_Add, char c_Key)
    {
        SetData_Write_Add(ClassString.GetDataVector3Encypt(v3_Add, c_Key));
    }

    public void SetData_Write_Add(Vector3Int v3_Add, char c_Key)
    {
        SetData_Write_Add(ClassString.GetDataVector3IntEncypt(v3_Add, c_Key));
    }

    #endregion

    #region File Custom Write Get String

    public string GetData_Write_String()
    {
        return m_TextWrite;
    }

    #endregion

    #endregion

    #region File Custom Read 

    private List<string> lm_TextRead = new List<string>();
    private int m_ReadRun = -1;

    public void SetData_Read_Clear()
    {
        lm_TextRead = new List<string>();
        m_ReadRun = -1;
    }

    #region File Custom Read Start

    public static List<string> GetData_Read_fromFile(string m_Path)
    {
        List<string> lm_TextRead = new List<string>();

        try
        {
            using (StreamReader sr = File.OpenText(m_Path))
            {
                string m_ReadRun = "";
                while ((m_ReadRun = sr.ReadLine()) != null)
                {
                    lm_TextRead.Add(m_ReadRun);
                }
            }

            return lm_TextRead;
        }
        catch
        {
            Debug.LogErrorFormat("[Error] File Read Fail: {0}", m_Path);

            return null;
        }
    }

    public void SetData_Read_Start(string m_Path)
    {
        lm_TextRead = GetData_Read_fromFile(m_Path);
    }

    public void SetData_Read_Persistent_Start(string m_Path_Folder, string m_FileName)
    {
        SetData_Read_Start(GetPath_Application_Persistent_File(m_Path_Folder, m_FileName));
    }

    public void SetData_Read_Resource_Start(string m_Path_Folder, string m_FileName)
    {
        //Debug.Log("SetData_Read_Start: " + m_Path_Folder + (m_Path_Folder.Equals("") ? "" : (@"\")) + m_FileName);

        TextAsset t_TextFile = (TextAsset)Resources.Load(
            m_Path_Folder + (m_Path_Folder.Equals("") ? "" : (@"\")) + m_FileName,
            typeof(TextAsset));

        lm_TextRead = ClassString.GetString_Split_List(t_TextFile.text, '\n');
    }

    #endregion

    #region File Custom Read Get Data

    public string GetData_Read_Auto_String()
    {
        m_ReadRun++;
        return lm_TextRead[m_ReadRun];
    }

    public int GetData_Read_Auto_Int()
    {
        m_ReadRun++;
        return int.Parse(lm_TextRead[m_ReadRun]);
    }

    public float GetData_Read_Auto_Float()
    {
        m_ReadRun++;
        return float.Parse(lm_TextRead[m_ReadRun]);
    }

    public double GetData_Read_Auto_Double()
    {
        m_ReadRun++;
        return double.Parse(lm_TextRead[m_ReadRun]);
    }

    public bool GetData_Read_Auto_Bool()
    {
        m_ReadRun++;
        return lm_TextRead[m_ReadRun] == "True";
    }

    public Vector2 GetData_Read_AutoVector2(char c_Key)
    {
        m_ReadRun++;
        return ClassString.GetDataVector2Dencypt(lm_TextRead[m_ReadRun], c_Key);
    }

    public Vector2Int GetData_Read_AutoVector2Int(char c_Key)
    {
        m_ReadRun++;
        return ClassString.GetDataVector2IntDencypt(lm_TextRead[m_ReadRun], c_Key);
    }

    public Vector3 GetData_Read_AutoVector3(char c_Key)
    {
        m_ReadRun++;
        return ClassString.GetDataVector3Dencypt(lm_TextRead[m_ReadRun], c_Key);
    }

    public Vector3Int GetData_Read_AutoVector3Int(char c_Key)
    {
        m_ReadRun++;
        return ClassString.GetDataVector3IntDencypt(lm_TextRead[m_ReadRun], c_Key);
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
        return lm_TextRead;
    }

    #endregion

    #endregion

    #endregion

    #region ================================================================== File JSON

    //NOTE:
    //Type "TextAsset" is a "Text Document" File or "*.txt" File

    //SAMPLE:
    //ClassData m_yData = Clasm_FileIO.GetData_fromJSON<ClassData>(file_JSON_Data_TextDocument);

    public static ClassData GetData_fromJSON<ClassData>(TextAsset file_JSON_Data_TextDocument)
    {
        return GetData_fromJSON<ClassData>(file_JSON_Data_TextDocument.text);
    }

    public static ClassData GetData_fromJSON<ClassData>(string m_JSON_Data)
    {
        return JsonUtility.FromJson<ClassData>(m_JSON_Data);
    }

    public static string GetData_toJSON(object obj_JSON_Data_Class)
    {
        return JsonUtility.ToJson(obj_JSON_Data_Class);
    }

    public static void SetData_JSON_toFile(string m_Path, object obj_JSON_Data_Class)
    {
        SetData_Write_toFile(m_Path, GetData_toJSON(obj_JSON_Data_Class));
    }

    #endregion
}