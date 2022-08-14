using UnityEngine;
using UnityEngine.UI;

public class IsoEditorUIMove : MonoBehaviour
{
    [Tooltip("Editor Block Move")]
    private IsoEditorMove cl_Editor_Block_Move;

    [Header("Button Move for Move Dir")]

    [Header("Dir XY")]

    [Tooltip("Button Dir Up")]
    [SerializeField]
    private UIButtonOnClick cl_Button_Dir_U;

    [Tooltip("Button Dir Down")]
    [SerializeField]
    private UIButtonOnClick cl_Button_Dir_D;

    [Tooltip("Button Dir Left")]
    [SerializeField]
    private UIButtonOnClick cl_Button_Dir_L;

    [Tooltip("Button Dir Right")]
    [SerializeField]
    private UIButtonOnClick cl_Button_Dir_R;

    [Header("Dir H")]

    [Tooltip("Button Dir Top")]
    [SerializeField]
    private UIButtonOnClick cl_Button_Dir_T;

    [Tooltip("Button Dir Bot")]
    [SerializeField]
    private UIButtonOnClick cl_Button_Dir_B;

    [Header("Dir Special")]

    [Tooltip("Button Dir Sta")]
    [SerializeField]
    private UIButtonOnClick cl_Button_Dir_Sta;

    [Tooltip("Button Dir Rev")]
    [SerializeField]
    private UIButtonOnClick cl_Button_Dir_Rev;

    [Header("Input Field for Move")]

    [Tooltip("Input Length")]
    [SerializeField]
    private InputField inp_Length;

    [Tooltip("Input Speed")]
    [SerializeField]
    private InputField inp_Speed;

    [Header("Button State for Move Status")]

    [Tooltip("Button Move Status")]
    [SerializeField]
    private UIButtonOnClick cl_Button_Status;

    [Tooltip("Button Move Status Text")]
    [SerializeField]
    private Text t_Move_Status;

    [Header("Button Move for Move List")]

    [Tooltip("Button Move List Add")]
    [SerializeField]
    private UIButtonOnClick cl_Button_List_Add;

    [Tooltip("Button Move List Del Lastest")]
    [SerializeField]
    private UIButtonOnClick cl_Button_List_Del_Lastest;

    [Tooltip("Move Dir")]
    private Vector3Int v3_Move_Dir;

    [Tooltip("Move Rev")]
    private bool b_Move_Rev = false;

    private void Start()
    {
        //cl_Button_Dir_U.Set_Event_Add_PointerDown(Button_Dir_U);
        //cl_Button_Dir_D.Set_Event_Add_PointerDown(Button_Dir_D);
        //cl_Button_Dir_L.Set_Event_Add_PointerDown(Button_Dir_L);
        //cl_Button_Dir_R.Set_Event_Add_PointerDown(Button_Dir_R);
        //cl_Button_Dir_T.Set_Event_Add_PointerDown(Button_Dir_T);
        //cl_Button_Dir_B.Set_Event_Add_PointerDown(Button_Dir_B);
        //cl_Button_Dir_Sta.Set_Event_Add_PointerDown(Button_Dir_Sta);
        //cl_Button_Dir_Rev.Set_Event_Add_PointerDown(Button_Dir_Rev);

        //cl_Button_Status.Set_Event_Add_PointerDown(Button_Status);

        //cl_Button_List_Add.Set_Event_Add_PointerDown(Button_Add);
        cl_Button_List_Add.Set_Button_Color_Active(cl_Button_List_Add.Get_Color_Normal_Primary());

        //cl_Button_List_Del_Lastest.Set_Event_Add_PointerDown(Button_Del_Lastest);
        cl_Button_List_Del_Lastest.Set_Button_Color_Active(cl_Button_List_Del_Lastest.Get_Color_Normal_Primary());

        Button_Dir_U();
        Set_Length(1);
        Set_Speed(0.005f);
    }

    public void Set_Editor_Block_Move(IsoEditorMove cl_Editor_Block_Move)
    {
        this.cl_Editor_Block_Move = cl_Editor_Block_Move;
    }

    #region Move Dir

