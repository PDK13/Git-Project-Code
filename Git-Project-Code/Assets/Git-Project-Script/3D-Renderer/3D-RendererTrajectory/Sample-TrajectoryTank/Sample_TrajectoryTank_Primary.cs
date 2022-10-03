using UnityEngine;

public class Sample_TrajectoryTanm_KeyPrimary : MonoBehaviour
{
    [SerializeField] private GameObject m_Bullet;

    private RendererTrajectory m_RendererTrajectory;

    private LineRenderer comLineRenderer;

    private Rigidbody com_Rigidbody;

    private RigidbodyRotation m_RigidbodyRotation;

    private void Awake()
    {
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
        comLineRenderer.startWidth = 0.2f;
        comLineRenderer.endWidth = 0.2f;

        if (GetComponent<RendererTrajectory>() == null)
        {
            gameObject.AddComponent<RendererTrajectory>();
        }

        m_RendererTrajectory = GetComponent<RendererTrajectory>();
        m_RendererTrajectory.SetTrajectoryLineRendererClear(comLineRenderer);

        if (GetComponent<RigidbodyRotation>() == null)
        {
            gameObject.AddComponent<RigidbodyRotation>();
        }

        m_RigidbodyRotation = GetComponent<RigidbodyRotation>();
    }

    private void Update()
    {
        //High by Y
        if (Input.GetKey(KeyCode.W))
        {
            m_RendererTrajectory.SetTrajectoryNextChance(Vector3.up * 0.01f);
        }
        else
        if (Input.GetKey(KeyCode.S))
        {
            m_RendererTrajectory.SetTrajectoryNextChance(Vector3.down * 0.01f);
        }

        //Power by X
        if (Input.GetKey(KeyCode.D))
        {
            m_RendererTrajectory.SetTrajectoryNextChance(transform.right * 0.01f);
        }
        else
        if (Input.GetKey(KeyCode.A))
        {
            m_RendererTrajectory.SetTrajectoryNextChance(transform.right * 0.01f * -1);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject m_BulletClone = ClassObject.SetGameObjectCreate(m_Bullet);
            m_BulletClone.transform.position = m_RendererTrajectory.GetTrajectoryStart();
            m_BulletClone.SetActive(true);

            m_RendererTrajectory.SetTrajectoryRigidbody(
                m_BulletClone.GetComponent<Rigidbody>(),
                m_RendererTrajectory.GetTrajectoryStart(),
                m_RendererTrajectory.GetTrajectoryNext());

            m_RigidbodyRotation.SetControlLock(false);
        }
    }

    private void FixedUpdate()
    {
        m_RendererTrajectory.SetTrajectoryLineRenderer(comLineRenderer, com_Rigidbody.drag, false);
    }
}
