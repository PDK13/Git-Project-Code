using System.Collections.Generic;
using UnityEngine;

public class Canvam_Camera : MonoBehaviour
{
    [Header("Camera")]
    public KeyCode k_Next = KeyCode.RightBracket;
    //Keycode: ]
    public KeyCode k_Back = KeyCode.LeftBracket;
    //Keycode: [
    //Chance Camera button

    public List<GameObject> l1_Camera = new List<GameObject>();
    //List of Camera (Start at First Camera)
    private int m_Camera = 0;
    //Index of Camera in List is Enable

    private Canvas c_Canvas;

    private void Awake()
    {
        c_Canvas = GetComponent<Canvas>();

        SetCameraEnable();
    }

    private void Update()
    {
        if (Input.GetKeyDown(k_Next))
        {
            m_Camera++;
            if (m_Camera > l1_Camera.Count - 1)
            {
                m_Camera = 0;
            }

            SetCameraEnable();
        }
        else
        if (Input.GetKeyDown(k_Back))
        {
            m_Camera--;
            if (m_Camera < 0)
            {
                m_Camera = l1_Camera.Count - 1;
            }

            SetCameraEnable();
        }
    }

    private void SetCameraEnable()
    {
        c_Canvas.worldCamera = l1_Camera[m_Camera].GetComponent<Camera>();
        l1_Camera[m_Camera].SetActive(true);

        for (int i = 0; i < l1_Camera.Count; i++)
        {
            if (i != m_Camera)
            {
                l1_Camera[i].SetActive(false);
            }
        }
    }

}
