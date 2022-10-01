using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IsoBlock))]
public class IsoBlockSwitchTo : MonoBehaviour
{
    [Header("Switch-To")]

    [Tooltip("List Pos Switch-To")]
    private List<Vector3Int> l_SwitchTo;

    private IsoBlock cl_Block;

    private void Awake()
    {
        l_SwitchTo = new List<Vector3Int>();
    }

    private void Start()
    {
        cl_Block = GetComponent<IsoBlock>();
    }

    #region Switch-To Active 

    /// <summary>
    /// Active Switch-To
    /// </summary>
    /// <param name="i_SwitchTo_Index"></param>
    /// <returns></returns>
    public GameObject Get_SwitchTo(int i_SwitchTo_Index)
    {
        return cl_Block.Get_World().Get_Primary_GameObject(l_SwitchTo[i_SwitchTo_Index]);
    }

    /// <summary>
    /// Active Switch-To
    /// </summary>
    /// <returns></returns>
    public List<GameObject> Get_SwitchTo()
    {
        List<GameObject> l_GameObject = new List<GameObject>();

        for (int i = 0; i < Get_Count(); i++)
        {
            l_GameObject.Add(Get_SwitchTo(i));
        }

        return l_GameObject;
    }

    #endregion

    #region Switch-To 

    #region Switch-To Add

    public void Set_Add(Vector3Int v3_SwitchTo_Pos)
    {
        l_SwitchTo.Add(v3_SwitchTo_Pos);
    }

    public void Set_List(List<Vector3Int> l_SwitchTo_Pos_List)
    {
        l_SwitchTo = l_SwitchTo_Pos_List;
    }

    #endregion

    #region SwitchTo List Chance

    public void Set_Chance(int i_SwitchTo_Index, Vector3Int v3_SwitchTo_Pos)
    {
        if (i_SwitchTo_Index > Get_Count() - 1)
        {
            return;
        }

        l_SwitchTo[i_SwitchTo_Index] = v3_SwitchTo_Pos;
    }

    #endregion

    #region Switch-To Remove

    public void Set_Remove(int i_SwitchTo_Index)
    {
        l_SwitchTo.RemoveAt(i_SwitchTo_Index);
    }

    public void Set_Remove_Lastest()
    {
        Set_Remove(Get_Count() - 1);
    }

    #endregion

    #region Switch-To Get

    public int Get_Count()
    {
        return l_SwitchTo.Count;
    }

    public Vector3Int Get_List(int i_SwitchTo_Index)
    {
        return l_SwitchTo[i_SwitchTo_Index];
    }

    public List<Vector3Int> Get_List()
    {
        return l_SwitchTo;
    }

    #endregion

    #endregion
}
