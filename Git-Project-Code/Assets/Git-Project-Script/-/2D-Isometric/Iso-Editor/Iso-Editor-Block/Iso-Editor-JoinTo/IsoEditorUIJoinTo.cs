using UnityEngine;
using UnityEngine.UI;

public class IsoEditorUIJoinTo : MonoBehaviour
{
    #region Input Field for Join-To Pos

    [Header("Input Field for Join Pos")]

    [Tooltip("Input Join-To Pos X")]
    [SerializeField]
    private InputField inp_Pos_X;

    [Tooltip("Input Join-To Pos Y")]
    [SerializeField]
    private InputField inp_Pos_Y;

    [Tooltip("Input Join-To Pos H")]
    [SerializeField]
    private InputField inp_Pos_H;

    #endregion

    #region Text for Join-To Pos

    [Header("Text for Join-To Pos")]

    [Tooltip("Text for Join-To Pos")]
    [SerializeField]
    private Text t_Pos;

    #endregion

    #region Button Function for Join-To Pos

    [Header("Button Join-To for Join-To Pos")]

    [Tooltip("Button Join-To Pos Add")]
    [SerializeField]
    private UIButtonOnClick cl_Button_Add;

    [Tooltip("Button Join-To Pos Del")]
    [SerializeField]
    private UIButtonOnClick cl_Button_Del;

    #endregion

    private void Awake()
    {
        Set_Text();
    }

    #region Join-To Pos

    /// <summary>
    /// Set Join-To Pos Text
    /// </summary>
    /// <param name="v3_JoinTo_Pos"></param>
    public void Set_Text(Vector3Int v3_JoinTo_Pos)
    {
        t_Pos.text = v3_JoinTo_Pos.ToString();
    }

    /// <summary>
    /// Set Join-To Pos None Text
    /// </summary>
    public void Set_Text()
    {
        t_Pos.text = "NONE";
    }

    public Vector3Int Get_Pos()
    {
        return new Vector3Int(
            int.Parse(inp_Pos_X.text),
            int.Parse(inp_Pos_Y.text),
            int.Parse(inp_Pos_H.text));
    }

    public UIButtonOnClick Get_Button_Add()
    {
        return cl_Button_Add;
    }

    public UIButtonOnClick Get_Button_Del()
    {
        return cl_Button_Del;
    }

    #endregion
}
