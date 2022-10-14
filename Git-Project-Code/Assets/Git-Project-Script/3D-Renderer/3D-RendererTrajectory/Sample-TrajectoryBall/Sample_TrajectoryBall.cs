using UnityEngine;

[RequireComponent(typeof(RendererTrajectory))]
public class Sample_TrajectoryBall : MonoBehaviour
{
    private Vector2 m_MouseDramStart;

    private Vector2 m_MouseDramNext;

    private RendererTrajectory m_RendererTrajectory;

    private LineRenderer m_LineRenderer;

    private Rigidbody m_Rigidbody;

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

        m_Rigidbody = GetComponent<Rigidbody>();

        if (GetComponent<LineRenderer>() == null)
        {
            gameObject.AddComponent<LineRenderer>();
        }

        m_LineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_MouseDramStart = transform.position;

            m_RendererTrajectory.SetTrajectoryStart(m_MouseDramStart);
        }

        if (Input.GetMouseButton(0))
        {
            m_MouseDramNext = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            m_RendererTrajectory.SetTrajectoryNext(m_MouseDramNext);

            m_RendererTrajectory.SetTrajectoryLineRenderer(m_LineRenderer, m_Rigidbody.drag, false);
        }

        if (Input.GetMouseButtonUp(0))
        {
            m_RendererTrajectory.SetTrajectoryRigidbody(m_Rigidbody, m_MouseDramStart, m_MouseDramNext);
        }
    }
}
