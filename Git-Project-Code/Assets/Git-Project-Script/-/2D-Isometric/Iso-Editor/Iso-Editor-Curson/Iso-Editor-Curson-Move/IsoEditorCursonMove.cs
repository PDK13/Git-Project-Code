using UnityEngine;

public class IsoEditorCursonMove : MonoBehaviour
{
    #region Editor

    [Header("Button Move Curson on World Matrix")]

    [Tooltip("Curson Move UP")]
    [SerializeField]
    private KeyCode k_Move_Up = KeyCode.UpArrow;

    [Tooltip("Curson Move DOWN")]
    [SerializeField]
    private KeyCode k_Move_Down = KeyCode.DownArrow;

    [Tooltip("Curson Move LEFT")]
    [SerializeField]
    private KeyCode k_Move_Left = KeyCode.LeftArrow;

    [Tooltip("Curson Move RIGHT")]
    [SerializeField]
    private KeyCode k_Move_Right = KeyCode.RightArrow;

    [Tooltip("Curson Move TOP")]
    [SerializeField]
    private KeyCode k_Move_Top = KeyCode.PageUp;

    [Tooltip("Curson Move BOT")]
    [SerializeField]
    private KeyCode k_Move_Bot = KeyCode.PageDown;

    [Header("UI Component")]

    [Tooltip("Editor Pos and Size UI")]
    [SerializeField]
    private IsoEditorUICursonMove iso_Editor_Curson_Move_UI;

    #endregion

    private IsoWorld iso_World;

    private IsoBlock iso_Block;

    private IsoEditorWorld iso_Editor_World;

    private IsoEditorWorldMatrix iso_Editor_World_Matrix;

    private void Awake()
    {
        iso_World = GameObject.FindGameObjectWithTag("IsoWorldManager").GetComponent<IsoWorld>();

        iso_Block = GetComponent<IsoBlock>();

        iso_Editor_World = GetComponent<IsoEditorWorld>();

        iso_Editor_World_Matrix = GetComponent<IsoEditorWorldMatrix>();

        iso_Editor_Curson_Move_UI.GetButton_Move_Up().Set_Button_Keycode(k_Move_Up);
        //iso_Editor_Curson_Move_UI.GetButton_Move_Up().Set_Event_Add_PointerDown(Button_Dir_Up);
        iso_Editor_Curson_Move_UI.GetButton_Move_Up().Set_Button_Color_Active(iso_Editor_Curson_Move_UI.GetButton_Move_Up().GetButton_Color_Normal());

        iso_Editor_Curson_Move_UI.GetButton_Move_Down().Set_Button_Keycode(k_Move_Down);
        //iso_Editor_Curson_Move_UI.GetButton_Move_Down().Set_Event_Add_PointerDown(Button_Dir_Down);
        iso_Editor_Curson_Move_UI.GetButton_Move_Down().Set_Button_Color_Active(iso_Editor_Curson_Move_UI.GetButton_Move_Down().GetButton_Color_Normal());

        iso_Editor_Curson_Move_UI.GetButton_Move_Left().Set_Button_Keycode(k_Move_Left);
        //iso_Editor_Curson_Move_UI.GetButton_Move_Left().Set_Event_Add_PointerDown(Button_Dir_Left);
        iso_Editor_Curson_Move_UI.GetButton_Move_Left().Set_Button_Color_Active(iso_Editor_Curson_Move_UI.GetButton_Move_Left().GetButton_Color_Normal());

        iso_Editor_Curson_Move_UI.GetButton_Move_Right().Set_Button_Keycode(k_Move_Right);
        //iso_Editor_Curson_Move_UI.GetButton_Move_Right().Set_Event_Add_PointerDown(Button_Dir_Right);
        iso_Editor_Curson_Move_UI.GetButton_Move_Right().Set_Button_Color_Active(iso_Editor_Curson_Move_UI.GetButton_Move_Right().GetButton_Color_Normal());

        iso_Editor_Curson_Move_UI.GetButton_Move_Top().Set_Button_Keycode(k_Move_Top);
        //iso_Editor_Curson_Move_UI.GetButton_Move_Top().Set_Event_Add_PointerDown(Button_Dir_Top);
        iso_Editor_Curson_Move_UI.GetButton_Move_Top().Set_Button_Color_Active(iso_Editor_Curson_Move_UI.GetButton_Move_Top().GetButton_Color_Normal());

        iso_Editor_Curson_Move_UI.GetButton_Move_Bot().Set_Button_Keycode(k_Move_Bot);
        //iso_Editor_Curson_Move_UI.GetButton_Move_Bot().Set_Event_Add_PointerDown(Button_Dir_Bot);
        iso_Editor_Curson_Move_UI.GetButton_Move_Bot().Set_Button_Color_Active(iso_Editor_Curson_Move_UI.GetButton_Move_Bot().GetButton_Color_Normal());
    }

    #region Curson 

    public void Button_Dir_Up()
    {
        Set_Dir(IsoClassDir.v3_Up_X);
    }

    public void Button_Dir_Down()
    {
        Set_Dir(IsoClassDir.v3_Down_X);
    }

    public void Button_Dir_Left()
    {
        Set_Dir(IsoClassDir.v3_Left_Y);
    }

    public void Button_Dir_Right()
    {
        Set_Dir(IsoClassDir.v3_Right_Y);
    }

    public void Button_Dir_Top()
    {
        Set_Dir(IsoClassDir.v3_Top_H);
    }

    public void Button_Dir_Bot()
    {
        Set_Dir(IsoClassDir.v3_Bot_H);
    }

    private void Set_Dir(Vector3Int v3_Move_Dir)
    {
        if (iso_World.GetWorld_inLimit(iso_Block.GetPosOnMatrix_Current(), v3_Move_Dir))
        {
            iso_Editor_World.Set_Reset_UI_PosAndSize(iso_Block.GetPosOnMatrix_Current() + v3_Move_Dir);
            iso_Editor_World.Set_Reset_UI_Block_List();

            iso_Editor_World_Matrix.Set_WorldView_nonActive();
        }
    }

    public void Button_Pos_Reset()
    {
        iso_Editor_World.Set_Reset_UI_PosAndSize(iso_Block.GetPosOnMatrix_Primary());
        iso_Editor_World.Set_Reset_UI_Block_List();
    }

    #endregion
}
