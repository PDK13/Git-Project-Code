using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(LineRenderer))]
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

    [SerializeField] private LayerMask l_Raycast_Burden;

    private Rigidbody com_Rigidbody;

    private RigidbodyGravity cs_RigidbodyGravity;

    private void Awake()
    {
        if (GetComponent<Rigidbody>() == null) gameObject.AddComponent<Rigidbody>();
        com_Rigidbody = GetComponent<Rigidbody>();

        if (GetComponent<RigidbodyGravity>() == null) gameObject.AddComponent<RigidbodyGravity>();
        cs_RigidbodyGravity = GetComponent<RigidbodyGravity>();
    }

    private void OnDrawGizmos()
    {
        //if (GetComponent<Rigidbody>() == null) gameObject.AddComponent<Rigidbody>();
        com_Rigidbody = GetComponent<Rigidbody>();

        //if (GetComponent<RigidbodyGravity>() == null) gameObject.AddComponent<RigidbodyGravity>();
        cs_RigidbodyGravity = GetComponent<RigidbodyGravity>();

        Vector3 v3_Trajectory_Start;

        if (b_Trajectory_Start_isThis)
        {
            v3_Trajectory_Start = this.transform.position;
        }
        else
        {
            v3_Trajectory_Start = this.v3_Trajectory_Start;
        }

        Vector3 v3_Trajectory_Next;

        v3_Trajectory_Next = this.v3_Trajectory_Next;

        Vector3 v3_Trajectory_Dir = Get_Trajectory_Dir(v3_Trajectory_Start, v3_Trajectory_Next, f_Trajectory_Power);

        Vector3[] lv3_Trajectory_Points = Get_Trajectory_Points(com_Rigidbody, v3_Trajectory_Start, v3_Trajectory_Dir, i_Trajectory_Step);

        for (int i = 1; i < lv3_Trajectory_Points.Length; i++)
        {
            Gizmos.DrawLine(lv3_Trajectory_Points[i], lv3_Trajectory_Points[i - 1]);
        }
    }

    #region Set Rigidbody Velocity with Trajectory

    public void Set_Trajectory_toRigidbody(Rigidbody com_Rigidbody, Vector3 v3_Trajectory_Start, Vector3 v3_Trajectory_Next)
    {
        Vector3 v3_Trajectory_Dir = (v3_Trajectory_Next - v3_Trajectory_Start) * f_Trajectory_Power;

        com_Rigidbody.velocity = v3_Trajectory_Dir;
    }

    #endregion

    #region Set Line Renderer with Trajectory

    public void Set_Trajectory_toLineRenderer(LineRenderer com_LineRenderer, Vector3 v3_Trajectory_Start, Vector3 v3_Trajectory_Next)
    {
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

    #endregion

    #region Set Trajectory Start Point and Next Point

    public void Set_Trajectory_Start(Vector3 v3_Trajectory_Start)
    {
        this.v3_Trajectory_Start = v3_Trajectory_Start;
    }

    public void Set_Trajectory_Next(Vector3 v3_Trajectory_Next)
    {
        this.v3_Trajectory_Next = v3_Trajectory_Next;
    }

    #endregion

    #region Get Trajectory

    public Vector3 Get_Trajectory_Dir(Vector3 v3_Trajectory_Start, Vector3 v3_Trajectory_Next, float f_Trajectory_Power)
    {
        return (v3_Trajectory_Next - v3_Trajectory_Start) * f_Trajectory_Power;
    }

    public Vector3[] Get_Trajectory_Points(Rigidbody com_Rigidbody, Vector3 v3_Pos_Start, Vector3 v3_Trajectory_Dir, int i_Trajectory_Step)
    {
        Vector3[] v3_Trajectory_Result;

        List<Vector3> lv3_Trajectory_Result_List = new List<Vector3>();

        float f_TimeStep = Time.fixedDeltaTime / Physics.defaultSolverVelocityIterations;

        Vector3 v3_Gravity_Accel = Physics.gravity * cs_RigidbodyGravity.Get_Gravity_Scale() * f_TimeStep * f_TimeStep;

        float f_Drag = 1f - f_TimeStep * com_Rigidbody.drag;
        Vector3 v3_MoveStep = v3_Trajectory_Dir * f_TimeStep;

        for (int i = 0; i < i_Trajectory_Step; i++)
        {
            v3_MoveStep += v3_Gravity_Accel;
            v3_MoveStep *= f_Drag;
            v3_Pos_Start += v3_MoveStep;

            if (b_Trajectory_Raycast)
            {
                bool ray_Raycast = Physics.Linecast(v3_Pos_Start + Vector3.down * 1f, v3_Pos_Start - Vector3.down * 1f, l_Raycast_Burden);

                if (ray_Raycast)
                {
                    v3_Trajectory_Result = new Vector3[lv3_Trajectory_Result_List.Count];
                    v3_Trajectory_Result = lv3_Trajectory_Result_List.ToArray();
                    return v3_Trajectory_Result;
                }
                else
                {
                    lv3_Trajectory_Result_List.Add(v3_Pos_Start);
                }
            }
            else
            {
                lv3_Trajectory_Result_List.Add(v3_Pos_Start);
            }
        }

        v3_Trajectory_Result = new Vector3[lv3_Trajectory_Result_List.Count];
        v3_Trajectory_Result = lv3_Trajectory_Result_List.ToArray();
        return v3_Trajectory_Result;
    }

    #endregion

    #region Get Trajectory Value

    #region Get Trajectory Value Main

    public float Get_Trajectory_Power()
    {
        return this.f_Trajectory_Power;
    }

    public int Get_Trajectory_Step()
    {
        return this.i_Trajectory_Step;
    }

    #endregion

    #region Get Trajectory Value Start Point

    public bool Get_Trajectory_Start_isThis()
    {
        return b_Trajectory_Start_isThis;
    }

    public Vector3 Get_Trajectory_Start()
    {
        return v3_Trajectory_Start;
    }

    #endregion

    #region Get Trajectory Value End Point

    public Vector3 Get_Trajectory_Next()
    {
        return v3_Trajectory_Next;
    }

    #endregion

    #endregion
}
