using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample_BackgroundMove : MonoBehaviour
{
    [Header("Camera")]

    [SerializeField] private Transform com_Camera;

    private Vector2 v2_Move_Dir;

    [Header("Keyboard")]

    [SerializeField] KeyCode k_Move_U = KeyCode.W;
    [SerializeField] KeyCode k_Move_D = KeyCode.S;
    [SerializeField] KeyCode k_Move_L = KeyCode.A;
    [SerializeField] KeyCode k_Move_R = KeyCode.D;

    [Header("Speed")]

    [SerializeField] float f_Move_Speed = 0.01f;

    [SerializeField] float f_Move_Speed_Max = 1f;

    private void Start()
    {
        if (com_Camera == null)
        {
            com_Camera = Camera.main.transform;
        }
    }

    private void Update()
    {
        if (Input.GetKey(k_Move_U))
        {
            if (v2_Move_Dir.y < f_Move_Speed_Max)
            {
                v2_Move_Dir.y += f_Move_Speed;
            }
        }
        else
        if (Input.GetKey(k_Move_D))
        {
            if (v2_Move_Dir.y > -f_Move_Speed_Max)
            {
                v2_Move_Dir.y -= f_Move_Speed;
            }
        }
        else
        {
            v2_Move_Dir.y = 0;
        }

        if (Input.GetKey(k_Move_L))
        {
            if (v2_Move_Dir.x > -f_Move_Speed_Max)
            {
                v2_Move_Dir.x -= f_Move_Speed;
            }
        }
        else
        if (Input.GetKey(k_Move_R))
        {
            if (v2_Move_Dir.x < f_Move_Speed_Max)
            {
                v2_Move_Dir.x += f_Move_Speed;
            }
        }
        else
        {
            v2_Move_Dir.x = 0;
        }
    }

    private void FixedUpdate()
    {
        com_Camera.transform.position = com_Camera.transform.position + (Vector3)v2_Move_Dir;
    }
}
