using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IsoWorldRenderer))]
public class IsoWorld : MonoBehaviour
{
    #region World Primary

    [Header("World Primary")]

    [Tooltip("World is Active (Block-Move can Active)")]
    [SerializeField]
    private bool m_WorldIsActive = false;

    [Tooltip("World is Hop (Block-Move can Hop)")]
    [SerializeField]
    private bool m_WorldIsHop = false;

    [Tooltip("is World Generated")]
    private bool m_WorldIsGenerated = false;

    #endregion

    #region World Fix

    [Header("World Fix")]

    [Tooltip("Fix Distance between Square")]
    [SerializeField]
    private Vector3 v3_Fix_Square = new Vector3(1, 1, 1);

    [Tooltip("Fix Offset of World")]
    [SerializeField]
    private Vector3 v3_Fix_Offset = new Vector3(0, 0, 0);

    #endregion

    #region World Data

    [Header("World Data")]

    #region World Data Primary

    [Tooltip("Block Matrix World")]
    private List<List<List<GameObject>>> l_Block_Primary;

    #endregion

    #region World Data List

    [Tooltip("Move Pos Primary of Block-on-Matrix List")]
    private List<Vector3Int> l_Block_Move;

    [Tooltip("Join-To Pos Primary of Block-on-Matrix List")]
    private List<Vector3Int> l_Block_JoinTo;

    [Tooltip("Character Player Pos Primary of Character-on-Matrix List")]
    private List<Vector3Int> l_Character_Player;

    [Tooltip("Character Good Pos Primary of Character-on-Matrix List")]
    private List<Vector3Int> l_Character_Good;

    [Tooltip("Character Neutral Pos Primary of Character-on-Matrix List")]
    private List<Vector3Int> l_Character_Neutral;

    [Tooltip("Character Bad Pos Primary of Character-on-Matrix List")]
    private List<Vector3Int> l_Character_Bad;

    #endregion

    #endregion

    #region World File

    [Header("World Data Resources")]

    [SerializeField]
    private string s_World_Folder = "";

    [SerializeField]
    private char c_Key = '|';

    #endregion

    #region World Teleport

    private string s_Teleport_World = "";

    private Vector3Int v3_Teleport_Spawm = new Vector3Int();

    #endregion

    private IsoDataWorld cl_Memory;

    private IsoWorldRenderer cl_WorldRenderer;

    private void Awake()
    {
        cl_Memory = new IsoDataWorld();

        cl_WorldRenderer = GetComponent<IsoWorldRenderer>();

        Set_List_New();
    }

    private void Set_List_New()
    {
        l_Block_Primary = new List<List<List<GameObject>>>();

        l_Block_Move = new List<Vector3Int>();
        l_Block_JoinTo = new List<Vector3Int>();

        l_Character_Player = new List<Vector3Int>();
        l_Character_Good = new List<Vector3Int>();
        l_Character_Neutral = new List<Vector3Int>();
        l_Character_Bad = new List<Vector3Int>();
    }

    //============================================================================================================ Memory

    #region World Memory 

    #region World Memory Set 

    public void Set_Memory_Origin(string s_World_Name, Vector3Int v3_World_Size)
    {
        cl_Memory = new IsoDataWorld(s_World_Name, v3_World_Size, cl_WorldRenderer);
    }

    public void Set_Memory_withGenerated()
    {
        cl_Memory = new IsoDataWorld(GetMemory().GetWorld_Name(), l_Block_Primary);
    }

    public void Set_Memory_withGenerated(string s_World_Name)
    {
        cl_Memory = new IsoDataWorld(s_World_Name, l_Block_Primary);
    }

    public void Set_Memory(IsoDataWorld cl_Data_World)
    {
        cl_Memory = cl_Data_World;
    }

    #endregion

    #region World Memory Get 

    public IsoDataWorld GetMemory_withOrigin(string s_World_Name, Vector3Int v3_World_Size)
    {
        return new IsoDataWorld(s_World_Name, v3_World_Size, cl_WorldRenderer);
    }

    public IsoDataWorld GetMemory_withGenerated()
    {
        return new IsoDataWorld(GetMemory().GetWorld_Name(), l_Block_Primary);
    }

    public IsoDataWorld GetMemory_withGenerated(string s_World_Name)
    {
        return new IsoDataWorld(s_World_Name, l_Block_Primary);
    }

    public IsoDataWorld GetMemory()
    {
        return cl_Memory;
    }

    #endregion

    #endregion

    //============================================================================================================ World

    #region World  

    #region World Generate 

    /// <summary>
    /// Generate World with World Data from Memory (Destroy World Firstly)
    /// </summary>
    public void Set_World_Generate_fromMemory()
    {
        Set_World_Destroy();

        //New Emty World Matrix

        l_Block_Primary = new List<List<List<GameObject>>>();
        for (int h = 0; h < cl_Memory.GetWorld_Size().z; h++)
        {
            l_Block_Primary.Add(new List<List<GameObject>>());
            for (int x = 0; x < cl_Memory.GetWorld_Size().x; x++)
            {
                l_Block_Primary[h].Add(new List<GameObject>());
                for (int y = 0; y < cl_Memory.GetWorld_Size().y; y++)
                {
                    l_Block_Primary[h][x].Add(null);
                }
            }
        }

        //Block Add
        for (int i = 0; i < cl_Memory.GetBlockCount(); i++)
        {
            Set_Block_Primary_Add(
                cl_Memory.GetBlock(i).GetPos(),
                cl_Memory.GetBlock(i).GetNameOrigin());
        }

        //Character Player Add
        for (int i = 0; i < cl_Memory.GetCharacter_PlayerCount(); i++)
        {
            Set_Block_Primary_Add(
                cl_Memory.GetCharacter_Player(i).GetPos(),
                cl_Memory.GetCharacter_Player(i).GetNameOrigin());
        }

        //Character Good Add
        for (int i = 0; i < cl_Memory.GetCharacter_GoodCount(); i++)
        {
            Set_Block_Primary_Add(
                cl_Memory.GetCharacter_Good(i).GetPos(),
                cl_Memory.GetCharacter_Good(i).GetNameOrigin());
        }

        //Character Neutral Add
        for (int i = 0; i < cl_Memory.GetCharacter_NeutralCount(); i++)
        {
            Set_Block_Primary_Add(
                cl_Memory.GetCharacter_Neutral(i).GetPos(),
                cl_Memory.GetCharacter_Neutral(i).GetNameOrigin());
        }

        //Character Bad Add
        for (int i = 0; i < cl_Memory.GetCharacter_BadCount(); i++)
        {
            Set_Block_Primary_Add(
                cl_Memory.GetCharacter_Bad(i).GetPos(),
                cl_Memory.GetCharacter_Bad(i).GetNameOrigin());
        }

        //Block Move Add
        for (int i = 0; i < cl_Memory.GetBlock_MoveCount(); i++)
        {
            Set_MoveBlock_Primary_Active_Add(
                cl_Memory.GetBlock_Move(i).GetBlock().GetPos(),
                cl_Memory.GetBlock_Move(i));
        }

        //Block Join-To Add
        for (int i = 0; i < cl_Memory.GetBlock_JoinToCount(); i++)
        {
            Set_JoinToBlock_Primary_Active_Add(
                cl_Memory.GetBlock_JoinTo(i).GetBlock().GetPos(),
                cl_Memory.GetBlock_JoinTo(i).GetJoinTo_Pos());
        }

        //Block Switch-To Add
        for (int i = 0; i < cl_Memory.GetBlock_SwitchToCount(); i++)
        {
            Set_SwitchToBlock_Primary_Active_Add(
                cl_Memory.GetBlock_SwitchTo(i).GetBlock().GetPos(),
                cl_Memory.GetBlock_SwitchTo(i));
        }

        //Block Message Add
        for (int i = 0; i < cl_Memory.GetBlock_MessageCount(); i++)
        {
            Set_MessageBlock_Primary_Active_Add(
                cl_Memory.GetBlock_Message(i).GetBlock().GetPos(),
                cl_Memory.GetBlock_Message(i));
        }

        //Block Teleport Add
        for (int i = 0; i < cl_Memory.GetBlock_TeleportCount(); i++)
        {
            Set_TeleportBlock_Primary_Active_Add(
                cl_Memory.GetBlock_Teleport(i).GetBlock().GetPos(),
                cl_Memory.GetBlock_Teleport(i));
        }

        m_WorldIsGenerated = true;

        //Debug.Log("Set_World_Generate_fromMemory: World is Generated!");
    }

    public bool GetWorldIsGenerated()
    {
        return m_WorldIsGenerated;
    }

    public void Set_World_Destroy()
    {
        if (l_Block_Primary == null)
        {
            return;
        }

        if (l_Block_Primary.Count == 0)
        {
            return;
        }

        if (l_Block_Primary[0].Count == 0)
        {
            return;
        }

        if (l_Block_Primary[0][0].Count == 0)
        {
            return;
        }

        //World Destroy Done - World Close
        m_WorldIsGenerated = false;

        //Remove GameObject(s) in World
        for (int h = 0; h < l_Block_Primary.Count; h++)
        {
            for (int x = 0; x < l_Block_Primary[h].Count; x++)
            {
                for (int y = 0; y < l_Block_Primary[h][x].Count; y++)
                {
                    //Remove GameObject
                    Set_Remove_GameObject(GetPrimary_GameObject(new Vector3Int(x, y, h)));
                }
            }
        }

        Set_List_New();
    }

    public Vector3Int GetWorld_Size_Current()
    {
        if (l_Block_Primary == null)
        {
            return new Vector3Int();
        }

        if (l_Block_Primary.Count == 0)
        {
            return new Vector3Int();
        }

        return new Vector3Int(
            l_Block_Primary[0].Count,
            l_Block_Primary[0][0].Count,
            l_Block_Primary.Count);
    }

    #endregion

    #region World Matrix 

    #region World Primary and Current Set and Get

    public GameObject GetPrimary_GameObject(Vector3Int v3_Pos)
    {
        if (!GetWorld_inLimit(v3_Pos))
        {
            return null;
        }

        return l_Block_Primary[v3_Pos.z][v3_Pos.x][v3_Pos.y];
    }

    public bool GetPrimaryIsEmty(Vector3Int v3_Pos)
    {
        if (!GetWorld_inLimit(v3_Pos))
        {
            return true;
        }

        return GetPrimary_GameObject(v3_Pos) == null;
    }

