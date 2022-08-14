using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IsoEditorUIFile : MonoBehaviour
{
    [Header("Text File for File")]

    [Tooltip("World Name")]
    [SerializeField]
    private Text t_WorldName;

    [Tooltip("World Size")]
    [SerializeField]
    private Text t_WorldSize;

    [Header("Input Field for File")]

    [Tooltip("World Name")]
    [SerializeField]
    private InputField i_WorldName;

    [Tooltip("Teleport Size X")]
    [SerializeField]
    private InputField inp_Size_X;

    [Tooltip("Teleport Size Y")]
    [SerializeField]
    private InputField inp_Size_Y;

    [Tooltip("Teleport Size H")]
    [SerializeField]
    private InputField inp_Size_H;

    [Header("Button File for File Manager")]

    [Tooltip("Button File New")]
    [SerializeField]
    private UIButtonOnClick cl_Button_New;

    [Tooltip("Button File Save")]
    [SerializeField]
    private UIButtonOnClick cl_Button_Save;

    [Tooltip("Button File Open")]
    [SerializeField]
    private UIButtonOnClick cl_Button_Open;

    [Tooltip("Button File Open-Temp")]
    [SerializeField]
    private UIButtonOnClick cl_Button_Open_Temp;

    private void Awake()
    {
        Set_World("...", new Vector3Int());
    }

    #region File

    public void Set_World(string s_WorldName, Vector3Int v3_WorldSize)
    {
        t_WorldName.text = s_WorldName;
        t_WorldSize.text = v3_WorldSize.ToString();
    }

    public string Get_World_Name()
    {
        return i_WorldName.text;
    }

    public Vector3Int Get_World_Size()
    {
        return new Vector3Int(
            int.Parse(inp_Size_X.text),
            int.Parse(inp_Size_Y.text),
            int.Parse(inp_Size_H.text));
    }

    public UIButtonOnClick Get_Button_New()
    {
        return cl_Button_New;
    }

    public UIButtonOnClick Get_Button_Save()
    {
        return cl_Button_Save;
    }

    public UIButtonOnClick Get_Button_Open()
    {
        return cl_Button_Open;
    }

    public UIButtonOnClick Get_Button_Open_Temp()
    {
        return cl_Button_Open_Temp;
    }

    #endregion
}
