using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

/*
 * Android Run Circle Life:
 * - onCreate()      | 
 * - onStart()       |
 * - onResume()      | OnApplicationPause(false)
 * - ActivityRunning |
 * - onPause()       | OnApplicationPause(true)
 * - onStop()        |
 * - onDestroy()     | OnApplicationQuit()
 */

public class Socket_ClientManager : MonoBehaviour
{
    #region Public

    /// <summary>
    /// Tag with Host
    /// </summary>
    [Header("Network Host Server")]
    [SerializeField]
    private string m_Tag_Host = "SocketHost";

    /// <summary>
    /// Host to Connect
    /// </summary>
    [SerializeField]
    private InputField inp_Host;

    /// <summary>
    /// Tag with Port
    /// </summary>
    [Header("Network Port Server")]
    [SerializeField]
    private string m_Tag_Port = "SocketPort";

    /// <summary>
    /// Port for Connect to Host
    /// </summary>
    [SerializeField]
    private InputField inp_Port;

    /// <summary>
    /// Auto Connect on Start
    /// </summary>
    [Header("Network On Start")]
    [SerializeField]
    private bool m_AutoConnect = true;

    /// <summary>
    /// Host to Connect or Connected
    /// </summary>
    [SerializeField]
    private string m_HostConnect = "192.168.100.38";

    /// <summary>
    /// Port to Connect or Connected
    /// </summary>
    [SerializeField]
    private string m_PortConnect = "5000";

    /// <summary>
    /// Auto Read by Thread
    /// </summary>
    [SerializeField]
    private bool m_AutoRead = true;

    [Header("Socket Message")]
    [SerializeField]
    private List<Text> lt_SocketMessage;

    [SerializeField]
    private string m_ConnectSuccess = "Connect Success!";

    [SerializeField]
    private string m_ConnectFailed = "Connect Failed!";

    #endregion

    #region Private

    //Socket

    /// <summary>
    /// IP on this Device or this Server
    /// </summary>
    private readonly string m_LocalHost = "localhost";

    /// <summary>
    /// Socket Connect OK?
    /// </summary>
    private bool m_SocketStart = false;

    /// <summary>
    /// Socket Auto Thread Read?
    /// </summary>
    private bool m_SocketRead = false;

    private TcpClient tcp_Socket;
    private NetworkStream net_Stream;
    private StreamWriter st_Writer;
    private StreamReader st_Reader;

    //Data

    private Thread th_GetData;

    [Header("Network Auto Read")]
    [SerializeField]
    private List<string> l_DataQueue;

    #endregion

    private void Start()
    {
        if (inp_Host == null)
        {
            inp_Host = GameObject.FindGameObjectWithTag(m_Tag_Host).GetComponent<InputField>();
        }
        if (inp_Host != null)
        {
            if (inp_Host.text == "")
            {
                inp_Host.text = m_HostConnect;
            }
        }

        if (inp_Port == null)
        {
            inp_Port = GameObject.FindGameObjectWithTag(m_Tag_Port).GetComponent<InputField>();
        }
        if (inp_Port != null)
        {
            if (inp_Port.text == "")
            {
                inp_Port.text = m_PortConnect;
            }
        }

        l_DataQueue = new List<string>();

        if (m_AutoConnect)
        {
            SetSocket_Start();
        }

        if (m_AutoRead)
        {
            SetSocketThread_Read(true);
        }

        th_GetData = new Thread(SetSocketThread_AutoRead);
        th_GetData.Start();
    }

    private void OnDestroy()
    {
        SetCloseApplication();
    }

    private void OnApplicationQuit()
    {
        SetCloseApplication();
    }

    private void OnApplicationPause(bool m_OnPause)
    {
        //Android Event onResume() and onPause()
        if (m_OnPause)
        {
            SetCloseApplication();
        }
        else
        {
            SetSocket_Start();
        }
    }

    private void SetCloseApplication()
    {
        if (th_GetData != null)
        {
            if (th_GetData.IsAlive)
            {
                th_GetData.Abort();
            }
        }

        SetSocket_Close();
    }

    #region Thread Read Data

