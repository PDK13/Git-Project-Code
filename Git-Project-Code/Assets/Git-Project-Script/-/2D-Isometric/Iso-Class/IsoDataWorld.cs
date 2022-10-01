using System.Collections.Generic;
using UnityEngine;

public class IsoDataWorld
{
    #region World Primary

    [Tooltip("World Name")]
    private string s_World_Name = "";

    [Tooltip("World Size")]
    private Vector3Int v3_World_Size = new Vector3Int(0, 0, 0);

    #endregion

    #region Block Primary

    [Tooltip("Block Data")]
    private List<IsoDataBlock> l_Block = new List<IsoDataBlock>();

    [Tooltip("Block-Move Data")]
    private List<IsoDataMove> l_Block_Move = new List<IsoDataMove>();

    [Tooltip("Block-Join-To Data")]
    private List<IsoDataJoinTo> l_Block_JoinTo = new List<IsoDataJoinTo>();

    [Tooltip("Block-Switch-To Data")]
    private List<IsoDataSwitchTo> l_Block_SwitchTo = new List<IsoDataSwitchTo>();

    [Tooltip("Block-Message Data")]
    private List<IsoDataMessage> l_Block_Message = new List<IsoDataMessage>();

    [Tooltip("Block-Teleport Data")]
    private List<IsoDataTeleport> l_Block_Teleport = new List<IsoDataTeleport>();

    #endregion

    #region Character Primary

    [Tooltip("Character Player Data (Same as Block Data)")]
    private List<IsoDataBlock> l_Character_Player = new List<IsoDataBlock>();

    [Tooltip("Character Good Data (Same as Block Data)")]
    private List<IsoDataBlock> l_Character_Good = new List<IsoDataBlock>();

    [Tooltip("Character Neutral Data (Same as Block Data)")]
    private List<IsoDataBlock> l_Character_Neutral = new List<IsoDataBlock>();

    [Tooltip("Character Bad Data (Same as Block Data)")]
    private List<IsoDataBlock> l_Character_Bad = new List<IsoDataBlock>();

    #endregion

    /// <summary>
    /// Set World Data with Emty Data
    /// </summary>
    public IsoDataWorld()
    {
        Set_Data_New();
    }

    /// <summary>
    /// Set World Data from Exist Data
    /// </summary>
    /// <param name="cl_Iso_Data"></param>
    public IsoDataWorld(IsoDataWorld cl_Iso_Data)
    {
        Set_DataIsExist(cl_Iso_Data);
    }

    /// <summary>
    /// Set World Data from Exist World
    /// </summary>
    /// <param name="v3_World_Size"></param>
    /// <param name="l3_World"></param>
    /// <param name="l_Spawm"></param>
    public IsoDataWorld(string s_World_Name, List<List<List<GameObject>>> l3_World)
    {
        Set_DataIsExist(s_World_Name, l3_World);
    }

    /// <summary>
    /// Set World Data with Origin
    /// </summary>
    /// <param name="v3_Size"></param>
    /// <param name="cl_World_Renderer"></param>
    public IsoDataWorld(string s_World_Name, Vector3Int v3_Size, IsoWorldRenderer cl_World_Renderer)
    {
        Set_Data_Origin(s_World_Name, v3_Size, cl_World_Renderer);
    }

    #region Data 

    /// <summary>
    /// Set World Data from Exist Data
    /// </summary>
    /// <param name="cl_Iso_Data"></param>
    public void Set_DataIsExist(IsoDataWorld cl_Iso_Data)
    {
        Set_Data_New();

        Set_World_Size(cl_Iso_Data.GetWorld_Size());

        Set_Block_List(cl_Iso_Data.GetBlock_List());
        Set_Block_Move_List(cl_Iso_Data.GetBlock_Move_List());
        Set_Block_JoinTo_List(cl_Iso_Data.GetBlock_JoinTo_List());
        Set_Block_SwitchTo_List(cl_Iso_Data.GetBlock_SwitchTo_List());
        Set_Block_Message_List(cl_Iso_Data.GetBlock_Message_List());

        Set_UI_Player_List(cl_Iso_Data.GetCharacter_Player_List());
        Set_Character_Good_List(cl_Iso_Data.GetCharacter_Good_List());
        Set_Character_Neutral_List(cl_Iso_Data.GetCharacter_Neutral_List());
        Set_Character_Bad_List(cl_Iso_Data.GetCharacter_Bad_List());
    }

