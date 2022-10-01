using UnityEngine;

public class Isometric2D_Rigidbody_World : MonoBehaviour
{
    #region Check on World Emty

    public bool GetWorld_Emty(Vector3Int v3_Check_Dir)
    {
        return GetComponent<IsoBlock>().GetWorld().GetCurrentIsEmty(GetComponent<IsoBlock>().GetPosOnMatrix_Current() + v3_Check_Dir);
    }

    public bool GetWorld_Emty_Up()
    {
        return GetWorld_Emty(IsoClassDir.v3_Up_X);
    }

    public bool GetWorld_Emty_Down()
    {
        return GetWorld_Emty(IsoClassDir.v3_Down_X);
    }

    public bool GetWorld_Emty_Left()
    {
        return GetWorld_Emty(IsoClassDir.v3_Left_Y);
    }

    public bool GetWorld_Emty_Right()
    {
        return GetWorld_Emty(IsoClassDir.v3_Right_Y);
    }

    public bool GetWorld_Emty_Top()
    {
        return GetWorld_Emty(IsoClassDir.v3_Top_H);
    }

    public bool GetWorld_Emty_Bot()
    {
        return GetWorld_Emty(IsoClassDir.v3_Bot_H);
    }

    #endregion

    #region Get on World Block

    public GameObject GetWorld_Block(Vector3Int v3_Check_Dir)
    {
        return GetComponent<IsoBlock>().GetWorld().GetCurrent_GameObject(GetComponent<IsoBlock>().GetPosOnMatrix_Current() + v3_Check_Dir);
    }

    public GameObject GetWorld_Block_Up()
    {
        return GetWorld_Block(IsoClassDir.v3_Up_X);
    }

    public GameObject GetWorld_Block_Down()
    {
        return GetWorld_Block(IsoClassDir.v3_Down_X);
    }

    public GameObject GetWorld_Block_Left()
    {
        return GetWorld_Block(IsoClassDir.v3_Left_Y);
    }

    public GameObject GetWorld_Block_Right()
    {
        return GetWorld_Block(IsoClassDir.v3_Right_Y);
    }

    public GameObject GetWorld_Block_Top()
    {
        return GetWorld_Block(IsoClassDir.v3_Top_H);
    }

    public GameObject GetWorld_Block_Bot()
    {
        return GetWorld_Block(IsoClassDir.v3_Bot_H);
    }

    #endregion
}
