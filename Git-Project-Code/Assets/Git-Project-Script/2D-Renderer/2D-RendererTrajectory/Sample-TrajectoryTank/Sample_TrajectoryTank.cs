using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample_TrajectoryTank : MonoBehaviour
{
    [SerializeField] private GameObject g_Bullet;

    [SerializeField] private Transform com_TransformPoint_Start;

    [SerializeField] private Transform com_TransformPoint_Next;

    [SerializeField] private Material ma_Material_LineRenderer;

    private RendererTrajectory cs_RendererTrajectory;

    private LineRenderer com_LineRenderer;

    private Rigidbody com_Rigidbody;

    private RigidbodyRotation cs_RigidbodyRotation;

    private void Awake()
    {
        if (GetComponent<Rigidbody>() == null) gameObject.AddComponent<Rigidbody>();
        com_Rigidbody = GetComponent<Rigidbody>();

        if (GetComponent<LineRenderer>() == null) gameObject.AddComponent<LineRenderer>();
        com_LineRenderer = GetComponent<LineRenderer>();

        if (GetComponent<RendererTrajectory>() == null) gameObject.AddComponent<RendererTrajectory>();
        cs_RendererTrajectory = GetComponent<RendererTrajectory>();
        cs_RendererTrajectory.Set_Trajectory_Start_isThis(false);
        cs_RendererTrajectory.Set_Trajectory_Start(com_TransformPoint_Start.transform.position);
        cs_RendererTrajectory.Set_Trajectory_toLineRenderer_Clear(com_LineRenderer);

        if (GetComponent<RigidbodyRotation>() == null) gameObject.AddComponent<RigidbodyRotation>();
        cs_RigidbodyRotation = GetComponent<RigidbodyRotation>();

        //if (GetComponent<Rigidbody>() == null) gameObject.AddComponent<Rigidbody>();
        //com_Rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            cs_RendererTrajectory.Set_Trajectory_toLineRenderer(
                com_LineRenderer,
                com_TransformPoint_Start.transform.position,
                com_TransformPoint_Next.transform.position,
                0.1f,
                ma_Material_LineRenderer);

            cs_RigidbodyRotation.Set_Control_isLock(true);

            //High by Y
            if (Input.GetKey(KeyCode.UpArrow))
            {
                com_TransformPoint_Next.transform.position += Vector3.up * 0.01f;

                cs_RendererTrajectory.Set_Trajectory_Next(com_TransformPoint_Next.transform.position);
            }
            else
            if (Input.GetKey(KeyCode.DownArrow))
            {
                com_TransformPoint_Next.transform.position += Vector3.down * 0.01f;

                cs_RendererTrajectory.Set_Trajectory_Next(com_TransformPoint_Next.transform.position);
            }

            //Power by X
            if (Input.GetKey(KeyCode.RightArrow))
            {
                com_TransformPoint_Next.transform.position += com_TransformPoint_Next.parent.right * 0.01f;

                cs_RendererTrajectory.Set_Trajectory_Next(com_TransformPoint_Next.transform.position);
            }
            else
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                com_TransformPoint_Next.transform.position -= com_TransformPoint_Next.parent.right * 0.01f;

                cs_RendererTrajectory.Set_Trajectory_Next(com_TransformPoint_Next.transform.position);
            }
        }
        
        if (Input.GetKeyUp(KeyCode.Space))
        {
            GameObject g_Bullet_Clone = Class_Object.Set_Prepab_Create(g_Bullet);
            g_Bullet_Clone.transform.position = com_TransformPoint_Start.transform.position;
            g_Bullet_Clone.SetActive(true);

            cs_RendererTrajectory.Set_Trajectory_toRigidbody(
                g_Bullet_Clone.GetComponent<Rigidbody>(),
                com_TransformPoint_Start.transform.position,
                com_TransformPoint_Next.transform.position);

            cs_RendererTrajectory.Set_Trajectory_toLineRenderer_Clear(com_LineRenderer);

            cs_RigidbodyRotation.Set_Control_isLock(false);
        }
    }

    private void OnDrawGizmos()
    {
        if (com_Rigidbody == null || cs_RendererTrajectory == null)
        {
            return;
        }

        Vector3 v3_Trajectory_Dir = cs_RendererTrajectory.Get_Trajectory_Dir(
            com_TransformPoint_Start.transform.position, 
            com_TransformPoint_Next.transform.position, 
            cs_RendererTrajectory.Get_Trajectory_Power());

        Vector3[] lv3_Trajectory_Points = cs_RendererTrajectory.Get_Trajectory_Points(
            com_Rigidbody,
            com_TransformPoint_Start.transform.position, 
            v3_Trajectory_Dir);

        for (int i = 1; i < lv3_Trajectory_Points.Length; i++)
        {
            Gizmos.DrawLine(lv3_Trajectory_Points[i], lv3_Trajectory_Points[i - 1]);
        }
    }
}
