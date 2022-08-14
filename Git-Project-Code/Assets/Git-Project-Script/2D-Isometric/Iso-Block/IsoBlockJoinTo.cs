using System.Collections;
using System.Collections.Generic;
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

    private bool b_JoinTo = false;

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
        if (b_JoinTo)
        {
            if (Get_JoinTo_isExist())
            {
                cl_Block.Set_Pos(g_JoinTo.GetComponent<IsoBlock>().Get_Pos_Current() + Get_JoinTo_Pos_Distance());
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

        this.g_JoinTo = g_Join;
        this.v3_JoinTo_Pos_Primary = g_Join.GetComponent<IsoBlock>().Get_PosOnMatrix_Primary();
        this.b_JoinTo = true;
    }

    public bool Get_JoinTo_isExist()
    {
        return this.g_JoinTo != null;
    }

    public void Set_JoinToBlock_Remove()
    {
        this.g_JoinTo = null;
        this.b_JoinTo = false;
    }

    public GameObject Get_JoinTo()
    {
        return g_JoinTo;
    }

    #endregion

    #region Join-To Pos Primary 

    /// <summary>
    /// Get Join-To Pos Primary
    /// </summary>
    /// <returns></returns>
    public Vector3Int Get_JoinTo_Pos_Primary()
    {
        return v3_JoinTo_Pos_Primary;
    }

    /// <summary>
    /// Get Join-To Pos Distance between Pos Primary
    /// </summary>
    /// <returns></returns>
    public Vector3Int Get_JoinTo_Pos_Distance()
    {
        return cl_Block.Get_PosOnMatrix_Primary() - g_JoinTo.GetComponent<IsoBlock>().Get_PosOnMatrix_Primary();
    }

    #endregion
}
