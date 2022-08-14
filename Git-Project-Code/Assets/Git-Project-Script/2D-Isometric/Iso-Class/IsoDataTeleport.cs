using UnityEngine;

public class IsoDataTeleport
{
    [Tooltip("Block")]
    private IsoDataBlock cl_Block;

    [Tooltip("World Name for Teleport")]
    private string s_WorldName = "";

    [Tooltip("Pos for Teleport")]
    private Vector3Int v3_Pos_Teleport;

    public IsoDataTeleport(IsoDataBlock cl_Block, string s_WorldName, Vector3Int v3_Teleport_Pos)
    {
        Set_Block(cl_Block);

        Set_Add(s_WorldName, v3_Teleport_Pos);
    }

    public IsoDataTeleport(IsoDataBlock cl_Block)
    {
        Set_Block(cl_Block);
    }

    public IsoDataTeleport(GameObject g_Teleport)
    {
        Set_Block(new IsoDataBlock(g_Teleport));

        Set_Add(
            g_Teleport.GetComponent<IsoBlockTeleport>().Get_World_Name(),
            g_Teleport.GetComponent<IsoBlockTeleport>().Get_Spawm_Pos());
    }

    #region Block 

    public void Set_Block(IsoDataBlock cl_Block)
    {
        this.cl_Block = cl_Block;
    }

    public IsoDataBlock Get_Block()
    {
        return cl_Block;
    }

    #endregion

    #region Teleport 

    public void Set_Add(string s_WorldName, Vector3Int v3_Pos_Teleport)
    {
        this.s_WorldName = s_WorldName;
        this.v3_Pos_Teleport = v3_Pos_Teleport;
    }

    public void Set_Add(IsoDataTeleport cl_Data_Teleport)
    {
        Set_Add(cl_Data_Teleport.Get_World_Name(), cl_Data_Teleport.Get_Pos());
    }

    public void Set_Pos_Add(int i_Pos_X_Add, int i_Pos_Y_Add, int i_Pos_High_Add)
    {
        v3_Pos_Teleport.x += i_Pos_X_Add;
        v3_Pos_Teleport.y += i_Pos_Y_Add;
        v3_Pos_Teleport.z += i_Pos_High_Add;
    }

    public void Set_Remove()
    {
        s_WorldName = "";
        v3_Pos_Teleport = new Vector3Int();
    }

    public bool Get_isExist()
    {
        return Get_World_Name() != "";
    }

    public string Get_World_Name()
    {
        return s_WorldName;
    }

    public Vector3Int Get_Pos()
    {
        return v3_Pos_Teleport;
    }

    #endregion
}
