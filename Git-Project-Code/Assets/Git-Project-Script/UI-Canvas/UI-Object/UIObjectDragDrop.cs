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

    [Tooltip("Anchor Min")]
    private Vector2 v2_Anchor_Min = new Vector2(0.5f, 0.5f);

    [Tooltip("Anchor Max")]
    private Vector2 v2_Anchor_Max = new Vector2(0.5f, 0.5f);

    [Tooltip("Rect Transform")]
    private RectTransform r_RectTransform;

    [Header("This Canvas")]

    [Tooltip("Canvas Alpha when Normal (Not Drag)")]
    [SerializeField]
    [Range(0f, 1f)]
    private float f_Canvas_Alpha_Normal = 1f;

    [Tooltip("Canvas Alpha when Drag")]
    [SerializeField]
    [Range(0f, 1f)]
    private float f_Canvas_Alpha_Drag = 0.6f;

    [Header("Event")]

    [Tooltip("Canvas Lock State")]
    [SerializeField]
    private bool m_Canvas_Lock = false;

    [Tooltip("Unity Pointer Enter Event Handle")]
    [Space]
    [SerializeField]
    private UnityEvent Event_PointerEnter;

    [Tooltip("Unity Pointer Exit Event Handle")]
    [Space]
    [SerializeField]
    private UnityEvent Event_PointerExit;

    [Tooltip("Unity Pointer Down Event Handle")]
    [Space]
    [SerializeField]
    private UnityEvent Event_PointerDown;

    [Tooltip("Unity Pointer Up Event Handle")]
    [Space]
    [SerializeField]
    private UnityEvent Event_PointerUp;

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
    private bool m_UI_Drag = false;

    [Tooltip("UI Hold Status")]
    private bool m_UI_Hold = false;

    [Tooltip("Button Ready Status")]
    private bool m_UI_Ready = false;

    private void Start()
    {
        if (c_ParentCanvas == null)
        {
            c_ParentCanvas = GetComponentInParent<Canvas>();
        }

        r_RectTransform = GetComponent<RectTransform>();

        r_RectTransform.pivot = v2_Pivot;
        r_RectTransform.anchorMin = v2_Anchor_Min;
        r_RectTransform.anchorMax = v2_Anchor_Max;

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
    public void Set_Event_Add_PointerEnter(UnityAction ua_Methode)
    {
        UnityEventTools.AddPersistentListener(Event_PointerEnter, ua_Methode);
    }

    /// <summary>
    /// This just work in Editor and not work in Build
    /// </summary>
    /// <param name="ua_Methode"></param>
    public void Set_Event_Add_PointerExit(UnityAction ua_Methode)
    {
        UnityEventTools.AddPersistentListener(Event_PointerExit, ua_Methode);
    }

    /// <summary>
    /// This just work in Editor and not work in Build
    /// </summary>
    /// <param name="ua_Methode"></param>
    public void Set_Event_Add_PointerDown(UnityAction ua_Methode)
    {
        UnityEventTools.AddPersistentListener(Event_PointerDown, ua_Methode);
    }

    /// <summary>
    /// This just work in Editor and not work in Build
    /// </summary>
    /// <param name="ua_Methode"></param>
    public void Set_Event_Add_PointerUp(UnityAction ua_Methode)
    {
        UnityEventTools.AddPersistentListener(Event_PointerUp, ua_Methode);
    }

    /// <summary>
    /// This just work in Editor and not work in Build
    /// </summary>
    /// <param name="ua_Methode"></param>
    public void Set_Event_Add_OnBeginDrag(UnityAction ua_Methode)
    {
        UnityEventTools.AddPersistentListener(Event_OnBeginDrag, ua_Methode);
    }

    /// <summary>
    /// This just work in Editor and not work in Build
    /// </summary>
    /// <param name="ua_Methode"></param>
    public void Set_Event_Add_OnDrag(UnityAction ua_Methode)
    {
        UnityEventTools.AddPersistentListener(Event_OnDrag, ua_Methode);
    }

    /// <summary>
    /// This just work in Editor and not work in Build
    /// </summary>
    /// <param name="ua_Methode"></param>
    public void Set_Event_Add_OnEndDrag(UnityAction ua_Methode)
    {
        UnityEventTools.AddPersistentListener(Event_OnEndDrag, ua_Methode);
    }

#endif

    #endregion

    #region Set Event Invoke

    private void Set_Event_Invoke_PointerEnter()
    {
        if (Event_PointerEnter != null)
        {
            Event_PointerEnter.Invoke();
        }
    }

    private void Set_Event_Invoke_PointerExit()
    {
        if (Event_PointerExit != null)
        {
            Event_PointerExit.Invoke();
        }
    }

    private void Set_Event_Invoke_PointerDown()
    {
        if (Event_PointerDown != null)
        {
            Event_PointerDown.Invoke();
        }
    }

    private void Set_Event_Invoke_PointerUp()
    {
        if (Event_PointerUp != null)
        {
            Event_PointerUp.Invoke();
        }
    }

    private void Set_Event_Invoke_OnBeginDrag()
    {
        if (Event_OnBeginDrag != null)
        {
            Event_OnBeginDrag.Invoke();
        }
    }

    private void Set_Event_Invoke_OnDrag()
    {
        if (Event_OnDrag != null)
        {
            Event_OnDrag.Invoke();
        }
    }

    private void Set_Event_Invoke_OnEndDrag()
    {
        if (Event_OnEndDrag != null)
        {
            Event_OnEndDrag.Invoke();
        }
    }

    #endregion

    #region Set Event Button

    private void Set_Event_PointerEnter()
    {
        if (m_Canvas_Lock)
        {
            return;
        }

        m_UI_Ready = true;

        Set_Event_Invoke_PointerEnter();
    }

    private void Set_Event_PointerExit()
    {
        if (m_Canvas_Lock)
        {
            return;
        }

        m_UI_Ready = false;

        Set_Event_Invoke_PointerExit();
    }

    private void Set_Event_PointerDown()
    {
        if (m_Canvas_Lock)
        {
            return;
        }

        m_UI_Hold = true;

        Set_Event_Invoke_PointerDown();
    }

    private void Set_Event_PointerUp()
    {
        if (m_Canvas_Lock)
        {
            return;
        }

        m_UI_Hold = false;

        Set_Event_Invoke_PointerUp();
    }

    private void Set_Event_OnBeginDrag()
    {
        if (m_Canvas_Lock)
        {
            return;
        }

        m_UI_Drag = true;

        c_CanvasGroup.alpha = f_Canvas_Alpha_Drag;

        c_CanvasGroup.blocksRaycasts = false;

        Set_Event_Invoke_OnBeginDrag();
    }

    private void Set_Event_OnDrag(PointerEventData eventData)
    {
        if (m_Canvas_Lock)
        {
            return;
        }

        r_RectTransform.anchoredPosition += eventData.delta / c_ParentCanvas.scaleFactor;

        Set_Event_Invoke_OnDrag();
    }

    private void Set_Event_OnEndDrag()
    {
        if (m_Canvas_Lock)
        {
            return;
        }

        m_UI_Drag = false;

        c_CanvasGroup.alpha = f_Canvas_Alpha_Normal;

        c_CanvasGroup.blocksRaycasts = true;

        Set_Event_Invoke_OnEndDrag();
    }

    #endregion

    #endregion

    #region On Event Handle

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        Set_Event_PointerEnter();
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        Set_Event_PointerExit();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Set_Event_PointerDown();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Set_Event_PointerUp();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Set_Event_OnBeginDrag();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Set_Event_OnDrag(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Set_Event_OnEndDrag();
    }

    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("OnDrop");
    }

    #endregion

    #region UI Status

    #region UI Status Set

    public void Set_UI_Canvas_Lock(bool m_Lock_Status)
    {
        m_Canvas_Lock = m_Lock_Status;
    }

    public void Set_UI_Canvas_Lock_True()
    {
        Set_UI_Canvas_Lock(true);
    }

    public void Set_UI_Canvas_Lock_False()
    {
        Set_UI_Canvas_Lock(false);
    }

    #endregion

    #region UI Status

    public bool GetUI_Canvas_Drag()
    {
        return m_UI_Drag;
    }

    public bool GetUI_Canvas_Hold()
    {
        return m_UI_Hold;
    }

    public bool GetUI_Canvas_Ready()
    {
        return m_UI_Ready;
    }

    public bool GetUI_Canvas_Lock()
    {
        return m_Canvas_Lock;
    }

    #endregion

    #endregion

    #region UI Canvas Alpha

    #region UI Canvas Alpha Normal

    /// <summary>
    /// Set Alpha Canvas when in Normal State (Not Drag State) (Alpha value from 0 to 1)
    /// </summary>
    /// <param name="f_Canvas_Alpha_Normal"></param>
    public void Set_UI_Canvas_Alpha_Normal(float f_Canvas_Alpha_Normal)
    {
        if (f_Canvas_Alpha_Normal < 0)
        {
            this.f_Canvas_Alpha_Normal = 0;
        }
        else
        if (f_Canvas_Alpha_Normal > 1)
        {
            this.f_Canvas_Alpha_Normal = 1;
        }
        else
        {
            this.f_Canvas_Alpha_Normal = f_Canvas_Alpha_Normal;
        }
    }

    /// <summary>
    /// Get Alpha Canvas when in Drag State
    /// </summary>
    /// <returns></returns>
    public float GetUI_Canvas_Alpha_Normal()
    {
        return f_Canvas_Alpha_Normal;
    }

    #endregion

    #region UI Canvas Alpha Drag

    /// <summary>
    /// Set Alpha Canvas when in Drag State (Alpha value from 0 to 1)
    /// </summary>
    /// <param name="f_Canvas_Alpha_Drag"></param>
    public void Set_UI_Canvas_Alpha_Drag(float f_Canvas_Alpha_Drag)
    {
        if (f_Canvas_Alpha_Drag < 0)
        {
            this.f_Canvas_Alpha_Drag = 0;
        }
        else
        if (f_Canvas_Alpha_Drag > 1)
        {
            this.f_Canvas_Alpha_Drag = 1;
        }
        else
        {
            this.f_Canvas_Alpha_Drag = f_Canvas_Alpha_Drag;
        }
    }

    /// <summary>
    /// Get Alpha Canvas when in Drag State
    /// </summary>
    /// <returns></returns>
    public float GetUI_Canvas_Alpha_Drag()
    {
        return f_Canvas_Alpha_Drag;
    }

    #endregion

    #endregion
}
