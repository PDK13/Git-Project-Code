using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(RigidbodyComponent))]

public class RigidbodyPlatform : MonoBehaviour
//Move Control Platform (X)
{
    private RigidbodyComponent m_Rigid;

    [Header("Keyboard")]

    public KeyCode m_KeyMoveL = KeyCode.LeftArrow;

    public KeyCode m_KeyMoveR = KeyCode.RightArrow;

    public KeyCode m_KeySpeedChance = KeyCode.LeftShift;

    [Header("Move")]

    public float m_SpeedNormal = 2f;

    public float m_SpeedChance = 5f;

    private float m_SpeedCur;

    public bool m_AllowStopRAway = false;

    public float m_SpeedStop = 3f;

    private void Awake()
    {
        m_Rigid = GetComponent<RigidbodyComponent>();
    }

    private void Update()
    {
        SetSpeedChance();
        SetMoveButton();
    }

    public void SetMoveButton()
    {
        if (Input.GetKey(m_KeyMoveL) && Input.GetKey(m_KeyMoveR))
        {
            //Press "L" & "R" same time >> Not m_ove
            SetStop();
        }
        else
            if (Input.GetKey(m_KeyMoveL))
        {
            //L
            SetMove(-1);
        }
        else
            if (Input.GetKey(m_KeyMoveR))
        {
            //R
            SetMove(1);
        }
        else
        {
            //Not Press
            SetStop();
        }
    }

    private void SetMove(int m_ButtonMove)
    {
        m_Rigid.SetMoveX_Velocity(m_ButtonMove, m_SpeedCur, m_SpeedCur);
    }

    private void SetStop()
    {
        if (m_AllowStopRAway)
        {
            m_Rigid.SetStopX_Velocity();
        }
        else
        {
            m_Rigid.SetStopX_Velocity(m_SpeedStop);
        }
    }

    public void SetSpeedChance()
    {
        m_SpeedCur = (Input.GetKey(m_KeySpeedChance)) ? m_SpeedChance : m_SpeedNormal;
    }
}