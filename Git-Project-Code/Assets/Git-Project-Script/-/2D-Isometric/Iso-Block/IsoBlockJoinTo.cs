using UnityEngine;

[RequireComponent(typeof(IsoBlock))]
public class IsoBlockJoinTo : MonoBehaviour
{
    [Header("Join-To")]

    [Tooltip("Block to Move follow")]
    private GameObject g_JoinTo;

    [Tooltip("Pos of Block to Move follow")]
    private Vector3Int v3_JoinTo_Pos_Primary;

    private IsoBlock cl_Block;

    private bool m_JoinTo = false;

    private void Awake()
    {
        cl_Block = GetComponent<IsoBlock>();
    }

    private void Update()
    {
        Set_Active();
    }

    private void Set_Active()
    {
        if (m_JoinTo)
        {
            if (GetJoinToIsExist())
            {
                cl_Block.Set_Pos(g_JoinTo.GetComponent<IsoBlock>().GetPos_Current() + GetJoinTo_Pos_Distance());
            }
        }
    }

    #region Join-To 

    public void Set_JoinTo(GameObject g_Join)
    {
        if (g_Join == null)
        {
            return;
        }

        g_JoinTo = g_Join;
        v3_JoinTo_Pos_Primary = g_Join.GetComponent<IsoBlock>().GetPosOnMatrix_Primary();
        m_JoinTo = true;
    }

    public bool GetJoinToIsExist()
    {
        return g_JoinTo != null;
    }

    public void Set_JoinToBlock_Remove()
    {
        g_JoinTo = null;
        m_JoinTo = false;
    }

    public GameObject GetJoinTo()
    {
        return g_JoinTo;
    }

    #endregion

    #region Join-To Pos Primary 

    /// <summary>
    /// Get Join-To Pos Primary
    /// </summary>
    /// <returns></returns>
    public Vector3Int GetJoinTo_Pos_Primary()
    {
        return v3_JoinTo_Pos_Primary;
    }

    /// <summary>
    /// Get Join-To Pos Distance between Pos Primary
    /// </summary>
    /// <returns></returns>
    public Vector3Int GetJoinTo_Pos_Distance()
    {
        return cl_Block.GetPosOnMatrix_Primary() - g_JoinTo.GetComponent<IsoBlock>().GetPosOnMatrix_Primary();
    }

    #endregion
}
