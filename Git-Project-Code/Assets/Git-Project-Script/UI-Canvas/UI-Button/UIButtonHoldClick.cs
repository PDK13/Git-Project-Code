#if UNITY_EDITOR
using UnityEditor.Events;
#endif
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButtonHoldClick : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [Header("Color Button")]

    [Tooltip("Color Normal Button")]
    [SerializeField]
    private Color c_Color_Normal = Color.white;

    [Tooltip("Color Ready Button")]
    [SerializeField]
    private Color c_ColorReady = Color.gray;

    [SerializeField]
    private Color c_ColorHold = Color.yellow;

    [Tooltip("Color Active Button")]
    [SerializeField]
    private Color c_ColorLock = Color.red;

    [Header("Event After Hold Time")]

    [Tooltip("Event After Hold Time")]
    [SerializeField]
    private bool m_AllowHoldTime= false;

    [Tooltip("Hold Time")]
    [SerializeField]
    private float m_HoldTime = 1f;

    [Tooltip("Time Remain in Hold Pressed")]
    private float m_HoldTimeRemain;

    [Header("Event")]

    [Tooltip("Keyboard Button")]
    [SerializeField]
    private KeyCode m_KeyButton_Keyboard = KeyCode.None;

    [Tooltip("Button Lock Status")]
    [SerializeField]
    private bool m_AllowButtonLock = false;

    [Tooltip("Unity Hold-State Event Handle")]
    [Space]
    [SerializeField]
    private UnityEvent EventHoldState;

    [Tooltip("Unity Pointer D Event Handle")]
    [Space]
    [SerializeField]
    private UnityEvent Event_PointerD;

    [Tooltip("Unity Pointer U Event Handle")]
    [Space]
    [SerializeField]
    private UnityEvent Event_PointerU;

    [Tooltip("Unity Pointer Enter Event Handle")]
    [Space]
    [SerializeField]
    private UnityEvent Event_PointerEnter;

    [Tooltip("Unity Pointer Exit Event Handle")]
    [Space]
    [SerializeField]
    private UnityEvent Event_PointerExit;

    [Tooltip("Button Hold Status")]
    private bool m_AllowButtonHold;

    [Tooltip("Button Ready Status")]
    private bool m_AllowButtonReady = false;

    private void Update()
    {
        SetEvent_Keyboard();

        SetEventActive();

        SetButton_Color();
    }

    private void SetEvent_Keyboard()
    {
        if (m_AllowButtonLock)
        {
            return;
        }

        if (Input.GetKeyDown(m_KeyButton_Keyboard))
        {
            SetEvent_PointerD();
        }

        if (Input.GetKeyUp(m_KeyButton_Keyboard))
        {
            SetEvent_PointerU();
        }
    }

    private void SetEventActive()
    {
        if (m_AllowButtonLock)
        {
            return;
        }

        if (m_AllowButtonHold)
        //If Hold Pressed >> Do...
        {
            if (m_AllowHoldTime)
            //If Need Time to do Event >> Do...
            {
                m_HoldTimeRemain -= Time.deltaTime;

                if (m_HoldTimeRemain < 0)
                //If out of Time Hold >> Do Event
                {
                    SetEvent_InvokeHoldState();
                }
            }
            else
            //If NOT Need Time to do Event >> Do Event R away
            {
                SetEvent_InvokeHoldState();
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
    public void SetEvent_AddHoldState(UnityAction ua_Methode)
    {
        UnityEventTools.AddPersistentListener(EventHoldState, ua_Methode);
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
    public void SetEvent_Add_PointerD(UnityAction ua_Methode)
    {
        UnityEventTools.AddPersistentListener(Event_PointerD, ua_Methode);
    }

    /// <summary>
    /// This just work in Editor and not work in Build
    /// </summary>
    /// <param name="ua_Methode"></param>
    public void SetEvent_Add_PointerU(UnityAction ua_Methode)
    {
        UnityEventTools.AddPersistentListener(Event_PointerU, ua_Methode);
    }

#endif

    #endregion

    #region Set Event Invoke

    private void SetEvent_InvokeHoldState()
    {
        if (EventHoldState != null)
        {
            EventHoldState.Invoke();
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

    private void SetEvent_Invoke_PointerD()
    {
        if (Event_PointerD != null)
        {
            Event_PointerD.Invoke();
        }
    }

    private void SetEvent_Invoke_PointerU()
    {
        if (Event_PointerU != null)
        {
            Event_PointerU.Invoke();
        }
    }

    #endregion

    #endregion

    #region Set Event Button

    private void SetEvent_PointerEnter()
    {
        if (m_AllowButtonLock)
        {
            return;
        }

        m_AllowButtonReady = true;

        SetEvent_Invoke_PointerEnter();
    }

    private void SetEvent_PointerExit()
    {
        if (m_AllowButtonLock)
        {
            return;
        }

        m_AllowButtonReady = false;

        SetEvent_Invoke_PointerExit();
    }

    private void SetEvent_PointerD()
    {
        if (m_AllowButtonLock)
        {
            return;
        }

        m_AllowButtonHold = true;

        SetEvent_Invoke_PointerD();
    }

    private void SetEvent_PointerU()
    {
        if (m_AllowButtonLock)
        {
            return;
        }

        m_AllowButtonHold = false;
        m_HoldTimeRemain = m_HoldTime;

        SetEvent_Invoke_PointerU();
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
        SetEvent_PointerD();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        SetEvent_PointerU();
    }

    #endregion

    #region Button Keyboard

    public void SetButton_Keyboard(KeyCode m_KeyButton_Keyboard)
    {
        this.m_KeyButton_Keyboard = m_KeyButton_Keyboard;
    }

    public KeyCode GetButton_Keyboard()
    {
        return m_KeyButton_Keyboard;
    }

    #endregion

    #region Button Status

    #region Button Status Set

    public void SetButtonLockChance()
    {
        m_AllowButtonLock = !m_AllowButtonLock;
    }

    public void SetButtonLock(bool m_AllowLockState)
    {
        m_AllowButtonLock = m_AllowLockState;
    }

    public void SetButtonLocs_KeyTrue()
    {
        SetButtonLock(true);
    }

    public void SetButtonLocs_KeyFalse()
    {
        SetButtonLock(false);
    }

    #endregion

    #region Button Status Get

    public bool GetCheckButtonHold()
    {
        return m_AllowButtonHold;
    }

    public float GetHoldTimeRemain()
    {
        return m_HoldTimeRemain;

    }

    public bool GetCheckButtonReady()
    {
        return m_AllowButtonReady;
    }

    public bool GetCheckButtonLock()
    {
        return m_AllowButtonLock;
    }

    #endregion

    #endregion

    #region Color Button

    #region Color Button Event

    private void SetButton_Color()
    {
        if (m_AllowButtonLock)
        {
            return;
        }

        if (m_AllowButtonHold)
        //If Hold Pressed >> Do...
        {
            if (m_AllowHoldTime)
            //If Need Time to do Event >> Do...
            {
                if (m_HoldTimeRemain < 0)
                //If out of Time Hold >> Do Event
                {
                    SetButton_Color(c_ColorHold);
                }
            }
            else
            //If NOT Need Time to do Event >> Do Event R away
            {
                SetButton_Color(c_ColorHold);
            }
        }
        else
        if (m_AllowButtonReady)
        //If Ready Pressed >> Do...
        {
            SetButton_Color(c_ColorReady);
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

    public void SetButton_ColorReady(Color c_ColorReady)
    {
        this.c_ColorReady = c_ColorReady;
    }

    public void SetButton_ColorHold(Color c_ColorHold)
    {
        this.c_ColorHold = c_ColorHold;
    }

    public void SetButton_ColorLock(Color c_ColorLock)
    {
        this.c_ColorLock = c_ColorLock;
    }

    #endregion

    #region Color Button Get

    public Color GetButton_Color_Normal()
    {
        return c_Color_Normal;
    }

    public Color GetButton_ColorReady()
    {
        return c_ColorReady;
    }

    public Color GetButton_ColorHoldl()
    {
        return c_ColorHold;
    }

    public Color GetButton_ColorLock()
    {
        return c_ColorLock;
    }

    #endregion

    #region Color Button Primary

    public Color GetColor_Normam_Primary()
    {
        return Color.white;
    }

    public Color GetColorReady_Primary()
    {
        return Color.gray;
    }

    public Color GetColorHold_Primary()
    {
        return Color.yellow;
    }

    public Color GetColorLocs_KeyPrimary()
    {
        return Color.red;
    }

    #endregion

    #endregion
}