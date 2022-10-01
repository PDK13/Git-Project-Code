﻿using System.Collections.Generic;
using UnityEngine;

public class IsoWorldRenderer : MonoBehaviour
{
    #region Resource Prefab Folder Path

    [Header("Resource Prefab Folder Path")]

    #region Primary Block

    [SerializeField]
    private List<string> ls_Block_Ground;

    private List<List<GameObject>> lg_Block_Ground;

    [SerializeField]
    private List<string> ls_Block_Object;

    private List<List<GameObject>> lg_Block_Object;

    [SerializeField]
    private List<string> ls_Block_Item;

    private List<List<GameObject>> lg_Block_Item;

    #endregion

    #region Stair Block

    [SerializeField]
    private List<string> ls_Block_StairUD;

    private List<List<GameObject>> lg_Block_StairUD;

    [SerializeField]
    private List<string> ls_Block_StairLR;

    private List<List<GameObject>> lg_Block_StairLR;

    #endregion

    #region Character Block

    [SerializeField]
    private List<string> ls_Character_Player;

    private List<List<GameObject>> lg_Character_Player;

    [SerializeField]
    private List<string> ls_Character_Good;

    private List<List<GameObject>> lg_Character_Good;

    [SerializeField]
    private List<string> ls_Character_Neutral;

    private List<List<GameObject>> lg_Character_Neutral;

    [SerializeField]
    private List<string> ls_Character_Bad;

    private List<List<GameObject>> lg_Character_Bad;

    #endregion

    #endregion

    private void Awake()
    {
        Set_Data_FromResources();
    }

    private void Set_Data_FromResources()
    {
        //Block Primary

        lg_Block_Ground = new List<List<GameObject>>();
        for (int i = 0; i < ls_Block_Ground.Count; i++)
        {
            lg_Block_Ground.Add(new List<GameObject>());
            lg_Block_Ground[i] =
                Class_Object.GetResources_Prefab(ls_Block_Ground[i]);
        }

        lg_Block_Object = new List<List<GameObject>>();
        for (int i = 0; i < ls_Block_Object.Count; i++)
        {
            lg_Block_Object.Add(new List<GameObject>());
            lg_Block_Object[i] =
                Class_Object.GetResources_Prefab(ls_Block_Object[i]);
        }

        lg_Block_Item = new List<List<GameObject>>();
        for (int i = 0; i < ls_Block_Item.Count; i++)
        {
            lg_Block_Item.Add(new List<GameObject>());
            lg_Block_Item[i] =
                Class_Object.GetResources_Prefab(ls_Block_Item[i]);
        }

        //Block Stair

        lg_Block_StairUD = new List<List<GameObject>>();
        for (int i = 0; i < ls_Block_StairUD.Count; i++)
        {
            lg_Block_StairUD.Add(new List<GameObject>());
            lg_Block_StairUD[i] =
                Class_Object.GetResources_Prefab(ls_Block_StairUD[i]);
        }

        lg_Block_StairLR = new List<List<GameObject>>();
        for (int i = 0; i < ls_Block_StairLR.Count; i++)
        {
            lg_Block_StairLR.Add(new List<GameObject>());
            lg_Block_StairLR[i] =
                Class_Object.GetResources_Prefab(ls_Block_StairLR[i]);
        }

        //Character

        lg_Character_Player = new List<List<GameObject>>();
        for (int i = 0; i < ls_Character_Player.Count; i++)
        {
            lg_Character_Player.Add(new List<GameObject>());
            lg_Character_Player[i] =
                Class_Object.GetResources_Prefab(ls_Character_Player[i]);
        }

        lg_Character_Good = new List<List<GameObject>>();
        for (int i = 0; i < ls_Character_Good.Count; i++)
        {
            lg_Character_Good.Add(new List<GameObject>());
            lg_Character_Good[i] =
                Class_Object.GetResources_Prefab(ls_Character_Good[i]);
        }

        lg_Character_Neutral = new List<List<GameObject>>();
        for (int i = 0; i < ls_Character_Neutral.Count; i++)
        {
            lg_Character_Neutral.Add(new List<GameObject>());
            lg_Character_Neutral[i] =
                Class_Object.GetResources_Prefab(ls_Character_Neutral[i]);
        }

        lg_Character_Bad = new List<List<GameObject>>();
        for (int i = 0; i < ls_Character_Bad.Count; i++)
        {
            lg_Character_Bad.Add(new List<GameObject>());
            lg_Character_Bad[i] =
                Class_Object.GetResources_Prefab(ls_Character_Bad[i]);
        }
    }

