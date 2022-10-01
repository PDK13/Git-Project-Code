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
    public string s_Format = "- {0} -";

    /// <summary>
    /// Vertical List
    /// </summary>
    private UIVerticalList cl_List;

    /// <summary>
    /// Index in Vertical List
    /// </summary>
    private int i_Index = -1;

    /// <summary>
    /// Set this Clone when Add this Clone to Vertical List
    /// </summary>
    /// <param name="cl_List"></param>
    /// <param name="i_Index"></param>
    public void SetClone(UIVerticalList cl_List, int i_Index)
    {
        this.cl_List = cl_List;
        this.i_Index = i_Index;

        Set_Text_Numberic(i_Index);
    }

    /// <summary>
    /// Fix Index when Remove other Clone from Vertical List
    /// </summary>
    /// <param name="i_Index"></param>
    public void SetClone_Index(int i_Index)
    {
        this.i_Index = i_Index;

        Set_Text_Numberic(i_Index);
    }

    /// <summary>
    /// Get Index on Vertical List
    /// </summary>
    /// <returns></returns>
    public int GetClone_Index()
    {
        return i_Index;
    }

    /// <summary>
    /// Remove this Clone from Vertical List
    /// </summary>
    public void SetClone_Remove()
    {
        cl_List.Set_ListVertical_Remove(i_Index);
    }

    /// <summary>
    /// Set Numberic Text by Index and Format
    /// </summary>
    /// <param name="i_Index"></param>
    private void Set_Text_Numberic(int i_Index)
    {
        if (t_Numberic == null)
        {
            return;
        }

        if (!ClassString.GetStringIsExist(s_Format, "{0}"))
        //If Format not right
        {
            t_Numberic.text = i_Index.ToString();
        }
        else
        {
            t_Numberic.text = string.Format(s_Format, i_Index);
        }
    }
}
