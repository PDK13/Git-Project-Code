using UnityEngine;

public class Sample_BackgroundMove : MonoBehaviour
{
    [Header("Camera")]

    [SerializeField] private Transform m_Camera;

    private Vector2 v2_MoveDir;

    [Header("Keyboard")]

    [SerializeField] private KeyCode m_KeyMoveU = KeyCode.W;
    [SerializeField] private KeyCode m_KeyMoveD = KeyCode.S;
    [SerializeField] private KeyCode m_KeyMoveL = KeyCode.A;
    [SerializeField] private KeyCode m_KeyMoveR = KeyCode.D;

    [Header("Speed")]

    [SerializeField] private float m_MoveSpeed = 0.01f;

    [SerializeField] private float m_MoveSpeedMax = 1f;

    private void Start()
    {
        if (m_Camera == null)
        {
            m_Camera = Camera.main.transform;
        }
    }

    private void Update()
    {
        if (Input.GetKey(m_KeyMoveU))
        {
            if (v2_MoveDir.y < m_MoveSpeedMax)
            {
                v2_MoveDir.y += m_MoveSpeed;
            }
        }
        else
        if (Input.GetKey(m_KeyMoveD))
        {
            if (v2_MoveDir.y > -m_MoveSpeedMax)
            {
                v2_MoveDir.y -= m_MoveSpeed;
            }
        }
        else
        {
            v2_MoveDir.y = 0;
        }

        if (Input.GetKey(m_KeyMoveL))
        {
            if (v2_MoveDir.x > -m_MoveSpeedMax)
            {
                v2_MoveDir.x -= m_MoveSpeed;
            }
        }
        else
        if (Input.GetKey(m_KeyMoveR))
        {
            if (v2_MoveDir.x < m_MoveSpeedMax)
            {
                v2_MoveDir.x += m_MoveSpeed;
            }
        }
        else
        {
            v2_MoveDir.x = 0;
        }
    }

    private void FixedUpdate()
    {
        m_Camera.transform.position = m_Camera.transform.position + (Vector3)v2_MoveDir;
    }
}
