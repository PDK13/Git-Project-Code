using UnityEngine;

public class Sample_TrajectoryTank_Angle_AI : MonoBehaviour
{
    [SerializeField] private GameObject g_Bullet;

    [SerializeField] private Transform com_Gun;

    [SerializeField] private Transform com_Tarket;

    private RendererTrajectory cm_RendererTrajectory;

    private LineRenderer com_LineRenderer;

    private Rigidbody com_Rigidbody;

    private RigidbodyRotation cm_RigidbodyRotation;

    [SerializeField] private float m_Deg_Cur = 0;

    [SerializeField] private bool m_Deg_High = true;

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

        com_LineRenderer = GetComponent<LineRenderer>();
        com_LineRenderer.startWidth = 0.2f;
        com_LineRenderer.endWidth = 0.2f;

        if (GetComponent<RendererTrajectory>() == null)
        {
            gameObject.AddComponent<RendererTrajectory>();
        }

        cm_RendererTrajectory = GetComponent<RendererTrajectory>();
        cm_RendererTrajectory.SetTrajectory_toLineRenderer_Clear(com_LineRenderer);

        if (GetComponent<RigidbodyRotation>() == null)
        {
            gameObject.AddComponent<RigidbodyRotation>();
        }

        cm_RigidbodyRotation = GetComponent<RigidbodyRotation>();
    }

    private void Update()
    {
        //Power by X
        if (Input.GetKey(KeyCode.D))
        {
            cm_RendererTrajectory.SetTrajectory_Power_Chance(0.1f);
        }
        else
        if (Input.GetKey(KeyCode.A))
        {
            cm_RendererTrajectory.SetTrajectory_Power_Chance(-0.1f);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            m_Deg_High = !m_Deg_High;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject g_BulletClone = Clasm_Object.SetGameObject_Create(g_Bullet);
            g_BulletClone.GetComponent<Sample_TrajectoryBullet>().SetTarket(com_Tarket.gameObject);
            g_BulletClone.transform.position = cm_RendererTrajectory.GetTrajectory_Start();
            g_BulletClone.SetActive(true);

            cm_RendererTrajectory.SetTrajectory_toRigidbody(
                g_BulletClone.GetComponent<Rigidbody>(),
                cm_RendererTrajectory.GetTrajectory_Start(),
                cm_RendererTrajectory.GetTrajectory_Next());

            cm_RigidbodyRotation.SetControlIsLock(false);
        }
    }

    private void FixedUpdate()
    {
        try
        {
            m_Deg_Cur = (float)cm_RendererTrajectory.GetTrajectory_Angle_toDeg(
                cm_RendererTrajectory.GetTrajectory_Start_toTransform().position,
                com_Tarket.transform.position,
                m_Deg_High);
        }
        catch
        {

        }

        com_Gun.rotation = ClassVector.GetRotationEulerToQuaternion(com_Gun.rotation.eulerAngles.x, com_Gun.rotation.eulerAngles.y, m_Deg_Cur);

        cm_RendererTrajectory.SetTrajectory_toLineRenderer(com_LineRenderer, com_Rigidbody.drag, true);
    }
}