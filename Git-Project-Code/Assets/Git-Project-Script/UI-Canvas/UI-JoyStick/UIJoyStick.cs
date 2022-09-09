using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIJoyStick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [Header("UI")]

    [SerializeField] private bool b_Lock_Position = true;

    [SerializeField] private bool b_Auto_Reset = true;

    [SerializeField] private RectTransform com_JoyStick_Limit;

    private Vector2 v2_JoyStick_Limit_Position_Primary;

    [SerializeField] private RectTransform com_JoyStick_Button;

    [Header("Lock")]

    [SerializeField] private bool b_Lock_X = false;

    [SerializeField] private bool b_Lock_Y = false;

    private Vector2 v2_JoyStick_Limit;

    private Vector2 v2_JoyStick_Touch;

    private Canvas com_Canvas;

    private Camera com_Camera;

    private void Start()
    {
        Vector2 center = new Vector2(0.5f, 0.5f);
        com_JoyStick_Limit.pivot = center;
        com_JoyStick_Button.anchorMin = center;
        com_JoyStick_Button.anchorMax = center;
        com_JoyStick_Button.pivot = center;
        com_JoyStick_Button.anchoredPosition = Vector2.zero;

        v2_JoyStick_Limit_Position_Primary = com_JoyStick_Limit.GetComponent<RectTransform>().anchoredPosition;

        if (com_Canvas == null)
        {
            com_Canvas = GetComponentInParent<Canvas>();

            if (com_Canvas == null)
            {
                Debug.LogErrorFormat("UIJoyStick: {0}'s parent doesn't is Canvas.");
            }
        }
    }

    #region Input Value

    public float Get_JoyStick_X()
    {
        return v2_JoyStick_Touch.x;
    }

    public float Get_JoyStick_Y()
    {
        return v2_JoyStick_Touch.y;
    }

    #endregion

    #region Event Handler

    public void OnPointerDown(PointerEventData eventData)
    {
        if (com_Canvas.renderMode == RenderMode.ScreenSpaceCamera)
        {
            com_Camera = com_Canvas.worldCamera;
        }

        if (!b_Lock_Position)
        {
            Vector2 v2_JoyStick_Limit_Pos = RectTransformUtility.WorldToScreenPoint(com_Camera, com_JoyStick_Limit.position);
            Vector2 v2_JoyStick_Limit = (eventData.position - v2_JoyStick_Limit_Pos) / com_Canvas.scaleFactor;

            com_JoyStick_Limit.anchoredPosition = com_JoyStick_Limit.anchoredPosition + v2_JoyStick_Limit;
        }

        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 v2_JoyStick_Limit_Pos = RectTransformUtility.WorldToScreenPoint(com_Camera, com_JoyStick_Limit.position);
        Vector2 v2_JoyStick_Limit_Radius = com_JoyStick_Limit.sizeDelta / 2;

        v2_JoyStick_Touch = (eventData.position - v2_JoyStick_Limit_Pos) / (v2_JoyStick_Limit_Radius * com_Canvas.scaleFactor);

        if (b_Lock_X)
        {
            v2_JoyStick_Touch.x = 0;
        }

        if (b_Lock_Y)
        {
            v2_JoyStick_Touch.y = 0;
        }

        if (v2_JoyStick_Touch.magnitude > 0)
        {
            if (v2_JoyStick_Touch.magnitude > 1)
            {
                v2_JoyStick_Touch = v2_JoyStick_Touch.normalized;
            }
        }
        else
        {
            v2_JoyStick_Touch = Vector2.zero;
        }

        com_JoyStick_Button.anchoredPosition = v2_JoyStick_Touch * v2_JoyStick_Limit_Radius * 1;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        com_JoyStick_Button.anchoredPosition = Vector2.zero;

        if (b_Auto_Reset)
        {
            com_JoyStick_Limit.anchoredPosition = v2_JoyStick_Limit_Position_Primary;
        }
    }

    #endregion

    #region Lock JoyStick

    public void Set_Lock_X(bool b_Lock_X)
    {
        this.b_Lock_X = b_Lock_X;
    }

    public void Set_Lock_Y(bool b_Lock_Y)
    {
        this.b_Lock_Y = b_Lock_Y;
    }

    #endregion
}

//[System.Serializable]
//public class UnityEventPointer_JoyStick
//{
//    [Tooltip("Unity Hold-State Event Handle")]
//    [Space]
//    [SerializeField]
//    private UnityEvent Event_DragState;

//    [Tooltip("Unity Hold-State Event Handle")]
//    [Space]
//    [SerializeField]
//    private UnityEvent Event_HoldState;

//    [Tooltip("Unity Pointer Down Event Handle")]
//    [Space]
//    [SerializeField]
//    private UnityEvent Event_PointerDown;

//    [Tooltip("Unity Pointer Up Event Handle")]
//    [Space]
//    [SerializeField]
//    private UnityEvent Event_PointerUp;

//    [Tooltip("Unity Pointer Enter Event Handle")]
//    [Space]
//    [SerializeField]
//    private UnityEvent Event_PointerEnter;

//    [Tooltip("Unity Pointer Exit Event Handle")]
//    [Space]
//    [SerializeField]
//    private UnityEvent Event_PointerExit;
//}