    #region Block Combine List 

    //============================================================================================================ Combine

    public GameObject GetCombine(string s_Block_or_Character_Name)
    {
        GameObject g_Prefab = null;

        //Block Primary

        g_Prefab = GetBlock_Ground(s_Block_or_Character_Name);

        if (g_Prefab != null)
        {
            return g_Prefab;
        }

        g_Prefab = GetBlock_Object(s_Block_or_Character_Name);

        if (g_Prefab != null)
        {
            return g_Prefab;
        }

        g_Prefab = GetBlock_Item(s_Block_or_Character_Name);

        if (g_Prefab != null)
        {
            return g_Prefab;
        }

        //Block Stair

        g_Prefab = GetBlock_StairUD(s_Block_or_Character_Name);

        if (g_Prefab != null)
        {
            return g_Prefab;
        }

        g_Prefab = GetBlock_StairLR(s_Block_or_Character_Name);

        if (g_Prefab != null)
        {
            return g_Prefab;
        }

        //Character

        g_Prefab = GetCharacter_Player(s_Block_or_Character_Name);

        if (g_Prefab != null)
        {
            return g_Prefab;
        }

        g_Prefab = GetCharacter_Good(s_Block_or_Character_Name);

        if (g_Prefab != null)
        {
            return g_Prefab;
        }

        g_Prefab = GetCharacter_Neutral(s_Block_or_Character_Name);

        if (g_Prefab != null)
        {
            return g_Prefab;
        }

        g_Prefab = GetCharacter_Bad(s_Block_or_Character_Name);

        if (g_Prefab != null)
        {
            return g_Prefab;
        }

        return null;
    }

    public GameObject GetCombine_First()
    {
        GameObject g_Prefab = null;

        //Block Primary

        g_Prefab = GetBlock_Ground(0, 0);

        if (g_Prefab != null)
        {
            return g_Prefab;
        }

        g_Prefab = GetBlock_Item(0, 0);

        if (g_Prefab != null)
        {
            return g_Prefab;
        }

        //Block Stair

        g_Prefab = GetBlock_StairUD(0, 0);

        if (g_Prefab != null)
        {
            return g_Prefab;
        }

        g_Prefab = GetBlock_StairLR(0, 0);

        if (g_Prefab != null)
        {
            return g_Prefab;
        }

        //Character

        g_Prefab = GetCharacter_Player(0, 0);

        if (g_Prefab != null)
        {
            return g_Prefab;
        }

        g_Prefab = GetCharacter_Good(0, 0);

        if (g_Prefab != null)
        {
            return g_Prefab;
        }

        g_Prefab = GetCharacter_Neutral(0, 0);

        if (g_Prefab != null)
        {
            return g_Prefab;
        }

        g_Prefab = GetCharacter_Bad(0, 0);

        if (g_Prefab != null)
        {
            return g_Prefab;
        }

        return null;
    }

