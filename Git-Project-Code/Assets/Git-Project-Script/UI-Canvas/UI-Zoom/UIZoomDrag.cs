using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;

public class UIZoomDrag : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    [SerializeField] private float f_Zoom_Speed = 0.01f;

#if UNITY_EDITOR

    public void Update()
    {
        Set_Zoom(Input.GetAxis("Mouse ScrollWheel") * 100f * f_Zoom_Speed);
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

            float f_Prev_Duration = (v2_Touch_0_Prev_Pos - v2_Touch_1_Prev_Pos).magnitude;
            float f_Cur_Duration = (t_Touch_0.position - t_Touch_1.position).magnitude;

            float f_Difference = f_Cur_Duration - f_Prev_Duration;

            Set_Zoom(f_Difference * f_Zoom_Speed);
        }
    }

    private void Set_Zoom(float f_Increment)
    {
        if (Camera.main.orthographic)
        {
            Camera.main.orthographicSize = Camera.main.orthographicSize - f_Increment;
        }
        else
        {
            Camera.main.fieldOfView = Camera.main.fieldOfView - f_Increment;
        }
        
    }
}
