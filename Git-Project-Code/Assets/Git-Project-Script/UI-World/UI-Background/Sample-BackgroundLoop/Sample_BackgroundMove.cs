using UnityEngine;

public class Sample_BackgroundMove : MonoBehaviour
{
    [Header("Camera")]

    [SerializeField] private Transform m_Camera;

    private Vector2 m_MoveDir;

    [Header("Keyboard")]

    [SerializeField] private KeyCode m_KeyMoveU = KeyCode.W;
    [SerializeField] private KeyCode m_KeyMoveD = KeyCode.S;
    [SerializeField] private KeyCode m_KeyMoveL = KeyCode.A;
    [SerializeField] private KeyCode m_KeyMoveR = KeyCode.D;

    [SerializeField] private KeyCode m_KeyZoomIn = KeyCode.PageUp;
    [SerializeField] private KeyCode m_KeyZoomOut = KeyCode.PageDown;

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
            if (m_MoveDir.y < m_MoveSpeedMax)
            {
                m_MoveDir.y += m_MoveSpeed;
            }
        }
        else
        if (Input.GetKey(m_KeyMoveD))
        {
            if (m_MoveDir.y > -m_MoveSpeedMax)
            {
                m_MoveDir.y -= m_MoveSpeed;
            }
        }
        else
        {
            m_MoveDir.y = 0;
        }

        if (Input.GetKey(m_KeyMoveL))
        {
            if (m_MoveDir.x > -m_MoveSpeedMax)
            {
                m_MoveDir.x -= m_MoveSpeed;
            }
        }
        else
        if (Input.GetKey(m_KeyMoveR))
        {
            if (m_MoveDir.x < m_MoveSpeedMax)
            {
                m_MoveDir.x += m_MoveSpeed;
            }
        }
        else
        {
            m_MoveDir.x = 0;
        }

        if (Input.GetKeyDown(m_KeyZoomIn))
        {
            m_Camera.GetComponent<Camera>().orthographicSize = m_Camera.GetComponent<Camera>().orthographicSize + 2f;
        }

        if (Input.GetKeyDown(m_KeyZoomOut))
        {
            m_Camera.GetComponent<Camera>().orthographicSize = m_Camera.GetComponent<Camera>().orthographicSize - 2f;
        }
    }

    private void FixedUpdate()
    {
        m_Camera.transform.position = m_Camera.transform.position + (Vector3)m_MoveDir;
    }
}
