using UnityEngine;
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

    [SerializeField] private bool b_Lock_X_L = false;

    [SerializeField] private bool b_Lock_X_R = false;

    [SerializeField] private bool b_Lock_Y_U = false;

    [SerializeField] private bool b_Lock_Y_D = false;

    [Header("Value")]

    [SerializeField] private Vector2 v2_JoyStick_Value_Primary;

    [SerializeField] private Vector2 v2_JoyStick_Value_Fixed;

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
                Debug.LogErrorFormat("{0}: This parent doesn't is Canvas.", name);
            }
        }
    }

    #region Input Value

    public Vector2 Get_JoyStick_Fixed()
    {
        return v2_JoyStick_Value_Fixed;
    }

    public float Get_JoyStick_Fixed_X()
    {
        return Get_JoyStick_Fixed().x;
    }

    public float Get_JoyStick_Fixed_Y()
    {
        return Get_JoyStick_Fixed().y;
    }

    public Vector2 Get_JoyStick_Primary()
    {
        return v2_JoyStick_Value_Primary;
    }

    public float Get_JoyStick_Primary_X()
    {
        return Get_JoyStick_Primary().x;
    }

    public float Get_JoyStick_PrimaryY_()
    {
        return Get_JoyStick_Primary().y;
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

        v2_JoyStick_Value_Primary = (eventData.position - v2_JoyStick_Limit_Pos) / (v2_JoyStick_Limit_Radius * com_Canvas.scaleFactor);

        //if (b_Lock_X)
        //{
        //    v2_JoyStick_Value_Primary.x = 0f;
        //}

        //if (b_Lock_Y)
        //{
        //    v2_JoyStick_Value_Primary.y = 0f;
        //}

        if (b_Lock_X_L && v2_JoyStick_Value_Primary.x < 0)
        {
            v2_JoyStick_Value_Primary.x = 0;
        }
        else
        if (b_Lock_X_R && v2_JoyStick_Value_Primary.x > 0)
        {
            v2_JoyStick_Value_Primary.x = 0;
        }

        if (b_Lock_Y_U && v2_JoyStick_Value_Primary.y > 0)
        {
            v2_JoyStick_Value_Primary.y = 0;
        }
        else
        if (b_Lock_Y_D && v2_JoyStick_Value_Primary.y < 0)
        {
            v2_JoyStick_Value_Primary.y = 0;
        }

        v2_JoyStick_Value_Fixed = v2_JoyStick_Value_Primary;

        if (v2_JoyStick_Value_Fixed.magnitude > 0)
        {
            if (v2_JoyStick_Value_Fixed.magnitude > 1)
            {
                v2_JoyStick_Value_Fixed = v2_JoyStick_Value_Fixed.normalized;
            }
        }
        else
        {
            v2_JoyStick_Value_Fixed = Vector2.zero;
        }

        com_JoyStick_Button.anchoredPosition = v2_JoyStick_Value_Fixed * v2_JoyStick_Limit_Radius * 1;
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

    public void Set_Lock_X_L(bool b_Lock_X_L)
    {
        this.b_Lock_X_L = b_Lock_X_L;
    }

    public void Set_Lock_X_R(bool b_Lock_X_R)
    {
        this.b_Lock_X_R = b_Lock_X_R;
    }

    public void Set_Lock_Y_U(bool b_Lock_Y_U)
    {
        this.b_Lock_Y_U = b_Lock_Y_U;
    }

    public void Set_Lock_Y_D(bool b_Lock_Y_D)
    {
        this.b_Lock_Y_D = b_Lock_Y_D;
    }

    public bool Get_Lock_X_L()
    {
        return this.b_Lock_X_L;
    }

    public bool Get_Lock_X_R()
    {
        return this.b_Lock_X_R;
    }

    public bool Get_Lock_Y_U()
    {
        return this.b_Lock_Y_U;
    }

    public bool Get_Lock_Y_D()
    {
        return this.b_Lock_Y_D;
    }

    #endregion
}