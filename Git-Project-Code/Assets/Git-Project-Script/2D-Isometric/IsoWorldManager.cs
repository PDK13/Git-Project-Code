using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsoWorldManager : MonoBehaviour
{
    private static IsoWorldManager m_This;

    [Header("World Manager")]

    [SerializeField] private IsoVector m_Scale = new IsoVector(1f, 1f, 1f);

    private List<IsoWorldFloor> m_WorldBlock = new List<IsoWorldFloor>() { new IsoWorldFloor(0) };

    private List<GameObject> m_WorldPlayer = new List<GameObject>();
    private List<GameObject> m_WorldFriend = new List<GameObject>();
    private List<GameObject> m_WorldNeutral = new List<GameObject>();
    private List<GameObject> m_WorldEnermy = new List<GameObject>();
    private List<GameObject> m_WorldObject = new List<GameObject>();

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

        m_This = this;
    }

//#if UNITY_EDITOR

    private void Start()
    {
        m_Blocks = GitResources.GetResourcesPrefab("Blocks");

        //StartCoroutine(Set());
    }

    private IEnumerator Set()
    {
        SetWorld(new IsoVector(2, 2, 5), m_Blocks[0]);
        SetWorld(new IsoVector(2, 2, 2), m_Blocks[0]);
        SetWorld(new IsoVector(2, 2, 7), m_Blocks[0]);

        yield return null;

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

        yield return null;

        int m_Color = 0;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                for (int t = 0; t < 3; t++)
                {
                    SetWorld(new IsoVector(i, j, t), m_Blocks[m_Color]);
                    m_Color++;

                    if (m_Color > m_Blocks.Count - 1)
                    {
                        m_Color = 0;
                    }

                    yield return null;

                    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
                }
            }
        }
    }

    //#endif

    #region World Manager

    public static void SetWorldGenerate()
    {

    }

    public static void SetWorld(IsoVector m_Pos, GameObject m_Block)
    {
        if (m_Block == null)
        {
            Debug.LogWarningFormat("{0}: Not found block?!.", m_This.name);
            return;
        }

        IsoBlock m_IsoBlock = m_Block.GetComponent<IsoBlock>();

        if (m_IsoBlock == null)
        {
            Debug.LogWarningFormat("{0}: Not found Iso Block component on block {1}.", m_This.name, m_Block.name);
            return;
        }

        switch (m_IsoBlock.Type)
        {
            case IsoType.Block:
                {
                    IsoWorldIndex m_BlockPosFound = m_This.GetWorldBlockIndex(m_Pos);

                    if (m_BlockPosFound.m_FloorFound == false)
                    {
                        m_BlockPosFound.m_FloorIndex = m_This.SetWorldBlockFloorAdd(m_Pos);
                    }

                    if (m_BlockPosFound.m_BlockIndex != -1)
                    {
                        Destroy(m_This.m_WorldBlock[m_BlockPosFound.m_FloorIndex].m_WorldFloor[m_BlockPosFound.m_BlockIndex]);
                    }

                    GameObject m_BlockClone = GitGameObject.SetGameObjectCreate(m_Block, m_This.transform);
                    m_BlockClone.GetComponent<IsoBlock>().Scale = m_This.m_Scale;
                    m_BlockClone.GetComponent<IsoBlock>().PosPrimary = m_Pos;
                    m_BlockClone.GetComponent<IsoBlock>().Pos = m_Pos;

                    if (m_BlockPosFound.m_BlockIndex != -1)
                    {
                        m_This.m_WorldBlock[m_BlockPosFound.m_FloorIndex].m_WorldFloor[m_BlockPosFound.m_BlockIndex] = m_BlockClone;
                    }
                    else
                    {
                        m_This.m_WorldBlock[m_BlockPosFound.m_FloorIndex].m_WorldFloor.Add(m_BlockClone);
                    }
                }
                break;
            case IsoType.Player:
            case IsoType.Friend:
            case IsoType.Neutral:
            case IsoType.Enermy:
            case IsoType.Object:
                {
                    GameObject m_BlockClone = GitGameObject.SetGameObjectCreate(m_Block, m_This.transform);
                    m_BlockClone.GetComponent<IsoBlock>().Scale = m_This.m_Scale;
                    m_BlockClone.GetComponent<IsoBlock>().PosPrimary = m_Pos;
                    m_BlockClone.GetComponent<IsoBlock>().Pos = m_Pos;

                    switch (m_IsoBlock.Type)
                    {

                    }
                }
                break;
        }
    }

    #region World Type Block Manager

    private void SetWorldBlock(IsoVector m_Pos, GameObject m_Block)
    {
        
    }

    private IsoWorldIndex GetWorldBlockIndex(IsoVector m_Pos)
    {
        int m_FloorFoundIndex = GetWorldBlockFloorIndex(m_Pos);

        if (m_FloorFoundIndex == -1)
        {
            return new IsoWorldIndex();
        }

        for (int i = 0; i < m_WorldBlock[m_FloorFoundIndex].m_WorldFloor.Count; i++) 
        {
            if (m_WorldBlock[m_FloorFoundIndex].m_WorldFloor[i].GetComponent<IsoBlock>().Pos == m_Pos)
            {
                return new IsoWorldIndex(true, m_FloorFoundIndex, i);
            }
        }

        return new IsoWorldIndex(true, m_FloorFoundIndex);
    }

    private int GetWorldBlockFloorIndex(IsoVector m_Pos)
    {
        int m_FloorIndexA = 0;
        int m_FloorIndexB = m_WorldBlock.Count - 1;
        int m_FloorIndexFound = -1;

        do
        {
            m_FloorIndexFound = (m_FloorIndexA + m_FloorIndexB) / 2;

            if (m_WorldBlock[m_FloorIndexFound].m_Floor == m_Pos.H_TB)
            {
                return m_FloorIndexFound;
            }
            else
            if (m_WorldBlock[m_FloorIndexFound].m_Floor > m_Pos.H_TBInt)
            {
                m_FloorIndexB = m_FloorIndexFound - 1;
            }
            else
            if (m_WorldBlock[m_FloorIndexFound].m_Floor < m_Pos.H_TBInt)
            {
                m_FloorIndexA = m_FloorIndexFound + 1;
            }
        }
        while (m_FloorIndexA <= m_FloorIndexB);

        return -1;
    }

    private int SetWorldBlockFloorAdd(IsoVector m_Pos)
    {
        m_WorldBlock.Add(new IsoWorldFloor(m_Pos.H_TBInt));

        int m_FloorIndex = m_WorldBlock.Count - 1;

        for (int i = 0; i < m_WorldBlock.Count - 1; i++)
        {
            for (int j = i + 1; j < m_WorldBlock.Count; j++)
            {
                if (m_WorldBlock[i].m_Floor > m_WorldBlock[j].m_Floor)
                {
                    IsoWorldFloor m_Temp = m_WorldBlock[i];
                    m_WorldBlock[i] = m_WorldBlock[j];
                    m_WorldBlock[j] = m_Temp;
                }
            }
        }

        for (int i = 0; i < m_WorldBlock.Count; i++) 
        {
            if (m_WorldBlock[i].m_Floor == m_Pos.H_TBInt)
            {
                m_FloorIndex = i;
            }
        }

        return m_FloorIndex;
    }

    #endregion

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

    public static bool GetBlocksUpdated()
    {
        return m_This.m_BlocksUpdated;
    }

    public static GameObject GetBlock(string m_BlockName)
    {
        foreach (GameObject m_Block in m_This.m_Blocks)
        {
            if (m_Block.name == m_BlockName)
            {
                return m_Block;
            }
        }

        Debug.LogWarningFormat("{0}: Not found block {1}!", m_This.name, m_BlockName);

        return null;
    }

    #endregion
}

public struct IsoWorldIndex
{
    public bool m_FloorFound;
    public int m_FloorIndex;
    public int m_BlockIndex;
    
    public IsoWorldIndex(bool m_FloorFound = false, int m_FloorIndex = 0, int m_BlockIndex = -1)
    {
        this.m_FloorFound = m_FloorFound;
        this.m_FloorIndex = m_FloorIndex;
        this.m_BlockIndex = m_BlockIndex;
    }
}

public struct IsoWorldFloor
{
    public List<GameObject> m_WorldFloor;
    public int m_Floor;

    public IsoWorldFloor(int m_Floor)
    {
        this.m_WorldFloor = new List<GameObject>();
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