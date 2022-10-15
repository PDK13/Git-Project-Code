#if UNITY_EDITOR
using UnityEditor.Events;
#endif
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIObjectDragDrop : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler,
    IPointerDownHandler, IPointerUpHandler,
    IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    [Header("Parent Canvas")]

    [Tooltip("Parent Canvas")]
    [SerializeField]
    private Canvas m_ParentCanvas;

    [Header("This RecTransform")]

    [Tooltip("Pivot")]
    [SerializeField]
    private Vector2 m_Pivot = new Vector2(0.5f, 0.5f);

    [Tooltip("Anchor m_in")]
    private Vector2 m_AnchorMin = new Vector2(0.5f, 0.5f);

    [Tooltip("Anchor m_ax")]
    private Vector2 m_AnchorMax = new Vector2(0.5f, 0.5f);

    [Tooltip("Rect Transform")]
    private RectTransform rRectTransform;

    [Header("This Canvas")]

    [Tooltip("Canvas Alpha when Normal (Not Drag)")]
    [SerializeField]
    [Range(0f, 1f)]
    private float m_CanvasAlphaNormal = 1f;

    [Tooltip("Canvas Alpha when Drag")]
    [SerializeField]
    [Range(0f, 1f)]
    private float m_CanvasAlphaDrag = 0.6f;

    [Header("Event")]

    [Tooltip("Canvas Lock State")]
    [SerializeField]
    private bool m_CavasLock = false;

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

    [Tooltip("Unity On Begin Drag Event Handle")]
    [Space]
    [SerializeField]
    private UnityEvent EventOnBeginDrag;

    [Tooltip("Unity On Drag Event Handle")]
    [Space]
    [SerializeField]
    private UnityEvent EventOnDrag;

    [Tooltip("Unity On End Drag Event Handle")]
    [Space]
    [SerializeField]
    private UnityEvent EventOnEndDrag;

    [Tooltip("Canvas Group")]
    private CanvasGroup m_CanvasGroup;

    [Tooltip("UI Drag Status")]
    private bool m_UIDrag = false;

    [Tooltip("UI Hold Status")]
    private bool m_UIHold = false;

    [Tooltip("Button Ready Status")]
    private bool m_UIReady = false;

    private void Start()
    {
        if (m_ParentCanvas == null)
        {
            m_ParentCanvas = GetComponentInParent<Canvas>();
        }

        rRectTransform = GetComponent<RectTransform>();

        rRectTransform.pivot = m_Pivot;
        rRectTransform.anchorMin = m_AnchorMin;
        rRectTransform.anchorMax = m_AnchorMax;

        if (GetComponent<CanvasGroup>() == null)
        {
            gameObject.AddComponent<CanvasGroup>();
        }

        m_CanvasGroup = GetComponent<CanvasGroup>();
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
    public void SetEventAddOnBeginDrag(UnityAction m_Methode)
    {
        UnityEventTools.AddPersistentListener(EventOnBeginDrag, m_Methode);
    }

    /// <summary>
    /// This just work in Editor and not work in Build
    /// </summary>
    /// <param name="m_Methode"></param>
    public void SetEventAddOnDrag(UnityAction m_Methode)
    {
        UnityEventTools.AddPersistentListener(EventOnDrag, m_Methode);
    }

    /// <summary>
    /// This just work in Editor and not work in Build
    /// </summary>
    /// <param name="m_Methode"></param>
    public void SetEventAddOnEndDrag(UnityAction m_Methode)
    {
        UnityEventTools.AddPersistentListener(EventOnEndDrag, m_Methode);
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

    private void SetEventInvokeOnBeginDrag()
    {
        if (EventOnBeginDrag != null)
        {
            EventOnBeginDrag.Invoke();
        }
    }

    private void SetEventInvokeOnDrag()
    {
        if (EventOnDrag != null)
        {
            EventOnDrag.Invoke();
        }
    }

    private void SetEventInvokeOnEndDrag()
    {
        if (EventOnEndDrag != null)
        {
            EventOnEndDrag.Invoke();
        }
    }

    #endregion

    #region Set Event Button

    private void SetEventPointerEnter()
    {
        if (m_CavasLock)
        {
            return;
        }

        m_UIReady = true;

        SetEventInvokePointerEnter();
    }

    private void SetEventPointerExit()
    {
        if (m_CavasLock)
        {
            return;
        }

        m_UIReady = false;

        SetEventInvokePointerExit();
    }

    private void SetEventPointerD()
    {
        if (m_CavasLock)
        {
            return;
        }

        m_UIHold = true;

        SetEventInvokePointerD();
    }

    private void SetEventPointerU()
    {
        if (m_CavasLock)
        {
            return;
        }

        m_UIHold = false;

        SetEventInvokePointerU();
    }

    private void SetEventOnBeginDrag()
    {
        if (m_CavasLock)
        {
            return;
        }

        m_UIDrag = true;

        m_CanvasGroup.alpha = m_CanvasAlphaDrag;

        m_CanvasGroup.blocksRaycasts = false;

        SetEventInvokeOnBeginDrag();
    }

    private void SetEventOnDrag(PointerEventData eventData)
    {
        if (m_CavasLock)
        {
            return;
        }

        rRectTransform.anchoredPosition += eventData.delta / m_ParentCanvas.scaleFactor;

        SetEventInvokeOnDrag();
    }

    private void SetEventOnEndDrag()
    {
        if (m_CavasLock)
        {
            return;
        }

        m_UIDrag = false;

        m_CanvasGroup.alpha = m_CanvasAlphaNormal;

        m_CanvasGroup.blocksRaycasts = true;

        SetEventInvokeOnEndDrag();
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

    public void OnBeginDrag(PointerEventData eventData)
    {
        SetEventOnBeginDrag();
    }

    public void OnDrag(PointerEventData eventData)
    {
        SetEventOnDrag(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        SetEventOnEndDrag();
    }

    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("OnDrop");
    }

    #endregion

    #region UI Status

    #region UI Status Set

    public void SetUICanvasLock(bool m_LockStatus)
    {
        m_CavasLock = m_LockStatus;
    }

    public void SetUICanvasLom_KeyTrue()
    {
        SetUICanvasLock(true);
    }

    public void SetUICanvasLom_KeyFalse()
    {
        SetUICanvasLock(false);
    }

    #endregion

    #region UI Status

    public bool GetUICanvasDrag()
    {
        return m_UIDrag;
    }

    public bool GetUICanvasHold()
    {
        return m_UIHold;
    }

    public bool GetUICanvasReady()
    {
        return m_UIReady;
    }

    public bool GetUICanvasLock()
    {
        return m_CavasLock;
    }

    #endregion

    #endregion

    #region UI Canvas Alpha

    #region UI Canvas Alpha Normal

    /// <summary>
    /// Set Alpha Canvas when in Normal State (Not Drag State) (Alpha value from 0 to 1)
    /// </summary>
    /// <param name="mCanvasAlphaNormal"></param>
    public void SetUICanvasAlphaNormal(float m_CanvasAlphaNormal)
    {
        if (m_CanvasAlphaNormal < 0)
        {
            m_CanvasAlphaNormal = 0;
        }
        else
        if (m_CanvasAlphaNormal > 1)
        {
            m_CanvasAlphaNormal = 1;
        }
        else
        {
            this.m_CanvasAlphaNormal = m_CanvasAlphaNormal;
        }
    }

    /// <summary>
    /// Get Alpha Canvas when in Drag State
    /// </summary>
    /// <returns></returns>
    public float GetUICanvasAlphaNormal()
    {
        return m_CanvasAlphaNormal;
    }

    #endregion

    #region UI Canvas Alpha Drag

    /// <summary>
    /// Set Alpha Canvas when in Drag State (Alpha value from 0 to 1)
    /// </summary>
    /// <param name="mCanvasAlphaDrag"></param>
    public void SetUICanvasAlphaDrag(float m_CanvasAlphaDrag)
    {
        if (m_CanvasAlphaDrag < 0)
        {
            m_CanvasAlphaDrag = 0;
        }
        else
        if (m_CanvasAlphaDrag > 1)
        {
            m_CanvasAlphaDrag = 1;
        }
        else
        {
            this.m_CanvasAlphaDrag = m_CanvasAlphaDrag;
        }
    }

    /// <summary>
    /// Get Alpha Canvas when in Drag State
    /// </summary>
    /// <returns></returns>
    public float GetUICanvasAlphaDrag()
    {
        return m_CanvasAlphaDrag;
    }

    #endregion

    #endregion
}
