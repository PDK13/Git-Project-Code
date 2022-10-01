using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sample_SocketHandle : MonoBehaviour
{
    [Header("Socket Manager")]
    [SerializeField]
    private Socket_ClientManager cl_SocketManager;

    [Header("Debug List")]
    [SerializeField]
    private Text t_Debug;

    [SerializeField]
    private List<string> l_ID;

    [SerializeField]
    private List<string> l_Message;

    private int m_Plus = 0;

    private void Start()
    {
        l_ID = new List<string>();
        l_Message = new List<string>();
    }

    private void FixedUpdate()
    {
        //if (cl_SocketManager.GetSocket_Queue_ReadIsExist())
        //{
        //    string m_SocketGet = cl_SocketManager.GetSocket_Queue_Read();
        //    Debug.Log("Debug:" + m_SocketGet);
        //    //List<string> l_Data = cl_SocketManager.GetSocketData(m_SocketGet);
        //    string[] l_Data = m_SocketGet.Split(':');

        //    string m_ID = l_Data[0];
        //    string m_Command = l_Data[1];
        //    Debug.Log("Debug: " + m_ID + "||" + m_Command);

        //    if (GetExist_ID(m_ID))
        //    {
        //        int m_Index = GetExist_ID_Index(m_ID);
        //        l_Message[m_Index] = m_Command;
        //    }
        //    else
        //    {
        //        l_ID.Add(m_ID);
        //        l_Message.Add(m_Command);
        //        //cl_SocketManager.SetGet(l_ID.Count);
        //    }

        //    string m_Debug = "";
        //    for(int i = 0; i < l_ID.Count; i++)
        //    {
        //        m_Debug += l_ID[i] + ":" + l_Message[i] + "\n";
        //    }
        //}
    }

    public void Button_SendDeviceID()
    {
        m_Plus++;
        cl_SocketManager.SetSocket_Write(cl_SocketManager.GetDeviceID() + ":" + m_Plus.ToString());
    }

    private bool GetExist_ID(string m_IDCheck)
    {
        for (int i = 0; i < l_ID.Count; i++)
        {
            if (l_ID[i] == m_IDCheck)
            {
                return true;
            }
        }
        return false;
    }

    private int GetExist_ID_Index(string m_IDCheck)
    {
        for (int i = 0; i < l_ID.Count; i++)
        {
            if (l_ID[i] == m_IDCheck)
            {
                return i;
            }
        }
        return -1;
    }
}