    /// <summary>
    /// Set World Data from Exist World
    /// </summary>
    /// <param name="v3_Size"></param>
    /// <param name="l3_World"></param>
    /// <param name="l_Spawm"></param>
    public void Set_DataIsExist(string s_World_Name, List<List<List<GameObject>>> l3_World)
    {
        Set_Data_New();

        this.s_World_Name = s_World_Name;

        Set_World_Size(new Vector3Int(l3_World[0].Count, l3_World[0][0].Count, l3_World.Count));

        for (int h = 0; h < l3_World.Count; h++)
        {
            for (int x = 0; x < l3_World[h].Count; x++)
            {
                for (int y = 0; y < l3_World[h][x].Count; y++)
                {
                    if (l3_World[h][x][y] != null)
                    {
                        if (l3_World[h][x][y].GetComponent<IsoBlock>().GetBlock_Check())
                        {
                            //Block
                            Set_Block_Add(l3_World[h][x][y]);
                        }
                        else
                        if (l3_World[h][x][y].GetComponent<IsoBlock>().GetCharacter_Check())
                        {
                            //Character Player
                            if (l3_World[h][x][y].GetComponent<IsoBlock>().GetCharacter_Check_Player())
                            {
                                Set_UI_Player_Add(l3_World[h][x][y]);
                            }
                            else
                            //Character Good
                            if (l3_World[h][x][y].GetComponent<IsoBlock>().GetCharacter_Check_Good())
                            {
                                Set_Character_Good_Add(l3_World[h][x][y]);
                            }
                            else
                            //Character Neutral
                            if (l3_World[h][x][y].GetComponent<IsoBlock>().GetCharacter_Check_Neutral())
                            {
                                Set_Character_Neutral_Add(l3_World[h][x][y]);
                            }
                            else
                            //Character Bad
                            if (l3_World[h][x][y].GetComponent<IsoBlock>().GetCharacter_Check_Bad())
                            {
                                Set_Character_Bad_Add(l3_World[h][x][y]);
                            }
                        }

                        //Block and Character are same as Block
                        {
                            //Move
                            if (l3_World[h][x][y].GetComponent<IsoBlockMove>() != null)
                            {
                                if (l3_World[h][x][y].GetComponent<IsoBlockMove>().GetCount() != 0)
                                {
                                    Set_Block_Move_Add(l3_World[h][x][y]);
                                }
                            }

                            //Join-To
                            if (l3_World[h][x][y].GetComponent<IsoBlockJoinTo>() != null)
                            {
                                if (l3_World[h][x][y].GetComponent<IsoBlockJoinTo>().GetJoinToIsExist() == true)
                                {
                                    Set_Block_JoinTo_Add(l3_World[h][x][y]);
                                }
                            }

                            //Switch-To
                            if (l3_World[h][x][y].GetComponent<IsoBlockSwitchTo>() != null)
                            {
                                if (l3_World[h][x][y].GetComponent<IsoBlockSwitchTo>().GetCount() != 0)
                                {
                                    Set_Block_SwitchTo_Add(l3_World[h][x][y]);
                                }
                            }

                            //Message
                            if (l3_World[h][x][y].GetComponent<IsoBlockMessage>() != null)
                            {
                                if (l3_World[h][x][y].GetComponent<IsoBlockMessage>().GetCount() != 0)
                                {
                                    Set_Block_Message_Add(l3_World[h][x][y]);
                                }
                            }

                            //Teleport
                            if (l3_World[h][x][y].GetComponent<IsoBlockTeleport>() != null)
                            {
                                if (l3_World[h][x][y].GetComponent<IsoBlockTeleport>().GetisExist())
                                {
                                    Set_Block_Teleport_Add(l3_World[h][x][y]);
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Set World Data with Origin
    /// </summary>
    /// <param name="v3_Size"></param>
    /// <param name="cl_World_Renderer"></param>
    public void Set_Data_Origin(string s_World_Name, Vector3Int v3_Size, IsoWorldRenderer cl_World_Renderer)
    {
        Set_Data_New();

        this.s_World_Name = s_World_Name;

        Set_World_Size(v3_Size);

        string s_Block_Name = ClassString.GetStringReplaceClone(cl_World_Renderer.GetCombine_First().name);

        for (int x = 0; x < v3_Size.x; x++)
        {
            for (int y = 0; y < v3_Size.y; y++)
            {
                Set_Block_Add(new IsoDataBlock(new Vector3Int(x, y, 0), s_Block_Name));
            }
        }

        for (int h = 1; h < v3_Size.z; h++)
        {
            Set_Block_Add(new IsoDataBlock(new Vector3Int(0, v3_Size.y - 1, h), s_Block_Name));
        }
    }

    /// <summary>
    /// Set Data Emty - New
    /// </summary>
    public void Set_Data_New()
    {
        s_World_Name = "";

        v3_World_Size = new Vector3Int(0, 0, 0);

        l_Block = new List<IsoDataBlock>();
        l_Block_Move = new List<IsoDataMove>();
        l_Block_JoinTo = new List<IsoDataJoinTo>();
        l_Block_SwitchTo = new List<IsoDataSwitchTo>();
        l_Block_Message = new List<IsoDataMessage>();

        l_Character_Player = new List<IsoDataBlock>();
        l_Character_Good = new List<IsoDataBlock>();
        l_Character_Neutral = new List<IsoDataBlock>();
        l_Character_Bad = new List<IsoDataBlock>();
    }

    #endregion

    //============================================================================================================ World Name

    public void Set_World_Name(string s_World_Name)
    {
        this.s_World_Name = s_World_Name;
    }

    public string GetWorld_Name()
    {
        return s_World_Name;
    }

    //============================================================================================================ World Size

    #region World Size Data 

    public void Set_World_Size(Vector3Int v3_World_Size)
    {
        this.v3_World_Size = v3_World_Size;
    }

    public void Set_World_Size_Add(int i_World_Size_X_Add, int i_World_Size_Y_Add, int i_World_Size_High_Add)
    {
        v3_World_Size.x += i_World_Size_X_Add;
        v3_World_Size.y += i_World_Size_Y_Add;
        v3_World_Size.z += i_World_Size_High_Add;
    }

    public Vector3Int GetWorld_Size()
    {
        return v3_World_Size;
    }

    #endregion

    //============================================================================================================ Block

    #region Block Data 

    //============================================================================================================ Block Primary

    #region Block Primary Data 

    #region Block Primary Data Add 

    public void Set_Block_Add(IsoDataBlock cl_Data)
    {
        l_Block.Add(cl_Data);
    }

    public void Set_Block_Add(GameObject g_Block)
    {
        Set_Block_Add(new IsoDataBlock(g_Block));
    }

    public void Set_Block_List(List<IsoDataBlock> l_Block)
    {
        this.l_Block = l_Block;
    }

    #endregion

    #region Block Primary Data Get 

    public IsoDataBlock GetBlock(int i_Block_Index)
    {
        if (i_Block_Index < 0 || i_Block_Index >= GetBlockCount())
        {
            return null;
        }
        return l_Block[i_Block_Index];
    }

    public int GetBlockCount()
    {
        return l_Block.Count;
    }

    public List<IsoDataBlock> GetBlock_List()
    {
        return l_Block;
    }

    #endregion

    #region Block Primary Data Chance by World Size Chance 

    #region Block Primary Data Remove by Pos 

    /// <summary>
    /// Set Block Data Remove by X
    /// </summary>
    /// <param name="v3_Pos_Remove"></param>
    public void Set_Block_Remove_X(Vector3Int v3_Pos_Remove)
    {
        int i_Index = 0;

        //Block Data

        do
        {
            for (i_Index = 0; i_Index < l_Block.Count; i_Index++)
            {
                if (l_Block[i_Index].GetPos().x == v3_Pos_Remove.x)
                {
                    l_Block.RemoveAt(i_Index);
                    break;
                }
            }
        }
        while (i_Index < l_Block.Count - 1);

        for (int i = 0; i < l_Block.Count; i++)
        {
            if (l_Block[i].GetPos().x > v3_Pos_Remove.x)
            {
                l_Block[i].Set_Pos_Add(-1, 0, 0);
            }
        }

        //Block Move Data

        do
        {
            for (i_Index = 0; i_Index < l_Block_Move.Count; i_Index++)
            {
                if (l_Block_Move[i_Index].GetBlock().GetPos().x == v3_Pos_Remove.x)
                {
                    l_Block_Move.RemoveAt(i_Index);
                    break;
                }
            }
        }
        while (i_Index < l_Block_Move.Count - 1);

        for (int i = 0; i < l_Block_Move.Count; i++)
        {
            if (l_Block_Move[i].GetBlock().GetPos().x > v3_Pos_Remove.x)
            {
                l_Block_Move[i].GetBlock().Set_Pos_Add(-1, 0, 0);
            }
        }

        //Block Join-To Data

        do
        {
            for (i_Index = 0; i_Index < l_Block_JoinTo.Count; i_Index++)
            {
                if (l_Block_JoinTo[i_Index].GetBlock().GetPos().x == v3_Pos_Remove.x)
                {
                    l_Block_JoinTo.RemoveAt(i_Index);
                    break;
                }
            }
        }
        while (i_Index < l_Block_JoinTo.Count - 1);

        for (int i = 0; i < l_Block_JoinTo.Count; i++)
        {
            if (l_Block_JoinTo[i].GetBlock().GetPos().x > v3_Pos_Remove.x)
            {
                l_Block_JoinTo[i].GetBlock().Set_Pos_Add(-1, 0, 0);
            }

            if (l_Block_JoinTo[i].GetJoinTo_Pos().x > v3_Pos_Remove.x)
            {
                l_Block_JoinTo[i].Set_Pos_JoinTo_Add(-1, 0, 0);
            }
        }

        //Block Switch-To Data

        do
        {
            for (i_Index = 0; i_Index < l_Block_SwitchTo.Count; i_Index++)
            {
                if (l_Block_SwitchTo[i_Index].GetBlock().GetPos().x == v3_Pos_Remove.x)
                {
                    l_Block_SwitchTo.RemoveAt(i_Index);
                    break;
                }
            }
        }
        while (i_Index < l_Block_SwitchTo.Count - 1);

        for (int i = 0; i < l_Block_SwitchTo.Count; i++)
        {
            if (l_Block_SwitchTo[i].GetBlock().GetPos().x > v3_Pos_Remove.x)
            {
                l_Block_SwitchTo[i].GetBlock().Set_Pos_Add(-1, 0, 0);
            }

            for (int j = 0; j < l_Block_SwitchTo[i].GetList().Count; j++)
            {
                if (l_Block_SwitchTo[i].GetList(j).x > v3_Pos_Remove.x)
                {
                    l_Block_SwitchTo[i].Set_Add(j, -1, 0, 0);
                }
            }
        }

        //Block Message Data

        do
        {
            for (i_Index = 0; i_Index < l_Block_Message.Count; i_Index++)
            {
                if (l_Block_Message[i_Index].GetBlock().GetPos().x == v3_Pos_Remove.x)
                {
                    l_Block_Message.RemoveAt(i_Index);
                    break;
                }
            }
        }
        while (i_Index < l_Block_Message.Count - 1);

        for (int i = 0; i < l_Block_Message.Count; i++)
        {
            if (l_Block_Message[i].GetBlock().GetPos().x > v3_Pos_Remove.x)
            {
                l_Block_Message[i].GetBlock().Set_Pos_Add(-1, 0, 0);
            }
        }

        //Block Teleport Message Data

        do
        {
            for (i_Index = 0; i_Index < l_Block_Teleport.Count; i_Index++)
            {
                if (l_Block_Teleport[i_Index].GetBlock().GetPos().x == v3_Pos_Remove.x)
                {
                    l_Block_Teleport.RemoveAt(i_Index);
                    break;
                }
            }
        }
        while (i_Index < l_Block_Teleport.Count - 1);

        for (int i = 0; i < l_Block_Teleport.Count; i++)
        {
            if (l_Block_Teleport[i].GetBlock().GetPos().x > v3_Pos_Remove.x)
            {
                l_Block_Teleport[i].GetBlock().Set_Pos_Add(-1, 0, 0);
            }
        }

        //Character Player Data

        do
        {
            for (i_Index = 0; i_Index < l_Character_Player.Count; i_Index++)
            {
                if (l_Character_Player[i_Index].GetPos().x == v3_Pos_Remove.x)
                {
                    l_Character_Player.RemoveAt(i_Index);
                    break;
                }
            }
        }
        while (i_Index < l_Character_Player.Count - 1);

        for (int i = 0; i < l_Character_Player.Count; i++)
        {
            if (l_Character_Player[i].GetPos().x > v3_Pos_Remove.x)
            {
                l_Character_Player[i].Set_Pos_Add(-1, 0, 0);
            }
        }

        //Character Good Data

        do
        {
            for (i_Index = 0; i_Index < l_Character_Good.Count; i_Index++)
            {
                if (l_Character_Good[i_Index].GetPos().x == v3_Pos_Remove.x)
                {
                    l_Character_Good.RemoveAt(i_Index);
                    break;
                }
            }
        }
        while (i_Index < l_Character_Good.Count - 1);

        for (int i = 0; i < l_Character_Good.Count; i++)
        {
            if (l_Character_Good[i].GetPos().x > v3_Pos_Remove.x)
            {
                l_Character_Good[i].Set_Pos_Add(-1, 0, 0);
            }
        }

        //Character Neutral Data

        do
        {
            for (i_Index = 0; i_Index < l_Character_Neutral.Count; i_Index++)
            {
                if (l_Character_Neutral[i_Index].GetPos().x == v3_Pos_Remove.x)
                {
                    l_Character_Neutral.RemoveAt(i_Index);
                    break;
                }
            }
        }
        while (i_Index < l_Character_Neutral.Count - 1);

        for (int i = 0; i < l_Character_Neutral.Count; i++)
        {
            if (l_Character_Neutral[i].GetPos().x > v3_Pos_Remove.x)
            {
                l_Character_Neutral[i].Set_Pos_Add(-1, 0, 0);
            }
        }

        //Character Bad Data

        do
        {
            for (i_Index = 0; i_Index < l_Character_Bad.Count; i_Index++)
            {
                if (l_Character_Bad[i_Index].GetPos().x == v3_Pos_Remove.x)
                {
                    l_Character_Bad.RemoveAt(i_Index);
                    break;
                }
            }
        }
        while (i_Index < l_Character_Bad.Count - 1);

        for (int i = 0; i < l_Character_Bad.Count; i++)
        {
            if (l_Character_Bad[i].GetPos().x > v3_Pos_Remove.x)
            {
                l_Character_Bad[i].Set_Pos_Add(-1, 0, 0);
            }
        }

        Set_World_Size_Add(-1, 0, 0);
    }

    /// <summary>
    /// Set Block Data Remove by Y
    /// </summary>
    /// <param name="v3_Pos_Remove"></param>
    public void Set_Block_Remove_Y(Vector3Int v3_Pos_Remove)
    {
        int i_Index = 0;

        //Block Data

        do
        {
            for (i_Index = 0; i_Index < l_Block.Count; i_Index++)
            {
                if (l_Block[i_Index].GetPos().y == v3_Pos_Remove.y)
                {
                    l_Block.RemoveAt(i_Index);
                    break;
                }
            }
        }
        while (i_Index < l_Block.Count - 1);

        for (int i = 0; i < l_Block.Count; i++)
        {
            if (l_Block[i].GetPos().y > v3_Pos_Remove.y)
            {
                l_Block[i].Set_Pos_Add(0, -1, 0);
            }
        }

        //Block Move Data

        do
        {
            for (i_Index = 0; i_Index < l_Block_Move.Count; i_Index++)
            {
                if (l_Block_Move[i_Index].GetBlock().GetPos().y == v3_Pos_Remove.y)
                {
                    l_Block_Move.RemoveAt(i_Index);
                    break;
                }
            }
        }
        while (i_Index < l_Block_Move.Count - 1);

        for (int i = 0; i < l_Block_Move.Count; i++)
        {
            if (l_Block_Move[i].GetBlock().GetPos().y > v3_Pos_Remove.y)
            {
                l_Block_Move[i].GetBlock().Set_Pos_Add(0, -1, 0);
            }
        }

        //Block Join-To Data

        do
        {
            for (i_Index = 0; i_Index < l_Block_JoinTo.Count; i_Index++)
            {
                if (l_Block_JoinTo[i_Index].GetBlock().GetPos().y == v3_Pos_Remove.y)
                {
                    l_Block_JoinTo.RemoveAt(i_Index);
                    break;
                }
            }
        }
        while (i_Index < l_Block_JoinTo.Count - 1);

        for (int i = 0; i < l_Block_JoinTo.Count; i++)
        {
            if (l_Block_JoinTo[i].GetBlock().GetPos().y > v3_Pos_Remove.y)
            {
                l_Block_JoinTo[i].GetBlock().Set_Pos_Add(0, -1, 0);
            }

            if (l_Block_JoinTo[i].GetJoinTo_Pos().y > v3_Pos_Remove.y)
            {
                l_Block_JoinTo[i].Set_Pos_JoinTo_Add(0, -1, 0);
            }
        }

        //Block Switch-To Data

        do
        {
            for (i_Index = 0; i_Index < l_Block_SwitchTo.Count; i_Index++)
            {
                if (l_Block_SwitchTo[i_Index].GetBlock().GetPos().y == v3_Pos_Remove.y)
                {
                    l_Block_SwitchTo.RemoveAt(i_Index);
                    break;
                }
            }
        }
        while (i_Index < l_Block_SwitchTo.Count - 1);

        for (int i = 0; i < l_Block_SwitchTo.Count; i++)
        {
            if (l_Block_SwitchTo[i].GetBlock().GetPos().y > v3_Pos_Remove.y)
            {
                l_Block_SwitchTo[i].GetBlock().Set_Pos_Add(0, -1, 0);
            }

            for (int j = 0; j < l_Block_SwitchTo[i].GetList().Count; j++)
            {
                if (l_Block_SwitchTo[i].GetList(j).x > v3_Pos_Remove.x)
                {
                    l_Block_SwitchTo[i].Set_Add(j, 0, -1, 0);
                }
            }
        }

        //Block Message Data

        int i_Remove_Message = 0;
        do
        {
            for (i_Remove_Message = 0; i_Remove_Message < l_Block_Message.Count; i_Remove_Message++)
            {
                if (l_Block_Message[i_Remove_Message].GetBlock().GetPos().y == v3_Pos_Remove.y)
                {
                    l_Block_Message.RemoveAt(i_Remove_Message);
                    break;
                }
            }
        }
        while (i_Remove_Message < l_Block_Message.Count - 1);

        for (int i = 0; i < l_Block_Message.Count; i++)
        {
            if (l_Block_Message[i].GetBlock().GetPos().y > v3_Pos_Remove.y)
            {
                l_Block_Message[i].GetBlock().Set_Pos_Add(0, -1, 0);
            }
        }

        //Block Teleport Message Data

        do
        {
            for (i_Index = 0; i_Index < l_Block_Teleport.Count; i_Index++)
            {
                if (l_Block_Teleport[i_Index].GetBlock().GetPos().y == v3_Pos_Remove.y)
                {
                    l_Block_Teleport.RemoveAt(i_Index);
                    break;
                }
            }
        }
        while (i_Index < l_Block_Teleport.Count - 1);

        for (int i = 0; i < l_Block_Teleport.Count; i++)
        {
            if (l_Block_Teleport[i].GetBlock().GetPos().y > v3_Pos_Remove.y)
            {
                l_Block_Teleport[i].GetBlock().Set_Pos_Add(0, -1, 0);
            }
        }

        //Character Player Data

        do
        {
            for (i_Index = 0; i_Index < l_Character_Player.Count; i_Index++)
            {
                if (l_Character_Player[i_Index].GetPos().y == v3_Pos_Remove.y)
                {
                    l_Character_Player.RemoveAt(i_Index);
                    break;
                }
            }
        }
        while (i_Index < l_Character_Player.Count - 1);

        for (int i = 0; i < l_Character_Player.Count; i++)
        {
            if (l_Character_Player[i].GetPos().y > v3_Pos_Remove.y)
            {
                l_Character_Player[i].Set_Pos_Add(0, -1, 0);
            }
        }

        //Character Good Data

        do
        {
            for (i_Index = 0; i_Index < l_Character_Good.Count; i_Index++)
            {
                if (l_Character_Good[i_Index].GetPos().y == v3_Pos_Remove.y)
                {
                    l_Character_Good.RemoveAt(i_Index);
                    break;
                }
            }
        }
        while (i_Index < l_Character_Good.Count - 1);

        for (int i = 0; i < l_Character_Good.Count; i++)
        {
            if (l_Character_Good[i].GetPos().y > v3_Pos_Remove.y)
            {
                l_Character_Good[i].Set_Pos_Add(0, -1, 0);
            }
        }

        //Character Neutral Data

        do
        {
            for (i_Index = 0; i_Index < l_Character_Neutral.Count; i_Index++)
            {
                if (l_Character_Neutral[i_Index].GetPos().y == v3_Pos_Remove.y)
                {
                    l_Character_Neutral.RemoveAt(i_Index);
                    break;
                }
            }
        }
        while (i_Index < l_Character_Neutral.Count - 1);

        for (int i = 0; i < l_Character_Neutral.Count; i++)
        {
            if (l_Character_Neutral[i].GetPos().y > v3_Pos_Remove.y)
            {
                l_Character_Neutral[i].Set_Pos_Add(0, -1, 0);
            }
        }

        //Character Bad Data

        do
        {
            for (i_Index = 0; i_Index < l_Character_Bad.Count; i_Index++)
            {
                if (l_Character_Bad[i_Index].GetPos().y == v3_Pos_Remove.y)
                {
                    l_Character_Bad.RemoveAt(i_Index);
                    break;
                }
            }
        }
        while (i_Index < l_Character_Bad.Count - 1);

        for (int i = 0; i < l_Character_Bad.Count; i++)
        {
            if (l_Character_Bad[i].GetPos().y > v3_Pos_Remove.y)
            {
                l_Character_Bad[i].Set_Pos_Add(0, -1, 0);
            }
        }

        Set_World_Size_Add(0, -1, 0);
    }

    /// <summary>
    /// Set Block Data Remove by High
    /// </summary>
    /// <param name="v3_Pos_Remove"></param>
    public void Set_Block_Remove_High(Vector3Int v3_Pos_Remove)
    {
        int i_Index = 0;

        //Block Data

        do
        {
            for (i_Index = 0; i_Index < l_Block.Count; i_Index++)
            {
                if (l_Block[i_Index].GetPos().z == v3_Pos_Remove.z)
                {
                    l_Block.RemoveAt(i_Index);
                    break;
                }
            }
        }
        while (i_Index < l_Block.Count - 1);

        for (int i = 0; i < l_Block.Count; i++)
        {
            if (l_Block[i].GetPos().z > v3_Pos_Remove.z)
            {
                l_Block[i].Set_Pos_Add(0, 0, -1);
            }
        }

        //Move Data

        do
        {
            for (i_Index = 0; i_Index < l_Block_Move.Count; i_Index++)
            {
                if (l_Block_Move[i_Index].GetBlock().GetPos().z == v3_Pos_Remove.z)
                {
                    l_Block_Move.RemoveAt(i_Index);
                    break;
                }
            }
        }
        while (i_Index < l_Block.Count - 1);

        for (int i = 0; i < l_Block.Count; i++)
        {
            if (l_Block_Move[i].GetBlock().GetPos().z > v3_Pos_Remove.z)
            {
                l_Block_Move[i].GetBlock().Set_Pos_Add(0, 0, -1);
            }
        }

        //Join-To Data

        do
        {
            for (i_Index = 0; i_Index < l_Block_JoinTo.Count; i_Index++)
            {
                if (l_Block_JoinTo[i_Index].GetBlock().GetPos().z == v3_Pos_Remove.z)
                {
                    l_Block_JoinTo.RemoveAt(i_Index);
                    break;
                }
            }
        }
        while (i_Index < l_Block.Count - 1);

        for (int i = 0; i < l_Block.Count; i++)
        {
            if (l_Block_JoinTo[i].GetBlock().GetPos().z > v3_Pos_Remove.z)
            {
                l_Block_JoinTo[i].GetBlock().Set_Pos_Add(0, 0, -1);
            }

            if (l_Block_JoinTo[i].GetJoinTo_Pos().z > v3_Pos_Remove.z)
            {
                l_Block_JoinTo[i].Set_Pos_JoinTo_Add(0, 0, -1);
            }
        }

        //Switch-To Data

        do
        {
            for (i_Index = 0; i_Index < l_Block_SwitchTo.Count; i_Index++)
            {
                if (l_Block_SwitchTo[i_Index].GetBlock().GetPos().z == v3_Pos_Remove.z)
                {
                    l_Block_SwitchTo.RemoveAt(i_Index);
                    break;
                }
            }
        }
        while (i_Index < l_Block.Count - 1);

        for (int i = 0; i < l_Block.Count; i++)
        {
            if (l_Block_SwitchTo[i].GetBlock().GetPos().z > v3_Pos_Remove.z)
            {
                l_Block_SwitchTo[i].GetBlock().Set_Pos_Add(0, 0, -1);
            }

            for (int j = 0; j < l_Block_SwitchTo[i].GetList().Count; j++)
            {
                if (l_Block_SwitchTo[i].GetList(j).x > v3_Pos_Remove.x)
                {
                    l_Block_SwitchTo[i].Set_Add(j, 0, 0, -1);
                }
            }
        }

        //Message Data

        do
        {
            for (i_Index = 0; i_Index < l_Block_Message.Count; i_Index++)
            {
                if (l_Block_Message[i_Index].GetBlock().GetPos().z == v3_Pos_Remove.z)
                {
                    l_Block_Message.RemoveAt(i_Index);
                    break;
                }
            }
        }
        while (i_Index < l_Block.Count - 1);

        for (int i = 0; i < l_Block.Count; i++)
        {
            if (l_Block_Message[i].GetBlock().GetPos().z > v3_Pos_Remove.z)
            {
                l_Block_Message[i].GetBlock().Set_Pos_Add(0, 0, -1);
            }
        }

        //Block Teleport Message Data

        do
        {
            for (i_Index = 0; i_Index < l_Block_Teleport.Count; i_Index++)
            {
                if (l_Block_Teleport[i_Index].GetBlock().GetPos().z == v3_Pos_Remove.z)
                {
                    l_Block_Teleport.RemoveAt(i_Index);
                    break;
                }
            }
        }
        while (i_Index < l_Block_Teleport.Count - 1);

        for (int i = 0; i < l_Block_Teleport.Count; i++)
        {
            if (l_Block_Teleport[i].GetBlock().GetPos().z > v3_Pos_Remove.z)
            {
                l_Block_Teleport[i].GetBlock().Set_Pos_Add(0, 0, -1);
            }
        }

        //Character Player Data

        do
        {
            for (i_Index = 0; i_Index < l_Character_Player.Count; i_Index++)
            {
                if (l_Character_Player[i_Index].GetPos().z == v3_Pos_Remove.z)
                {
                    l_Character_Player.RemoveAt(i_Index);
                    break;
                }
            }
        }
        while (i_Index < l_Character_Player.Count - 1);

        for (int i = 0; i < l_Character_Player.Count; i++)
        {
            if (l_Character_Player[i].GetPos().z > v3_Pos_Remove.z)
            {
                l_Character_Player[i].Set_Pos_Add(0, 0, -1);
            }
        }

        //Character Good Data

        do
        {
            for (i_Index = 0; i_Index < l_Character_Good.Count; i_Index++)
            {
                if (l_Character_Good[i_Index].GetPos().z == v3_Pos_Remove.z)
                {
                    l_Character_Good.RemoveAt(i_Index);
                    break;
                }
            }
        }
        while (i_Index < l_Character_Good.Count - 1);

        for (int i = 0; i < l_Character_Good.Count; i++)
        {
            if (l_Character_Good[i].GetPos().z > v3_Pos_Remove.z)
            {
                l_Character_Good[i].Set_Pos_Add(0, 0, -1);
            }
        }

        //Character Neutral Data

        do
        {
            for (i_Index = 0; i_Index < l_Character_Neutral.Count; i_Index++)
            {
                if (l_Character_Neutral[i_Index].GetPos().z == v3_Pos_Remove.z)
                {
                    l_Character_Neutral.RemoveAt(i_Index);
                    break;
                }
            }
        }
        while (i_Index < l_Character_Neutral.Count - 1);

        for (int i = 0; i < l_Character_Neutral.Count; i++)
        {
            if (l_Character_Neutral[i].GetPos().z > v3_Pos_Remove.z)
            {
                l_Character_Neutral[i].Set_Pos_Add(0, 0, -1);
            }
        }

        //Character Bad Data

        do
        {
            for (i_Index = 0; i_Index < l_Character_Bad.Count; i_Index++)
            {
                if (l_Character_Bad[i_Index].GetPos().z == v3_Pos_Remove.z)
                {
                    l_Character_Bad.RemoveAt(i_Index);
                    break;
                }
            }
        }
        while (i_Index < l_Character_Bad.Count - 1);

        for (int i = 0; i < l_Character_Bad.Count; i++)
        {
            if (l_Character_Bad[i].GetPos().z > v3_Pos_Remove.z)
            {
                l_Character_Bad[i].Set_Pos_Add(0, 0, -1);
            }
        }

        Set_World_Size_Add(0, 0, -1);
    }

    #endregion

    #region Block Primary Data Add Pos by Pos 

    /// <summary>
    /// Set Block Data Add by X
    /// </summary>
    /// <param name="v3_Pos_Add"></param>
    public void Set_Block_Add_X(Vector3Int v3_Pos_Add)
    {
        //Block Block Data
        for (int i = 0; i < l_Block.Count; i++)
        {
            if (l_Block[i].GetPos().x >= v3_Pos_Add.x)
            {
                l_Block[i].Set_Pos_Add(1, 0, 0);
            }
        }

        //Block Move Data
        for (int i = 0; i < l_Block_Move.Count; i++)
        {
            if (l_Block_Move[i].GetBlock().GetPos().x >= v3_Pos_Add.x)
            {
                l_Block_Move[i].GetBlock().Set_Pos_Add(1, 0, 0);
            }
        }

        //Block Join-To Data
        for (int i = 0; i < l_Block_JoinTo.Count; i++)
        {
            if (l_Block_JoinTo[i].GetBlock().GetPos().x >= v3_Pos_Add.x)
            {
                l_Block_JoinTo[i].GetBlock().Set_Pos_Add(1, 0, 0);
            }

            if (l_Block_JoinTo[i].GetJoinTo_Pos().x >= v3_Pos_Add.x)
            {
                l_Block_JoinTo[i].Set_Pos_JoinTo_Add(1, 0, 0);
            }
        }

        //Block Switch-To Data
        for (int i = 0; i < l_Block_SwitchTo.Count; i++)
        {
            if (l_Block_SwitchTo[i].GetBlock().GetPos().x >= v3_Pos_Add.x)
            {
                l_Block_SwitchTo[i].GetBlock().Set_Pos_Add(1, 0, 0);
            }

            for (int j = 0; j < l_Block_SwitchTo[i].GetList().Count; j++)
            {
                if (l_Block_SwitchTo[i].GetList(j).x > v3_Pos_Add.x)
                {
                    l_Block_SwitchTo[i].Set_Add(j, 1, 0, 0);
                }
            }
        }

        //Block Message Data
        for (int i = 0; i < l_Block_Message.Count; i++)
        {
            if (l_Block_Message[i].GetBlock().GetPos().x >= v3_Pos_Add.x)
            {
                l_Block_Message[i].GetBlock().Set_Pos_Add(1, 0, 0);
            }
        }

        //Block Teleport Data
        for (int i = 0; i < l_Block_Teleport.Count; i++)
        {
            if (l_Block_Teleport[i].GetBlock().GetPos().x >= v3_Pos_Add.x)
            {
                l_Block_Teleport[i].GetBlock().Set_Pos_Add(1, 0, 0);

                l_Block_Teleport[i].Set_Pos_Add(1, 0, 0);
            }
        }

        //Character Player Data
        for (int i = 0; i < l_Character_Player.Count; i++)
        {
            if (l_Character_Player[i].GetPos().x >= v3_Pos_Add.x)
            {
                l_Character_Player[i].Set_Pos_Add(1, 0, 0);
            }
        }

        //Character Good Data
        for (int i = 0; i < l_Character_Good.Count; i++)
        {
            if (l_Character_Good[i].GetPos().x >= v3_Pos_Add.x)
            {
                l_Character_Good[i].Set_Pos_Add(1, 0, 0);
            }
        }

        //Character Neutral Data
        for (int i = 0; i < l_Character_Neutral.Count; i++)
        {
            if (l_Character_Neutral[i].GetPos().x >= v3_Pos_Add.x)
            {
                l_Character_Neutral[i].Set_Pos_Add(1, 0, 0);
            }
        }

        //Character Bad Data
        for (int i = 0; i < l_Character_Bad.Count; i++)
        {
            if (l_Character_Bad[i].GetPos().x >= v3_Pos_Add.x)
            {
                l_Character_Bad[i].Set_Pos_Add(1, 0, 0);
            }
        }

        Set_World_Size_Add(1, 0, 0);
    }

    /// <summary>
    /// Set Block Data Add by Y
    /// </summary>
    /// <param name="v3_Pos_Add"></param>
    public void Set_Block_Add_Y(Vector3Int v3_Pos_Add)
    {
        //Block Block Data
        for (int i = 0; i < l_Block.Count; i++)
        {
            if (l_Block[i].GetPos().y >= v3_Pos_Add.y)
            {
                l_Block[i].Set_Pos_Add(0, 1, 0);
            }
        }

        //Block Move Data
        for (int i = 0; i < l_Block_Move.Count; i++)
        {
            if (l_Block_Move[i].GetBlock().GetPos().y >= v3_Pos_Add.y)
            {
                l_Block_Move[i].GetBlock().Set_Pos_Add(0, 1, 0);
            }
        }

        //Block Join-To Data
        for (int i = 0; i < l_Block_JoinTo.Count; i++)
        {
            if (l_Block_JoinTo[i].GetBlock().GetPos().y >= v3_Pos_Add.y)
            {
                l_Block_JoinTo[i].GetBlock().Set_Pos_Add(0, 1, 0);
            }

            if (l_Block_JoinTo[i].GetJoinTo_Pos().y >= v3_Pos_Add.y)
            {
                l_Block_JoinTo[i].Set_Pos_JoinTo_Add(0, 1, 0);
            }
        }

        //Block Switch-To Data
        for (int i = 0; i < l_Block_SwitchTo.Count; i++)
        {
            if (l_Block_SwitchTo[i].GetBlock().GetPos().y >= v3_Pos_Add.y)
            {
                l_Block_SwitchTo[i].GetBlock().Set_Pos_Add(0, 1, 0);
            }

            for (int j = 0; j < l_Block_SwitchTo[i].GetList().Count; j++)
            {
                if (l_Block_SwitchTo[i].GetList(j).x > v3_Pos_Add.x)
                {
                    l_Block_SwitchTo[i].Set_Add(j, 0, 1, 0);
                }
            }
        }

        //Block Message Data
        for (int i = 0; i < l_Block_Message.Count; i++)
        {
            if (l_Block_Message[i].GetBlock().GetPos().y >= v3_Pos_Add.y)
            {
                l_Block_Message[i].GetBlock().Set_Pos_Add(0, 1, 0);
            }
        }

        //Block Teleport Data
        for (int i = 0; i < l_Block_Teleport.Count; i++)
        {
            if (l_Block_Teleport[i].GetBlock().GetPos().y >= v3_Pos_Add.y)
            {
                l_Block_Teleport[i].GetBlock().Set_Pos_Add(0, 1, 0);

                l_Block_Teleport[i].Set_Pos_Add(0, 1, 0);
            }
        }

        //Character Player Data
        for (int i = 0; i < l_Character_Player.Count; i++)
        {
            if (l_Character_Player[i].GetPos().y >= v3_Pos_Add.y)
            {
                l_Character_Player[i].Set_Pos_Add(0, 1, 0);
            }
        }

        //Character Good Data
        for (int i = 0; i < l_Character_Good.Count; i++)
        {
            if (l_Character_Good[i].GetPos().y >= v3_Pos_Add.y)
            {
                l_Character_Good[i].Set_Pos_Add(0, 1, 0);
            }
        }

        //Character Neutral Data
        for (int i = 0; i < l_Character_Neutral.Count; i++)
        {
            if (l_Character_Neutral[i].GetPos().y >= v3_Pos_Add.y)
            {
                l_Character_Neutral[i].Set_Pos_Add(0, 1, 0);
            }
        }

        //Character Bad Data
        for (int i = 0; i < l_Character_Bad.Count; i++)
        {
            if (l_Character_Bad[i].GetPos().y >= v3_Pos_Add.y)
            {
                l_Character_Bad[i].Set_Pos_Add(0, 1, 0);
            }
        }

        Set_World_Size_Add(0, 1, 0);
    }

    /// <summary>
    /// Set Block Data Add by High
    /// </summary>
    /// <param name="v3_Pos_Add"></param>
    public void Set_Block_Add_High(Vector3Int v3_Pos_Add)
    {
        //Block Data
        for (int i = 0; i < l_Block.Count; i++)
        {
            if (l_Block[i].GetPos().z >= v3_Pos_Add.z)
            {
                l_Block[i].Set_Pos_Add(0, 0, 1);
            }
        }

        //Block Move Data
        for (int i = 0; i < l_Block_Move.Count; i++)
        {
            if (l_Block_Move[i].GetBlock().GetPos().z >= v3_Pos_Add.z)
            {
                l_Block_Move[i].GetBlock().Set_Pos_Add(0, 0, 1);
            }
        }

        //Block Join-To Data
        for (int i = 0; i < l_Block_JoinTo.Count; i++)
        {
            if (l_Block_JoinTo[i].GetBlock().GetPos().z >= v3_Pos_Add.z)
            {
                l_Block_JoinTo[i].GetBlock().Set_Pos_Add(0, 0, 1);
            }

            if (l_Block_JoinTo[i].GetJoinTo_Pos().z >= v3_Pos_Add.z)
            {
                l_Block_JoinTo[i].Set_Pos_JoinTo_Add(0, 0, 1);
            }
        }

        //Block Switch-To Data
        for (int i = 0; i < l_Block_SwitchTo.Count; i++)
        {
            if (l_Block_SwitchTo[i].GetBlock().GetPos().z >= v3_Pos_Add.z)
            {
                l_Block_SwitchTo[i].GetBlock().Set_Pos_Add(0, 0, 1);
            }

            for (int j = 0; j < l_Block_SwitchTo[i].GetList().Count; j++)
            {
                if (l_Block_SwitchTo[i].GetList(j).x > v3_Pos_Add.x)
                {
                    l_Block_SwitchTo[i].Set_Add(j, 0, 0, 1);
                }
            }
        }

        //Block Message Data
        for (int i = 0; i < l_Block_Message.Count; i++)
        {
            if (l_Block_Message[i].GetBlock().GetPos().z >= v3_Pos_Add.z)
            {
                l_Block_Message[i].GetBlock().Set_Pos_Add(0, 0, 1);
            }
        }

        //Block Teleport Data
        for (int i = 0; i < l_Block_Teleport.Count; i++)
        {
            if (l_Block_Teleport[i].GetBlock().GetPos().z >= v3_Pos_Add.z)
            {
                l_Block_Teleport[i].GetBlock().Set_Pos_Add(0, 0, 1);

                l_Block_Teleport[i].Set_Pos_Add(0, 0, 1);
            }
        }

        //Character Player Data
        for (int i = 0; i < l_Character_Player.Count; i++)
        {
            if (l_Character_Player[i].GetPos().z >= v3_Pos_Add.z)
            {
                l_Character_Player[i].Set_Pos_Add(0, 0, 1);
            }
        }

        //Character Good Data
        for (int i = 0; i < l_Character_Good.Count; i++)
        {
            if (l_Character_Good[i].GetPos().z >= v3_Pos_Add.z)
            {
                l_Character_Good[i].Set_Pos_Add(0, 0, 1);
            }
        }

        //Character Neutral Data
        for (int i = 0; i < l_Character_Neutral.Count; i++)
        {
            if (l_Character_Neutral[i].GetPos().z >= v3_Pos_Add.z)
            {
                l_Character_Neutral[i].Set_Pos_Add(0, 0, 1);
            }
        }

        //Character Bad Data
        for (int i = 0; i < l_Character_Bad.Count; i++)
        {
            if (l_Character_Bad[i].GetPos().z >= v3_Pos_Add.z)
            {
                l_Character_Bad[i].Set_Pos_Add(0, 0, 1);
            }
        }

        Set_World_Size_Add(0, 0, 1);
    }

    #endregion

    #endregion

    #endregion

    //============================================================================================================ Block Move

    #region Block Move Data 

    #region Block Move Add 

    public void Set_Block_Move_Add(IsoDataMove cl_Data_Move)
    {
        l_Block_Move.Add(cl_Data_Move);
    }

    public void Set_Block_Move_Add(GameObject cl_Move)
    {
        l_Block_Move.Add(new IsoDataMove(cl_Move));
    }

    public void Set_Block_Move_List(List<IsoDataMove> l_Move)
    {
        l_Block_Move = l_Move;
    }

    #endregion

    #region Block Move Get 

    public int GetBlock_MoveCount()
    {
        if (l_Block_Move == null)
        {
            l_Block_Move = new List<IsoDataMove>();

            return 0;
        }

        return l_Block_Move.Count;
    }

    public IsoDataMove GetBlock_Move(int i_Move_Index)
    {
        if (i_Move_Index < 0 || i_Move_Index >= GetBlock_MoveCount())
        {
            return null;
        }

        return l_Block_Move[i_Move_Index];
    }

    public List<IsoDataMove> GetBlock_Move_List()
    {
        return l_Block_Move;
    }

    #endregion

    #endregion

    //============================================================================================================ Block Join To

    #region Block Join-To Data 

    #region Block Join-To Add 

    public void Set_Block_JoinTo_Add(IsoDataJoinTo cl_Data_JoinTo)
    {
        l_Block_JoinTo.Add(cl_Data_JoinTo);
    }

    public void Set_Block_JoinTo_Add(GameObject cl_JoinTo)
    {
        l_Block_JoinTo.Add(new IsoDataJoinTo(cl_JoinTo));
    }

    public void Set_Block_JoinTo_List(List<IsoDataJoinTo> l_JoinTo)
    {
        l_Block_JoinTo = l_JoinTo;
    }

    #endregion

    #region Block Join Data Get 

    public int GetBlock_JoinToCount()
    {
        if (l_Block_JoinTo == null)
        {
            l_Block_JoinTo = new List<IsoDataJoinTo>();

            return 0;
        }

        return l_Block_JoinTo.Count;
    }

    public IsoDataJoinTo GetBlock_JoinTo(int i_JoinTo_Index)
    {
        if (i_JoinTo_Index < 0 || i_JoinTo_Index >= GetBlock_JoinToCount())
        {
            return null;
        }

        return l_Block_JoinTo[i_JoinTo_Index];
    }

    public List<IsoDataJoinTo> GetBlock_JoinTo_List()
    {
        return l_Block_JoinTo;
    }

    #endregion

    #endregion

    //============================================================================================================ Block Switch To

    #region Block Switch-To Data 

    #region Block Switch-To Add 

    public void Set_Block_SwitchTo_Add(IsoDataSwitchTo cl_Data_SwitchTo)
    {
        l_Block_SwitchTo.Add(cl_Data_SwitchTo);
    }

    public void Set_Block_SwitchTo_Add(GameObject g_SwitchTo)
    {
        l_Block_SwitchTo.Add(new IsoDataSwitchTo(g_SwitchTo));
    }

    public void Set_Block_SwitchTo_List(List<IsoDataSwitchTo> l_SwitchTo)
    {
        l_Block_SwitchTo = l_SwitchTo;
    }

    #endregion

    #region Block Switch-To Get 

    public int GetBlock_SwitchToCount()
    {
        return l_Block_SwitchTo.Count;
    }

    public IsoDataSwitchTo GetBlock_SwitchTo(int i_SwitchTo_Index)
    {
        return l_Block_SwitchTo[i_SwitchTo_Index];
    }

    public List<IsoDataSwitchTo> GetBlock_SwitchTo_List()
    {
        return l_Block_SwitchTo;
    }

    #endregion

    #endregion

    //============================================================================================================ Block Message

    #region Block Message Data 

    #region Block Message Add 

    public void Set_Block_Message_Add(IsoDataMessage cl_Data_Message)
    {
        l_Block_Message.Add(cl_Data_Message);
    }

    public void Set_Block_Message_Add(GameObject g_Message)
    {
        l_Block_Message.Add(new IsoDataMessage(g_Message));
    }

    public void Set_Block_Message_List(List<IsoDataMessage> l_Message)
    {
        l_Block_Message = l_Message;
    }

    #endregion

    #region Block Message Get 

    public int GetBlock_MessageCount()
    {
        return l_Block_Message.Count;
    }

    public IsoDataMessage GetBlock_Message(int i_Message_Index)
    {
        return l_Block_Message[i_Message_Index];
    }

    public List<IsoDataMessage> GetBlock_Message_List()
    {
        return l_Block_Message;
    }

    #endregion

    #endregion

    //============================================================================================================ Block Teleport

    #region Block Teleport Data 

    #region Block Teleport Add 

    public void Set_Block_Teleport_Add(IsoDataTeleport cl_Data_Teleport)
    {
        l_Block_Teleport.Add(cl_Data_Teleport);
    }

    public void Set_Block_Teleport_Add(GameObject g_Teleport)
    {
        l_Block_Teleport.Add(new IsoDataTeleport(g_Teleport));
    }

    public void Set_Block_Teleport_List(List<IsoDataTeleport> l_Teleport)
    {
        l_Block_Teleport = l_Teleport;
    }

    #endregion

    #region Block Teleport Get 

    public int GetBlock_TeleportCount()
    {
        return l_Block_Teleport.Count;
    }

    public IsoDataTeleport GetBlock_Teleport(int i_Teleport_Index)
    {
        return l_Block_Teleport[i_Teleport_Index];
    }

    public List<IsoDataTeleport> GetBlock_Teleport_List()
    {
        return l_Block_Teleport;
    }

    #endregion

    #endregion

    #endregion

    //============================================================================================================ Character

    #region Character Data 

    //============================================================================================================ Character Player

    #region Character Player 

    #region Character Player Add 

    public void Set_UI_Player_Add(IsoDataBlock cl_Data_Character)
    {
        l_Character_Player.Add(cl_Data_Character);
    }

    public void Set_UI_Player_Add(GameObject g_Character)
    {
        l_Character_Player.Add(new IsoDataBlock(g_Character));
    }

    public void Set_UI_Player_List(List<IsoDataBlock> l_Character)
    {
        l_Character_Player = l_Character;
    }

    #endregion

    #region Character Player Get 

    public int GetCharacter_PlayerCount()
    {
        return l_Character_Player.Count;
    }

    public IsoDataBlock GetCharacter_Player(int i_Character_Index)
    {
        return l_Character_Player[i_Character_Index];
    }

    public List<IsoDataBlock> GetCharacter_Player_List()
    {
        return l_Character_Player;
    }

    #endregion

    #endregion

    //============================================================================================================ Character Good

    #region Character Good 

    #region Character Good Add 

    public void Set_Character_Good_Add(IsoDataBlock cl_Data_Character)
    {
        l_Character_Good.Add(cl_Data_Character);
    }

    public void Set_Character_Good_Add(GameObject g_Character)
    {
        l_Character_Good.Add(new IsoDataBlock(g_Character));
    }

    public void Set_Character_Good_List(List<IsoDataBlock> l_Character)
    {
        l_Character_Good = l_Character;
    }

    #endregion

    #region Character Good Get 

    public int GetCharacter_GoodCount()
    {
        return l_Character_Good.Count;
    }

    public IsoDataBlock GetCharacter_Good(int i_Character_Index)
    {
        return l_Character_Good[i_Character_Index];
    }

    public List<IsoDataBlock> GetCharacter_Good_List()
    {
        return l_Character_Good;
    }

    #endregion

    #endregion

    //============================================================================================================ Character Neutral

    #region Character Neutral 

    #region Character Neutral Add 

    public void Set_Character_Neutral_Add(IsoDataBlock cl_Data_Character)
    {
        l_Character_Neutral.Add(cl_Data_Character);
    }

    public void Set_Character_Neutral_Add(GameObject g_Character)
    {
        l_Character_Neutral.Add(new IsoDataBlock(g_Character));
    }

    public void Set_Character_Neutral_List(List<IsoDataBlock> l_Character)
    {
        l_Character_Neutral = l_Character;
    }

    #endregion

    #region Character Neutral Get 

    public int GetCharacter_NeutralCount()
    {
        return l_Character_Neutral.Count;
    }

    public IsoDataBlock GetCharacter_Neutral(int i_Character_Index)
    {
        return l_Character_Neutral[i_Character_Index];
    }

    public List<IsoDataBlock> GetCharacter_Neutral_List()
    {
        return l_Character_Neutral;
    }

    #endregion

    #endregion

    //============================================================================================================ Character Bad

    #region Character Bad 

    #region Character Bad Add 

    public void Set_Character_Bad_Add(IsoDataBlock cl_Data_Character)
    {
        l_Character_Bad.Add(cl_Data_Character);
    }

    public void Set_Character_Bad_Add(GameObject g_Character)
    {
        l_Character_Bad.Add(new IsoDataBlock(g_Character));
    }

    public void Set_Character_Bad_List(List<IsoDataBlock> l_Character)
    {
        l_Character_Bad = l_Character;
    }

    #endregion

    #region Character Bad Get 

    public int GetCharacter_BadCount()
    {
        return l_Character_Bad.Count;
    }

    public IsoDataBlock GetCharacter_Bad(int i_Character_Index)
    {
        return l_Character_Bad[i_Character_Index];
    }

    public List<IsoDataBlock> GetCharacter_Bad_List()
    {
        return l_Character_Bad;
    }

    #endregion

    #endregion

    #endregion
}
