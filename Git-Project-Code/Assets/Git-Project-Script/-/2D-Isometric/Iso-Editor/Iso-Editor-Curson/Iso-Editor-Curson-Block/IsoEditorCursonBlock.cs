using UnityEngine;

public class IsoEditorCursonBlock : MonoBehaviour
{
    #region Editor

    [Header("Curson Block List Block")]

    [Tooltip("Block List Block")]
    [SerializeField]
    private UIVerticalList cl_Curson_Block_List;

    [Header("Button Curson Block on World Matrix")]

    [Tooltip("Curson Block Edit")]
    [SerializeField]
    private KeyCode k_Block_Edit = KeyCode.Home;

    [Tooltip("Curson Block Remove")]
    [SerializeField]
    private KeyCode k_Block_Remove = KeyCode.Delete;

    [Tooltip("Curson Block Block Next")]
    [SerializeField]
    private KeyCode k_Index_Next = KeyCode.F1;

    [Tooltip("Curson Block Block Back")]
    [SerializeField]
    private KeyCode k_Index_Back = KeyCode.F2;

    private int i_Index = 0;

    [Tooltip("Curson Block Page Next")]
    [SerializeField]
    private KeyCode k_Page_Next = KeyCode.F3;

    [Tooltip("Curson Block Page Back")]
    [SerializeField]
    private KeyCode k_Page_Back = KeyCode.F4;

    private int i_Page = 0;

    [Header("Button Check Curson on World Matrix")]

    [Tooltip("Curson Block Check")]
    [SerializeField]
    private KeyCode k_Block_Check = KeyCode.Tab;

    [Tooltip("Curson Primary Sprite")]
    [SerializeField]
    private Sprite s_Curson_Primary;

    [Tooltip("Curson Block Check")]
    private bool b_Check = false;

    [Header("UI Component")]

    [Tooltip("Editor Block UI")]
    [SerializeField]
    private IsoEditorUICursonBlock iso_Editor_Block_UI;

    #endregion

    private IsoEditorFile cl_Editor_File;

    private IsoEditorMove cl_Editor_Block_Move;

    private IsoEditorJoinTo cl_Editor_Block_JoinTo;

    private IsoEditorBlockSwitchTo cl_Editor_Block_SwichTo;

    private IsoEditorMessage cl_Editor_Block_Message;

    private IsoBlock cl_Block;

    private IsoWorld cl_World;

    private IsoWorldRenderer cl_World_Renderer;

    private void Awake()
    {
        cl_Block = GetComponent<IsoBlock>();

        if (cl_World == null)
        {
            cl_World = GameObject.FindGameObjectWithTag("IsoWorldManager").GetComponent<IsoWorld>();
        }

        cl_Editor_File = GetComponent<IsoEditorFile>();

        cl_World_Renderer = cl_World.GetComponent<IsoWorldRenderer>();

        cl_Editor_Block_Move = GetComponent<IsoEditorMove>();

        cl_Editor_Block_JoinTo = GetComponent<IsoEditorJoinTo>();

        cl_Editor_Block_SwichTo = GetComponent<IsoEditorBlockSwitchTo>();

        cl_Editor_Block_Message = GetComponent<IsoEditorMessage>();

        iso_Editor_Block_UI.Get_Button_Edit().Set_Button_Keycode(k_Block_Edit);
        //iso_Editor_Block_UI.Get_Button_Edit().Set_Event_Add_PointerDown(Button_Block_Edit);
        iso_Editor_Block_UI.Get_Button_Edit().Set_Button_Color_Active(iso_Editor_Block_UI.Get_Button_Edit().Get_Button_Color_Normal());

        iso_Editor_Block_UI.Get_Button_Remove().Set_Button_Keycode(k_Block_Remove);
        //iso_Editor_Block_UI.Get_Button_Remove().Set_Event_Add_PointerDown(Button_Block_Remove);
        iso_Editor_Block_UI.Get_Button_Remove().Set_Button_Color_Active(iso_Editor_Block_UI.Get_Button_Remove().Get_Button_Color_Normal());

        iso_Editor_Block_UI.Get_Button_Index_Next().Set_Button_Keycode(k_Index_Next);
        //iso_Editor_Block_UI.Get_Button_Index_Next().Set_Event_Add_PointerDown(Button_Index_Next);
        iso_Editor_Block_UI.Get_Button_Index_Next().Set_Button_Color_Active(iso_Editor_Block_UI.Get_Button_Index_Next().Get_Button_Color_Normal());

        iso_Editor_Block_UI.Get_Button_Index_Back().Set_Button_Keycode(k_Index_Back);
        //iso_Editor_Block_UI.Get_Button_Block_Back().Set_Event_Add_PointerDown(Button_Block_Back);
        iso_Editor_Block_UI.Get_Button_Index_Back().Set_Button_Color_Active(iso_Editor_Block_UI.Get_Button_Index_Back().Get_Button_Color_Normal());

        iso_Editor_Block_UI.Get_Button_Page_Next().Set_Button_Keycode(k_Page_Next);
        //iso_Editor_Block_UI.Get_Button_Page_Next().Set_Event_Add_PointerDown(Button_Page_Next);
        iso_Editor_Block_UI.Get_Button_Page_Next().Set_Button_Color_Active(iso_Editor_Block_UI.Get_Button_Page_Next().Get_Button_Color_Normal());

        iso_Editor_Block_UI.Get_Button_Page_Back().Set_Button_Keycode(k_Page_Back);
        //iso_Editor_Block_UI.Get_Button_Page_Back().Set_Event_Add_PointerDown(Button_Page_Back);
        iso_Editor_Block_UI.Get_Button_Page_Back().Set_Button_Color_Active(iso_Editor_Block_UI.Get_Button_Page_Back().Get_Button_Color_Normal());

        iso_Editor_Block_UI.Get_Button_Check().Set_Button_Keycode(k_Block_Check);
        //iso_Editor_Block_UI.Get_Button_Check().Set_Event_Add_PointerDown(Set_Check_Chance);
    }

