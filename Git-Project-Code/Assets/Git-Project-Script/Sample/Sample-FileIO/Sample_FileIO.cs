using UnityEngine;
using UnityEngine.UI;

public class Sample_FileIO : MonoBehaviour
{
    //This Script can work both on WINDOW and ANDROID

    public InputField m_Send;
    public Text tReceive;

    public Text t_Error;

    private GitFileIO m_File;

    private string m_PathFile = "";

    private void Start()
    {
        m_File = new GitFileIO();

        m_PathFile = GitFileIO.GetPath(GitPathType.Document, "HelloWorld");

        //m_PathFile = ClassFileIO.GetPathFileWriteToResources("GameSaved", "HelloWorld");
    }

    public void Button_Send()
    {
        try
        {
            m_File.SetWriteDataClear();

            m_File.SetWriteDataAdd(m_Send.text);

            m_File.SetWriteDataStart(m_PathFile);

            t_Error.text = "WRITE OK: \n" + m_PathFile;
        }
        catch
        {
            t_Error.text = "WRITE ERROR: \n" + m_PathFile;
        }
    }

    public void ButtonReceive()
    {
        try
        {
            m_File.SetReadDataClear();

            m_File.SetReadDataStart(m_PathFile);

            tReceive.text = m_File.GetReadDataAutoString();

            t_Error.text = "READ OK: \n" + m_PathFile;
        }
        catch
        {
            t_Error.text = "READ ERROR: \n" + m_PathFile;
        }
    }
}