    public GameObject GetCombine(int i_Block_Page, int i_Block_Index)
    {
        int i_PagePerList = 0;

        int i_PagePerList_Before = 0;

        //Block Primary

        //Block Ground

        i_PagePerList_Before += 0;

        if (lg_Block_Ground.Count != 0)
        {
            i_PagePerList += lg_Block_Ground.Count;

            if (i_Block_Page < i_PagePerList)
            {
                return GetBlock_Ground(i_Block_Page - i_PagePerList_Before, i_Block_Index);
            }
        }

        //Block Object

        i_PagePerList_Before += lg_Block_Ground.Count;

        if (lg_Block_Object.Count != 0)
        {
            i_PagePerList += lg_Block_Object.Count;

            if (i_Block_Page < i_PagePerList)
            {
                return GetBlock_Object(i_Block_Page - i_PagePerList_Before, i_Block_Index);
            }
        }

        //Block Item

        i_PagePerList_Before += lg_Block_Object.Count;

        if (lg_Block_Item.Count != 0)
        {
            i_PagePerList += lg_Block_Item.Count;

            if (i_Block_Page < i_PagePerList)
            {
                return GetBlock_Item(i_Block_Page - i_PagePerList_Before, i_Block_Index);
            }
        }

        //Block Stair

        //Block Stair UD

        i_PagePerList_Before += lg_Block_Item.Count;

        if (lg_Block_StairUD.Count != 0)
        {
            i_PagePerList += lg_Block_StairUD.Count;

            if (i_Block_Page < i_PagePerList)
            {
                return GetBlock_StairUD(i_Block_Page - i_PagePerList_Before, i_Block_Index);
            }
        }

        //Block Stair LR

        i_PagePerList_Before += lg_Block_StairUD.Count;

        if (lg_Block_StairLR.Count != 0)
        {
            i_PagePerList += lg_Block_StairLR.Count;

            if (i_Block_Page < i_PagePerList)
            {
                return GetBlock_StairLR(i_Block_Page - i_PagePerList_Before, i_Block_Index);
            }
        }

        //Character

        //Character Player

        i_PagePerList_Before += lg_Block_StairLR.Count;

        if (lg_Character_Player.Count != 0)
        {
            i_PagePerList += lg_Character_Player.Count;

            if (i_Block_Page < i_PagePerList)
            {
                return GetCharacter_Player(i_Block_Page - i_PagePerList_Before, i_Block_Index);
            }
        }

        //Character Good

        i_PagePerList_Before += lg_Character_Player.Count;

        if (lg_Character_Good.Count != 0)
        {
            i_PagePerList += lg_Character_Good.Count;

            if (i_Block_Page < i_PagePerList)
            {
                return GetCharacter_Good(i_Block_Page - i_PagePerList_Before, i_Block_Index);
            }
        }

        //Character Neutral

        i_PagePerList_Before += lg_Character_Good.Count;

        if (lg_Character_Neutral.Count != 0)
        {
            i_PagePerList += lg_Character_Neutral.Count;

            if (i_Block_Page < i_PagePerList)
            {
                return GetCharacter_Neutral(i_Block_Page - i_PagePerList_Before, i_Block_Index);
            }
        }

        //Character Bad

        i_PagePerList_Before += lg_Character_Neutral.Count;

        if (lg_Character_Bad.Count != 0)
        {
            i_PagePerList += lg_Character_Bad.Count;

            if (i_Block_Page < i_PagePerList)
            {
                return GetCharacter_Bad(i_Block_Page - i_PagePerList_Before, i_Block_Index);
            }
        }

        return null;
    }

    public string GetCombine_Name(int i_Block_Page, int i_Block_Index)
    {
        return ClassString.GetStringReplaceClone(
            GetCombine(i_Block_Page, i_Block_Index).name);
    }

    /// <summary>
    /// Count Page of Combine List
    /// </summary>
    /// <returns></returns>
    public int GetCombineCount()
    {
        int i_PagePerList = 0;

        i_PagePerList += lg_Block_Ground.Count;
        i_PagePerList += lg_Block_Object.Count;
        i_PagePerList += lg_Block_Item.Count;
        i_PagePerList += lg_Block_StairUD.Count;
        i_PagePerList += lg_Block_StairLR.Count;
        i_PagePerList += lg_Character_Player.Count;
        i_PagePerList += lg_Character_Good.Count;
        i_PagePerList += lg_Character_Neutral.Count;
        i_PagePerList += lg_Character_Bad.Count;

        return i_PagePerList;
    }

