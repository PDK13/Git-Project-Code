using UnityEngine;

/// <summary>
/// Isometric Editor World with Add and Remove 
/// </summary>
public class IsoEditorWorldMatrix : MonoBehaviour
{
    [Header("Color View Block(s) on Matrix World")]

    [Tooltip("Color Block when Uncheck")]
    [SerializeField]
    private Color c_Color_Uncheck_Block = Color.gray;

    [Header("UI Component")]

    [Tooltip("Editor World Matrix UI")]
    [SerializeField]
    private IsoEditorUIWorldMatrix iso_Editor_World_Matrix_UI;

    [Tooltip("View Matrix X")]
    private bool b_View_X = false;

    [Tooltip("View Matrix Y")]
    private bool b_View_Y = false;

    [Tooltip("View Matrix H")]
    private bool b_View_H = false;

    [Tooltip("View Matrix All")]
    private bool b_View_All = false;

    private IsoEditorFile cl_Editor_File;

    private IsoWorld iso_World;

    private IsoBlock iso_Block;

    private IsoEditorWorld iso_Editor_World;

    private void Awake()
    {
        iso_World = GameObject.FindGameObjectWithTag("IsoWorldManager").GetComponent<IsoWorld>();

        cl_Editor_File = GetComponent<IsoEditorFile>();

        iso_Block = GetComponent<IsoBlock>();

        iso_Editor_World = GetComponent<IsoEditorWorld>();

        //Add

        //iso_Editor_World_Matrix_UI.Get_Button_Add_XH().Set_Event_Add_PointerDown(Button_WorldMatrix_Add_XH);
        iso_Editor_World_Matrix_UI.Get_Button_Add_XH().Set_Button_Color_Active(iso_Editor_World_Matrix_UI.Get_Button_Add_XH().Get_Button_Color_Normal());

        //iso_Editor_World_Matrix_UI.Get_Button_Add_YH().Set_Event_Add_PointerDown(Button_WorldMatrix_Add_YH);
        iso_Editor_World_Matrix_UI.Get_Button_Add_YH().Set_Button_Color_Active(iso_Editor_World_Matrix_UI.Get_Button_Add_YH().Get_Button_Color_Normal());

        //iso_Editor_World_Matrix_UI.Get_Button_Add_XY().Set_Event_Add_PointerDown(Button_WorldMatrix_Add_XY);
        iso_Editor_World_Matrix_UI.Get_Button_Add_XY().Set_Button_Color_Active(iso_Editor_World_Matrix_UI.Get_Button_Add_XY().Get_Button_Color_Normal());

        //Remove

        //iso_Editor_World_Matrix_UI.Get_Button_Remove_XH().Set_Event_Add_PointerDown(Button_WorldMatrix_Remove_XH);
        iso_Editor_World_Matrix_UI.Get_Button_Remove_XH().Set_Button_Color_Active(iso_Editor_World_Matrix_UI.Get_Button_Remove_XH().Get_Button_Color_Normal());

        //iso_Editor_World_Matrix_UI.Get_Button_Remove_YH().Set_Event_Add_PointerDown(Button_WorldMatrix_Remove_YH);
        iso_Editor_World_Matrix_UI.Get_Button_Remove_YH().Set_Button_Color_Active(iso_Editor_World_Matrix_UI.Get_Button_Remove_YH().Get_Button_Color_Normal());

        //iso_Editor_World_Matrix_UI.Get_Button_Remove_XY().Set_Event_Add_PointerDown(Button_WorldMatrix_Remove_XY);
        iso_Editor_World_Matrix_UI.Get_Button_Remove_XY().Set_Button_Color_Active(iso_Editor_World_Matrix_UI.Get_Button_Remove_XY().Get_Button_Color_Normal());

        //View

        //iso_Editor_World_Matrix_UI.Get_Button_View_X().Set_Event_Add_PointerDown(Button_WorldView_X);

        //iso_Editor_World_Matrix_UI.Get_Button_View_Y().Set_Event_Add_PointerDown(Button_WorldView_Y);

        //iso_Editor_World_Matrix_UI.Get_Button_View_H().Set_Event_Add_PointerDown(Button_WorldView_H);
    }

    private void FixedUpdate()
    {
        Set_WorldView_whenActive();
    }

    #region World 

    #region World Add 

    /// <summary>
    /// Set Matrix World Add by XY - TopBot - High
    /// </summary>
    public void Button_WorldMatrix_Add_XY()
    {
        iso_World.Set_World_Add_XY(iso_Block.Get_PosOnMatrix_Current());

        iso_Editor_World.Set_Reset_UI_Block_List();
        iso_Editor_World.Set_Reset_UI_PosAndSize(iso_Block.Get_PosOnMatrix_Primary());

        cl_Editor_File.Button_Save_Temp();
    }

