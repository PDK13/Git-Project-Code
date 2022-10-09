﻿using UnityEngine;
using UnityEngine.UI;

public class Sample_FileIO : MonoBehaviour
{
    //This Script can work both on WINDOW and ANDROID

    public InputField m_Send;
    public Text tReceive;

    public Text t_Error;

    private ClassFileIO cs_File;

    private string m_PathFile = "";

    private void Start()
    {
        cs_File = new ClassFileIO();

        m_PathFile = ClassFileIO.GetPath(FileIOPathType.Document, "HelloWorld");

        //m_PathFile = ClassFileIO.GetPathFileWriteToResources("GameSaved", "HelloWorld");
    }

    public void Button_Send()
    {
        try
        {
            cs_File.SetWriteDataClear();

            cs_File.SetWriteDataAdd(m_Send.text);

            cs_File.SetWriteDataStart(m_PathFile);

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
            cs_File.SetReadDataClear();

            cs_File.SetReadDataStart(m_PathFile);

            tReceive.text = cs_File.GetReadDataAutoString();

            t_Error.text = "READ OK: \n" + m_PathFile;
        }
        catch
        {
            t_Error.text = "READ ERROR: \n" + m_PathFile;
        }
    }
}
