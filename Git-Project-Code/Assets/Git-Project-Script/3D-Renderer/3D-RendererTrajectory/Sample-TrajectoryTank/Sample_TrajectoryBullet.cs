using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample_TrajectoryBullet : MonoBehaviour
{
    [Header("No hit")]

    [SerializeField] private Material m_Material_NotHit;

    [SerializeField] private float f_Coroutine_Destroy_NoHit = 10f;

    [Header("Hit")]

    [SerializeField] private Material m_Material_Hit;

    [SerializeField] private float f_Coroutine_Destroy_Hit = 1f;

    private MeshRenderer com_MessRenderer;

    private RigidbodyRotate cs_RigidbodyRotate;

    private void Awake()
    {
        if (GetComponent<MeshRenderer>() == null) gameObject.AddComponent<MeshRenderer>();
        com_MessRenderer = GetComponent<MeshRenderer>();
        com_MessRenderer.material = m_Material_NotHit;

        if (GetComponent<RigidbodyRotate>() == null) gameObject.AddComponent<RigidbodyRotate>();
        cs_RigidbodyRotate = GetComponent<RigidbodyRotate>();
        cs_RigidbodyRotate.Set_Rigidbody_isForward(true);

        StartCoroutine(Set_Bullet_Destroy(f_Coroutine_Destroy_NoHit));
    }

    private void OnCollisionEnter(Collision collision)
    {
        com_MessRenderer.material = m_Material_Hit;

        cs_RigidbodyRotate.Set_Rigidbody_isForward(false);

        StartCoroutine(Set_Bullet_Destroy(f_Coroutine_Destroy_Hit));
    }

    private IEnumerator Set_Bullet_Destroy(float f_Destory_AfterTime)
    {
        yield return new WaitForSeconds(f_Destory_AfterTime);

        Destroy(this.gameObject);
    }
}