    public void Set_Dir(Vector3Int v3_Move_Dir)
    {
        if (v3_Move_Dir == IsoClassDir.v3_Up_X)
        {
            Button_Dir_U();
        }
        else
        if (v3_Move_Dir == IsoClassDir.v3_Down_X)
        {
            Button_Dir_D();
        }
        else
        if (v3_Move_Dir == IsoClassDir.v3_Left_Y)
        {
            Button_Dir_L();
        }
        else
        if (v3_Move_Dir == IsoClassDir.v3_Right_Y)
        {
            Button_Dir_R();
        }
        else
        if (v3_Move_Dir == IsoClassDir.v3_Top_H)
        {
            Button_Dir_T();
        }
        else
        if (v3_Move_Dir == IsoClassDir.v3_Bot_H)
        {
            Button_Dir_B();
        }
        else
        if (v3_Move_Dir == IsoClassDir.v3_None)
        {
            Button_Dir_Sta();
        }
    }

    public void Button_Dir_U()
    {
        v3_Move_Dir = IsoClassDir.v3_Up_X;
        cl_Button_Dir_U.Set_Button_Color_Normal(cl_Button_Dir_U.Get_Button_Color_Active());
        cl_Button_Dir_U.Set_Button_Active_True();

        cl_Button_Dir_D.Set_Button_Color_Normal(cl_Button_Dir_D.Get_Color_Normal_Primary());
        cl_Button_Dir_D.Set_Button_Active_False();

        cl_Button_Dir_L.Set_Button_Color_Normal(cl_Button_Dir_L.Get_Color_Normal_Primary());
        cl_Button_Dir_L.Set_Button_Active_False();

        cl_Button_Dir_R.Set_Button_Color_Normal(cl_Button_Dir_R.Get_Color_Normal_Primary());
        cl_Button_Dir_R.Set_Button_Active_False();

        cl_Button_Dir_T.Set_Button_Color_Normal(cl_Button_Dir_T.Get_Color_Normal_Primary());
        cl_Button_Dir_T.Set_Button_Active_False();

        cl_Button_Dir_B.Set_Button_Color_Normal(cl_Button_Dir_B.Get_Color_Normal_Primary());
        cl_Button_Dir_B.Set_Button_Active_False();

        cl_Button_Dir_Sta.Set_Button_Color_Normal(cl_Button_Dir_Sta.Get_Color_Normal_Primary());
        cl_Button_Dir_Sta.Set_Button_Active_False();

        cl_Button_Dir_Rev.Set_Button_Color_Normal(cl_Button_Dir_Rev.Get_Color_Normal_Primary());
        cl_Button_Dir_Rev.Set_Button_Active_False();
        b_Move_Rev = false;
    }

    public void Button_Dir_D()
    {
        cl_Button_Dir_U.Set_Button_Color_Normal(cl_Button_Dir_U.Get_Color_Normal_Primary());
        cl_Button_Dir_U.Set_Button_Active_False();

        v3_Move_Dir = IsoClassDir.v3_Down_X;
        cl_Button_Dir_D.Set_Button_Color_Normal(cl_Button_Dir_D.Get_Button_Color_Active());
        cl_Button_Dir_D.Set_Button_Active_True();

        cl_Button_Dir_L.Set_Button_Color_Normal(cl_Button_Dir_L.Get_Color_Normal_Primary());
        cl_Button_Dir_L.Set_Button_Active_False();

        cl_Button_Dir_R.Set_Button_Color_Normal(cl_Button_Dir_R.Get_Color_Normal_Primary());
        cl_Button_Dir_R.Set_Button_Active_False();

        cl_Button_Dir_T.Set_Button_Color_Normal(cl_Button_Dir_T.Get_Color_Normal_Primary());
        cl_Button_Dir_T.Set_Button_Active_False();

        cl_Button_Dir_B.Set_Button_Color_Normal(cl_Button_Dir_B.Get_Color_Normal_Primary());
        cl_Button_Dir_B.Set_Button_Active_False();

        cl_Button_Dir_Sta.Set_Button_Color_Normal(cl_Button_Dir_Sta.Get_Color_Normal_Primary());
        cl_Button_Dir_Sta.Set_Button_Active_False();

        cl_Button_Dir_Rev.Set_Button_Color_Normal(cl_Button_Dir_Rev.Get_Color_Normal_Primary());
        cl_Button_Dir_Rev.Set_Button_Active_False();
        b_Move_Rev = false;
    }

