using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RigidbodyGravity))]
public class RendererTrajectory : MonoBehaviour
{
    //Main Dir is Vector.R

    [Header("Trajectory main")]

    [SerializeField] private float m_Trajectory_Power = 5f;

    [SerializeField] private int m_Trajectory_Step = 500;

    [Header("Trajectory Point")]

    [SerializeField] private Transform com_TrajectoryStart;

    [SerializeField] private Transform com_Trajectory_Next;

    [Header("Trajectory Raycast")]

    [SerializeField] private bool m_AllowTrajectoryRaycast = false;

    [SerializeField] private LayerMask m_TrajectoryRaycastLayerMask;

    [SerializeField] private float m_TrajectoryRaycastSize = 0.5f;

    private RigidbodyGravity m_RigidbodyGravity;

    private void Awake()
    {
        if (GetComponent<RigidbodyGravity>() == null)
        {
            gameObject.AddComponent<RigidbodyGravity>();
        }

        m_RigidbodyGravity = GetComponent<RigidbodyGravity>();
    }

    #region Trajectory Value

    #region Trajectory Start Point and Next Point

    public void SetTrajectoryStart(Transform com_TrajectoryStart)
    {
        this.com_TrajectoryStart = com_TrajectoryStart;
    }

    public void SetTrajectoryStart(Vector3 m_TrajectoryStart)
    {
        com_TrajectoryStart.position = m_TrajectoryStart;
    }

    public void SetTrajectoryStartChance(Vector3 m_TrajectoryStartChance)
    {
        SetTrajectoryStart(GetTrajectoryStart() + m_TrajectoryStartChance);
    }

    public void SetTrajectory_Next(Transform com_Trajectory_Next)
    {
        this.com_Trajectory_Next = com_Trajectory_Next;
    }

    public void SetTrajectory_Next(Vector3 m_Trajectory_Next)
    {
        com_Trajectory_Next.position = m_Trajectory_Next;
    }

    public void SetTrajectory_NextChance(Vector3 m_Trajectory_NextChance)
    {
        SetTrajectory_Next(GetTrajectory_Next() + m_Trajectory_NextChance);
    }

    public Transform GetTrajectoryStart_toTransform()
    {
        return com_TrajectoryStart;
    }

    public Vector3 GetTrajectoryStart()
    {
        return com_TrajectoryStart.position;
    }

    public Transform GetTrajectory_Next_toTransform()
    {
        return com_Trajectory_Next;
    }

    public Vector3 GetTrajectory_Next()
    {
        return com_Trajectory_Next.position;
    }

    public float GetTrajectoryDuration()
    {
        return GetTrajectoryDir_Primary(false).magnitude;
    }

    #endregion

    #region Trajectory Power

    public void SetTrajectory_Power(float m_Trajectory_Power)
    {
        this.m_Trajectory_Power = m_Trajectory_Power;
    }

    public void SetTrajectory_PowerChance(float m_Trajectory_PowerChance)
    {
        m_Trajectory_Power += m_Trajectory_PowerChance;
    }

    public float GetTrajectory_Power()
    {
        return m_Trajectory_Power;
    }

    #endregion

    #region Trajectory Step

    public void SetTrajectory_Step(int m_Trajectory_Step)
    {
        this.m_Trajectory_Step = m_Trajectory_Step;
    }

    public int GetTrajectory_Step()
    {
        return m_Trajectory_Step;
    }

    #endregion

    #endregion

    #region Trajectory

    public Vector3[] GetTrajectory_Points(float m_RigidbodyDrag, bool m_AllowDir_Normalized)
    {
        Vector3[] m_TrajectoryResult;

        List<Vector3> m_TrajectoryResumList = new List<Vector3>();

        float m_TimeStep = Time.fixedDeltaTime / Physics.defaultSolverVelocityIterations;

        Vector3 m_Gravity_Accel = m_RigidbodyGravity.GetGravity_Globam_toVector() * m_RigidbodyGravity.GetGravity_Scale() * m_TimeStep * m_TimeStep;

        float m_Drag = 1f - m_TimeStep * m_RigidbodyDrag;

        Vector3 m_TrajectoryDir = GetTrajectoryDir_Primary(m_AllowDir_Normalized);

        Vector3 m_MoveStep = m_TrajectoryDir * m_TimeStep;

        Vector3 m_Pom_Point = GetTrajectoryStart();

        for (int i = 0; i < m_Trajectory_Step; i++)
        {
            m_MoveStep += m_Gravity_Accel;

            m_MoveStep *= m_Drag;

            m_Pom_Point += m_MoveStep;

            if (m_AllowTrajectoryRaycast)
            {
                bool rayRaycast = Physics.Linecast(m_Pom_Point + Vector3.down * m_TrajectoryRaycastSize, m_Pom_Point - Vector3.down * m_TrajectoryRaycastSize, m_TrajectoryRaycastLayerMask);

                if (rayRaycast)
                {
                    m_TrajectoryResult = new Vector3[m_TrajectoryResumList.Count];
                    m_TrajectoryResult = m_TrajectoryResumList.ToArray();
                    return m_TrajectoryResult;
                }
                else
                {
                    m_TrajectoryResumList.Add(m_Pom_Point);
                }
            }
            else
            {
                m_TrajectoryResumList.Add(m_Pom_Point);
            }
        }

        m_TrajectoryResult = new Vector3[m_TrajectoryResumList.Count];
        m_TrajectoryResult = m_TrajectoryResumList.ToArray();
        return m_TrajectoryResult;
    }

