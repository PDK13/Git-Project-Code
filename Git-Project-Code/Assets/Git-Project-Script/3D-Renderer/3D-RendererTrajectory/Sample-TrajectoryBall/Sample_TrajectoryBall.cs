using UnityEngine;

[RequireComponent(typeof(RendererTrajectory))]
public class Sample_TrajectoryBall : MonoBehaviour
{
    private Vector2 v2_MouseDramStart;

    private Vector2 v2_MouseDramNext;

    private RendererTrajectory m_RendererTrajectory;

    private LineRenderer comLineRenderer;

    private Rigidbody com_Rigidbody;

    private void Awake()
    {
        if (GetComponent<RendererTrajectory>() == null)
        {
            gameObject.AddComponent<RendererTrajectory>();
        }

        m_RendererTrajectory = GetComponent<RendererTrajectory>();

        if (GetComponent<Rigidbody>() == null)
        {
            gameObject.AddComponent<Rigidbody>();
        }

        com_Rigidbody = GetComponent<Rigidbody>();

        if (GetComponent<LineRenderer>() == null)
        {
            gameObject.AddComponent<LineRenderer>();
        }

        comLineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            v2_MouseDramStart = transform.position;

            m_RendererTrajectory.SetTrajectoryStart(v2_MouseDramStart);
        }

        if (Input.GetMouseButton(0))
        {
            v2_MouseDramNext = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            m_RendererTrajectory.SetTrajectoryNext(v2_MouseDramNext);

            m_RendererTrajectory.SetTrajectoryLineRenderer(comLineRenderer, com_Rigidbody.drag, false);
        }

        if (Input.GetMouseButtonUp(0))
        {
            m_RendererTrajectory.SetTrajectoryRigidbody(com_Rigidbody, v2_MouseDramStart, v2_MouseDramNext);
        }
    }
}
