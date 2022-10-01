using UnityEngine;
using UnityEngine.UI;

public class IsoEditorUITeleport : MonoBehaviour
{
    [Header("Text Teleport for Teleport")]

    [Tooltip("World Name")]
    [SerializeField]
    private Text t_WorldName;

    [Tooltip("Teleport Pos")]
    [SerializeField]
    private Text t_Teleport_Pos;

    [Header("Input Field Teleport for Teleport")]

    [Tooltip("World Name")]
    [SerializeField]
    private InputField i_WorldName;

    [Tooltip("World Size X")]
    [SerializeField]
    private InputField i_Pos_X;

    [Tooltip("World Size Y")]
    [SerializeField]
    private InputField i_Pos_Y;

    [Tooltip("World Size H")]
    [SerializeField]
    private InputField i_Pos_H;

    [Header("Button Teleport for Teleport")]

    [Tooltip("Button Del")]
    [SerializeField]
    private UIButtonOnClick cl_Button_Del;

    [Tooltip("Button Add")]
    [SerializeField]
    private UIButtonOnClick cl_Button_Add;

    private void Awake()
    {
        Set_Teleport("", new Vector3Int());
    }

    public void Set_Teleport(string s_WorldName, Vector3Int v3_Teleport_Pos)
    {
        if (s_WorldName != "")
        {
            t_WorldName.text = s_WorldName;
            t_Teleport_Pos.text = v3_Teleport_Pos.ToString();
        }
        else
        {
            t_WorldName.text = "";
            t_Teleport_Pos.text = "NONE TELEPORT";
        }
    }

    public IsoDataTeleport GetData()
    {
        return new IsoDataTeleport(null, GetWorld_Name(), GetPos());
    }

    public string GetWorld_Name()
    {
        return i_WorldName.text;
    }

    public Vector3Int GetPos()
    {
        return new Vector3Int(
            int.Parse(i_Pos_X.text),
            int.Parse(i_Pos_Y.text),
            int.Parse(i_Pos_H.text));
    }

    public UIButtonOnClick GetButton_Del()
    {
        return cl_Button_Del;
    }

    public UIButtonOnClick GetButton_Add()
    {


        return cl_Button_Add;
    }
}
