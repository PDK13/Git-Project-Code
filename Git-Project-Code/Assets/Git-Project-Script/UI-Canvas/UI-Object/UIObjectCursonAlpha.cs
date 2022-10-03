#if UNITY_EDITOR
using UnityEditor.Events;
#endif
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class UIObjectCursonAlpha : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler
{
    [Header("This Canvas")]

    [Header("Canvas Lock")]

    [Tooltip("Canvas Alpha when is Exit State on Start")]
    [SerializeField]
    private bool mAllowCavasLockEnter = false;

    [Tooltip("Button for Canvas Alpha Lock Chance")]
    [SerializeField]
    private UIButtonOnClick cs_ButtonLockChance;

    [Header("Canvas Alpha")]

    [Tooltip("Canvas Alpha when Pointer Enter")]
    [SerializeField]
    [Range(0f, 1f)]
    private float m_Canvas_AlphaEnter = 1f;

    [Tooltip("Canvas Alpha when Pointer Exit")]
    [SerializeField]
    [Range(0f, 1f)]
    private float m_Canvas_Alpha_Exit = 0.2f;

    [Header("Event")]

    [Tooltip("Unity Pointer Enter Event Handle")]
    [Space]
    [SerializeField]
    private UnityEvent Event_PointerEnter;

    [Tooltip("Unity Pointer Exit Event Handle")]
    [Space]
    [SerializeField]
    private UnityEvent Event_PointerExit;

    [Tooltip("Canvas Group")]
    private CanvasGroup c_CanvasGroup;

    private void Start()
    {
        if (GetComponent<CanvasGroup>() == null)
        {
            gameObject.AddComponent<CanvasGroup>();
        }

        c_CanvasGroup = GetComponent<CanvasGroup>();

        if (mAllowCavasLockEnter)
        {
            c_CanvasGroup.alpha = m_Canvas_AlphaEnter;
        }
        else
        {
            c_CanvasGroup.alpha = m_Canvas_Alpha_Exit;
        }

        if (cs_ButtonLockChance != null)
        {
            //cs_ButtonLockChance.SetEvent_Add_PointerD(SetUICanvasLockEnterChance);
            cs_ButtonLockChance.SetButtonActive(mAllowCavasLockEnter);
        }
    }

    #region Set Event

    #region Set Event Add

#if UNITY_EDITOR

    public void SetEvent_Add_PointerEnter(UnityAction ua_Methode)
    {
        UnityEventTools.AddPersistentListener(Event_PointerEnter, ua_Methode);
    }

    public void SetEvent_Add_PointerExit(UnityAction ua_Methode)
    {
        UnityEventTools.AddPersistentListener(Event_PointerExit, ua_Methode);
    }

#endif

    #endregion

    #region Set Event Invoke

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

    #endregion

    #region Set Event Button

    private void SetEvent_PointerEnter()
    {
        c_CanvasGroup.alpha = m_Canvas_AlphaEnter;

        SetEvent_Invoke_PointerEnter();
    }

    private void SetEvent_PointerExit()
    {
        if (mAllowCavasLockEnter)
        {
            c_CanvasGroup.alpha = m_Canvas_AlphaEnter;

            return;
        }

        if (GetComponent<UIObjectDragDrop>() != null)
        {
            if (GetComponent<UIObjectDragDrop>().GetCheckUICanvasDrag())
            {
                return;
            }
        }

        c_CanvasGroup.alpha = m_Canvas_Alpha_Exit;

        SetEvent_Invoke_PointerExit();
    }

    #endregion

    #endregion

    #region On Event Handle

    public void OnPointerEnter(PointerEventData eventData)
    {
        SetEvent_PointerEnter();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SetEvent_PointerExit();
    }

    #endregion

    #region UI Canvas Lock

    public void SetUICanvasLockEnter(bool mAllowCavasLockEnter)
    {
        this.mAllowCavasLockEnter = mAllowCavasLockEnter;
    }

    public void SetUICanvasLockEnterChance()
    {
        SetUICanvasLockEnter(!GetCheckUICanvasLockEnter());
    }

    public bool GetCheckUICanvasLockEnter()
    {
        return mAllowCavasLockEnter;
    }

    #endregion

    #region UI Canvas Alpha

    #region UI Canvas Alpha Enter

    /// <summary>
    /// Set Alpha Canvas when in Curson Enter State (Alpha value from 0 to 1)
    /// </summary>
    /// <param name="m_Canvas_AlphaEnter"></param>
    public void SetUICanvas_AlphaEnter(float m_Canvas_AlphaEnter)
    {
        if (m_Canvas_AlphaEnter < 0)
        {
            m_Canvas_AlphaEnter = 0;
        }
        else
        if (m_Canvas_AlphaEnter > 1)
        {
            m_Canvas_AlphaEnter = 1;
        }
        else
        {
            this.m_Canvas_AlphaEnter = m_Canvas_AlphaEnter;
        }
    }

    /// <summary>
    /// Get Alpha Canvas when in Curson Enter State
    /// </summary>
    /// <returns></returns>
    public float GetUICanvas_AlphaEnter()
    {
        return m_Canvas_AlphaEnter;
    }

    #endregion

    #region UI Canvas Alpha Exit

    /// <summary>
    /// Set Alpha Canvas when in Curson Exit State (Alpha value from 0 to 1)
    /// </summary>
    /// <param name="mCanvas_Alpha_Exit"></param>
    public void SetUICanvas_Alpha_Exit(float m_Canvas_Alpha_Exit)
    {
        if (m_Canvas_Alpha_Exit < 0)
        {
            m_Canvas_Alpha_Exit = 0;
        }
        else
        if (m_Canvas_Alpha_Exit > 1)
        {
            m_Canvas_Alpha_Exit = 1;
        }
        else
        {
            this.m_Canvas_Alpha_Exit = m_Canvas_Alpha_Exit;
        }
    }

    /// <summary>
    /// Get Alpha Canvas when in Curson Exit State
    /// </summary>
    /// <returns></returns>
    public float GetUICanvas_Alpha_Exit()
    {
        return m_Canvas_Alpha_Exit;
    }

    #endregion

    #endregion
}
