using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(RigidbodyComponent))]

public class RigidbodyRotation : MonoBehaviour
{
    private RigidbodyComponent m_Rigid;

    [Header("Keyboard")]

    [SerializeField] private bool m_AllowLockControl = false;

    [SerializeField] private KeyCode m_KeyMoveForward = KeyCode.UpArrow;

    [SerializeField] private KeyCode m_KeyMoveBackward = KeyCode.DownArrow;

    [SerializeField] private KeyCode m_KeyTurnL = KeyCode.LeftArrow;

    [SerializeField] private KeyCode m_KeyTurnR = KeyCode.RightArrow;

    [SerializeField] private bool m_AllowMutiButton = true;

    [SerializeField] private KeyCode m_KeySpeedChance = KeyCode.LeftShift;

    [Header("Move")]

    [SerializeField] private float m_SpeedNormal = 7f;

    [SerializeField] private float m_SpeedChance = 10f;

    private float m_SpeedCur;

    [Header("Rotate")]

    [Range(0.1f, 5f)]
    [SerializeField] private float m_SpeedRotate = 1f;

    [SerializeField] private bool m_AllowStopRAway = false;

    //[SerializeField] private float m_SpeedStop = 3f;

    [SerializeField] private bool m_AllowSlowWhenTurn = true;

    [SerializeField] private float m_SpeedSlow = 5f;

    private void Awake()
    {
        m_Rigid = GetComponent<RigidbodyComponent>();
    }

    private void Update()
    {
        if (m_AllowLockControl)
        {
            return;
        }

        SetControlMain_Keyboard();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;

        RigidbodyComponent m_Rigid = GetComponent<RigidbodyComponent>();

        Gizmos.DrawLine(
            transform.position,
            transform.position + ClassVector.GetPosOnCircleXZ(-m_Rigid.GetRotationXZ(), 1f));
        Gizmos.DrawWireSphere(
            transform.position + ClassVector.GetPosOnCircleXZ(-m_Rigid.GetRotationXZ(), 1f),
            0.1f);
    }

    #region Control main

    private void SetControlMain_Keyboard()
    {
        m_SpeedCur = (Input.GetKey(m_KeySpeedChance)) ? m_SpeedChance : m_SpeedNormal;

        if (Input.GetKey(m_KeyTurnL) && Input.GetKey(m_KeyTurnR) ||
            Input.GetKey(m_KeyMoveForward) && Input.GetKey(m_KeyMoveBackward))
        {
            //
            return;
        }

        if (m_AllowMutiButton)
        {
            if (Input.GetKey(m_KeyTurnL))
            {
                SetContromRotate(-1);
            }
            else
            if (Input.GetKey(m_KeyTurnR)) 
            {
                SetContromRotate(1);
            }

            if (Input.GetKey(m_KeyMoveForward))
            {
                SetControlMove(1);
            }
            else
            if (Input.GetKey(m_KeyMoveBackward))
            {
                SetControlMove(-1);
            }
            else
            {
                SetControlMoveStop();
            }
        }
        else
        {
            if (Input.GetKey(m_KeyTurnL))
            {
                SetContromRotate(-1);
            }
            else
            if (Input.GetKey(m_KeyTurnR))
            {
                SetContromRotate(1);
            }
            else
            if (Input.GetKey(m_KeyMoveForward))
            {
                SetControlMove(1);
            }
            else
            if (Input.GetKey(m_KeyMoveBackward))
            {
                SetControlMove(-1);
            }
            else
            {
                SetControlMoveStop();
            }
        }



        SetControlMoveSlow();
    }

    private void SetControlMove(int m_MoveDir)
    {
        m_Rigid.SetMoveRotationXZ(m_Rigid.GetRotationXZ(), m_SpeedCur * m_MoveDir);
    }

    private void SetControlMoveStop()
    {
        if (m_AllowStopRAway)
        {
            m_Rigid.SetStopX_Velocity();
            m_Rigid.SetStopZ_Velocity();
        }
    }

    private void SetControlMoveSlow()
    {
        m_Rigid.SetStopX_Velocity(m_SpeedSlow);
        m_Rigid.SetStopZ_Velocity(m_SpeedSlow);
    }

    private void SetContromRotate(int m_RotationDir)
    {
        if (m_AllowSlowWhenTurn)
        {
            SetControlMoveSlow();
        }

        m_Rigid.SetRotationChanceXZ(m_SpeedRotate * m_RotationDir);
    }

    #endregion

    #region Control is Lock

    public void SetControlLock(bool m_AllowLockControl)
    {
        this.m_AllowLockControl = m_AllowLockControl;
    }

    public bool GetCheckControlLock()
    {
        return m_AllowLockControl;
    }

    #endregion
}
