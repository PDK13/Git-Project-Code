using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(RigidbodyComponent))]

public class RigidbodyRotation : MonoBehaviour
//Move Control Surface (X & Z & Rotation)
{
    private RigidbodyComponent cm_Rigid;

    [Header("Keyboard")]

    [SerializeField] private bool m_LockControl = false;

    [SerializeField] private KeyCode k_MoveForward = KeyCode.UpArrow;

    [SerializeField] private KeyCode k_MoveBackward = KeyCode.DownArrow;

    [SerializeField] private KeyCode k_TurnLeft = KeyCode.LeftArrow;

    [SerializeField] private KeyCode k_TurnRight = KeyCode.RightArrow;

    [SerializeField] private bool m_MutiButton = true;

    [SerializeField] private KeyCode k_SpeedChance = KeyCode.LeftShift;

    [Header("Move")]

    [SerializeField] private float m_SpeedNormal = 7f;

    [SerializeField] private float m_SpeedChance = 10f;

    private float m_SpeedCur;

    [Header("Rotate")]

    [Range(0.1f, 5f)]
    [SerializeField] private float m_SpeedRotate = 1f;

    [SerializeField] private bool m_StopRightAway = false;

    [SerializeField] private float m_SpeedStop = 3f;

    [SerializeField] private bool m_SlowWhenTurn = true;

    [SerializeField] private float m_SpeedSlow = 5f;

    private void Awake()
    {
        cm_Rigid = GetComponent<RigidbodyComponent>();
    }

    private void Update()
    {
        if (m_LockControl)
        {
            return;
        }

        SetControl_Main_Keyboard();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;

        RigidbodyComponent cm_Rigid = GetComponent<RigidbodyComponent>();

        Gizmos.DrawLine(
            transform.position,
            transform.position + ClassVector.GetPosOnCircleXZ(-cm_Rigid.GetRotation_XZ(), 1f));
        Gizmos.DrawWireSphere(
            transform.position + ClassVector.GetPosOnCircleXZ(-cm_Rigid.GetRotation_XZ(), 1f),
            0.1f);
    }

    #region Control Main

    private void SetControl_Main_Keyboard()
    {
        m_SpeedCur = (Input.GetKey(k_SpeedChance)) ? m_SpeedChance : m_SpeedNormal;

        if (Input.GetKey(k_TurnLeft) && Input.GetKey(k_TurnRight) ||
            Input.GetKey(k_MoveForward) && Input.GetKey(k_MoveBackward))
        {
            //
            return;
        }

        if (Input.GetKey(k_TurnLeft))
        {
            SetControl_Rotate(-1);
        }
        else
        if (Input.GetKey(k_TurnRight))
        {
            SetControl_Rotate(1);
        }

        if (Input.GetKey(k_MoveForward))
        {
            SetControl_Move(1);
        }

        else
        if (Input.GetKey(k_MoveBackward))
        {
            SetControl_Move(-1);
        }
        else
        {
            SetControl_Move_Stop();
        }

        SetControl_Move_Slow();
    }

    private void SetControl_Move(int m_MoveDir)
    {
        cm_Rigid.SetMoveRotation_XZ(cm_Rigid.GetRotation_XZ(), m_SpeedCur * m_MoveDir);
    }

    private void SetControl_Move_Stop()
    {
        if (m_StopRightAway)
        {
            cm_Rigid.SetStopX_Velocity();
            cm_Rigid.SetStopZ_Velocity();
        }
    }

    private void SetControl_Move_Slow()
    {
        cm_Rigid.SetStopX_Velocity(m_SpeedSlow);
        cm_Rigid.SetStopZ_Velocity(m_SpeedSlow);
    }

    private void SetControl_Rotate(int m_RotationDir)
    {
        if (m_SlowWhenTurn)
        {
            SetControl_Move_Slow();
        }

        cm_Rigid.SetRotationChance_XZ(m_SpeedRotate * m_RotationDir);
    }

    #endregion

    #region Control is Lock

    public void SetControlIsLock(bool m_LockControl)
    {
        this.m_LockControl = m_LockControl;
    }

    public bool GetControlIsLock()
    {
        return m_LockControl;
    }

    #endregion
}
