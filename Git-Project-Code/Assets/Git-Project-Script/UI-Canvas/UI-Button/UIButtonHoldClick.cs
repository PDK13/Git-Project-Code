#if UNITY_EDITOR
using UnityEditor.Events;
#endif
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButtonHoldClick : MonoBehaviour,
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

    [SerializeField]
    private Color c_Color_Hold = Color.yellow;

    [Tooltip("Color Active Button")]
    [SerializeField]
    private Color c_Color_Lock = Color.red;

    [Header("Event After Hold Time")]

    [Tooltip("Event After Hold Time")]
    [SerializeField]
    private bool m_HoldTimeIsAllow = false;

    [Tooltip("Hold Time")]
    [SerializeField]
    private float m_HoldTime = 1f;

    [Tooltip("Time Remain in Hold Pressed")]
    private float m_HoldTime_Remain;

    [Header("Event")]

    [Tooltip("Keyboard Button")]
    [SerializeField]
    private KeyCode k_Button_Keyboard = KeyCode.None;

    [Tooltip("Button Lock Status")]
    [SerializeField]
    private bool m_Button_Lock = false;

    [Tooltip("Unity Hold-State Event Handle")]
    [Space]
    [SerializeField]
    private UnityEvent Event_HoldState;

    [Tooltip("Unity Pointer Down Event Handle")]
    [Space]
    [SerializeField]
    private UnityEvent Event_PointerDown;

    [Tooltip("Unity Pointer Up Event Handle")]
    [Space]
    [SerializeField]
    private UnityEvent Event_PointerUp;

    [Tooltip("Unity Pointer Enter Event Handle")]
    [Space]
    [SerializeField]
    private UnityEvent Event_PointerEnter;

    [Tooltip("Unity Pointer Exit Event Handle")]
    [Space]
    [SerializeField]
    private UnityEvent Event_PointerExit;

    [Tooltip("Button Hold Status")]
    private bool m_Button_Hold;

    [Tooltip("Button Ready Status")]
    private bool m_Button_Ready = false;

    private void Update()
    {
        SetEvent_Keyboard();

        SetEvent_Active();

        SetButton_Color();
    }

    private void SetEvent_Keyboard()
    {
        if (m_Button_Lock)
        {
            return;
        }

        if (Input.GetKeyDown(k_Button_Keyboard))
        {
            SetEvent_PointerDown();
        }

        if (Input.GetKeyUp(k_Button_Keyboard))
        {
            SetEvent_PointerUp();
        }
    }

    private void SetEvent_Active()
    {
        if (m_Button_Lock)
        {
            return;
        }

        if (m_Button_Hold)
        //If Hold Pressed >> Do...
        {
            if (m_HoldTimeIsAllow)
            //If Need Time to do Event >> Do...
            {
                m_HoldTime_Remain -= Time.deltaTime;

                if (m_HoldTime_Remain < 0)
                //If out of Time Hold >> Do Event
                {
                    SetEvent_Invoke_HoldState();
                }
            }
            else
            //If NOT Need Time to do Event >> Do Event Right away
            {
                SetEvent_Invoke_HoldState();
            }
        }
    }

    #region Set Event

    #region Set Event Add

#if UNITY_EDITOR

    /// <summary>
    /// This just work in Editor and not work in Build
    /// </summary>
    /// <param name="ua_Methode"></param>
    public void SetEvent_Add_HoldState(UnityAction ua_Methode)
    {
        UnityEventTools.AddPersistentListener(Event_HoldState, ua_Methode);
    }

    /// <summary>
    /// This just work in Editor and not work in Build
    /// </summary>
    /// <param name="ua_Methode"></param>
    public void SetEvent_Add_PointerEnter(UnityAction ua_Methode)
    {
        UnityEventTools.AddPersistentListener(Event_PointerEnter, ua_Methode);
    }

    /// <summary>
    /// This just work in Editor and not work in Build
    /// </summary>
    /// <param name="ua_Methode"></param>
    public void SetEvent_Add_PointerExit(UnityAction ua_Methode)
    {
        UnityEventTools.AddPersistentListener(Event_PointerExit, ua_Methode);
    }

    /// <summary>
    /// This just work in Editor and not work in Build
    /// </summary>
    /// <param name="ua_Methode"></param>
    public void SetEvent_Add_PointerDown(UnityAction ua_Methode)
    {
        UnityEventTools.AddPersistentListener(Event_PointerDown, ua_Methode);
    }

    /// <summary>
    /// This just work in Editor and not work in Build
    /// </summary>
    /// <param name="ua_Methode"></param>
    public void SetEvent_Add_PointerUp(UnityAction ua_Methode)
    {
        UnityEventTools.AddPersistentListener(Event_PointerUp, ua_Methode);
    }

