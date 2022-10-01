using System.Collections.Generic;
using UnityEngine;

public class IsoDataMessage
{
    [Tooltip("Block")]
    private IsoDataBlock cl_Data_Block;

    [Tooltip("Message List")]
    private List<IsoDataMessageSingle> l_Data_Message = new List<IsoDataMessageSingle>();

    public IsoDataMessage()
    {

    }

    public IsoDataMessage(IsoDataBlock cl_Data_Block)
    {
        Set_Block(cl_Data_Block);
    }

    public IsoDataMessage(GameObject g_Block)
    {
        Set_Block(g_Block);

        Set_List(g_Block.GetComponent<IsoBlockMessage>().Get_List());
    }

    #region Block 

    public void Set_Block(IsoDataBlock cl_Data_Block)
    {
        this.cl_Data_Block = cl_Data_Block;
    }

    public void Set_Block(GameObject g_Message)
    {
        Set_Block(new IsoDataBlock(
            g_Message.GetComponent<IsoBlock>().Get_PosOnMatrix_Primary(),
            g_Message.GetComponent<IsoBlock>().Get_Name()));
    }

    public IsoDataBlock Get_Block()
    {
        return cl_Data_Block;
    }

    #endregion

    #region Message List 

    public void Set_List(List<IsoDataMessageSingle> l_Message_List)
    {
        l_Data_Message = l_Message_List;
    }

    public void Set_Add(IsoDataMessageSingle cl_Message_Single)
    {
        l_Data_Message.Add(cl_Message_Single);
    }

    public void Set_Add(List<IsoDataMessageSingle> l_Message_List)
    {
        for (int i = 0; i < l_Message_List.Count; i++)
        {
            l_Data_Message.Add(l_Message_List[i]);
        }
    }

    public void Set_Remove(int i_Message_Index)
    {
        l_Data_Message.RemoveAt(i_Message_Index);
    }

    public void Set_Remove_All()
    {
        l_Data_Message = new List<IsoDataMessageSingle>();
    }

    public int Get_Count()
    {
        if (l_Data_Message == null)
        {
            l_Data_Message = new List<IsoDataMessageSingle>();

            return 0;
        }

        return l_Data_Message.Count;
    }

    public IsoDataMessageSingle Get_List(int i_Message_Index)
    {
        return l_Data_Message[i_Message_Index];
    }

    public List<IsoDataMessageSingle> Get_List()
    {
        return l_Data_Message;
    }

    #endregion

    #region Message List Read

    #endregion
}
