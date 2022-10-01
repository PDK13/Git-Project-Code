using UnityEngine;
using UnityEngine.EventSystems;

public class UIJoyStick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [Header("UI")]

    [SerializeField] private bool m_AllowLockPos = true;

    [SerializeField] private bool m_AllowAutoReset = true;

    [SerializeField] private RectTransform com_JoyStickLimit;

    private Vector2 v2_JoyStickLimitPos_Primary;

    [SerializeField] private RectTransform com_JoySticm_KeyButton;

    [Header("Lock")]

    [SerializeField] private bool m_AllowLockXL = false;

    [SerializeField] private bool m_AllowLockXR = false;

    [SerializeField] private bool m_AllowLockYU = false;

    [SerializeField] private bool m_AllowLockYD = false;

    [Header("Value")]

    [SerializeField] private Vector2 v2_JoySticm_KeyValue_Primary;

    [SerializeField] private Vector2 v2_JoySticm_KeyValue_Fixed;

    private Canvas com_Canvas;

    private Camera com_Camera;

    private void Start()
    {
        Vector2 center = new Vector2(0.5f, 0.5f);
        com_JoyStickLimit.pivot = center;
        com_JoySticm_KeyButton.anchorMin = center;
        com_JoySticm_KeyButton.anchorMax = center;
        com_JoySticm_KeyButton.pivot = center;
        com_JoySticm_KeyButton.anchoredPosition = Vector2.zero;

        v2_JoyStickLimitPos_Primary = com_JoyStickLimit.GetComponent<RectTransform>().anchoredPosition;

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

    public Vector2 GetJoySticm_KeyFixed()
    {
        return v2_JoySticm_KeyValue_Fixed;
    }

    public float GetJoySticm_KeyFixedX()
    {
        return GetJoySticm_KeyFixed().x;
    }

    public float GetJoySticm_KeyFixedY()
    {
        return GetJoySticm_KeyFixed().y;
    }

    public Vector2 GetJoySticm_KeyPrimary()
    {
        return v2_JoySticm_KeyValue_Primary;
    }

    public float GetJoySticm_KeyPrimaryX()
    {
        return GetJoySticm_KeyPrimary().x;
    }

    public float GetJoySticm_KeyPrimaryY_()
    {
        return GetJoySticm_KeyPrimary().y;
    }

    #endregion

    #region Event Handler

    public void OnPointerDown(PointerEventData eventData)
    {
        if (com_Canvas.renderMode == RenderMode.ScreenSpaceCamera)
        {
            com_Camera = com_Canvas.worldCamera;
        }

        if (!m_AllowLockPos)
        {
            Vector2 v2_JoyStickLimit_Pos = RectTransformUtility.WorldToScreenPoint(com_Camera, com_JoyStickLimit.position);
            Vector2 v2_JoyStickLimit = (eventData.position - v2_JoyStickLimit_Pos) / com_Canvas.scaleFactor;

            com_JoyStickLimit.anchoredPosition = com_JoyStickLimit.anchoredPosition + v2_JoyStickLimit;
        }

        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 v2_JoyStickLimit_Pos = RectTransformUtility.WorldToScreenPoint(com_Camera, com_JoyStickLimit.position);
        Vector2 v2_JoyStickLimitRadius = com_JoyStickLimit.sizeDelta / 2;

        v2_JoySticm_KeyValue_Primary = (eventData.position - v2_JoyStickLimit_Pos) / (v2_JoyStickLimitRadius * com_Canvas.scaleFactor);

        //if (m_LockX)
        //{
        //    v2_JoySticm_KeyValue_Primary.x = 0f;
        //}

        //if (m_LockY)
        //{
        //    v2_JoySticm_KeyValue_Primary.y = 0f;
        //}

        if (m_AllowLockXL && v2_JoySticm_KeyValue_Primary.x < 0)
        {
            v2_JoySticm_KeyValue_Primary.x = 0;
        }
        else
        if (m_AllowLockXR && v2_JoySticm_KeyValue_Primary.x > 0)
        {
            v2_JoySticm_KeyValue_Primary.x = 0;
        }

        if (m_AllowLockYU && v2_JoySticm_KeyValue_Primary.y > 0)
        {
            v2_JoySticm_KeyValue_Primary.y = 0;
        }
        else
        if (m_AllowLockYD && v2_JoySticm_KeyValue_Primary.y < 0)
        {
            v2_JoySticm_KeyValue_Primary.y = 0;
        }

        v2_JoySticm_KeyValue_Fixed = v2_JoySticm_KeyValue_Primary;

        if (v2_JoySticm_KeyValue_Fixed.magnitude > 0)
        {
            if (v2_JoySticm_KeyValue_Fixed.magnitude > 1)
            {
                v2_JoySticm_KeyValue_Fixed = v2_JoySticm_KeyValue_Fixed.normalized;
            }
        }
        else
        {
            v2_JoySticm_KeyValue_Fixed = Vector2.zero;
        }

        com_JoySticm_KeyButton.anchoredPosition = v2_JoySticm_KeyValue_Fixed * v2_JoyStickLimitRadius * 1;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        com_JoySticm_KeyButton.anchoredPosition = Vector2.zero;

        if (m_AllowAutoReset)
        {
            com_JoyStickLimit.anchoredPosition = v2_JoyStickLimitPos_Primary;
        }
    }

    #endregion

    #region Lock JoyStick

    public void SetLockXL(bool m_AllowLockXL)
    {
        this.m_AllowLockXL = m_AllowLockXL;
    }

    public void SetLockXR(bool m_AllowLockXR)
    {
        this.m_AllowLockXR = m_AllowLockXR;
    }

    public void SetLockYU(bool m_AllowLockYU)
    {
        this.m_AllowLockYU = m_AllowLockYU;
    }

    public void SetLockYD(bool m_AllowLockYD)
    {
        this.m_AllowLockYD = m_AllowLockYD;
    }

    public bool GetCheckLockXL()
    {
        return m_AllowLockXL;
    }

    public bool GetCheckLockXR()
    {
        return m_AllowLockXR;
    }

    public bool GetCheckLockYU()
    {
        return m_AllowLockYU;
    }

    public bool GetCheckLockYD()
    {
        return m_AllowLockYD;
    }

    #endregion
}