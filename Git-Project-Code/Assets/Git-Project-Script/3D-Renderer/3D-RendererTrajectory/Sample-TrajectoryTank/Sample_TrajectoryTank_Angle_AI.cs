using UnityEngine;

public class Sample_TrajectoryTanm_KeyAngle_AI : MonoBehaviour
{
    [SerializeField] private GameObject m_Bullet;

    [SerializeField] private Transform com_Gun;

    [SerializeField] private Transform com_Tarket;

    private RendererTrajectory m_RendererTrajectory;

    private LineRenderer comLineRenderer;

    private Rigidbody com_Rigidbody;

    private RigidbodyRotation m_RigidbodyRotation;

    [SerializeField] private float m_DemCurrent = 0;

    [SerializeField] private bool mAllowDem_High = true;

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
            mAllowDem_High = !mAllowDem_High;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject m_BulletClone = ClassObject.SetGameObjectCreate(m_Bullet);
            m_BulletClone.GetComponent<Sample_TrajectoryBullet>().SetTarket(com_Tarket.gameObject);
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
                com_Tarket.transform.position,
                mAllowDem_High);
        }
        catch
        {

        }

        com_Gun.rotation = ClassVector.GetRotationEulerToQuaternion(com_Gun.rotation.eulerAngles.x, com_Gun.rotation.eulerAngles.y, m_DemCurrent);

        m_RendererTrajectory.SetTrajectoryLineRenderer(comLineRenderer, com_Rigidbody.drag, true);
    }
}