    public GameObject GetCurrent_GameObject(Vector3Int v3_Pos)
    {
        if (!GetWorld_inLimit(v3_Pos))
        {
            return null;
        }

        if (!GetPrimaryIsEmty(v3_Pos))
        {
            //Block with Primary Pos and Current Pos on Same this Pos is not Emty
            if (GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlock>().GetPosOnMatrix_StayOnPrimary())
            {
                //If a Block or Character is Stay on Primary >> Return that as Block
                return GetPrimary_GameObject(v3_Pos);
            }
        }

        {
            //Move Block Current
            int i_Index_Move_Current = GetMoveBlock_Current_Index(v3_Pos);

            if (i_Index_Move_Current != -1)
            {
                return GetPrimary_GameObject(l_Block_Move[i_Index_Move_Current]);
            }

            //Join-To Block Current
            int i_Index_JoinTo_Current = GetJoinTo_Current_Index(v3_Pos);

            if (i_Index_JoinTo_Current != -1)
            {
                return GetPrimary_GameObject(l_Block_JoinTo[i_Index_JoinTo_Current]);
            }
        }

        {
            //Player Character Current
            int i_Index_Character_Player_Current = GetCharacterPlayer_Current_Index(v3_Pos);

            if (i_Index_Character_Player_Current != -1)
            {
                return GetCharacterPlayer_Primary_GameObject(l_Character_Player[i_Index_Character_Player_Current]);
            }

            //Good Character Current
            int i_Index_Character_Good_Current = GetCharacterGood_Current_Index(v3_Pos);

            if (i_Index_Character_Good_Current != -1)
            {
                return GetCharacterGood_Primary_GameObject(l_Character_Good[i_Index_Character_Good_Current]);
            }

            //Neutral Character Current
            int i_Index_Character_Neutral_Current = GetCharacterNeutral_Current_Index(v3_Pos);

            if (i_Index_Character_Neutral_Current != -1)
            {
                return GetCharacterNeutral_Primary_GameObject(l_Character_Neutral[i_Index_Character_Neutral_Current]);
            }

            //Bad Character Current
            int i_Index_Character_Bad_Current = GetCharacterBad_Current_Index(v3_Pos);

            if (i_Index_Character_Bad_Current != -1)
            {
                return GetCharacterBad_Primary_GameObject(l_Character_Bad[i_Index_Character_Bad_Current]);
            }
        }

        //Non-Block on this Pos
        return null;
    }

    public bool GetCurrentIsEmty(Vector3Int v3_Pos)
    {
        if (!GetWorld_inLimit(v3_Pos))
        {
            return true;
        }

        return GetCurrent_GameObject(v3_Pos) == null;
    }

    #endregion

    #region World Primary

    public void Set_Primary_Chance(Vector3Int v3_Pos, GameObject g_Block_or_Character)
    {
        if (!GetWorld_inLimit(v3_Pos))
        {
            return;
        }

        l_Block_Primary[v3_Pos.z][v3_Pos.x][v3_Pos.y] = g_Block_or_Character;

        //Transfer Data of all Join-To Exist to new Block
        Set_Block_Primary_Transfer_JoinTo(v3_Pos, g_Block_or_Character);
    }

    public void Set_Primary_Remove(Vector3Int v3_Pos, bool m_Remove_onList)
    {
        if (!GetWorld_inLimit(v3_Pos))
        {
            return;
        }

        if (GetPrimaryIsEmty(v3_Pos))
        {
            return;
        }

        //Remove Pos on List

        if (m_Remove_onList)
        {
            if (GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlock>().GetBlock_Check())
            {
                Set_MoveBlock_Primary_ListPos_Remove(v3_Pos);

                Set_JoinToBlock_Primary_ListPos_Remove(v3_Pos);
            }
            else
            if (GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlock>().GetCharacter_Check())
            {
                Set_CharacterPlayer_Primary_ListPos_Remove(v3_Pos);

                Set_CharacterGood_Primary_ListPos_Remove(v3_Pos);

                Set_CharacterNeutral_Primary_ListPos_Remove(v3_Pos);

                Set_CharacterBad_Primary_ListPos_Remove(v3_Pos);
            }
        }

        //Remove GameObject at Pos

        Set_Remove_GameObject(GetPrimary_GameObject(v3_Pos));

        Set_Primary_Chance(v3_Pos, null);
    }

    private void Set_Remove_GameObject(GameObject g_GameObject)
    {
        Class_Object.Set_GameObject_Destroy(g_GameObject);
    }

    #endregion

    #endregion

    #region World Matrix Chance 

    #region World Matrix Remove 

    /// <summary>
    /// Set Matrix World Remove by XY - TopBot - High
    /// </summary>
    /// <param name="v3_Pos"></param>
    public void Set_World_Remove_XY(Vector3Int v3_Pos)
    {
        if (!GetWorld_inLimit(v3_Pos))
        {
            return;
        }

        cl_Memory.Set_Block_Remove_High(v3_Pos);

        Set_World_Generate_fromMemory();
    }

    /// <summary>
    /// Set Matrix World Remove by XH - LR - Y
    /// </summary>
    /// <param name="v3_Pos"></param>
    public void Set_World_Remove_XH(Vector3Int v3_Pos)
    {
        if (!GetWorld_inLimit(v3_Pos))
        {
            return;
        }

        cl_Memory.Set_Block_Remove_Y(v3_Pos);

        Set_World_Generate_fromMemory();
    }

    /// <summary>
    /// Set Matrix World Remove by YH - UD - X
    /// </summary>
    /// <param name="v3_Pos"></param>
    public void Set_World_Remove_YH(Vector3Int v3_Pos)
    {
        if (!GetWorld_inLimit(v3_Pos))
        {
            return;
        }

        cl_Memory.Set_Block_Remove_X(v3_Pos);

        Set_World_Generate_fromMemory();
    }

    #endregion

    #region World Matrix Add 

    /// <summary>
    /// Set Matrix World Add by XY - TopBot - High
    /// </summary>
    /// <param name="v3_Pos"></param>
    public void Set_World_Add_XY(Vector3Int v3_Pos)
    {
        if (!GetWorld_inLimit(v3_Pos))
        {
            return;
        }

        cl_Memory.Set_Block_Add_High(v3_Pos);

        Set_World_Generate_fromMemory();
    }

    /// <summary>
    /// Set Matrix World Add by XH - LR - Y
    /// </summary>
    /// <param name="v3_Pos"></param>
    public void Set_World_Add_XH(Vector3Int v3_Pos)
    {
        if (!GetWorld_inLimit(v3_Pos))
        {
            return;
        }

        cl_Memory.Set_Block_Add_Y(v3_Pos);

        Set_World_Generate_fromMemory();
    }

    /// <summary>
    /// Set Matrix World Add by YH - UD - X
    /// </summary>
    /// <param name="v3_Pos"></param>
    public void Set_World_Add_YH(Vector3Int v3_Pos)
    {
        if (!GetWorld_inLimit(v3_Pos))
        {
            return;
        }

        cl_Memory.Set_Block_Add_X(v3_Pos);

        Set_World_Generate_fromMemory();
    }

    #endregion

    #endregion

    #region World Matrix Limit 

    public bool GetWorld_inLimit(Vector3Int v3_Pos)
    {
        return GetWorld_inLimit(v3_Pos, IsoClassDir.v3_None);
    }

    public bool GetWorld_inLimit(Vector3Int v3_Pos, Vector3Int v3_Dir)
    {
        //Limit mean in Count of List [0..n-1]

        //If out-Limit X
        if (v3_Pos.x + v3_Dir.x > l_Block_Primary[v3_Pos.z].Count - 1 || v3_Pos.x + v3_Dir.x < 0)
        {
            //Debug.LogError("GetWorld_inLimit: Pos " + v3_Pos + " + Dir " + v3_Dir + " = " + (v3_Pos + v3_Dir) + " is out of Limit X [0.." + (l_Block[v3_Pos.z].Count - 1).ToString() + "]!");
            return false;
        }
        //If out-Limit Y
        if (v3_Pos.y + v3_Dir.y > l_Block_Primary[v3_Pos.z][v3_Pos.x].Count - 1 || v3_Pos.y + v3_Dir.y < 0)
        {
            //Debug.LogError("GetWorld_inLimit: Pos " + v3_Pos + " + Dir " + v3_Dir + " = " + (v3_Pos + v3_Dir) + " is out of Limit Y [0.." + (l_Block[v3_Pos.z][v3_Pos.x].Count - 1).ToString() + "]!");
            return false;
        }
        //If out-Limit Z
        if (v3_Pos.z + v3_Dir.z > l_Block_Primary.Count - 1 || v3_Pos.z + v3_Dir.z < 0)
        {
            //Debug.LogError("GetWorld_inLimit: Pos " + v3_Pos + " + Dir " + v3_Dir + " = " + (v3_Pos + v3_Dir) + " is out of Limit Z [0.." + (l_Block.Count - 1).ToString() + "]!");
            return false;
        }

        //is in-Limit
        return true;
    }

    #endregion

    #region World Active 

    public void Set_WorldIsActive_Chance()
    {
        m_WorldIsActive = !m_WorldIsActive;
    }

    public void Set_WorldIsActive_Chance(bool m_Move_Active)
    {
        m_WorldIsActive = m_Move_Active;
    }

    public bool GetWorldIsActive()
    {
        return m_WorldIsActive;
    }

    #endregion

    #region World Hop 

    public void Set_World_Hop_Active()
    {
        for (int i = 0; i < l_Block_Move.Count; i++)
        {
            GetPrimary_GameObject(l_Block_Move[i]).GetComponent<IsoBlockMove>().Set_HopIsActive();
        }
    }

    public void Set_WorldIsHop_Chance()
    {
        m_WorldIsHop = !m_WorldIsHop;
    }

    public void Set_WorldIsHop_Chance(bool m_Move_Hop)
    {
        m_WorldIsHop = m_Move_Hop;
    }

    public bool GetWorldIsHop()
    {
        return m_WorldIsHop;
    }

    #endregion

    #endregion

    //============================================================================================================ Block

    #region Block 

    //============================================================================================================ Block Primary

    #region Block Primary Add

    public void Set_Block_Primary_Add(Vector3Int v3_Pos_Primary, string s_Block_Or_Character_Name)
    {
        Set_Block_Primary_Add(v3_Pos_Primary, v3_Pos_Primary, s_Block_Or_Character_Name);
    }

