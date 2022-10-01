using System.Collections.Generic;
using UnityEngine;

public class IsoEditorMove : MonoBehaviour
{
    [Header("UI Component")]

    [Header("UI List Vertical")]

    [Tooltip("Vertical List")]
    [SerializeField]
    private UIVerticalList cl_Vertical_List;

    [Tooltip("Editor Block Move UI")]
    [SerializeField]
    private GameObject g_MoveClone;

    [Header("UI Editor Block Move")]

    [Tooltip("Editor Block Move UI")]
    [SerializeField]
    private IsoEditorUIMove iso_Editor_Block_Move_UI;

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

        iso_Editor_Block_Move_UI.Set_Editor_Block_Move(this);

        ui_Object_DragDrop = iso_Editor_Block_Move_UI.gameObject.GetComponent<UIObjectDragDrop>();

        iso_Editor_Block_List = cl_Vertical_List.gameObject.GetComponent<Iso_Editor_UI_Block_List>();

        //ui_Object_DragDrop.Set_Event_Add_PointerDown(Set_ListVertical_Data_Current_Pos_Matrix_This);

        //UI Move Firstly
        iso_Editor_Block_List.Set_UI_Move(iso_Block.GetPosOnMatrix_Current());
    }

    #region Move List Vertical

    public void Set_ListVertical_Data_Current_Pos_Matrix_This()
    {
        iso_Editor_Block_List.Set_UI_Move(iso_Block.GetPosOnMatrix_Current());

        Set_ListVertical_Data_Current_Pos_Matrix();
    }

    /// <summary>
    /// Set Move List Vertical with Move Data Current on Current Pos Matrix
    /// </summary>
    public void Set_ListVertical_Data_Current_Pos_Matrix()
    {
        if (!iso_World.GetWorldIsGenerated())
        {
            return;
        }

        if (!iso_Editor_Block_List.GetUI_Move())
        {
            return;
        }

        iso_Editor_Block_List.Set_UI_Move(iso_Block.GetPosOnMatrix_Current());

        cl_Vertical_List.Set_ListVertical_Remove_All();

        if (iso_World.GetPrimaryIsEmty(iso_Block.GetPosOnMatrix_Current())) //Check Emty
        {
            iso_Editor_Block_Move_UI.Set_Status(false);

            return;
        }

        //if (!iso_World.GetPrimary_GameObject(iso_Block.GetPosOnMatrix_Current()).GetComponent<Isometric2D_Block>().GetBlock_Check()) //Check Block
        //{
        //    iso_Editor_Block_Move_UI.Set_Status(false);

        //    return;
        //}

        if (iso_World.GetPrimary_GameObject(iso_Block.GetPosOnMatrix_Current()).GetComponent<IsoBlockMove>() == null) //Check Componenet
        {
            iso_Editor_Block_Move_UI.Set_Status(false);

            return;
        }

        iso_Editor_Block_Move_UI.Set_Status(iso_World.GetPrimary_GameObject(iso_Block.GetPosOnMatrix_Current()).GetComponent<IsoBlockMove>().GetStatusIsActive());

        List<IsoDataMoveSingle> l_Move_Data_Single = iso_World.GetPrimary_GameObject(iso_Block.GetPosOnMatrix_Current()).GetComponent<IsoBlockMove>().GetList();

        cl_Vertical_List.Set_ListVertical_Remove_All();

        cl_Vertical_List.SetClone(g_MoveClone);

        for (int i = 0; i < l_Move_Data_Single.Count; i++)
        {
            cl_Vertical_List.Set_ListVertical_Add();

            cl_Vertical_List.GetListVertical_GameObject_Lastest().GetComponent<IsoEditorUIMoveClone>().SetClone(
                l_Move_Data_Single[i].GetDir(),
                l_Move_Data_Single[i].GetLength(),
                l_Move_Data_Single[i].GetSpeed(),
                l_Move_Data_Single[i].GetPosMoveTo());
        }
    }

    #endregion

    #region Move List

    public void Set_Add(Vector3Int v3_Move_Dir, int i_Move_Length, float f_Move_Speed)
    {
        iso_World.Set_MoveBlock_Primary_Active_Add(iso_Block.GetPosOnMatrix_Current(), v3_Move_Dir, i_Move_Length, f_Move_Speed);

        Set_ListVertical_Data_Current_Pos_Matrix();

        iso_Editor_File.Button_Save_Temp();
    }

    public void Set_Add_Rev()
    {
        iso_World.Set_MoveBlock_Primary_Active_Add_Rev(iso_Block.GetPosOnMatrix_Current());

        Set_ListVertical_Data_Current_Pos_Matrix();

        iso_Editor_File.Button_Save_Temp();
    }

    public void Set_Del_Lastest()
    {
        iso_World.Set_MoveBlock_Primary_Active_Remove_Lastest(iso_Block.GetPosOnMatrix_Current());

        Set_ListVertical_Data_Current_Pos_Matrix();

        iso_Editor_File.Button_Save_Temp();
    }

    #endregion

    #region Move Status

    public void Set_Status_Chance()
    {
        iso_World.Set_MoveBlock_Primary_Status_Chance(iso_Block.GetPosOnMatrix_Current());

        iso_World.GetPrimary_GameObject(iso_Block.GetPosOnMatrix_Current()).GetComponent<IsoBlockMove>().Set_Active_Reset();

        iso_Editor_File.Button_Save_Temp();
    }

    public bool GetStatus()
    {
        return iso_World.GetPrimary_GameObject(iso_Block.GetPosOnMatrix_Current()).GetComponent<IsoBlockMove>().GetStatusIsActive();
    }

    #endregion

    #region Move Clone

    public void Set_Copy(int i_Move_Index)
    {
        IsoDataMoveSingle cl_Move_Data_Single = iso_World.GetPrimary_GameObject(iso_Block.GetPosOnMatrix_Current()).GetComponent<IsoBlockMove>().GetList(i_Move_Index);

        iso_Editor_Block_Move_UI.Set_Dir(cl_Move_Data_Single.GetDir());
        iso_Editor_Block_Move_UI.Set_Length(cl_Move_Data_Single.GetLength());
        iso_Editor_Block_Move_UI.Set_Speed(cl_Move_Data_Single.GetSpeed());
    }

    public void Set_Fix(int i_Move_Index)
    {
        iso_World.Set_MoveBlock_Primary_Active_Chance(
            iso_Block.GetPosOnMatrix_Current(),
            i_Move_Index,
            iso_Editor_Block_Move_UI.GetDir(),
            iso_Editor_Block_Move_UI.GetLength(),
            iso_Editor_Block_Move_UI.GetSpeed());

        Set_ListVertical_Data_Current_Pos_Matrix();

        iso_Editor_File.Button_Save_Temp();
    }

    public void Set_Del(int i_Move_Index)
    {
        iso_World.Set_MoveBlock_Primary_Active_Remove(iso_Block.GetPosOnMatrix_Current(), i_Move_Index);

        Set_ListVertical_Data_Current_Pos_Matrix();

        iso_Editor_File.Button_Save_Temp();
    }

    #endregion
}
