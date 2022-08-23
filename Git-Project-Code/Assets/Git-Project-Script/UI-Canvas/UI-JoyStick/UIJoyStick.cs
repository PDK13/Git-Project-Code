using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIJoyStick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [Header("UI")]

    [SerializeField] private RectTransform com_JoyStick_Limit;

    [SerializeField] private RectTransform com_JoyStick_Button;

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
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        com_Camera = null;

        if (com_Canvas.renderMode == RenderMode.ScreenSpaceCamera)
        {
            com_Camera = com_Canvas.worldCamera;
        }

        Vector2 v2_JoyStick_Limit_Pos = RectTransformUtility.WorldToScreenPoint(com_Camera, com_JoyStick_Limit.position);
        Vector2 v2_JoyStick_Limit_Radius = com_JoyStick_Limit.sizeDelta / 2;

        v2_JoyStick_Touch = (eventData.position - v2_JoyStick_Limit_Pos) / (v2_JoyStick_Limit_Radius * com_Canvas.scaleFactor);

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