    /// <summary>
    /// Count Index in Page of Combine List
    /// </summary>
    /// <param name="i_Block_Page"></param>
    /// <returns></returns>
    public int GetCombineCount(int i_Block_Page)
    {
        int i_PagePerList = 0;

        int i_PagePerList_Before = 0;

        //Block Primary

        //Block Ground

        i_PagePerList_Before += 0;

        if (lg_Block_Ground.Count != 0)
        {
            i_PagePerList += lg_Block_Ground.Count;

            if (i_Block_Page < i_PagePerList)
            {
                return GetBlock_GroundCount(i_Block_Page - i_PagePerList_Before);
            }
        }

        //Block Object

        i_PagePerList_Before += lg_Block_Ground.Count;

        if (lg_Block_Object.Count != 0)
        {
            i_PagePerList += lg_Block_Object.Count;

            if (i_Block_Page < i_PagePerList)
            {
                return GetBlock_ObjectCount(i_Block_Page - i_PagePerList_Before);
            }
        }

        //Block Item

        i_PagePerList_Before += lg_Block_Object.Count;

        if (lg_Block_Item.Count != 0)
        {
            i_PagePerList += lg_Block_Item.Count;

            if (i_Block_Page < i_PagePerList)
            {
                return GetBlock_ItemCount(i_Block_Page - i_PagePerList_Before);
            }
        }

        //Block Stair

        //Block Stair UD

        i_PagePerList_Before += lg_Block_Item.Count;

        if (lg_Block_StairUD.Count != 0)
        {
            i_PagePerList += lg_Block_StairUD.Count;

            if (i_Block_Page < i_PagePerList)
            {
                return GetBlock_StairUDCount(i_Block_Page - i_PagePerList_Before);
            }
        }

        //Block Stair LR

        i_PagePerList_Before += lg_Block_StairUD.Count;

        if (lg_Block_StairLR.Count != 0)
        {
            i_PagePerList += lg_Block_StairLR.Count;

            if (i_Block_Page < i_PagePerList)
            {
                return GetBlock_StairLRCount(i_Block_Page - i_PagePerList_Before);
            }
        }

        //Character

        //Character Player

        i_PagePerList_Before += lg_Block_StairLR.Count;

        if (lg_Character_Player.Count != 0)
        {
            i_PagePerList += lg_Character_Player.Count;

            if (i_Block_Page < i_PagePerList)
            {
                return GetCharacter_PlayerCount(i_Block_Page - i_PagePerList_Before);
            }
        }

        //Character Good

        i_PagePerList_Before += lg_Character_Player.Count;

        if (lg_Character_Good.Count != 0)
        {
            i_PagePerList += lg_Character_Good.Count;

            if (i_Block_Page < i_PagePerList)
            {
                return GetCharacter_GoodCount(i_Block_Page - i_PagePerList_Before);
            }
        }

        //Character Neutral

        i_PagePerList_Before += lg_Character_Good.Count;

        if (lg_Character_Neutral.Count != 0)
        {
            i_PagePerList += lg_Character_Neutral.Count;

            if (i_Block_Page < i_PagePerList)
            {
                return GetCharacter_NeutralCount(i_Block_Page - i_PagePerList_Before);
            }
        }

        //Character Bad

        i_PagePerList_Before += lg_Character_Neutral.Count;

        if (lg_Character_Bad.Count != 0)
        {
            i_PagePerList += lg_Character_Bad.Count;

            if (i_Block_Page < i_PagePerList)
            {
                return GetCharacter_BadCount(i_Block_Page - i_PagePerList_Before);
            }
        }

        return 0;
    }

    /// <summary>
    /// Imformation with Page of Combine List
    /// </summary>
    /// <param name="i_Block_Page"></param>
    /// <returns></returns>
    public IsoClassBlock GetCombine_Imformation(int i_Block_Page)
    {
        int i_PagePerList = 0;

        int i_PagePerList_Before = 0;

        //Block Primary

        //Block Ground

        i_PagePerList_Before += 0;

        if (lg_Block_Ground.Count != 0)
        {
            i_PagePerList += lg_Block_Ground.Count;

            if (i_Block_Page < i_PagePerList)
            {
                return new IsoClassBlock(
                    IsoClassBlock.s_Block,
                    IsoClassBlock.s_Block_Ground);
            }
        }

        //Block Object

        i_PagePerList_Before += lg_Block_Ground.Count;

        if (lg_Block_Item.Count != 0)
        {
            i_PagePerList += lg_Block_Object.Count;

            if (i_Block_Page < i_PagePerList)
            {
                return new IsoClassBlock(
                    IsoClassBlock.s_Block,
                    IsoClassBlock.s_Block_Object);
            }
        }

        //Block Item

        i_PagePerList_Before += lg_Block_Ground.Count;

        if (lg_Block_Item.Count != 0)
        {
            i_PagePerList += lg_Block_Item.Count;

            if (i_Block_Page < i_PagePerList)
            {
                return new IsoClassBlock(
                    IsoClassBlock.s_Block,
                    IsoClassBlock.s_Block_Item);
            }
        }

        //Block Stair

        //Block Stair UD

        i_PagePerList_Before += lg_Block_Item.Count;

        if (lg_Block_StairUD.Count != 0)
        {
            i_PagePerList += lg_Block_StairUD.Count;

            if (i_Block_Page < i_PagePerList)
            {
                return new IsoClassBlock(
                    IsoClassBlock.s_Block,
                    IsoClassBlock.s_Block_StairUD);
            }
        }

        //Block Stair LR

        i_PagePerList_Before += lg_Block_StairUD.Count;

        if (lg_Block_StairLR.Count != 0)
        {
            i_PagePerList += lg_Block_StairLR.Count;

            if (i_Block_Page < i_PagePerList)
            {
                return new IsoClassBlock(
                    IsoClassBlock.s_Block,
                    IsoClassBlock.s_Block_StairLR);
            }
        }

        //Character

        //Character Player

        i_PagePerList_Before += lg_Block_StairLR.Count;

        if (lg_Character_Player.Count != 0)
        {
            i_PagePerList += lg_Character_Player.Count;

            if (i_Block_Page < i_PagePerList)
            {
                return new IsoClassBlock(
                    IsoClassBlock.s_Character,
                    IsoClassBlock.s_Character_Player);
            }
        }

        //Character Good

        i_PagePerList_Before += lg_Character_Player.Count;

        if (lg_Character_Good.Count != 0)
        {
            i_PagePerList += lg_Character_Good.Count;

            if (i_Block_Page < i_PagePerList)
            {
                return new IsoClassBlock(
                    IsoClassBlock.s_Character,
                    IsoClassBlock.s_Character_Good);
            }
        }

        //Character Neutral

        i_PagePerList_Before += lg_Character_Good.Count;

        if (lg_Character_Neutral.Count != 0)
        {
            i_PagePerList += lg_Character_Neutral.Count;

            if (i_Block_Page < i_PagePerList)
            {
                return new IsoClassBlock(
                    IsoClassBlock.s_Character,
                    IsoClassBlock.s_Character_Neutral);
            }
        }

        //Character Bad

        i_PagePerList_Before += lg_Character_Neutral.Count;

        if (lg_Character_Bad.Count != 0)
        {
            i_PagePerList += lg_Character_Bad.Count;

            if (i_Block_Page < i_PagePerList)
            {
                return new IsoClassBlock(
                    IsoClassBlock.s_Character,
                    IsoClassBlock.s_Character_Bad);
            }
        }

        return new IsoClassBlock("...", "...");
    }