    public void Set_Block_Primary_Add(Vector3Int v3_Pos_Primary, Vector3Int v3_Pos, string s_Block_Or_Character_Name)
    {
        if (!GetWorld_inLimit(v3_Pos_Primary) || !GetWorld_inLimit(v3_Pos))
        {
            return;
        }

        //Create new Block GameObject
        GameObject g_Block_Primary = Class_Object.Set_GameObject_Create(
            cl_WorldRenderer.GetCombine(s_Block_Or_Character_Name),
            transform);

        if (g_Block_Primary == null)
        {
            return;
        }

        if (g_Block_Primary.GetComponent<IsoBlock>() == null)
        {
            g_Block_Primary.AddComponent<IsoBlock>();
        }

        g_Block_Primary.GetComponent<IsoBlock>().Set_World(this);

        g_Block_Primary.GetComponent<IsoBlock>().Set_PosOnMatrix_Primary(v3_Pos_Primary);
        g_Block_Primary.GetComponent<IsoBlock>().Set_Pos(v3_Pos);

        g_Block_Primary.GetComponent<IsoBlock>().Set_Imformation(
            cl_WorldRenderer.GetCombine_Imformation(s_Block_Or_Character_Name));

        g_Block_Primary.SetActive(true);

        //Check Block Exist at this Pos
        if (!GetPrimaryIsEmty(v3_Pos_Primary))
        {
            //Exist Block is Block
            if (GetPrimary_GameObject(v3_Pos_Primary).GetComponent<IsoBlock>().GetBlock_Check())
            {
                //New Block is Block
                if (g_Block_Primary.GetComponent<IsoBlock>().GetImformation().GetType_Main().Equals(IsoClassBlock.s_Block))
                {
                    //Transfer Data of Exist Block Move, Join-To, Switch-To, Message and Teleport to New Block
                    Set_Block_Primary_Transfer(
                        GetPrimary_GameObject(v3_Pos_Primary),
                        g_Block_Primary,
                        out g_Block_Primary);

                    //Transfer Data of all Join-To Exist to new Block
                    Set_Block_Primary_Transfer_JoinTo(v3_Pos_Primary, g_Block_Primary);

                    //Remove Block, but not in List
                    Set_Primary_Remove(v3_Pos_Primary, false);
                }
                else
                //New Block is Character
                if (g_Block_Primary.GetComponent<IsoBlock>().GetImformation().GetType_Main().Equals(IsoClassBlock.s_Character))
                {
                    //Remove Block and in List
                    Set_Primary_Remove(v3_Pos_Primary, true);
                }
            }
            else
            //Exist Block is Character
            if (GetPrimary_GameObject(v3_Pos_Primary).GetComponent<IsoBlock>().GetCharacter_Check())
            {
                //Remove Block and in List
                Set_Primary_Remove(v3_Pos_Primary, true);
            }
        }

        //Add New Block to World
        Set_Primary_Chance(v3_Pos_Primary, g_Block_Primary);

        //New Block is Character
        if (g_Block_Primary.GetComponent<IsoBlock>().GetImformation().GetType_Main().Equals(IsoClassBlock.s_Character))
        {
            //Add new Pos of Character to List
            if (g_Block_Primary.GetComponent<IsoBlock>().GetImformation().GetType().Equals(IsoClassBlock.s_Character_Player))
            {
                Set_CharacterPlayer_Primary_ListPos_Add(v3_Pos_Primary);
            }
            else
                if (g_Block_Primary.GetComponent<IsoBlock>().GetImformation().GetType().Equals(IsoClassBlock.s_Character_Good))
            {
                Set_CharacterGood_Primary_ListPos_Add(v3_Pos_Primary);
            }
            else
                if (g_Block_Primary.GetComponent<IsoBlock>().GetImformation().GetType().Equals(IsoClassBlock.s_Character_Neutral))
            {
                Set_CharacterNeutral_Primary_ListPos_Add(v3_Pos_Primary);
            }
            else
                if (g_Block_Primary.GetComponent<IsoBlock>().GetImformation().GetType().Equals(IsoClassBlock.s_Character_Bad))
            {
                Set_CharacterBad_Primary_ListPos_Add(v3_Pos_Primary);
            }
        }
    }

    #endregion

    #region Block Primary Transfer

    /// <summary>
    /// Transfer Data of Old Block Move, Join-To, Switch-To, Message and Teleport to New Block
    /// </summary>
    /// <param name="g_Block_From"></param>
    /// <param name="g_Block_To"></param>
    /// <param name="g_Block_Get"></param>
    private void Set_Block_Primary_Transfer(GameObject g_Block_From, GameObject g_Block_To, out GameObject g_Block_Get)
    {
        if (g_Block_From.GetComponent<IsoBlockMove>() != null)
        {
            if (g_Block_To.GetComponent<IsoBlockMove>() == null)
            {
                g_Block_To.AddComponent<IsoBlockMove>();
            }

            g_Block_To.GetComponent<IsoBlockMove>().Set_List(g_Block_From.GetComponent<IsoBlockMove>().GetList());
            g_Block_To.GetComponent<IsoBlockMove>().Set_StatusIsActive(g_Block_From.GetComponent<IsoBlockMove>().GetStatusIsActive());

            g_Block_To.GetComponent<IsoBlockMove>().Set_HopIsAllow(GetWorldIsHop());
        }

        if (g_Block_From.GetComponent<IsoBlockJoinTo>() != null)
        {
            if (g_Block_To.GetComponent<IsoBlockJoinTo>() == null)
            {
                g_Block_To.AddComponent<IsoBlockJoinTo>();
            }

            g_Block_To.GetComponent<IsoBlockJoinTo>().Set_JoinTo(g_Block_From.GetComponent<IsoBlockJoinTo>().GetJoinTo());
        }

        if (g_Block_From.GetComponent<IsoBlockSwitchTo>() != null)
        {
            if (g_Block_To.GetComponent<IsoBlockSwitchTo>() == null)
            {
                g_Block_To.AddComponent<IsoBlockSwitchTo>();
            }

            g_Block_To.GetComponent<IsoBlockSwitchTo>().Set_List(g_Block_From.GetComponent<IsoBlockSwitchTo>().GetList());
        }

        if (g_Block_From.GetComponent<IsoBlockMessage>() != null)
        {
            if (g_Block_To.GetComponent<IsoBlockMessage>() == null)
            {
                g_Block_To.AddComponent<IsoBlockMessage>();
            }

            g_Block_To.GetComponent<IsoBlockMessage>().Set_List(g_Block_From.GetComponent<IsoBlockMessage>().GetList());
        }

        g_Block_Get = g_Block_To;
    }

    /// <summary>
    /// Transfer Data of all Join-To Exist to new Block at Pos
    /// </summary>
    /// <param name="v3_Pos"></param>
    /// <param name="g_Block_JoinTo"></param>
    private void Set_Block_Primary_Transfer_JoinTo(Vector3Int v3_Pos, GameObject g_Block_JoinTo)
    {
        for (int i = 0; i < GetJoinTo_Primary_ListPos().Count; i++)
        {
            if (GetPrimary_GameObject(GetJoinTo_Primary_ListPos()[i]).GetComponent<IsoBlockJoinTo>().GetJoinTo_Pos_Primary() == v3_Pos)
            {
                GetPrimary_GameObject(GetJoinTo_Primary_ListPos()[i]).GetComponent<IsoBlockJoinTo>().Set_JoinTo(g_Block_JoinTo);
            }
        }
    }

    #endregion

    //============================================================================================================ Block Move

    #region Block Move 

    #region Block Move Data 

    #region Block Move Data Add 

    public void Set_MoveBlock_Primary_Active_Add(Vector3Int v3_Pos, IsoDataMove cl_Data_Move)
    {
        //Add Componenet "Move"
        if (GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlockMove>() == null)
        {
            GetPrimary_GameObject(v3_Pos).AddComponent<IsoBlockMove>();

            GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlockMove>().Set_HopIsAllow(GetWorldIsHop());
        }

        GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlockMove>().Set_List(cl_Data_Move.GetList());

        GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlockMove>().Set_StatusIsActive(cl_Data_Move.GetStatus_Numberic());

        Set_MoveBlock_Primary_ListPos_Add(v3_Pos);
    }

    public void Set_MoveBlock_Primary_Active_Add(Vector3Int v3_Pos, Vector3Int v3_Move_Dir, int i_Move_Length, float f_Move_Speed)
    {
        //Add Componenet "Move"
        if (GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlockMove>() == null)
        {
            GetPrimary_GameObject(v3_Pos).AddComponent<IsoBlockMove>();

            GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlockMove>().Set_HopIsAllow(GetWorldIsHop());
        }

        GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlockMove>().Set_Add(v3_Move_Dir, i_Move_Length, f_Move_Speed);

        Set_MoveBlock_Primary_ListPos_Add(v3_Pos);
    }

    public void Set_MoveBlock_Primary_Active_Add_Rev(Vector3Int v3_Pos)
    {
        //Add Componenet "Move"
        if (GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlockMove>() == null)
        {
            GetPrimary_GameObject(v3_Pos).AddComponent<IsoBlockMove>();

            GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlockMove>().Set_HopIsAllow(GetWorldIsHop());
        }

        GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlockMove>().Set_Add_Res();
    }

    #endregion

    #region Block Move Data Chance 

    public void Set_MoveBlock_Primary_Active_Chance(Vector3Int v3_Pos, int i_Move_Index, Vector3Int v3_Move_Dir, int i_Move_Length, float f_Move_Speed)
    {
        //Add Componenet "Move"
        if (GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlockMove>() == null)
        {
            GetPrimary_GameObject(v3_Pos).AddComponent<IsoBlockMove>();
        }

        GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlockMove>().Set_Chance(i_Move_Index, v3_Move_Dir, i_Move_Length, f_Move_Speed);
    }

    #endregion

    #region Block Move Data Remove 

    public void Set_MoveBlock_Primary_Active_Remove(Vector3Int v3_Pos, int i_Move_Index)
    {
        //Add Componenet "Move"
        if (GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlockMove>() == null)
        {
            GetPrimary_GameObject(v3_Pos).AddComponent<IsoBlockMove>();
        }

        GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlockMove>().Set_Remove(i_Move_Index);

        if (GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlockMove>().GetCount() == 0)
        {
            Set_MoveBlock_Primary_ListPos_Remove(v3_Pos);
        }
    }

    public void Set_MoveBlock_Primary_Active_Remove_Lastest(Vector3Int v3_Pos)
    {
        //Add Componenet "Move"
        if (GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlockMove>() == null)
        {
            GetPrimary_GameObject(v3_Pos).AddComponent<IsoBlockMove>();
        }

        if (GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlockMove>().GetCount() == 0)
        {
            return;
        }

        GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlockMove>().Set_Remove_Lastest();

        if (GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlockMove>().GetCount() == 0)
        {
            Set_MoveBlock_Primary_ListPos_Remove(v3_Pos);
        }
    }

    #endregion

    #region Block Move Data Status 

    public void Set_MoveBlock_Primary_Status_Chance(Vector3Int v3_Pos)
    {
        //Add Componenet "Move"
        if (GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlockMove>() == null)
        {
            GetPrimary_GameObject(v3_Pos).AddComponent<IsoBlockMove>();
        }

        GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlockMove>().Set_StatusIsActive_Chance();
    }

