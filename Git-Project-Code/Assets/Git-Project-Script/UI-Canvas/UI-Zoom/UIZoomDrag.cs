using UnityEngine;

using UnityEngine.EventSystems;

public class UIZoomDrag : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    [SerializeField] private float m_Zoom_Speed = 0.01f;

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
            Touch t_Touch_0 = Input.GetTouch(0);
            Touch t_Touch_1 = Input.GetTouch(1);

            Vector2 v2_Touch_0_Prev_Pos = t_Touch_0.position - t_Touch_0.deltaPosition;
            Vector2 v2_Touch_1_Prev_Pos = t_Touch_1.position - t_Touch_1.deltaPosition;

            float m_Prev_Duration = (v2_Touch_0_Prev_Pos - v2_Touch_1_Prev_Pos).magnitude;
            float m_Cur_Duration = (t_Touch_0.position - t_Touch_1.position).magnitude;

            float m_Difference = m_Cur_Duration - m_Prev_Duration;

            SetZoom(m_Difference * m_Zoom_Speed);
        }
    }

    private void SetZoom(float m_Increment)
    {
        if (Camera.main.orthographic)
        {
            Camera.main.orthographicSize = Camera.main.orthographicSize - m_Increment;
        }
        else
        {
            Camera.main.fieldOfView = Camera.main.fieldOfView - m_Increment;
        }
    }
}
