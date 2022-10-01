using UnityEngine;
using UnityEngine.UI;

public class IsoEditorUISwitchToClone : MonoBehaviour
{
    [Tooltip("Button Fix")]
    //[SerializeField]
    private IsoEditorBlockSwitchTo cl_Editor_Block_SwitchTo;

    [Header("Text for Switch-To Clone")]

    [Tooltip("Switch-To Dir")]
    [SerializeField]
    private Text t_SwitchTo_Pos;

    [Header("Button  for Switch-To Clone")]

    [Tooltip("Button Fix")]
    [SerializeField]
    private UIButtonOnClick ui_Button_Fix;

    [Tooltip("Button Copy")]
    [SerializeField]
    private UIButtonOnClick ui_Button_Copy;

    [Tooltip("Button Delete")]
    [SerializeField]
    private UIButtonOnClick ui_Button_Del;

    private void Awake()
    {
        if (cl_Editor_Block_SwitchTo == null)
        {
            cl_Editor_Block_SwitchTo = GameObject.FindGameObjectWithTag("IsoWorldEditor").GetComponent<IsoEditorBlockSwitchTo>();
        }

        //ui_Button_Fix.Set_Event_Add_PointerDown(Button_Fix);
        ui_Button_Fix.Set_Button_Color_Active(ui_Button_Fix.GetButton_Color_Normal());

        //ui_Button_Copy.Set_Event_Add_PointerDown(Button_Copy);
        ui_Button_Copy.Set_Button_Color_Active(ui_Button_Copy.GetButton_Color_Normal());

        //ui_Button_Del.Set_Event_Add_PointerDown(Button_Del);
        ui_Button_Del.Set_Button_Color_Active(ui_Button_Del.GetButton_Color_Normal());
    }

    #region Set Clone

    /// <summary>
    /// Set Switch-To
    /// </summary>
    /// <param name="v3_SwitchTo_Pos"></param>
    public void Set_SwitchToBlockClone(Vector3Int v3_SwitchTo_Pos)
    {
        t_SwitchTo_Pos.text = v3_SwitchTo_Pos.ToString();
    }

    #endregion

    #region Button Clone

    /// <summary>
    /// Button Switch-To Fix
    /// </summary>
    public void Button_Fix()
    {
        cl_Editor_Block_SwitchTo.Set_Fix(GetComponent<UIVerticalClone>().GetClone_Index());
    }

    /// <summary>
    /// Button Switch-To Copy
    /// </summary>
    public void Button_Copy()
    {
        cl_Editor_Block_SwitchTo.Set_Copy(GetComponent<UIVerticalClone>().GetClone_Index());
    }

    /// <summary>
    /// Button Switch-To Del
    /// </summary>
    public void Button_Del()
    {
        cl_Editor_Block_SwitchTo.Set_Del(GetComponent<UIVerticalClone>().GetClone_Index());
    }

    #endregion
}