    public void Button_Dir_L()
    {
        cl_Button_Dir_U.Set_Button_Color_Normal(cl_Button_Dir_U.Get_Color_Normal_Primary());
        cl_Button_Dir_U.Set_Button_Active_False();

        cl_Button_Dir_D.Set_Button_Color_Normal(cl_Button_Dir_D.Get_Color_Normal_Primary());
        cl_Button_Dir_D.Set_Button_Active_False();

        v3_Move_Dir = IsoClassDir.v3_Left_Y;
        cl_Button_Dir_L.Set_Button_Color_Normal(cl_Button_Dir_L.Get_Button_Color_Active());
        cl_Button_Dir_L.Set_Button_Active_True();

        cl_Button_Dir_R.Set_Button_Color_Normal(cl_Button_Dir_R.Get_Color_Normal_Primary());
        cl_Button_Dir_R.Set_Button_Active_False();

        cl_Button_Dir_T.Set_Button_Color_Normal(cl_Button_Dir_T.Get_Color_Normal_Primary());
        cl_Button_Dir_T.Set_Button_Active_False();

        cl_Button_Dir_B.Set_Button_Color_Normal(cl_Button_Dir_B.Get_Color_Normal_Primary());
        cl_Button_Dir_B.Set_Button_Active_False();

        cl_Button_Dir_Sta.Set_Button_Color_Normal(cl_Button_Dir_Sta.Get_Color_Normal_Primary());
        cl_Button_Dir_Sta.Set_Button_Active_False();

        cl_Button_Dir_Rev.Set_Button_Color_Normal(cl_Button_Dir_Rev.Get_Color_Normal_Primary());
        cl_Button_Dir_Rev.Set_Button_Active_False();
        b_Move_Rev = false;
    }

    public void Button_Dir_R()
    {
        cl_Button_Dir_U.Set_Button_Color_Normal(cl_Button_Dir_U.Get_Color_Normal_Primary());
        cl_Button_Dir_U.Set_Button_Active_False();

        cl_Button_Dir_D.Set_Button_Color_Normal(cl_Button_Dir_D.Get_Color_Normal_Primary());
        cl_Button_Dir_D.Set_Button_Active_False();

        cl_Button_Dir_L.Set_Button_Color_Normal(cl_Button_Dir_L.Get_Color_Normal_Primary());
        cl_Button_Dir_L.Set_Button_Active_False();

        v3_Move_Dir = IsoClassDir.v3_Right_Y;
        cl_Button_Dir_R.Set_Button_Color_Normal(cl_Button_Dir_R.Get_Button_Color_Active());
        cl_Button_Dir_R.Set_Button_Active_True();

        cl_Button_Dir_T.Set_Button_Color_Normal(cl_Button_Dir_T.Get_Color_Normal_Primary());
        cl_Button_Dir_T.Set_Button_Active_False();

        cl_Button_Dir_B.Set_Button_Color_Normal(cl_Button_Dir_B.Get_Color_Normal_Primary());
        cl_Button_Dir_B.Set_Button_Active_False();

        cl_Button_Dir_Sta.Set_Button_Color_Normal(cl_Button_Dir_Sta.Get_Color_Normal_Primary());
        cl_Button_Dir_Sta.Set_Button_Active_False();

        cl_Button_Dir_Rev.Set_Button_Color_Normal(cl_Button_Dir_Rev.Get_Color_Normal_Primary());
        cl_Button_Dir_Rev.Set_Button_Active_False();
        b_Move_Rev = false;
    }

