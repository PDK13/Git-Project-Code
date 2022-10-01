using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IsoBlock))]
public class IsoBlockMessage : MonoBehaviour
{
    [Header("Message")]

    [SerializeField]
    private bool m_Message_Lock = false;

    [SerializeField]
    private bool m_Message_Loop = false;

    [Tooltip("Message List")]
    private List<IsoDataMessageSingle> l_Message;

    private void Awake()
    {
        l_Message = new List<IsoDataMessageSingle>();
    }

    #region Message Add

    public void Set_Add(IsoDataMessageSingle cl_Message_Single)
    {
        l_Message.Add(cl_Message_Single);
    }

    public void Set_List(List<IsoDataMessageSingle> l_Message_List)
    {
        l_Message = l_Message_List;
    }

    #endregion

    #region Message Chance

    public void Set_Chance(int i_Message_Index, IsoDataMessageSingle cl_Message_Single)
    {
        if (i_Message_Index > GetCount() - 1)
        {
            return;
        }

        l_Message[i_Message_Index] = cl_Message_Single;
    }

    #endregion

    #region Message Remove

    public void Set_Remove(int i_Message_Index)
    {
        l_Message.RemoveAt(i_Message_Index);
    }

    public void Set_Remove_Lastest()
    {
        Set_Remove(GetCount() - 1);
    }

    #endregion

    #region Message Get

    public int GetCount()
    {
        return l_Message.Count;
    }

    public IsoDataMessageSingle GetList(int i_Message_Index)
    {
        return l_Message[i_Message_Index];
    }

    public List<IsoDataMessageSingle> GetList()
    {
        return l_Message;
    }

    #endregion

    #region Message Lock

    public void Set_Lock(bool m_Message_Lock)
    {
        if (GetLoop() && m_Message_Lock)
        {
            //Debug.LogWarning("Loop Current Active, so can not Lock this Message!");

            this.m_Message_Lock = false;
        }
        else
        {
            this.m_Message_Lock = m_Message_Lock;
        }
    }

    public bool GetLock()
    {
        return m_Message_Lock;
    }

    #endregion

    #region Message Loop

    public void Set_Loop(bool m_Message_Loop)
    {
        this.m_Message_Loop = m_Message_Loop;
    }

    public bool GetLoop()
    {
        return m_Message_Loop;
    }

    #endregion
}
