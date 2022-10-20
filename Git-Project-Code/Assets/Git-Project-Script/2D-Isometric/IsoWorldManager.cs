using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsoWorldManager : MonoBehaviour
{
    private static IsoWorldManager m_This;

    [Header("World Manager")]

    [SerializeField] private IsoVector m_Scale = new IsoVector(1f, 1f, 1f);

    //private IsoWorld m_World;

    //private List<List<List<GameObject>>> m_WorldMatrix;

    //private List<>

    private List<IsoBlockMatrix> m_World;

    private bool m_WorldGenerated = false;

    [Header("Block(s) Manager")]

    private List<GameObject> m_Blocks;

    private bool m_BlocksUpdated = false;

    public Action act_BlocksUpdated;
    public Action act_WorldGenerated;
    public Action act_WorldDestroyed;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (m_This == null)
        {
            m_This = this;
        }
    }

    #region World Manager

    public static void SetWorldBlock(IsoVector m_Pos, IsoBlock m_Block)
    {
        //Check if out of matrix

        int m_BlockIndex = GetWorldIndex(m_Pos);

        if (m_BlockIndex != -1)
        {
            Destroy(m_This.m_World[m_BlockIndex].m_Block.gameObject);
            m_This.m_World[m_BlockIndex].m_Block = m_Block;
        }
        else
        {
            IsoBlockMatrix m_BlockInWorld = new IsoBlockMatrix(m_Block);
            m_BlockInWorld.m_UX = GetWorldIndex(m_Pos.GetVectorAdd(1, 0, 0));
            m_BlockInWorld.m_DX = GetWorldIndex(m_Pos.GetVectorAdd(-1, 0, 0));
            m_BlockInWorld.m_LY = GetWorldIndex(m_Pos.GetVectorAdd(0, -1, 0));
            m_BlockInWorld.m_RY = GetWorldIndex(m_Pos.GetVectorAdd(0, 1, 0));
            m_BlockInWorld.m_TH = GetWorldIndex(m_Pos.GetVectorAdd(0, 0, 1));
            m_BlockInWorld.m_BH = GetWorldIndex(m_Pos.GetVectorAdd(0, 0, -1));
            m_This.m_World.Add(m_BlockInWorld);
        }
    }

    public static int GetWorldIndex(IsoVector m_Pos)
    {
        //Check if out of matrix

        for (int i = 0; i < m_This.m_World.Count; i++)
        {
            //This Pos
            if (m_This.m_World[i].m_Block.GetPosPrimary().GetVectorEqual(m_Pos))
            {
                return i;
            }
            else
            //Neighbor Pos
            if (m_This.m_World[m_This.m_World[i].m_UX].m_Block.GetPosPrimary().GetVectorEqual(m_Pos))
            {
                return m_This.m_World[i].m_UX;
            }
            else
            if (m_This.m_World[m_This.m_World[i].m_DX].m_Block.GetPosPrimary().GetVectorEqual(m_Pos))
            {
                return m_This.m_World[i].m_DX;
            }
            else
            if (m_This.m_World[m_This.m_World[i].m_LY].m_Block.GetPosPrimary().GetVectorEqual(m_Pos))
            {
                return m_This.m_World[i].m_LY;
            }
            else
            if (m_This.m_World[m_This.m_World[i].m_RY].m_Block.GetPosPrimary().GetVectorEqual(m_Pos))
            {
                return m_This.m_World[i].m_RY;
            }
            else
            if (m_This.m_World[m_This.m_World[i].m_TH].m_Block.GetPosPrimary().GetVectorEqual(m_Pos))
            {
                return m_This.m_World[i].m_TH;
            }
            else
            if (m_This.m_World[m_This.m_World[i].m_BH].m_Block.GetPosPrimary().GetVectorEqual(m_Pos))
            {
                return m_This.m_World[i].m_BH;
            }
        }

        return -1;
    }

    public static IsoBlock GetWorldBlock(IsoVector m_Pos)
    {
        //Check if out of matrix

        int m_BlockIndex = GetWorldIndex(m_Pos);

        if (m_BlockIndex != -1)
        {
            return m_This.m_World[m_BlockIndex].m_Block;
        }

        return null;
    }

    public static IsoBlock GetWorld(int m_Index, bool m_Check = true)
    {
        //Check if out of matrix

        return m_This.m_World[m_Index].m_Block;
    }

    //public static void SetWorldGenerate(IsoWorldImformation m_WorldImformation)
    //{
    //    m_This.m_World = new IsoWorld(m_WorldImformation.m_Size);

    //    foreach(IsoBlockImformation m_BlockImformation in m_WorldImformation.m_BlocksImformation)
    //    {
    //        GameObject m_BlockClone = GetBlock(m_BlockImformation.m_BlockName);

    //        GameObject m_Block = GitGameObject.SetGameObjectCreate(m_BlockClone, m_This.transform);

    //        if (m_Block.GetComponent<IsoBlock>() == null) 
    //            m_Block.AddComponent<IsoBlock>();

    //        m_Block.GetComponent<IsoBlock>().SetImformation(m_BlockImformation);
    //        m_Block.GetComponent<IsoBlock>().SetPosPrimary(m_BlockImformation.m_PosPrimary);
    //        m_Block.GetComponent<IsoBlock>().SetPos(m_BlockImformation.m_PosPrimary);

    //        m_Block.GetComponent<IsoBlock>().SetScale(m_This.m_Scale);
    //        m_Block.SetActive(true);

    //        m_This.m_World.SetWorld(m_BlockImformation.m_PosPrimary, m_Block);
    //    }

    //    m_This.act_WorldGenerated?.Invoke();
    //}

    //public static IsoWorld GetWorld()
    //{
    //    if (!m_This.m_WorldGenerated)
    //    {
    //        return null;
    //    }

    //    return m_This.m_World;
    //}

    //public static bool GetWorldGenerated()
    //{
    //    return m_This.m_WorldGenerated;
    //}

    //public static void SetWorldDestroy()
    //{
    //    if (!m_This.m_WorldGenerated)
    //    {
    //        return;
    //    }

    //    for (int h = 0; h < m_This.m_World.GetSize().m_TBHInt; h++)
    //    {
    //        for (int x = 0; x < m_This.m_World.GetSize().m_UDXInt; x++)
    //        {
    //            for (int y = 0; y < m_This.m_World.GetSize().m_LRYInt; y++)
    //            {
    //                GitGameObject.SetGameObjectDestroy(m_This.m_World.GetWorld(new IsoVector(x, y, h)));
    //            }
    //        }
    //    }


    //}

    #endregion

    #region Block(s) Manager

    public static void SetBlocksUpdate(List<GameObject> m_Blocks)
    {
        if (m_This.m_BlocksUpdated)
        {
            Debug.LogWarningFormat("{0}: Blocks were updated and will be replace with new blocks!", m_This.name);
        }

        m_This.m_Blocks = new List<GameObject>();

        m_This.m_Blocks = m_Blocks;

        m_This.m_BlocksUpdated = true;

        m_This.act_BlocksUpdated?.Invoke();
    }

    public static GameObject GetBlock(string m_BlockName)
    {
        foreach(GameObject m_Block in m_This.m_Blocks)
        {
            if (m_Block.name == m_BlockName)
            {
                return m_Block;
            }
        }

        Debug.LogWarningFormat("{0}: Not found block {1}!", m_This.name, m_BlockName);

        return null;
    }

    public static bool GetBlocksUpdated()
    {
        return m_This.m_BlocksUpdated;
    }

    #endregion
}

