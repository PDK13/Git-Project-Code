using UnityEngine;
using UnityEngine.UI;

public class Sample_FileIO : MonoBehaviour
{
    //This Script can work both on WINDOW and ANDROID

    public InputField m_Send;
    public Text t_Receive;

    public Text t_Error;

    private Clasm_FileIO cl_File;

    private string m_LinkFile = "";

    private void Start()
    {
        cl_File = new Clasm_FileIO();

        m_LinkFile = Clasm_FileIO.GetPath_Application_Persistent() + Clasm_FileIO.GetPath_File("HelloWorld", "", "txt");

        //m_LinkFile = Clasm_FileIO.GetPath_File_WriteToResources("GameSaved", "HelloWorld");
    }

    public void Button_Send()
    {
        try
        {
            cl_File.SetData_Write_Clear();

            cl_File.SetData_Write_Add(m_Send.text);

            cl_File.SetData_Write_Start(m_LinkFile);

            t_Error.text = "WRITE OK: \n" + m_LinkFile;
        }
        catch
        {
            t_Error.text = "WRITE ERROR: \n" + m_LinkFile;
        }
    }

    public void Button_Receive()
    {
        try
        {
            cl_File.SetData_Read_Clear();

            cl_File.SetData_Read_Start(m_LinkFile);

            t_Receive.text = cl_File.GetData_Read_Auto_String();

            t_Error.text = "READ OK: \n" + m_LinkFile;
        }
        catch
        {
            t_Error.text = "READ ERROR: \n" + m_LinkFile;
        }
    }
}
