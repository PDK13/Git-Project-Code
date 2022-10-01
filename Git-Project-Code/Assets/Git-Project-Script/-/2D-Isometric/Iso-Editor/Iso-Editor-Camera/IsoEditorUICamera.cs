using UnityEngine;

public class IsoEditorUICamera : MonoBehaviour
{
    [Header("Button Camera Zoom")]

    [Tooltip("Button Camera Zoom Dec")]
    [SerializeField]
    private UIButtonOnClick cl_Button_Zoom_Dec;

    [Tooltip("Button Camera Zoom Inc")]
    [SerializeField]
    private UIButtonOnClick cl_Button_Zoom_Inc;

    private void Start()
    {
        cl_Button_Zoom_Dec.Set_Button_Color_Active(cl_Button_Zoom_Dec.GetButton_Color_Normal());

        cl_Button_Zoom_Inc.Set_Button_Color_Active(cl_Button_Zoom_Inc.GetButton_Color_Normal());
    }
}