    /// <summary>
    /// Set Matrix World Add by XH - LR - Y
    /// </summary>
    public void Button_WorldMatrix_Add_XH()
    {
        iso_World.Set_World_Add_XH(iso_Block.Get_PosOnMatrix_Current());

        iso_Editor_World.Set_Reset_UI_Block_List();
        iso_Editor_World.Set_Reset_UI_PosAndSize(iso_Block.Get_PosOnMatrix_Primary());

        cl_Editor_File.Button_Save_Temp();
    }

    /// <summary>
    /// Set Matrix World Add by YH - UD - X
    /// </summary>
    public void Button_WorldMatrix_Add_YH()
    {
        iso_World.Set_World_Add_YH(iso_Block.Get_PosOnMatrix_Current());

        iso_Editor_World.Set_Reset_UI_Block_List();
        iso_Editor_World.Set_Reset_UI_PosAndSize(iso_Block.Get_PosOnMatrix_Primary());

        cl_Editor_File.Button_Save_Temp();
    }

    #endregion

    #region World Remove 

    /// <summary>
    /// Set Matrix World Remove by XY - TopBot - High
    /// </summary>
    public void Button_WorldMatrix_Remove_XY()
    {
        iso_World.Set_World_Remove_XY(iso_Block.Get_PosOnMatrix_Current());

        iso_Editor_World.Set_Reset_UI_Block_List();
        iso_Editor_World.Set_Reset_UI_PosAndSize(iso_Block.Get_PosOnMatrix_Primary());

        cl_Editor_File.Button_Save_Temp();
    }

    /// <summary>
    /// Set Matrix World Remove by XH - LR - Y
    /// </summary>
    public void Button_WorldMatrix_Remove_XH()
    {
        iso_World.Set_World_Remove_XH(iso_Block.Get_PosOnMatrix_Current());

        iso_Editor_World.Set_Reset_UI_Block_List();
        iso_Editor_World.Set_Reset_UI_PosAndSize(iso_Block.Get_PosOnMatrix_Primary());

        cl_Editor_File.Button_Save_Temp();
    }

    /// <summary>
    /// Set Matrix World Remove by YH - UD - X
    /// </summary>
    public void Button_WorldMatrix_Remove_YH()
    {
        iso_World.Set_World_Remove_YH(iso_Block.Get_PosOnMatrix_Current());

        iso_Editor_World.Set_Reset_UI_Block_List();
        iso_Editor_World.Set_Reset_UI_PosAndSize(iso_Block.Get_PosOnMatrix_Primary());

        cl_Editor_File.Button_Save_Temp();
    }

    #endregion

    #endregion

    #region World View 

    private void Set_WorldView_whenActive()
    {
        if (!iso_World.Get_World_isActive())
        {
            return;
        }

        if (b_View_X)
        {
            Set_WorldView_X();

            b_View_All = false;
        }
        else
        if (b_View_Y)
        {
            Set_WorldView_Y();

            b_View_All = false;
        }
        else
        if (b_View_H)
        {
            Set_WorldView_H();

            b_View_All = false;
        }
        else
        if (!b_View_All)
        {
            Set_WorldView_All();

            b_View_All = true;
        }
    }

    public void Set_WorldView_nonActive()
    {
        if (iso_World.Get_World_isActive())
        {
            return;
        }

        if (b_View_X)
        {
            Set_WorldView_X();

            b_View_All = false;
        }
        else
        if (b_View_Y)
        {
            Set_WorldView_Y();

            b_View_All = false;
        }
        else
        if (b_View_H)
        {
            Set_WorldView_H();

            b_View_All = false;
        }
        else
        if (!b_View_All)
        {
            Set_WorldView_All();

            b_View_All = true;
        }
    }

    #region World View All

    private void Set_WorldView_All()
    {
        for (int x = 0; x < iso_World.Get_World_Size_Current().x; x++)
        {
            for (int y = 0; y < iso_World.Get_World_Size_Current().y; y++)
            {
                for (int h = 0; h < iso_World.Get_World_Size_Current().z; h++)
                {
                    if (!iso_World.Get_Current_isEmty(new Vector3Int(x, y, h)))
                    {
                        iso_World.Get_Current_GameObject(new Vector3Int(x, y, h)).GetComponent<SpriteRenderer>().color = Color.white;
                    }
                }
            }
        }
    }

    #endregion

    #region World View X 

