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

        //iso_Editor_File_UI.Get_Button_New().Set_Event_Add_PointerDown(Button_New);
        iso_Editor_File_UI.Get_Button_New().Set_Button_Color_Active(iso_Editor_File_UI.Get_Button_New().Get_Color_Normal_Primary());

        //iso_Editor_File_UI.Get_Button_Save().Set_Event_Add_PointerDown(Button_Save);
        iso_Editor_File_UI.Get_Button_Save().Set_Button_Color_Active(iso_Editor_File_UI.Get_Button_Save().Get_Color_Normal_Primary());

        //iso_Editor_File_UI.Get_Button_Open().Set_Event_Add_PointerDown(Button_Open);
        iso_Editor_File_UI.Get_Button_Open().Set_Button_Color_Active(iso_Editor_File_UI.Get_Button_Open().Get_Color_Normal_Primary());

        //iso_Editor_File_UI.Get_Button_Open_Temp().Set_Event_Add_PointerDown(Button_Open_Temp);
        iso_Editor_File_UI.Get_Button_Open_Temp().Set_Button_Color_Active(iso_Editor_File_UI.Get_Button_Open_Temp().Get_Color_Normal_Primary());
    }

    #region Switch-To List

    public void Button_New()
    {
        iso_World.Set_Memory_Origin(
            iso_Editor_File_UI.Get_World_Name(),
            iso_Editor_File_UI.Get_World_Size());

        iso_World.Set_World_Generate_fromMemory();

        iso_Editor_File_UI.Set_World(
            iso_Editor_File_UI.Get_World_Name(),
            iso_Editor_File_UI.Get_World_Size());

        iso_Editor_World.Set_Reset_UI_PosAndSize(iso_Block.Get_PosOnMatrix_Primary());
        iso_Editor_World.Set_Reset_UI_Block_List();
    }

    public void Button_Save()
    {
        //iso_World.Set_Memory_Chance_fromCurrent();

        iso_World.Set_Memory_File_Resources(
            iso_World.Get_Memory_File_FolderName_Resources(),
            iso_World.Get_Memory_withGenerated(iso_Editor_File_UI.Get_World_Name()),
            false
            );

        iso_Editor_File_UI.Set_World(
            iso_Editor_File_UI.Get_World_Name(),
            iso_World.Get_Memory_withGenerated().Get_World_Size());
    }

    public void Button_Save_Temp()
    {
        //iso_World.Set_Memory_Chance_fromCurrent();

        iso_World.Set_Memory_File_Resources(
            iso_World.Get_Memory_File_FolderName_Resources(),
            iso_World.Get_Memory_withGenerated(iso_Editor_File_UI.Get_World_Name()),
            true
            );

        iso_Editor_File_UI.Set_World(
            iso_Editor_File_UI.Get_World_Name(),
            iso_World.Get_Memory_withGenerated().Get_World_Size());
    }

    public void Button_Open()
    {
        iso_World.Set_Memory(
            iso_World.Get_Memory_File_Resources(
                iso_World.Get_Memory_File_FolderName_Resources(),
                iso_Editor_File_UI.Get_World_Name(),
                false)
            );

        iso_World.Set_World_Generate_fromMemory();

        iso_Editor_File_UI.Set_World(
            iso_World.Get_Memory_withGenerated().Get_World_Name(),
            iso_World.Get_Memory_withGenerated().Get_World_Size());

        iso_Editor_World.Set_Reset_UI_PosAndSize(iso_Block.Get_PosOnMatrix_Primary());
        iso_Editor_World.Set_Reset_UI_Block_List();
    }

    public void Button_Open_Temp()
    {
        iso_World.Set_Memory(
            iso_World.Get_Memory_File_Resources(
                iso_World.Get_Memory_File_FolderName_Resources(),
                iso_Editor_File_UI.Get_World_Name(),
                true)
            );

        iso_World.Set_World_Generate_fromMemory();

        iso_Editor_File_UI.Set_World(
            iso_World.Get_Memory_withGenerated().Get_World_Name(),
            iso_World.Get_Memory_withGenerated().Get_World_Size());

        iso_Editor_World.Set_Reset_UI_PosAndSize(iso_Block.Get_PosOnMatrix_Primary());
        iso_Editor_World.Set_Reset_UI_Block_List();
    }

    #endregion
}