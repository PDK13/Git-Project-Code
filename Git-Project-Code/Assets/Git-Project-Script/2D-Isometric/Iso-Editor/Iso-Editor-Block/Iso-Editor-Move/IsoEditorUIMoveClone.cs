using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IsoEditorUIMoveClone : MonoBehaviour
{
    [Tooltip("Button Fix")]
    //[SerializeField]
    private IsoEditorMove cl_Editor_Block_Move;

    [Header("Text for Move Clone")]

    [Tooltip("Move Dir")]
    [SerializeField]
    private Text t_Dir;

    [Tooltip("Move Length")]
    [SerializeField]
    private Text t_Length;

    [Tooltip("Move Speed")]
    [SerializeField]
    private Text t_Speed;

    [Tooltip("Move Pos-Move-To")]
    [SerializeField]
    private Text t_PosMoveTo;

    [Header("Button  for Move Clone")]

    [Tooltip("Button Fix")]
    [SerializeField]
    private UIButtonOnClick ui_Button_Fix;

    [Tooltip("Button Copy")]
    [SerializeField]
    private UIButtonOnClick ui_Button_Copy;

    [Tooltip("Button Delete")]
    [SerializeField]
    private UIButtonOnClick ui_Button_Del;

    private void Awake()
    {
        if (cl_Editor_Block_Move == null)
        {
            cl_Editor_Block_Move = GameObject.FindGameObjectWithTag("IsoWorldEditor").GetComponent<IsoEditorMove>();
        }

        //ui_Button_Fix.Set_Event_Add_PointerDown(Button_Fix);
        ui_Button_Fix.Set_Button_Color_Active(ui_Button_Fix.Get_Button_Color_Normal());

        //ui_Button_Copy.Set_Event_Add_PointerDown(Button_Copy);
        ui_Button_Copy.Set_Button_Color_Active(ui_Button_Copy.Get_Button_Color_Normal());

        //ui_Button_Del.Set_Event_Add_PointerDown(Button_Del);
        ui_Button_Del.Set_Button_Color_Active(ui_Button_Del.Get_Button_Color_Normal());
    }

    #region Set Clone

    public void Set_Clone(Vector3Int v3_Dir, int i_Length, float f_Speed, Vector3Int v3_PosMoveTo)
    {
        if (v3_Dir == IsoClassDir.v3_None)
        {
            t_Dir.text = "STA";
            t_Dir.fontSize = 8;
            t_Length.text = "";
            t_Speed.text = "";
            t_PosMoveTo.text = v3_PosMoveTo.ToString();

            return;
        }

        if (v3_Dir == IsoClassDir.v3_Up_X)
        {
            t_Dir.text = "U";
        }
        else
        if (v3_Dir == IsoClassDir.v3_Down_X)
        {
            t_Dir.text = "D";
        }
        else
        if (v3_Dir == IsoClassDir.v3_Left_Y)
        {
            t_Dir.text = "L";
        }
        else
        if (v3_Dir == IsoClassDir.v3_Right_Y)
        {
            t_Dir.text = "R";
        }
        else
        if (v3_Dir == IsoClassDir.v3_Top_H)
        {
            t_Dir.text = "T";
        }
        else
        if (v3_Dir == IsoClassDir.v3_Bot_H)
        {
            t_Dir.text = "B";
        }
        t_Dir.fontSize = 10;
        t_Length.text = i_Length.ToString();
        t_Speed.text = f_Speed.ToString();
        t_PosMoveTo.text = v3_PosMoveTo.ToString();
    }

    #endregion

    #region Button Clone

    public void Button_Fix()
    {
        cl_Editor_Block_Move.Set_Fix(GetComponent<UIVerticalClone>().Get_Clone_Index());
    }

    public void Button_Copy()
    {
        cl_Editor_Block_Move.Set_Copy(GetComponent<UIVerticalClone>().Get_Clone_Index());
    }

    public void Button_Del()
    {
        cl_Editor_Block_Move.Set_Del(GetComponent<UIVerticalClone>().Get_Clone_Index());
    }

    #endregion
}
