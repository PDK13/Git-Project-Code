using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sample_SocketHandle : MonoBehaviour
{
    [Header("Socket Manager")]
    [SerializeField]
    private Socket_ClientManager cs_SocketManager;

    [Header("Debug List")]
    [SerializeField]
    private Text tDebug;

    [SerializeField]
    private List<string> m_ID;

    [SerializeField]
    private List<string> m_Message;

    private int m_Plus = 0;

    private void Start()
    {
        m_ID = new List<string>();
        m_Message = new List<string>();
    }

    private void FixedUpdate()
    {
        //if (cs_SocketManager.GetSocket_QueueReadExist())
        //{
        //    string m_SocketGet = cs_SocketManager.GetSocket_QueueRead();
        //    Debug.Log("Debug:" + m_SocketGet);
        //    //List<string> m_Data = cs_SocketManager.GetSocketData(m_SocketGet);
        //    string[] m_Data = m_SocketGet.Split(':');

        //    string m_ID = m_Data[0];
        //    string m_Command = m_Data[1];
        //    Debug.Log("Debug: " + m_ID + "||" + m_Command);

        //    if (GetExist_ID(m_ID))
        //    {
        //        int m_Index = GetExist_ID_Index(m_ID);
        //        m_Message[m_Index] = m_Command;
        //    }
        //    else
        //    {
        //        m_ID.Add(m_ID);
        //        m_Message.Add(m_Command);
        //        //cs_SocketManager.SetGet(m_ID.Count);
        //    }

        //    string m_Debug = "";
        //    for(int i = 0; i < m_ID.Count; i++)
        //    {
        //        m_Debug += m_ID[i] + ":" + m_Message[i] + "\n";
        //    }
        //}
    }

    public void Button_SendDeviceID()
    {
        m_Plus++;
        cs_SocketManager.SetSocket_Write(cs_SocketManager.GetDeviceID() + ":" + m_Plus.ToString());
    }

    private bool GetCheckExist_ID(string m_IDCheck)
    {
        for (int i = 0; i < m_ID.Count; i++)
        {
            if (m_ID[i] == m_IDCheck)
            {
                return true;
            }
        }
        return false;
    }

    private int GetExist_ID_Index(string m_IDCheck)
    {
        for (int i = 0; i < m_ID.Count; i++)
        {
            if (m_ID[i] == m_IDCheck)
            {
                return i;
            }
        }
        return -1;
    }
}