    public void Button_Dir_T()
    {
        cl_Button_Dir_U.Set_Button_Color_Normal(cl_Button_Dir_U.Get_Color_Normal_Primary());
        cl_Button_Dir_U.Set_Button_Active_False();

        cl_Button_Dir_D.Set_Button_Color_Normal(cl_Button_Dir_D.Get_Color_Normal_Primary());
        cl_Button_Dir_D.Set_Button_Active_False();

        cl_Button_Dir_L.Set_Button_Color_Normal(cl_Button_Dir_L.Get_Color_Normal_Primary());
        cl_Button_Dir_L.Set_Button_Active_False();

        cl_Button_Dir_R.Set_Button_Color_Normal(cl_Button_Dir_R.Get_Color_Normal_Primary());
        cl_Button_Dir_R.Set_Button_Active_False();

        v3_Move_Dir = IsoClassDir.v3_Top_H;
        cl_Button_Dir_T.Set_Button_Color_Normal(cl_Button_Dir_T.Get_Button_Color_Active());
        cl_Button_Dir_T.Set_Button_Active_True();

        cl_Button_Dir_B.Set_Button_Color_Normal(cl_Button_Dir_B.Get_Color_Normal_Primary());
        cl_Button_Dir_B.Set_Button_Active_False();

        cl_Button_Dir_Sta.Set_Button_Color_Normal(cl_Button_Dir_Sta.Get_Color_Normal_Primary());
        cl_Button_Dir_Sta.Set_Button_Active_False();

        cl_Button_Dir_Rev.Set_Button_Color_Normal(cl_Button_Dir_Rev.Get_Color_Normal_Primary());
        cl_Button_Dir_Rev.Set_Button_Active_False();
        b_Move_Rev = false;
    }

    public void Button_Dir_B()
    {
        cl_Button_Dir_U.Set_Button_Color_Normal(cl_Button_Dir_U.Get_Color_Normal_Primary());
        cl_Button_Dir_U.Set_Button_Active_False();

        cl_Button_Dir_D.Set_Button_Color_Normal(cl_Button_Dir_D.Get_Color_Normal_Primary());
        cl_Button_Dir_D.Set_Button_Active_False();

        cl_Button_Dir_L.Set_Button_Color_Normal(cl_Button_Dir_L.Get_Color_Normal_Primary());
        cl_Button_Dir_L.Set_Button_Active_False();

        cl_Button_Dir_R.Set_Button_Color_Normal(cl_Button_Dir_R.Get_Color_Normal_Primary());
        cl_Button_Dir_R.Set_Button_Active_False();

        cl_Button_Dir_T.Set_Button_Color_Normal(cl_Button_Dir_T.Get_Color_Normal_Primary());
        cl_Button_Dir_T.Set_Button_Active_False();

        v3_Move_Dir = IsoClassDir.v3_Bot_H;
        cl_Button_Dir_B.Set_Button_Color_Normal(cl_Button_Dir_B.Get_Button_Color_Active());
        cl_Button_Dir_B.Set_Button_Active_True();

        cl_Button_Dir_Sta.Set_Button_Color_Normal(cl_Button_Dir_Sta.Get_Color_Normal_Primary());
        cl_Button_Dir_Sta.Set_Button_Active_False();

        cl_Button_Dir_Rev.Set_Button_Color_Normal(cl_Button_Dir_Rev.Get_Color_Normal_Primary());
        cl_Button_Dir_Rev.Set_Button_Active_False();
        b_Move_Rev = false;
    }

    public void Button_Dir_Sta()
    {
        cl_Button_Dir_U.Set_Button_Color_Normal(cl_Button_Dir_U.Get_Color_Normal_Primary());
        cl_Button_Dir_U.Set_Button_Active_False();

        cl_Button_Dir_D.Set_Button_Color_Normal(cl_Button_Dir_D.Get_Color_Normal_Primary());
        cl_Button_Dir_D.Set_Button_Active_False();

        cl_Button_Dir_L.Set_Button_Color_Normal(cl_Button_Dir_L.Get_Color_Normal_Primary());
        cl_Button_Dir_L.Set_Button_Active_False();

        cl_Button_Dir_R.Set_Button_Color_Normal(cl_Button_Dir_R.Get_Color_Normal_Primary());
        cl_Button_Dir_R.Set_Button_Active_False();

        cl_Button_Dir_T.Set_Button_Color_Normal(cl_Button_Dir_T.Get_Color_Normal_Primary());
        cl_Button_Dir_T.Set_Button_Active_False();

        cl_Button_Dir_B.Set_Button_Color_Normal(cl_Button_Dir_B.Get_Color_Normal_Primary());
        cl_Button_Dir_B.Set_Button_Active_False();

        v3_Move_Dir = IsoClassDir.v3_None;
        cl_Button_Dir_Sta.Set_Button_Color_Normal(cl_Button_Dir_Sta.Get_Button_Color_Active());
        cl_Button_Dir_Sta.Set_Button_Active_True();

        cl_Button_Dir_Rev.Set_Button_Color_Normal(cl_Button_Dir_Rev.Get_Color_Normal_Primary());
        cl_Button_Dir_Rev.Set_Button_Active_False();
        b_Move_Rev = false;
    }

