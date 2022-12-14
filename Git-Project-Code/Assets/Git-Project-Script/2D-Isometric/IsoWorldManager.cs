using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsoWorldManager : MonoBehaviour
{
    private static IsoWorldManager m_This;

    [Header("World Manager")]

    [SerializeField] private IsoVector m_Scale = new IsoVector(1f, 1f, 1f);

    public static IsoVector Scale { get => m_This.m_Scale; }

    //[SerializeField]
    private List<IsoWorldBlockFloor> m_WorldBlock = new List<IsoWorldBlockFloor>() { new IsoWorldBlockFloor(0) };

    public static List<IsoWorldBlockFloor> WorldBlock
    {
        get => m_This.m_WorldBlock;
    }

    //[SerializeField]
    private List<GameObject> m_WorldPlayer = new List<GameObject>();
    //[SerializeField]
    private List<GameObject> m_WorldFriend = new List<GameObject>();
    //[SerializeField]
    private List<GameObject> m_WorldNeutral = new List<GameObject>();
    //[SerializeField]
    private List<GameObject> m_WorldEnermy = new List<GameObject>();
    //[SerializeField]
    private List<GameObject> m_WorldObject = new List<GameObject>();

    public static List<GameObject> WorldPlayer
    {
        get
        {
            return m_This.m_WorldPlayer;
        }
    }
    public static List<GameObject> WorldFriend
    {
        get
        {
            return m_This.m_WorldFriend;
        }
    }
    public static List<GameObject> WorldNeutral
    {
        get
        {
            return m_This.m_WorldNeutral;
        }
    }
    public static List<GameObject> WorldEnermy
    {
        get
        {
            return m_This.m_WorldEnermy;
        }
    }
    public static List<GameObject> WorldObject
    {
        get
        {
            return m_This.m_WorldObject;
        }
    }

    private bool m_WorldGenerated = false;
    private bool m_WorldGenerating = false;
    private bool m_WorldDestroying = false;
    public Action act_WorldGeneratedStart;
    public Action act_WorldGeneratedEnd;
    public Action act_WorldDestroyedStart;
    public Action act_WorldDestroyedEnd;

    public static List<IsoWorldBlockData> WorldData;

    [Header("Block(s) Manager")]

    //[SerializeField]
    private List<GameObject> m_Blocks;

    public static List<GameObject> Blocks
    {
        get => m_This.m_Blocks;
    }

    private bool m_BlocksUpdated = false;
    public Action act_BlocksUpdated;
    
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        SetWorldManagerStatic();
    }

    public void SetWorldManagerStatic()
    {
        if (m_This == null)
        {
            m_This = this;
        }
    }

    #region World Manager

    #region World Main Manager

    public static void SetWorldGenerate()
    {
        if (m_This.m_WorldGenerated || m_This.m_WorldGenerating) return;

        m_This.m_WorldGenerating = true;

        m_This.act_WorldGeneratedStart?.Invoke();
        
        foreach (IsoWorldBlockData m_BlockData in WorldData)
        {
            //Create!

            SetWorldCreate(m_BlockData.PosPrimary, GetBlock(m_BlockData.Name), m_BlockData.Codes);
        }

        m_This.m_WorldGenerated = true;

        m_This.m_WorldGenerating = false;

        m_This.act_WorldGeneratedEnd?.Invoke();
    }

    public static void SetWorldDestroy()
    {
        if (!m_This.m_WorldGenerated || m_This.m_WorldDestroying) return;

        m_This.m_WorldDestroying = true;

        m_This.m_WorldGenerated = false;

        m_This.act_WorldDestroyedStart?.Invoke();

        //Block(s)

        for (int i = 0; i < m_This.m_WorldBlock.Count; i++)
        {
            for (int j = 0; j < m_This.m_WorldBlock[i].m_BlocksFloor.Count; j++)
            {
                Destroy(GetWorldBlock(i, j));
            }
        }

        //None-Block(s)

        foreach (GameObject m_Block in m_This.m_WorldPlayer) Destroy(m_Block);

        foreach (GameObject m_Block in m_This.m_WorldFriend) Destroy(m_Block);

        foreach (GameObject m_Block in m_This.m_WorldNeutral) Destroy(m_Block);

        foreach (GameObject m_Block in m_This.m_WorldEnermy) Destroy(m_Block);

        foreach (GameObject m_Block in m_This.m_WorldObject) Destroy(m_Block);

        m_This.m_WorldDestroying = false;

        m_This.act_WorldDestroyedEnd?.Invoke();
    }

    public static bool GetWorldGenerated()
    {
        return m_This.m_WorldGenerated && !m_This.m_WorldGenerating && !m_This.m_WorldDestroying;
    }

    #endregion

    #region World Block(s) Manager

    //Create

    public static void SetWorldCreate(IsoVector m_Pos, GameObject m_Block, List<IsoCode> m_Codes)
    {
        if (!GetBlockCheck(m_Block)) return;

        m_This.SetWorldCreate(m_Pos, m_Block, m_Block.GetComponent<IsoBlock>().Type, m_Codes);
    }

    private void SetWorldCreate(IsoVector m_Pos, GameObject m_Block, IsoType m_Type, List<IsoCode> m_Codes)
    {
        if (!GetBlockCheck(m_Block)) return;

        switch (m_Type)
        {
            case IsoType.Block:
                m_This.SetWorldBlock(m_Pos, m_Block, m_Codes);
                break;
            case IsoType.Player:
            case IsoType.Friend:
            case IsoType.Neutral:
            case IsoType.Enermy:
            case IsoType.Object:
                m_This.SetWorldNoneBlock(m_Pos, m_Block, m_Type, m_Codes);
                break;
        }
    }

    //Remove

    public static void SetWorldRemove(IsoVector m_Pos, IsoType m_Type)
    {
        switch (m_Type)
        {
            case IsoType.Block:
                {
                    IsoWorldBlockIndex m_BlockPosFound = GetWorldBlockIndex(m_Pos);

                    if (m_BlockPosFound.m_FloorFound == true && m_BlockPosFound.m_BlockFound == true)
                    {
                        if (Application.isEditor)
                            DestroyImmediate(GetWorldBlock(m_BlockPosFound.m_FloorIndex, m_BlockPosFound.m_BlockIndex));
                        else
                            Destroy(GetWorldBlock(m_BlockPosFound.m_FloorIndex, m_BlockPosFound.m_BlockIndex));

                        m_This.m_WorldBlock[m_BlockPosFound.m_FloorIndex].m_BlocksFloor.RemoveAt(m_BlockPosFound.m_BlockIndex);
                    }
                }
                break;
            case IsoType.Player:
                {
                    List<GameObject> m_BlocksFound = GetWorldCurrent(m_Pos, IsoType.Player);

                    foreach(GameObject m_BlockFound in m_BlocksFound)
                    {
                        m_This.m_WorldPlayer.Remove(m_BlockFound);

                        if (Application.isEditor)
                            DestroyImmediate(m_BlockFound);
                        else
                            Destroy(m_BlockFound);
                    }
                }
                break;
            case IsoType.Friend:
                {
                    List<GameObject> m_BlocksFound = GetWorldCurrent(m_Pos, IsoType.Friend);

                    foreach (GameObject m_BlockFound in m_BlocksFound)
                    {
                        m_This.m_WorldPlayer.Remove(m_BlockFound);

                        if (Application.isEditor)
                            DestroyImmediate(m_BlockFound);
                        else
                            Destroy(m_BlockFound);
                    }
                }
                break;
            case IsoType.Neutral:
                {
                    List<GameObject> m_BlocksFound = GetWorldCurrent(m_Pos, IsoType.Neutral);

                    foreach (GameObject m_BlockFound in m_BlocksFound)
                    {
                        m_This.m_WorldPlayer.Remove(m_BlockFound);

                        if (Application.isEditor)
                            DestroyImmediate(m_BlockFound);
                        else
                            Destroy(m_BlockFound);
                    }
                }
                break;
            case IsoType.Enermy:
                {
                    List<GameObject> m_BlocksFound = GetWorldCurrent(m_Pos, IsoType.Enermy);

                    foreach (GameObject m_BlockFound in m_BlocksFound)
                    {
                        m_This.m_WorldPlayer.Remove(m_BlockFound);

                        if (Application.isEditor)
                            DestroyImmediate(m_BlockFound);
                        else
                            Destroy(m_BlockFound);
                    }
                }
                break;
            case IsoType.Object:
                {
                    List<GameObject> m_BlocksFound = GetWorldCurrent(m_Pos, IsoType.Object);

                    foreach (GameObject m_BlockFound in m_BlocksFound)
                    {
                        m_This.m_WorldPlayer.Remove(m_BlockFound);

                        if (Application.isEditor)
                            DestroyImmediate(m_BlockFound);
                        else
                            Destroy(m_BlockFound);
                    }
                }
                break;
        }
    }

    //Get

    public static List<GameObject> GetWorldCurrent(IsoVector m_Pos, IsoType m_Type)
    {
        switch (m_Type)
        {
            case IsoType.Block:
                return new List<GameObject>() { GetWorldBlock(m_Pos) };
            case IsoType.Player:
                return m_This.GetWorldNoneBlock(m_Pos, m_This.m_WorldPlayer);
            case IsoType.Friend:
                return m_This.GetWorldNoneBlock(m_Pos, m_This.m_WorldFriend);
            case IsoType.Neutral:
                return m_This.GetWorldNoneBlock(m_Pos, m_This.m_WorldNeutral);
            case IsoType.Enermy:
                return m_This.GetWorldNoneBlock(m_Pos, m_This.m_WorldEnermy);
            case IsoType.Object:
                return m_This.GetWorldNoneBlock(m_Pos, m_This.m_WorldObject);
        }
        return null;
    }

    #endregion

    #region World Type Block(s) Progess

    //Set

    private void SetWorldBlock(IsoVector m_Pos, GameObject m_Block, List<IsoCode> m_Codes)
    {
        IsoWorldBlockIndex m_BlockPosFound = GetWorldBlockIndex(m_Pos);

        if (m_BlockPosFound.m_FloorFound == false)
        {
            m_BlockPosFound.m_FloorIndex = m_This.SetWorldBlockFloorAdd(m_Pos);
        }

        if (m_BlockPosFound.m_BlockFound == true)
        {
            if (Application.isEditor)
                DestroyImmediate(GetWorldBlock(m_BlockPosFound.m_FloorIndex, m_BlockPosFound.m_BlockIndex));
            else
                Destroy(GetWorldBlock(m_BlockPosFound.m_FloorIndex, m_BlockPosFound.m_BlockIndex));
        }

        GameObject m_BlockClone = GitGameObject.SetGameObjectCreate(m_Block, m_This.transform);
        m_BlockClone.GetComponent<IsoBlock>().Scale = m_This.m_Scale;
        m_BlockClone.GetComponent<IsoBlock>().PosPrimary = m_Pos;
        m_BlockClone.GetComponent<IsoBlock>().Pos = m_Pos;

        if (m_Codes != null)
        {
            m_BlockClone.GetComponent<IsoBlock>().Codes = m_Codes;
        }

        if (m_BlockPosFound.m_BlockFound == true)
        {
            m_This.m_WorldBlock[m_BlockPosFound.m_FloorIndex].m_BlocksFloor[m_BlockPosFound.m_BlockIndex] = m_BlockClone;
        }
        else
        {
            m_This.m_WorldBlock[m_BlockPosFound.m_FloorIndex].m_BlocksFloor.Add(m_BlockClone);
        }
    }

    //Find & Range

    public static IsoWorldBlockIndex GetWorldBlockIndex(IsoVector m_Pos)
    {
        int m_FloorFoundIndex = m_This.GetWorldBlockFloorIndex(m_Pos);

        if (m_FloorFoundIndex == -1)
        {
            return new IsoWorldBlockIndex();
        }

        for (int i = 0; i < m_This.m_WorldBlock[m_FloorFoundIndex].m_BlocksFloor.Count; i++)
        {
            if (GetWorldBlock(m_FloorFoundIndex, i).GetComponent<IsoBlock>().Pos == m_Pos)
            {
                return new IsoWorldBlockIndex(true, m_FloorFoundIndex, true, i);
            }
        }

        return new IsoWorldBlockIndex(true, m_FloorFoundIndex, false);
    }

    private int GetWorldBlockFloorIndex(IsoVector m_Pos)
    {
        int m_FloorIndexA = 0;
        int m_FloorIndexB = m_WorldBlock.Count - 1;
        int m_FloorIndexFound = -1;

        do
        {
            m_FloorIndexFound = (m_FloorIndexA + m_FloorIndexB) / 2;

            if (m_WorldBlock[m_FloorIndexFound].m_WorldFloor == m_Pos.H_TB)
            {
                return m_FloorIndexFound;
            }
            else
            if (m_WorldBlock[m_FloorIndexFound].m_WorldFloor > m_Pos.H_TBInt)
            {
                m_FloorIndexB = m_FloorIndexFound - 1;
            }
            else
            if (m_WorldBlock[m_FloorIndexFound].m_WorldFloor < m_Pos.H_TBInt)
            {
                m_FloorIndexA = m_FloorIndexFound + 1;
            }
        }
        while (m_FloorIndexA <= m_FloorIndexB);

        return -1;
    }

    private int SetWorldBlockFloorAdd(IsoVector m_Pos)
    {
        m_WorldBlock.Add(new IsoWorldBlockFloor(m_Pos.H_TBInt));

        int m_FloorIndex = m_WorldBlock.Count - 1;

        for (int i = 0; i < m_WorldBlock.Count - 1; i++)
        {
            for (int j = i + 1; j < m_WorldBlock.Count; j++)
            {
                if (m_WorldBlock[i].m_WorldFloor > m_WorldBlock[j].m_WorldFloor)
                {
                    IsoWorldBlockFloor m_Temp = m_WorldBlock[i];
                    m_WorldBlock[i] = m_WorldBlock[j];
                    m_WorldBlock[j] = m_Temp;
                }
            }
        }

        for (int i = 0; i < m_WorldBlock.Count; i++) 
        {
            if (m_WorldBlock[i].m_WorldFloor == m_Pos.H_TBInt)
            {
                m_FloorIndex = i;
            }
        }

        return m_FloorIndex;
    }

    //Get

    public static GameObject GetWorldBlock(IsoVector m_Pos)
    {
        IsoWorldBlockIndex m_BlockPosFound = GetWorldBlockIndex(m_Pos);

        if (m_BlockPosFound.m_FloorFound == true && m_BlockPosFound.m_BlockFound == true)
        {
            return GetWorldBlock(m_BlockPosFound.m_FloorIndex, m_BlockPosFound.m_BlockIndex);
        }

        return null;
    }

    public static GameObject GetWorldBlock(int m_FloorIndex, int m_BlockIndex)
    {
        return m_This.m_WorldBlock[m_FloorIndex].m_BlocksFloor[m_BlockIndex];
    }

    #endregion

    #region World Type None-Block(s) Progess

    //Set

    private void SetWorldNoneBlock(IsoVector m_Pos, GameObject m_Block, IsoType m_IsoType, List<IsoCode> m_Codes)
    {
        GameObject m_BlockClone = GitGameObject.SetGameObjectCreate(m_Block, m_This.transform);
        m_BlockClone.GetComponent<IsoBlock>().Scale = m_This.m_Scale;
        m_BlockClone.GetComponent<IsoBlock>().PosPrimary = m_Pos;
        m_BlockClone.GetComponent<IsoBlock>().Pos = m_Pos;

        if (m_Codes != null)
        {
            m_BlockClone.GetComponent<IsoBlock>().Codes = m_Codes;
        }

        switch (m_IsoType)
        {
            case IsoType.Block:
                //...
                break;
            case IsoType.Player:
                m_This.m_WorldPlayer.Add(m_BlockClone);
                break;
            case IsoType.Friend:
                m_This.m_WorldFriend.Add(m_BlockClone);
                break;
            case IsoType.Neutral:
                m_This.m_WorldNeutral.Add(m_BlockClone);
                break;
            case IsoType.Enermy:
                m_This.m_WorldEnermy.Add(m_BlockClone);
                break;
            case IsoType.Object:
                m_This.m_WorldObject.Add(m_BlockClone);
                break;
        }
    }

    //Get

    private List<GameObject> GetWorldNoneBlock(IsoVector m_Pos, List<GameObject> m_World)
    {
        List<GameObject> m_WorldFound = new List<GameObject>();

        foreach (GameObject m_Block in m_World)
        {
            if (m_Block.GetComponent<IsoBlock>().Pos == m_Pos)
            {
                m_WorldFound.Add(m_Block);
            }
        }

        return m_WorldFound;
    }

    #endregion

    #endregion

    #region Renderer Block(s) Manager

    //List

    public static void SetBlock(List<GameObject> m_Blocks)
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

    public static bool GetBlock()
    {
        return m_This.m_BlocksUpdated;
    }

    public static List<GameObject> GetBlock(IsoType m_IsoType)
    {
        List<GameObject> m_BlockList = new List<GameObject>();

        foreach (GameObject m_Block in m_This.m_Blocks)
        {
            if (m_Block.GetComponent<IsoBlock>().Type == m_IsoType)
            {
                m_BlockList.Add(m_Block);
            }
        }

        return m_BlockList;
    }

    //Block

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

    public static bool GetBlockCheck(GameObject m_Block)
    {
        if (m_Block == null) return false;

        if (m_Block.GetComponent<IsoBlock>() == null) return false;

        return true;
    }

    #endregion
}

