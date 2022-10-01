public class IsoClassBlock
{
    public static readonly string s_None = "";

    #region Type

    #region Type Block

    public static readonly string s_Block = "Block";

    public static readonly string s_Block_Ground = "Ground";

    public static readonly string s_Block_Object = "Object";

    public static readonly string s_Block_Item = "Item";

    public static readonly string s_Block_Stair = "Stair";

    public static readonly string s_Block_StairUD = "StairUD";

    public static readonly string s_Block_StairLR = "StairLR";

    #endregion

    #region Type Character

    public static readonly string s_Character = "Character";

    public static readonly string s_Character_Player = "Player";

    public static readonly string s_Character_Good = "Good";

    public static readonly string s_Character_Neutral = "Neutral";

    public static readonly string s_Character_Bad = "Bad";

    #endregion

    #endregion

    #region Block Data

    private readonly string s_Type_Main = "";

    private readonly string s_Type = "";

    public IsoClassBlock(string s_Type_Main, string s_Type)
    {
        this.s_Type_Main = s_Type_Main;
        this.s_Type = s_Type;
    }

    /// <summary>
    /// Type Block or Character
    /// </summary>
    /// <returns></returns>
    public string GetType_Main()
    {
        return s_Type_Main;
    }

    /// <summary>
    /// Type Child of Type A (Block or Character)
    /// </summary>
    /// <returns></returns>
    public string GetType()
    {
        return s_Type;
    }

    #endregion
}
