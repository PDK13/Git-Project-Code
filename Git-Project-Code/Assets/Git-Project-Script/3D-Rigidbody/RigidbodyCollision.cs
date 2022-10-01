using UnityEngine;

[AddComponentMenu("Git-Project-Code/Rigidbody/Rigidbody Collision")]
public class RigidbodyCollision : MonoBehaviour
{
    [SerializeField] private bool m_Collision_PassingThrough = true;

    [SerializeField] private bool m_Rigidbody_MovingFast = false;

    private Rigidbody com_Rigidbody;

    private Rigidbody2D com_Rigidbody2D;

    private void Awake()
    {
        if (GetComponent<Rigidbody>() != null)
        {
            com_Rigidbody = GetComponent<Rigidbody>();

            SetRigidbody_Component_3D();
        }
        else
        if (GetComponent<Rigidbody2D>() != null)
        {
            com_Rigidbody2D = GetComponent<Rigidbody2D>();

            SetRigidbody_Component_2D();
        }
        else
        {
            Debug.LogErrorFormat("{0}: Require Componenet Rigidbody or Rigidbody2D.", name);
        }
    }

    private void SetRigidbody_Component_3D()
    {
        if (m_Collision_PassingThrough && m_Rigidbody_MovingFast)
        {
            com_Rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        }
        else
        if (m_Collision_PassingThrough)
        {
            com_Rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }
        else
        {
            com_Rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
        }
    }

    private void SetRigidbody_Component_2D()
    {
        if (m_Collision_PassingThrough && m_Rigidbody_MovingFast)
        {
            com_Rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        }
        else
        if (m_Collision_PassingThrough)
        {
            com_Rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        }
        else
        {
            com_Rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Discrete;
        }
    }
}