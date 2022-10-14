using UnityEngine;

public class Sample_TrajectoryTanm_KeyAngle_AI : MonoBehaviour
{
    [SerializeField] private GameObject m_Bullet;

    [SerializeField] private Transform m_Gun;

    [SerializeField] private Transform m_Tarket;

    private RendererTrajectory m_RendererTrajectory;

    private LineRenderer m_LineRenderer;

    private Rigidbody m_Rigidbody;

    private RigidbodyRotation m_RigidbodyRotation;

    [SerializeField] private float m_DemCurrent = 0;

    [SerializeField] private bool m_AllowAngleHigh = true;

    private void Awake()
    {
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
        m_LineRenderer.startWidth = 0.2f;
        m_LineRenderer.endWidth = 0.2f;

        if (GetComponent<RendererTrajectory>() == null)
        {
            gameObject.AddComponent<RendererTrajectory>();
        }

        m_RendererTrajectory = GetComponent<RendererTrajectory>();
        m_RendererTrajectory.SetTrajectoryLineRendererClear(m_LineRenderer);

        if (GetComponent<RigidbodyRotation>() == null)
        {
            gameObject.AddComponent<RigidbodyRotation>();
        }

        m_RigidbodyRotation = GetComponent<RigidbodyRotation>();
    }

    private void Update()
    {
        //Power by X
        if (Input.GetKey(KeyCode.D))
        {
            m_RendererTrajectory.SetTrajectoryPowerChance(0.1f);
        }
        else
        if (Input.GetKey(KeyCode.A))
        {
            m_RendererTrajectory.SetTrajectoryPowerChance(-0.1f);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            m_AllowAngleHigh = !m_AllowAngleHigh;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject m_BulletClone = ClassObject.SetGameObjectCreate(m_Bullet);
            m_BulletClone.GetComponent<Sample_TrajectoryBullet>().SetTarket(m_Tarket.gameObject);
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
        try
        {
            m_DemCurrent = (float)m_RendererTrajectory.GetTrajectoryAngleDeg(
                m_RendererTrajectory.GetTrajectoryStartTransform().position,
                m_Tarket.transform.position,
                m_AllowAngleHigh);
        }
        catch
        {

        }

        m_Gun.rotation = ClassVector.GetRotationEulerToQuaternion(m_Gun.rotation.eulerAngles.x, m_Gun.rotation.eulerAngles.y, m_DemCurrent);

        m_RendererTrajectory.SetTrajectoryLineRenderer(m_LineRenderer, m_Rigidbody.drag, true);
    }
}