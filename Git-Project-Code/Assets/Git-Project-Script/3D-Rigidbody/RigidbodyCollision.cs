using UnityEngine;

[AddComponentMenu("Git-Project-Code/Rigidbody/Rigidbody-Collision")]
public class RigidbodyCollision : MonoBehaviour
{
    [SerializeField] private bool mAllowCollisionPassingThrough = true;

    [SerializeField] private bool mAllowRigidbodyMovingFast = false;

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
        if (mAllowCollisionPassingThrough && mAllowRigidbodyMovingFast)
        {
            m_Rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        }
        else
        if (mAllowCollisionPassingThrough)
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
        if (mAllowCollisionPassingThrough && mAllowRigidbodyMovingFast)
        {
            m_Rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        }
        else
        if (mAllowCollisionPassingThrough)
        {
            m_Rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        }
        else
        {
            m_Rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Discrete;
        }
    }
}