using UnityEngine;

public class IsoDataBlock
{
    #region Private

    [Tooltip("Pos on World")]
    private Vector3Int v3_Pos;

    [Tooltip("Name Origin on World")]
    private string s_Name_Origin;

    [Tooltip("Type of Block")]
    private string s_Type;

    [Tooltip("Dir of Block")]
    private string s_Dir;

    #endregion

    public IsoDataBlock(Vector3Int v3_Pos, string s_Name_Origin)
    {
        Set_Block_Chance(v3_Pos, s_Name_Origin);
    }

    public IsoDataBlock(GameObject g_Block)
    {
        Set_Block_Chance(g_Block);
    }

    public IsoDataBlock(string s_Name_Origin, string s_Type, string s_Dir)
    {
        Set_NameOrigin_Chance(s_Name_Origin);
        Set_Type_Chance(s_Type);
        Set_Dir_Chance(s_Dir);
    }

    #region Block 

    public void Set_Block_Chance(Vector3Int v3_Pos, string s_Name_Origin)
    {
        Set_Pos_Chance(v3_Pos);
        Set_NameOrigin_Chance(s_Name_Origin);
    }

    public void Set_Block_Chance(GameObject g_Block)
    {
        Set_Block_Chance(
            g_Block.GetComponent<IsoBlock>().Get_PosOnMatrix_Primary(),
            g_Block.GetComponent<IsoBlock>().Get_Name());
    }

    #endregion

    #region Pos 

    public void Set_Pos_Chance(Vector3Int v3_Pos)
    {
        this.v3_Pos = v3_Pos;
    }

    public Vector3Int Get_Pos()
    {
        return v3_Pos;
    }

    public void Set_Pos_Add(int i_Pos_X_Add, int i_Pos_Y_Add, int i_Pos_High_Add)
    {
        v3_Pos.x += i_Pos_X_Add;
        v3_Pos.y += i_Pos_Y_Add;
        v3_Pos.z += i_Pos_High_Add;
    }

    #endregion

    #region Name 

    public void Set_NameOrigin_Chance(string s_Name_Origin)
    {
        this.s_Name_Origin = s_Name_Origin;
    }

    public string Get_NameOrigin()
    {
        return s_Name_Origin;
    }

    #endregion

    #region Type and Dir 

    public void Set_Type_Chance(string s_Type)
    {
        this.s_Type = s_Type;
    }

    public void Set_Dir_Chance(string s_Dir)
    {
        this.s_Dir = s_Dir;
    }

    #endregion
}
