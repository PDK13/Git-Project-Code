using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RigidbodyGravity))]
public class RendererTrajectory : MonoBehaviour
{
    [Header("Trajectory Main")]

    [SerializeField] private float f_Trajectory_Power = 5f;

    [SerializeField] private int i_Trajectory_Step = 500;

    [Header("Trajectory Start Point")]

    [SerializeField] private bool b_Trajectory_Start_isThis = true;

    [SerializeField] private Vector3 v3_Trajectory_Start = new Vector3(0f, 0f);

    [Header("Trajectory Next Point")]

    [SerializeField] private Vector3 v3_Trajectory_Next = new Vector3(1f, 1f);

    [Header("Trajectory Raycast")]

    [SerializeField] private bool b_Trajectory_Raycast = false;

    [SerializeField] private LayerMask l_Trajectory_Raycast;

    [SerializeField] private float f_Trajectory_Raycast = 0.5f;

    private RigidbodyGravity cs_RigidbodyGravity;

    private void Awake()
    {
        if (GetComponent<RigidbodyGravity>() == null) gameObject.AddComponent<RigidbodyGravity>();
        cs_RigidbodyGravity = GetComponent<RigidbodyGravity>();
    }

    #region Trajectory Value

    #region Trajectory Start Point and Next Point

    public void Set_Trajectory_Start_isThis(bool b_Trajectory_Start_isThis)
    {
        this.b_Trajectory_Start_isThis = b_Trajectory_Start_isThis;
    }

    public void Set_Trajectory_Start(Vector3 v3_Trajectory_Start)
    {
        this.v3_Trajectory_Start = v3_Trajectory_Start;
    }

    public void Set_Trajectory_Next(Vector3 v3_Trajectory_Next)
    {
        this.v3_Trajectory_Next = v3_Trajectory_Next;
    }

    public bool Get_Trajectory_Start_isThis()
    {
        return b_Trajectory_Start_isThis;
    }

    public Vector3 Get_Trajectory_Start()
    {
        return v3_Trajectory_Start;
    }

    public Vector3 Get_Trajectory_Next()
    {
        return v3_Trajectory_Next;
    }

    #endregion

    #region Trajectory Power

    public void Set_Trajectory_Power(float f_Trajectory_Power)
    {
        this.f_Trajectory_Power = f_Trajectory_Power;
    }

    public float Get_Trajectory_Power()
    {
        return this.f_Trajectory_Power;
    }

    #endregion

    #region Trajectory Step

    public void Set_Trajectory_Step(int i_Trajectory_Step)
    {
        this.i_Trajectory_Step = i_Trajectory_Step;
    }

    public int Get_Trajectory_Step()
    {
        return this.i_Trajectory_Step;
    }

    #endregion

    #endregion

    #region Trajectory

    public Vector3[] Get_Trajectory_Points(Rigidbody com_Rigidbody, Vector3 v3_Pos_Start, Vector3 v3_Trajectory_Dir, int i_Trajectory_Step = 500)
    {
        Vector3[] v3_Trajectory_Result;

        List<Vector3> lv3_Trajectory_Result_List = new List<Vector3>();

        float f_TimeStep = Time.fixedDeltaTime / Physics.defaultSolverVelocityIterations;

        Vector3 v3_Gravity_Accel = Physics.gravity * cs_RigidbodyGravity.Get_Gravity_Scale() * f_TimeStep * f_TimeStep;

        float f_Drag = 1f - f_TimeStep * com_Rigidbody.drag;
        Vector3 v3_MoveStep = v3_Trajectory_Dir * f_TimeStep;

        Vector3 v3_Pos_Point = v3_Pos_Start;

        for (int i = 0; i < i_Trajectory_Step; i++)
        {
            v3_MoveStep += v3_Gravity_Accel;
            v3_MoveStep *= f_Drag;
            v3_Pos_Point += v3_MoveStep;

            if (b_Trajectory_Raycast)
            {
                bool ray_Raycast = Physics.Linecast(v3_Pos_Point + Vector3.down * f_Trajectory_Raycast, v3_Pos_Point - Vector3.down * f_Trajectory_Raycast, l_Trajectory_Raycast);

                if (ray_Raycast)
                {
                    v3_Trajectory_Result = new Vector3[lv3_Trajectory_Result_List.Count];
                    v3_Trajectory_Result = lv3_Trajectory_Result_List.ToArray();
                    return v3_Trajectory_Result;
                }
                else
                {
                    lv3_Trajectory_Result_List.Add(v3_Pos_Point);
                }
            }
            else
            {
                lv3_Trajectory_Result_List.Add(v3_Pos_Point);
            }
        }

        v3_Trajectory_Result = new Vector3[lv3_Trajectory_Result_List.Count];
        v3_Trajectory_Result = lv3_Trajectory_Result_List.ToArray();
        return v3_Trajectory_Result;
    }