    public void Button_WorldView_X()
    {
        b_View_X = !b_View_X;
        b_View_Y = false;
        b_View_H = false;

        iso_Editor_World_Matrix_UI.Get_Button_View_X().Set_Button_Active(b_View_X);
        iso_Editor_World_Matrix_UI.Get_Button_View_Y().Set_Button_Active(b_View_Y);
        iso_Editor_World_Matrix_UI.Get_Button_View_H().Set_Button_Active(b_View_H);

        Set_WorldView_nonActive();
    }

    private void Set_WorldView_X()
    {
        for (int x = 0; x < iso_World.Get_World_Size_Current().x; x++)
        {
            for (int y = 0; y < iso_World.Get_World_Size_Current().y; y++)
            {
                for (int h = 0; h < iso_World.Get_World_Size_Current().z; h++)
                {
                    if (x == iso_Block.Get_PosOnMatrix_Current().x)
                    {
                        if (!iso_World.Get_Current_isEmty(new Vector3Int(x, y, h)))
                        {
                            iso_World.Get_Current_GameObject(new Vector3Int(x, y, h)).GetComponent<SpriteRenderer>().color = Color.white;
                        }
                    }
                    else
                    {
                        if (!iso_World.Get_Current_isEmty(new Vector3Int(x, y, h)))
                        {
                            iso_World.Get_Current_GameObject(new Vector3Int(x, y, h)).GetComponent<SpriteRenderer>().color = c_Color_Uncheck_Block;
                        }
                    }
                }
            }
        }
    }

    #endregion

    #region World View Y 

    public void Button_WorldView_Y()
    {
        b_View_X = false;
        b_View_Y = !b_View_Y;
        b_View_H = false;

        iso_Editor_World_Matrix_UI.Get_Button_View_X().Set_Button_Active(b_View_X);
        iso_Editor_World_Matrix_UI.Get_Button_View_Y().Set_Button_Active(b_View_Y);
        iso_Editor_World_Matrix_UI.Get_Button_View_H().Set_Button_Active(b_View_H);

        Set_WorldView_nonActive();
    }

    private void Set_WorldView_Y()
    {
        for (int x = 0; x < iso_World.Get_World_Size_Current().x; x++)
        {
            for (int y = 0; y < iso_World.Get_World_Size_Current().y; y++)
            {
                for (int h = 0; h < iso_World.Get_World_Size_Current().z; h++)
                {
                    if (y == iso_Block.Get_PosOnMatrix_Current().y)
                    {
                        if (!iso_World.Get_Current_isEmty(new Vector3Int(x, y, h)))
                        {
                            iso_World.Get_Current_GameObject(new Vector3Int(x, y, h)).GetComponent<SpriteRenderer>().color = Color.white;
                        }
                    }
                    else
                    {
                        if (!iso_World.Get_Current_isEmty(new Vector3Int(x, y, h)))
                        {
                            iso_World.Get_Current_GameObject(new Vector3Int(x, y, h)).GetComponent<SpriteRenderer>().color = c_Color_Uncheck_Block;
                        }
                    }
                }
            }
        }
    }

    #endregion

    #region World View H 

    public void Button_WorldView_H()
    {
        b_View_X = false;
        b_View_Y = false;
        b_View_H = !b_View_H;

        iso_Editor_World_Matrix_UI.Get_Button_View_X().Set_Button_Active(b_View_X);
        iso_Editor_World_Matrix_UI.Get_Button_View_Y().Set_Button_Active(b_View_Y);
        iso_Editor_World_Matrix_UI.Get_Button_View_H().Set_Button_Active(b_View_H);

        Set_WorldView_nonActive();
    }

    private void Set_WorldView_H()
    {
        for (int x = 0; x < iso_World.Get_World_Size_Current().x; x++)
        {
            for (int y = 0; y < iso_World.Get_World_Size_Current().y; y++)
            {
                for (int h = 0; h < iso_World.Get_World_Size_Current().z; h++)
                {
                    if (h == iso_Block.Get_PosOnMatrix_Current().z)
                    {
                        if (!iso_World.Get_Current_isEmty(new Vector3Int(x, y, h)))
                        {
                            iso_World.Get_Current_GameObject(new Vector3Int(x, y, h)).GetComponent<SpriteRenderer>().color = Color.white;
                        }
                    }
                    else
                    {
                        if (!iso_World.Get_Current_isEmty(new Vector3Int(x, y, h)))
                        {
                            iso_World.Get_Current_GameObject(new Vector3Int(x, y, h)).GetComponent<SpriteRenderer>().color = c_Color_Uncheck_Block;
                        }
                    }
                }
            }
        }
    }

    #endregion

    #endregion
}