public class IsoWorld
{
    private List<List<List<GameObject>>> m_World;

    private IsoVector m_Size;

    public IsoWorld(IsoVector m_Size)
    {
        this.m_Size = new IsoVector(m_Size);

        m_World = new List<List<List<GameObject>>>();

        for (int h = 0; h < m_Size.m_TBHInt; h++)
        {
            m_World.Add(new List<List<GameObject>>());

            for (int x = 0; x < m_Size.m_UDXInt; x++)
            {
                m_World[h].Add(new List<GameObject>());

                for (int y = 0; y < m_Size.m_LRYInt; y++)
                {
                    m_World[h][x].Add(new GameObject());
                }
            }
        }
    }

    public void SetWorld(IsoVector m_Pos, GameObject m_Block)
    {
        if (!GetLimit(m_Pos))
        {
            return;
        }

        m_World[m_Pos.m_TBHInt][m_Pos.m_UDXInt][m_Pos.m_LRYInt] = m_Block;
    }

    public GameObject GetWorld(IsoVector m_Pos)
    {
        if (!GetLimit(m_Pos))
        {
            return null;
        }

        return m_World[m_Pos.m_TBHInt][m_Pos.m_UDXInt][m_Pos.m_LRYInt];
    }

    public IsoVector GetSize()
    {
        return m_Size;
    }

    public bool GetLimit(IsoVector m_Pos)
    {
        if (m_Pos.m_UDXInt < 0 || m_Pos.m_UDXInt > m_Size.m_UDXInt - 1)
        {
            return false;
        }

        if (m_Pos.m_LRYInt < 0 || m_Pos.m_LRYInt > m_Size.m_LRYInt - 1)
        {
            return false;
        }

        if (m_Pos.m_TBHInt < 0 || m_Pos.m_TBHInt > m_Size.m_TBHInt - 1)
        {
            return false;
        }

        return true;
    }
}

public class IsoBlockMatrix
{
    public IsoBlock m_Block;

    public int m_UX = -1;
    public int m_DX = -1;
    public int m_LY = -1;
    public int m_RY = -1;
    public int m_TH = -1;
    public int m_BH = -1;

    public IsoBlockMatrix(IsoBlock m_Block)
    {
        this.m_Block = m_Block;
    }
}

//=====================================

public class IsoWorldImformation
{
    public List<IsoBlockImformation> m_BlocksImformation;

    public IsoVector m_Size;
}

public class IsoBlockImformation
{
    public string m_BlockName;

    public IsoVector m_PosPrimary;

    public List<IsoBlockMoveImformation> m_Moves;

    public IsoVector m_Join;

    public List<IsoVector> m_Links;

    public List<IsoBlockEventImformation> m_Events;
}

public class IsoBlockMoveImformation
{
    public IsoVector m_Dir;

    public int m_Step;

    public float m_Speed;
}

public class IsoBlockEventImformation
{
    public string m_Type;

    public List<string> m_Codes;
}

public class IsoBlockMessageImformation
{
    public string m_From;

    public string m_Content;
}