    public void Set_MoveBlock_Primary_Status_Chance(Vector3Int v3_Pos, bool m_Move_Status)
    {
        //Add Componenet "Move"
        if (GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlockMove>() == null)
        {
            GetPrimary_GameObject(v3_Pos).AddComponent<IsoBlockMove>();
        }

        GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlockMove>().Set_StatusIsActive(m_Move_Status);
    }

    #endregion

    #endregion

    #region Block Move List 

    #region Block Move List Primary 

    public List<Vector3Int> GetMoveBlock_Primary_ListPos()
    {
        return l_Block_Move;
    }

    public void Set_MoveBlock_Primary_ListPos_Add(Vector3Int v3_Pos)
    {
        if (GetMoveBlock_Primary_Index(v3_Pos) == -1)
        {
            l_Block_Move.Add(v3_Pos);
        }
    }

    public void Set_MoveBlock_Primary_ListPos_Remove(Vector3Int v3_Pos)
    {
        int i_Move_Index = GetMoveBlock_Primary_Index(v3_Pos);

        if (i_Move_Index != -1)
        {
            l_Block_Move.RemoveAt(i_Move_Index);
        }

        for (int i = 0; i < GetJoinTo_Primary_ListPos().Count; i++)
        {
            if (GetPrimary_GameObject(GetJoinTo_Primary_ListPos()[i]).GetComponent<IsoBlockJoinTo>().GetJoinTo_Pos_Primary() == v3_Pos)
            {
                GetPrimary_GameObject(GetJoinTo_Primary_ListPos()[i]).GetComponent<IsoBlockJoinTo>().Set_JoinToBlock_Remove();
            }
        }
    }

    #endregion

    #region Block Primary Move List 

    public GameObject GetMoveBlock_Primary_GameObject(Vector3Int v3_Pos)
    {
        int i_Move_Index = GetMoveBlock_Primary_Index(v3_Pos);

        if (i_Move_Index != -1)
        {
            return GetPrimary_GameObject(l_Block_Move[i_Move_Index]);
        }

        return null;
    }

    public GameObject GetMoveBlock_Primary_GameObject(int i_Move_Index)
    {
        if (i_Move_Index < 0 || i_Move_Index >= l_Block_Move.Count)
        {
            return null;
        }

        return GetPrimary_GameObject(l_Block_Move[i_Move_Index]);
    }

    public int GetMoveBlock_Primary_Index(Vector3Int v3_Pos)
    {
        for (int i = 0; i < l_Block_Move.Count; i++)
        {
            if (l_Block_Move[i] == v3_Pos)
            {
                return i;
            }
        }
        return -1;
    }

    #endregion

    #region Block Current Move List 

    public GameObject GetMoveBlock_Current_GameObject(Vector3Int v3_Pos)
    {
        int i_Move_Index = GetMoveBlock_Current_Index(v3_Pos);

        if (i_Move_Index != -1)
        {
            return GetPrimary_GameObject(l_Block_Move[i_Move_Index]);
        }

        return null;
    }

    public int GetMoveBlock_Current_Index(Vector3Int v3_Pos)
    {
        for (int i = 0; i < l_Block_Move.Count; i++)
        {
            if (GetPrimary_GameObject(l_Block_Move[i]).GetComponent<IsoBlock>().GetPosOnMatrix_Current() == v3_Pos)
            {
                return i;
            }
        }
        return -1;
    }

    #endregion

    #endregion

    #endregion

    //============================================================================================================ Block Join To

    #region Block Join-To 

    #region Block Join-To Data 

    public void Set_JoinToBlock_Primary_Active_Add(Vector3Int v3_Pos, Vector3Int v3_JoinTo_Pos)
    {
        //Add Componenet "Join-To"
        if (GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlockJoinTo>() == null)
        {
            GetPrimary_GameObject(v3_Pos).AddComponent<IsoBlockJoinTo>();
        }

        GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlockJoinTo>().Set_JoinTo(GetPrimary_GameObject(v3_JoinTo_Pos));

        Set_JoinToBlock_Primary_ListPos_Add(v3_Pos);
    }

    public void Set_JoinToBlock_Primary_Active_Remove(Vector3Int v3_Pos)
    {
        //Add Componenet "Join-To"
        if (GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlockJoinTo>() == null)
        {
            GetPrimary_GameObject(v3_Pos).AddComponent<IsoBlockJoinTo>();
        }

        GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlockJoinTo>().Set_JoinToBlock_Remove();

        Set_JoinToBlock_Primary_ListPos_Remove(v3_Pos);
    }

    #endregion

    #region Block Join-To List 

    #region Block Join-To List Primary 

    public List<Vector3Int> GetJoinTo_Primary_ListPos()
    {
        return l_Block_JoinTo;
    }

    public void Set_JoinToBlock_Primary_ListPos_Add(Vector3Int v3_Pos)
    {
        if (GetJoinTo_Primary_Index(v3_Pos) == -1)
        {
            l_Block_JoinTo.Add(v3_Pos);
        }
    }

    public void Set_JoinToBlock_Primary_ListPos_Remove(Vector3Int v3_Pos)
    {
        int i_JoinTo_Index = GetJoinTo_Primary_Index(v3_Pos);

        if (i_JoinTo_Index != -1)
        {
            l_Block_JoinTo.RemoveAt(i_JoinTo_Index);
        }
    }

    #endregion

    #region Block Primary Join-To List 

    public GameObject GetJoinTo_Primary_GameObject(Vector3Int v3_Pos)
    {
        int i_JoinTo_Index = GetJoinTo_Primary_Index(v3_Pos);

        if (i_JoinTo_Index != -1)
        {
            return GetPrimary_GameObject(l_Block_JoinTo[i_JoinTo_Index]);
        }

        return null;
    }

    public int GetJoinTo_Primary_Index(Vector3Int v3_Pos)
    {
        for (int i = 0; i < l_Block_JoinTo.Count; i++)
        {
            if (l_Block_JoinTo[i] == v3_Pos)
            {
                return i;
            }
        }
        return -1;
    }

    #endregion

    #region Block Current Join-To List 

    public GameObject GetJoinTo_Current_GameObject(Vector3Int v3_Pos)
    {
        int i_JoinTo_Index = GetJoinTo_Current_Index(v3_Pos);

        if (i_JoinTo_Index != -1)
        {
            return GetPrimary_GameObject(l_Block_JoinTo[i_JoinTo_Index]);
        }

        return null;
    }

    public int GetJoinTo_Current_Index(Vector3Int v3_Pos)
    {
        for (int i = 0; i < l_Block_JoinTo.Count; i++)
        {
            if (GetPrimary_GameObject(l_Block_JoinTo[i]).GetComponent<IsoBlock>().GetPosOnMatrix_Current() == v3_Pos)
            {
                return i;
            }
        }
        return -1;
    }

    #endregion

    #endregion

    #endregion

    //============================================================================================================ Block Switch To

    #region Block Switch-To 

    #region Block Switch-To Add 

    public void Set_SwitchToBlock_Primary_Active_Add(Vector3Int v3_Pos, IsoDataSwitchTo cl_Data_SwitchTo)
    {
        //Add Componenet "Switch-To"
        if (GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlockSwitchTo>() == null)
        {
            GetPrimary_GameObject(v3_Pos).AddComponent<IsoBlockSwitchTo>();
        }

        GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlockSwitchTo>().Set_List(cl_Data_SwitchTo.GetList());
    }

    public void Set_SwitchToBlock_Primary_Active_Add(Vector3Int v3_Pos, Vector3Int v3_SwitchTo_Pos)
    {
        //Add Componenet "Switch-To"
        if (GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlockSwitchTo>() == null)
        {
            GetPrimary_GameObject(v3_Pos).AddComponent<IsoBlockSwitchTo>();
        }

        GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlockSwitchTo>().Set_Add(v3_SwitchTo_Pos);
    }

    #endregion

    #region Block Switch-To Remove 

    public void Set_SwitchToBlock_Primary_Active_Remove(Vector3Int v3_Pos, int i_SwitchTo_Index)
    {
        //Add Componenet "Switch-To"
        if (GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlockSwitchTo>() == null)
        {
            GetPrimary_GameObject(v3_Pos).AddComponent<IsoBlockSwitchTo>();
        }

        if (GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlockSwitchTo>().GetCount() == 0)
        {
            return;
        }

        GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlockSwitchTo>().Set_Remove(i_SwitchTo_Index);
    }

    public void Set_SwitchToBlock_Primary_Active_Remove_Lastest(Vector3Int v3_Pos)
    {
        //Add Componenet "Switch-To"
        if (GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlockSwitchTo>() == null)
        {
            GetPrimary_GameObject(v3_Pos).AddComponent<IsoBlockSwitchTo>();
        }

        if (GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlockSwitchTo>().GetCount() == 0)
        {
            return;
        }

        GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlockSwitchTo>().Set_Remove_Lastest();
    }

    #endregion

    #region Block Switch-To Chance 

    public void Set_SwitchToBlock_Primary_Active_Chance(Vector3Int v3_Pos, int i_SwitchTo_Index, Vector3Int v3_SwitchTo_Pos)
    {
        //Add Componenet "Switch-To"
        if (GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlockSwitchTo>() == null)
        {
            GetPrimary_GameObject(v3_Pos).AddComponent<IsoBlockSwitchTo>();
        }

        if (GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlockSwitchTo>().GetCount() == 0)
        {
            return;
        }

        GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlockSwitchTo>().Set_Chance(i_SwitchTo_Index, v3_SwitchTo_Pos);
    }

    #endregion

    #endregion

    //============================================================================================================ Block Message

    #region Block Message 

    #region Block Message Add 

    public void Set_MessageBlock_Primary_Active_Add(Vector3Int v3_Pos, IsoDataMessage cl_Data_Message)
    {
        //Add Componenet "Message"
        if (GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlockMessage>() == null)
        {
            GetPrimary_GameObject(v3_Pos).AddComponent<IsoBlockMessage>();
        }

        GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlockMessage>().Set_List(cl_Data_Message.GetList());
    }

    public void Set_MessageBlock_Primary_Active_Add(Vector3Int v3_Pos, IsoDataMessageSingle cl_Data_Message_Single)
    {
        //Add Componenet "Message"
        if (GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlockMessage>() == null)
        {
            GetPrimary_GameObject(v3_Pos).AddComponent<IsoBlockMessage>();
        }

        GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlockMessage>().Set_Add(cl_Data_Message_Single);
    }

    #endregion

    #region Block Message Remove 

    public void Set_MessageBlock_Primary_Active_Remove(Vector3Int v3_Pos, int i_Message_Index)
    {
        //Add Componenet "Message"
        if (GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlockMessage>() == null)
        {
            GetPrimary_GameObject(v3_Pos).AddComponent<IsoBlockMessage>();
        }

        if (GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlockMessage>().GetCount() == 0)
        {
            return;
        }

        GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlockMessage>().Set_Remove(i_Message_Index);
    }