    public Vector3[] Get_Trajectory_Points(Rigidbody2D com_Rigidbody2D, Vector3 v3_Pos_Start, Vector3 v3_Trajectory_Dir, int i_Trajectory_Step = 500)
    {
        Vector3[] v3_Trajectory_Result;

        List<Vector3> lv3_Trajectory_Result_List = new List<Vector3>();

        float f_TimeStep = Time.fixedDeltaTime / Physics.defaultSolverVelocityIterations;

        Vector3 v3_Gravity_Accel = Physics.gravity * cs_RigidbodyGravity.Get_Gravity_Scale() * f_TimeStep * f_TimeStep;

        float f_Drag = 1f - f_TimeStep * com_Rigidbody2D.drag;
        Vector3 v3_MoveStep = v3_Trajectory_Dir * f_TimeStep;

        Vector3 v3_Pos_Point = v3_Pos_Start;

        for (int i = 0; i < i_Trajectory_Step; i++)
        {
            v3_MoveStep += v3_Gravity_Accel;
            v3_MoveStep *= f_Drag;
            v3_Pos_Point += v3_MoveStep;

            if (b_Trajectory_Raycast)
            {
                bool ray_Raycast = Physics.Linecast(v3_Pos_Point + Vector3.down * f_Trajectory_Raycast, v3_Pos_Point - Vector3.down * f_Trajectory_Raycast, l_Trajectory_Raycast);

                if (ray_Raycast)
                {
                    v3_Trajectory_Result = new Vector3[lv3_Trajectory_Result_List.Count];
                    v3_Trajectory_Result = lv3_Trajectory_Result_List.ToArray();
                    return v3_Trajectory_Result;
                }
                else
                {
                    lv3_Trajectory_Result_List.Add(v3_Pos_Point);
                }
            }
            else
            {
                lv3_Trajectory_Result_List.Add(v3_Pos_Point);
            }
        }

        v3_Trajectory_Result = new Vector3[lv3_Trajectory_Result_List.Count];
        v3_Trajectory_Result = lv3_Trajectory_Result_List.ToArray();
        return v3_Trajectory_Result;
    }

    public Vector3 Get_Trajectory_Dir(Vector3 v3_Trajectory_Start, Vector3 v3_Trajectory_Next, float f_Trajectory_Power)
    {
        return (v3_Trajectory_Next - v3_Trajectory_Start) * f_Trajectory_Power;
    }

    #endregion

    #region Rigidbody Velocity with Trajectory

    public void Set_Trajectory_toRigidbody(Rigidbody com_Rigidbody, Vector3 v3_Trajectory_Start, Vector3 v3_Trajectory_Next)
    {
        Vector3 v3_Trajectory_Dir = (v3_Trajectory_Next - v3_Trajectory_Start) * f_Trajectory_Power;

        com_Rigidbody.velocity = v3_Trajectory_Dir;
    }

    #endregion

    #region Line Renderer with Trajectory

    public void Set_Trajectory_toLineRenderer(Rigidbody com_Rigidbody, LineRenderer com_LineRenderer, Vector3 v3_Trajectory_Start, Vector3 v3_Trajectory_Next, float f_LineRenderer_Width = 0.2f, Material m_Trajectory_Material = null)
    {
        if (m_Trajectory_Material != null)
        {
            com_LineRenderer.material = m_Trajectory_Material;
        }

        com_LineRenderer.startWidth = f_LineRenderer_Width;
        com_LineRenderer.endWidth = f_LineRenderer_Width;

        Vector3 v3_Trajectory_Dir = (v3_Trajectory_Next - v3_Trajectory_Start) * f_Trajectory_Power;

        Vector3[] trajectory = Get_Trajectory_Points(
            com_Rigidbody,
            v3_Trajectory_Start,
            v3_Trajectory_Dir,
            Get_Trajectory_Step());

        com_LineRenderer.positionCount = trajectory.Length;
        Vector3[] position = new Vector3[trajectory.Length];

        for (int i = 0; i < position.Length; i++)
        {
            position[i] = trajectory[i];
        }

        com_LineRenderer.SetPositions(position);
    }

    public void Set_Trajectory_toLineRenderer(Rigidbody2D com_Rigidbody2D, LineRenderer com_LineRenderer, Vector3 v3_Trajectory_Start, Vector3 v3_Trajectory_Next, float f_LineRenderer_Width = 0.2f, Material m_Trajectory_Material = null)
    {
        if (m_Trajectory_Material != null)
        {
            com_LineRenderer.material = m_Trajectory_Material;
        }

        com_LineRenderer.startWidth = f_LineRenderer_Width;
        com_LineRenderer.endWidth = f_LineRenderer_Width;

        Vector3 v3_Trajectory_Dir = (v3_Trajectory_Next - v3_Trajectory_Start) * f_Trajectory_Power;

        Vector3[] trajectory = Get_Trajectory_Points(
            com_Rigidbody2D,
            v3_Trajectory_Start,
            v3_Trajectory_Dir,
            Get_Trajectory_Step());

        com_LineRenderer.positionCount = trajectory.Length;
        Vector3[] position = new Vector3[trajectory.Length];

        for (int i = 0; i < position.Length; i++)
        {
            position[i] = trajectory[i];
        }

        com_LineRenderer.SetPositions(position);
    }

    public void Set_Trajectory_toLineRenderer_Clear(LineRenderer com_LineRenderer)
    {
        com_LineRenderer.positionCount = 0;
        com_LineRenderer.SetPositions(new Vector3[0]);
    }

    #endregion
}