using UnityEngine;

[RequireComponent(typeof(RendererTrajectory))]
public class Sample_TrajectoryBall : MonoBehaviour
{
    private Vector2 v2_MouseDrag_Start;

    private Vector2 v2_MouseDrag_Next;

    private RendererTrajectory cm_RendererTrajectory;

    private LineRenderer com_LineRenderer;

    private Rigidbody com_Rigidbody;

    private void Awake()
    {
        if (GetComponent<RendererTrajectory>() == null)
        {
            gameObject.AddComponent<RendererTrajectory>();
        }

        cm_RendererTrajectory = GetComponent<RendererTrajectory>();

        if (GetComponent<Rigidbody>() == null)
        {
            gameObject.AddComponent<Rigidbody>();
        }

        com_Rigidbody = GetComponent<Rigidbody>();

        if (GetComponent<LineRenderer>() == null)
        {
            gameObject.AddComponent<LineRenderer>();
        }

        com_LineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            v2_MouseDrag_Start = transform.position;

            cm_RendererTrajectory.SetTrajectory_Start(v2_MouseDrag_Start);
        }

        if (Input.GetMouseButton(0))
        {
            v2_MouseDrag_Next = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            cm_RendererTrajectory.SetTrajectory_Next(v2_MouseDrag_Next);

            cm_RendererTrajectory.SetTrajectory_toLineRenderer(com_LineRenderer, com_Rigidbody.drag, false);
        }

        if (Input.GetMouseButtonUp(0))
        {
            cm_RendererTrajectory.SetTrajectory_toRigidbody(com_Rigidbody, v2_MouseDrag_Start, v2_MouseDrag_Next);
        }
    }
}
