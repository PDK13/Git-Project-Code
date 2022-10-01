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

    #region Resource Data Path (Access Read / Write in Editor, but just Access Read in Application)

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

    #region Persistent Data Path (Access to Project or Application)

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

    #region ================================================================== File Exist

    public static bool Get_File_isExist(string s_Path)
    {
        return File.Exists(s_Path);
    }

    public static bool Get_File_Application_Resources_isExist(string s_Path_Folder, string s_File_Name)
    {
        return Get_File_isExist(Get_Path_Application_Resources_File(s_Path_Folder, s_File_Name));
    }

    public static bool Get_File_Application_Persistent_isExist(string s_Path_Folder, string s_File_Name)
    {
        return Get_File_isExist(Get_Path_Application_Persistent_File(s_Path_Folder, s_File_Name));
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
        using (FileStream myFile = File.Create(s_Path))
        {
            try
            {
                byte[] b_Info = new UTF8Encoding(true).GetBytes(s_Data);
                myFile.Write(b_Info, 0, b_Info.Length);
            }
            catch
            {
                Debug.LogErrorFormat("[Error] File Write Fail: {0}", s_Path);
            }
        }
    }

    public void Set_Data_Write_Start(string s_Path)
    {
        Set_Data_Write_toFile(s_Path, Get_Data_Write_String());
    }

    public void Set_Data_Write_Resource_Start(string s_Path_Folder, string s_File_Name)
    {
        Set_Data_Write_Start(Get_Path_Application_Resources_File(s_Path_Folder, s_File_Name));
    }

    public void Set_Data_Write_Persistent_Start(string s_Path_Folder, string s_File_Name)
    {
        Set_Data_Write_Start(Get_Path_Application_Persistent_File(s_Path_Folder, s_File_Name));
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

    public void Set_Data_Write_Add(bool b_Add)
    {
        if (s_TextWrite.Length != 0)
        {
            s_TextWrite += "\n";
        }

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

    #region File Custom Write Get String

    public string Get_Data_Write_String()
    {
        return s_TextWrite;
    }

    #endregion

    #endregion

    #region File Custom Read 

    private List<string> ls_TextRead = new List<string>();
    private int i_ReadRun = -1;

    public void Set_Data_Read_Clear()
    {
        ls_TextRead = new List<string>();
        i_ReadRun = -1;
    }

    #region File Custom Read Start

    public static List<string> Get_Data_Read_fromFile(string s_Path)
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
        ls_TextRead = Get_Data_Read_fromFile(s_Path);
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

    #region File Custom Read Get Data

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

    #region File Custom Read Get List

    public List<string> Get_Data_Read_List()
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
    //ClassData myData = Class_FileIO.Get_Data_fromJSON<ClassData>(file_JSON_Data_TextDocument);

    public static ClassData Get_Data_fromJSON<ClassData>(TextAsset file_JSON_Data_TextDocument)
    {
        return Get_Data_fromJSON<ClassData>(file_JSON_Data_TextDocument.text);
    }

    public static ClassData Get_Data_fromJSON<ClassData>(string s_JSON_Data)
    {
        return JsonUtility.FromJson<ClassData>(s_JSON_Data);
    }

    public static string Get_Data_toJSON(object obj_JSON_Data_Class)
    {
        return JsonUtility.ToJson(obj_JSON_Data_Class);
    }

    public static void Set_Data_JSON_toFile(string s_Path, object obj_JSON_Data_Class)
    {
        Set_Data_Write_toFile(s_Path, Get_Data_toJSON(obj_JSON_Data_Class));
    }

    #endregion
}