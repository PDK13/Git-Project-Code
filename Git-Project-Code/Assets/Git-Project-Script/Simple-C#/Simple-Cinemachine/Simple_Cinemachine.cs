using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simple_Cinemachine : MonoBehaviour
{
    //This will automatic make camera move follow a target!

    public Transform m_Player;

    public float m_SpeedMove = 0.1f;

    public CinemachineVirtualCamera m_CameraCinemachine;

    public float m_SpeedZoom = 1f;

    public PolygonCollider2D m_WorldLimit;

    private void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
            m_Player.position = m_Player.transform.position + Vector3.up * m_SpeedMove;
        if (Input.GetKey(KeyCode.DownArrow))
            m_Player.position = m_Player.transform.position + Vector3.down * m_SpeedMove;
        if (Input.GetKey(KeyCode.LeftArrow))
            m_Player.position = m_Player.transform.position + Vector3.left * m_SpeedMove;
        if (Input.GetKey(KeyCode.RightArrow))
            m_Player.position = m_Player.transform.position + Vector3.right * m_SpeedMove;

        if (Input.GetKeyDown(KeyCode.PageUp))
            m_CameraCinemachine.m_Lens.OrthographicSize = m_CameraCinemachine.m_Lens.OrthographicSize + m_SpeedZoom;
        if (Input.GetKeyDown(KeyCode.PageDown))
            m_CameraCinemachine.m_Lens.OrthographicSize = m_CameraCinemachine.m_Lens.OrthographicSize - m_SpeedZoom;
    }

    private void OnDrawGizmos()
    {
        if (m_WorldLimit != null)
        {
            GitGizmos.GizmosPolygonCollider2D(m_WorldLimit, Color.red);
        }

        if (Camera.main != null)
        {
            GitGizmos.GizmosCamera(Camera.main, Color.yellow);
        }
    }
}
