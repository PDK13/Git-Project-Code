using UnityEngine;

[AddComponentMenu("Git-Project-Code/Rigidbody/Rigidbody-Collision")]
public class RigidbodyCollision : MonoBehaviour
{
    [SerializeField] private bool m_CollisionPassingThrough = true;

    [SerializeField] private bool m_RigidbodyMovingFast = false;

    private Rigidbody m_Rigidbody;

    private Rigidbody2D m_Rigidbody2D;

    private void Awake()
    {
        if (GetComponent<Rigidbody>() != null)
        {
            m_Rigidbody = GetComponent<Rigidbody>();

            SetRigidbodyComponent3D();
        }
        else
        if (GetComponent<Rigidbody2D>() != null)
        {
            m_Rigidbody2D = GetComponent<Rigidbody2D>();

            SetRigidbodyComponent2D();
        }
        else
        {
            Debug.LogErrorFormat("{0}: Require Componenet Rigidbody or Rigidbody2D.", name);
        }
    }

    private void SetRigidbodyComponent3D()
    {
        if (m_CollisionPassingThrough && m_RigidbodyMovingFast)
        {
            m_Rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        }
        else
        if (m_CollisionPassingThrough)
        {
            m_Rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }
        else
        {
            m_Rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
        }
    }

    private void SetRigidbodyComponent2D()
    {
        if (m_CollisionPassingThrough && m_RigidbodyMovingFast)
        {
            m_Rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        }
        else
        if (m_CollisionPassingThrough)
        {
            m_Rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        }
        else
        {
            m_Rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Discrete;
        }
    }
}