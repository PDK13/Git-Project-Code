using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsoEditorCamera : MonoBehaviour
{
    [SerializeField]
    private Camera c_Camera;

    private void Start()
    {
        //c_Camera.orthographicSize = 10;
    }

    public void Button_CameraZoom_Inc()
    {
        c_Camera.orthographicSize++;
    }

    public void Button_CameraZoom_Dec()
    {
        if (c_Camera.orthographicSize > 1)
        {
            c_Camera.orthographicSize--;
        }
    }
}