    /// <summary>
    /// Imformation with Page of Combine List
    /// </summary>
    /// <param name="i_Block_Page"></param>
    /// <returns></returns>
    public IsoClassBlock GetCombine_Imformation(string s_Block_or_Character_Name)
    {
        GameObject g_Prefab = null;

        //Block Primary

        g_Prefab = GetBlock_Ground(s_Block_or_Character_Name);

        if (g_Prefab != null)
        {
            return new IsoClassBlock(
                IsoClassBlock.s_Block,
                IsoClassBlock.s_Block_Ground);
        }

        g_Prefab = GetBlock_Object(s_Block_or_Character_Name);

        if (g_Prefab != null)
        {
            return new IsoClassBlock(
                IsoClassBlock.s_Block,
                IsoClassBlock.s_Block_Object);
        }

        g_Prefab = GetBlock_Item(s_Block_or_Character_Name);

        if (g_Prefab != null)
        {
            return new IsoClassBlock(
                IsoClassBlock.s_Block,
                IsoClassBlock.s_Block_Item);
        }

        //Block Stair

        g_Prefab = GetBlock_StairUD(s_Block_or_Character_Name);

        if (g_Prefab != null)
        {
            return new IsoClassBlock(
                IsoClassBlock.s_Block,
                IsoClassBlock.s_Block_StairUD);
        }

        g_Prefab = GetBlock_StairLR(s_Block_or_Character_Name);

        if (g_Prefab != null)
        {
            return new IsoClassBlock(
                IsoClassBlock.s_Block,
                IsoClassBlock.s_Block_StairLR);
        }

        //Character

        g_Prefab = GetCharacter_Player(s_Block_or_Character_Name);

        if (g_Prefab != null)
        {
            return new IsoClassBlock(
                IsoClassBlock.s_Character,
                IsoClassBlock.s_Character_Player);
        }

        g_Prefab = GetCharacter_Good(s_Block_or_Character_Name);

        if (g_Prefab != null)
        {
            return new IsoClassBlock(
                IsoClassBlock.s_Character,
                IsoClassBlock.s_Character_Good);
        }

        g_Prefab = GetCharacter_Neutral(s_Block_or_Character_Name);

        if (g_Prefab != null)
        {
            return new IsoClassBlock(
                IsoClassBlock.s_Character,
                IsoClassBlock.s_Character_Neutral);
        }

        g_Prefab = GetCharacter_Bad(s_Block_or_Character_Name);

        if (g_Prefab != null)
        {
            return new IsoClassBlock(
                IsoClassBlock.s_Character,
                IsoClassBlock.s_Character_Bad);
        }

        return new IsoClassBlock("...", "...");
    }

    #endregion

    //============================================================================================================ Block

    #region Block List 

    #region Block Primary 

    #region Block Ground List 

    public GameObject GetBlock_Ground(string s_Block_Name)
    {
        if (s_Block_Name.Equals(""))
        {
            return null;
        }

        for (int i = 0; i < lg_Block_Ground.Count; i++)
        {
            for (int j = 0; j < lg_Block_Ground[i].Count; j++)
            {
                if (lg_Block_Ground[i][j].name.Equals(s_Block_Name))
                {
                    return lg_Block_Ground[i][j];
                }
            }
        }

        return null;
    }

