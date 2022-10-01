using System.Collections.Generic;
using UnityEngine;

public class IsoEditorMessage : MonoBehaviour
{
    [Header("UI Component")]

    [Header("UI List Vertical")]

    [Tooltip("Vertical List")]
    [SerializeField]
    private UIVerticalList cl_Vertical_List;

    [Tooltip("Editor Block Message UI")]
    [SerializeField]
    private GameObject g_MessageClone;

    [Header("UI Editor Block Message")]

    [Tooltip("Editor Block Message UI")]
    [SerializeField]
    private IsoEditorUIMessage iso_Editor_Block_Message_UI;

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

        ui_Object_DragDrop = iso_Editor_Block_Message_UI.gameObject.GetComponent<UIObjectDragDrop>();

        iso_Editor_Block_List = cl_Vertical_List.gameObject.GetComponent<Iso_Editor_UI_Block_List>();

        //iso_Editor_Block_Message_UI.GetButton_Add().Set_Event_Add_PointerDown(Button_Add);
        iso_Editor_Block_Message_UI.GetButton_Add().Set_Button_Color_Active(iso_Editor_Block_Message_UI.GetButton_Add().GetColor_Normal_Primary());

        //iso_Editor_Block_Message_UI.GetButton_Del_Lastest().Set_Event_Add_PointerDown(Button_Del_Lastest);
        iso_Editor_Block_Message_UI.GetButton_Del_Lastest().Set_Button_Color_Active(iso_Editor_Block_Message_UI.GetButton_Del_Lastest().GetColor_Normal_Primary());

        //ui_Object_DragDrop.Set_Event_Add_PointerDown(Set_ListVertical_Data_Current_Pos_Matrix_This);
    }

    #region Message List Vertical

    public void Set_ListVertical_Data_Current_Pos_Matrix_This()
    {
        iso_Editor_Block_List.Set_UI_Message(iso_Block.GetPosOnMatrix_Current());

        Set_ListVertical_Data_Current_Pos_Matrix();
    }

    /// <summary>
    /// Set Message List Vertical with Message Data Current on Current Pos Matrix
    /// </summary>
    public void Set_ListVertical_Data_Current_Pos_Matrix()
    {
        if (!iso_World.GetWorldIsGenerated())
        {
            return;
        }

        if (!iso_Editor_Block_List.GetUI_Message())
        {
            return;
        }

        iso_Editor_Block_List.Set_UI_Message(iso_Block.GetPosOnMatrix_Current());

        cl_Vertical_List.Set_ListVertical_Remove_All();

        if (iso_World.GetPrimaryIsEmty(iso_Block.GetPosOnMatrix_Current())) //Check Emty
        {
            return;
        }

        //if (!iso_World.GetPrimary_GameObject(iso_Block.GetPosOnMatrix_Current()).GetComponent<Isometric2D_Block>().GetBlock_Check()) //Check Block
        //{
        //    return;
        //}

        if (iso_World.GetPrimary_GameObject(iso_Block.GetPosOnMatrix_Current()).GetComponent<IsoBlockMessage>() == null) //Check Componenet
        {
            return;
        }

        List<IsoDataMessageSingle> l_Message_Data_Single = iso_World.GetPrimary_GameObject(iso_Block.GetPosOnMatrix_Current()).GetComponent<IsoBlockMessage>().GetList();

        cl_Vertical_List.Set_ListVertical_Remove_All();

        cl_Vertical_List.SetClone(g_MessageClone);

        for (int i = 0; i < l_Message_Data_Single.Count; i++)
        {
            cl_Vertical_List.Set_ListVertical_Add();

            cl_Vertical_List.GetListVertical_GameObject_Lastest().GetComponent<IsoEditorUIMessageClone>().SetClone(
                l_Message_Data_Single[i].GetName(),
                l_Message_Data_Single[i].GetMessage());
        }
    }

    #endregion

    #region Message List

    public void Button_Add()
    {
        iso_World.Set_MessageBlock_Primary_Active_Add(iso_Block.GetPosOnMatrix_Current(), iso_Editor_Block_Message_UI.GetData());

        Set_ListVertical_Data_Current_Pos_Matrix();

        iso_Editor_File.Button_Save_Temp();
    }

    public void Button_Del_Lastest()
    {
        iso_World.Set_MessageBlock_Primary_Active_Remove_Lastest(iso_Block.GetPosOnMatrix_Current());

        Set_ListVertical_Data_Current_Pos_Matrix();

        iso_Editor_File.Button_Save_Temp();
    }

    #endregion

    #region Message Clone

    public void Set_Copy(int i_Message_Index)
    {
        IsoDataMessageSingle cl_Message_Data_Single = iso_World.GetPrimary_GameObject(iso_Block.GetPosOnMatrix_Current()).GetComponent<IsoBlockMessage>().GetList(i_Message_Index);

        iso_Editor_Block_Message_UI.SetClone(cl_Message_Data_Single);
    }

    public void Set_Fix(int i_Message_Index)
    {
        iso_World.Set_MessageBlock_Primary_Active_Chance(
            iso_Block.GetPosOnMatrix_Current(),
            i_Message_Index,
            iso_Editor_Block_Message_UI.GetData());

        Set_ListVertical_Data_Current_Pos_Matrix();

        iso_Editor_File.Button_Save_Temp();
    }

    public void Set_Del(int i_Message_Index)
    {
        iso_World.Set_MessageBlock_Primary_Active_Remove(iso_Block.GetPosOnMatrix_Current(), i_Message_Index);

        Set_ListVertical_Data_Current_Pos_Matrix();

        iso_Editor_File.Button_Save_Temp();
    }

    #endregion
}
