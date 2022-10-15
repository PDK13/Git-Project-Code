#if UNITY_EDITOR
using UnityEditor.Events;
#endif
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButtonOnClick : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [Header("Color Button")]

    [Tooltip("Color Normal Button")]
    [SerializeField]
    private Color m_ColorNormal = Color.white;

    [Tooltip("Color Ready Button")]
    [SerializeField]
    private Color m_ColorReady = Color.gray;

    [Tooltip("Color Hold Button")]
    [SerializeField]
    private Color m_ColorHold = Color.yellow;

    [Tooltip("Color Active Button")]
    [SerializeField]
    private Color m_ColorActive = Color.green;

    [Tooltip("Color Active Button")]
    [SerializeField]
    private Color m_ColorLock = Color.red;

    [Header("Event")]

    [Tooltip("Keyboard Button")]
    [SerializeField]
    private KeyCode m_KeyButton_Keyboard = KeyCode.None;

    [Tooltip("Button Lock Status")]
    [SerializeField]
    private bool m_ButtonLock = false;

    [Tooltip("Button Active Status")]
    [SerializeField]
    private bool m_ButtonActive = false;

    [Tooltip("Unity Pointer Enter Event Handle")]
    [Space]
    [SerializeField]
    private UnityEvent EventPointerEnter;

    [Tooltip("Unity Pointer Exit Event Handle")]
    [Space]
    [SerializeField]
    private UnityEvent EventPointerExit;

    [Tooltip("Unity Pointer D Event Handle")]
    [Space]
    [SerializeField]
    private UnityEvent EventPointerD;

    [Tooltip("Unity Pointer U Event Handle")]
    [Space]
    [SerializeField]
    private UnityEvent EventPointerU;

    [Tooltip("Unity Active-State (Same as Hold-State) Event Handle")]
    [Space]
    [SerializeField]
    private UnityEvent EventActiveState;

    [Tooltip("Button Hold Status")]
    private bool m_ButtonHold = false;

    [Tooltip("Button Ready Status")]
    private bool m_ButtonReady = false;

    private void Update()
    {
        SetEventKeyboard();

        SetEventActive();

        SetButton_Color();
    }

    private void SetEventKeyboard()
    {
        if (m_ButtonLock)
        {
            return;
        }

        if (Input.GetKeyDown(m_KeyButton_Keyboard))
        {
            SetEventPointerD();
        }

        if (Input.GetKeyUp(m_KeyButton_Keyboard))
        {
            SetEventPointerU();
        }
    }

    private void SetEventActive()
    {
        if (m_ButtonLock)
        {
            return;
        }

        if (m_ButtonActive)
        //If Active Pressed >> Do...
        {
            SetEventInvokeActiveState();
        }
    }

    #region Set Event

    #region Set Event Add

#if UNITY_EDITOR

    /// <summary>
    /// This just work in Editor and not work in Build
    /// </summary>
    /// <param name="m_Methode"></param>
    public void SetEventAddPointerEnter(UnityAction m_Methode)
    {
        UnityEventTools.AddPersistentListener(EventPointerEnter, m_Methode);
    }

    /// <summary>
    /// This just work in Editor and not work in Build
    /// </summary>
    /// <param name="m_Methode"></param>
    public void SetEventAddPointerExit(UnityAction m_Methode)
    {
        UnityEventTools.AddPersistentListener(EventPointerExit, m_Methode);
    }

    /// <summary>
    /// This just work in Editor and not work in Build
    /// </summary>
    /// <param name="m_Methode"></param>
    public void SetEventAddPointerD(UnityAction m_Methode)
    {
        UnityEventTools.AddPersistentListener(EventPointerD, m_Methode);
    }

    /// <summary>
    /// This just work in Editor and not work in Build
    /// </summary>
    /// <param name="m_Methode"></param>
    public void SetEventAddPointerU(UnityAction m_Methode)
    {
        UnityEventTools.AddPersistentListener(EventPointerU, m_Methode);
    }

    /// <summary>
    /// This just work in Editor and not work in Build
    /// </summary>
    /// <param name="m_Methode"></param>
    public void SetEventAddActiveState(UnityAction m_Methode)
    {
        UnityEventTools.AddPersistentListener(EventActiveState, m_Methode);
    }

