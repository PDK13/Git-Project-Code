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
    private string m_Tam_Host = "SocketHost";

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
    private string m_Tam_Port = "SocketPort";

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
    private bool mAllowAutoConnect = true;

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
    private bool mAllowAutoRead = true;

    [Header("Socket m_essage")]
    [SerializeField]
    private List<Text> m_SocketMessage;

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
    private bool mAllowSocketStart = false;

    /// <summary>
    /// Socket Auto Thread Read?
    /// </summary>
    private bool mAllowSocketRead = false;

    private TcpClient tcp_Socket;
    private NetworkStream net_Stream;
    private StreamWriter st_Writer;
    private StreamReader stReader;

    //Data

    private Thread m_GetData;

    [Header("Network Auto Read")]
    [SerializeField]
    private List<string> m_DataQueue;

    #endregion

    private void Start()
    {
        if (inp_Host == null)
        {
            inp_Host = GameObject.FindGameObjectWithTag(m_Tam_Host).GetComponent<InputField>();
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
            inp_Port = GameObject.FindGameObjectWithTag(m_Tam_Port).GetComponent<InputField>();
        }
        if (inp_Port != null)
        {
            if (inp_Port.text == "")
            {
                inp_Port.text = m_PortConnect;
            }
        }

        m_DataQueue = new List<string>();

        if (mAllowAutoConnect)
        {
            SetSocketStart();
        }

        if (mAllowAutoRead)
        {
            SetSocketThreadRead(true);
        }

        m_GetData = new Thread(SetSocketThread_AutoRead);
        m_GetData.Start();
    }

    private void OnDestroy()
    {
        SetCloseApplication();
    }

    private void OnApplicationQuit()
    {
        SetCloseApplication();
    }

    private void OnApplicationPause(bool mAllowOnPause)
    {
        //Android Event onResume() and onPause()
        if (mAllowOnPause)
        {
            SetCloseApplication();
        }
        else
        {
            SetSocketStart();
        }
    }

    private void SetCloseApplication()
    {
        if (m_GetData != null)
        {
            if (m_GetData.IsAlive)
            {
                m_GetData.Abort();
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
            if (mAllowSocketStart)
            //If Socket Started
            {
                if (mAllowSocketRead)
                //If Socket Read
                {
                    string m_DataGet = GetSocketReadData();
                    if (m_DataGet != "")
                    {
                        //Debug.Log("SetThread_AutoRead: " + m_DataGet);
                        m_DataQueue.Add(m_DataGet);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Set Socket Read by Thread
    /// </summary>
    /// <param name="m_SocketRead"></param>
    public void SetSocketThreadRead(bool mAllowSocketRead)
    {
        this.mAllowSocketRead = mAllowSocketRead;
    }

    /// <summary>
    /// Get Socket Read by Thread
    /// </summary>
    /// <returns></returns>
    public bool GetCheckSocketThreadRead()
    {
        return mAllowSocketRead;
    }

    #endregion

    #region Start Connect to Server

    /// <summary>
    /// Set Socket Ready
    /// </summary>
    public void SetSocketStart()
    {
        if (!GetCheckSocketStart())
        {
            try
            {
                tcp_Socket = new TcpClient();

                if (inp_Port == null || inp_Port == null)
                {
                    Debug.LogError("SetSocketStart: Require Input Field!");
                    return;
                }
                else
                {
                    if (inp_Port.text == "")
                    {
                        Debug.LogWarning("SetSocketStart: Port Require!");
                        return;
                    }

                    if (inp_Host.text == "")
                    {
                        Debug.LogWarning("SetSocketStart: Local Host Instead!");
                        Debug.LogWarning("SetSocketStart: Device " + SystemInfo.deviceUniqueIdentifier);
                        tcp_Socket.Connect(m_LocalHost, int.Parse(inp_Port.text));
                    }
                    else
                    {
                        Debug.LogWarning("SetSocketStart: Host " + inp_Host.text);
                        Debug.LogWarning("SetSocketStart: Device " + SystemInfo.deviceUniqueIdentifier);
                        tcp_Socket.Connect(inp_Host.text, int.Parse(inp_Port.text));
                    }
                }

                net_Stream = tcp_Socket.GetStream();
                net_Stream.ReadTimeout = 1;
                st_Writer = new StreamWriter(net_Stream);
                stReader = new StreamReader(net_Stream);

                m_HostConnect = inp_Host.text;
                m_PortConnect = inp_Port.text;

                mAllowSocketStart = true;

                Debug.LogWarning("SetSocketStart: Socket Start!");

                for (int i = 0; i < m_SocketMessage.Count; i++)
                {
                    m_SocketMessage[i].text = m_ConnectSuccess;
                }

            }
            catch (Exception e)
            {
                Debug.LogError("SetSocketStart: Socket error '" + e + "'");

                for (int i = 0; i < m_SocketMessage.Count; i++)
                {
                    m_SocketMessage[i].text = m_ConnectFailed;
                }
            }
        }
    }

    /// <summary>
    /// Socket is Started?
    /// </summary>
    /// <returns></returns>
    public bool GetCheckSocketStart()
    {
        return mAllowSocketStart;
    }

    #endregion

    #region Write Data to Server

    /// <summary>
    /// Sent Data to Server
    /// </summary>
    /// <param name="mData"></param>
    public void SetSocket_Write(string m_Data)
    {
        if (!GetCheckSocketStart())
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
    private string GetSocketReadData()
    {
        if (!GetCheckSocketStart())
        {
            return "";
        }
        if (net_Stream.DataAvailable)
        {
            string m_ReadData = stReader.ReadLine();
            Debug.Log("GetSocketRead: " + m_ReadData);
            return m_ReadData;
        }
        return "";
    }

    //Queue

    /// <summary>
    /// Get Data from Queue List
    /// </summary>
    /// <returns></returns>
    public string GetSocketQueueRead()
    {
        if (GetSocketQueueCount() <= 0)
        {
            return "";
        }
        string m_DataGet = m_DataQueue[0];
        m_DataQueue.RemoveAt(0);
        return m_DataGet;
    }

    /// <summary>
    /// Get Data Exist from Queue List
    /// </summary>
    /// <returns></returns>
    public int GetSocketQueueCount()
    {
        return m_DataQueue.Count;
    }

    #endregion

    #region Close Connect

    /// <summary>
    /// Close Connect to Server
    /// </summary>
    public void SetSocket_Close()
    {
        if (!GetCheckSocketStart())
        {
            return;
        }

        SetSocket_Write("Exit");
        st_Writer.Close();
        stReader.Close();
        tcp_Socket.Close();
        mAllowSocketStart = false;

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