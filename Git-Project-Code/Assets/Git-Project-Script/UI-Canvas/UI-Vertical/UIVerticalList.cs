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
    //private bool m_AllowComponent = true;

    [Header("List-Vertical Data")]

    [Tooltip("List Vertical")]
    //[SerializeField]
    private List<GameObject> m_Vertical;

    private void Awake()
    {
        m_Vertical = new List<GameObject>();

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
    public void SetListVerticam_Add()
    {
        //if (!m_Component)
        //{
        //    return;
        //}

        GameObject m_GameObjectCreate = ClassObject.SetGameObjectCreate(gClone, t_Content);

        m_GameObjectCreate.GetComponent<UIVerticalClone>().SetClone(this, m_Vertical.Count);

        m_Vertical.Add(m_GameObjectCreate);
    }

    #endregion

    #region List Remove

    /// <summary>
    /// Remove Clone in List Vertical
    /// </summary>
    /// <param name="m_Index"></param>
    public void SetListVerticamRemove(int m_Index)
    {
        //if (!m_Component)
        //{
        //    return;
        //}

        if (m_Index < 0 || m_Index > m_Vertical.Count - 1)
        {
            return;
        }

        //Remove Clone at Index
        ClassObject.SetGameObjectDestroy(m_Vertical[m_Index]);
        m_Vertical.RemoveAt(m_Index);

        //Fix Index other Clone
        for (int i = m_Index; i < m_Vertical.Count; i++)
        {
            GetListVerticalClone(i).SetClone_Index(i);
        }
    }

    /// <summary>
    /// Remove Clone Lastest in List Vertical
    /// </summary>
    public void SetListVerticamRemoveLastest()
    {
        SetListVerticamRemove(m_Vertical.Count - 1);
    }

    /// <summary>
    /// Remove All Clone(s) in List Vertical
    /// </summary>
    public void SetListVerticamRemoveAll()
    {
        //if (!m_Component)
        //{
        //    return;
        //}

        for (int i = 0; i < m_Vertical.Count; i++)
        {
            ClassObject.SetGameObjectDestroy(m_Vertical[i]);
        }

        m_Vertical = new List<GameObject>();
    }

    #endregion

    #region List Get

    /// <summary>
    /// Get Count of List Vertical
    /// </summary>
    /// <returns></returns>
    public int GetListVerticalCount()
    {
        return m_Vertical.Count;
    }

    /// <summary>
    /// Get GameObject of Clone from List Vertical
    /// </summary>
    /// <param name="m_Index"></param>
    /// <returns></returns>
    public GameObject GetListVerticam_GameObject(int m_Index)
    {
        //if (!m_Component)
        //{
        //    return null;
        //}

        if (m_Index < 0 || m_Index > m_Vertical.Count - 1)
        {
            return null;
        }

        return m_Vertical[m_Index];
    }

    /// <summary>
    /// Get GameObject of Clone from List Vertical Lastest
    /// </summary>
    /// <returns></returns>
    public GameObject GetListVerticam_GameObjectLastest()
    {
        return GetListVerticam_GameObject(GetListVerticalCount() - 1);
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

        if (m_Index < 0 || m_Index > m_Vertical.Count - 1)
        {
            return null;
        }

        return m_Vertical[m_Index].GetComponent<UIVerticalClone>();
    }

    /// <summary>
    /// Get Clone Component of Clone from List Vertical Lastest
    /// </summary>
    /// <returns></returns>
    public UIVerticalClone GetListVerticalCloneLastest()
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
    //public bool GetCheckClone_Component()
    //{
    //    return m_Component;
    //}

    #endregion
}
