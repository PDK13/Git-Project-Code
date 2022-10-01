using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(RigidbodyComponent))]

public class RigidbodyRotation : MonoBehaviour
//Move Control Surface (X & Z & Rotation)
{
    private RigidbodyComponent cs_Rigid;

    [Header("Keyboard")]

    [SerializeField] private bool b_LockControl = false;

    [SerializeField] private KeyCode k_MoveForward = KeyCode.UpArrow;

    [SerializeField] private KeyCode k_MoveBackward = KeyCode.DownArrow;

    [SerializeField] private KeyCode k_TurnLeft = KeyCode.LeftArrow;

    [SerializeField] private KeyCode k_TurnRight = KeyCode.RightArrow;

    [SerializeField] private bool b_MutiButton = true;

    [SerializeField] private KeyCode k_SpeedChance = KeyCode.LeftShift;

    [Header("Move")]

    [SerializeField] private float f_SpeedNormal = 7f;

    [SerializeField] private float f_SpeedChance = 10f;

    private float f_SpeedCur;

    [Header("Rotate")]

    [Range(0.1f, 5f)]
    [SerializeField] private float f_SpeedRotate = 1f;

    [SerializeField] private bool b_StopRightAway = false;

    [SerializeField] private float f_SpeedStop = 3f;

    [SerializeField] private bool b_SlowWhenTurn = true;

    [SerializeField] private float f_SpeedSlow = 5f;

    private void Awake()
    {
        cs_Rigid = GetComponent<RigidbodyComponent>();
    }

    private void Update()
    {
        if (b_LockControl)
        {
            return;
        }

        Set_Control_Main_Keyboard();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;

        RigidbodyComponent cs_Rigid = GetComponent<RigidbodyComponent>();

        Gizmos.DrawLine(
            transform.position,
            transform.position + Class_Vector.Get_DegToVector_XZ(-cs_Rigid.Get_Rotation_XZ(), 1f));
        Gizmos.DrawWireSphere(
            transform.position + Class_Vector.Get_DegToVector_XZ(-cs_Rigid.Get_Rotation_XZ(), 1f),
            0.1f);
    }

    #region Control Main

    private void Set_Control_Main_Keyboard()
    {
        f_SpeedCur = (Input.GetKey(k_SpeedChance)) ? f_SpeedChance : f_SpeedNormal;

        if (Input.GetKey(k_TurnLeft) && Input.GetKey(k_TurnRight) ||
            Input.GetKey(k_MoveForward) && Input.GetKey(k_MoveBackward))
        {
            //
            return;
        }

        if (Input.GetKey(k_TurnLeft))
        {
            Set_Control_Rotate(-1);
        }
        else
        if (Input.GetKey(k_TurnRight))
        {
            Set_Control_Rotate(1);
        }

        if (Input.GetKey(k_MoveForward))
        {
            Set_Control_Move(1);
        }

        else
        if (Input.GetKey(k_MoveBackward))
        {
            Set_Control_Move(-1);
        }
        else
        {
            Set_Control_Move_Stop();
        }

        Set_Control_Move_Slow();
    }

    private void Set_Control_Move(int i_MoveDir)
    {
        cs_Rigid.Set_MoveRotation_XZ(cs_Rigid.Get_Rotation_XZ(), f_SpeedCur * i_MoveDir);
    }

    private void Set_Control_Move_Stop()
    {
        if (b_StopRightAway)
        {
            cs_Rigid.Set_StopX_Velocity();
            cs_Rigid.Set_StopZ_Velocity();
        }
    }

    private void Set_Control_Move_Slow()
    {
        cs_Rigid.Set_StopX_Velocity(f_SpeedSlow);
        cs_Rigid.Set_StopZ_Velocity(f_SpeedSlow);
    }

    private void Set_Control_Rotate(int i_RotationDir)
    {
        if (b_SlowWhenTurn)
        {
            Set_Control_Move_Slow();
        }

        cs_Rigid.Set_RotationChance_XZ(f_SpeedRotate * i_RotationDir);
    }

    #endregion

    #region Control is Lock

    public void Set_Control_isLock(bool b_LockControl)
    {
        this.b_LockControl = b_LockControl;
    }

    public bool Get_Control_isLock()
    {
        return b_LockControl;
    }

    #endregion
}
