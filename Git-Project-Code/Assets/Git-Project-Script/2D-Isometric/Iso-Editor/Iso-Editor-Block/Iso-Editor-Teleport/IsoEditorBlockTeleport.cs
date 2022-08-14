using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsoEditorBlockTeleport : MonoBehaviour
{
    [Header("UI Component")]

    [Tooltip("Editor Block Join-To UI")]
    [SerializeField]
    private IsoEditorUITeleport iso_Editor_Block_Teleport_UI;

    private IsoEditorFile iso_Editor_File;

    private IsoBlock iso_Block;

    private IsoWorld iso_World;

    private void Awake()
    {
        iso_Block = GetComponent<IsoBlock>();

        iso_World = GameObject.FindGameObjectWithTag("IsoWorldManager").GetComponent<IsoWorld>();

        iso_Editor_File = GetComponent<IsoEditorFile>();

        //iso_Editor_Block_Teleport_UI.Get_Button_Add().Set_Event_Add_PointerDown(Button_Add);
        iso_Editor_Block_Teleport_UI.Get_Button_Add().Set_Button_Color_Active(iso_Editor_Block_Teleport_UI.Get_Button_Add().Get_Color_Normal_Primary());

        //iso_Editor_Block_Teleport_UI.Get_Button_Del().Set_Event_Add_PointerDown(Button_Del);
        iso_Editor_Block_Teleport_UI.Get_Button_Del().Set_Button_Color_Active(iso_Editor_Block_Teleport_UI.Get_Button_Del().Get_Color_Normal_Primary());
    }

    public void Set_ListVertical_Data_Current_Pos_Matrix()
    {
        if (!iso_World.Get_World_isGenerated())
        {
            return;
        }

        if (iso_World.Get_Primary_isEmty(iso_Block.Get_PosOnMatrix_Current())) //Check Emty
        {
            iso_Editor_Block_Teleport_UI.Set_Teleport("", new Vector3Int());

            return;
        }

        //if (!iso_World.Get_Primary_GameObject(iso_Block.Get_PosOnMatrix_Current()).GetComponent<Isometric2D_Block>().Get_Block_Check()) //Check Block
        //{
        //    iso_Editor_Block_Teleport_UI.Set_Teleport("", new Vector3Int());

        //    return;
        //}

        if (iso_World.Get_Primary_GameObject(iso_Block.Get_PosOnMatrix_Current()).GetComponent<IsoBlockTeleport>() == null) //Check Component
        {
            iso_Editor_Block_Teleport_UI.Set_Teleport("", new Vector3Int());

            return;
        }

        iso_Editor_Block_Teleport_UI.Set_Teleport(
            iso_World.Get_Primary_GameObject(iso_Block.Get_PosOnMatrix_Current()).GetComponent<IsoBlockTeleport>().Get_World_Name(),
            iso_World.Get_Primary_GameObject(iso_Block.Get_PosOnMatrix_Current()).GetComponent<IsoBlockTeleport>().Get_Spawm_Pos());
    }

    #region Join-To Pos

    public void Button_Add()
    {
        iso_World.Set_TeleportBlock_Primary_Active_Add(iso_Block.Get_PosOnMatrix_Current(), iso_Editor_Block_Teleport_UI.Get_Data());

        iso_Editor_Block_Teleport_UI.Set_Teleport(iso_Editor_Block_Teleport_UI.Get_Data().Get_World_Name(), iso_Editor_Block_Teleport_UI.Get_Data().Get_Pos());

        iso_Editor_File.Button_Save_Temp();
    }

    public void Button_Del()
    {
        iso_World.Set_TeleportBlock_Primary_Active_Remove(iso_Block.Get_PosOnMatrix_Current());

        iso_Editor_File.Button_Save_Temp();
    }

    #endregion
}
