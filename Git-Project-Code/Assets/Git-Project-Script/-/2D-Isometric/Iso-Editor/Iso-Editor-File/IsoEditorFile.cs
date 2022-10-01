using UnityEngine;

public class IsoEditorFile : MonoBehaviour
{
    [Header("UI Component")]

    [Tooltip("Editor File UI")]
    [SerializeField]
    private IsoEditorUIFile iso_Editor_File_UI;

    private IsoEditorWorld iso_Editor_World;

    private IsoWorld iso_World;

    private IsoBlock iso_Block;

    private void Awake()
    {
        iso_World = GameObject.FindGameObjectWithTag("IsoWorldManager").GetComponent<IsoWorld>();

        iso_Block = GetComponent<IsoBlock>();

        iso_Editor_World = GetComponent<IsoEditorWorld>();

        //iso_Editor_File_UI.GetButton_New().Set_Event_Add_PointerDown(Button_New);
        iso_Editor_File_UI.GetButton_New().Set_Button_Color_Active(iso_Editor_File_UI.GetButton_New().GetColor_Normal_Primary());

        //iso_Editor_File_UI.GetButton_Save().Set_Event_Add_PointerDown(Button_Save);
        iso_Editor_File_UI.GetButton_Save().Set_Button_Color_Active(iso_Editor_File_UI.GetButton_Save().GetColor_Normal_Primary());

        //iso_Editor_File_UI.GetButton_Open().Set_Event_Add_PointerDown(Button_Open);
        iso_Editor_File_UI.GetButton_Open().Set_Button_Color_Active(iso_Editor_File_UI.GetButton_Open().GetColor_Normal_Primary());

        //iso_Editor_File_UI.GetButton_Open_Temp().Set_Event_Add_PointerDown(Button_Open_Temp);
        iso_Editor_File_UI.GetButton_Open_Temp().Set_Button_Color_Active(iso_Editor_File_UI.GetButton_Open_Temp().GetColor_Normal_Primary());
    }

    #region Switch-To List

    public void Button_New()
    {
        iso_World.Set_Memory_Origin(
            iso_Editor_File_UI.GetWorld_Name(),
            iso_Editor_File_UI.GetWorld_Size());

        iso_World.Set_World_Generate_fromMemory();

        iso_Editor_File_UI.Set_World(
            iso_Editor_File_UI.GetWorld_Name(),
            iso_Editor_File_UI.GetWorld_Size());

        iso_Editor_World.Set_Reset_UI_PosAndSize(iso_Block.GetPosOnMatrix_Primary());
        iso_Editor_World.Set_Reset_UI_Block_List();
    }

    public void Button_Save()
    {
        //iso_World.Set_Memory_Chance_fromCurrent();

        iso_World.Set_Memory_File_Resources(
            iso_World.GetMemory_File_FolderName_Resources(),
            iso_World.GetMemory_withGenerated(iso_Editor_File_UI.GetWorld_Name()),
            false
            );

        iso_Editor_File_UI.Set_World(
            iso_Editor_File_UI.GetWorld_Name(),
            iso_World.GetMemory_withGenerated().GetWorld_Size());
    }

    public void Button_Save_Temp()
    {
        //iso_World.Set_Memory_Chance_fromCurrent();

        iso_World.Set_Memory_File_Resources(
            iso_World.GetMemory_File_FolderName_Resources(),
            iso_World.GetMemory_withGenerated(iso_Editor_File_UI.GetWorld_Name()),
            true
            );

        iso_Editor_File_UI.Set_World(
            iso_Editor_File_UI.GetWorld_Name(),
            iso_World.GetMemory_withGenerated().GetWorld_Size());
    }

    public void Button_Open()
    {
        iso_World.Set_Memory(
            iso_World.GetMemory_File_Resources(
                iso_World.GetMemory_File_FolderName_Resources(),
                iso_Editor_File_UI.GetWorld_Name(),
                false)
            );

        iso_World.Set_World_Generate_fromMemory();

        iso_Editor_File_UI.Set_World(
            iso_World.GetMemory_withGenerated().GetWorld_Name(),
            iso_World.GetMemory_withGenerated().GetWorld_Size());

        iso_Editor_World.Set_Reset_UI_PosAndSize(iso_Block.GetPosOnMatrix_Primary());
        iso_Editor_World.Set_Reset_UI_Block_List();
    }

    public void Button_Open_Temp()
    {
        iso_World.Set_Memory(
            iso_World.GetMemory_File_Resources(
                iso_World.GetMemory_File_FolderName_Resources(),
                iso_Editor_File_UI.GetWorld_Name(),
                true)
            );

        iso_World.Set_World_Generate_fromMemory();

        iso_Editor_File_UI.Set_World(
            iso_World.GetMemory_withGenerated().GetWorld_Name(),
            iso_World.GetMemory_withGenerated().GetWorld_Size());

        iso_Editor_World.Set_Reset_UI_PosAndSize(iso_Block.GetPosOnMatrix_Primary());
        iso_Editor_World.Set_Reset_UI_Block_List();
    }

    #endregion
}