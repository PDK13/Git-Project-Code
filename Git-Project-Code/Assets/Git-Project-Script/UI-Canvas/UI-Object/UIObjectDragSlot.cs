#if UNITY_EDITOR
using UnityEditor.Events;
#endif
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIObjectDragSlot : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler,
    IDropHandler
{
    [Header("This Canvas")]

    [Tooltip("Button Lock Status")]
    [SerializeField]
    private bool m_AllowUILock = false;

    [Tooltip("Rect Transform")]
    private RectTransform rRectTransform;

    [Header("Event")]

    [Tooltip("Unity Pointer Enter Event Handle")]
    [Space]
    [SerializeField]
    private UnityEvent Event_PointerEnter;

    [Tooltip("Unity Pointer Exit Event Handle")]
    [Space]
    [SerializeField]
    private UnityEvent Event_PointerExit;

    [Tooltip("Unity On Drop Event Handle")]
    [Space]
    [SerializeField]
    private UnityEvent Event_OnDrop;

    [Tooltip("UI Drop Status")]
    //[SerializeField]
    private bool m_AllowUIDropStatus = false;

    [Tooltip("Object in Slot")]
    //[SerializeField]
    private GameObject m_UI_GameObject_InSlot;

    [Tooltip("Button Ready Status")]
    private bool m_AllowUIReady = false;

    private void Start()
    {
        rRectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (GetCheckUIDrop())
        {
            if (m_UI_GameObject_InSlot.GetComponent<RectTransform>().anchoredPosition != rRectTransform.anchoredPosition)
            {
                m_UI_GameObject_InSlot = null;

                m_AllowUIDropStatus = false;
            }
        }
    }

    #region Set Event Add

#if UNITY_EDITOR

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
    public void SetEvent_Add_OnDrop(UnityAction ua_Methode)
    {
        UnityEventTools.AddPersistentListener(Event_OnDrop, ua_Methode);
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

    private void SetEvent_Invoke_OnDrop()
    {
        if (Event_OnDrop != null)
        {
            Event_OnDrop.Invoke();
        }
    }

    #endregion

    #region Set Event Button

    private void SetEvent_PointerEnter()
    {
        if (m_AllowUILock)
        {
            return;
        }

        m_AllowUIReady = true;

        SetEvent_Invoke_PointerEnter();
    }

    private void SetEvent_PointerExit()
    {
        if (m_AllowUILock)
        {
            return;
        }

        m_AllowUIReady = false;

        SetEvent_Invoke_PointerExit();
    }

    private void SetEvent_OnDrop(PointerEventData eventData)
    {
        if (m_AllowUILock)
        {
            return;
        }

        if (eventData.pointerDrag != null)
        {
            m_UI_GameObject_InSlot = eventData.pointerDrag;

            m_UI_GameObject_InSlot.GetComponent<RectTransform>().anchoredPosition = rRectTransform.anchoredPosition;

            m_AllowUIDropStatus = true;
        }

        SetEvent_Invoke_OnDrop();
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

    public void OnDrop(PointerEventData eventData)
    {
        SetEvent_OnDrop(eventData);
    }

    #endregion

    #region UI Status

    #region UI Status Set

    public void SetButtonLock(bool m_AllowLockStatus)
    {
        m_AllowUILock = m_AllowLockStatus;
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

    #region UI Status Get

    public bool GetCheckUIReady()
    {
        return m_AllowUIReady;
    }

    public bool GetCheckUIDrop()
    {
        return m_AllowUIDropStatus;
    }

    public GameObject GetUI_GameObjectDrop()
    {
        return m_UI_GameObject_InSlot;
    }

    #endregion

    #endregion
}
