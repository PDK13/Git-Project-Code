using System.Collections.Generic;
using UnityEngine;

public class UIVerticalList : MonoBehaviour
{
    [Header("List-Vertical Manager")]

    [SerializeField]
    [Tooltip("Transform to Hold Vertical Clone(s) of List")]
    private Transform t_Content;

    [Header("List-Vertical Clone(s)")]

    [Tooltip("Vertical Clone(s) will Add to List")]
    [SerializeField]
    private GameObject g_Clone;

    //[Tooltip("Check Component Required Exist in Clone")]
    //private bool b_Component = true;

    [Header("List-Vertical Data")]

    [Tooltip("List Vertical")]
    //[SerializeField]
    private List<GameObject> l_Vertical;

    private void Awake()
    {
        l_Vertical = new List<GameObject>();

        //if (g_Clone == null)
        //{
        //    b_Component = false;

        //    return;
        //}

        //if (g_Clone.GetComponent<Vertical_Clone>() == null)
        //{
        //    b_Component = false;

        //    return;
        //}

        //b_Component = true;
    }

    #region List 

    #region List Add

    /// <summary>
    /// Add Clone to List Vertical
    /// </summary>
    public void Set_ListVertical_Add()
    {
        //if (!b_Component)
        //{
        //    return;
        //}

        GameObject g_GameObject_Create = Class_Object.Set_Prepab_Create(g_Clone, t_Content);

        g_GameObject_Create.GetComponent<UIVerticalClone>().Set_Clone(this, l_Vertical.Count);

        l_Vertical.Add(g_GameObject_Create);
    }

    #endregion

    #region List Remove

    /// <summary>
    /// Remove Clone in List Vertical
    /// </summary>
    /// <param name="i_Index"></param>
    public void Set_ListVertical_Remove(int i_Index)
    {
        //if (!b_Component)
        //{
        //    return;
        //}

        if (i_Index < 0 || i_Index > l_Vertical.Count - 1)
        {
            return;
        }

        //Remove Clone at Index
        Class_Object.Set_Destroy_GameObject(l_Vertical[i_Index]);
        l_Vertical.RemoveAt(i_Index);

        //Fix Index other Clone
        for (int i = i_Index; i < l_Vertical.Count; i++)
        {
            Get_ListVertical_Clone(i).Set_Clone_Index(i);
        }
    }

    /// <summary>
    /// Remove Clone Lastest in List Vertical
    /// </summary>
    public void Set_ListVertical_Remove_Lastest()
    {
        Set_ListVertical_Remove(l_Vertical.Count - 1);
    }

    /// <summary>
    /// Remove All Clone(s) in List Vertical
    /// </summary>
    public void Set_ListVertical_Remove_All()
    {
        //if (!b_Component)
        //{
        //    return;
        //}

        for (int i = 0; i < l_Vertical.Count; i++)
        {
            Class_Object.Set_Destroy_GameObject(l_Vertical[i]);
        }

        l_Vertical = new List<GameObject>();
    }

    #endregion

    #region List Get

    /// <summary>
    /// Get Count of List Vertical
    /// </summary>
    /// <returns></returns>
    public int Get_ListVertical_Count()
    {
        return l_Vertical.Count;
    }

    /// <summary>
    /// Get GameObject of Clone from List Vertical
    /// </summary>
    /// <param name="i_Index"></param>
    /// <returns></returns>
    public GameObject Get_ListVertical_GameObject(int i_Index)
    {
        //if (!b_Component)
        //{
        //    return null;
        //}

        if (i_Index < 0 || i_Index > l_Vertical.Count - 1)
        {
            return null;
        }

        return l_Vertical[i_Index];
    }

    /// <summary>
    /// Get GameObject of Clone from List Vertical Lastest
    /// </summary>
    /// <returns></returns>
    public GameObject Get_ListVertical_GameObject_Lastest()
    {
        return Get_ListVertical_GameObject(Get_ListVertical_Count() - 1);
    }

    /// <summary>
    /// Get Clone Component of Clone from List Vertical
    /// </summary>
    /// <param name="i_Index"></param>
    /// <returns></returns>
    public UIVerticalClone Get_ListVertical_Clone(int i_Index)
    {
        //if (!b_Component)
        //{
        //    return null;
        //}

        if (i_Index < 0 || i_Index > l_Vertical.Count - 1)
        {
            return null;
        }

        return l_Vertical[i_Index].GetComponent<UIVerticalClone>();
    }

    /// <summary>
    /// Get Clone Component of Clone from List Vertical Lastest
    /// </summary>
    /// <returns></returns>
    public UIVerticalClone Get_ListVertical_Clone_Lastest()
    {
        return Get_ListVertical_Clone(Get_ListVertical_Count());
    }

    #endregion

    #endregion

    #region Clone 

    /// <summary>
    /// Set Clone for List Vertical
    /// </summary>
    /// <param name="g_Clone"></param>
    public void Set_Clone(GameObject g_Clone)
    {
        if (g_Clone == null)
        {
            return;
        }

        if (g_Clone.GetComponent<UIVerticalClone>() == null)
        {
            return;
        }

        //b_Component = true;
        this.g_Clone = g_Clone;
    }

    /// <summary>
    /// Set Clone Emty for List Vertical
    /// </summary>
    public void Set_Clone_Emty()
    {
        //b_Component = false;
        this.g_Clone = null;
    }

    /// <summary>
    /// Check Component Required Exist in Clone
    /// </summary>
    /// <returns></returns>
    //public bool Get_Clone_Component()
    //{
    //    return b_Component;
    //}

    #endregion
}
