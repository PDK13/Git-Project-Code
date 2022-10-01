using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RigidbodyGravity))]
public class RendererTrajectory : MonoBehaviour
{
    //Main Dir is Vector.Right

    [Header("Trajectory Main")]

    [SerializeField] private float f_Trajectory_Power = 5f;

    [SerializeField] private int i_Trajectory_Step = 500;

    [Header("Trajectory Point")]

    [SerializeField] private Transform com_Trajectory_Start;

    [SerializeField] private Transform com_Trajectory_Next;

    [Header("Trajectory Raycast")]

    [SerializeField] private bool b_Trajectory_Raycast = false;

    [SerializeField] private LayerMask l_Trajectory_Raycast;

    [SerializeField] private float f_Trajectory_Raycast = 0.5f;

    private RigidbodyGravity cs_RigidbodyGravity;

    private void Awake()
    {
        if (GetComponent<RigidbodyGravity>() == null)
        {
            gameObject.AddComponent<RigidbodyGravity>();
        }

        cs_RigidbodyGravity = GetComponent<RigidbodyGravity>();
    }

    #region Trajectory Value

    #region Trajectory Start Point and Next Point

    public void Set_Trajectory_Start(Transform com_Trajectory_Start)
    {
        this.com_Trajectory_Start = com_Trajectory_Start;
    }

    public void Set_Trajectory_Start(Vector3 v3_Trajectory_Start)
    {
        com_Trajectory_Start.position = v3_Trajectory_Start;
    }

    public void Set_Trajectory_Start_Chance(Vector3 v3_Trajectory_Start_Chance)
    {
        Set_Trajectory_Start(Get_Trajectory_Start() + v3_Trajectory_Start_Chance);
    }

    public void Set_Trajectory_Next(Transform com_Trajectory_Next)
    {
        this.com_Trajectory_Next = com_Trajectory_Next;
    }

    public void Set_Trajectory_Next(Vector3 v3_Trajectory_Next)
    {
        com_Trajectory_Next.position = v3_Trajectory_Next;
    }

    public void Set_Trajectory_Next_Chance(Vector3 v3_Trajectory_Next_Chance)
    {
        Set_Trajectory_Next(Get_Trajectory_Next() + v3_Trajectory_Next_Chance);
    }

    public Transform Get_Trajectory_Start_toTransform()
    {
        return com_Trajectory_Start;
    }

    public Vector3 Get_Trajectory_Start()
    {
        return com_Trajectory_Start.position;
    }

    public Transform Get_Trajectory_Next_toTransform()
    {
        return com_Trajectory_Next;
    }

    public Vector3 Get_Trajectory_Next()
    {
        return com_Trajectory_Next.position;
    }

    public float Get_Trajectory_Duration()
    {
        return Get_Trajectory_Dir_Primary(false).magnitude;
    }

    #endregion

    #region Trajectory Power

    public void Set_Trajectory_Power(float f_Trajectory_Power)
    {
        this.f_Trajectory_Power = f_Trajectory_Power;
    }

    public void Set_Trajectory_Power_Chance(float f_Trajectory_Power_Chance)
    {
        f_Trajectory_Power += f_Trajectory_Power_Chance;
    }

    public float Get_Trajectory_Power()
    {
        return f_Trajectory_Power;
    }

    #endregion

    #region Trajectory Step

    public void Set_Trajectory_Step(int i_Trajectory_Step)
    {
        this.i_Trajectory_Step = i_Trajectory_Step;
    }

    public int Get_Trajectory_Step()
    {
        return i_Trajectory_Step;
    }

    #endregion

    #endregion

    #region Trajectory

