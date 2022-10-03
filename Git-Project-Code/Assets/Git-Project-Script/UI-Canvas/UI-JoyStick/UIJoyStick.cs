using UnityEngine;
using UnityEngine.EventSystems;

public class UIJoyStick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [Header("UI")]

    [SerializeField] private bool mAllowLockPos = true;

    [SerializeField] private bool mAllowAutoReset = true;

    [SerializeField] private RectTransform com_JoyStickLimit;

    private Vector2 v2_JoyStickLimitPosPrimary;

    [SerializeField] private RectTransform com_JoyStickButton;

    [Header("Lock")]

    [SerializeField] private bool mAllowLockXL = false;

    [SerializeField] private bool mAllowLockXR = false;

    [SerializeField] private bool mAllowLockYU = false;

    [SerializeField] private bool mAllowLockYD = false;

    [Header("Value")]

    [SerializeField] private Vector2 v2_JoyStickValuePrimary;

    [SerializeField] private Vector2 v2_JoyStickValue_Fixed;

    private Canvas com_Canvas;

    private Camera com_Camera;

    private void Start()
    {
        Vector2 center = new Vector2(0.5f, 0.5f);
        com_JoyStickLimit.pivot = center;
        com_JoyStickButton.anchorMin = center;
        com_JoyStickButton.anchorMax = center;
        com_JoyStickButton.pivot = center;
        com_JoyStickButton.anchoredPosition = Vector2.zero;

        v2_JoyStickLimitPosPrimary = com_JoyStickLimit.GetComponent<RectTransform>().anchoredPosition;

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

    public Vector2 GetJoyStickFixed()
    {
        return v2_JoyStickValue_Fixed;
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
        return v2_JoyStickValuePrimary;
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
        if (com_Canvas.renderMode == RenderMode.ScreenSpaceCamera)
        {
            com_Camera = com_Canvas.worldCamera;
        }

        if (!mAllowLockPos)
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

        v2_JoyStickValuePrimary = (eventData.position - v2_JoyStickLimit_Pos) / (v2_JoyStickLimitRadius * com_Canvas.scaleFactor);

        //if (m_LockX)
        //{
        //    v2_JoyStickValuePrimary.x = 0f;
        //}

        //if (m_LockY)
        //{
        //    v2_JoyStickValuePrimary.y = 0f;
        //}

        if (mAllowLockXL && v2_JoyStickValuePrimary.x < 0)
        {
            v2_JoyStickValuePrimary.x = 0;
        }
        else
        if (mAllowLockXR && v2_JoyStickValuePrimary.x > 0)
        {
            v2_JoyStickValuePrimary.x = 0;
        }

        if (mAllowLockYU && v2_JoyStickValuePrimary.y > 0)
        {
            v2_JoyStickValuePrimary.y = 0;
        }
        else
        if (mAllowLockYD && v2_JoyStickValuePrimary.y < 0)
        {
            v2_JoyStickValuePrimary.y = 0;
        }

        v2_JoyStickValue_Fixed = v2_JoyStickValuePrimary;

        if (v2_JoyStickValue_Fixed.magnitude > 0)
        {
            if (v2_JoyStickValue_Fixed.magnitude > 1)
            {
                v2_JoyStickValue_Fixed = v2_JoyStickValue_Fixed.normalized;
            }
        }
        else
        {
            v2_JoyStickValue_Fixed = Vector2.zero;
        }

        com_JoyStickButton.anchoredPosition = v2_JoyStickValue_Fixed * v2_JoyStickLimitRadius * 1;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        com_JoyStickButton.anchoredPosition = Vector2.zero;

        if (mAllowAutoReset)
        {
            com_JoyStickLimit.anchoredPosition = v2_JoyStickLimitPosPrimary;
        }
    }

    #endregion

    #region Lock JoyStick

    public void SetLockXL(bool mAllowLockXL)
    {
        this.mAllowLockXL = mAllowLockXL;
    }

    public void SetLockXR(bool mAllowLockXR)
    {
        this.mAllowLockXR = mAllowLockXR;
    }

    public void SetLockYU(bool mAllowLockYU)
    {
        this.mAllowLockYU = mAllowLockYU;
    }

    public void SetLockYD(bool mAllowLockYD)
    {
        this.mAllowLockYD = mAllowLockYD;
    }

    public bool GetCheckLockXL()
    {
        return mAllowLockXL;
    }

    public bool GetCheckLockXR()
    {
        return mAllowLockXR;
    }

    public bool GetCheckLockYU()
    {
        return mAllowLockYU;
    }

    public bool GetCheckLockYD()
    {
        return mAllowLockYD;
    }

    #endregion
}