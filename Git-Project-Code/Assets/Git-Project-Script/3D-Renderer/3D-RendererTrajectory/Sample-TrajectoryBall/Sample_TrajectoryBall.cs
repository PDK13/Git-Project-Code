using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RendererTrajectory))]
public class Sample_TrajectoryBall : MonoBehaviour
{
    private Vector2 v2_MouseDrag_Start;

    private Vector2 v2_MouseDrag_Next;

    private RendererTrajectory cs_RendererTrajectory;

    private LineRenderer com_LineRenderer;

    private Rigidbody com_Rigidbody;

    private void Awake()
    {
        if (GetComponent<RendererTrajectory>() == null) gameObject.AddComponent<RendererTrajectory>();
        cs_RendererTrajectory = GetComponent<RendererTrajectory>();

        if (GetComponent<Rigidbody>() == null) gameObject.AddComponent<Rigidbody>();
        com_Rigidbody = GetComponent<Rigidbody>();

        if (GetComponent<LineRenderer>() == null) gameObject.AddComponent<LineRenderer>();
        com_LineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            v2_MouseDrag_Start = this.transform.position;

            cs_RendererTrajectory.Set_Trajectory_Start(v2_MouseDrag_Start);
        }

        if (Input.GetMouseButton(0))
        {
            v2_MouseDrag_Next = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            cs_RendererTrajectory.Set_Trajectory_Next(v2_MouseDrag_Next);

            cs_RendererTrajectory.Set_Trajectory_toLineRenderer(com_LineRenderer, com_Rigidbody.drag, false);
        }

        if (Input.GetMouseButtonUp(0))
        {
            cs_RendererTrajectory.Set_Trajectory_toRigidbody(com_Rigidbody, v2_MouseDrag_Start, v2_MouseDrag_Next);
        }
    }
}