#endif

    #endregion

    #region Set Event Invoke

    private void SetEventInvokePointerEnter()
    {
        if (EventPointerEnter != null)
        {
            EventPointerEnter.Invoke();
        }
    }

    private void SetEventInvokePointerExit()
    {
        if (EventPointerExit != null)
        {
            EventPointerExit.Invoke();
        }
    }

    private void SetEventInvokePointerD()
    {
        if (EventPointerD != null)
        {
            EventPointerD.Invoke();
        }
    }

    private void SetEventInvokePointerU()
    {
        if (EventPointerU != null)
        {
            EventPointerU.Invoke();
        }
    }

    private void SetEventInvokeActiveState()
    {
        if (EventActiveState != null)
        {
            EventActiveState.Invoke();
        }
    }

    #endregion

    #region Set Event Button

    private void SetEventPointerEnter()
    {
        if (m_ButtonLock)
        {
            return;
        }

        m_ButtonReady = true;

        SetEventInvokePointerEnter();
    }

    private void SetEventPointerExit()
    {
        if (m_ButtonLock)
        {
            return;
        }

        m_ButtonReady = false;

        SetEventInvokePointerExit();
    }

    private void SetEventPointerD()
    {
        if (m_ButtonLock)
        {
            return;
        }

        m_ButtonActive = !m_ButtonActive;

        m_ButtonHold = true;

        SetEventInvokePointerD();
    }

    private void SetEventPointerU()
    {
        if (m_ButtonLock)
        {
            return;
        }

        m_ButtonHold = false;

        SetEventInvokePointerU();
    }

    #endregion

    #endregion

    #region On Event Handle

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        SetEventPointerEnter();
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        SetEventPointerExit();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SetEventPointerD();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        SetEventPointerU();
    }

    #endregion

    #region Button Keyboard

    public void SetButton_Keycode(KeyCode m_KeyButton_Keycode)
    {
        m_KeyButton_Keyboard = m_KeyButton_Keycode;
    }

    public KeyCode GetButton_Keycode()
    {
        return m_KeyButton_Keyboard;
    }

    #endregion

    #region Button Status

    #region Button Status Set

    #region Button Status Active

    public void SetButtonActiveChance()
    {
        m_ButtonActive = !m_ButtonActive;
    }

    public void SetButtonActive(bool m_ActiveState)
    {
        m_ButtonActive = m_ActiveState;
    }

    public void SetButtonActive_True()
    {
        SetButtonActive(true);
    }

    public void SetButtonActive_False()
    {
        SetButtonActive(false);
    }

    #endregion

    #region Button Status Lock

    public void SetButtonLockChance()
    {
        m_ButtonLock = !m_ButtonLock;
    }

    public void SetButtonLock(bool m_LockState)
    {
        m_ButtonLock = m_LockState;
    }

    public void SetButtonLom_KeyTrue()
    {
        SetButtonLock(true);
    }

    public void SetButtonLom_KeyFalse()
    {
        SetButtonLock(false);
    }

    #endregion

    #endregion

    #region Button Status Get

    public bool GetButtonActive()
    {
        return m_ButtonActive;
    }

    public bool GetButtonHold()
    {
        return m_ButtonHold;
    }

    public bool GetButtonReady()
    {
        return m_ButtonReady;
    }

    public bool GetButtonLock()
    {
        return m_ButtonLock;
    }

    #endregion

    #endregion

    #region Color Button

    #region Color Button Event

    private void SetButton_Color()
    {
        if (m_ButtonLock)
        //If Lock Pressed >> Do...
        {
            SetButton_Color(m_ColorLock);
        }
        else
        if (m_ButtonHold)
        //If Hold Pressed >> Do...
        {
            SetButton_Color(m_ColorHold);
        }
        else
        if (m_ButtonReady)
        //If Ready Pressed >> Do...
        {
            SetButton_Color(m_ColorReady);
        }
        else
        if (m_ButtonActive)
        //If Active Pressed >> Do...
        {
            SetButton_Color(m_ColorActive);
        }
        else
        //If Not Active Pressed >> Do...
        {
            SetButton_Color(m_ColorNormal);
        }
    }

    private void SetButton_Color(Color m_Color)
    {
        if (GetComponent<Image>() != null)
        {
            GetComponent<Image>().color = m_Color;
        }
        else
        if (GetComponent<SpriteRenderer>() != null)
        {
            GetComponent<SpriteRenderer>().color = m_Color;
        }
        else
        {
            gameObject.AddComponent<Image>();
            GetComponent<Image>().color = m_Color;
        }
    }

    #endregion

    #region Color Button Set

    public void SetButton_ColorNormal(Color m_ColorNormal)
    {
        this.m_ColorNormal = m_ColorNormal;
    }

    public void SetButton_ColorReady(Color m_ColorReady)
    {
        this.m_ColorReady = m_ColorReady;
    }

    public void SetButton_ColorHold(Color m_ColorHold)
    {
        this.m_ColorHold = m_ColorHold;
    }

    public void SetButton_ColorActive(Color m_ColorActive)
    {
        this.m_ColorActive = m_ColorActive;
    }

    public void SetButton_ColorLock(Color m_ColorLock)
    {
        this.m_ColorLock = m_ColorLock;
    }

    #endregion

    #region Color Button Get

    public Color GetButton_ColorNormal()
    {
        return m_ColorNormal;
    }

    public Color GetButton_ColorReady()
    {
        return m_ColorReady;
    }

    public Color GetButton_ColorHoldl()
    {
        return m_ColorHold;
    }

    public Color GetButton_ColorActive()
    {
        return m_ColorActive;
    }

    public Color GetButton_ColorLock()
    {
        return m_ColorLock;
    }

    #endregion

    #region Color Button Primary

    public Color GetColorNormamPrimary()
    {
        return Color.white;
    }

    public Color GetColorReadyPrimary()
    {
        return Color.gray;
    }

    public Color GetColorHoldPrimary()
    {
        return Color.yellow;
    }

    public Color GetColorActivePrimary()
    {
        return Color.green;
    }

    public Color GetColorLom_KeyPrimary()
    {
        return Color.red;
    }

    #endregion

    #endregion
}
