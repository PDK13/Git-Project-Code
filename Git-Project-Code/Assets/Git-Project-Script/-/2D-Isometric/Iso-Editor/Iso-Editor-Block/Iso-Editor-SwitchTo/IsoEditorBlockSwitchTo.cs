using System.Collections.Generic;
using UnityEngine;

public class IsoEditorBlockSwitchTo : MonoBehaviour
{
    [Header("UI Component")]

    [Header("UI List Vertical")]

    [Tooltip("Vertical List")]
    [SerializeField]
    private UIVerticalList cl_Vertical_List;

    [Tooltip("Editor Block Switch-To UI")]
    [SerializeField]
    private GameObject g_SwitchToClone;

    [Header("UI Editor Block SwitchTo")]

    [Tooltip("Editor Block Switch-To UI")]
    [SerializeField]
    private IsoEditorUISwitchTo iso_Editor_Block_SwitchTo_UI;

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

        ui_Object_DragDrop = iso_Editor_Block_SwitchTo_UI.gameObject.GetComponent<UIObjectDragDrop>();

        iso_Editor_Block_List = cl_Vertical_List.gameObject.GetComponent<Iso_Editor_UI_Block_List>();

        //iso_Editor_Block_SwitchTo_UI.GetButton_Add().Set_Event_Add_PointerDown(Button_Add);
        iso_Editor_Block_SwitchTo_UI.GetButton_Add().Set_Button_Color_Active(iso_Editor_Block_SwitchTo_UI.GetButton_Add().GetColor_Normal_Primary());

        //iso_Editor_Block_SwitchTo_UI.GetButton_Del_Lastest().Set_Event_Add_PointerDown(Button_Del_Lastest);
        iso_Editor_Block_SwitchTo_UI.GetButton_Del_Lastest().Set_Button_Color_Active(iso_Editor_Block_SwitchTo_UI.GetButton_Del_Lastest().GetColor_Normal_Primary());

        //ui_Object_DragDrop.Set_Event_Add_PointerDown(Set_ListVertical_Data_Current_Pos_Matrix_This);
    }

    #region Switch-To List Vertical

    public void Set_ListVertical_Data_Current_Pos_Matrix_This()
    {
        iso_Editor_Block_List.Set_UI_SwitchTo(iso_Block.GetPosOnMatrix_Current());

        Set_ListVertical_Data_Current_Pos_Matrix();
    }

    /// <summary>
    /// Set Switch-To List Vertical with Switch-To Data Current on Current Pos Matrix
    /// </summary>
    public void Set_ListVertical_Data_Current_Pos_Matrix()
    {
        if (!iso_World.GetWorldIsGenerated())
        {
            return;
        }

        if (!iso_Editor_Block_List.GetUI_SwitchTo())
        {
            return;
        }

        iso_Editor_Block_List.Set_UI_SwitchTo(iso_Block.GetPosOnMatrix_Current());

        cl_Vertical_List.Set_ListVertical_Remove_All();

        if (iso_World.GetPrimaryIsEmty(iso_Block.GetPosOnMatrix_Current())) //Check Emty
        {
            return;
        }

        //if (!iso_World.GetPrimary_GameObject(iso_Block.GetPosOnMatrix_Current()).GetComponent<Isometric2D_Block>().GetBlock_Check()) //Check Block
        //{
        //    return;
        //}

        if (iso_World.GetPrimary_GameObject(iso_Block.GetPosOnMatrix_Current()).GetComponent<IsoBlockSwitchTo>() == null) //Check Component
        {
            return;
        }

        List<Vector3Int> l_SwitchTo_Data_Single = iso_World.GetPrimary_GameObject(iso_Block.GetPosOnMatrix_Current()).GetComponent<IsoBlockSwitchTo>().GetList();

        cl_Vertical_List.Set_ListVertical_Remove_All();

        cl_Vertical_List.SetClone(g_SwitchToClone);

        for (int i = 0; i < l_SwitchTo_Data_Single.Count; i++)
        {
            cl_Vertical_List.Set_ListVertical_Add();

            cl_Vertical_List.GetListVertical_GameObject_Lastest().GetComponent<IsoEditorUISwitchToClone>().Set_SwitchToBlockClone(
                l_SwitchTo_Data_Single[i]);
        }
    }

    #endregion

    #region Switch-To List

    public void Button_Add()
    {
        iso_World.Set_SwitchToBlock_Primary_Active_Add(iso_Block.GetPosOnMatrix_Current(), iso_Editor_Block_SwitchTo_UI.GetPos());

        Set_ListVertical_Data_Current_Pos_Matrix();

        iso_Editor_File.Button_Save_Temp();
    }

    public void Button_Del_Lastest()
    {
        iso_World.Set_SwitchToBlock_Primary_Active_Remove_Lastest(iso_Block.GetPosOnMatrix_Current());

        Set_ListVertical_Data_Current_Pos_Matrix();

        iso_Editor_File.Button_Save_Temp();
    }

    #endregion

    #region Switch-To Clone

    public void Set_Copy(int i_SwitchTo_Index)
    {
        Vector3Int cl_SwitchTo_Data_Single = iso_World.GetPrimary_GameObject(iso_Block.GetPosOnMatrix_Current()).GetComponent<IsoBlockSwitchTo>().GetList(i_SwitchTo_Index);

        iso_Editor_Block_SwitchTo_UI.SetClone(cl_SwitchTo_Data_Single);
    }

    public void Set_Fix(int i_SwitchTo_Index)
    {
        iso_World.Set_SwitchToBlock_Primary_Active_Chance(
            iso_Block.GetPosOnMatrix_Current(),
            i_SwitchTo_Index,
            iso_Editor_Block_SwitchTo_UI.GetPos());

        Set_ListVertical_Data_Current_Pos_Matrix();

        iso_Editor_File.Button_Save_Temp();
    }

    public void Set_Del(int i_SwitchTo_Index)
    {
        iso_World.Set_SwitchToBlock_Primary_Active_Remove(iso_Block.GetPosOnMatrix_Current(), i_SwitchTo_Index);

        Set_ListVertical_Data_Current_Pos_Matrix();

        iso_Editor_File.Button_Save_Temp();
    }

    #endregion
}