[System.Serializable]
public class IsoWorldBlockData
{
    [SerializeField] private string m_Name;
    [SerializeField] private IsoVector m_Pos;
    [SerializeField] private List<IsoCode> m_Codes;

    public string Name { get => m_Name; }
    public IsoVector PosPrimary { get => m_Pos; }
    public List<IsoCode> Codes { get => m_Codes; }

    public IsoWorldBlockData(string m_Name, IsoVector m_Pos, List<IsoCode> m_Codes)
    {
        this.m_Name = m_Name;
        this.m_Pos = m_Pos;
        this.m_Codes = m_Codes;
    }
}

[System.Serializable]
public struct IsoWorldBlockIndex
{
    public bool m_FloorFound;
    public int m_FloorIndex;

    public bool m_BlockFound;
    public int m_BlockIndex;
    
    public IsoWorldBlockIndex(bool m_FloorFound = false, int m_FloorIndex = -1, bool m_BlockFound = false, int m_BlockIndex = -1)
    {
        this.m_FloorFound = m_FloorFound;
        this.m_FloorIndex = m_FloorIndex;
        this.m_BlockFound = m_BlockFound;
        this.m_BlockIndex = m_BlockIndex;
    }
}

[System.Serializable]
public struct IsoWorldBlockFloor
{
    public List<GameObject> m_BlocksFloor;
    public int m_WorldFloor;

    public IsoWorldBlockFloor(int m_Floor)
    {
        this.m_BlocksFloor = new List<GameObject>();
        this.m_WorldFloor = m_Floor;
    }
}