    public GameObject GetBlock_Ground(int i_Block_Page, int i_Block_Index)
    {
        if (i_Block_Page < 0 || i_Block_Page > lg_Block_Ground.Count - 1)
        {
            return null;
        }

        if (i_Block_Index < 0 || i_Block_Index > lg_Block_Ground[i_Block_Page].Count)
        {
            return null;
        }

        return lg_Block_Ground[i_Block_Page][i_Block_Index];
    }

    public string GetBlock_Ground_Name(int i_Block_Page, int i_Block_Index)
    {
        return ClassString.GetStringReplaceClone(
            GetBlock_Ground(i_Block_Page, i_Block_Index).name);
    }

    public int GetBlock_GroundCount()
    {
        return lg_Block_Ground.Count;
    }

    public int GetBlock_GroundCount(int i_Block_Page)
    {
        if (i_Block_Page < 0 || i_Block_Page > lg_Block_Ground.Count - 1)
        {
            return 0;
        }

        return lg_Block_Ground[i_Block_Page].Count;
    }

    #endregion

    #region Block Object List 

    public GameObject GetBlock_Object(string s_Block_Name)
    {
        if (s_Block_Name.Equals(""))
        {
            return null;
        }

        for (int i = 0; i < lg_Block_Object.Count; i++)
        {
            for (int j = 0; j < lg_Block_Object[i].Count; j++)
            {
                if (lg_Block_Object[i][j].name.Equals(s_Block_Name))
                {
                    return lg_Block_Object[i][j];
                }
            }
        }

        return null;
    }

    public GameObject GetBlock_Object(int i_Block_Page, int i_Block_Index)
    {
        if (i_Block_Page < 0 || i_Block_Page > lg_Block_Object.Count - 1)
        {
            return null;
        }

        if (i_Block_Index < 0 || i_Block_Index > lg_Block_Object[i_Block_Page].Count)
        {
            return null;
        }

        return lg_Block_Object[i_Block_Page][i_Block_Index];
    }

    public string GetBlock_Object_Name(int i_Block_Page, int i_Block_Index)
    {
        return ClassString.GetStringReplaceClone(
            GetBlock_Object(i_Block_Page, i_Block_Index).name);
    }

    public int GetBlock_ObjectCount()
    {
        return lg_Block_Object.Count;
    }

    public int GetBlock_ObjectCount(int i_Block_Page)
    {
        if (i_Block_Page < 0 || i_Block_Page > lg_Block_Object.Count - 1)
        {
            return 0;
        }

        return lg_Block_Object[i_Block_Page].Count;
    }

    #endregion

    #region Block Item List 

    public GameObject GetBlock_Item(string s_Block_Name)
    {
        if (s_Block_Name.Equals(""))
        {
            return null;
        }

        for (int i = 0; i < lg_Block_Item.Count; i++)
        {
            for (int j = 0; j < lg_Block_Item[i].Count; j++)
            {
                if (lg_Block_Item[i][j].name.Equals(s_Block_Name))
                {
                    return lg_Block_Item[i][j];
                }
            }
        }

        return null;
    }

    public GameObject GetBlock_Item(int i_Block_Page, int i_Block_Index)
    {
        if (i_Block_Page < 0 || i_Block_Page > lg_Block_Item.Count - 1)
        {
            return null;
        }

        if (i_Block_Index < 0 || i_Block_Index > lg_Block_Item[i_Block_Page].Count)
        {
            return null;
        }

        return lg_Block_Item[i_Block_Page][i_Block_Index];
    }

    public string GetBlock_Item_Name(int i_Block_Page, int i_Block_Index)
    {
        return ClassString.GetStringReplaceClone(
            GetBlock_Item(i_Block_Page, i_Block_Index).name);
    }

    public int GetBlock_ItemCount()
    {
        return lg_Block_Item.Count;
    }

    public int GetBlock_ItemCount(int i_Block_Page)
    {
        if (i_Block_Page < 0 || i_Block_Page > lg_Block_Item.Count - 1)
        {
            return 0;
        }

        return lg_Block_Item[i_Block_Page].Count;
    }

    #endregion

    #endregion

    #region Block Stair List 

    #region Block StairUD List 

    public GameObject GetBlock_StairUD(string s_Block_Name)
    {
        if (s_Block_Name.Equals(""))
        {
            return null;
        }

        for (int i = 0; i < lg_Block_StairUD.Count; i++)
        {
            for (int j = 0; j < lg_Block_StairUD[i].Count; j++)
            {
                if (lg_Block_StairUD[i][j].name.Equals(s_Block_Name))
                {
                    return lg_Block_StairUD[i][j];
                }
            }
        }

        return null;
    }

