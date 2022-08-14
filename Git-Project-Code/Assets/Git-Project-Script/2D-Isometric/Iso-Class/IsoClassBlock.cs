using UnityEngine;

public class IsoClassBlock
{
    public readonly static string s_None = "";

    #region Type

    #region Type Block

    public readonly static string s_Block = "Block";

    public readonly static string s_Block_Ground = "Ground";

    public readonly static string s_Block_Object = "Object";

    public readonly static string s_Block_Item = "Item";

    public readonly static string s_Block_Stair = "Stair";

    public readonly static string s_Block_StairUD = "StairUD";

    public readonly static string s_Block_StairLR = "StairLR";

    #endregion

    #region Type Character

    public readonly static string s_Character = "Character";

    public readonly static string s_Character_Player = "Player";

    public readonly static string s_Character_Good = "Good";

    public readonly static string s_Character_Neutral = "Neutral";

    public readonly static string s_Character_Bad = "Bad";

    #endregion

    #endregion

    #region Block Data

    private string s_Type_Main = "";

    private string s_Type = "";

    public IsoClassBlock (string s_Type_Main, string s_Type)
    {
        this.s_Type_Main = s_Type_Main;
        this.s_Type = s_Type;
    }

    /// <summary>
    /// Type Block or Character
    /// </summary>
    /// <returns></returns>
    public string Get_Type_Main()
    {
        return this.s_Type_Main;
    }

    /// <summary>
    /// Type Child of Type A (Block or Character)
    /// </summary>
    /// <returns></returns>
    public string Get_Type()
    {
        return this.s_Type;
    }

    #endregion
}
