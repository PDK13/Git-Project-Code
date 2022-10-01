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
    private bool m_Canvam_Lock_Enter = false;

    [Tooltip("Button for Canvas Alpha Lock Chance")]
    [SerializeField]
    private UIButtonOnClick cl_Button_Lock_Chance;

    [Header("Canvas Alpha")]

    [Tooltip("Canvas Alpha when Pointer Enter")]
    [SerializeField]
    [Range(0f, 1f)]
    private float m_Canvam_Alpha_Enter = 1f;

    [Tooltip("Canvas Alpha when Pointer Exit")]
    [SerializeField]
    [Range(0f, 1f)]
    private float m_Canvam_Alpha_Exit = 0.2f;

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

        if (m_Canvam_Lock_Enter)
        {
            c_CanvasGroup.alpha = m_Canvam_Alpha_Enter;
        }
        else
        {
            c_CanvasGroup.alpha = m_Canvam_Alpha_Exit;
        }

        if (cl_Button_Lock_Chance != null)
        {
            //cl_Button_Lock_Chance.SetEvent_Add_PointerDown(SetUI_Canvam_Lock_Enter_Chance);
            cl_Button_Lock_Chance.SetButton_Active(m_Canvam_Lock_Enter);
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
        c_CanvasGroup.alpha = m_Canvam_Alpha_Enter;

        SetEvent_Invoke_PointerEnter();
    }

    private void SetEvent_PointerExit()
    {
        if (m_Canvam_Lock_Enter)
        {
            c_CanvasGroup.alpha = m_Canvam_Alpha_Enter;

            return;
        }

        if (GetComponent<UIObjectDragDrop>() != null)
        {
            if (GetComponent<UIObjectDragDrop>().GetUI_Canvam_Drag())
            {
                return;
            }
        }

        c_CanvasGroup.alpha = m_Canvam_Alpha_Exit;

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

    public void SetUI_Canvam_Lock_Enter(bool m_Canvam_Lock_Enter)
    {
        this.m_Canvam_Lock_Enter = m_Canvam_Lock_Enter;
    }

    public void SetUI_Canvam_Lock_Enter_Chance()
    {
        SetUI_Canvam_Lock_Enter(!GetUI_Canvam_Lock_Enter());
    }

    public bool GetUI_Canvam_Lock_Enter()
    {
        return m_Canvam_Lock_Enter;
    }

    #endregion

    #region UI Canvas Alpha

    #region UI Canvas Alpha Enter

    /// <summary>
    /// Set Alpha Canvas when in Curson Enter State (Alpha value from 0 to 1)
    /// </summary>
    /// <param name="m_Canvam_Alpha_Enter"></param>
    public void SetUI_Canvam_Alpha_Enter(float m_Canvam_Alpha_Enter)
    {
        if (m_Canvam_Alpha_Enter < 0)
        {
            this.m_Canvam_Alpha_Enter = 0;
        }
        else
        if (m_Canvam_Alpha_Enter > 1)
        {
            this.m_Canvam_Alpha_Enter = 1;
        }
        else
        {
            this.m_Canvam_Alpha_Enter = m_Canvam_Alpha_Enter;
        }
    }

    /// <summary>
    /// Get Alpha Canvas when in Curson Enter State
    /// </summary>
    /// <returns></returns>
    public float GetUI_Canvam_Alpha_Enter()
    {
        return m_Canvam_Alpha_Enter;
    }

    #endregion

    #region UI Canvas Alpha Exit

    /// <summary>
    /// Set Alpha Canvas when in Curson Exit State (Alpha value from 0 to 1)
    /// </summary>
    /// <param name="m_Canvam_Alpha_Exit"></param>
    public void SetUI_Canvam_Alpha_Exit(float m_Canvam_Alpha_Exit)
    {
        if (m_Canvam_Alpha_Exit < 0)
        {
            this.m_Canvam_Alpha_Exit = 0;
        }
        else
        if (m_Canvam_Alpha_Exit > 1)
        {
            this.m_Canvam_Alpha_Exit = 1;
        }
        else
        {
            this.m_Canvam_Alpha_Exit = m_Canvam_Alpha_Exit;
        }
    }

    /// <summary>
    /// Get Alpha Canvas when in Curson Exit State
    /// </summary>
    /// <returns></returns>
    public float GetUI_Canvam_Alpha_Exit()
    {
        return m_Canvam_Alpha_Exit;
    }

    #endregion

    #endregion
}
