using UnityEngine;

using UnityEngine.EventSystems;

public class UIZoomDrag : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    [SerializeField] private float m_Zoom_Speed = 0.01f;

    private float m_Increment = 0;

#if UNITY_EDITOR

    public void Update()
    {
        SetZoom(Input.GetAxis("Mouse ScrollWheel") * 100f * m_Zoom_Speed);
    }

#endif

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Input.touchCount == 2)
        {
            Touch tuch_0 = Input.GetTouch(0);
            Touch tuch_1 = Input.GetTouch(1);

            Vector2 v2uch_0_Prev_Pos = tuch_0.position - tuch_0.deltaPosition;
            Vector2 v2uch_1_Prev_Pos = tuch_1.position - tuch_1.deltaPosition;

            float m_PrevDuration = (v2uch_0_Prev_Pos - v2uch_1_Prev_Pos).magnitude;
            float m_CurrentDuration = (tuch_0.position - tuch_1.position).magnitude;

            float m_Difference = m_CurrentDuration - m_PrevDuration;

            SetZoom(m_Difference * m_Zoom_Speed);
        }
    }

    private void SetZoom(float m_Increment)
    {
        this.m_Increment = m_Increment;

        if (Camera.main.orthographic)
        {
            Camera.main.orthographicSize = Camera.main.orthographicSize - m_Increment;
        }
        else
        {
            Camera.main.fieldOfView = Camera.main.fieldOfView - m_Increment;
        }
    }

    public float GetIncrement()
    {
        return m_Increment;
    }
}
