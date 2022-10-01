﻿using UnityEngine;

/// <summary>
/// Control Velocity GameObject in 3D
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class RigidbodyComponent : MonoBehaviour
{
    /// <summary>
    /// Use Script Auto Set at Start
    /// </summary>
    [Header("Rigid")]
    [SerializeField]
    private bool m_UseScriptStart = true;

    /// <summary>
    /// Set Kinematic (Static)
    /// </summary>
    [SerializeField]
    private bool m_Kinematic = false;

    /// <summary>
    /// Lock Rotation
    /// </summary>
    [SerializeField]
    private bool m_LockRot = true;

    /// <summary>
    /// Lock Pos
    /// </summary>
    [SerializeField]
    private bool m_LockPos = false;

    /// <summary>
    /// Layer Mask to Check Foot and Head
    /// </summary>
    [Header("Layer Check")]
    [SerializeField]
    private LayerMask l_GroundCheck;

    /// <summary>
    /// Debug Foot Check
    /// </summary>
    [Header("Foot Check")]
    [SerializeField]
    private bool m_FootDebug = true;

    /// <summary>
    /// Box Cast of Foot
    /// </summary>
    [SerializeField]
    private Vector3 v3_FootCast = new Vector3(1f, 0.1f, 1f);

    /// <summary>
    /// Box Cast Distance of Foot
    /// </summary>
    [SerializeField]
    private float m_FootCast = 1f;

    /// <summary>
    /// Use Script Auto Gravity
    /// </summary>
    [SerializeField]
    private bool m_UseScriptGravity = true;

    /// <summary>
    /// Auto Gravity Velocity
    /// </summary>
    [SerializeField]
    private float m_GravityFall = 20f;

    /// <summary>
    /// Debug Head Check
    /// </summary>
    [Header("Head Check")]
    [SerializeField]
    private bool m_HeadDebug = true;

    /// <summary>
    /// Box Cast of Head
    /// </summary>
    [SerializeField]
    private Vector3 v3_HeadCast = new Vector3(1f, 0.1f, 1f);

    /// <summary>
    /// Box Cast Distance of Foot
    /// </summary>
    [SerializeField]
    private float m_HeadCast = 1f;

    /// <summary>
    /// Use Script Auto Bounce
    /// </summary>
    [SerializeField]
    private bool m_UseScriptBounce = true;

    /// <summary>
    /// Auto Bounce Velocity
    /// </summary>
    [SerializeField]
    private float m_HeadBounce = 10f;

    private void Awake()
    {
        if (m_UseScriptStart)
        {
            Set_Rigid();
        }
    }

    private void FixedUpdate()
    {
        if (m_UseScriptBounce)
        {
            if (GetCheckHead() && !GetCheckFoot())
            {
                //If Jump but Head touch Top >> Set Fall Down
                Set_MoveY_Fall(m_HeadBounce);
            }
        }

        if (m_UseScriptGravity)
        {
            if (!GetCheckFoot())
            {
                //If not Stand on Ground >> Gravity Set
                Set_MoveY_Gravity(m_GravityFall);
            }
        }
    }

    /// <summary>
    /// Set Rigid by EDITOR
    /// </summary>
    public void Set_Rigid()
    {
        Rigidbody r_Rigidbody = GetComponent<Rigidbody>();
        r_Rigidbody.isKinematic = m_Kinematic;
        r_Rigidbody.useGravity = false;

        if (m_LockRot)
        {
            r_Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        }

        if (m_LockPos)
        {
            r_Rigidbody.constraints = RigidbodyConstraints.FreezePosition;
        }
    }

    /// <summary>
    /// Set Rigid by SCRIPT
    /// </summary>
    /// <param name="m_Kinematic"></param>
    /// <param name="m_LockRot">No Rotation</param>
    /// <param name="m_LockPos">No Move</param>
    public void Set_Rigid(bool m_Kinematic, bool m_LockRot, bool m_LockPos)
    {
        Rigidbody r_Rigidbody = GetComponent<Rigidbody>();
        r_Rigidbody.isKinematic = m_Kinematic;
        r_Rigidbody.useGravity = false;
        if (m_LockRot && !m_LockPos)
        {
            r_Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        }
        else
        if (!m_LockRot && m_LockPos)
        {
            r_Rigidbody.constraints = RigidbodyConstraints.FreezePosition;
        }
        else
        {
            r_Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    /// <summary>
    /// Move by Velocity
    /// </summary>
    /// <param name="v3_Velocity"></param>
    public void Set_Rigidbody_Velocity(Vector3 v3_Velocity)
    {
        Rigidbody r_Rigidbody = GetComponent<Rigidbody>();
        r_Rigidbody.velocity = v3_Velocity;
    }

    //X

    /// <summary>
    /// Move X by Rigid
    /// </summary>
    /// <param name="m_DirMoveRight"></param>
    /// <param name="m_VelocityMove"></param>
    /// <param name="m_VelocityMoveMax"></param>
    public void Set_MoveX_Velocity(int m_DirMoveRight, float m_VelocityMove, float m_VelocityMoveMax)
    {
        int m_Dir = (m_DirMoveRight > 0) ? 1 : (m_DirMoveRight < 0) ? -1 : 0;
        Rigidbody r_Rigidbody = GetComponent<Rigidbody>();
        if (Mathf.Abs(r_Rigidbody.velocity.x) <= m_VelocityMoveMax)
        {
            r_Rigidbody.AddForce(Vector3.right * m_VelocityMove * m_Dir);
        }
    }

    /// <summary>
    /// Stop X by Rigid
    /// </summary>
    /// <param name="m_VelocityStop"></param>
    public void Set_StopX_Velocity(float m_VelocityStop)
    {
        Rigidbody r_Rigidbody = GetComponent<Rigidbody>();
        if (m_VelocityStop != 0)
        {
            r_Rigidbody.AddForce(Vector3.left * r_Rigidbody.velocity.x * m_VelocityStop);
        }
        else
        {
            r_Rigidbody.AddForce(Vector3.left * r_Rigidbody.velocity.x);
        }
    }

    /// <summary>
    /// Stop X by Rigid Instanly
    /// </summary>
    public void Set_StopX_Velocity()
    {
        Rigidbody r_Rigidbody = GetComponent<Rigidbody>();
        r_Rigidbody.velocity = new Vector3(0, r_Rigidbody.velocity.y, r_Rigidbody.velocity.z);
    }

    /// <summary>
    /// Move by Translate
    /// </summary>
    /// <param name="m_DirMoveRight"></param>
    /// <param name="m_VelocityMove"></param>
    public void Set_MoveX_NotVelocity(int m_DirMoveRight, float m_VelocityMove)
    {
        int m_Dir = (m_DirMoveRight > 0) ? 1 : (m_DirMoveRight < 0) ? -1 : 0;
        transform.Translate(Vector3.right * m_VelocityMove * m_Dir * Time.fixedDeltaTime);
    }

    //Z

    /// <summary>
    /// Move Z by Velocity
    /// </summary>
    /// <param name="m_DirMoveForward"></param>
    /// <param name="m_VelocityMove"></param>
    /// <param name="m_VelocityMoveMax"></param>
    public void Set_MoveZ_Velocity(int m_DirMoveForward, float m_VelocityMove, float m_VelocityMoveMax)
    {
        int m_Dir = (m_DirMoveForward > 0) ? 1 : (m_DirMoveForward < 0) ? -1 : 0;
        Rigidbody r_Rigidbody = GetComponent<Rigidbody>();
        if (Mathf.Abs(r_Rigidbody.velocity.z) <= m_VelocityMoveMax)
        {
            r_Rigidbody.AddForce(Vector3.forward * m_VelocityMove * m_Dir);
        }
    }

    /// <summary>
    /// Stop Z by Velocity
    /// </summary>
    /// <param name="m_VelocityStop"></param>
    public void Set_StopZ_Velocity(float m_VelocityStop)
    {
        Rigidbody r_Rigidbody = GetComponent<Rigidbody>();
        if (m_VelocityStop != 0)
        {
            r_Rigidbody.AddForce(Vector3.back * r_Rigidbody.velocity.z * m_VelocityStop);
        }
        else
        {
            r_Rigidbody.AddForce(Vector3.back * r_Rigidbody.velocity.z);
        }
    }

    /// <summary>
    /// Stop Z by Velocity
    /// </summary>
    public void Set_StopZ_Velocity()
    {
        Rigidbody r_Rigidbody = GetComponent<Rigidbody>();
        r_Rigidbody.velocity = new Vector3(r_Rigidbody.velocity.x, r_Rigidbody.velocity.y, 0);
    }

    //Non-Velocity

    /// <summary>
    /// Move by Move Toward
    /// </summary>
    /// <param name="v_PosMoveTo"></param>
    /// <param name="m_VelocityMove"></param>
    public void Set_Move_NotVelocity_MoveTowards(Vector3 v_PosMoveTo, float m_VelocityMove)
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            new Vector3(v_PosMoveTo.x, v_PosMoveTo.y, v_PosMoveTo.z),
            m_VelocityMove * Time.fixedDeltaTime);
    }

    /// <summary>
    /// Move by Translate
    /// </summary>
    /// <param name="v_DirMove">"Vector3.Up" or "(0; 1; 0)" or etc</param>
    /// <param name="m_VelocityMove"></param>
    public void Set_Move_NotVelocity_Translate(Vector3 v_DirMove, float m_VelocityMove)
    {
        transform.Translate(v_DirMove * m_VelocityMove * Time.fixedDeltaTime);
    }

    //Deg

    /// <summary>
    /// Get Rotation XY
    /// </summary>
    /// <returns></returns>
    public float GetRotation_XY()
    {
        return ClassVector.GetDegExchanceUnity(ClassVector.GetRotationQuaternionToEuler(transform.rotation).z);
    }

    /// <summary>
    /// Get Rotation XZ
    /// </summary>
    /// <returns>Degree</returns>
    public float GetRotation_XZ()
    {
        return ClassVector.GetDegExchanceUnity(ClassVector.GetRotationQuaternionToEuler(transform.rotation).y);
    }

    /// <summary>
    /// Set Rotation XY
    /// </summary>
    /// <param name="m_Rotation"></param>
    public void Set_Rotation_XY(float m_Rotation)
    {
        transform.rotation = ClassVector.GetRotationEulerToQuaternion(0, 0, m_Rotation);
    }

    /// <summary>
    /// Set Rotation XZ
    /// </summary>
    /// <param name="m_Rotation"></param>
    public void Set_Rotation_XZ(float m_Rotation)
    {
        transform.rotation = ClassVector.GetRotationEulerToQuaternion(new Vector3(0, m_Rotation, 0));
    }

    /// <summary>
    /// Set Rotation Chance XY
    /// </summary>
    /// <param name="m_RotationChance"></param>
    public void Set_RotationChance_XY(float m_RotationChance)
    {
        Set_Rotation_XY(GetRotation_XZ() + m_RotationChance);
    }

    /// <summary>
    /// Set Rotation Chance XZ
    /// </summary>
    /// <param name="m_RotationChance"></param>
    public void Set_RotationChance_XZ(float m_RotationChance)
    {
        Set_Rotation_XZ(GetRotation_XZ() + m_RotationChance);
    }

    /// <summary>
    /// Set Move by Rotation
    /// </summary>
    /// <param name="m_Rotation"></param>
    /// <param name="m_VelocityMove"></param>
    public void Set_MoveRotation_XZ(float m_Rotation, float m_VelocityMove)
    {
        Rigidbody r_Rigidbody = GetComponent<Rigidbody>();

        r_Rigidbody.AddForce(
            ClassVector.GetPosOnCircleXZ(
                ClassVector.GetDegExchanceUnity(-m_Rotation), 1).normalized * m_VelocityMove);
    }

    //Y

    /// <summary>
    /// Set Jump
    /// </summary>
    /// <param name="m_VelocityJump"></param>
    public void Set_MoveY_Jump(float m_VelocityJump)
    {
        Rigidbody r_Rigidbody = GetComponent<Rigidbody>();
        r_Rigidbody.velocity = new Vector3(r_Rigidbody.velocity.x, m_VelocityJump, r_Rigidbody.velocity.z);
    }

    /// <summary>
    /// Set Gravity
    /// </summary>
    /// <param name="m_VelocityGravity"></param>
    public void Set_MoveY_Gravity(float m_VelocityGravity)
    {
        Rigidbody r_Rigidbody = GetComponent<Rigidbody>();
        r_Rigidbody.AddForce(Vector3.down * m_VelocityGravity);
    }

    /// <summary>
    /// Set Fall
    /// </summary>
    /// <param name="m_VelocityFall"></param>
    public void Set_MoveY_Fall(float m_VelocityFall)
    {
        Rigidbody r_Rigidbody = GetComponent<Rigidbody>();
        r_Rigidbody.velocity = new Vector3(r_Rigidbody.velocity.x, -m_VelocityFall, r_Rigidbody.velocity.z);
    }

    //Check

    /// <summary>
    /// Check Foot
    /// </summary>
    /// <returns></returns>
    public bool GetCheckFoot()
    {
        Class_Eye cs_Eye = new Class_Eye();

        return cs_Eye.GetBoxCast_Dir_Check(
            transform.position,
            v3_FootCast,
            Vector3.down,
            ClassVector.GetRotationQuaternionToEuler(transform.rotation),
            m_FootCast,
            l_GroundCheck);
    }

    /// <summary>
    /// Check Head
    /// </summary>
    /// <returns></returns>
    public bool GetCheckHead()
    {
        Class_Eye cs_Eye = new Class_Eye();

        return cs_Eye.GetBoxCast_Dir_Check(
            transform.position,
            v3_HeadCast,
            Vector3.up,
            ClassVector.GetRotationQuaternionToEuler(transform.rotation),
            m_HeadCast,
            l_GroundCheck);
    }

    //Gizmos

    private void OnDrawGizmos()
    {
        if (m_FootDebug)
        {
            //Foot Check
            if (GetCheckFoot())
            {
                Gizmos.color = Color.red;
            }
            else
            {
                Gizmos.color = Color.white;
            }

            Gizmos.DrawLine(transform.position, transform.position + Vector3.down * m_FootCast);
            Gizmos.DrawWireCube(transform.position + Vector3.down * m_FootCast, v3_FootCast);
        }

        if (m_HeadDebug)
        {
            //Head Check
            if (GetCheckHead())
            {
                Gizmos.color = Color.red;
            }
            else
            {
                Gizmos.color = Color.white;
            }

            Gizmos.DrawLine(transform.position, transform.position + Vector3.up * m_HeadCast);
            Gizmos.DrawWireCube(transform.position + Vector3.up * m_HeadCast, v3_HeadCast);
        }
    }
}