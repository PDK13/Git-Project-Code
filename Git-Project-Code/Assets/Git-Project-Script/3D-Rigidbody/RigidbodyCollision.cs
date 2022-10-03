using UnityEngine;

[AddComponentMenu("Git-Project-Code/Rigidbody/Rigidbody-Collision")]
public class RigidbodyCollision : MonoBehaviour
{
    [SerializeField] private bool m_AllowCollisionPassingThrough = true;

    [SerializeField] private bool m_AllowRigidbodyMovingFast = false;

    private Rigidbody com_Rigidbody;

    private Rigidbody2D com_Rigidbody2D;

    private void Awake()
    {
        if (GetComponent<Rigidbody>() != null)
        {
            com_Rigidbody = GetComponent<Rigidbody>();

            SetRigidbodyComponent3D();
        }
        else
        if (GetComponent<Rigidbody2D>() != null)
        {
            com_Rigidbody2D = GetComponent<Rigidbody2D>();

            SetRigidbodyComponent2D();
        }
        else
        {
            Debug.LogErrorFormat("{0}: Require Componenet Rigidbody or Rigidbody2D.", name);
        }
    }

    private void SetRigidbodyComponent3D()
    {
        if (m_AllowCollisionPassingThrough && m_AllowRigidbodyMovingFast)
        {
            com_Rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        }
        else
        if (m_AllowCollisionPassingThrough)
        {
            com_Rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }
        else
        {
            com_Rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
        }
    }

    private void SetRigidbodyComponent2D()
    {
        if (m_AllowCollisionPassingThrough && m_AllowRigidbodyMovingFast)
        {
            com_Rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        }
        else
        if (m_AllowCollisionPassingThrough)
        {
            com_Rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        }
        else
        {
            com_Rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Discrete;
        }
    }
}