    public void Set_MessageBlock_Primary_Active_Remove_Lastest(Vector3Int v3_Pos)
    {
        //Add Componenet "Message"
        if (GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlockMessage>() == null)
        {
            GetPrimary_GameObject(v3_Pos).AddComponent<IsoBlockMessage>();
        }

        if (GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlockMessage>().GetCount() == 0)
        {
            return;
        }

        GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlockMessage>().Set_Remove_Lastest();
    }

    #endregion

    #region Block Message Chance 

    public void Set_MessageBlock_Primary_Active_Chance(Vector3Int v3_Pos, int i_SwitchTo_Index, IsoDataMessageSingle cl_Data_Message_Single)
    {
        //Add Componenet "Message"
        if (GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlockMessage>() == null)
        {
            GetPrimary_GameObject(v3_Pos).AddComponent<IsoBlockMessage>();
        }

        if (GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlockMessage>().GetCount() == 0)
        {
            return;
        }

        GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlockMessage>().Set_Chance(i_SwitchTo_Index, cl_Data_Message_Single);
    }

    #endregion

    #endregion

    //============================================================================================================ Block Teleport

    #region Block Teleport 

    public void Set_TeleportBlock_Primary_Active_Add(Vector3Int v3_Pos, IsoDataTeleport cl_Data_Teleport)
    {
        //Add Componenet "Teleport"
        if (GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlockTeleport>() == null)
        {
            GetPrimary_GameObject(v3_Pos).AddComponent<IsoBlockTeleport>();
        }

        GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlockTeleport>().Set_Add(cl_Data_Teleport);
    }

    public void Set_TeleportBlock_Primary_Active_Remove(Vector3Int v3_Pos)
    {
        //Add Componenet "Teleport"
        if (GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlockTeleport>() == null)
        {
            GetPrimary_GameObject(v3_Pos).AddComponent<IsoBlockTeleport>();
        }

        GetPrimary_GameObject(v3_Pos).GetComponent<IsoBlockTeleport>().Set_Remove();
    }

    #endregion

    #endregion

    //============================================================================================================ Character

    #region Character 

    #region Character Player List 

    #region Character Player List Primary 

    public List<Vector3Int> GetCharacterPlayer_Primary_ListPos()
    {
        return l_Character_Player;
    }

    public Vector3Int GetCharacterPlayer_Primary_Pos(int i_Player_Index)
    {
        return l_Character_Player[i_Player_Index];
    }

    public void Set_CharacterPlayer_Primary_ListPos_Add(Vector3Int v3_Pos)
    {
        if (GetCharacterPlayer_Primary_Index(v3_Pos) == -1)
        {
            l_Character_Player.Add(v3_Pos);
        }
    }

    public void Set_CharacterPlayer_Primary_ListPos_Remove(Vector3Int v3_Pos)
    {
        int i_Player_Index = GetCharacterPlayer_Primary_Index(v3_Pos);

        if (i_Player_Index != -1)
        {
            l_Character_Player.RemoveAt(i_Player_Index);
        }
    }

    #endregion

    #region Character Primary Player List 

    public GameObject GetCharacterPlayer_Primary_GameObject(Vector3Int v3_Pos)
    {
        return GetCharacterPlayer_Primary_GameObject(GetCharacterPlayer_Primary_Index(v3_Pos));
    }

    public GameObject GetCharacterPlayer_Primary_GameObject(int i_Player_Index)
    {
        if (i_Player_Index < 0 || i_Player_Index > l_Character_Player.Count - 1)
        {
            return null;
        }

        return GetPrimary_GameObject(l_Character_Player[i_Player_Index]);
    }

    public int GetCharacterPlayer_Primary_Index(Vector3Int v3_Pos)
    {
        for (int i = 0; i < l_Character_Player.Count; i++)
        {
            if (l_Character_Player[i] == v3_Pos)
            {
                return i;
            }
        }
        return -1;
    }

    #endregion

    #region Character Current Player List 

    public GameObject GetCharacterPlayer_Current_GameObject(Vector3Int v3_Pos)
    {
        int i_Player_Index = GetCharacterPlayer_Current_Index(v3_Pos);

        if (i_Player_Index != -1)
        {
            return GetPrimary_GameObject(l_Character_Player[i_Player_Index]);
        }

        return null;
    }

    public int GetCharacterPlayer_Current_Index(Vector3Int v3_Pos)
    {
        for (int i = 0; i < l_Character_Player.Count; i++)
        {
            if (GetPrimary_GameObject(l_Character_Player[i]).GetComponent<IsoBlock>().GetPosOnMatrix_Current() == v3_Pos)
            {
                return i;
            }
        }
        return -1;
    }

    #endregion

    #endregion

    #region Character Good List 

    #region Character Good List Primary 

    public List<Vector3Int> GetCharacterGood_Primary_ListPos()
    {
        return l_Character_Good;
    }

    public Vector3Int GetCharacterGood_Primary_Pos(int i_Good_Index)
    {
        return l_Character_Good[i_Good_Index];
    }

    public void Set_CharacterGood_Primary_ListPos_Add(Vector3Int v3_Pos)
    {
        if (GetCharacterGood_Primary_Index(v3_Pos) == -1)
        {
            l_Character_Good.Add(v3_Pos);
        }
    }

    public void Set_CharacterGood_Primary_ListPos_Remove(Vector3Int v3_Pos)
    {
        int i_Good_Index = GetCharacterGood_Primary_Index(v3_Pos);

        if (i_Good_Index != -1)
        {
            l_Character_Good.RemoveAt(i_Good_Index);
        }
    }

    #endregion

    #region Character Primary Good List 

    public GameObject GetCharacterGood_Primary_GameObject(Vector3Int v3_Pos)
    {
        return GetCharacterGood_Primary_GameObject(GetCharacterGood_Primary_Index(v3_Pos));
    }

    public GameObject GetCharacterGood_Primary_GameObject(int i_Good_Index)
    {
        if (i_Good_Index < 0 || i_Good_Index > l_Character_Good.Count - 1)
        {
            return null;
        }

        return GetPrimary_GameObject(l_Character_Good[i_Good_Index]);
    }

    public int GetCharacterGood_Primary_Index(Vector3Int v3_Pos)
    {
        for (int i = 0; i < l_Character_Good.Count; i++)
        {
            if (l_Character_Good[i] == v3_Pos)
            {
                return i;
            }
        }
        return -1;
    }

    #endregion

    #region Character Current Good List 

    public GameObject GetCharacterGood_Current_GameObject(Vector3Int v3_Pos)
    {
        int i_Good_Index = GetCharacterGood_Current_Index(v3_Pos);

        if (i_Good_Index != -1)
        {
            return GetPrimary_GameObject(l_Character_Good[i_Good_Index]);
        }

        return null;
    }

    public int GetCharacterGood_Current_Index(Vector3Int v3_Pos)
    {
        for (int i = 0; i < l_Character_Good.Count; i++)
        {
            if (GetPrimary_GameObject(l_Character_Good[i]).GetComponent<IsoBlock>().GetPosOnMatrix_Current() == v3_Pos)
            {
                return i;
            }
        }
        return -1;
    }

    #endregion

    #endregion

    #region Character Neutral List 

    #region Character Neutral List Primary 

    public List<Vector3Int> GetCharacterNeutral_Primary_ListPos()
    {
        return l_Character_Neutral;
    }

    public Vector3Int GetCharacterNeutral_Primary_Pos(int i_Neutral_Index)
    {
        return l_Character_Neutral[i_Neutral_Index];
    }

    public void Set_CharacterNeutral_Primary_ListPos_Add(Vector3Int v3_Pos)
    {
        if (GetCharacterNeutral_Primary_Index(v3_Pos) == -1)
        {
            l_Character_Neutral.Add(v3_Pos);
        }
    }

    public void Set_CharacterNeutral_Primary_ListPos_Remove(Vector3Int v3_Pos)
    {
        int i_Neutral_Index = GetCharacterNeutral_Primary_Index(v3_Pos);

        if (i_Neutral_Index != -1)
        {
            l_Character_Neutral.RemoveAt(i_Neutral_Index);
        }
    }

    #endregion

    #region Character Primary Neutral List 

    public GameObject GetCharacterNeutral_Primary_GameObject(Vector3Int v3_Pos)
    {
        return GetCharacterNeutral_Primary_GameObject(GetCharacterNeutral_Primary_Index(v3_Pos));
    }

    public GameObject GetCharacterNeutral_Primary_GameObject(int i_Neutral_Index)
    {
        if (i_Neutral_Index < 0 || i_Neutral_Index > l_Character_Neutral.Count - 1)
        {
            return null;
        }

        return GetPrimary_GameObject(l_Character_Neutral[i_Neutral_Index]);
    }

    public int GetCharacterNeutral_Primary_Index(Vector3Int v3_Pos)
    {
        for (int i = 0; i < l_Character_Neutral.Count; i++)
        {
            if (l_Character_Neutral[i] == v3_Pos)
            {
                return i;
            }
        }
        return -1;
    }

    #endregion

    #region Character Current Neutral List 

    public GameObject GetCharacterNeutral_Current_GameObject(Vector3Int v3_Pos)
    {
        int i_Neutral_Index = GetCharacterNeutral_Current_Index(v3_Pos);

        if (i_Neutral_Index != -1)
        {
            return GetPrimary_GameObject(l_Character_Neutral[i_Neutral_Index]);
        }

        return null;
    }

    public int GetCharacterNeutral_Current_Index(Vector3Int v3_Pos)
    {
        for (int i = 0; i < l_Character_Neutral.Count; i++)
        {
            if (GetPrimary_GameObject(l_Character_Neutral[i]).GetComponent<IsoBlock>().GetPosOnMatrix_Current() == v3_Pos)
            {
                return i;
            }
        }
        return -1;
    }

    #endregion

    #endregion

    #region Character Bad List 

    #region Character Bad List Primary 

    public List<Vector3Int> GetCharacterBad_Primary_ListPos()
    {
        return l_Character_Bad;
    }

    public Vector3Int GetCharacterBad_Primary_Pos(int i_Bad_Index)
    {
        return l_Character_Bad[i_Bad_Index];
    }

    public void Set_CharacterBad_Primary_ListPos_Add(Vector3Int v3_Pos)
    {
        if (GetCharacterBad_Primary_Index(v3_Pos) == -1)
        {
            l_Character_Bad.Add(v3_Pos);
        }
    }

    public void Set_CharacterBad_Primary_ListPos_Remove(Vector3Int v3_Pos)
    {
        int i_Bad_Index = GetCharacterBad_Primary_Index(v3_Pos);

        if (i_Bad_Index != -1)
        {
            l_Character_Bad.RemoveAt(i_Bad_Index);
        }
    }