    public Vector3 GetTrajectoryDir_Primary(bool m_AllowDir_Normalized)
    {
        if (m_AllowDir_Normalized)
        {
            return (GetTrajectory_Next() - GetTrajectoryStart()).normalized * m_Trajectory_Power;
        }

        return (GetTrajectory_Next() - GetTrajectoryStart()) * m_Trajectory_Power;
    }

    public Vector3 GetTrajectoryDir()
    {
        return (GetTrajectory_Next() - GetTrajectoryStart()) * m_Trajectory_Power;
    }

    #endregion

    #region Angle for hit Trajectory

    public float? GetTrajectory_Angle_toDeg(Vector3 m_PomStart, Vector3 m_Pom_Tarket, bool m_AllowAngleHighAllow)
    {
        Vector3 m_TarketDir = m_Pom_Tarket - m_PomStart;

        float m_Y_High = m_TarketDir.y;

        m_TarketDir.y = 0f;

        float m_XDuration = m_TarketDir.magnitude;

        float m_Gravity = m_RigidbodyGravity.GetGravity_Globam_toFloat() * m_RigidbodyGravity.GetGravity_Scale();

        float m_Speed_SQR = m_Trajectory_Power * m_Trajectory_Power;

        float m_Under_SQR = (m_Speed_SQR * m_Speed_SQR) - m_Gravity * (m_Gravity * m_XDuration * m_XDuration + 2 * m_Y_High * m_Speed_SQR);

        if (m_Under_SQR >= 0)
        {
            float m_Under_SQRT = Mathf.Sqrt(m_Under_SQR);
            float m_Angle_High = m_Speed_SQR + m_Under_SQRT;
            float m_AngleLow = m_Speed_SQR - m_Under_SQRT;

            if (m_AllowAngleHighAllow)
            {
                return Mathf.Atan2(m_Angle_High, m_Gravity * m_XDuration) * Mathf.Rad2Deg;
            }
            else
            {
                return Mathf.Atan2(m_AngleLow, m_Gravity * m_XDuration) * Mathf.Rad2Deg;
            }
        }

        return null;
    }

    #endregion

    #region Rigidbody Velocity with Trajectory

    public void SetTrajectory_toRigidbody(Rigidbody com_Rigidbody, Vector3 m_TrajectoryStart, Vector3 m_Trajectory_Next)
    {
        if (com_Rigidbody.GetComponent<RigidbodyGravity>() == null)
        {
            com_Rigidbody.gameObject.AddComponent<RigidbodyGravity>();
        }

        com_Rigidbody.GetComponent<RigidbodyGravity>().SetGravity_Scale(m_RigidbodyGravity.GetGravity_Scale());
        com_Rigidbody.GetComponent<RigidbodyGravity>().SetRigidbodyDrag(m_RigidbodyGravity.GetRigidbodyDrag());

        Vector3 m_TrajectoryDir = (m_Trajectory_Next - m_TrajectoryStart) * GetTrajectory_Power();

        com_Rigidbody.velocity = m_TrajectoryDir;
    }

    public void SetTrajectory_toRigidbody(Rigidbody2D com_Rigidbody2D, Vector2 v2_TrajectoryStart, Vector2 v2_Trajectory_Next)
    {
        Vector2 v2_TrajectoryDir = (v2_Trajectory_Next - v2_TrajectoryStart) * GetTrajectory_Power();

        //com_Rigidbody2D.drag = m_RigidbodyGravity.GetRigidbodyDrag();
        com_Rigidbody2D.mass = 0;
        com_Rigidbody2D.gravityScale = m_RigidbodyGravity.GetGravity_Scale();
        com_Rigidbody2D.velocity = v2_TrajectoryDir;
    }

    #endregion

    #region Line Renderer with Trajectory

    public void SetTrajectory_toLineRenderer(LineRenderer comLineRenderer, float m_RigidbodyDrag, bool m_AllowDir_Normalized)
    {
        Vector3[] trajectory = GetTrajectory_Points(m_RigidbodyDrag, m_AllowDir_Normalized);

        comLineRenderer.positionCount = trajectory.Length;
        Vector3[] position = new Vector3[trajectory.Length];

        for (int i = 0; i < position.Length; i++)
        {
            position[i] = trajectory[i];
        }

        comLineRenderer.SetPositions(position);
    }

    public void SetTrajectory_toLineRendererClear(LineRenderer comLineRenderer)
    {
        comLineRenderer.positionCount = 0;
        comLineRenderer.SetPositions(new Vector3[0]);
    }

    #endregion
}