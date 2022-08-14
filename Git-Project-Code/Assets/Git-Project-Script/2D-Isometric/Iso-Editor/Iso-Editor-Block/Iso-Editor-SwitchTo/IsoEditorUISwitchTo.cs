using UnityEngine;
using UnityEngine.UI;

public class IsoEditorUISwitchTo : MonoBehaviour
{
    [Header("Input Field for Switch-To Pos")]

    [Tooltip("Input Switch-To Pos X")]
    [SerializeField]
    private InputField inp_Pos_X;

    [Tooltip("Input Switch-To Pos Y")]
    [SerializeField]
    private InputField inp_Pos_Y;

    [Tooltip("Input Switch-To Pos H")]
    [SerializeField]
    private InputField inp_Pos_H;

    [Header("Button Switch-To for Switch-To List")]

    [Tooltip("Button Switch-To List Add")]
    [SerializeField]
    private UIButtonOnClick cl_Button_Add;

    [Tooltip("Button Switch-To List Del Lastest")]
    [SerializeField]
    private UIButtonOnClick cl_Button_Del_Lastest;

    #region Switch-To Pos

    public void Set_Clone(Vector3Int v3_SwitchTo_Pos)
    {
        inp_Pos_X.text = v3_SwitchTo_Pos.x.ToString();
        inp_Pos_Y.text = v3_SwitchTo_Pos.y.ToString();
        inp_Pos_H.text = v3_SwitchTo_Pos.z.ToString();
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

    public UIButtonOnClick Get_Button_Del_Lastest()
    {
        return cl_Button_Del_Lastest;
    }

    #endregion
}
