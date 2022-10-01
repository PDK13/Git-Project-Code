using UnityEngine;

[RequireComponent(typeof(IsoEditorCursonBlock))]
[RequireComponent(typeof(IsoEditorCursonMove))]
[RequireComponent(typeof(IsoEditorWorldMatrix))]
[RequireComponent(typeof(IsoEditorMove))]
[RequireComponent(typeof(IsoEditorJoinTo))]
[RequireComponent(typeof(IsoEditorBlockSwitchTo))]
[RequireComponent(typeof(IsoEditorMessage))]
[RequireComponent(typeof(IsoEditorBlockTeleport))]
[RequireComponent(typeof(IsoEditorFile))]
[RequireComponent(typeof(IsoEditorWorldActive))]
[RequireComponent(typeof(IsoEditorCamera))]
public class IsoEditorWorld : MonoBehaviour
{
    [Header("UI Component")]

    [Tooltip("Editor Pos and Size UI")]
    [SerializeField]
    private IsoEditorUIPosAndSize iso_Editor_PosAndSize_UI;

    private IsoWorld iso_World;

    private IsoBlock iso_Block;

    private IsoEditorMove cl_Editor_Block_Move;

    private IsoEditorJoinTo cl_Editor_Block_JoinTo;

    private IsoEditorBlockSwitchTo cl_Editor_Block_SwichTo;

    private IsoEditorMessage cl_Editor_Block_Message;

    private IsoEditorBlockTeleport cl_Editor_Block_Teleport;

    private void Awake()
    {
        iso_World = GameObject.FindGameObjectWithTag("IsoWorldManager").GetComponent<IsoWorld>();

        iso_Block = GetComponent<IsoBlock>();

        cl_Editor_Block_Move = GetComponent<IsoEditorMove>();
        cl_Editor_Block_JoinTo = GetComponent<IsoEditorJoinTo>();
        cl_Editor_Block_SwichTo = GetComponent<IsoEditorBlockSwitchTo>();
        cl_Editor_Block_Message = GetComponent<IsoEditorMessage>();
        cl_Editor_Block_Teleport = GetComponent<IsoEditorBlockTeleport>();
    }

    public void Set_Reset_UI_Block_List()
    {
        cl_Editor_Block_Move.Set_ListVertical_Data_Current_Pos_Matrix();
        cl_Editor_Block_JoinTo.Set_ListVertical_Data_Current_Pos_Matrix();
        cl_Editor_Block_SwichTo.Set_ListVertical_Data_Current_Pos_Matrix();
        cl_Editor_Block_Message.Set_ListVertical_Data_Current_Pos_Matrix();
        cl_Editor_Block_Teleport.Set_ListVertical_Data_Current_Pos_Matrix();
    }

    public void Set_Reset_UI_PosAndSize()
    {
        iso_Editor_PosAndSize_UI.Set_UI_WorldSize(iso_World.GetWorld_Size_Current());
        iso_Editor_PosAndSize_UI.Set_UI_Pos(iso_Block.GetPosOnMatrix_Current());
    }

    public void Set_Reset_UI_PosAndSize(Vector3Int v3_Pos_MoveTo)
    {
        iso_Editor_PosAndSize_UI.Set_UI_WorldSize(iso_World.GetWorld_Size_Current());
        iso_Editor_PosAndSize_UI.Set_UI_Pos(v3_Pos_MoveTo);

        iso_Block.Set_Pos(v3_Pos_MoveTo);
    }

    public void Set_Reset_Block_Move()
    {
        for (int i = 0; i < iso_World.GetMoveBlock_Primary_ListPos().Count; i++)
        {
            iso_World.GetPrimary_GameObject(iso_World.GetMoveBlock_Primary_ListPos()[i]).GetComponent<IsoBlockMove>().Set_Active_Reset();
        }
    }
}
