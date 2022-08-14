#if UNITY_EDITOR
using UnityEditor.Events;
#endif
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButtonOnClick : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler,
    IPointerDownHandler, IPointerUpHandler
{
    [Header("Color Button")]

    [Tooltip("Color Normal Button")]
    [SerializeField]
    private Color c_Color_Normal = Color.white;

    [Tooltip("Color Ready Button")]
    [SerializeField]
    private Color c_Color_Ready = Color.gray;

    [Tooltip("Color Hold Button")]
    [SerializeField]
    private Color c_Color_Hold = Color.yellow;

    [Tooltip("Color Active Button")]
    [SerializeField]
    private Color c_Color_Active = Color.green;

    [Tooltip("Color Active Button")]
    [SerializeField]
    private Color c_Color_Lock = Color.red;

    [Header("Event")]

    [Tooltip("Keyboard Button")]
    [SerializeField]
    private KeyCode k_Button_Keyboard = KeyCode.None;

    [Tooltip("Button Lock Status")]
    [SerializeField]
    private bool b_Button_Lock = false;

    [Tooltip("Button Active Status")]
    [SerializeField]
    private bool b_Button_Active = false;

    [Tooltip("Unity Pointer Enter Event Handle")]
    [Space]
    [SerializeField]
    private UnityEvent Event_PointerEnter;

    [Tooltip("Unity Pointer Exit Event Handle")]
    [Space]
    [SerializeField]
    private UnityEvent Event_PointerExit;

    [Tooltip("Unity Pointer Down Event Handle")]
    [Space]
    [SerializeField]
    private UnityEvent Event_PointerDown;

    [Tooltip("Unity Pointer Up Event Handle")]
    [Space]
    [SerializeField]
    private UnityEvent Event_PointerUp;

    [Tooltip("Unity Active-State (Same as Hold-State) Event Handle")]
    [Space]
    [SerializeField]
    private UnityEvent Event_ActiveState;

    [Tooltip("Button Hold Status")]
    private bool b_Button_Hold = false;

    [Tooltip("Button Ready Status")]
    private bool b_Button_Ready = false;

    private void Update()
    {
        Set_Event_Keyboard();

        Set_Event_Active();

        Set_Button_Color();
    }

    private void Set_Event_Keyboard()
    {
        if (b_Button_Lock)
        {
            return;
        }

        if (Input.GetKeyDown(k_Button_Keyboard))
        {
            Set_Event_PointerDown();
        }

        if (Input.GetKeyUp(k_Button_Keyboard))
        {
            Set_Event_PointerUp();
        }
    }

    private void Set_Event_Active()
    {
        if (b_Button_Lock)
        {
            return;
        }

        if (b_Button_Active)
        //If Active Pressed >> Do...
        {
            Set_Event_Invoke_ActiveState();
        }
    }

    #region Set Event

    #region Set Event Add

#if UNITY_EDITOR

    /// <summary>
    /// This just work in Editor and not work in Build
    /// </summary>
    /// <param name="ua_Methode"></param>
    public void Set_Event_Add_PointerEnter(UnityAction ua_Methode)
    {
        UnityEventTools.AddPersistentListener(Event_PointerEnter, ua_Methode);
    }

    /// <summary>
    /// This just work in Editor and not work in Build
    /// </summary>
    /// <param name="ua_Methode"></param>
    public void Set_Event_Add_PointerExit(UnityAction ua_Methode)
    {
        UnityEventTools.AddPersistentListener(Event_PointerExit, ua_Methode);
    }

    /// <summary>
    /// This just work in Editor and not work in Build
    /// </summary>
    /// <param name="ua_Methode"></param>
    public void Set_Event_Add_PointerDown(UnityAction ua_Methode)
    {
        UnityEventTools.AddPersistentListener(Event_PointerDown, ua_Methode);
    }

    /// <summary>
    /// This just work in Editor and not work in Build
    /// </summary>
    /// <param name="ua_Methode"></param>
    public void Set_Event_Add_PointerUp(UnityAction ua_Methode)
    {
        UnityEventTools.AddPersistentListener(Event_PointerUp, ua_Methode);
    }

    /// <summary>
    /// This just work in Editor and not work in Build
    /// </summary>
    /// <param name="ua_Methode"></param>
    public void Set_Event_Add_ActiveState(UnityAction ua_Methode)
    {
        UnityEventTools.AddPersistentListener(Event_ActiveState, ua_Methode);
    }

#endif

    #endregion

    #region Set Event Invoke

    private void Set_Event_Invoke_PointerEnter()
    {
        if (Event_PointerEnter != null)
            Event_PointerEnter.Invoke();
    }

    private void Set_Event_Invoke_PointerExit()
    {
        if (Event_PointerExit != null)
            Event_PointerExit.Invoke();
    }

    private void Set_Event_Invoke_PointerDown()
    {
        if (Event_PointerDown != null)
            Event_PointerDown.Invoke();
    }

    private void Set_Event_Invoke_PointerUp()
    {
        if (Event_PointerUp != null)
            Event_PointerUp.Invoke();
    }

    private void Set_Event_Invoke_ActiveState()
    {
        if (Event_ActiveState != null)
            Event_ActiveState.Invoke();
    }

    #endregion

    #region Set Event Button

    private void Set_Event_PointerEnter()
    {
        if (b_Button_Lock)
        {
            return;
        }

        b_Button_Ready = true;

        Set_Event_Invoke_PointerEnter();
    }

    private void Set_Event_PointerExit()
    {
        if (b_Button_Lock)
        {
            return;
        }

        b_Button_Ready = false;

        Set_Event_Invoke_PointerExit();
    }

    private void Set_Event_PointerDown()
    {
        if (b_Button_Lock)
        {
            return;
        }

        b_Button_Active = !b_Button_Active;

        b_Button_Hold = true;

        Set_Event_Invoke_PointerDown();
    }

    private void Set_Event_PointerUp()
    {
        if (b_Button_Lock)
        {
            return;
        }

        b_Button_Hold = false;

        Set_Event_Invoke_PointerUp();
    }

    #endregion

    #endregion

    #region On Event Handle

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        Set_Event_PointerEnter();
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        Set_Event_PointerExit();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Set_Event_PointerDown();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Set_Event_PointerUp();
    }

    #endregion

    #region Button Keyboard

    public void Set_Button_Keycode(KeyCode k_Button_Keycode)
    {
        this.k_Button_Keyboard = k_Button_Keycode;
    }

    public KeyCode Get_Button_Keycode()
    {
        return k_Button_Keyboard;
    }

    #endregion

    #region Button Status

    #region Button Status Set

    #region Button Status Active

    public void Set_Button_Active_Chance()
    {
        this.b_Button_Active = !this.b_Button_Active;
    }

    public void Set_Button_Active(bool b_Active_State)
    {
        this.b_Button_Active = b_Active_State;
    }

    public void Set_Button_Active_True()
    {
        Set_Button_Active(true);
    }

    public void Set_Button_Active_False()
    {
        Set_Button_Active(false);
    }

    #endregion

    #region Button Status Lock

    public void Set_Button_Lock_Chance()
    {
        this.b_Button_Lock = !this.b_Button_Lock;
    }

    public void Set_Button_Lock(bool b_Lock_State)
    {
        this.b_Button_Lock = b_Lock_State;
    }

    public void Set_Button_Lock_True()
    {
        Set_Button_Lock(true);
    }

    public void Set_Button_Lock_False()
    {
        Set_Button_Lock(false);
    }

    #endregion

    #endregion

    #region Button Status Get

    public bool Get_Button_Active()
    {
        return b_Button_Active;
    }

    public bool Get_Button_Hold()
    {
        return b_Button_Hold;
    }

    public bool Get_Button_Ready()
    {
        return b_Button_Ready;
    }

    public bool Get_Button_Lock()
    {
        return b_Button_Lock;
    }

    #endregion

    #endregion

    #region Color Button

    #region Color Button Event

    private void Set_Button_Color()
    {
        if (b_Button_Lock)
        //If Lock Pressed >> Do...
        {
            Set_Button_Color(c_Color_Lock);
        }
        else
        if (b_Button_Hold)
        //If Hold Pressed >> Do...
        {
            Set_Button_Color(c_Color_Hold);
        }
        else
        if (b_Button_Ready)
        //If Ready Pressed >> Do...
        {
            Set_Button_Color(c_Color_Ready);
        }
        else
        if (b_Button_Active)
        //If Active Pressed >> Do...
        {
            Set_Button_Color(c_Color_Active);
        }
        else
        //If Not Active Pressed >> Do...
        {
            Set_Button_Color(c_Color_Normal);
        }
    }

    private void Set_Button_Color(Color c_Color)
    {
        if (GetComponent<Image>() != null)
        {
            GetComponent<Image>().color = c_Color;
        }
        else
        if (GetComponent<SpriteRenderer>() != null)
        {
            GetComponent<SpriteRenderer>().color = c_Color;
        }
        else
        {
            this.gameObject.AddComponent<Image>();
            GetComponent<Image>().color = c_Color;
        }
    }

    #endregion

    #region Color Button Set

    public void Set_Button_Color_Normal(Color c_Color_Normal)
    {
        this.c_Color_Normal = c_Color_Normal;
    }

    public void Set_Button_Color_Ready(Color c_Color_Ready)
    {
        this.c_Color_Ready = c_Color_Ready;
    }

    public void Set_Button_Color_Hold(Color c_Color_Hold)
    {
        this.c_Color_Hold = c_Color_Hold;
    }

    public void Set_Button_Color_Active(Color c_Color_Active)
    {
        this.c_Color_Active = c_Color_Active;
    }

    public void Set_Button_Color_Lock(Color c_Color_Lock)
    {
        this.c_Color_Lock = c_Color_Lock;
    }

    #endregion

    #region Color Button Get

    public Color Get_Button_Color_Normal()
    {
        return c_Color_Normal;
    }

    public Color Get_Button_Color_Ready()
    {
        return c_Color_Ready;
    }

    public Color Get_Button_Color_Holdl()
    {
        return c_Color_Hold;
    }

    public Color Get_Button_Color_Active()
    {
        return c_Color_Active;
    }

    public Color Get_Button_Color_Lock()
    {
        return c_Color_Lock;
    }

    #endregion

    #region Color Button Primary

    public Color Get_Color_Normal_Primary()
    {
        return Color.white;
    }

    public Color Get_Color_Ready_Primary()
    {
        return Color.gray;
    }

    public Color Get_Color_Hold_Primary()
    {
        return Color.yellow;
    }

    public Color Get_Color_Active_Primary()
    {
        return Color.green;
    }

    public Color Get_Color_Lock_Primary()
    {
        return Color.red;
    }

    #endregion

    #endregion
}
