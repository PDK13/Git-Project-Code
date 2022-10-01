using UnityEngine;

public class IsoEditorUIWorldMatrix : MonoBehaviour
{
    [Header("Button")]

    [Header("World Add")]

    [Tooltip("World Add YH")]
    [SerializeField]
    private UIButtonOnClick ui_Button_Add_YH;

    [Tooltip("World Add XH")]
    [SerializeField]
    private UIButtonOnClick ui_Button_Add_XH;

    [Tooltip("World Add XY")]
    [SerializeField]
    private UIButtonOnClick ui_Button_Add_XY;

    [Header("World Remove")]

    [Tooltip("World Remove YH")]
    [SerializeField]
    private UIButtonOnClick ui_Button_Remove_YH;

    [Tooltip("World Remove XH")]
    [SerializeField]
    private UIButtonOnClick ui_Button_Remove_XH;

    [Tooltip("World Remove XY")]
    [SerializeField]
    private UIButtonOnClick ui_Button_Remove_XY;

    [Header("World View")]

    [Tooltip("World View X")]
    [SerializeField]
    private UIButtonOnClick ui_Button_View_X;

    [Tooltip("World View Y")]
    [SerializeField]
    private UIButtonOnClick ui_Button_View_Y;

    [Tooltip("World View H")]
    [SerializeField]
    private UIButtonOnClick ui_Button_View_H;

    #region Button

    #region World Add

    public UIButtonOnClick Get_Button_Add_XH()
    {
        return ui_Button_Add_XH;
    }

    public UIButtonOnClick Get_Button_Add_YH()
    {
        return ui_Button_Add_YH;
    }

    public UIButtonOnClick Get_Button_Add_XY()
    {
        return ui_Button_Add_XY;
    }

    public UIButtonOnClick Get_Button_Choice_Back()
    {
        return ui_Button_Add_XY;
    }

    #endregion

    #region World Remove

    public UIButtonOnClick Get_Button_Remove_XH()
    {
        return ui_Button_Remove_XH;
    }

    public UIButtonOnClick Get_Button_Remove_YH()
    {
        return ui_Button_Remove_YH;
    }

    public UIButtonOnClick Get_Button_Remove_XY()
    {
        return ui_Button_Remove_XY;
    }

    #endregion

    #region World View

    public UIButtonOnClick Get_Button_View_X()
    {
        return ui_Button_View_X;
    }

    public UIButtonOnClick Get_Button_View_Y()
    {
        return ui_Button_View_Y;
    }

    public UIButtonOnClick Get_Button_View_H()
    {
        return ui_Button_View_H;
    }

    #endregion

    #endregion
}
