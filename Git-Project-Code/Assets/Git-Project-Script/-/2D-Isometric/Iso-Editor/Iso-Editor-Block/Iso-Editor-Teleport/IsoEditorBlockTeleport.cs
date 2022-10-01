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

        //iso_Editor_Block_Teleport_UI.GetButton_Add().Set_Event_Add_PointerDown(Button_Add);
        iso_Editor_Block_Teleport_UI.GetButton_Add().Set_Button_Color_Active(iso_Editor_Block_Teleport_UI.GetButton_Add().GetColor_Normal_Primary());

        //iso_Editor_Block_Teleport_UI.GetButton_Del().Set_Event_Add_PointerDown(Button_Del);
        iso_Editor_Block_Teleport_UI.GetButton_Del().Set_Button_Color_Active(iso_Editor_Block_Teleport_UI.GetButton_Del().GetColor_Normal_Primary());
    }

    public void Set_ListVertical_Data_Current_Pos_Matrix()
    {
        if (!iso_World.GetWorldIsGenerated())
        {
            return;
        }

        if (iso_World.GetPrimaryIsEmty(iso_Block.GetPosOnMatrix_Current())) //Check Emty
        {
            iso_Editor_Block_Teleport_UI.Set_Teleport("", new Vector3Int());

            return;
        }

        //if (!iso_World.GetPrimary_GameObject(iso_Block.GetPosOnMatrix_Current()).GetComponent<Isometric2D_Block>().GetBlock_Check()) //Check Block
        //{
        //    iso_Editor_Block_Teleport_UI.Set_Teleport("", new Vector3Int());

        //    return;
        //}

        if (iso_World.GetPrimary_GameObject(iso_Block.GetPosOnMatrix_Current()).GetComponent<IsoBlockTeleport>() == null) //Check Component
        {
            iso_Editor_Block_Teleport_UI.Set_Teleport("", new Vector3Int());

            return;
        }

        iso_Editor_Block_Teleport_UI.Set_Teleport(
            iso_World.GetPrimary_GameObject(iso_Block.GetPosOnMatrix_Current()).GetComponent<IsoBlockTeleport>().GetWorld_Name(),
            iso_World.GetPrimary_GameObject(iso_Block.GetPosOnMatrix_Current()).GetComponent<IsoBlockTeleport>().GetSpawm_Pos());
    }

    #region Join-To Pos

    public void Button_Add()
    {
        iso_World.Set_TeleportBlock_Primary_Active_Add(iso_Block.GetPosOnMatrix_Current(), iso_Editor_Block_Teleport_UI.GetData());

        iso_Editor_Block_Teleport_UI.Set_Teleport(iso_Editor_Block_Teleport_UI.GetData().GetWorld_Name(), iso_Editor_Block_Teleport_UI.GetData().GetPos());

        iso_Editor_File.Button_Save_Temp();
    }

    public void Button_Del()
    {
        iso_World.Set_TeleportBlock_Primary_Active_Remove(iso_Block.GetPosOnMatrix_Current());

        iso_Editor_File.Button_Save_Temp();
    }

    #endregion
}
