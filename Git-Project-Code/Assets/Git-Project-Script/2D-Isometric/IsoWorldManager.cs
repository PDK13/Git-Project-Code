using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsoWorldManager : MonoBehaviour
{
    private static IsoWorldManager m_This;

    [Header("World Manager")]

    [SerializeField] private IsoVector m_Scale = new IsoVector(1f, 1f, 1f);

    //[SerializeField]
    private List<IsoWorldBlockFloor> m_WorldBlock = new List<IsoWorldBlockFloor>() { new IsoWorldBlockFloor(0) };

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

    [Header("Block(s) Manager")]

    //[SerializeField]
    private List<GameObject> m_Blocks;

    private bool m_BlocksUpdated = false;

    public Action act_BlocksUpdated;
    public Action act_WorldGenerated;
    public Action act_WorldDestroyed;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        m_This = this;
    }

    #region World Manager

    #region World Main Manager

    public static void SetWorldGenerate()
    {

    }

    public static void SetWorld(IsoVector m_Pos, GameObject m_Block)
    {
        SetWorld(m_Pos, m_Block, m_Block.GetComponent<IsoBlock>().Type);
    }

    public static void SetWorld(IsoVector m_Pos, GameObject m_Block, IsoType m_Type)
    {
        switch (m_Type)
        {
            case IsoType.Block:
                m_This.SetWorldBlock(m_Pos, m_Block);
                break;
            case IsoType.Player:
            case IsoType.Friend:
            case IsoType.Neutral:
            case IsoType.Enermy:
            case IsoType.Object:
                m_This.SetWorldNoneBlock(m_Pos, m_Block, m_Type);
                break;
        }
    }

    public static void SetWorldDestroy(GameObject m_BlockCheck)
    {
        IsoBlock m_IsoBlock = m_BlockCheck.GetComponent<IsoBlock>();

        switch (m_IsoBlock.Type)
        {
            case IsoType.Block:
                {
                    IsoWorldBlockIndex m_BlockPosFound = m_This.GetWorldBlockIndex(m_IsoBlock.Pos);

                    if (m_BlockPosFound.m_FloorFound == true && m_BlockPosFound.m_BlockFound == true)
                    {
                        Destroy(m_This.m_WorldBlock[m_BlockPosFound.m_FloorIndex].m_BlocksFloor[m_BlockPosFound.m_BlockIndex]);
                        m_This.m_WorldBlock[m_BlockPosFound.m_FloorIndex].m_BlocksFloor.RemoveAt(m_BlockPosFound.m_BlockIndex);
                    }
                }
                break;
            case IsoType.Player:
                m_This.m_WorldPlayer.Remove(m_BlockCheck);
                Destroy(m_BlockCheck);
                break;
            case IsoType.Friend:
                m_This.m_WorldFriend.Remove(m_BlockCheck);
                Destroy(m_BlockCheck);
                break;
            case IsoType.Neutral:
                m_This.m_WorldNeutral.Remove(m_BlockCheck);
                Destroy(m_BlockCheck);
                break;
            case IsoType.Enermy:
                m_This.m_WorldEnermy.Remove(m_BlockCheck);
                Destroy(m_BlockCheck);
                break;
            case IsoType.Object:
                m_This.m_WorldObject.Remove(m_BlockCheck);
                Destroy(m_BlockCheck);
                break;
        }
    }

    public static List<GameObject> GetWorld(IsoVector m_Pos, IsoType m_Type)
    {
        switch (m_Type)
        {
            case IsoType.Block:
                return new List<GameObject>() { m_This.GetWorldBlock(m_Pos) };
            case IsoType.Player:
                return m_This.GetWorldBlock(m_Pos, m_This.m_WorldPlayer);
            case IsoType.Friend:
                return m_This.GetWorldBlock(m_Pos, m_This.m_WorldFriend);
            case IsoType.Neutral:
                return m_This.GetWorldBlock(m_Pos, m_This.m_WorldNeutral);
            case IsoType.Enermy:
                return m_This.GetWorldBlock(m_Pos, m_This.m_WorldEnermy);
            case IsoType.Object:
                return m_This.GetWorldBlock(m_Pos, m_This.m_WorldObject);
        }
        return null;
    }

    #endregion

    #region World Type Block Manager

    //Set

    private void SetWorldBlock(IsoVector m_Pos, GameObject m_Block)
    {
        IsoWorldBlockIndex m_BlockPosFound = m_This.GetWorldBlockIndex(m_Pos);

        if (m_BlockPosFound.m_FloorFound == false)
        {
            m_BlockPosFound.m_FloorIndex = m_This.SetWorldBlockFloorAdd(m_Pos);
        }

        if (m_BlockPosFound.m_BlockFound == true)
        {
            Destroy(m_This.GetWorldBlock(m_BlockPosFound.m_FloorIndex, m_BlockPosFound.m_BlockIndex));
        }

        GameObject m_BlockClone = GitGameObject.SetGameObjectCreate(m_Block, m_This.transform);
        m_BlockClone.GetComponent<IsoBlock>().Scale = m_This.m_Scale;
        m_BlockClone.GetComponent<IsoBlock>().PosPrimary = m_Pos;
        m_BlockClone.GetComponent<IsoBlock>().Pos = m_Pos;

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

    private IsoWorldBlockIndex GetWorldBlockIndex(IsoVector m_Pos)
    {
        int m_FloorFoundIndex = GetWorldBlockFloorIndex(m_Pos);

        if (m_FloorFoundIndex == -1)
        {
            return new IsoWorldBlockIndex();
        }

        for (int i = 0; i < m_WorldBlock[m_FloorFoundIndex].m_BlocksFloor.Count; i++)
        {
            if (GetWorldBlock(m_FloorFoundIndex, i).GetComponent<IsoBlock>().Pos == m_Pos)
            {
                return new IsoWorldBlockIndex(true, m_FloorFoundIndex, true, i);
            }
        }

        return new IsoWorldBlockIndex(true, m_FloorFoundIndex, false);
    }

    private GameObject GetWorldBlock(int m_FloorIndex, int m_BlockIndex)
    {
        return m_WorldBlock[m_FloorIndex].m_BlocksFloor[m_BlockIndex];
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

    private GameObject GetWorldBlock(IsoVector m_Pos)
    {
        IsoWorldBlockIndex m_BlockPosFound = m_This.GetWorldBlockIndex(m_Pos);

        if (m_BlockPosFound.m_FloorFound == true && m_BlockPosFound.m_BlockFound == true)
        {
            return m_This.GetWorldBlock(m_BlockPosFound.m_FloorIndex, m_BlockPosFound.m_BlockIndex);
        }

        return null;
    }

    #endregion

    #region World Type None Block Manager

    //Set

    private void SetWorldNoneBlock(IsoVector m_Pos, GameObject m_Block, IsoType m_IsoType)
    {
        GameObject m_BlockClone = GitGameObject.SetGameObjectCreate(m_Block, m_This.transform);
        m_BlockClone.GetComponent<IsoBlock>().Scale = m_This.m_Scale;
        m_BlockClone.GetComponent<IsoBlock>().PosPrimary = m_Pos;
        m_BlockClone.GetComponent<IsoBlock>().Pos = m_Pos;

        switch (m_IsoType)
        {
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

    private List<GameObject> GetWorldBlock(IsoVector m_Pos, List<GameObject> m_World)
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