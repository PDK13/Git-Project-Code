using UnityEngine;
using UnityEngine.EventSystems;

public class UIJoyStick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [Header("UI")]

    [SerializeField] private bool m_AllowLockPos = true;

    [SerializeField] private bool m_AllowAutoReset = true;

    [SerializeField] private RectTransform m_JoyStickLimit;

    private Vector2 m_JoyStickLimitPosPrimary;

    [SerializeField] private RectTransform m_JoyStickButton;

    [Header("Lock")]

    [SerializeField] private bool m_AllowLockXL = false;

    [SerializeField] private bool m_AllowLockXR = false;

    [SerializeField] private bool m_AllowLockYU = false;

    [SerializeField] private bool m_AllowLockYD = false;

    [Header("Value")]

    [SerializeField] private Vector2 m_JoyStickValuePrimary;

    [SerializeField] private Vector2 m_JoyStickValue_Fixed;

    private Canvas m_Canvas;

    private Camera m_Camera;

    private void Start()
    {
        Vector2 center = new Vector2(0.5f, 0.5f);
        m_JoyStickLimit.pivot = center;
        m_JoyStickButton.anchorMin = center;
        m_JoyStickButton.anchorMax = center;
        m_JoyStickButton.pivot = center;
        m_JoyStickButton.anchoredPosition = Vector2.zero;

        m_JoyStickLimitPosPrimary = m_JoyStickLimit.GetComponent<RectTransform>().anchoredPosition;

        if (m_Canvas == null)
        {
            m_Canvas = GetComponentInParent<Canvas>();

            if (m_Canvas == null)
            {
                Debug.LogErrorFormat("{0}: This parent doesn't is Canvas.", name);
            }
        }
    }

    #region Input Value

    public Vector2 GetJoyStickFixed()
    {
        return m_JoyStickValue_Fixed;
    }

    public float GetJoyStickFixedX()
    {
        return GetJoyStickFixed().x;
    }

    public float GetJoyStickFixedY()
    {
        return GetJoyStickFixed().y;
    }

    public Vector2 GetJoyStickPrimary()
    {
        return m_JoyStickValuePrimary;
    }

    public float GetJoyStickPrimaryX()
    {
        return GetJoyStickPrimary().x;
    }

    public float GetJoyStickPrimaryY()
    {
        return GetJoyStickPrimary().y;
    }

    #endregion

    #region Event Handler

    public void OnPointerDown(PointerEventData eventData)
    {
        if (m_Canvas.renderMode == RenderMode.ScreenSpaceCamera)
        {
            m_Camera = m_Canvas.worldCamera;
        }

        if (!m_AllowLockPos)
        {
            Vector2 m_JoyStickLimit_Pos = RectTransformUtility.WorldToScreenPoint(m_Camera, this.m_JoyStickLimit.position);
            Vector2 m_JoyStickLimit = (eventData.position - m_JoyStickLimit_Pos) / m_Canvas.scaleFactor;

            this.m_JoyStickLimit.anchoredPosition = this.m_JoyStickLimit.anchoredPosition + m_JoyStickLimit;
        }

        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 m_JoyStickLimit_Pos = RectTransformUtility.WorldToScreenPoint(m_Camera, m_JoyStickLimit.position);
        Vector2 m_JoyStickLimitRadius = m_JoyStickLimit.sizeDelta / 2;

        m_JoyStickValuePrimary = (eventData.position - m_JoyStickLimit_Pos) / (m_JoyStickLimitRadius * m_Canvas.scaleFactor);

        //if (m_LockX)
        //{
        //    m_JoyStickValuePrimary.x = 0f;
        //}

        //if (m_LockY)
        //{
        //    m_JoyStickValuePrimary.y = 0f;
        //}

        if (m_AllowLockXL && m_JoyStickValuePrimary.x < 0)
        {
            m_JoyStickValuePrimary.x = 0;
        }
        else
        if (m_AllowLockXR && m_JoyStickValuePrimary.x > 0)
        {
            m_JoyStickValuePrimary.x = 0;
        }

        if (m_AllowLockYU && m_JoyStickValuePrimary.y > 0)
        {
            m_JoyStickValuePrimary.y = 0;
        }
        else
        if (m_AllowLockYD && m_JoyStickValuePrimary.y < 0)
        {
            m_JoyStickValuePrimary.y = 0;
        }

        m_JoyStickValue_Fixed = m_JoyStickValuePrimary;

        if (m_JoyStickValue_Fixed.magnitude > 0)
        {
            if (m_JoyStickValue_Fixed.magnitude > 1)
            {
                m_JoyStickValue_Fixed = m_JoyStickValue_Fixed.normalized;
            }
        }
        else
        {
            m_JoyStickValue_Fixed = Vector2.zero;
        }

        m_JoyStickButton.anchoredPosition = m_JoyStickValue_Fixed * m_JoyStickLimitRadius * 1;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        m_JoyStickButton.anchoredPosition = Vector2.zero;

        if (m_AllowAutoReset)
        {
            m_JoyStickLimit.anchoredPosition = m_JoyStickLimitPosPrimary;
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