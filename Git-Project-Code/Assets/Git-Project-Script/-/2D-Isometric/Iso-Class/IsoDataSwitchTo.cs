using System.Collections.Generic;
using UnityEngine;

public class IsoDataSwitchTo
{
    [Tooltip("Block")]
    private IsoDataBlock cl_Data_Block;

    [Tooltip("Switch List")]
    private List<Vector3Int> l_Data_Switch = new List<Vector3Int>();

    public IsoDataSwitchTo(IsoDataBlock cl_Data_Block)
    {
        Set_Block(cl_Data_Block);
    }

    public IsoDataSwitchTo(GameObject g_Block)
    {
        Set_Block(g_Block);

        Set_List(g_Block.GetComponent<IsoBlockSwitchTo>().GetList());
    }

    #region Block 

    public void Set_Block(IsoDataBlock cl_Data_Block)
    {
        this.cl_Data_Block = cl_Data_Block;
    }

    public void Set_Block(GameObject g_SwitchTo)
    {
        Set_Block(new IsoDataBlock(
            g_SwitchTo.GetComponent<IsoBlock>().GetPosOnMatrix_Primary(),
            g_SwitchTo.GetComponent<IsoBlock>().GetName()));
    }

    public IsoDataBlock GetBlock()
    {
        return cl_Data_Block;
    }

    #endregion

    #region Switch 

    public void Set_List(List<Vector3Int> l_SwitchTo_Pos_LIst)
    {
        l_Data_Switch = l_SwitchTo_Pos_LIst;
    }

    public void Set_Add(Vector3Int v3_SwitchTo_Pos)
    {
        l_Data_Switch.Add(v3_SwitchTo_Pos);
    }

    public void Set_Add(int i_SwitchTo_Index, int i_Pos_X_Add, int i_Pos_Y_Add, int i_Pos_High_Add)
    {
        Vector3Int v3_Pos_Add = new Vector3Int(i_Pos_X_Add, i_Pos_Y_Add, i_Pos_High_Add);

        l_Data_Switch[i_SwitchTo_Index] += v3_Pos_Add;
    }

    public int GetCount()
    {
        return l_Data_Switch.Count;
    }

    public Vector3Int GetList(int i_SwitchTo_Index)
    {
        return l_Data_Switch[i_SwitchTo_Index];
    }

    public List<Vector3Int> GetList()
    {
        return l_Data_Switch;
    }

    #endregion
}