    public Vector3[] Get_Trajectory_Points(float f_Rigidbody_Drag, bool b_Dir_Normalized)
    {
        Vector3[] v3_Trajectory_Result;

        List<Vector3> lv3_Trajectory_Result_List = new List<Vector3>();

        float f_TimeStep = Time.fixedDeltaTime / Physics.defaultSolverVelocityIterations;

        Vector3 v3_Gravity_Accel = cs_RigidbodyGravity.Get_Gravity_Global_toVector() * cs_RigidbodyGravity.Get_Gravity_Scale() * f_TimeStep * f_TimeStep;

        float f_Drag = 1f - f_TimeStep * f_Rigidbody_Drag;

        Vector3 v3_Trajectory_Dir = Get_Trajectory_Dir_Primary(b_Dir_Normalized);

        Vector3 v3_MoveStep = v3_Trajectory_Dir * f_TimeStep;

        Vector3 v3_Pos_Point = Get_Trajectory_Start();

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

    public Vector3 Get_Trajectory_Dir_Primary(bool b_Dir_Normalized)
    {
        if (b_Dir_Normalized)
        {
            return (Get_Trajectory_Next() - Get_Trajectory_Start()).normalized * f_Trajectory_Power;
        }

        return (Get_Trajectory_Next() - Get_Trajectory_Start()) * f_Trajectory_Power;
    }

    public Vector3 Get_Trajectory_Dir()
    {
        return (Get_Trajectory_Next() - Get_Trajectory_Start()) * f_Trajectory_Power;
    }

    #endregion

    #region Angle for hit Trajectory

    public float? Get_Trajectory_Angle_toDeg(Vector3 v3_Pos_Start, Vector3 v3_Pos_Tarket, bool b_Angle_High)
    {
        Vector3 v3_Tarket_Dir = v3_Pos_Tarket - v3_Pos_Start;

        float f_Y_High = v3_Tarket_Dir.y;

        v3_Tarket_Dir.y = 0f;

        float f_X_Duration = v3_Tarket_Dir.magnitude;

        float f_Gravity = cs_RigidbodyGravity.Get_Gravity_Global_toFloat() * cs_RigidbodyGravity.Get_Gravity_Scale();

        float f_Speed_SQR = f_Trajectory_Power * f_Trajectory_Power;

        float f_Under_SQR = (f_Speed_SQR * f_Speed_SQR) - f_Gravity * (f_Gravity * f_X_Duration * f_X_Duration + 2 * f_Y_High * f_Speed_SQR);

        if (f_Under_SQR >= 0)
        {
            float f_Under_SQRT = Mathf.Sqrt(f_Under_SQR);
            float f_Angle_High = f_Speed_SQR + f_Under_SQRT;
            float f_Angle_Low = f_Speed_SQR - f_Under_SQRT;

            if (b_Angle_High)
            {
                return Mathf.Atan2(f_Angle_High, f_Gravity * f_X_Duration) * Mathf.Rad2Deg;
            }
            else
            {
                return Mathf.Atan2(f_Angle_Low, f_Gravity * f_X_Duration) * Mathf.Rad2Deg;
            }
        }

        return null;
    }

    #endregion

    #region Rigidbody Velocity with Trajectory

    public void Set_Trajectory_toRigidbody(Rigidbody com_Rigidbody, Vector3 v3_Trajectory_Start, Vector3 v3_Trajectory_Next)
    {
        if (com_Rigidbody.GetComponent<RigidbodyGravity>() == null)
        {
            com_Rigidbody.gameObject.AddComponent<RigidbodyGravity>();
        }

        com_Rigidbody.GetComponent<RigidbodyGravity>().Set_Gravity_Scale(cs_RigidbodyGravity.Get_Gravity_Scale());
        com_Rigidbody.GetComponent<RigidbodyGravity>().Set_Rigidbody_Drag(cs_RigidbodyGravity.Get_Rigidbody_Drag());

        Vector3 v3_Trajectory_Dir = (v3_Trajectory_Next - v3_Trajectory_Start) * Get_Trajectory_Power();

        com_Rigidbody.velocity = v3_Trajectory_Dir;
    }

    public void Set_Trajectory_toRigidbody(Rigidbody2D com_Rigidbody2D, Vector2 v2_Trajectory_Start, Vector2 v2_Trajectory_Next)
    {
        Vector2 v2_Trajectory_Dir = (v2_Trajectory_Next - v2_Trajectory_Start) * Get_Trajectory_Power();

        //com_Rigidbody2D.drag = cs_RigidbodyGravity.Get_Rigidbody_Drag();
        com_Rigidbody2D.mass = 0;
        com_Rigidbody2D.gravityScale = cs_RigidbodyGravity.Get_Gravity_Scale();
        com_Rigidbody2D.velocity = v2_Trajectory_Dir;
    }

    #endregion

    #region Line Renderer with Trajectory

    public void Set_Trajectory_toLineRenderer(LineRenderer com_LineRenderer, float f_Rigidbody_Drag, bool b_Dir_Normalized)
    {
        Vector3[] trajectory = Get_Trajectory_Points(f_Rigidbody_Drag, b_Dir_Normalized);

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