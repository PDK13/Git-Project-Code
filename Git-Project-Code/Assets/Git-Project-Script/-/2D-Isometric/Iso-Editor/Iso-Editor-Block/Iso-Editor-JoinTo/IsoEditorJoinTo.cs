using System.Collections.Generic;
using UnityEngine;

public class IsoEditorJoinTo : MonoBehaviour
{
    [Header("UI Component")]

    [Header("UI List Vertical")]

    [Tooltip("Vertical List")]
    [SerializeField]
    private UIVerticalList cl_Vertical_List;

    [Tooltip("Editor Block Join-To UI")]
    [SerializeField]
    private GameObject g_JoinTo_Clone;

    [Header("UI Editor Block Join-To")]

    [Tooltip("Editor Block Join-To UI")]
    [SerializeField]
    private IsoEditorUIJoinTo iso_Editor_Block_JoinTo_UI;

    [Tooltip("Drag-Drop UI")]
    private UIObjectDragDrop ui_Object_DragDrop;

    [Tooltip("Editor Block List UI")]
    private Iso_Editor_UI_Block_List iso_Editor_Block_List;

    private IsoBlock iso_Block;

    private IsoWorld iso_World;

    private IsoEditorFile iso_Editor_File;

    private void Awake()
    {
        iso_Block = GetComponent<IsoBlock>();

        iso_World = GameObject.FindGameObjectWithTag("IsoWorldManager").GetComponent<IsoWorld>();

        iso_Editor_File = GetComponent<IsoEditorFile>();

        ui_Object_DragDrop = iso_Editor_Block_JoinTo_UI.gameObject.GetComponent<UIObjectDragDrop>();

        iso_Editor_Block_List = cl_Vertical_List.gameObject.GetComponent<Iso_Editor_UI_Block_List>();

        //iso_Editor_Block_JoinTo_UI.Get_Button_Add().Set_Event_Add_PointerDown(Button_Add);
        iso_Editor_Block_JoinTo_UI.Get_Button_Add().Set_Button_Color_Active(iso_Editor_Block_JoinTo_UI.Get_Button_Add().Get_Color_Normal_Primary());

        //iso_Editor_Block_JoinTo_UI.Get_Button_Del().Set_Event_Add_PointerDown(Button_Del);
        iso_Editor_Block_JoinTo_UI.Get_Button_Del().Set_Button_Color_Active(iso_Editor_Block_JoinTo_UI.Get_Button_Del().Get_Color_Normal_Primary());

        //ui_Object_DragDrop.Set_Event_Add_PointerDown(Set_ListVertical_Data_Current_Pos_Matrix_This);
    }

    #region Join-To Pos List Vertical

    public void Set_ListVertical_Data_Current_Pos_Matrix_This()
    {
        iso_Editor_Block_List.Set_UI_JoinTo(iso_Block.Get_PosOnMatrix_Current());

        Set_ListVertical_Data_Current_Pos_Matrix();
    }

    /// <summary>
    /// Set Join-To List Vertical with Join-To Data Current on Current Pos Matrix
    /// </summary>
    public void Set_ListVertical_Data_Current_Pos_Matrix()
    {
        if (!iso_World.Get_World_isGenerated())
        {
            return;
        }

        if (!iso_Editor_Block_List.Get_UI_JoinTo())
        {
            return;
        }

        cl_Vertical_List.Set_ListVertical_Remove_All();



        if (iso_World.Get_Primary_isEmty(iso_Block.Get_PosOnMatrix_Current())) //Check Emty
        {
            iso_Editor_Block_JoinTo_UI.Set_Text();

            return;
        }

        //if (!iso_World.Get_Primary_GameObject(iso_Block.Get_PosOnMatrix_Current()).GetComponent<Isometric2D_Block>().Get_Block_Check()) //Check Block
        //{
        //    iso_Editor_Block_JoinTo_UI.Set_Text();

        //    return;
        //}

        if (iso_World.Get_Primary_GameObject(iso_Block.Get_PosOnMatrix_Current()).GetComponent<IsoBlockJoinTo>() == null) //Check Component
        {
            iso_Editor_Block_JoinTo_UI.Set_Text();

            return;
        }

        if (!iso_World.Get_Primary_GameObject(iso_Block.Get_PosOnMatrix_Current()).GetComponent<IsoBlockJoinTo>().Get_JoinTo_isExist()) //Check Join-To Exist
        {
            iso_Editor_Block_JoinTo_UI.Set_Text();

            return;
        }

        Vector3Int v3_JoinTo_Pos = iso_World.Get_Primary_GameObject(iso_Block.Get_PosOnMatrix_Current()).GetComponent<IsoBlockJoinTo>().Get_JoinTo_Pos_Primary();

        iso_Editor_Block_JoinTo_UI.Set_Text(v3_JoinTo_Pos);

        iso_Editor_Block_List.Set_UI_JoinTo(iso_Block.Get_PosOnMatrix_Current());

        Vector3Int v3_JoinTo_Pos_Distance = iso_World.Get_Primary_GameObject(iso_Block.Get_PosOnMatrix_Current()).GetComponent<IsoBlockJoinTo>().Get_JoinTo_Pos_Distance();

        List<IsoDataMoveSingle> l_Move_Data_Single = iso_World.Get_Primary_GameObject(v3_JoinTo_Pos).GetComponent<IsoBlockMove>().Get_List();

        cl_Vertical_List.Set_ListVertical_Remove_All();

        cl_Vertical_List.Set_Clone(g_JoinTo_Clone);

        for (int i = 0; i < l_Move_Data_Single.Count; i++)
        {
            cl_Vertical_List.Set_ListVertical_Add();

            cl_Vertical_List.Get_ListVertical_GameObject_Lastest().GetComponent<IsoEditorUIJoinToClone>().Set_Clone(
                l_Move_Data_Single[i].Get_Dir(),
                l_Move_Data_Single[i].Get_Length(),
                l_Move_Data_Single[i].Get_Speed(),
                l_Move_Data_Single[i].Get_PosMoveTo() + v3_JoinTo_Pos_Distance);
        }
    }

    #endregion

    #region Join-To Pos

    public void Button_Add()
    {
        iso_World.Set_JoinToBlock_Primary_Active_Add(iso_Block.Get_PosOnMatrix_Current(), iso_Editor_Block_JoinTo_UI.Get_Pos());

        Set_ListVertical_Data_Current_Pos_Matrix();

        iso_Editor_File.Button_Save_Temp();
    }

    public void Button_Del()
    {
        iso_World.Set_JoinToBlock_Primary_Active_Remove(iso_Block.Get_PosOnMatrix_Current());

        Set_ListVertical_Data_Current_Pos_Matrix();

        iso_Editor_File.Button_Save_Temp();
    }

    #endregion
}