    private void Start()
    {
        Set_Page();
    }

    #region Edit World

    public void Button_Block_Add()
    {
        cl_World.Set_Block_Primary_Add(
            cl_Block.Get_PosOnMatrix_Current(),
            cl_World_Renderer.Get_Combine_Name(Get_Page(), Get_Index()));

        if (!cl_World.Get_Current_isEmty(cl_Block.Get_PosOnMatrix_Current()))
        {
            if (cl_World.Get_Current_GameObject(cl_Block.Get_PosOnMatrix_Current()).GetComponent<IsoBlock>().Get_Block_Check())
            {
                if (cl_World.Get_Primary_GameObject(cl_Block.Get_PosOnMatrix_Current()).GetComponent<IsoBlockMove>() != null)
                {
                    cl_World.Get_Primary_GameObject(cl_Block.Get_PosOnMatrix_Current()).GetComponent<IsoBlockMove>().Set_Hop_isAllow(cl_World.Get_World_isHop());
                }
            }
        }

        cl_Editor_Block_Move.Set_ListVertical_Data_Current_Pos_Matrix();

        cl_Editor_Block_JoinTo.Set_ListVertical_Data_Current_Pos_Matrix();

        cl_Editor_Block_SwichTo.Set_ListVertical_Data_Current_Pos_Matrix();

        cl_Editor_Block_Message.Set_ListVertical_Data_Current_Pos_Matrix();

        cl_Editor_File.Button_Save_Temp();
    }

    public void Button_Block_Remove()
    {
        cl_World.Set_Primary_Remove(cl_Block.Get_PosOnMatrix_Current(), true);

        cl_Editor_Block_Move.Set_ListVertical_Data_Current_Pos_Matrix();

        cl_Editor_Block_JoinTo.Set_ListVertical_Data_Current_Pos_Matrix();

        cl_Editor_Block_SwichTo.Set_ListVertical_Data_Current_Pos_Matrix();

        cl_Editor_Block_Message.Set_ListVertical_Data_Current_Pos_Matrix();

        cl_Editor_File.Button_Save_Temp();
    }

    #endregion

    #region Edit Block 

    #region Edit Block Block 

    public void Button_Index_Next()
    {
        i_Index++;
        Set_Index();
    }

    public void Button_Index_Back()
    {
        i_Index--;
        Set_Index();
    }

    public void Button_Index(int i_Block)
    {
        i_Index = i_Block;
        Set_Index();
    }

    public int Get_Index()
    {
        return i_Index;
    }

    private void Set_Index()
    {
        if (i_Index >= cl_World_Renderer.Get_Combine_Count(i_Page))
        {
            i_Index = 0;
        }
        else
        if (i_Index < 0)
        {
            i_Index = cl_World_Renderer.Get_Combine_Count(i_Page) - 1;
        }

        iso_Editor_Block_UI.Set_Canvas_Block(
            cl_World_Renderer.Get_Combine_Imformation(i_Page).Get_Type(),
            i_Index,
            cl_World_Renderer.Get_Combine_Count(i_Page),
            cl_World_Renderer.Get_Combine(i_Page, i_Index).GetComponent<SpriteRenderer>());

        Set_Check();
    }

    #endregion

    #region Edit Block Layer 

    public void Button_Page_Next()
    {
        i_Page++;
        Set_Page();
    }

    public void Button_Page_Back()
    {
        i_Page--;
        Set_Page();
    }

    public int Get_Page()
    {
        return i_Page;
    }

    private void Set_Page()
    {
        if (i_Page > cl_World_Renderer.Get_Combine_Count() - 1)
        {
            i_Page = 0;
        }
        else
        if (i_Page < 0)
        {
            i_Page = cl_World_Renderer.Get_Combine_Count() - 1;
        }

        Set_Page_Curson_Block_List();

        i_Index = 0;

        iso_Editor_Block_UI.Set_Canvas_Block(
            cl_World_Renderer.Get_Combine_Imformation(i_Page).Get_Type(),
            i_Index,
            cl_World_Renderer.Get_Combine_Count(i_Page),
            cl_World_Renderer.Get_Combine(i_Page, i_Index).GetComponent<SpriteRenderer>());

        Set_Check();
    }

    private void Set_Page_Curson_Block_List()
    {
        cl_Curson_Block_List.Set_ListVertical_Remove_All();

        for (int i = 0; i < cl_World_Renderer.Get_Combine_Count(i_Page); i++)
        {
            cl_Curson_Block_List.Set_ListVertical_Add();

            cl_Curson_Block_List.Get_ListVertical_GameObject_Lastest().GetComponent<IsoEditorUICursonBlockClone>().Set_Image(
                cl_World_Renderer.Get_Combine(i_Page, i).GetComponent<SpriteRenderer>());
        }
    }

    #endregion

    #endregion

    #region Edit Check Curson

    public void Button_Check_Chance()
    {
        b_Check = !b_Check;

        Set_Check();
    }

    private void Set_Check()
    {
        if (b_Check)
        {
            GetComponent<SpriteRenderer>().sprite = iso_Editor_Block_UI.Get_Image_Block_Renderer().sprite;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = s_Curson_Primary;
        }
    }

    #endregion
}