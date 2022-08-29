using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample_TrajectoryTank_Angle : MonoBehaviour
{
    [SerializeField] private GameObject g_Bullet;

    [SerializeField] Transform com_Gun;

    private RendererTrajectory cs_RendererTrajectory;

    private LineRenderer com_LineRenderer;

    private Rigidbody com_Rigidbody;

    private RigidbodyRotation cs_RigidbodyRotation;

    [SerializeField] private float f_Deg_Cur = 0;

    private void Awake()
    {
        if (GetComponent<Rigidbody>() == null) gameObject.AddComponent<Rigidbody>();
        com_Rigidbody = GetComponent<Rigidbody>();

        if (GetComponent<LineRenderer>() == null) gameObject.AddComponent<LineRenderer>();
        com_LineRenderer = GetComponent<LineRenderer>();
        com_LineRenderer.startWidth = 0.2f;
        com_LineRenderer.endWidth = 0.2f;

        if (GetComponent<RendererTrajectory>() == null) gameObject.AddComponent<RendererTrajectory>();
        cs_RendererTrajectory = GetComponent<RendererTrajectory>();
        cs_RendererTrajectory.Set_Trajectory_toLineRenderer_Clear(com_LineRenderer);

        if (GetComponent<RigidbodyRotation>() == null) gameObject.AddComponent<RigidbodyRotation>();
        cs_RigidbodyRotation = GetComponent<RigidbodyRotation>();
    }

    private void Update()
    {
        //Deg by Y
        if (Input.GetKey(KeyCode.W))
        {
            f_Deg_Cur += 1f;
        }
        else
        if (Input.GetKey(KeyCode.S))
        {
            f_Deg_Cur -= 1f;
        }

        com_Gun.rotation = Class_Vector.Get_Rot_EulerToQuaternion(com_Gun.rotation.eulerAngles.x, com_Gun.rotation.eulerAngles.y, f_Deg_Cur);

        //Power by X
        if (Input.GetKey(KeyCode.D))
        {
            cs_RendererTrajectory.Set_Trajectory_Power_Chance(0.1f);
        }
        else
        if (Input.GetKey(KeyCode.A))
        {
            cs_RendererTrajectory.Set_Trajectory_Power_Chance(-0.1f);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject g_Bullet_Clone = Class_Object.Set_Prepab_Create(g_Bullet);
            g_Bullet_Clone.transform.position = cs_RendererTrajectory.Get_Trajectory_Start();
            g_Bullet_Clone.SetActive(true);

            cs_RendererTrajectory.Set_Trajectory_toRigidbody(
                g_Bullet_Clone.GetComponent<Rigidbody>(),
                cs_RendererTrajectory.Get_Trajectory_Start(),
                cs_RendererTrajectory.Get_Trajectory_Next());

            cs_RigidbodyRotation.Set_Control_isLock(false);
        }
    }

    private void FixedUpdate()
    {
        cs_RendererTrajectory.Set_Trajectory_toLineRenderer(com_LineRenderer, com_Rigidbody.drag, true);
    }
}
