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
    private bool b_UI_Lock = false;

    [Tooltip("Rect Transform")]
    private RectTransform r_RectTransform;

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
    private bool b_UI_Drop_Status = false;

    [Tooltip("Object in Slot")]
    //[SerializeField]
    private GameObject g_UI_GameObject_InSlot;

    [Tooltip("Button Ready Status")]
    private bool b_UI_Ready = false;

    private void Start()
    {
        r_RectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (Get_UI_Drop())
        {
            if (g_UI_GameObject_InSlot.GetComponent<RectTransform>().anchoredPosition != r_RectTransform.anchoredPosition)
            {
                g_UI_GameObject_InSlot = null;

                b_UI_Drop_Status = false;
            }
        }
    }

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
    public void Set_Event_Add_OnDrop(UnityAction ua_Methode)
    {
        UnityEventTools.AddPersistentListener(Event_OnDrop, ua_Methode);
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

    private void Set_Event_Invoke_OnDrop()
    {
        if (Event_OnDrop != null)
        {
            Event_OnDrop.Invoke();
        }
    }

    #endregion

    #region Set Event Button

    private void Set_Event_PointerEnter()
    {
        if (b_UI_Lock)
        {
            return;
        }

        b_UI_Ready = true;

        Set_Event_Invoke_PointerEnter();
    }

    private void Set_Event_PointerExit()
    {
        if (b_UI_Lock)
        {
            return;
        }

        b_UI_Ready = false;

        Set_Event_Invoke_PointerExit();
    }

    private void Set_Event_OnDrop(PointerEventData eventData)
    {
        if (b_UI_Lock)
        {
            return;
        }

        if (eventData.pointerDrag != null)
        {
            g_UI_GameObject_InSlot = eventData.pointerDrag;

            g_UI_GameObject_InSlot.GetComponent<RectTransform>().anchoredPosition = r_RectTransform.anchoredPosition;

            b_UI_Drop_Status = true;
        }

        Set_Event_Invoke_OnDrop();
    }

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

    public void OnDrop(PointerEventData eventData)
    {
        Set_Event_OnDrop(eventData);
    }

    #endregion

    #region UI Status

    #region UI Status Set

    public void Set_Button_Lock(bool b_Lock_Status)
    {
        b_UI_Lock = b_Lock_Status;
    }

    public void Set_Button_Lock_True()
    {
        Set_Button_Lock(true);
    }

    public void Set_Button_Lock_False()
    {
        Set_Button_Lock(false);
    }

    #endregion

    #region UI Status Get

    public bool Get_UI_Ready()
    {
        return b_UI_Ready;
    }

    public bool Get_UI_Drop()
    {
        return b_UI_Drop_Status;
    }

    public GameObject Get_UI_GameObject_Drop()
    {
        return g_UI_GameObject_InSlot;
    }

    #endregion

    #endregion
}
