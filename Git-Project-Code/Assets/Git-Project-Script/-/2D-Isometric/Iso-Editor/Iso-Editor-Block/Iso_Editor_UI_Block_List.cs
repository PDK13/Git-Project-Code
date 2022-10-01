using UnityEngine;
using UnityEngine.UI;

public class Iso_Editor_UI_Block_List : MonoBehaviour
{
    [Header("UI List")]

    [Tooltip("Main List Name")]
    [SerializeField]
    private Text t_Main_Name;

    [Tooltip("Main List Pos")]
    [SerializeField]
    private Text t_Main_Pos;

    private readonly int i_UI_Move = 1;

    private readonly int i_UI_JoinTo = 2;

    private readonly int i_UI_SwitchTo = 3;

    private readonly int i_UI_Message = 4;

    private int i_UI = 0;

    #region UI Set

    public void Set_UI_Move(Vector3Int v3_Main_Pos)
    {
        i_UI = i_UI_Move;

        Set_Main_Name("MOVE");
        Set_Main_Pos(v3_Main_Pos);
    }

    public void Set_UI_JoinTo(Vector3Int v3_Main_Pos)
    {
        i_UI = i_UI_JoinTo;

        Set_Main_Name("JOIN-TO");
        Set_Main_Pos(v3_Main_Pos);
    }

    public void Set_UI_SwitchTo(Vector3Int v3_Main_Pos)
    {
        i_UI = i_UI_SwitchTo;

        Set_Main_Name("SWITCH-TO");
        Set_Main_Pos(v3_Main_Pos);
    }

    public void Set_UI_Message(Vector3Int v3_Main_Pos)
    {
        i_UI = i_UI_Message;

        Set_Main_Name("MESSAGE");
        Set_Main_Pos(v3_Main_Pos);
    }

    private void Set_Main_Name(string s_Main_Name)
    {
        t_Main_Name.text = s_Main_Name;
    }

    private void Set_Main_Pos(Vector3Int v3_Main_Pos)
    {
        t_Main_Pos.text = v3_Main_Pos.ToString();
    }

    #endregion

    #region UI Get

    public bool Get_UI_Move()
    {
        return i_UI == i_UI_Move;
    }

    public bool Get_UI_JoinTo()
    {
        return i_UI == i_UI_JoinTo;
    }

    public bool Get_UI_SwitchTo()
    {
        return i_UI == i_UI_SwitchTo;
    }

    public bool Get_UI_Message()
    {
        return i_UI == i_UI_Message;
    }

    #endregion
}
