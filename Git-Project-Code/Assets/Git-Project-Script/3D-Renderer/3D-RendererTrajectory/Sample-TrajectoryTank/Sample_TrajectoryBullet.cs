using System.Collections;
using UnityEngine;

public class Sample_TrajectoryBullet : MonoBehaviour
{
    private GameObject g_Tarket;

    [Header("No hit")]

    [SerializeField] private Material m_Material_NotHit;

    [SerializeField] private float m_Coroutine_Destroy_NoHit = 10f;

    [Header("Hit")]

    [SerializeField] private Material m_Material_Hit;

    [SerializeField] private float m_Coroutine_Destroy_Hit = 1f;

    private MeshRenderer com_MessRenderer;

    private RigidbodyRotate cm_RigidbodyRotate;

    private void Awake()
    {
        if (GetComponent<MeshRenderer>() == null)
        {
            gameObject.AddComponent<MeshRenderer>();
        }

        com_MessRenderer = GetComponent<MeshRenderer>();
        com_MessRenderer.material = m_Material_NotHit;

        if (GetComponent<RigidbodyRotate>() == null)
        {
            gameObject.AddComponent<RigidbodyRotate>();
        }

        cm_RigidbodyRotate = GetComponent<RigidbodyRotate>();
        cm_RigidbodyRotate.SetFollow_Right(true);

        StartCoroutine(SetBullet_Destroy(m_Coroutine_Destroy_NoHit));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.Equals(g_Tarket))
        {
            Debug.Log("Hit Tarket!");
        }

        com_MessRenderer.material = m_Material_Hit;

        cm_RigidbodyRotate.SetFollow_Right(false);

        StartCoroutine(SetBullet_Destroy(m_Coroutine_Destroy_Hit));
    }

    private IEnumerator SetBullet_Destroy(float m_Destory_AfterTime)
    {
        yield return new WaitForSeconds(m_Destory_AfterTime);

        Destroy(gameObject);
    }

    public void SetTarket(GameObject g_Tarket)
    {
        this.g_Tarket = g_Tarket;
    }
}
