using UnityEngine;
using UnityEngine.UI;

public class Sample_FileIO : MonoBehaviour
{
    //This Script can work both on WINDOW and ANDROID

    public InputField m_Send;
    public Text t_Receive;

    public Text t_Error;

    private Class_FileIO cl_File;

    private string s_LinkFile = "";

    private void Start()
    {
        cl_File = new Class_FileIO();

        s_LinkFile = Class_FileIO.GetPath_Application_Persistent() + Class_FileIO.GetPath_File("HelloWorld", "", "txt");

        //s_LinkFile = Class_FileIO.GetPath_File_WriteToResources("GameSaved", "HelloWorld");
    }

    public void Button_Send()
    {
        try
        {
            cl_File.Set_Data_Write_Clear();

            cl_File.Set_Data_Write_Add(m_Send.text);

            cl_File.Set_Data_Write_Start(s_LinkFile);

            t_Error.text = "WRITE OK: \n" + s_LinkFile;
        }
        catch
        {
            t_Error.text = "WRITE ERROR: \n" + s_LinkFile;
        }
    }

    public void Button_Receive()
    {
        try
        {
            cl_File.Set_Data_Read_Clear();

            cl_File.Set_Data_Read_Start(s_LinkFile);

            t_Receive.text = cl_File.GetData_Read_Auto_String();

            t_Error.text = "READ OK: \n" + s_LinkFile;
        }
        catch
        {
            t_Error.text = "READ ERROR: \n" + s_LinkFile;
        }
    }
}
