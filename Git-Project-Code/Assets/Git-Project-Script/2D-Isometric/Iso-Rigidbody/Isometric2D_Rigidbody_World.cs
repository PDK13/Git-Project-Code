using UnityEngine;

public class Isometric2D_Rigidbody_World : MonoBehaviour
{
    #region Check on World Emty

    public bool Get_World_Emty(Vector3Int v3_Check_Dir)
    {
        return GetComponent<IsoBlock>().Get_World().Get_Current_isEmty(GetComponent<IsoBlock>().Get_PosOnMatrix_Current() + v3_Check_Dir);
    }

    public bool Get_World_Emty_Up()
    {
        return Get_World_Emty(IsoClassDir.v3_Up_X);
    }

    public bool Get_World_Emty_Down()
    {
        return Get_World_Emty(IsoClassDir.v3_Down_X);
    }

    public bool Get_World_Emty_Left()
    {
        return Get_World_Emty(IsoClassDir.v3_Left_Y);
    }

    public bool Get_World_Emty_Right()
    {
        return Get_World_Emty(IsoClassDir.v3_Right_Y);
    }

    public bool Get_World_Emty_Top()
    {
        return Get_World_Emty(IsoClassDir.v3_Top_H);
    }

    public bool Get_World_Emty_Bot()
    {
        return Get_World_Emty(IsoClassDir.v3_Bot_H);
    }

    #endregion

    #region Get on World Block

    public GameObject Get_World_Block(Vector3Int v3_Check_Dir)
    {
        return GetComponent<IsoBlock>().Get_World().Get_Current_GameObject(GetComponent<IsoBlock>().Get_PosOnMatrix_Current() + v3_Check_Dir);
    }

    public GameObject Get_World_Block_Up()
    {
        return Get_World_Block(IsoClassDir.v3_Up_X);
    }

    public GameObject Get_World_Block_Down()
    {
        return Get_World_Block(IsoClassDir.v3_Down_X);
    }

    public GameObject Get_World_Block_Left()
    {
        return Get_World_Block(IsoClassDir.v3_Left_Y);
    }

    public GameObject Get_World_Block_Right()
    {
        return Get_World_Block(IsoClassDir.v3_Right_Y);
    }

    public GameObject Get_World_Block_Top()
    {
        return Get_World_Block(IsoClassDir.v3_Top_H);
    }

    public GameObject Get_World_Block_Bot()
    {
        return Get_World_Block(IsoClassDir.v3_Bot_H);
    }

    #endregion
}
