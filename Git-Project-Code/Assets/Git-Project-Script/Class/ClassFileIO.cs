using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class ClassFileIO
{
    public const string m_ExamplePath = @"D:\ClassFileIO.txt";

    public ClassFileIO()
    {
        SetWriteDataClear();
        SetReadDataClear();
    }

    #region ================================================================== File Path 

    public static string GetPathDisk(char m_PathDisk)
    {
        return m_PathDisk + @":\";
    }

    public static string GetPathFile(string sName, string m_Type, string m_FileExtend)
    {
        return sName + ((m_Type != null) ? "_" : "") + m_Type + "." + m_FileExtend;
    }

    #region Application Data Path 

    public static string GetPathApplication()
    {
        return Application.dataPath + @"\";
    }

    #endregion

    #region Resource Data Path (Access Read / Write in Editor, but just Access Read in Application)

    public static string GetPathApplication_ProjectData()
    {
        return GetPathApplication() + @"ProjectData\";
    }

    public static string GetPathApplicationResource()
    {
        return GetPathApplication() + @"resources\";
    }

    public static string GetPathApplicationResourcemFile(string m_PathFolder, string m_FileName)
    {
        return GetPathApplicationResource() + m_PathFolder + (m_PathFolder.Equals("") ? "" : (@"\")) + m_FileName + ".txt";
    }

    #endregion

    #region Persistent Data Path (Access to Project or Application)

    public static string GetPathApplicationPersistent()
    {
        return Application.persistentDataPath + @"\";
    }

    public static string GetPathApplicationPersistentFile(string m_PathFolder, string m_FileName)
    {
        return GetPathApplicationPersistent() + m_PathFolder + (m_PathFolder.Equals("") ? "" : (@"\")) + m_FileName + ".txt";
    }

    #endregion

    #endregion

    #region ================================================================== File Exist

    public static bool GetCheckFileExist(string m_Path)
    {
        return File.Exists(m_Path);
    }

    public static bool GetCheckFileApplicationResourcesExist(string m_PathFolder, string m_FileName)
    {
        return GetCheckFileExist(GetPathApplicationResourcemFile(m_PathFolder, m_FileName));
    }

    public static bool GetCheckFileApplicationPersistentExist(string m_PathFolder, string m_FileName)
    {
        return GetCheckFileExist(GetPathApplicationPersistentFile(m_PathFolder, m_FileName));
    }

    #endregion

    #region ================================================================== File Custom

    #region File Custom Write 

    private string m_TextWrite = "";

    public void SetWriteDataClear()
    {
        m_TextWrite = "";
    }

    #region File Custom Write Start

    public static void SetWriteDatatoFile(string m_Path, string m_Data)
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

    public void SetWriteDataStart(string m_Path)
    {
        SetWriteDatatoFile(m_Path, GetWriteDataString());
    }

    public void SetWriteDataResourceStart(string m_PathFolder, string m_FileName)
    {
        SetWriteDataStart(GetPathApplicationResourcemFile(m_PathFolder, m_FileName));
    }

    public void SetWriteDataPersistentStart(string m_PathFolder, string m_FileName)
    {
        SetWriteDataStart(GetPathApplicationPersistentFile(m_PathFolder, m_FileName));
    }

    #endregion

    #region File Custom Write Set Data

    public void SetWriteDataAdd(string m_DataAdd)
    {
        if (m_TextWrite.Length != 0)
        {
            m_TextWrite += "\n";
        }

        m_TextWrite += m_DataAdd;
    }

    public void SetWriteDataAdd(int m_DataAdd)
    {
        if (m_TextWrite.Length != 0)
        {
            m_TextWrite += "\n";
        }

        m_TextWrite += m_DataAdd.ToString();
    }

    public void SetWriteDataAdd(float m_DataAdd)
    {
        if (m_TextWrite.Length != 0)
        {
            m_TextWrite += "\n";
        }

        m_TextWrite += m_DataAdd.ToString();
    }

    public void SetWriteDataAdd(double m_DataAdd)
    {
        if (m_TextWrite.Length != 0)
        {
            m_TextWrite += "\n";
        }

        m_TextWrite += m_DataAdd.ToString();
    }

    public void SetWriteDataAdd(bool m_DataAdd)
    {
        if (m_TextWrite.Length != 0)
        {
            m_TextWrite += "\n";
        }

        m_TextWrite += ((m_DataAdd) ? "True" : "False");
    }

    public void SetWriteDataAdd(Vector2 m_DataAdd, char m_Key)
    {
        SetWriteDataAdd(ClassString.GetDataVector2Encypt(m_DataAdd, m_Key));
    }

    public void SetWriteDataAdd(Vector2Int m_DataAdd, char m_Key)
    {
        SetWriteDataAdd(ClassString.GetDataVector2IntEncypt(m_DataAdd, m_Key));
    }

    public void SetWriteDataAdd(Vector3 m_DataAdd, char m_Key)
    {
        SetWriteDataAdd(ClassString.GetDataVector3Encypt(m_DataAdd, m_Key));
    }

    public void SetWriteDataAdd(Vector3Int m_DataAdd, char m_Key)
    {
        SetWriteDataAdd(ClassString.GetDataVector3IntEncypt(m_DataAdd, m_Key));
    }

    #endregion

    #region File Custom Write Get String

    public string GetWriteDataString()
    {
        return m_TextWrite;
    }

    #endregion

    #endregion

    #region File Custom Read 

    private List<string> lm_TextRead = new List<string>();
    private int m_ReadRun = -1;

    public void SetReadDataClear()
    {
        lm_TextRead = new List<string>();
        m_ReadRun = -1;
    }

    #region File Custom Read Start

    public static List<string> GetReadDatafromFile(string m_Path)
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

    public void SetReadDataStart(string m_Path)
    {
        lm_TextRead = GetReadDatafromFile(m_Path);
    }

    public void SetReadDataPersistentStart(string m_PathFolder, string m_FileName)
    {
        SetReadDataStart(GetPathApplicationPersistentFile(m_PathFolder, m_FileName));
    }

    public void SetReadDataResourceStart(string m_PathFolder, string m_FileName)
    {
        //Debug.Log("SetReadDataStart: " + m_PathFolder + (m_PathFolder.Equals("") ? "" : (@"\")) + m_FileName);

        TextAsset t_TextFile = (TextAsset)Resources.Load(
            m_PathFolder + (m_PathFolder.Equals("") ? "" : (@"\")) + m_FileName,
            typeof(TextAsset));

        lm_TextRead = ClassString.GetStrinm_SplitList(t_TextFile.text, '\n');
    }

    #endregion

    #region File Custom Read Get Data

    public string GetReadDataAutoString()
    {
        m_ReadRun++;
        return lm_TextRead[m_ReadRun];
    }

    public int GetReadDataAutoInt()
    {
        m_ReadRun++;
        return int.Parse(lm_TextRead[m_ReadRun]);
    }

    public float GetReadDataAutoFloat()
    {
        m_ReadRun++;
        return float.Parse(lm_TextRead[m_ReadRun]);
    }

    public double GetReadDataAutoDouble()
    {
        m_ReadRun++;
        return double.Parse(lm_TextRead[m_ReadRun]);
    }

    public bool GetCheckReadDataAutoBool()
    {
        m_ReadRun++;
        return lm_TextRead[m_ReadRun] == "True";
    }

    public Vector2 GetReadDataAutoVector2(char m_Key)
    {
        m_ReadRun++;
        return ClassString.GetDataVector2Dencypt(lm_TextRead[m_ReadRun], m_Key);
    }

    public Vector2Int GetReadDataAutoVector2Int(char m_Key)
    {
        m_ReadRun++;
        return ClassString.GetDataVector2IntDencypt(lm_TextRead[m_ReadRun], m_Key);
    }

    public Vector3 GetReadDataAutoVector3(char m_Key)
    {
        m_ReadRun++;
        return ClassString.GetDataVector3Dencypt(lm_TextRead[m_ReadRun], m_Key);
    }

    public Vector3Int GetReadDataAutoVector3Int(char m_Key)
    {
        m_ReadRun++;
        return ClassString.GetDataVector3IntDencypt(lm_TextRead[m_ReadRun], m_Key);
    }

    public int GetReadDataAutoCurrent()
    {
        return m_ReadRun;
    }

    public bool CheckGetReadDataAutoEnd()
    {
        return GetReadDataAutoCurrent() >= GetReadDataList().Count - 1;
    }

    #endregion

    #region File Custom Read Get List

    public List<string> GetReadDataList()
    {
        return lm_TextRead;
    }

    #endregion

    #endregion

    #endregion

    #region ================================================================== File Json

    //NOTE:
    //Type "TextAsset" is a "Text Document" File or "*.txt" File

    //SAMPLE:
    //ClassData m_yData = ClassFileIO.GetDatafromJson<ClassData>(m_JsonDataTextDocument);

    public static ClassData GetDataJson<ClassData>(TextAsset m_JsonDataTextDocument)
    {
        return GetDataJson<ClassData>(m_JsonDataTextDocument.text);
    }

    public static ClassData GetDataJson<ClassData>(string m_JsonData)
    {
        return JsonUtility.FromJson<ClassData>(m_JsonData);
    }

    public static string GetDataJson(object m_JsonDataClass)
    {
        return JsonUtility.ToJson(m_JsonDataClass);
    }

    public static void SetDataJsonFile(string m_Path, object m_JsonDataClass)
    {
        SetWriteDatatoFile(m_Path, GetDataJson(m_JsonDataClass));
    }

    #endregion
}