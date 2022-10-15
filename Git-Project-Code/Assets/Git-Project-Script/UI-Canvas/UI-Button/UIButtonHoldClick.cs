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

    [SerializeField]
    private Color m_ColorNormal = Color.white;

    [SerializeField]
    private Color m_ColorReady = Color.gray;
    
    [SerializeField]
    private Color m_ColorHold = Color.yellow;

    [SerializeField]
    private Color m_ColorLock = Color.red;

    [Header("Event After Hold Time")]

    [SerializeField]
    private bool m_HoldTimeActive = false;

    [SerializeField]
    private float m_HoldTime = 1f;

    private float m_HoldTimeRemain;

    [Header("Event")]

    [SerializeField]
    private KeyCode m_ButtonKey = KeyCode.None;

    [SerializeField]
    private bool m_ButtonLock = false;

    [Space]
    [SerializeField]
    private UnityEvent EventHoldState;

    [Space]
    [SerializeField]
    private UnityEvent EventPointerDown;

    [Space]
    [SerializeField]
    private UnityEvent EventPointerUp;

    [Space]
    [SerializeField]
    private UnityEvent EventPointerEnter;

    [Space]
    [SerializeField]
    private UnityEvent EventPointerExit;

    private bool m_ButtonHold;

    private bool m_ButtonReady = false;

    private void Update()
    {
        SetEventKeyboard();

        SetEventActive();

        SetButtonColor();
    }

    private void SetEventKeyboard()
    {
        if (m_ButtonLock)
        {
            return;
        }

        if (Input.GetKeyDown(m_ButtonKey))
        {
            SetEventPointerDown();
        }

        if (Input.GetKeyUp(m_ButtonKey))
        {
            SetEventPointerUp();
        }
    }

    private void SetEventActive()
    {
        if (m_ButtonLock)
        {
            return;
        }

        if (m_ButtonHold)
        //If Hold Pressed >> Do...
        {
            if (m_HoldTimeActive)
            //If Need Time to do Event >> Do...
            {
                m_HoldTimeRemain -= Time.deltaTime;

                if (m_HoldTimeRemain < 0)
                //If out of Time Hold >> Do Event
                {
                    SetEventInvokeHoldState();
                }
            }
            else
            //If NOT Need Time to do Event >> Do Event R away
            {
                SetEventInvokeHoldState();
            }
        }
    }

    #region Set Event

    #region Set Event Add

#if UNITY_EDITOR

    /// <summary>
    /// This just work in Editor and not work in Build
    /// </summary>
    /// <param name="m_Methode"></param>
    public void SetEventAddHoldState(UnityAction m_Methode)
    {
        UnityEventTools.AddPersistentListener(EventHoldState, m_Methode);
    }

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
        UnityEventTools.AddPersistentListener(EventPointerDown, m_Methode);
    }

    /// <summary>
    /// This just work in Editor and not work in Build
    /// </summary>
    /// <param name="m_Methode"></param>
    public void SetEventAddPointerU(UnityAction m_Methode)
    {
        UnityEventTools.AddPersistentListener(EventPointerUp, m_Methode);
    }

#endif

    #endregion

    #region Set Event Invoke

    private void SetEventInvokeHoldState()
    {
        if (EventHoldState != null)
        {
            EventHoldState.Invoke();
        }
    }

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

    private void SetEventInvokePointerDown()
    {
        if (EventPointerDown != null)
        {
            EventPointerDown.Invoke();
        }
    }

    private void SetEventInvokePointerUp()
    {
        if (EventPointerUp != null)
        {
            EventPointerUp.Invoke();
        }
    }

    #endregion

    #endregion

    #region Set Event Button

    private void SetEventPointerEnter()
    {
        if (m_ButtonLock)
        {
            return;
        }

        if (Application.platform == RuntimePlatform.Android)
        {
            SetEventPointerDown();
        }
        else
        {
            m_ButtonReady = true;

            SetEventInvokePointerEnter();
        }
    }

    private void SetEventPointerExit()
    {
        if (m_ButtonLock)
        {
            return;
        }

        if (Application.platform == RuntimePlatform.Android)
        {
            SetEventPointerUp();
        }
        else
        {
            m_ButtonReady = false;

            SetEventInvokePointerExit();
        }
    }

    private void SetEventPointerDown()
    {
        if (m_ButtonLock)
        {
            return;
        }

        m_ButtonHold = true;

        SetEventInvokePointerDown();
    }

    private void SetEventPointerUp()
    {
        if (m_ButtonLock)
        {
            return;
        }

        m_ButtonHold = false;
        m_HoldTimeRemain = m_HoldTime;

        SetEventInvokePointerUp();
    }

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
        SetEventPointerDown();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        SetEventPointerUp();
    }

    #endregion

    #region Button Keyboard

    public void SetButtonKey(KeyCode m_ButtonKey)
    {
        this.m_ButtonKey = m_ButtonKey;
    }

    public KeyCode GetButtonKey()
    {
        return m_ButtonKey;
    }

    #endregion

    #region Button Status

    #region Button Status Set

    public void SetButtonLockChance()
    {
        m_ButtonLock = !m_ButtonLock;
    }

    public void SetButtonLock(bool m_LockState)
    {
        m_ButtonLock = m_LockState;
    }

    public void SetButtonLockKeyTrue()
    {
        SetButtonLock(true);
    }

    public void SetButtonLockKeyFalse()
    {
        SetButtonLock(false);
    }

    #endregion

    #region Button Status Get

    public bool GetButtonHold()
    {
        return m_ButtonHold;
    }

    public float GetHoldTimeRemain()
    {
        return m_HoldTimeRemain;

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

    private void SetButtonColor()
    {
        if (m_ButtonLock)
        {
            return;
        }

        if (m_ButtonHold)
        //If Hold Pressed >> Do...
        {
            if (m_HoldTimeActive)
            //If Need Time to do Event >> Do...
            {
                if (m_HoldTimeRemain < 0)
                //If out of Time Hold >> Do Event
                {
                    SetButtonColor(m_ColorHold);
                }
            }
            else
            //If NOT Need Time to do Event >> Do Event R away
            {
                SetButtonColor(m_ColorHold);
            }
        }
        else
        if (m_ButtonReady)
        //If Ready Pressed >> Do...
        {
            SetButtonColor(m_ColorReady);
        }
        else
        //If Not Hold Pressed >> Do...
        {
            SetButtonColor(m_ColorNormal);
        }
    }

    private void SetButtonColor(Color m_Color)
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
            GetComponent<SpriteRenderer>().color = m_Color;
        }
    }

    #endregion

    #region Color Button Set

    public void SetButtonColorNormal(Color m_ColorNormal)
    {
        this.m_ColorNormal = m_ColorNormal;
    }

    public void SetButtonColorReady(Color m_ColorReady)
    {
        this.m_ColorReady = m_ColorReady;
    }

    public void SetButtonColorHold(Color m_ColorHold)
    {
        this.m_ColorHold = m_ColorHold;
    }

    public void SetButtonColorLock(Color m_ColorLock)
    {
        this.m_ColorLock = m_ColorLock;
    }

    #endregion

    #region Color Button Get

    public Color GetButtonColorNormal()
    {
        return m_ColorNormal;
    }

    public Color GetButtonColorReady()
    {
        return m_ColorReady;
    }

    public Color GetButtonColorHold()
    {
        return m_ColorHold;
    }

    public Color GetButtonColorLock()
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

    public Color GetColorLockKeyPrimary()
    {
        return Color.red;
    }

    #endregion

    #endregion
}