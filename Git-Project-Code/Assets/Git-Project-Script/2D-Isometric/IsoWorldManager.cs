using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR

//[ExecuteAlways]

#endif

public class IsoWorldManager : MonoBehaviour
{
    private static IsoWorldManager m_This;

    [Header("World Manager")]

    [SerializeField] private IsoVector m_Scale = new IsoVector(1f, 1f, 1f);

    //private IsoWorld m_World;

    //private List<List<List<GameObject>>> m_WorldMatrix;

    //private List<>

    [SerializeField] private List<IsoBlockMatrix> m_World;

    private bool m_WorldGenerated = false;

    [Header("Block(s) Manager")]

    [SerializeField] private List<GameObject> m_Blocks;

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

        m_World = new List<IsoBlockMatrix>();
    }

//#if UNITY_EDITOR

    private void Start()
    {
        m_Blocks = GitResources.GetResourcesPrefab("Blocks");

        //StartCoroutine(Set());
            
        SetWorldBlock(new IsoVector(0, 0, 0), GitGameObject.SetGameObjectCreate(m_This.m_Blocks[0], this.transform).GetComponent<IsoBlock>());
    }

    private IEnumerator Set()
    {
        yield return null;

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                for (int t = 0; t < 10; t++)
                {
                    SetWorldBlock(new IsoVector(i, j, t), GitGameObject.SetGameObjectCreate(m_This.m_Blocks[0], this.transform).GetComponent<IsoBlock>());

                    yield return null;
                }
            }
        }

        yield return null;

        do
        {
            for (int i = 0; i < 10; i++)
            {
                GetWorldBlock(new IsoVector(0, 0, 0)).Pos = new IsoVector(i * 1.0f / 10, 0, 0);

                yield return null;
            }

            for (int i = 10; i > 0; i--)
            {
                GetWorldBlock(new IsoVector(0, 0, 0)).Pos = new IsoVector(i * 1.0f / 10, 0, 0);

                yield return null;
            }
        }
        while (1 == 1);

        yield return null;
    }

    //#endif

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

            m_BlockInWorld.m_Block.SetScale(m_This.m_Scale);
            m_BlockInWorld.m_Block.SetPosPrimary(m_Pos);
            m_BlockInWorld.m_Block.Pos += m_Pos;

            //U

            m_BlockInWorld.m_UX = GetWorldIndex(m_Pos.IfUp, m_This.m_World.Count);

            if (m_BlockInWorld.m_UX != -1)
            {
                m_This.m_World[m_BlockInWorld.m_UX].m_DX = m_This.m_World.Count;
            }

            //D

            m_BlockInWorld.m_DX = GetWorldIndex(m_Pos.IfDown, m_This.m_World.Count);

            if (m_BlockInWorld.m_DX != -1)
            {
                m_This.m_World[m_BlockInWorld.m_DX].m_UX = m_This.m_World.Count;
            }

            //L

            m_BlockInWorld.m_LY = GetWorldIndex(m_Pos.IfLeft, m_This.m_World.Count);

            if (m_BlockInWorld.m_LY != -1)
            {
                m_This.m_World[m_BlockInWorld.m_LY].m_RY = m_This.m_World.Count;
            }

            //R

            m_BlockInWorld.m_RY = GetWorldIndex(m_Pos.IfRight, m_This.m_World.Count);

            if (m_BlockInWorld.m_RY != -1)
            {
                m_This.m_World[m_BlockInWorld.m_RY].m_LY = m_This.m_World.Count;
            }

            //T

            m_BlockInWorld.m_TH = GetWorldIndex(m_Pos.IfTop, m_This.m_World.Count);

            if (m_BlockInWorld.m_TH != -1)
            {
                m_This.m_World[m_BlockInWorld.m_TH].m_BH = m_This.m_World.Count;
            }

            //B

            m_BlockInWorld.m_BH = GetWorldIndex(m_Pos.IfBot, m_This.m_World.Count);

            if (m_BlockInWorld.m_BH != -1)
            {
                m_This.m_World[m_BlockInWorld.m_BH].m_TH = m_This.m_World.Count;
            }

            m_This.m_World.Add(m_BlockInWorld); //Check???
        }
    }

    public static int GetWorldIndex(IsoVector m_Pos, int m_BlockIndexAcess = -1)
    {
        //Check if out of matrix

        for (int i = 0; i < m_This.m_World.Count; i++)
        {
            //This Pos
            if (m_This.m_World[i].m_Block.GetPosPrimary() == m_Pos)
            {
                return i;
            }
            //Neighbor Pos
            if (m_This.m_World[i].m_UX != -1 && m_BlockIndexAcess != m_This.m_World[i].m_UX)
            {
                if (m_This.m_World[m_This.m_World[i].m_UX].m_Block.GetPosPrimary()== m_Pos)
                {
                    return m_This.m_World[i].m_UX;
                }
            }
            else
            if (m_This.m_World[i].m_DX != -1 && m_BlockIndexAcess != m_This.m_World[i].m_DX)
            {
                if (m_This.m_World[m_This.m_World[i].m_DX].m_Block.GetPosPrimary()== m_Pos)
                {
                    return m_This.m_World[i].m_DX;
                }
            }
            else
            if (m_This.m_World[i].m_LY != -1 && m_BlockIndexAcess != m_This.m_World[i].m_LY)
            {
                if (m_This.m_World[m_This.m_World[i].m_LY].m_Block.GetPosPrimary()== m_Pos)
                {
                    return m_This.m_World[i].m_LY;
                }
            }
            else
            if (m_This.m_World[i].m_RY != -1 && m_BlockIndexAcess != m_This.m_World[i].m_RY)
            {
                if (m_This.m_World[m_This.m_World[i].m_RY].m_Block.GetPosPrimary()== m_Pos)
                {
                    return m_This.m_World[i].m_RY;
                }
            }
            else
            if (m_This.m_World[i].m_TH != -1 && m_BlockIndexAcess != m_This.m_World[i].m_TH)
            {
                if (m_This.m_World[m_This.m_World[i].m_TH].m_Block.GetPosPrimary()== m_Pos)
                {
                    return m_This.m_World[i].m_TH;
                }
            }
            else
            if (m_This.m_World[i].m_BH != -1 && m_BlockIndexAcess != m_This.m_World[i].m_BH)
            {
                if (m_This.m_World[m_This.m_World[i].m_BH].m_Block.GetPosPrimary()== m_Pos)
                {
                    return m_This.m_World[i].m_BH;
                }
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

//public class IsoWorld
//{
//    private List<List<List<GameObject>>> m_World;

//    private IsoVector m_Size;

//    public IsoWorld(IsoVector m_Size)
//    {
//        this.m_Size = new IsoVector(m_Size);

//        m_World = new List<List<List<GameObject>>>();

//        for (int h = 0; h < m_Size.m_TBHInt; h++)
//        {
//            m_World.Add(new List<List<GameObject>>());

//            for (int x = 0; x < m_Size.m_UDXInt; x++)
//            {
//                m_World[h].Add(new List<GameObject>());

//                for (int y = 0; y < m_Size.m_LRYInt; y++)
//                {
//                    m_World[h][x].Add(new GameObject());
//                }
//            }
//        }
//    }

//    public void SetWorld(IsoVector m_Pos, GameObject m_Block)
//    {
//        if (!GetLimit(m_Pos))
//        {
//            return;
//        }

//        m_World[m_Pos.m_TBHInt][m_Pos.m_UDXInt][m_Pos.m_LRYInt] = m_Block;
//    }

//    public GameObject GetWorld(IsoVector m_Pos)
//    {
//        if (!GetLimit(m_Pos))
//        {
//            return null;
//        }

//        return m_World[m_Pos.m_TBHInt][m_Pos.m_UDXInt][m_Pos.m_LRYInt];
//    }

//    public IsoVector GetSize()
//    {
//        return m_Size;
//    }

//    public bool GetLimit(IsoVector m_Pos)
//    {
//        if (m_Pos.m_UDXInt < 0 || m_Pos.m_UDXInt > m_Size.m_UDXInt - 1)
//        {
//            return false;
//        }

//        if (m_Pos.m_LRYInt < 0 || m_Pos.m_LRYInt > m_Size.m_LRYInt - 1)
//        {
//            return false;
//        }

//        if (m_Pos.m_TBHInt < 0 || m_Pos.m_TBHInt > m_Size.m_TBHInt - 1)
//        {
//            return false;
//        }

//        return true;
//    }
//}

[System.Serializable]
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