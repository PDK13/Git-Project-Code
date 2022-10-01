using UnityEngine;

public class IsoEditorUICursonMove : MonoBehaviour
{
    [Header("Button")]

    [Header("Move XY")]

    [Tooltip("Curson Move Up")]
    [SerializeField]
    private UIButtonOnClick ui_Button_Up;

    [Tooltip("Curson Move Down")]
    [SerializeField]
    private UIButtonOnClick ui_Button_Down;

    [Tooltip("Curson Move Left")]
    [SerializeField]
    private UIButtonOnClick ui_Button_Left;

    [Tooltip("Curson Move Right")]
    [SerializeField]
    private UIButtonOnClick ui_Button_Right;

    [Header("Move H")]

    [Tooltip("Curson Move Top")]
    [SerializeField]
    private UIButtonOnClick ui_Button_Top;

    [Tooltip("Curson Move Bot")]
    [SerializeField]
    private UIButtonOnClick ui_Button_Bot;

    #region Button

    public UIButtonOnClick GetButton_Move_Up()
    {
        return ui_Button_Up;
    }

    public UIButtonOnClick GetButton_Move_Down()
    {
        return ui_Button_Down;
    }

    public UIButtonOnClick GetButton_Move_Left()
    {
        return ui_Button_Left;
    }

    public UIButtonOnClick GetButton_Move_Right()
    {
        return ui_Button_Right;
    }

    public UIButtonOnClick GetButton_Move_Top()
    {
        return ui_Button_Top;
    }

    public UIButtonOnClick GetButton_Move_Bot()
    {
        return ui_Button_Bot;
    }

    #endregion
}
