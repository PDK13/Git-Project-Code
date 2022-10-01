using System.Collections.Generic;
using UnityEngine;

public class IsoDataMove
{
    [Tooltip("Block")]
    private IsoDataBlock cl_Data_Block;

    [Tooltip("Move List")]
    private List<IsoDataMoveSingle> l_Data_Move = new List<IsoDataMoveSingle>();

    [Tooltip("Move Status Active")]
    private int i_Data_Status_Numberic = -1;

    public IsoDataMove(IsoDataBlock cl_Data_Block)
    {
        Set_Block(cl_Data_Block);
    }

    public IsoDataMove(GameObject g_Move)
    {
        Set_Block(g_Move);

        Set_List(g_Move.GetComponent<IsoBlockMove>().GetList());
        Set_Status_Numberic(g_Move.GetComponent<IsoBlockMove>().GetStatusIsActive_Numberic());
    }

    #region Block 

    public void Set_Block(IsoDataBlock cl_Data_Block)
    {
        this.cl_Data_Block = cl_Data_Block;
    }

    public void Set_Block(GameObject g_Move)
    {
        Set_Block(new IsoDataBlock(
            g_Move.GetComponent<IsoBlock>().GetPosOnMatrix_Primary(),
            g_Move.GetComponent<IsoBlock>().GetName()));
    }

    public IsoDataBlock GetBlock()
    {
        return cl_Data_Block;
    }

    #endregion

    #region Move 

    public void Set_List(List<IsoDataMoveSingle> l_Move_List)
    {
        l_Data_Move = l_Move_List;
    }

    public void Set_Add(IsoDataMoveSingle cl_Move_Single)
    {
        l_Data_Move.Add(cl_Move_Single);
    }

    public int GetCount()
    {
        if (l_Data_Move == null)
        {
            l_Data_Move = new List<IsoDataMoveSingle>();

            return 0;
        }

        return l_Data_Move.Count;
    }

    public IsoDataMoveSingle GetList(int i_Move_Index)
    {
        if (i_Move_Index < 0 || i_Move_Index >= GetCount())
        {
            return null;
        }

        return l_Data_Move[i_Move_Index];
    }

    public List<IsoDataMoveSingle> GetList()
    {
        return l_Data_Move;
    }

    public void Set_Status_Numberic(int i_Status_Numberic)
    {
        i_Data_Status_Numberic = i_Status_Numberic;
    }

    public int GetStatus_Numberic()
    {
        return i_Data_Status_Numberic;
    }

    #endregion
}
