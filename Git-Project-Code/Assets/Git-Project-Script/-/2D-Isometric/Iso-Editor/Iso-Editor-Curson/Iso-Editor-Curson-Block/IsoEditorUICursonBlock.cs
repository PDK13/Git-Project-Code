using UnityEngine;
using UnityEngine.UI;

public class IsoEditorUICursonBlock : MonoBehaviour
{
    [Header("UI")]

    [Tooltip("Block Image Renderer")]
    [SerializeField]
    private Image i_Block_Renderer;

    [Tooltip("Text Block Page-Block")]
    [SerializeField]
    private Text t_Block_Page;

    [Tooltip("Text Block Block")]
    [SerializeField]
    private Text t_Block_Index;

    [Header("Button")]

    [Header("Primary")]

    [Tooltip("Curson Block Edit")]
    [SerializeField]
    private UIButtonOnClick ui_Button_Edit;

    [Tooltip("Curson Block Remove")]
    [SerializeField]
    private UIButtonOnClick ui_Button_Remove;

    [Tooltip("Curson Block Check")]
    [SerializeField]
    private UIButtonOnClick ui_Button_Check;

    [Header("Block")]

    [Tooltip("Curson Block Block Next")]
    [SerializeField]
    private UIButtonOnClick ui_Button_Index_Next;

    [Tooltip("Curson Block Block Back")]
    [SerializeField]
    private UIButtonOnClick ui_Button_Index_Back;

    [Header("Page")]

    [Tooltip("Curson Block Page Next")]
    [SerializeField]
    private UIButtonOnClick ui_Button_Page_Next;

    [Tooltip("Curson Block Page Back")]
    [SerializeField]
    private UIButtonOnClick ui_Button_Page_Back;

    #region UI

    /// <summary>
    /// Set Canvas Block
    /// </summary>
    /// <param name="s_LayerName"></param>
    /// <param name="i_IndexList"></param>
    /// <param name="i_ListCount"></param>
    /// <param name="sp_SpriteRenderer"></param>
    public void Set_Canvas_Block(string s_LayerName, int i_IndexList, int i_ListCount, SpriteRenderer sp_SpriteRenderer)
    {
        t_Block_Page.text = s_LayerName;
        t_Block_Index.text = (i_IndexList + 1).ToString() + "/" + i_ListCount.ToString();
        i_Block_Renderer.sprite = sp_SpriteRenderer.sprite;
    }

    /// <summary>
    /// Set Canvas Block when None
    /// </summary>
    /// <param name="s_LayerName"></param>
    public void Set_Canvas_Block(string s_LayerName)
    {
        t_Block_Page.text = s_LayerName;
        t_Block_Index.text = "[ NONE ]";
        i_Block_Renderer.sprite = null;
    }

    public Image GetImage_Block_Renderer()
    {
        return i_Block_Renderer;
    }

    #endregion

    #region Button

    //Block

    public UIButtonOnClick GetButton_Edit()
    {
        return ui_Button_Edit;
    }

    public UIButtonOnClick GetButton_Remove()
    {
        return ui_Button_Remove;
    }

    public UIButtonOnClick GetButton_Check()
    {
        return ui_Button_Check;
    }

    //Index

    public UIButtonOnClick GetButton_Index_Next()
    {
        return ui_Button_Index_Next;
    }

    public UIButtonOnClick GetButton_Index_Back()
    {
        return ui_Button_Index_Back;
    }

    //Page

    public UIButtonOnClick GetButton_Page_Next()
    {
        return ui_Button_Page_Next;
    }

    public UIButtonOnClick GetButton_Page_Back()
    {
        return ui_Button_Page_Back;
    }


    #endregion
}
