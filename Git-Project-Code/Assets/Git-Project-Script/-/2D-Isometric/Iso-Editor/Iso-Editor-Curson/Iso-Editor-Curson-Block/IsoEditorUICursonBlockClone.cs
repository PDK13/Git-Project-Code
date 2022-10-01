using UnityEngine;
using UnityEngine.UI;

public class IsoEditorUICursonBlockClone : MonoBehaviour
{
    [Tooltip("Button Fix")]
    //[SerializeField]
    private IsoEditorCursonBlock cl_Editor_Curson_Block;

    //[Header("Button  for Curson Block Clone")]

    [Tooltip("Button Curson Block")]
    //[SerializeField]
    private UIButtonOnClick ui_Button_Block_Choice;

    private void Awake()
    {
        if (cl_Editor_Curson_Block == null)
        {
            cl_Editor_Curson_Block = GameObject.FindGameObjectWithTag("IsoWorldEditor").GetComponent<IsoEditorCursonBlock>();
        }

        ui_Button_Block_Choice = GetComponent<UIButtonOnClick>();

        ui_Button_Block_Choice.Set_Button_Color_Active(ui_Button_Block_Choice.Get_Button_Color_Normal());
    }

    public void Button_Block_Choice()
    {
        cl_Editor_Curson_Block.Button_Index(GetComponent<UIVerticalClone>().Get_Clone_Index());
    }

    public void Set_Image(SpriteRenderer sp_SpriteRenderer)
    {
        if (GetComponent<Image>() != null)
        {
            GetComponent<Image>().sprite = sp_SpriteRenderer.sprite;
        }
        else
        if (GetComponent<SpriteRenderer>() != null)
        {
            GetComponent<SpriteRenderer>().sprite = sp_SpriteRenderer.sprite;
        }
        else
        {
            gameObject.AddComponent<Image>();
            GetComponent<Image>().sprite = sp_SpriteRenderer.sprite;
        }
    }
}
