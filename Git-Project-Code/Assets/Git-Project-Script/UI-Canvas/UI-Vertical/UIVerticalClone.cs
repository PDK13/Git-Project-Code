using UnityEngine;
using UnityEngine.UI;

public class UIVerticalClone : MonoBehaviour
{
    /// <summary>
    /// Text Numberic
    /// </summary>
    [Header("Numberic")]
    public Text t_Numberic;

    /// <summary>
    /// Text Numberic Format
    /// </summary>
    [Tooltip("Remember to add \"{0}\" in this Format string")]
    public string m_Format = "- {0} -";

    /// <summary>
    /// Vertical List
    /// </summary>
    private UIVerticalList cmList;

    /// <summary>
    /// Index in Vertical List
    /// </summary>
    private int m_Index = -1;

    /// <summary>
    /// Set this Clone when Add this Clone to Vertical List
    /// </summary>
    /// <param name="cmList"></param>
    /// <param name="m_Index"></param>
    public void SetClone(UIVerticalList cmList, int m_Index)
    {
        this.cmList = cmList;
        this.m_Index = m_Index;

        SetText_Numberic(m_Index);
    }

    /// <summary>
    /// Fix Index when Remove other Clone from Vertical List
    /// </summary>
    /// <param name="m_Index"></param>
    public void SetClone_Index(int m_Index)
    {
        this.m_Index = m_Index;

        SetText_Numberic(m_Index);
    }

    /// <summary>
    /// Get Index on Vertical List
    /// </summary>
    /// <returns></returns>
    public int GetClone_Index()
    {
        return m_Index;
    }

    /// <summary>
    /// Remove this Clone from Vertical List
    /// </summary>
    public void SetCloneRemove()
    {
        cmList.SetListVerticamRemove(m_Index);
    }

    /// <summary>
    /// Set Numberic Text by Index and Format
    /// </summary>
    /// <param name="m_Index"></param>
    private void SetText_Numberic(int m_Index)
    {
        if (t_Numberic == null)
        {
            return;
        }

        if (!ClassString.GetCheckStringExist(m_Format, "{0}"))
        //If Format not right
        {
            t_Numberic.text = m_Index.ToString();
        }
        else
        {
            t_Numberic.text = string.Format(m_Format, m_Index);
        }
    }
}
