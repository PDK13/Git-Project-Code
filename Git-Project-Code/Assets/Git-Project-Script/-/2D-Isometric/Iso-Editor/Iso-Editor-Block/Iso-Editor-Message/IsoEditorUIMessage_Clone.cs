using UnityEngine;
using UnityEngine.UI;

public class IsoEditorUIMessage_Clone : MonoBehaviour
{
    [Tooltip("Button Fix")]
    //[SerializeField]
    private IsoEditorMessage cl_Editor_Block_Message;

    [Header("Text for Message Clone")]

    [Tooltip("Message Name")]
    [SerializeField]
    private Text t_Message_Name;

    [Tooltip("Message Say")]
    [SerializeField]
    private Text t_Message_Message;

    [Header("Button  for Message Clone")]

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
        if (cl_Editor_Block_Message == null)
        {
            cl_Editor_Block_Message = GameObject.FindGameObjectWithTag("IsoWorldEditor").GetComponent<IsoEditorMessage>();
        }

        //ui_Button_Fix.Set_Event_Add_PointerDown(Button_Fix);
        ui_Button_Fix.Set_Button_Color_Active(ui_Button_Fix.Get_Button_Color_Normal());

        //ui_Button_Copy.Set_Event_Add_PointerDown(Button_Copy);
        ui_Button_Copy.Set_Button_Color_Active(ui_Button_Copy.Get_Button_Color_Normal());

        //ui_Button_Del.Set_Event_Add_PointerDown(Button_Del);
        ui_Button_Del.Set_Button_Color_Active(ui_Button_Del.Get_Button_Color_Normal());
    }

    #region Set Clone

    public void Set_Clone(string s_Message_Name, string s_Message_Message)
    {
        t_Message_Name.text = s_Message_Name;
        t_Message_Message.text = s_Message_Message;
    }

    #endregion

    #region Button Clone

    public void Button_Fix()
    {
        cl_Editor_Block_Message.Set_Fix(GetComponent<UIVerticalClone>().Get_Clone_Index());
    }

    public void Button_Copy()
    {
        cl_Editor_Block_Message.Set_Copy(GetComponent<UIVerticalClone>().Get_Clone_Index());
    }

    public void Button_Del()
    {
        cl_Editor_Block_Message.Set_Del(GetComponent<UIVerticalClone>().Get_Clone_Index());
    }

    #endregion
}
