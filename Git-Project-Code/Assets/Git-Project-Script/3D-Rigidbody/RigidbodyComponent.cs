using UnityEngine;

/// <summary>
/// Control Velocity GameObject in 3D
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class RigidbodyComponent : MonoBehaviour
{
    [Header("Rigid")]

    [SerializeField]
    private bool m_AllowUseScriptStart = true;

    [SerializeField]
    private bool m_AllowKinematic = false;

    [SerializeField]
    private bool m_AllowLockRot = true;

    [SerializeField]
    private bool m_AllowLockPos = false;

    [Header("Layer Check")]

    [SerializeField]
    private LayerMask m_GroundCheck;

    [Header("Foot Check")]
    [SerializeField]
    private bool m_AllowFootDebug = true;

    [SerializeField]
    private Vector3 m_FootCastSize = new Vector3(1f, 0.1f, 1f);

    [SerializeField]
    private float m_FootCastDistance = 1f;

    [SerializeField]
    private bool m_AllowUseScriptGravity = true;

    [SerializeField]
    private float m_GravityFall = 20f;

    [Header("Head Check")]

    [SerializeField]
    private bool m_AllowHeadDebug = true;

    [SerializeField]
    private Vector3 m_HeadCastSize = new Vector3(1f, 0.1f, 1f);

    [SerializeField]
    private float m_HeadCastDistance = 1f;

    [SerializeField]
    private bool m_AllowUseScriptBounce = true;

    [SerializeField]
    private float m_HeadBounce = 10f;

    private void Awake()
    {
        if (m_AllowUseScriptStart)
        {
            SetRigid();
        }
    }

    private void FixedUpdate()
    {
        if (m_AllowUseScriptBounce)
        {
            if (GetCheckHead() && !GetCheckFoot())
            {
                //If Jump but Head touch Top >> Set Fall D
                SetMoveY_Fall(m_HeadBounce);
            }
        }

        if (m_AllowUseScriptGravity)
        {
            if (!GetCheckFoot())
            {
                //If not Stand on Ground >> Gravity Set
                SetMoveY_Gravity(m_GravityFall);
            }
        }
    }

    /// <summary>
    /// Set Rigid by EDITOR
    /// </summary>
    public void SetRigid()
    {
        Rigidbody m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.isKinematic = m_AllowKinematic;
        m_Rigidbody.useGravity = false;

        if (m_AllowLockRot)
        {
            m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        }

        if (m_AllowLockPos)
        {
            m_Rigidbody.constraints = RigidbodyConstraints.FreezePosition;
        }
    }

    /// <summary>
    /// Set Rigid by SCRIPT
    /// </summary>
    /// <param name="m_Kinematic"></param>
    /// <param name="mLockRot">No Rotation</param>
    /// <param name="mLockPos">No m_ove</param>
    public void SetRigid(bool mAllowKinematic, bool mAllowLockRot, bool mAllowLockPos)
    {
        Rigidbody m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.isKinematic = mAllowKinematic;
        m_Rigidbody.useGravity = false;
        if (mAllowLockRot && !mAllowLockPos)
        {
            m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        }
        else
        if (!mAllowLockRot && mAllowLockPos)
        {
            m_Rigidbody.constraints = RigidbodyConstraints.FreezePosition;
        }
        else
        {
            m_Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    /// <summary>
    /// m_ove by Velocity
    /// </summary>
    /// <param name="m_Velocity"></param>
    public void SetRigidbodyVelocity(Vector3 m_Velocity)
    {
        Rigidbody m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.velocity = m_Velocity;
    }

    //X

    /// <summary>
    /// m_ove X by Rigid
    /// </summary>
    /// <param name="m_DirMoveR"></param>
    /// <param name="m_VelocityMove"></param>
    /// <param name="m_VelocityMoveMax"></param>
    public void SetMoveX_Velocity(int m_DirMoveR, float m_VelocityMove, float m_VelocityMoveMax)
    {
        int m_Dir = (m_DirMoveR > 0) ? 1 : (m_DirMoveR < 0) ? -1 : 0;
        Rigidbody m_Rigidbody = GetComponent<Rigidbody>();
        if (Mathf.Abs(m_Rigidbody.velocity.x) <= m_VelocityMoveMax)
        {
            m_Rigidbody.AddForce(Vector3.right * m_VelocityMove * m_Dir);
        }
    }

    /// <summary>
    /// Stop X by Rigid
    /// </summary>
    /// <param name="m_VelocityStop"></param>
    public void SetStopX_Velocity(float m_VelocityStop)
    {
        Rigidbody m_Rigidbody = GetComponent<Rigidbody>();
        if (m_VelocityStop != 0)
        {
            m_Rigidbody.AddForce(Vector3.left * m_Rigidbody.velocity.x * m_VelocityStop);
        }
        else
        {
            m_Rigidbody.AddForce(Vector3.left * m_Rigidbody.velocity.x);
        }
    }

    /// <summary>
    /// Stop X by Rigid Instanly
    /// </summary>
    public void SetStopX_Velocity()
    {
        Rigidbody m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.velocity = new Vector3(0, m_Rigidbody.velocity.y, m_Rigidbody.velocity.z);
    }

    /// <summary>
    /// m_ove by Translate
    /// </summary>
    /// <param name="m_DirMoveR"></param>
    /// <param name="m_VelocityMove"></param>
    public void SetMoveX_NotVelocity(int m_DirMoveR, float m_VelocityMove)
    {
        int m_Dir = (m_DirMoveR > 0) ? 1 : (m_DirMoveR < 0) ? -1 : 0;
        transform.Translate(Vector3.right * m_VelocityMove * m_Dir * Time.fixedDeltaTime);
    }

    //Z

    /// <summary>
    /// m_ove Z by Velocity
    /// </summary>
    /// <param name="m_DirMoveForward"></param>
    /// <param name="m_VelocityMove"></param>
    /// <param name="m_VelocityMoveMax"></param>
    public void SetMoveZ_Velocity(int m_DirMoveForward, float m_VelocityMove, float m_VelocityMoveMax)
    {
        int m_Dir = (m_DirMoveForward > 0) ? 1 : (m_DirMoveForward < 0) ? -1 : 0;
        Rigidbody m_Rigidbody = GetComponent<Rigidbody>();
        if (Mathf.Abs(m_Rigidbody.velocity.z) <= m_VelocityMoveMax)
        {
            m_Rigidbody.AddForce(Vector3.forward * m_VelocityMove * m_Dir);
        }
    }

    /// <summary>
    /// Stop Z by Velocity
    /// </summary>
    /// <param name="m_VelocityStop"></param>
    public void SetStopZ_Velocity(float m_VelocityStop)
    {
        Rigidbody m_Rigidbody = GetComponent<Rigidbody>();
        if (m_VelocityStop != 0)
        {
            m_Rigidbody.AddForce(Vector3.back * m_Rigidbody.velocity.z * m_VelocityStop);
        }
        else
        {
            m_Rigidbody.AddForce(Vector3.back * m_Rigidbody.velocity.z);
        }
    }

    /// <summary>
    /// Stop Z by Velocity
    /// </summary>
    public void SetStopZ_Velocity()
    {
        Rigidbody m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, m_Rigidbody.velocity.y, 0);
    }

    //Non-Velocity

    /// <summary>
    /// m_ove by m_ove Toward
    /// </summary>
    /// <param name="v_PosMoveTo"></param>
    /// <param name="m_VelocityMove"></param>
    public void SetMoveNotVelocity_MoveTowards(Vector3 v_PosMoveTo, float m_VelocityMove)
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            new Vector3(v_PosMoveTo.x, v_PosMoveTo.y, v_PosMoveTo.z),
            m_VelocityMove * Time.fixedDeltaTime);
    }

    /// <summary>
    /// m_ove by Translate
    /// </summary>
    /// <param name="vDirMove">"Vector3.U" or "(0; 1; 0)" or etc</param>
    /// <param name="m_VelocityMove"></param>
    public void SetMoveNotVelocity_Translate(Vector3 vDirMove, float m_VelocityMove)
    {
        transform.Translate(vDirMove * m_VelocityMove * Time.fixedDeltaTime);
    }

    //Deg

    /// <summary>
    /// Get Rotation XY
    /// </summary>
    /// <returns></returns>
    public float GetRotationXY()
    {
        return ClassVector.GetDegExchanceUnity(ClassVector.GetRotationQuaternionToEuler(transform.rotation).z);
    }

    /// <summary>
    /// Get Rotation XZ
    /// </summary>
    /// <returns>Degree</returns>
    public float GetRotationXZ()
    {
        return ClassVector.GetDegExchanceUnity(ClassVector.GetRotationQuaternionToEuler(transform.rotation).y);
    }

    /// <summary>
    /// Set Rotation XY
    /// </summary>
    /// <param name="mRotation"></param>
    public void SetRotationXY(float m_Rotation)
    {
        transform.rotation = ClassVector.GetRotationEulerToQuaternion(0, 0, m_Rotation);
    }

    /// <summary>
    /// Set Rotation XZ
    /// </summary>
    /// <param name="mRotation"></param>
    public void SetRotationXZ(float m_Rotation)
    {
        transform.rotation = ClassVector.GetRotationEulerToQuaternion(new Vector3(0, m_Rotation, 0));
    }

    /// <summary>
    /// Set Rotation Chance XY
    /// </summary>
    /// <param name="mRotationChance"></param>
    public void SetRotationChanceXY(float m_RotationChance)
    {
        SetRotationXY(GetRotationXZ() + m_RotationChance);
    }

    /// <summary>
    /// Set Rotation Chance XZ
    /// </summary>
    /// <param name="mRotationChance"></param>
    public void SetRotationChanceXZ(float m_RotationChance)
    {
        SetRotationXZ(GetRotationXZ() + m_RotationChance);
    }

    /// <summary>
    /// Set m_ove by Rotation
    /// </summary>
    /// <param name="mRotation"></param>
    /// <param name="m_VelocityMove"></param>
    public void SetMoveRotationXZ(float m_Rotation, float m_VelocityMove)
    {
        Rigidbody m_Rigidbody = GetComponent<Rigidbody>();

        m_Rigidbody.AddForce(
            ClassVector.GetPosOnCircleXZ(
                ClassVector.GetDegExchanceUnity(-m_Rotation), 1).normalized * m_VelocityMove);
    }

    //Y

    /// <summary>
    /// Set Jump
    /// </summary>
    /// <param name="m_VelocityJump"></param>
    public void SetMoveY_Jump(float m_VelocityJump)
    {
        Rigidbody m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, m_VelocityJump, m_Rigidbody.velocity.z);
    }

    /// <summary>
    /// Set Gravity
    /// </summary>
    /// <param name="m_VelocityGravity"></param>
    public void SetMoveY_Gravity(float m_VelocityGravity)
    {
        Rigidbody m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.AddForce(Vector3.down * m_VelocityGravity);
    }

    /// <summary>
    /// Set Fall
    /// </summary>
    /// <param name="m_VelocityFall"></param>
    public void SetMoveY_Fall(float m_VelocityFall)
    {
        Rigidbody m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x, -m_VelocityFall, m_Rigidbody.velocity.z);
    }

    //Check

    /// <summary>
    /// Check Foot
    /// </summary>
    /// <returns></returns>
    public bool GetCheckFoot()
    {
        ClassEye m_Eye = new ClassEye();

        return m_Eye.GetCheckBoxCastDir(
            transform.position,
            m_FootCastSize,
            Vector3.down,
            ClassVector.GetRotationQuaternionToEuler(transform.rotation),
            m_FootCastDistance,
            m_GroundCheck);
    }

    /// <summary>
    /// Check Head
    /// </summary>
    /// <returns></returns>
    public bool GetCheckHead()
    {
        ClassEye m_Eye = new ClassEye();

        return m_Eye.GetCheckBoxCastDir(
            transform.position,
            m_HeadCastSize,
            Vector3.up,
            ClassVector.GetRotationQuaternionToEuler(transform.rotation),
            m_HeadCastDistance,
            m_GroundCheck);
    }

    //Gizmos

    private void OnDrawGizmos()
    {
        if (m_AllowFootDebug)
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

            Gizmos.DrawLine(transform.position, transform.position + Vector3.down * m_FootCastDistance);
            Gizmos.DrawWireCube(transform.position + Vector3.down * m_FootCastDistance, m_FootCastSize);
        }

        if (m_AllowHeadDebug)
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

            Gizmos.DrawLine(transform.position, transform.position + Vector3.up * m_HeadCastDistance);
            Gizmos.DrawWireCube(transform.position + Vector3.up * m_HeadCastDistance, m_HeadCastSize);
        }
    }
}