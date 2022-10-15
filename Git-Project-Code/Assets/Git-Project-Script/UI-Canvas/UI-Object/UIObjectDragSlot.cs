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
    private bool m_UILock = false;

    [Tooltip("Rect Transform")]
    private RectTransform rRectTransform;

    [Header("Event")]

    [Tooltip("Unity Pointer Enter Event Handle")]
    [Space]
    [SerializeField]
    private UnityEvent EventPointerEnter;

    [Tooltip("Unity Pointer Exit Event Handle")]
    [Space]
    [SerializeField]
    private UnityEvent EventPointerExit;

    [Tooltip("Unity On Drop Event Handle")]
    [Space]
    [SerializeField]
    private UnityEvent EventOnDrop;

    [Tooltip("UI Drop Status")]
    //[SerializeField]
    private bool m_UIDropStatus = false;

    [Tooltip("Object in Slot")]
    //[SerializeField]
    private GameObject m_UI_GameObject_InSlot;

    [Tooltip("Button Ready Status")]
    private bool m_UIReady = false;

    private void Start()
    {
        rRectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (GetUIDrop())
        {
            if (m_UI_GameObject_InSlot.GetComponent<RectTransform>().anchoredPosition != rRectTransform.anchoredPosition)
            {
                m_UI_GameObject_InSlot = null;

                m_UIDropStatus = false;
            }
        }
    }

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
    public void SetEventAddOnDrop(UnityAction m_Methode)
    {
        UnityEventTools.AddPersistentListener(EventOnDrop, m_Methode);
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

    private void SetEventInvokeOnDrop()
    {
        if (EventOnDrop != null)
        {
            EventOnDrop.Invoke();
        }
    }

    #endregion

    #region Set Event Button

    private void SetEventPointerEnter()
    {
        if (m_UILock)
        {
            return;
        }

        m_UIReady = true;

        SetEventInvokePointerEnter();
    }

    private void SetEventPointerExit()
    {
        if (m_UILock)
        {
            return;
        }

        m_UIReady = false;

        SetEventInvokePointerExit();
    }

    private void SetEventOnDrop(PointerEventData eventData)
    {
        if (m_UILock)
        {
            return;
        }

        if (eventData.pointerDrag != null)
        {
            m_UI_GameObject_InSlot = eventData.pointerDrag;

            m_UI_GameObject_InSlot.GetComponent<RectTransform>().anchoredPosition = rRectTransform.anchoredPosition;

            m_UIDropStatus = true;
        }

        SetEventInvokeOnDrop();
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

    public void OnDrop(PointerEventData eventData)
    {
        SetEventOnDrop(eventData);
    }

    #endregion

    #region UI Status

    #region UI Status Set

    public void SetButtonLock(bool m_LockStatus)
    {
        m_UILock = m_LockStatus;
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

    public bool GetUIReady()
    {
        return m_UIReady;
    }

    public bool GetUIDrop()
    {
        return m_UIDropStatus;
    }

    public GameObject GetUI_GameObjectDrop()
    {
        return m_UI_GameObject_InSlot;
    }

    #endregion

    #endregion
}
