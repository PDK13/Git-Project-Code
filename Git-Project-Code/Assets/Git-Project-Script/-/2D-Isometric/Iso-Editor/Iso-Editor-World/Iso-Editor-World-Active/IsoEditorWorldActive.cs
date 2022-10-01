using UnityEngine;
using UnityEngine.UI;

public class IsoEditorWorldActive : MonoBehaviour
{
    [Header("UI Component")]

    [Tooltip("Button World Active Chance")]
    [SerializeField]
    private UIButtonOnClick ui_Button_World_Active;

    [Tooltip("Button World Active Chance")]
    [SerializeField]
    private Text t_World_Active;

    private IsoWorld iso_World;

    private IsoEditorWorld iso_Editor_World;

    private void Awake()
    {
        iso_World = GameObject.FindGameObjectWithTag("IsoWorldManager").GetComponent<IsoWorld>();

        iso_Editor_World = GetComponent<IsoEditorWorld>();

        //ui_Button_World_Active.Set_Event_Add_PointerDown(Set_WorldIsActive_Chance);

        ui_Button_World_Active.Set_Button_Active(iso_World.GetWorldIsActive());

        Set_UI_Button();
    }

    public void Set_WorldIsActive_Chance()
    {
        iso_World.Set_WorldIsActive_Chance();

        Set_UI_Button();

        iso_Editor_World.Set_Reset_Block_Move();
    }

    private void Set_UI_Button()
    {
        if (iso_World.GetWorldIsActive())
        {
            t_World_Active.text = "WORLD ACTIVE: RUN!";
        }
        else
        {
            t_World_Active.text = "WORLD ACTIVE: NONE!";
        }
    }
}
