using System.Collections;
using UnityEngine;

public class Sample_TrajectoryBullet : MonoBehaviour
{
    private GameObject m_Tarket;

    [Header("No hit")]

    [SerializeField] private Material m_MaterialNotHit;

    [SerializeField] private float m_CoroutineDestroy_NoHit = 10f;

    [Header("Hit")]

    [SerializeField] private Material m_MaterialHit;

    [SerializeField] private float m_CoroutineDestroy_Hit = 1f;

    private MeshRenderer com_MessRenderer;

    private RigidbodyRotate m_RigidbodyRotate;

    private void Awake()
    {
        if (GetComponent<MeshRenderer>() == null)
        {
            gameObject.AddComponent<MeshRenderer>();
        }

        com_MessRenderer = GetComponent<MeshRenderer>();
        com_MessRenderer.material = m_MaterialNotHit;

        if (GetComponent<RigidbodyRotate>() == null)
        {
            gameObject.AddComponent<RigidbodyRotate>();
        }

        m_RigidbodyRotate = GetComponent<RigidbodyRotate>();
        m_RigidbodyRotate.SetFollowR(true);

        StartCoroutine(SetBulletDestroy(m_CoroutineDestroy_NoHit));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.Equals(m_Tarket))
        {
            Debug.Log("Hit Tarket!");
        }

        com_MessRenderer.material = m_MaterialHit;

        m_RigidbodyRotate.SetFollowR(false);

        StartCoroutine(SetBulletDestroy(m_CoroutineDestroy_Hit));
    }

    private IEnumerator SetBulletDestroy(float m_Destory_AfterTime)
    {
        yield return new WaitForSeconds(m_Destory_AfterTime);

        Destroy(gameObject);
    }

    public void SetTarket(GameObject m_Tarket)
    {
        this.m_Tarket = m_Tarket;
    }
}
