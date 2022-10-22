using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsoWorldManager : MonoBehaviour
{
    private static readonly IsoWorldManager m_This = new IsoWorldManager();

    public static IsoWorldManager GetThis()
    {
        return m_This;
    }

    [Header("World Manager")]

    [SerializeField] private IsoVector m_Scale = new IsoVector(1f, 1f, 1f);

    private List<IsoWorldFloor> m_World = new List<IsoWorldFloor>() { new IsoWorldFloor(0) };

    private int m_WorldPrimaryH = 0;

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
    }

//#if UNITY_EDITOR

    private void Start()
    {
        m_Blocks = GitResources.GetResourcesPrefab("Blocks");

        //StartCoroutine(Set());
    }

    //#endif

    #region World Manager

    public static void SetWorld(IsoVector m_Pos, GameObject m_Block)
    {
        IsoWorldIndex m_BlockPosFound = m_This.GetWorldIndex(m_Pos);

        if (m_BlockPosFound.m_FloorFound == false)
        {
            m_BlockPosFound.m_FloorIndex = m_This.SetWorldFloorAdd(m_Pos);
        }

        if (m_BlockPosFound.m_BlockIndex != -1)
        {
            Destroy(m_This.m_World[m_BlockPosFound.m_FloorIndex].m_WorldFloor[m_BlockPosFound.m_BlockIndex]);
        }

        GameObject m_BlockClone = GitGameObject.SetGameObjectCreate(m_Block, m_This.transform);
        m_BlockClone.GetComponent<IsoBlock>().SetScale(m_This.m_Scale);
        m_BlockClone.GetComponent<IsoBlock>().SetPosPrimary(m_Pos);
        m_BlockClone.GetComponent<IsoBlock>().Pos = m_Pos;

        if (m_BlockPosFound.m_BlockIndex == -1)
        {
            m_This.m_World[m_BlockPosFound.m_FloorIndex].m_WorldFloor.Add(m_BlockClone);
        }
        else
        {
            m_This.m_World[m_BlockPosFound.m_FloorIndex].m_WorldFloor[m_BlockPosFound.m_BlockIndex] = m_BlockClone;
        }
    }

    private IsoWorldIndex GetWorldIndex(IsoVector m_Pos)
    {
        int m_FloorFoundIndex = GetWorldFloorIndex(m_Pos);

        if (m_FloorFoundIndex == -1)
        {
            return new IsoWorldIndex();
        }

        for (int i = 0; i < m_World[m_FloorFoundIndex].m_WorldFloor.Count; i++) 
        {
            if (m_World[m_FloorFoundIndex].m_WorldFloor[i].GetComponent<IsoBlock>().Pos == m_Pos)
            {
                return new IsoWorldIndex(true, m_FloorFoundIndex, i);
            }
        }

        return new IsoWorldIndex(true, m_FloorFoundIndex);
    }

    private int GetWorldFloorIndex(IsoVector m_Pos)
    {
        int m_FloorIndexA = 0;
        int m_FloorIndexB = m_World.Count - 1;
        int m_FloorIndexFound = -1;

        do
        {
            m_FloorIndexFound = (m_FloorIndexA + m_FloorIndexB) / 2;

            if (m_World[m_FloorIndexFound].m_Floor == m_Pos.H_TB)
            {
                return m_FloorIndexFound;
            }
        }
        while (m_FloorIndexA < m_FloorIndexB);

        return -1;
    }

    private int SetWorldFloorAdd(IsoVector m_Pos)
    {
        m_World.Add(new IsoWorldFloor(m_Pos.H_TBInt));

        int m_FloorIndex = m_World.Count - 1;

        for (int i = 0; i < m_World.Count - 1; i++)
        {
            for (int j = i + 1; j < m_World.Count; j++)
            {
                if (m_World[i].m_Floor > m_World[j].m_Floor)
                {
                    IsoWorldFloor m_Temp = m_World[i];
                    m_World[i] = m_World[j];
                    m_World[j] = m_Temp;

                    if (m_World[i].m_Floor == m_Pos.H_TBInt)
                    {
                        m_FloorIndex = i;
                    }
                    else
                    if (m_World[j].m_Floor == m_Pos.H_TBInt)
                    {
                        m_FloorIndex = j;
                    }
                }
            }
        }

        return m_FloorIndex;
    }

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

public class IsoWorldIndex
{
    public bool m_FloorFound = false;
    public int m_FloorIndex = 0;
    public int m_BlockIndex = -1;
    
    public IsoWorldIndex(bool m_FloorFound = false, int m_FloorIndex = 0, int m_BlockIndex = -1)
    {
        this.m_FloorFound = m_FloorFound;
        this.m_FloorIndex = m_FloorIndex;
        this.m_BlockIndex = m_BlockIndex;
    }
}

public class IsoWorldFloor
{
    public List<GameObject> m_WorldFloor = new List<GameObject>();
    public int m_Floor = 0;

    public IsoWorldFloor(int m_Floor)
    {
        this.m_Floor = m_Floor;
    }
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