    public void Button_Dir_Rev()
    {
        cl_Button_Dir_U.Set_Button_Color_Normal(cl_Button_Dir_U.Get_Color_Normal_Primary());
        cl_Button_Dir_U.Set_Button_Active_False();

        cl_Button_Dir_D.Set_Button_Color_Normal(cl_Button_Dir_D.Get_Color_Normal_Primary());
        cl_Button_Dir_D.Set_Button_Active_False();

        cl_Button_Dir_L.Set_Button_Color_Normal(cl_Button_Dir_L.Get_Color_Normal_Primary());
        cl_Button_Dir_L.Set_Button_Active_False();

        cl_Button_Dir_R.Set_Button_Color_Normal(cl_Button_Dir_R.Get_Color_Normal_Primary());
        cl_Button_Dir_R.Set_Button_Active_False();

        cl_Button_Dir_T.Set_Button_Color_Normal(cl_Button_Dir_T.Get_Color_Normal_Primary());
        cl_Button_Dir_T.Set_Button_Active_False();

        cl_Button_Dir_B.Set_Button_Color_Normal(cl_Button_Dir_B.Get_Color_Normal_Primary());
        cl_Button_Dir_B.Set_Button_Active_False();

        cl_Button_Dir_Sta.Set_Button_Color_Normal(cl_Button_Dir_Sta.Get_Color_Normal_Primary());
        cl_Button_Dir_Sta.Set_Button_Active_False();

        v3_Move_Dir = IsoClassDir.v3_None;
        cl_Button_Dir_Rev.Set_Button_Color_Normal(cl_Button_Dir_Rev.Get_Button_Color_Active());
        cl_Button_Dir_Rev.Set_Button_Active_True();
        b_Move_Rev = true;
    }

    public Vector3Int Get_Dir()
    {
        return v3_Move_Dir;
    }

    public bool Get_Rev()
    {
        return b_Move_Rev;
    }

    #endregion

    #region Move Length

    public void Set_Length(int i_Move_Length)
    {
        inp_Length.text = i_Move_Length.ToString();
    }

    public int Get_Length()
    {
        return int.Parse(inp_Length.text);
    }

    #endregion

    #region Move Speed

    public void Set_Speed(float f_Move_Speed)
    {
        inp_Speed.text = f_Move_Speed.ToString();
    }

    public float Get_Speed()
    {
        return float.Parse(inp_Speed.text);
    }

    #endregion

    #region Move Status

    public void Button_Status()
    {
        cl_Editor_Block_Move.Set_Status_Chance();

        Set_Status(cl_Editor_Block_Move.Get_Status());
    }

    public void Set_Status(bool b_Move_Status)
    {
        cl_Button_Status.Set_Button_Active(b_Move_Status);

        if (b_Move_Status)
        {
            t_Move_Status.text = "STATUS: RUN!";
        }
        else
        {
            t_Move_Status.text = "STATUS: STA!";
        }
    }

    public bool Get_Status()
    {
        return cl_Button_Status.Get_Button_Active();
    }

    #endregion

    #region Move List

    public void Button_Add()
    {
        if (Get_Rev())
        {
            cl_Editor_Block_Move.Set_Add_Rev();
        }
        else
        {
            cl_Editor_Block_Move.Set_Add(Get_Dir(), Get_Length(), Get_Speed());
        }
    }

    public void Button_Del_Lastest()
    {
        cl_Editor_Block_Move.Set_Del_Lastest();
    }

    #endregion
}
