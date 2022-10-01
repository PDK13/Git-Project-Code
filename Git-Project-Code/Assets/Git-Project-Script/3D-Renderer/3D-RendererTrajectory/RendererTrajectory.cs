using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RigidbodyGravity))]
public class RendererTrajectory : MonoBehaviour
{
    //Main Dir is Vector.Right

    [Header("Trajectory Main")]

    [SerializeField] private float m_Trajectory_Power = 5f;

    [SerializeField] private int m_Trajectory_Step = 500;

    [Header("Trajectory Point")]

    [SerializeField] private Transform com_Trajectory_Start;

    [SerializeField] private Transform com_Trajectory_Next;

    [Header("Trajectory Raycast")]

    [SerializeField] private bool m_TrajectoryRaycastIsAllow = false;

    [SerializeField] private LayerMask l_Trajectory_Raycast;

    [SerializeField] private float m_Trajectory_Raycast = 0.5f;

    private RigidbodyGravity cm_RigidbodyGravity;

    private void Awake()
    {
        if (GetComponent<RigidbodyGravity>() == null)
        {
            gameObject.AddComponent<RigidbodyGravity>();
        }

        cm_RigidbodyGravity = GetComponent<RigidbodyGravity>();
    }

    #region Trajectory Value

    #region Trajectory Start Point and Next Point

    public void SetTrajectory_Start(Transform com_Trajectory_Start)
    {
        this.com_Trajectory_Start = com_Trajectory_Start;
    }

    public void SetTrajectory_Start(Vector3 v3_Trajectory_Start)
    {
        com_Trajectory_Start.position = v3_Trajectory_Start;
    }

    public void SetTrajectory_Start_Chance(Vector3 v3_Trajectory_Start_Chance)
    {
        SetTrajectory_Start(GetTrajectory_Start() + v3_Trajectory_Start_Chance);
    }

    public void SetTrajectory_Next(Transform com_Trajectory_Next)
    {
        this.com_Trajectory_Next = com_Trajectory_Next;
    }

    public void SetTrajectory_Next(Vector3 v3_Trajectory_Next)
    {
        com_Trajectory_Next.position = v3_Trajectory_Next;
    }

    public void SetTrajectory_Next_Chance(Vector3 v3_Trajectory_Next_Chance)
    {
        SetTrajectory_Next(GetTrajectory_Next() + v3_Trajectory_Next_Chance);
    }

    public Transform GetTrajectory_Start_toTransform()
    {
        return com_Trajectory_Start;
    }

    public Vector3 GetTrajectory_Start()
    {
        return com_Trajectory_Start.position;
    }

    public Transform GetTrajectory_Next_toTransform()
    {
        return com_Trajectory_Next;
    }

    public Vector3 GetTrajectory_Next()
    {
        return com_Trajectory_Next.position;
    }

    public float GetTrajectory_Duration()
    {
        return GetTrajectory_Dir_Primary(false).magnitude;
    }

    #endregion

    #region Trajectory Power

    public void SetTrajectory_Power(float m_Trajectory_Power)
    {
        this.m_Trajectory_Power = m_Trajectory_Power;
    }

