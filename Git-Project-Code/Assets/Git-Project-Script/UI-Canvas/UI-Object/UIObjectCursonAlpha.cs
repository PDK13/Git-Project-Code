#if UNITY_EDITOR
using UnityEditor.Events;
#endif
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class UIObjectCursonAlpha : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("This Canvas")]

    [Header("Canvas Lock")]

    [Tooltip("Canvas Alpha when is Exit State on Start")]
    [SerializeField]
    private bool m_CavasLockEnter = false;

    [Tooltip("Button for Canvas Alpha Lock Chance")]
    [SerializeField]
    private UIButtonOnClick m_ButtonLockChance;

    [Header("Canvas Alpha")]

    [Tooltip("Canvas Alpha when Pointer Enter")]
    [SerializeField]
    [Range(0f, 1f)]
    private float m_CanvasAlphaEnter = 1f;

    [Tooltip("Canvas Alpha when Pointer Exit")]
    [SerializeField]
    [Range(0f, 1f)]
    private float m_CanvasAlphaExit = 0.2f;

    [Header("Event")]

    [Tooltip("Unity Pointer Enter Event Handle")]
    [Space]
    [SerializeField]
    private UnityEvent EventPointerEnter;

    [Tooltip("Unity Pointer Exit Event Handle")]
    [Space]
    [SerializeField]
    private UnityEvent EventPointerExit;

    [Tooltip("Canvas Group")]
    private CanvasGroup m_CanvasGroup;

    private void Start()
    {
        if (GetComponent<CanvasGroup>() == null)
        {
            gameObject.AddComponent<CanvasGroup>();
        }

        m_CanvasGroup = GetComponent<CanvasGroup>();

        if (m_CavasLockEnter)
        {
            m_CanvasGroup.alpha = m_CanvasAlphaEnter;
        }
        else
        {
            m_CanvasGroup.alpha = m_CanvasAlphaExit;
        }

        if (m_ButtonLockChance != null)
        {
            //m_ButtonLockChance.SetEventAddPointerD(SetUICanvasLockEnterChance);
            m_ButtonLockChance.SetButtonActive(m_CavasLockEnter);
        }
    }

    #region Set Event

    #region Set Event Add

#if UNITY_EDITOR

    public void SetEventAddPointerEnter(UnityAction m_Methode)
    {
        UnityEventTools.AddPersistentListener(EventPointerEnter, m_Methode);
    }

    public void SetEventAddPointerExit(UnityAction m_Methode)
    {
        UnityEventTools.AddPersistentListener(EventPointerExit, m_Methode);
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

    #endregion

    #region Set Event Button

    private void SetEventPointerEnter()
    {
        m_CanvasGroup.alpha = m_CanvasAlphaEnter;

        SetEventInvokePointerEnter();
    }

    private void SetEventPointerExit()
    {
        if (m_CavasLockEnter)
        {
            m_CanvasGroup.alpha = m_CanvasAlphaEnter;

            return;
        }

        if (GetComponent<UIObjectDragDrop>() != null)
        {
            if (GetComponent<UIObjectDragDrop>().GetUICanvasDrag())
            {
                return;
            }
        }

        m_CanvasGroup.alpha = m_CanvasAlphaExit;

        SetEventInvokePointerExit();
    }

    #endregion

    #endregion

    #region On Event Handle

    public void OnPointerEnter(PointerEventData eventData)
    {
        SetEventPointerEnter();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SetEventPointerExit();
    }

    #endregion

    #region UI Canvas Lock

    public void SetUICanvasLockEnter(bool m_CavasLockEnter)
    {
        this.m_CavasLockEnter = m_CavasLockEnter;
    }

    public void SetUICanvasLockEnterChance()
    {
        SetUICanvasLockEnter(!GetUICanvasLockEnter());
    }

    public bool GetUICanvasLockEnter()
    {
        return m_CavasLockEnter;
    }

    #endregion

    #region UI Canvas Alpha

    #region UI Canvas Alpha Enter

    /// <summary>
    /// Set Alpha Canvas when in Curson Enter State (Alpha value from 0 to 1)
    /// </summary>
    /// <param name="m_CanvasAlphaEnter"></param>
    public void SetUICanvasAlphaEnter(float m_CanvasAlphaEnter)
    {
        if (m_CanvasAlphaEnter < 0)
        {
            m_CanvasAlphaEnter = 0;
        }
        else
        if (m_CanvasAlphaEnter > 1)
        {
            m_CanvasAlphaEnter = 1;
        }
        else
        {
            this.m_CanvasAlphaEnter = m_CanvasAlphaEnter;
        }
    }

    /// <summary>
    /// Get Alpha Canvas when in Curson Enter State
    /// </summary>
    /// <returns></returns>
    public float GetUICanvasAlphaEnter()
    {
        return m_CanvasAlphaEnter;
    }

    #endregion

    #region UI Canvas Alpha Exit

    /// <summary>
    /// Set Alpha Canvas when in Curson Exit State (Alpha value from 0 to 1)
    /// </summary>
    /// <param name="mCanvasAlphaExit"></param>
    public void SetUICanvasAlphaExit(float m_CanvasAlphaExit)
    {
        if (m_CanvasAlphaExit < 0)
        {
            m_CanvasAlphaExit = 0;
        }
        else
        if (m_CanvasAlphaExit > 1)
        {
            m_CanvasAlphaExit = 1;
        }
        else
        {
            this.m_CanvasAlphaExit = m_CanvasAlphaExit;
        }
    }

    /// <summary>
    /// Get Alpha Canvas when in Curson Exit State
    /// </summary>
    /// <returns></returns>
    public float GetUICanvasAlphaExit()
    {
        return m_CanvasAlphaExit;
    }

    #endregion

    #endregion
}
