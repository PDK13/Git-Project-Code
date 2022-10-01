using UnityEngine;

public class Sample_TrajectoryTank_Primary : MonoBehaviour
{
    [SerializeField] private GameObject g_Bullet;

    private RendererTrajectory cm_RendererTrajectory;

    private LineRenderer com_LineRenderer;

    private Rigidbody com_Rigidbody;

    private RigidbodyRotation cm_RigidbodyRotation;

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
        //High by Y
        if (Input.GetKey(KeyCode.W))
        {
            cm_RendererTrajectory.SetTrajectory_Next_Chance(Vector3.up * 0.01f);
        }
        else
        if (Input.GetKey(KeyCode.S))
        {
            cm_RendererTrajectory.SetTrajectory_Next_Chance(Vector3.down * 0.01f);
        }

        //Power by X
        if (Input.GetKey(KeyCode.D))
        {
            cm_RendererTrajectory.SetTrajectory_Next_Chance(transform.right * 0.01f);
        }
        else
        if (Input.GetKey(KeyCode.A))
        {
            cm_RendererTrajectory.SetTrajectory_Next_Chance(transform.right * 0.01f * -1);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject g_BulletClone = Clasm_Object.SetGameObject_Create(g_Bullet);
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
        cm_RendererTrajectory.SetTrajectory_toLineRenderer(com_LineRenderer, com_Rigidbody.drag, false);
    }
}
