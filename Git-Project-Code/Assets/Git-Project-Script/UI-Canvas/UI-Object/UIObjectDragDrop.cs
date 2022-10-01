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
    private Canvas c_ParentCanvas;

    [Header("This RecTransform")]

    [Tooltip("Pivot")]
    [SerializeField]
    private Vector2 v2_Pivot = new Vector2(0.5f, 0.5f);

    [Tooltip("Anchor m_in")]
    private Vector2 v2_AnchorMin = new Vector2(0.5f, 0.5f);

    [Tooltip("Anchor m_ax")]
    private Vector2 v2_AnchorMax = new Vector2(0.5f, 0.5f);

    [Tooltip("Rect Transform")]
    private RectTransform rRectTransform;

    [Header("This Canvas")]

    [Tooltip("Canvas Alpha when Normal (Not Drag)")]
    [SerializeField]
    [Range(0f, 1f)]
    private float m_Canvas_Alpha_Normal = 1f;

    [Tooltip("Canvas Alpha when Drag")]
    [SerializeField]
    [Range(0f, 1f)]
    private float m_Canvas_AlphaDrag = 0.6f;

    [Header("Event")]

    [Tooltip("Canvas Lock State")]
    [SerializeField]
    private bool m_AllowCavasLock = false;

    [Tooltip("Unity Pointer Enter Event Handle")]
    [Space]
    [SerializeField]
    private UnityEvent Event_PointerEnter;

    [Tooltip("Unity Pointer Exit Event Handle")]
    [Space]
    [SerializeField]
    private UnityEvent Event_PointerExit;

    [Tooltip("Unity Pointer D Event Handle")]
    [Space]
    [SerializeField]
    private UnityEvent Event_PointerD;

    [Tooltip("Unity Pointer U Event Handle")]
    [Space]
    [SerializeField]
    private UnityEvent Event_PointerU;

    [Tooltip("Unity On Begin Drag Event Handle")]
    [Space]
    [SerializeField]
    private UnityEvent Event_OnBeginDrag;

    [Tooltip("Unity On Drag Event Handle")]
    [Space]
    [SerializeField]
    private UnityEvent Event_OnDrag;

    [Tooltip("Unity On End Drag Event Handle")]
    [Space]
    [SerializeField]
    private UnityEvent Event_OnEndDrag;

    [Tooltip("Canvas Group")]
    private CanvasGroup c_CanvasGroup;

    [Tooltip("UI Drag Status")]
    private bool m_AllowUIDrag = false;

    [Tooltip("UI Hold Status")]
    private bool m_AllowUIHold = false;

    [Tooltip("Button Ready Status")]
    private bool m_AllowUIReady = false;

    private void Start()
    {
        if (c_ParentCanvas == null)
        {
            c_ParentCanvas = GetComponentInParent<Canvas>();
        }

        rRectTransform = GetComponent<RectTransform>();

        rRectTransform.pivot = v2_Pivot;
        rRectTransform.anchorMin = v2_AnchorMin;
        rRectTransform.anchorMax = v2_AnchorMax;

        if (GetComponent<CanvasGroup>() == null)
        {
            gameObject.AddComponent<CanvasGroup>();
        }

        c_CanvasGroup = GetComponent<CanvasGroup>();
    }

    #region Set Event

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

    /// <summary>
    /// This just work in Editor and not work in Build
    /// </summary>
    /// <param name="ua_Methode"></param>
    public void SetEvent_Add_OnBeginDrag(UnityAction ua_Methode)
    {
        UnityEventTools.AddPersistentListener(Event_OnBeginDrag, ua_Methode);
    }

    /// <summary>
    /// This just work in Editor and not work in Build
    /// </summary>
    /// <param name="ua_Methode"></param>
    public void SetEvent_Add_OnDrag(UnityAction ua_Methode)
    {
        UnityEventTools.AddPersistentListener(Event_OnDrag, ua_Methode);
    }

    /// <summary>
    /// This just work in Editor and not work in Build
    /// </summary>
    /// <param name="ua_Methode"></param>
    public void SetEvent_Add_OnEndDrag(UnityAction ua_Methode)
    {
        UnityEventTools.AddPersistentListener(Event_OnEndDrag, ua_Methode);
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

    private void SetEvent_Invoke_OnBeginDrag()
    {
        if (Event_OnBeginDrag != null)
        {
            Event_OnBeginDrag.Invoke();
        }
    }

    private void SetEvent_Invoke_OnDrag()
    {
        if (Event_OnDrag != null)
        {
            Event_OnDrag.Invoke();
        }
    }

    private void SetEvent_Invoke_OnEndDrag()
    {
        if (Event_OnEndDrag != null)
        {
            Event_OnEndDrag.Invoke();
        }
    }

    #endregion

    #region Set Event Button

    private void SetEvent_PointerEnter()
    {
        if (m_AllowCavasLock)
        {
            return;
        }

        m_AllowUIReady = true;

        SetEvent_Invoke_PointerEnter();
    }

    private void SetEvent_PointerExit()
    {
        if (m_AllowCavasLock)
        {
            return;
        }

        m_AllowUIReady = false;

        SetEvent_Invoke_PointerExit();
    }

    private void SetEvent_PointerD()
    {
        if (m_AllowCavasLock)
        {
            return;
        }

        m_AllowUIHold = true;

        SetEvent_Invoke_PointerD();
    }

    private void SetEvent_PointerU()
    {
        if (m_AllowCavasLock)
        {
            return;
        }

        m_AllowUIHold = false;

        SetEvent_Invoke_PointerU();
    }

    private void SetEvent_OnBeginDrag()
    {
        if (m_AllowCavasLock)
        {
            return;
        }

        m_AllowUIDrag = true;

        c_CanvasGroup.alpha = m_Canvas_AlphaDrag;

        c_CanvasGroup.blocksRaycasts = false;

        SetEvent_Invoke_OnBeginDrag();
    }

    private void SetEvent_OnDrag(PointerEventData eventData)
    {
        if (m_AllowCavasLock)
        {
            return;
        }

        rRectTransform.anchoredPosition += eventData.delta / c_ParentCanvas.scaleFactor;

        SetEvent_Invoke_OnDrag();
    }

    private void SetEvent_OnEndDrag()
    {
        if (m_AllowCavasLock)
        {
            return;
        }

        m_AllowUIDrag = false;

        c_CanvasGroup.alpha = m_Canvas_Alpha_Normal;

        c_CanvasGroup.blocksRaycasts = true;

        SetEvent_Invoke_OnEndDrag();
    }

    #endregion

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

    public void OnBeginDrag(PointerEventData eventData)
    {
        SetEvent_OnBeginDrag();
    }

    public void OnDrag(PointerEventData eventData)
    {
        SetEvent_OnDrag(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        SetEvent_OnEndDrag();
    }

    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("OnDrop");
    }

    #endregion

    #region UI Status

    #region UI Status Set

    public void SetUICanvasLock(bool m_AllowLockStatus)
    {
        m_AllowCavasLock = m_AllowLockStatus;
    }

    public void SetUICanvasLocs_KeyTrue()
    {
        SetUICanvasLock(true);
    }

    public void SetUICanvasLocs_KeyFalse()
    {
        SetUICanvasLock(false);
    }

    #endregion

    #region UI Status

    public bool GetCheckUICanvasDrag()
    {
        return m_AllowUIDrag;
    }

    public bool GetCheckUICanvasHold()
    {
        return m_AllowUIHold;
    }

    public bool GetCheckUICanvasReady()
    {
        return m_AllowUIReady;
    }

    public bool GetCheckUICanvasLock()
    {
        return m_AllowCavasLock;
    }

    #endregion

    #endregion

    #region UI Canvas Alpha

    #region UI Canvas Alpha Normal

    /// <summary>
    /// Set Alpha Canvas when in Normal State (Not Drag State) (Alpha value from 0 to 1)
    /// </summary>
    /// <param name="mCanvas_Alpha_Normal"></param>
    public void SetUICanvas_Alpha_Normal(float m_Canvas_Alpha_Normal)
    {
        if (m_Canvas_Alpha_Normal < 0)
        {
            m_Canvas_Alpha_Normal = 0;
        }
        else
        if (m_Canvas_Alpha_Normal > 1)
        {
            m_Canvas_Alpha_Normal = 1;
        }
        else
        {
            this.m_Canvas_Alpha_Normal = m_Canvas_Alpha_Normal;
        }
    }

    /// <summary>
    /// Get Alpha Canvas when in Drag State
    /// </summary>
    /// <returns></returns>
    public float GetUICanvas_Alpha_Normal()
    {
        return m_Canvas_Alpha_Normal;
    }

    #endregion

    #region UI Canvas Alpha Drag

    /// <summary>
    /// Set Alpha Canvas when in Drag State (Alpha value from 0 to 1)
    /// </summary>
    /// <param name="mCanvas_AlphaDrag"></param>
    public void SetUICanvas_AlphaDrag(float m_Canvas_AlphaDrag)
    {
        if (m_Canvas_AlphaDrag < 0)
        {
            m_Canvas_AlphaDrag = 0;
        }
        else
        if (m_Canvas_AlphaDrag > 1)
        {
            m_Canvas_AlphaDrag = 1;
        }
        else
        {
            this.m_Canvas_AlphaDrag = m_Canvas_AlphaDrag;
        }
    }

    /// <summary>
    /// Get Alpha Canvas when in Drag State
    /// </summary>
    /// <returns></returns>
    public float GetUICanvas_AlphaDrag()
    {
        return m_Canvas_AlphaDrag;
    }

    #endregion

    #endregion
}