    #endregion

    #region Character Primary Bad List 

    public GameObject GetCharacterBad_Primary_GameObject(Vector3Int v3_Pos)
    {
        return GetCharacterBad_Primary_GameObject(GetCharacterBad_Primary_Index(v3_Pos));
    }

    public GameObject GetCharacterBad_Primary_GameObject(int i_Bad_Index)
    {
        if (i_Bad_Index < 0 || i_Bad_Index > l_Character_Bad.Count - 1)
        {
            return null;
        }

        return GetPrimary_GameObject(l_Character_Bad[i_Bad_Index]);
    }

    public int GetCharacterBad_Primary_Index(Vector3Int v3_Pos)
    {
        for (int i = 0; i < l_Character_Bad.Count; i++)
        {
            if (l_Character_Bad[i] == v3_Pos)
            {
                return i;
            }
        }
        return -1;
    }

    #endregion

    #region Character Current Bad List 

    public GameObject GetCharacterBad_Current_GameObject(Vector3Int v3_Pos)
    {
        int i_Bad_Index = GetCharacterBad_Current_Index(v3_Pos);

        if (i_Bad_Index != -1)
        {
            return GetPrimary_GameObject(l_Character_Bad[i_Bad_Index]);
        }

        return null;
    }

    public int GetCharacterBad_Current_Index(Vector3Int v3_Pos)
    {
        for (int i = 0; i < l_Character_Bad.Count; i++)
        {
            if (GetPrimary_GameObject(l_Character_Bad[i]).
                GetComponent<IsoBlock>().GetPosOnMatrix_Current() == v3_Pos)
            {
                return i;
            }
        }
        return -1;
    }

    #endregion

    #endregion

    #endregion

    //============================================================================================================ File

    #region File

    public IsoDataWorld GetMemory_File_Resources(string s_World_Folder, string s_World_Name, bool m_TempFile)
    {
        Set_World_Destroy();

        Class_FileIO cl_FileIO = new Class_FileIO();

        cl_FileIO.Set_Data_Read_Clear();

        cl_FileIO.Set_Data_Read_Resource_Start(
            s_World_Folder,
            GetMemory_File_FileName_Resources(s_World_Name, m_TempFile));

        //==========================================

        IsoDataWorld cl_Data_World = new IsoDataWorld();

        cl_Data_World.Set_World_Name(s_World_Name);

        //========================================== World-Size

        cl_FileIO.GetData_Read_Auto_String();

        {
            //Dencypt
            string s_Data_Size = cl_FileIO.GetData_Read_Auto_String();
            List<string> l_Data_Size = ClassString.GetDataDencyptString(s_Data_Size, c_Key);
            Vector3Int v3_Size = new Vector3Int(
                int.Parse(l_Data_Size[0]),
                int.Parse(l_Data_Size[1]),
                int.Parse(l_Data_Size[2]));

            //Add Data
            cl_Data_World.Set_World_Size(v3_Size);
        }

        //========================================== Block Primary-Data

        cl_FileIO.GetData_Read_Auto_String();

        {
            int i_WorldCount = cl_FileIO.GetData_Read_Auto_Int();

            for (int i = 0; i < i_WorldCount; i++)
            {
                //Read String from File (X:Y:High:Name)
                string s_Data_Block = cl_FileIO.GetData_Read_Auto_String();

                //Dencypt String from File
                List<string> l_DataDencypt = ClassString.GetDataDencyptString(s_Data_Block, c_Key);

                //Varible Data
                Vector3Int v3_Pos = new Vector3Int(
                    int.Parse(l_DataDencypt[0]),
                    int.Parse(l_DataDencypt[1]),
                    int.Parse(l_DataDencypt[2]));
                string s_Name = l_DataDencypt[3];

                //Add Data
                IsoDataBlock cl_Data_Block = new IsoDataBlock(v3_Pos, s_Name);

                cl_Data_World.Set_Block_Add(cl_Data_Block);
            }
        }

        //========================================== Block Move-Data

        cl_FileIO.GetData_Read_Auto_String();

        {
            int i_MoveCount = cl_FileIO.GetData_Read_Auto_Int();

            for (int i = 0; i < i_MoveCount; i++)
            {
                //Read String from File (X:Y:High:Status)
                string s_Data_Move = cl_FileIO.GetData_Read_Auto_String();

                //Dencypt String from File
                List<string> l_Data_MoveDencypt = ClassString.GetDataDencyptString(s_Data_Move, c_Key);

                //Varible Data
                Vector3Int v3_Pos = new Vector3Int(
                    int.Parse(l_Data_MoveDencypt[0]),
                    int.Parse(l_Data_MoveDencypt[1]),
                    int.Parse(l_Data_MoveDencypt[2]));
                int i_Status_Numberic = int.Parse(l_Data_MoveDencypt[3]);
                int i_Move_DirCount = int.Parse(l_Data_MoveDencypt[4]);

                //Add Data (Pos and Status)
                IsoDataMove cl_Data_Move = new IsoDataMove(new IsoDataBlock(v3_Pos, null));
                cl_Data_Move.Set_Status_Numberic(i_Status_Numberic);

                for (int j = 0; j < i_Move_DirCount; j++)
                {
                    //Read String from File (Dir:Length:Speed)
                    string s_Data_Dir = cl_FileIO.GetData_Read_Auto_String();

                    //Dencypt String from File
                    List<string> l_Data_DirDencypt = ClassString.GetDataDencyptString(s_Data_Dir, c_Key);

                    //Varible Data
                    Vector3Int v3_Dir = IsoClassDir.GetDir_Dencyt(l_Data_DirDencypt[0]);
                    //Vector3Int v3_Dir = new Vector3Int(
                    //    int.Parse(l_Data_DirDencypt[0]), 
                    //    int.Parse(l_Data_DirDencypt[1]), 
                    //    int.Parse(l_Data_DirDencypt[2]));
                    int i_Length = int.Parse(l_Data_DirDencypt[1]);
                    float f_Speed = float.Parse(l_Data_DirDencypt[2]);

                    //Add Data (Dir:Length:Speed)
                    cl_Data_Move.Set_Add(new IsoDataMoveSingle(v3_Dir, i_Length, f_Speed));
                }

                //Add Final Data
                cl_Data_World.Set_Block_Move_Add(cl_Data_Move);
            }
        }

        //========================================== Block Join-Data

        cl_FileIO.GetData_Read_Auto_String();

        {
            int i_JoinCount = cl_FileIO.GetData_Read_Auto_Int();

            for (int i = 0; i < i_JoinCount; i++)
            {
                //Read String from File (X:Y:High)
                string s_Data_Join = cl_FileIO.GetData_Read_Auto_String();

                //Dencypt String from File
                List<string> l_Data_JoinDencypt = ClassString.GetDataDencyptString(s_Data_Join, c_Key);

                //Varible Data
                Vector3Int v3_Pos = new Vector3Int(
                    int.Parse(l_Data_JoinDencypt[0]),
                    int.Parse(l_Data_JoinDencypt[1]),
                    int.Parse(l_Data_JoinDencypt[2]));
                Vector3Int v3_JoinTo_Pos = new Vector3Int(
                    int.Parse(l_Data_JoinDencypt[3]),
                    int.Parse(l_Data_JoinDencypt[4]),
                    int.Parse(l_Data_JoinDencypt[5]));

                //Add Data
                IsoDataJoinTo cl_Data_JoinTo = new IsoDataJoinTo(new IsoDataBlock(v3_Pos, null));
                cl_Data_JoinTo.Set_JoinToBlock_Pos(v3_JoinTo_Pos);


                cl_Data_World.Set_Block_JoinTo_Add(cl_Data_JoinTo);
            }
        }

        //========================================== Block Switch-To-Data

        cl_FileIO.GetData_Read_Auto_String();

        int i_SwitchToCount = cl_FileIO.GetData_Read_Auto_Int();

        for (int i = 0; i < i_SwitchToCount; i++)
        {
            //Read String from File (X:Y:High:Status)
            string s_Data_SwitchTo = cl_FileIO.GetData_Read_Auto_String();

            //Dencypt String from File
            List<string> l_Data_SwitchToDencypt = ClassString.GetDataDencyptString(s_Data_SwitchTo, c_Key);

            //Varible Data
            Vector3Int v3_Pos = new Vector3Int(
                int.Parse(l_Data_SwitchToDencypt[0]),
                int.Parse(l_Data_SwitchToDencypt[1]),
                int.Parse(l_Data_SwitchToDencypt[2]));
            int i_SwitchTo_DirCount = int.Parse(l_Data_SwitchToDencypt[3]);

            //Add Data (Pos and Status)
            IsoDataSwitchTo cl_Data_SwitchTo = new IsoDataSwitchTo(new IsoDataBlock(v3_Pos, null));

            for (int j = 0; j < i_SwitchTo_DirCount; j++)
            {
                //Read String from File (Dir:Length:Speed)
                string s_Data_Pos = cl_FileIO.GetData_Read_Auto_String();

                //Dencypt String from File
                List<string> l_Data_PosDencypt = ClassString.GetDataDencyptString(s_Data_Pos, c_Key);

                //Varible Data
                Vector3Int v3_SwitchTo_Pos = new Vector3Int(
                    int.Parse(l_Data_PosDencypt[0]),
                    int.Parse(l_Data_PosDencypt[1]),
                    int.Parse(l_Data_PosDencypt[2]));

                //Add Data (Dir:Length:Speed)
                cl_Data_SwitchTo.Set_Add(v3_SwitchTo_Pos);
            }

            //Add Final Data
            cl_Data_World.Set_Block_SwitchTo_Add(cl_Data_SwitchTo);
        }

        //========================================== Block Message-Data

        cl_FileIO.GetData_Read_Auto_String();

        {
            int i_MessageCount = cl_FileIO.GetData_Read_Auto_Int();

            for (int i = 0; i < i_MessageCount; i++)
            {
                //Read String from File (X:Y:High)
                string s_Data_Message = cl_FileIO.GetData_Read_Auto_String();

                //Dencypt String from File
                List<string> l_Data_MessageDencypt = ClassString.GetDataDencyptString(s_Data_Message, c_Key);

                //Varible Data
                Vector3Int v3_Pos = new Vector3Int(
                    int.Parse(l_Data_MessageDencypt[0]),
                    int.Parse(l_Data_MessageDencypt[1]),
                    int.Parse(l_Data_MessageDencypt[2]));
                int i_Message_SingleCount = int.Parse(l_Data_MessageDencypt[3]);

                //Add Data (Pos)
                IsoDataMessage cl_Data_Message = new IsoDataMessage(new IsoDataBlock(v3_Pos, null));

                for (int j = 0; j < i_Message_SingleCount; j++)
                {
                    //Read String from File (Dir:Length:Speed)
                    string s_Data_Message_Single = cl_FileIO.GetData_Read_Auto_String();

                    //Dencypt String from File
                    List<string> l_Data_Message_SingleDencypt = ClassString.GetDataDencyptString(s_Data_Message_Single, c_Key);

                    //Varible Data
                    string s_Name = l_Data_Message_SingleDencypt[0];
                    string s_Message = l_Data_Message_SingleDencypt[1];

                    //Add Data (Name and Message)
                    cl_Data_Message.Set_Add(new IsoDataMessageSingle(s_Name, s_Message));
                }

                //Add Final Data
                cl_Data_World.Set_Block_Message_Add(cl_Data_Message);
            }
        }

        //========================================== Block Teleport-Data

        cl_FileIO.GetData_Read_Auto_String();

        {
            int i_TeleportCount = cl_FileIO.GetData_Read_Auto_Int();

            for (int i = 0; i < i_TeleportCount; i++)
            {
                //Read String from File (X:Y:High:Name)
                string s_Data_Teleport = cl_FileIO.GetData_Read_Auto_String();

                //Dencypt String from File
                List<string> l_DataDencypt = ClassString.GetDataDencyptString(s_Data_Teleport, c_Key);

                //Varible Data
                Vector3Int v3_Pos = new Vector3Int(
                    int.Parse(l_DataDencypt[0]),
                    int.Parse(l_DataDencypt[1]),
                    int.Parse(l_DataDencypt[2]));
                string s_Teleport_World_Name = l_DataDencypt[3];
                Vector3Int v3_Teleport_World_Spawm = new Vector3Int(
                    int.Parse(l_DataDencypt[4]),
                    int.Parse(l_DataDencypt[5]),
                    int.Parse(l_DataDencypt[6]));

                //Add Data
                IsoDataTeleport cl_Data_Teleport = new IsoDataTeleport(new IsoDataBlock(v3_Pos, null));
                cl_Data_Teleport.Set_Add(s_Teleport_World_Name, v3_Teleport_World_Spawm);

                cl_Data_World.Set_Block_Teleport_Add(cl_Data_Teleport);
            }
        }

        //========================================== Character Player-Data

        cl_FileIO.GetData_Read_Auto_String();

        {
            int i_PlayerCount = cl_FileIO.GetData_Read_Auto_Int();

            for (int i = 0; i < i_PlayerCount; i++)
            {
                //Read String from File (X:Y:High:Name)
                string s_Data_Character = cl_FileIO.GetData_Read_Auto_String();

                //Dencypt String from File
                List<string> l_DataDencypt = ClassString.GetDataDencyptString(s_Data_Character, c_Key);

                //Varible Data
                Vector3Int v3_Pos = new Vector3Int(
                    int.Parse(l_DataDencypt[0]),
                    int.Parse(l_DataDencypt[1]),
                    int.Parse(l_DataDencypt[2]));
                string s_Player_Name = l_DataDencypt[3];

                //Add Data
                IsoDataBlock cl_Data_Player = new IsoDataBlock(v3_Pos, s_Player_Name);

                cl_Data_World.Set_UI_Player_Add(cl_Data_Player);
            }
        }

        //========================================== Character Good-Data

        cl_FileIO.GetData_Read_Auto_String();

        {
            int i_GoodCount = cl_FileIO.GetData_Read_Auto_Int();

            for (int i = 0; i < i_GoodCount; i++)
            {
                //Read String from File (X:Y:High:Name)
                string s_Data_Character = cl_FileIO.GetData_Read_Auto_String();

                //Dencypt String from File
                List<string> l_DataDencypt = ClassString.GetDataDencyptString(s_Data_Character, c_Key);

                //Varible Data
                Vector3Int v3_Pos = new Vector3Int(
                    int.Parse(l_DataDencypt[0]),
                    int.Parse(l_DataDencypt[1]),
                    int.Parse(l_DataDencypt[2]));
                string s_Good_Name = l_DataDencypt[3];

                //Add Data
                IsoDataBlock cl_Data_Good = new IsoDataBlock(v3_Pos, s_Good_Name);

                cl_Data_World.Set_Character_Good_Add(cl_Data_Good);
            }
        }

        //========================================== Character Neutral-Data

        cl_FileIO.GetData_Read_Auto_String();

        {
            int i_NeutralCount = cl_FileIO.GetData_Read_Auto_Int();

            for (int i = 0; i < i_NeutralCount; i++)
            {
                //Read String from File (X:Y:High:Name)
                string s_Data_Character = cl_FileIO.GetData_Read_Auto_String();

                //Dencypt String from File
                List<string> l_DataDencypt = ClassString.GetDataDencyptString(s_Data_Character, c_Key);

                //Varible Data
                Vector3Int v3_Pos = new Vector3Int(
                    int.Parse(l_DataDencypt[0]),
                    int.Parse(l_DataDencypt[1]),
                    int.Parse(l_DataDencypt[2]));
                string s_Neutral_Name = l_DataDencypt[3];

                //Add Data
                IsoDataBlock cl_Data_Neutral = new IsoDataBlock(v3_Pos, s_Neutral_Name);

                cl_Data_World.Set_Character_Neutral_Add(cl_Data_Neutral);
            }
        }

        //========================================== Character Bad-Data

        cl_FileIO.GetData_Read_Auto_String();

        {
            int i_BadCount = cl_FileIO.GetData_Read_Auto_Int();

            for (int i = 0; i < i_BadCount; i++)
            {
                //Read String from File (X:Y:High:Name)
                string s_Data_Character = cl_FileIO.GetData_Read_Auto_String();

                //Dencypt String from File
                List<string> l_DataDencypt = ClassString.GetDataDencyptString(s_Data_Character, c_Key);

                //Varible Data
                Vector3Int v3_Pos = new Vector3Int(
                    int.Parse(l_DataDencypt[0]),
                    int.Parse(l_DataDencypt[1]),
                    int.Parse(l_DataDencypt[2]));
                string s_Bad_Name = l_DataDencypt[3];

                //Add Data
                IsoDataBlock cl_Data_Bad = new IsoDataBlock(v3_Pos, s_Bad_Name);

                cl_Data_World.Set_Character_Bad_Add(cl_Data_Bad);
            }
        }

        return cl_Data_World;
    }