    public GameObject GetBlock_StairUD(int i_Block_Page, int i_Block_Index)
    {
        if (i_Block_Page < 0 || i_Block_Page > lg_Block_StairUD.Count - 1)
        {
            return null;
        }

        if (i_Block_Index < 0 || i_Block_Index > lg_Block_StairUD[i_Block_Page].Count)
        {
            return null;
        }

        return lg_Block_StairUD[i_Block_Page][i_Block_Index];
    }

    public string GetBlock_StairUD_Name(int i_Block_Page, int i_Block_Index)
    {
        return ClassString.GetStringReplaceClone(
            GetBlock_StairUD(i_Block_Page, i_Block_Index).name);
    }

    public int GetBlock_StairUDCount()
    {
        return lg_Block_StairUD.Count;
    }

    public int GetBlock_StairUDCount(int i_Block_Page)
    {
        if (i_Block_Page < 0 || i_Block_Page > lg_Block_StairUD.Count - 1)
        {
            return 0;
        }

        return lg_Block_StairUD[i_Block_Page].Count;
    }

    #endregion

    #region Block StairLR List 

    public GameObject GetBlock_StairLR(string s_Block_Name)
    {
        if (s_Block_Name.Equals(""))
        {
            return null;
        }

        for (int i = 0; i < lg_Block_StairLR.Count; i++)
        {
            for (int j = 0; j < lg_Block_StairLR[i].Count; j++)
            {
                if (lg_Block_StairLR[i][j].name.Equals(s_Block_Name))
                {
                    return lg_Block_StairLR[i][j];
                }
            }
        }

        return null;
    }

    public GameObject GetBlock_StairLR(int i_Block_Page, int i_Block_Index)
    {
        if (i_Block_Page < 0 || i_Block_Page > lg_Block_StairLR.Count - 1)
        {
            return null;
        }

        if (i_Block_Index < 0 || i_Block_Index > lg_Block_StairLR[i_Block_Page].Count)
        {
            return null;
        }

        return lg_Block_StairLR[i_Block_Page][i_Block_Index];
    }

    public string GetBlock_StairLR_Name(int i_Block_Page, int i_Block_Index)
    {
        return ClassString.GetStringReplaceClone(
            GetBlock_StairLR(i_Block_Page, i_Block_Index).name);
    }

    public int GetBlock_StairLRCount()
    {
        return lg_Block_StairLR.Count;
    }

    public int GetBlock_StairLRCount(int i_Block_Page)
    {
        if (i_Block_Page < 0 || i_Block_Page > lg_Block_StairLR.Count - 1)
        {
            return 0;
        }

        return lg_Block_StairLR[i_Block_Page].Count;
    }

    #endregion

    #endregion

    #endregion

    //============================================================================================================ Character

    #region Character 

    #region Character Player 

    public GameObject GetCharacter_Player(string s_Block_Name)
    {
        if (s_Block_Name.Equals(""))
        {
            return null;
        }

        for (int i = 0; i < lg_Character_Player.Count; i++)
        {
            for (int j = 0; j < lg_Character_Player[i].Count; j++)
            {
                if (lg_Character_Player[i][j].name.Equals(s_Block_Name))
                {
                    return lg_Character_Player[i][j];
                }
            }
        }

        return null;
    }

    public GameObject GetCharacter_Player(int i_Block_Page, int i_Block_Index)
    {
        if (i_Block_Page < 0 || i_Block_Page > lg_Character_Player.Count - 1)
        {
            return null;
        }

        if (i_Block_Index < 0 || i_Block_Index > lg_Character_Player[i_Block_Page].Count)
        {
            return null;
        }

        return lg_Character_Player[i_Block_Page][i_Block_Index];
    }

    public string GetCharacter_Player_Name(int i_Block_Page, int i_Block_Index)
    {
        return ClassString.GetStringReplaceClone(
            GetCharacter_Player(i_Block_Page, i_Block_Index).name);
    }

    public int GetCharacter_PlayerCount()
    {
        return lg_Character_Player.Count;
    }

    public int GetCharacter_PlayerCount(int i_Block_Page)
    {
        if (i_Block_Page < 0 || i_Block_Page > lg_Character_Player.Count - 1)
        {
            return 0;
        }

        return lg_Character_Player[i_Block_Page].Count;
    }

    #endregion

    #region Character Good 

