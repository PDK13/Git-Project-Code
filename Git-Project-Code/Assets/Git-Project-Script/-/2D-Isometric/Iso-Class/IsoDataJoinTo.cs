using UnityEngine;

public class IsoDataJoinTo
{
    [Tooltip("Block")]
    private IsoDataBlock cl_Block;

    [Tooltip("Pos Join-To")]
    private Vector3Int v3_JoinTo_Pos;

    public IsoDataJoinTo(IsoDataBlock cl_Data_Block)
    {
        Set_Block(cl_Data_Block);
    }

    public IsoDataJoinTo(GameObject g_Join)
    {
        Set_Block(new IsoDataBlock(g_Join));

        Set_JoinToBlock_Pos(g_Join.GetComponent<IsoBlockJoinTo>().GetJoinTo_Pos_Primary());
    }

    #region Block 

    public void Set_Block(IsoDataBlock cl_Block)
    {
        this.cl_Block = cl_Block;
    }

    public IsoDataBlock GetBlock()
    {
        return cl_Block;
    }

    #endregion

    #region Join-To 

    public void Set_JoinToBlock_Pos(Vector3Int v3_JoinTo_Pos)
    {
        this.v3_JoinTo_Pos = v3_JoinTo_Pos;
    }

    public Vector3Int GetJoinTo_Pos()
    {
        return v3_JoinTo_Pos;
    }

    public void Set_Pos_JoinTo_Add(int i_JoinTo_Pos_X_Add, int i_JoinTo_Pos_Y_Add, int i_JoinTo_Pos_High_Add)
    {
        v3_JoinTo_Pos.x += i_JoinTo_Pos_X_Add;
        v3_JoinTo_Pos.y += i_JoinTo_Pos_Y_Add;
        v3_JoinTo_Pos.z += i_JoinTo_Pos_High_Add;
    }

    #endregion
}
