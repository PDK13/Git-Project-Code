public class IsoClassBlock
{
    public static readonly string m_None = "";

    #region Type

    #region Type Block

    public static readonly string m_Block = "Block";

    public static readonly string m_Block_Ground = "Ground";

    public static readonly string m_Block_Object = "Object";

    public static readonly string m_Block_Item = "Item";

    public static readonly string m_Block_Stair = "Stair";

    public static readonly string m_Block_StairUD = "StairUD";

    public static readonly string m_Block_StairLR = "StairLR";

    #endregion

    #region Type Character

    public static readonly string m_Character = "Character";

    public static readonly string m_Character_Player = "Player";

    public static readonly string m_Character_Good = "Good";

    public static readonly string m_Character_Neutral = "Neutral";

    public static readonly string m_Character_Bad = "Bad";

    #endregion

    #endregion

    #region Block Data

    private readonly string m_Type_Main = "";

    private readonly string m_Type = "";

    public IsoClassBlock(string m_Type_Main, string m_Type)
    {
        this.m_Type_Main = m_Type_Main;
        this.m_Type = m_Type;
    }

    /// <summary>
    /// Type Block or Character
    /// </summary>
    /// <returns></returns>
    public string GetType_Main()
    {
        return m_Type_Main;
    }

    /// <summary>
    /// Type Child of Type A (Block or Character)
    /// </summary>
    /// <returns></returns>
    public string GetType()
    {
        return m_Type;
    }

    #endregion
}
