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
    private GameObject gClone;

    //[Tooltip("Check Component Required Exist in Clone")]
    //private bool m_Component = true;

    [Header("List-Vertical Data")]

    [Tooltip("List Vertical")]
    //[SerializeField]
    private List<GameObject> l_Vertical;

    private void Awake()
    {
        l_Vertical = new List<GameObject>();

        //if (gClone == null)
        //{
        //    m_Component = false;

        //    return;
        //}

        //if (gClone.GetComponent<VerticalClone>() == null)
        //{
        //    m_Component = false;

        //    return;
        //}

        //m_Component = true;
    }

    #region List 

    #region List Add

    /// <summary>
    /// Add Clone to List Vertical
    /// </summary>
    public void Set_ListVertical_Add()
    {
        //if (!m_Component)
        //{
        //    return;
        //}

        GameObject g_GameObject_Create = Class_Object.Set_GameObject_Create(gClone, t_Content);

        g_GameObject_Create.GetComponent<UIVerticalClone>().SetClone(this, l_Vertical.Count);

        l_Vertical.Add(g_GameObject_Create);
    }

    #endregion

    #region List Remove

    /// <summary>
    /// Remove Clone in List Vertical
    /// </summary>
    /// <param name="m_Index"></param>
    public void Set_ListVertical_Remove(int m_Index)
    {
        //if (!m_Component)
        //{
        //    return;
        //}

        if (m_Index < 0 || m_Index > l_Vertical.Count - 1)
        {
            return;
        }

        //Remove Clone at Index
        Class_Object.Set_GameObject_Destroy(l_Vertical[m_Index]);
        l_Vertical.RemoveAt(m_Index);

        //Fix Index other Clone
        for (int i = m_Index; i < l_Vertical.Count; i++)
        {
            GetListVerticalClone(i).SetClone_Index(i);
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
        //if (!m_Component)
        //{
        //    return;
        //}

        for (int i = 0; i < l_Vertical.Count; i++)
        {
            Class_Object.Set_GameObject_Destroy(l_Vertical[i]);
        }

        l_Vertical = new List<GameObject>();
    }

    #endregion

    #region List Get

    /// <summary>
    /// Get Count of List Vertical
    /// </summary>
    /// <returns></returns>
    public int GetListVerticalCount()
    {
        return l_Vertical.Count;
    }

    /// <summary>
    /// Get GameObject of Clone from List Vertical
    /// </summary>
    /// <param name="m_Index"></param>
    /// <returns></returns>
    public GameObject GetListVertical_GameObject(int m_Index)
    {
        //if (!m_Component)
        //{
        //    return null;
        //}

        if (m_Index < 0 || m_Index > l_Vertical.Count - 1)
        {
            return null;
        }

        return l_Vertical[m_Index];
    }

    /// <summary>
    /// Get GameObject of Clone from List Vertical Lastest
    /// </summary>
    /// <returns></returns>
    public GameObject GetListVertical_GameObject_Lastest()
    {
        return GetListVertical_GameObject(GetListVerticalCount() - 1);
    }

    /// <summary>
    /// Get Clone Component of Clone from List Vertical
    /// </summary>
    /// <param name="m_Index"></param>
    /// <returns></returns>
    public UIVerticalClone GetListVerticalClone(int m_Index)
    {
        //if (!m_Component)
        //{
        //    return null;
        //}

        if (m_Index < 0 || m_Index > l_Vertical.Count - 1)
        {
            return null;
        }

        return l_Vertical[m_Index].GetComponent<UIVerticalClone>();
    }

    /// <summary>
    /// Get Clone Component of Clone from List Vertical Lastest
    /// </summary>
    /// <returns></returns>
    public UIVerticalClone GetListVerticalClone_Lastest()
    {
        return GetListVerticalClone(GetListVerticalCount());
    }

    #endregion

    #endregion

    #region Clone 

    /// <summary>
    /// Set Clone for List Vertical
    /// </summary>
    /// <param name="gClone"></param>
    public void SetClone(GameObject gClone)
    {
        if (gClone == null)
        {
            return;
        }

        if (gClone.GetComponent<UIVerticalClone>() == null)
        {
            return;
        }

        //m_Component = true;
        this.gClone = gClone;
    }

    /// <summary>
    /// Set Clone Emty for List Vertical
    /// </summary>
    public void SetClone_Emty()
    {
        //m_Component = false;
        gClone = null;
    }

    /// <summary>
    /// Check Component Required Exist in Clone
    /// </summary>
    /// <returns></returns>
    //public bool GetClone_Component()
    //{
    //    return m_Component;
    //}

    #endregion
}
