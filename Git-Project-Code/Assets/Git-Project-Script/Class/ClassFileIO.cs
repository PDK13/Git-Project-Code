using System;
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

    #region ================================================================== File IO Path 

    public static string GetPath(FileIOPathType m_FileIOPathType, string m_FileIOName, params string[] m_FileIOPath)
    {
        string m_Path = "";

        for (int i = 0; i < m_FileIOPath.Length; i++)
        {
            m_Path += m_FileIOPath[i] + @"\";
        }

        m_Path += m_FileIOName + ".txt";

        switch (m_FileIOPathType)
        {
            case FileIOPathType.Persistent:
                m_Path = Application.persistentDataPath + @"\" + m_Path;
                break;
            case FileIOPathType.Resources:
                m_Path = Application.dataPath + @"\resources\" + m_Path;
                break;
            case FileIOPathType.Document:
                m_Path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\" + m_Path;
                break;
            case FileIOPathType.Picture:
                m_Path = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + @"\" + m_Path;
                break;
            case FileIOPathType.Music:
                m_Path = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic) + @"\" + m_Path;
                break;
            case FileIOPathType.Video:
                m_Path = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos) + @"\" + m_Path;
                break;
        }

        Debug.LogFormat("Get Path: {0}", m_Path);
        return m_Path;
    }

    public static bool GetCheckPathExist(string m_Path)
    {
        return File.Exists(m_Path);
    }

    #endregion

    #region ================================================================== File IO Write 

    private string m_TextWrite = "";

    private void SetWriteDatatoFile(string m_Path, string m_Data)
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

    public void SetWriteDataClear()
    {
        m_TextWrite = "";
    }

    public void SetWriteDataStart(string m_Path)
    {
        SetWriteDatatoFile(m_Path, GetWriteDataString());
    } //Call Last

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

    public string GetWriteDataString()
    {
        return m_TextWrite;
    }

    #endregion

    #region ================================================================== File IO Read 

    private List<string> lm_TextRead = new List<string>();
    private int m_ReadRun = -1;

    private List<string> GetReadDatafromFile(string m_Path)
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

    public void SetReadDataClear()
    {
        lm_TextRead = new List<string>();
        m_ReadRun = -1;
    }

    public void SetReadDataStart(string m_Path)
    {
        lm_TextRead = GetReadDatafromFile(m_Path);
    } //Call First

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

    public List<string> GetReadDataList()
    {
        return lm_TextRead;
    }

    #endregion

    #region ================================================================== File Json

    //NOTE:
    //Type "TextAsset" is a "Text Document" File or "*.txt" File

    //SAMPLE:
    //ClassData m_Data = ClassFileIO.GetDatafromJson<ClassData>(m_JsonDataTextDocument);

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

    #endregion
}

public enum FileIOPathType
{
    None        = 0,
    Persistent  = 1,
    Resources   = 2,
    Document    = 3,
    Picture     = 4,
    Music       = 5,
    Video       = 6
}