    public void SetTrajectory_Power_Chance(float m_Trajectory_Power_Chance)
    {
        m_Trajectory_Power += m_Trajectory_Power_Chance;
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

    public Vector3[] GetTrajectory_Points(float m_Rigidbody_Drag, bool m_Dir_Normalized)
    {
        Vector3[] v3_Trajectory_Result;

        List<Vector3> m_Trajectory_Result_List = new List<Vector3>();

        float m_TimeStep = Time.fixedDeltaTime / Physics.defaultSolverVelocityIterations;

        Vector3 v3_Gravity_Accel = cm_RigidbodyGravity.GetGravity_Global_toVector() * cm_RigidbodyGravity.GetGravity_Scale() * m_TimeStep * m_TimeStep;

        float m_Drag = 1f - m_TimeStep * m_Rigidbody_Drag;

        Vector3 v3_Trajectory_Dir = GetTrajectory_Dir_Primary(m_Dir_Normalized);

        Vector3 v3_MoveStep = v3_Trajectory_Dir * m_TimeStep;

        Vector3 v3_Pom_Point = GetTrajectory_Start();

        for (int i = 0; i < m_Trajectory_Step; i++)
        {
            v3_MoveStep += v3_Gravity_Accel;

            v3_MoveStep *= m_Drag;

            v3_Pom_Point += v3_MoveStep;

            if (m_TrajectoryRaycastIsAllow)
            {
                bool ray_Raycast = Physics.Linecast(v3_Pom_Point + Vector3.down * m_Trajectory_Raycast, v3_Pom_Point - Vector3.down * m_Trajectory_Raycast, l_Trajectory_Raycast);

                if (ray_Raycast)
                {
                    v3_Trajectory_Result = new Vector3[m_Trajectory_Result_List.Count];
                    v3_Trajectory_Result = m_Trajectory_Result_List.ToArray();
                    return v3_Trajectory_Result;
                }
                else
                {
                    m_Trajectory_Result_List.Add(v3_Pom_Point);
                }
            }
            else
            {
                m_Trajectory_Result_List.Add(v3_Pom_Point);
            }
        }

        v3_Trajectory_Result = new Vector3[m_Trajectory_Result_List.Count];
        v3_Trajectory_Result = m_Trajectory_Result_List.ToArray();
        return v3_Trajectory_Result;
    }

    public Vector3 GetTrajectory_Dir_Primary(bool m_Dir_Normalized)
    {
        if (m_Dir_Normalized)
        {
            return (GetTrajectory_Next() - GetTrajectory_Start()).normalized * m_Trajectory_Power;
        }

        return (GetTrajectory_Next() - GetTrajectory_Start()) * m_Trajectory_Power;
    }

    public Vector3 GetTrajectory_Dir()
    {
        return (GetTrajectory_Next() - GetTrajectory_Start()) * m_Trajectory_Power;
    }

    #endregion

    #region Angle for hit Trajectory

    public float? GetTrajectory_Angle_toDeg(Vector3 v3_Pom_Start, Vector3 v3_Pom_Tarket, bool m_AngleHighIsAllow)
    {
        Vector3 v3_Tarket_Dir = v3_Pom_Tarket - v3_Pom_Start;

        float m_Y_High = v3_Tarket_Dir.y;

        v3_Tarket_Dir.y = 0f;

        float m_X_Duration = v3_Tarket_Dir.magnitude;

        float m_Gravity = cm_RigidbodyGravity.GetGravity_Global_toFloat() * cm_RigidbodyGravity.GetGravity_Scale();

        float m_Speed_SQR = m_Trajectory_Power * m_Trajectory_Power;

        float m_Under_SQR = (m_Speed_SQR * m_Speed_SQR) - m_Gravity * (m_Gravity * m_X_Duration * m_X_Duration + 2 * m_Y_High * m_Speed_SQR);

        if (m_Under_SQR >= 0)
        {
            float m_Under_SQRT = Mathf.Sqrt(m_Under_SQR);
            float m_Angle_High = m_Speed_SQR + m_Under_SQRT;
            float m_Angle_Low = m_Speed_SQR - m_Under_SQRT;

            if (m_AngleHighIsAllow)
            {
                return Mathf.Atan2(m_Angle_High, m_Gravity * m_X_Duration) * Mathf.Rad2Deg;
            }
            else
            {
                return Mathf.Atan2(m_Angle_Low, m_Gravity * m_X_Duration) * Mathf.Rad2Deg;
            }
        }

        return null;
    }

    #endregion

    #region Rigidbody Velocity with Trajectory

    public void SetTrajectory_toRigidbody(Rigidbody com_Rigidbody, Vector3 v3_Trajectory_Start, Vector3 v3_Trajectory_Next)
    {
        if (com_Rigidbody.GetComponent<RigidbodyGravity>() == null)
        {
            com_Rigidbody.gameObject.AddComponent<RigidbodyGravity>();
        }

        com_Rigidbody.GetComponent<RigidbodyGravity>().SetGravity_Scale(cm_RigidbodyGravity.GetGravity_Scale());
        com_Rigidbody.GetComponent<RigidbodyGravity>().SetRigidbody_Drag(cm_RigidbodyGravity.GetRigidbody_Drag());

        Vector3 v3_Trajectory_Dir = (v3_Trajectory_Next - v3_Trajectory_Start) * GetTrajectory_Power();

        com_Rigidbody.velocity = v3_Trajectory_Dir;
    }

    public void SetTrajectory_toRigidbody(Rigidbody2D com_Rigidbody2D, Vector2 v2_Trajectory_Start, Vector2 v2_Trajectory_Next)
    {
        Vector2 v2_Trajectory_Dir = (v2_Trajectory_Next - v2_Trajectory_Start) * GetTrajectory_Power();

        //com_Rigidbody2D.drag = cm_RigidbodyGravity.GetRigidbody_Drag();
        com_Rigidbody2D.mass = 0;
        com_Rigidbody2D.gravityScale = cm_RigidbodyGravity.GetGravity_Scale();
        com_Rigidbody2D.velocity = v2_Trajectory_Dir;
    }

    #endregion

    #region Line Renderer with Trajectory

    public void SetTrajectory_toLineRenderer(LineRenderer com_LineRenderer, float m_Rigidbody_Drag, bool m_Dir_Normalized)
    {
        Vector3[] trajectory = GetTrajectory_Points(m_Rigidbody_Drag, m_Dir_Normalized);

        com_LineRenderer.positionCount = trajectory.Length;
        Vector3[] position = new Vector3[trajectory.Length];

        for (int i = 0; i < position.Length; i++)
        {
            position[i] = trajectory[i];
        }

        com_LineRenderer.SetPositions(position);
    }

    public void SetTrajectory_toLineRenderer_Clear(LineRenderer com_LineRenderer)
    {
        com_LineRenderer.positionCount = 0;
        com_LineRenderer.SetPositions(new Vector3[0]);
    }

    #endregion
}