    public void Set_Memory_File_Resources(string s_World_Folder, IsoDataWorld cl_Data_World, bool m_TempFile)
    {
        Class_FileIO cl_FileIO = new Class_FileIO();

        cl_FileIO.Set_Data_Write_Clear();

        //========================================== World-Size

        cl_FileIO.Set_Data_Write_Add("*World-Size");

        {
            //Encypt Data
            List<string> l_Data_Size = new List<string>
            {
                cl_Data_World.GetWorld_Size().x.ToString(),
                cl_Data_World.GetWorld_Size().y.ToString(),
                cl_Data_World.GetWorld_Size().z.ToString()
            };

            //Varible Data
            string s_Data_Size = ClassString.GetDataEncypt(l_Data_Size, c_Key);

            cl_FileIO.Set_Data_Write_Add(s_Data_Size);
        }

        //========================================== Block Primary-Data

        cl_FileIO.Set_Data_Write_Add("*Block-Data");

        //Block Count
        cl_FileIO.Set_Data_Write_Add(cl_Data_World.GetBlockCount());

        for (int i = 0; i < cl_Data_World.GetBlockCount(); i++)
        {
            //Encypt Data
            List<string> l_Data_Square = new List<string>
            {
                cl_Data_World.GetBlock(i).GetPos().x.ToString(),
                cl_Data_World.GetBlock(i).GetPos().y.ToString(),
                cl_Data_World.GetBlock(i).GetPos().z.ToString(),
                cl_Data_World.GetBlock(i).GetNameOrigin()
            };

            //Varible Data
            string s_Data_Block = ClassString.GetDataEncypt(l_Data_Square, c_Key);

            //Add Data to File
            cl_FileIO.Set_Data_Write_Add(s_Data_Block);
        }

        //========================================== Block Move-Data

        cl_FileIO.Set_Data_Write_Add("*Move-Data");

        cl_FileIO.Set_Data_Write_Add(cl_Data_World.GetBlock_MoveCount());

        for (int i = 0; i < cl_Data_World.GetBlock_MoveCount(); i++)
        {
            //Encypt Data
            List<string> l_Data_Move = new List<string>
            {
                cl_Data_World.GetBlock_Move(i).GetBlock().GetPos().x.ToString(),
                cl_Data_World.GetBlock_Move(i).GetBlock().GetPos().y.ToString(),
                cl_Data_World.GetBlock_Move(i).GetBlock().GetPos().z.ToString(),
                cl_Data_World.GetBlock_Move(i).GetStatus_Numberic().ToString(),
                cl_Data_World.GetBlock_Move(i).GetCount().ToString()
            };

            //Varible Data
            string s_Data_Move = ClassString.GetDataEncypt(l_Data_Move, c_Key);

            cl_FileIO.Set_Data_Write_Add(s_Data_Move);

            for (int j = 0; j < cl_Data_World.GetBlock_Move(i).GetCount(); j++)
            {
                //Encypt Data
                List<string> l_Data_Dir = new List<string>
                {
                    IsoClassDir.GetDirEncypt(cl_Data_World.GetBlock_Move(i).GetList(j).GetDir()).ToString(),
                    //l_Data_Dir.Add(cl_Data_World.GetMove(i).GetList(j).GetDir().x.ToString());
                    //l_Data_Dir.Add(cl_Data_World.GetMove(i).GetList(j).GetDir().y.ToString());
                    //l_Data_Dir.Add(cl_Data_World.GetMove(i).GetList(j).GetDir().z.ToString());
                    cl_Data_World.GetBlock_Move(i).GetList(j).GetLength().ToString(),
                    cl_Data_World.GetBlock_Move(i).GetList(j).GetSpeed().ToString()
                };

                //Varible Data
                string s_Data_Dir = ClassString.GetDataEncypt(l_Data_Dir, c_Key);

                //Add Data to File
                cl_FileIO.Set_Data_Write_Add(s_Data_Dir);
            }
        }

        //========================================== Block Join-To-Data

        cl_FileIO.Set_Data_Write_Add("*Join-To-Data");

        cl_FileIO.Set_Data_Write_Add(cl_Data_World.GetBlock_JoinToCount());

        for (int i = 0; i < cl_Data_World.GetBlock_JoinToCount(); i++)
        {
            //Encypt Data
            List<string> l_Data_Join = new List<string>
            {
                cl_Data_World.GetBlock_JoinTo(i).GetBlock().GetPos().x.ToString(),
                cl_Data_World.GetBlock_JoinTo(i).GetBlock().GetPos().y.ToString(),
                cl_Data_World.GetBlock_JoinTo(i).GetBlock().GetPos().z.ToString(),
                cl_Data_World.GetBlock_JoinTo(i).GetJoinTo_Pos().x.ToString(),
                cl_Data_World.GetBlock_JoinTo(i).GetJoinTo_Pos().y.ToString(),
                cl_Data_World.GetBlock_JoinTo(i).GetJoinTo_Pos().z.ToString()
            };

            //Varible Data
            string s_Data_Join = ClassString.GetDataEncypt(l_Data_Join, c_Key);

            //Add Data to File
            cl_FileIO.Set_Data_Write_Add(s_Data_Join);
        }

        //========================================== Block Switch-To-Data

        cl_FileIO.Set_Data_Write_Add("*Switch-To-Data");

        cl_FileIO.Set_Data_Write_Add(cl_Data_World.GetBlock_SwitchToCount());

        for (int i = 0; i < cl_Data_World.GetBlock_SwitchToCount(); i++)
        {
            //Encypt Data
            List<string> l_Data_Switch = new List<string>
            {
                cl_Data_World.GetBlock_SwitchTo(i).GetBlock().GetPos().x.ToString(),
                cl_Data_World.GetBlock_SwitchTo(i).GetBlock().GetPos().y.ToString(),
                cl_Data_World.GetBlock_SwitchTo(i).GetBlock().GetPos().z.ToString(),
                cl_Data_World.GetBlock_SwitchTo(i).GetCount().ToString()
            };

            //Varible Data
            string s_Data_Switch = ClassString.GetDataEncypt(l_Data_Switch, c_Key);

            cl_FileIO.Set_Data_Write_Add(s_Data_Switch);

            for (int j = 0; j < cl_Data_World.GetBlock_SwitchTo(i).GetCount(); j++)
            {
                //Encypt Data
                List<string> l_Data_Dir = new List<string>
                {
                    cl_Data_World.GetBlock_SwitchTo(i).GetList(j).x.ToString(),
                    cl_Data_World.GetBlock_SwitchTo(i).GetList(j).y.ToString(),
                    cl_Data_World.GetBlock_SwitchTo(i).GetList(j).z.ToString()
                };

                //Varible Data
                string s_Data_Dir = ClassString.GetDataEncypt(l_Data_Dir, c_Key);

                //Add Data to File
                cl_FileIO.Set_Data_Write_Add(s_Data_Dir);
            }
        }

        //========================================== Block Message-Data

        cl_FileIO.Set_Data_Write_Add("*Message-Data");

        cl_FileIO.Set_Data_Write_Add(cl_Data_World.GetBlock_MessageCount());

        for (int i = 0; i < cl_Data_World.GetBlock_MessageCount(); i++)
        {
            //Encypt Data
            List<string> l_Data_Switch = new List<string>
            {
                cl_Data_World.GetBlock_Message(i).GetBlock().GetPos().x.ToString(),
                cl_Data_World.GetBlock_Message(i).GetBlock().GetPos().y.ToString(),
                cl_Data_World.GetBlock_Message(i).GetBlock().GetPos().z.ToString(),
                cl_Data_World.GetBlock_Message(i).GetCount().ToString()
            };

            //Varible Data
            string s_Data_Switch = ClassString.GetDataEncypt(l_Data_Switch, c_Key);

            cl_FileIO.Set_Data_Write_Add(s_Data_Switch);

            for (int j = 0; j < cl_Data_World.GetBlock_Message(i).GetCount(); j++)
            {
                //Encypt Data
                List<string> l_Data_Dir = new List<string>
                {
                    cl_Data_World.GetBlock_Message(i).GetList(j).GetName(),
                    cl_Data_World.GetBlock_Message(i).GetList(j).GetMessage()
                };

                //Varible Data
                string s_Data_Dir = ClassString.GetDataEncypt(l_Data_Dir, c_Key);

                //Add Data to File
                cl_FileIO.Set_Data_Write_Add(s_Data_Dir);
            }
        }

        //========================================== Block Teleport-Data

        cl_FileIO.Set_Data_Write_Add("*Teleport-Data");

        cl_FileIO.Set_Data_Write_Add(cl_Data_World.GetBlock_TeleportCount());

        for (int i = 0; i < cl_Data_World.GetBlock_TeleportCount(); i++)
        {
            //Encypt Data
            List<string> l_Data_Join = new List<string>
            {
                cl_Data_World.GetBlock_Teleport(i).GetBlock().GetPos().x.ToString(),
                cl_Data_World.GetBlock_Teleport(i).GetBlock().GetPos().y.ToString(),
                cl_Data_World.GetBlock_Teleport(i).GetBlock().GetPos().z.ToString(),
                cl_Data_World.GetBlock_Teleport(i).GetWorld_Name(),
                cl_Data_World.GetBlock_Teleport(i).GetPos().x.ToString(),
                cl_Data_World.GetBlock_Teleport(i).GetPos().y.ToString(),
                cl_Data_World.GetBlock_Teleport(i).GetPos().z.ToString()
            };

            //Varible Data
            string s_Data_Join = ClassString.GetDataEncypt(l_Data_Join, c_Key);

            //Add Data to File
            cl_FileIO.Set_Data_Write_Add(s_Data_Join);
        }

        //========================================== Character Player-Data

        cl_FileIO.Set_Data_Write_Add("*Player-Data");

        cl_FileIO.Set_Data_Write_Add(cl_Data_World.GetCharacter_PlayerCount());

        for (int i = 0; i < cl_Data_World.GetCharacter_PlayerCount(); i++)
        {
            //Encypt Data
            List<string> l_Data_Join = new List<string>
            {
                cl_Data_World.GetCharacter_Player(i).GetPos().x.ToString(),
                cl_Data_World.GetCharacter_Player(i).GetPos().y.ToString(),
                cl_Data_World.GetCharacter_Player(i).GetPos().z.ToString(),
                cl_Data_World.GetCharacter_Player(i).GetNameOrigin()
            };

            //Varible Data
            string s_Data_Join = ClassString.GetDataEncypt(l_Data_Join, c_Key);

            //Add Data to File
            cl_FileIO.Set_Data_Write_Add(s_Data_Join);
        }

        //========================================== Character Good-Data

        cl_FileIO.Set_Data_Write_Add("*Good-Data");

        cl_FileIO.Set_Data_Write_Add(cl_Data_World.GetCharacter_GoodCount());

        for (int i = 0; i < cl_Data_World.GetCharacter_GoodCount(); i++)
        {
            //Encypt Data
            List<string> l_Data_Join = new List<string>
            {
                cl_Data_World.GetCharacter_Good(i).GetPos().x.ToString(),
                cl_Data_World.GetCharacter_Good(i).GetPos().y.ToString(),
                cl_Data_World.GetCharacter_Good(i).GetPos().z.ToString(),
                cl_Data_World.GetCharacter_Good(i).GetNameOrigin()
            };

            //Varible Data
            string s_Data_Join = ClassString.GetDataEncypt(l_Data_Join, c_Key);

            //Add Data to File
            cl_FileIO.Set_Data_Write_Add(s_Data_Join);
        }

        //========================================== Character Neutral-Data

        cl_FileIO.Set_Data_Write_Add("*Neutral-Data");

        cl_FileIO.Set_Data_Write_Add(cl_Data_World.GetCharacter_NeutralCount());

        for (int i = 0; i < cl_Data_World.GetCharacter_NeutralCount(); i++)
        {
            //Encypt Data
            List<string> l_Data_Join = new List<string>
            {
                cl_Data_World.GetCharacter_Neutral(i).GetPos().x.ToString(),
                cl_Data_World.GetCharacter_Neutral(i).GetPos().y.ToString(),
                cl_Data_World.GetCharacter_Neutral(i).GetPos().z.ToString(),
                cl_Data_World.GetCharacter_Neutral(i).GetNameOrigin()
            };

            //Varible Data
            string s_Data_Join = ClassString.GetDataEncypt(l_Data_Join, c_Key);

            //Add Data to File
            cl_FileIO.Set_Data_Write_Add(s_Data_Join);
        }

        //========================================== Character Bad-Data

        cl_FileIO.Set_Data_Write_Add("*Bad-Data");

        cl_FileIO.Set_Data_Write_Add(cl_Data_World.GetCharacter_BadCount());

        for (int i = 0; i < cl_Data_World.GetCharacter_BadCount(); i++)
        {
            //Encypt Data
            List<string> l_Data_Join = new List<string>
            {
                cl_Data_World.GetCharacter_Bad(i).GetPos().x.ToString(),
                cl_Data_World.GetCharacter_Bad(i).GetPos().y.ToString(),
                cl_Data_World.GetCharacter_Bad(i).GetPos().z.ToString(),
                cl_Data_World.GetCharacter_Bad(i).GetNameOrigin()
            };

            //Varible Data
            string s_Data_Join = ClassString.GetDataEncypt(l_Data_Join, c_Key);

            //Add Data to File
            cl_FileIO.Set_Data_Write_Add(s_Data_Join);
        }

        //Write File Start
        cl_FileIO.Set_Data_Write_Resource_Start(
            s_World_Folder,
            //Class_String.GetStringReplace(cl_Data_World.GetWorld_Name(), " ", "_") + ((m_TempFile) ? "_Temp" : ""),
            GetMemory_File_FileName_Resources(cl_Data_World.GetWorld_Name(), m_TempFile));
    }

    public string GetMemory_File_FolderName_Resources()
    {
        return s_World_Folder;
    }

    public string GetMemory_File_FileName_Resources(string s_WorldName, bool m_TempFile)
    {
        return ClassString.GetStringReplace(s_WorldName, " ", "_") + ((m_TempFile) ? "_Temp" : "");
    }

    public void Set_File_Key(char c_Key)
    {
        this.c_Key = c_Key;
    }

    public char GetFile_Key()
    {
        return c_Key;
    }

    #endregion

    //============================================================================================================ Teleport

    #region Teleport

    public void Set_Teleport(string s_Teleport_World, Vector3Int v3_Teleport_Spawm)
    {
        this.s_Teleport_World = s_Teleport_World;
        this.v3_Teleport_Spawm = v3_Teleport_Spawm;
    }

    public string GetTeleport_World()
    {
        return s_Teleport_World;
    }

    public Vector3Int GetTeleport_Spawm()
    {
        return v3_Teleport_Spawm;
    }

    #endregion

    //============================================================================================================ ...

    #region Fix Single

    public Vector3 GetFix_Offset()
    {
        return v3_Fix_Offset;
    }

    public Vector3 GetFix_Square()
    {
        return v3_Fix_Square;
    }

    #endregion
}