    public GameObject GetCharacter_Good(string s_Block_Name)
    {
        if (s_Block_Name.Equals(""))
        {
            return null;
        }

        for (int i = 0; i < lg_Character_Good.Count; i++)
        {
            for (int j = 0; j < lg_Character_Good[i].Count; j++)
            {
                if (lg_Character_Good[i][j].name.Equals(s_Block_Name))
                {
                    return lg_Character_Good[i][j];
                }
            }
        }

        return null;
    }

    public GameObject GetCharacter_Good(int i_Block_Page, int i_Block_Index)
    {
        if (i_Block_Page < 0 || i_Block_Page > lg_Character_Good.Count - 1)
        {
            return null;
        }

        if (i_Block_Index < 0 || i_Block_Index > lg_Character_Good[i_Block_Page].Count)
        {
            return null;
        }

        return lg_Character_Good[i_Block_Page][i_Block_Index];
    }

    public string GetCharacter_Good_Name(int i_Block_Page, int i_Block_Index)
    {
        return ClassString.GetStringReplaceClone(
            GetCharacter_Good(i_Block_Page, i_Block_Index).name);
    }

    public int GetCharacter_GoodCount()
    {
        return lg_Character_Good.Count;
    }

    public int GetCharacter_GoodCount(int i_Block_Page)
    {
        if (i_Block_Page < 0 || i_Block_Page > lg_Character_Good.Count - 1)
        {
            return 0;
        }

        return lg_Character_Good[i_Block_Page].Count;
    }

    #endregion

    #region Character Neutral 

    public GameObject GetCharacter_Neutral(string s_Block_Name)
    {
        if (s_Block_Name.Equals(""))
        {
            return null;
        }

        for (int i = 0; i < lg_Character_Neutral.Count; i++)
        {
            for (int j = 0; j < lg_Character_Neutral[i].Count; j++)
            {
                if (lg_Character_Neutral[i][j].name.Equals(s_Block_Name))
                {
                    return lg_Character_Neutral[i][j];
                }
            }
        }

        return null;
    }

    public GameObject GetCharacter_Neutral(int i_Block_Page, int i_Block_Index)
    {
        if (i_Block_Page < 0 || i_Block_Page > lg_Character_Neutral.Count - 1)
        {
            return null;
        }

        if (i_Block_Index < 0 || i_Block_Index > lg_Character_Neutral[i_Block_Page].Count)
        {
            return null;
        }

        return lg_Character_Neutral[i_Block_Page][i_Block_Index];
    }

    public string GetCharacter_Neutral_Name(int i_Block_Page, int i_Block_Index)
    {
        return ClassString.GetStringReplaceClone(
            GetCharacter_Neutral(i_Block_Page, i_Block_Index).name);
    }

    public int GetCharacter_NeutralCount()
    {
        return lg_Character_Neutral.Count;
    }

    public int GetCharacter_NeutralCount(int i_Block_Page)
    {
        if (i_Block_Page < 0 || i_Block_Page > lg_Character_Neutral.Count - 1)
        {
            return 0;
        }

        return lg_Character_Neutral[i_Block_Page].Count;
    }

    #endregion

    #region Character Bad 

    public GameObject GetCharacter_Bad(string s_Block_Name)
    {
        if (s_Block_Name.Equals(""))
        {
            return null;
        }

        for (int i = 0; i < lg_Character_Bad.Count; i++)
        {
            for (int j = 0; j < lg_Character_Bad[i].Count; j++)
            {
                if (lg_Character_Bad[i][j].name.Equals(s_Block_Name))
                {
                    return lg_Character_Bad[i][j];
                }
            }
        }

        return null;
    }

    public GameObject GetCharacter_Bad(int i_Block_Page, int i_Block_Index)
    {
        if (i_Block_Page < 0 || i_Block_Page > lg_Character_Bad.Count - 1)
        {
            return null;
        }

        if (i_Block_Index < 0 || i_Block_Index > lg_Character_Bad[i_Block_Page].Count)
        {
            return null;
        }

        return lg_Character_Bad[i_Block_Page][i_Block_Index];
    }

    public string GetCharacter_Bad_Name(int i_Block_Page, int i_Block_Index)
    {
        return ClassString.GetStringReplaceClone(
            GetCharacter_Bad(i_Block_Page, i_Block_Index).name);
    }

    public int GetCharacter_BadCount()
    {
        return lg_Character_Bad.Count;
    }

    public int GetCharacter_BadCount(int i_Block_Page)
    {
        if (i_Block_Page < 0 || i_Block_Page > lg_Character_Bad.Count - 1)
        {
            return 0;
        }

        return lg_Character_Bad[i_Block_Page].Count;
    }

    #endregion

    #endregion
}
