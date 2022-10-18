using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsoWorldManager : MonoBehaviour
{
    private static IsoWorldManager m_This;

    [Header("World Manager")]

    [SerializeField] private IsoVector m_Scale = new IsoVector(1f, 1f, 1f);

    private IsoWorld m_World;

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

    #region World Generate

    public static void SetWorldGenerate(IsoWorldImformation m_WorldImformation)
    {
        m_This.m_World = new IsoWorld(m_WorldImformation.m_Size);

        foreach(IsoBlockImformation m_BlockImformation in m_WorldImformation.m_BlocksImformation)
        {
            GameObject m_Block = GetBlock(m_BlockImformation.m_BlockName);

            //Add Componenet(s)

            m_This.m_World.SetWorld(m_BlockImformation.m_Pos, m_Block, m_This.m_Scale);
        }

        m_This.act_WorldGenerated?.Invoke();
    }

    #endregion

    #region Block(s) Updated

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

    public void SetWorld(IsoVector m_Pos, GameObject m_Block, IsoVector m_Scale)
    {
        if (!GetLimit(m_Pos))
        {
            return;
        }

        if (m_Block.GetComponent<IsoBlock>() == null) m_Block.AddComponent<IsoBlock>();

        m_Block.GetComponent<IsoBlock>().SetIsoScale(m_Scale);
        m_Block.GetComponent<IsoBlock>().SetPosPrimary(m_Pos);
        m_Block.GetComponent<IsoBlock>().SetPos(m_Pos);

        m_Block.SetActive(true);

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

public class IsoWorldImformation
{
    public List<IsoBlockImformation> m_BlocksImformation;

    public IsoVector m_Size;
}

public class IsoBlockImformation
{
    public string m_BlockName;

    public IsoVector m_Pos;

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