    /// <summary>
    /// Auto Read Data for Debug
    /// </summary>
    private void SetSocketThread_AutoRead()
    {
        while (true)
        {
            if (m_SocketStart)
            //If Socket Started
            {
                if (m_SocketRead)
                //If Socket Read
                {
                    string m_DataGet = GetSocketData_Read();
                    if (m_DataGet != "")
                    {
                        //Debug.Log("SetThread_AutoRead: " + m_DataGet);
                        l_DataQueue.Add(m_DataGet);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Set Socket Read by Thread
    /// </summary>
    /// <param name="m_SocketRead"></param>
    public void SetSocketThread_Read(bool m_SocketRead)
    {
        this.m_SocketRead = m_SocketRead;
    }

    /// <summary>
    /// Get Socket Read by Thread
    /// </summary>
    /// <returns></returns>
    public bool GetSocketThread_Read()
    {
        return m_SocketRead;
    }

    #endregion

    #region Start Connect to Server

    /// <summary>
    /// Set Socket Ready
    /// </summary>
    public void SetSocket_Start()
    {
        if (!GetSocket_Start())
        {
            try
            {
                tcp_Socket = new TcpClient();

                if (inp_Port == null || inp_Port == null)
                {
                    Debug.LogError("SetSocket_Start: Require Input Field!");
                    return;
                }
                else
                {
                    if (inp_Port.text == "")
                    {
                        Debug.LogWarning("SetSocket_Start: Port Require!");
                        return;
                    }

                    if (inp_Host.text == "")
                    {
                        Debug.LogWarning("SetSocket_Start: Local Host Instead!");
                        Debug.LogWarning("SetSocket_Start: Device " + SystemInfo.deviceUniqueIdentifier);
                        tcp_Socket.Connect(m_LocalHost, int.Parse(inp_Port.text));
                    }
                    else
                    {
                        Debug.LogWarning("SetSocket_Start: Host " + inp_Host.text);
                        Debug.LogWarning("SetSocket_Start: Device " + SystemInfo.deviceUniqueIdentifier);
                        tcp_Socket.Connect(inp_Host.text, int.Parse(inp_Port.text));
                    }
                }

                net_Stream = tcp_Socket.GetStream();
                net_Stream.ReadTimeout = 1;
                st_Writer = new StreamWriter(net_Stream);
                st_Reader = new StreamReader(net_Stream);

                m_HostConnect = inp_Host.text;
                m_PortConnect = inp_Port.text;

                m_SocketStart = true;

                Debug.LogWarning("SetSocket_Start: Socket Start!");

                for (int i = 0; i < lt_SocketMessage.Count; i++)
                {
                    lt_SocketMessage[i].text = m_ConnectSuccess;
                }

            }
            catch (Exception e)
            {
                Debug.LogError("SetSocket_Start: Socket error '" + e + "'");

                for (int i = 0; i < lt_SocketMessage.Count; i++)
                {
                    lt_SocketMessage[i].text = m_ConnectFailed;
                }
            }
        }
    }

    /// <summary>
    /// Socket is Started?
    /// </summary>
    /// <returns></returns>
    public bool GetSocket_Start()
    {
        return m_SocketStart;
    }

    #endregion

    #region Write Data to Server

    /// <summary>
    /// Sent Data to Server
    /// </summary>
    /// <param name="m_Data"></param>
    public void SetSocket_Write(string m_Data)
    {
        if (!GetSocket_Start())
        {
            return;
        }

        string foo = m_Data + "\r\n";
        st_Writer.Write(foo);
        st_Writer.Flush();

        Debug.Log("SetSocket_Write: " + m_Data);
    }

    #endregion

    #region Read Data from Server

    //Socket

    /// <summary>
    /// Get Data from Server
    /// </summary>
    /// <remarks>
    /// Should use this in 'void FixedUpdate()' or use with 'Thread'
    /// </remarks>
    /// <returns></returns>
    private string GetSocketData_Read()
    {
        if (!GetSocket_Start())
        {
            return "";
        }
        if (net_Stream.DataAvailable)
        {
            string m_ReadData = st_Reader.ReadLine();
            Debug.Log("GetSocket_Read: " + m_ReadData);
            return m_ReadData;
        }
        return "";
    }

    //Queue

    /// <summary>
    /// Get Data from Queue List
    /// </summary>
    /// <returns></returns>
    public string GetSocketQueue_Read()
    {
        if (GetSocketQueueCount() <= 0)
        {
            return "";
        }
        string m_DataGet = l_DataQueue[0];
        l_DataQueue.RemoveAt(0);
        return m_DataGet;
    }

    /// <summary>
    /// Get Data Exist from Queue List
    /// </summary>
    /// <returns></returns>
    public int GetSocketQueueCount()
    {
        return l_DataQueue.Count;
    }

    #endregion

    #region Close Connect

    /// <summary>
    /// Close Connect to Server
    /// </summary>
    public void SetSocket_Close()
    {
        if (!GetSocket_Start())
        {
            return;
        }

        SetSocket_Write("Exit");
        st_Writer.Close();
        st_Reader.Close();
        tcp_Socket.Close();
        m_SocketStart = false;

        Debug.LogWarning("SetSocket_Close: Called!");
    }

    #endregion

    /// <summary>
    /// Get ID of this Device
    /// </summary>
    /// <returns></returns>
    public string GetDeviceID()
    {
        return SystemInfo.deviceUniqueIdentifier;
    }
}