#endif

    #endregion

    #region Set Event Invoke

    private void SetEvent_Invoke_HoldState()
    {
        if (Event_HoldState != null)
        {
            Event_HoldState.Invoke();
        }
    }

    private void SetEvent_Invoke_PointerEnter()
    {
        if (Event_PointerEnter != null)
        {
            Event_PointerEnter.Invoke();
        }
    }

    private void SetEvent_Invoke_PointerExit()
    {
        if (Event_PointerExit != null)
        {
            Event_PointerExit.Invoke();
        }
    }

    private void SetEvent_Invoke_PointerDown()
    {
        if (Event_PointerDown != null)
        {
            Event_PointerDown.Invoke();
        }
    }

    private void SetEvent_Invoke_PointerUp()
    {
        if (Event_PointerUp != null)
        {
            Event_PointerUp.Invoke();
        }
    }

    #endregion

    #endregion

    #region Set Event Button

    private void SetEvent_PointerEnter()
    {
        if (m_Button_Lock)
        {
            return;
        }

        m_Button_Ready = true;

        SetEvent_Invoke_PointerEnter();
    }

    private void SetEvent_PointerExit()
    {
        if (m_Button_Lock)
        {
            return;
        }

        m_Button_Ready = false;

        SetEvent_Invoke_PointerExit();
    }

    private void SetEvent_PointerDown()
    {
        if (m_Button_Lock)
        {
            return;
        }

        m_Button_Hold = true;

        SetEvent_Invoke_PointerDown();
    }

    private void SetEvent_PointerUp()
    {
        if (m_Button_Lock)
        {
            return;
        }

        m_Button_Hold = false;
        m_HoldTime_Remain = m_HoldTime;

        SetEvent_Invoke_PointerUp();
    }

    #endregion

    #region On Event Handle

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        SetEvent_PointerEnter();
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        SetEvent_PointerExit();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SetEvent_PointerDown();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        SetEvent_PointerUp();
    }

    #endregion

    #region Button Keyboard

    public void SetButton_Keyboard(KeyCode k_Button_Keyboard)
    {
        this.k_Button_Keyboard = k_Button_Keyboard;
    }

    public KeyCode GetButton_Keyboard()
    {
        return k_Button_Keyboard;
    }

    #endregion

    #region Button Status

    #region Button Status Set

    public void SetButton_Lock_Chance()
    {
        m_Button_Lock = !m_Button_Lock;
    }

    public void SetButton_Lock(bool m_Lock_State)
    {
        m_Button_Lock = m_Lock_State;
    }

    public void SetButton_Lock_True()
    {
        SetButton_Lock(true);
    }

    public void SetButton_Lock_False()
    {
        SetButton_Lock(false);
    }

    #endregion

    #region Button Status Get

    public bool GetButton_Hold()
    {
        return m_Button_Hold;
    }

    public float GetHoldTime_Remain()
    {
        return m_HoldTime_Remain;

    }

    public bool GetButton_Ready()
    {
        return m_Button_Ready;
    }

    public bool GetButton_Lock()
    {
        return m_Button_Lock;
    }

    #endregion

    #endregion

    #region Color Button

    #region Color Button Event

    private void SetButton_Color()
    {
        if (m_Button_Lock)
        {
            return;
        }

        if (m_Button_Hold)
        //If Hold Pressed >> Do...
        {
            if (m_HoldTimeIsAllow)
            //If Need Time to do Event >> Do...
            {
                if (m_HoldTime_Remain < 0)
                //If out of Time Hold >> Do Event
                {
                    SetButton_Color(c_Color_Hold);
                }
            }
            else
            //If NOT Need Time to do Event >> Do Event Right away
            {
                SetButton_Color(c_Color_Hold);
            }
        }
        else
        if (m_Button_Ready)
        //If Ready Pressed >> Do...
        {
            SetButton_Color(c_Color_Ready);
        }
        else
        //If Not Hold Pressed >> Do...
        {
            SetButton_Color(c_Color_Normal);
        }
    }

    private void SetButton_Color(Color c_Color)
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
            gameObject.AddComponent<Image>();
            GetComponent<SpriteRenderer>().color = c_Color;
        }
    }

    #endregion

    #region Color Button Set

    public void SetButton_Color_Normal(Color c_Color_Normal)
    {
        this.c_Color_Normal = c_Color_Normal;
    }

    public void SetButton_Color_Ready(Color c_Color_Ready)
    {
        this.c_Color_Ready = c_Color_Ready;
    }

    public void SetButton_Color_Hold(Color c_Color_Hold)
    {
        this.c_Color_Hold = c_Color_Hold;
    }

    public void SetButton_Color_Lock(Color c_Color_Lock)
    {
        this.c_Color_Lock = c_Color_Lock;
    }

    #endregion

    #region Color Button Get

    public Color GetButton_Color_Normal()
    {
        return c_Color_Normal;
    }

    public Color GetButton_Color_Ready()
    {
        return c_Color_Ready;
    }

    public Color GetButton_Color_Holdl()
    {
        return c_Color_Hold;
    }

    public Color GetButton_Color_Lock()
    {
        return c_Color_Lock;
    }

    #endregion

    #region Color Button Primary

    public Color GetColor_Normal_Primary()
    {
        return Color.white;
    }

    public Color GetColor_Ready_Primary()
    {
        return Color.gray;
    }

    public Color GetColor_Hold_Primary()
    {
        return Color.yellow;
    }

    public Color GetColor_Lock_Primary()
    {
        return Color.red;
    }

    #endregion

    #endregion
}