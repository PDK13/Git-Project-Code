using UnityEngine;

[AddComponentMenu("Git-Project-Code/Rigidbody/Rigidbody-Collision")]
public class RigidbodyCollision : MonoBehaviour
{
    [SerializeField] private bool mAllowCollisionPassingThrough = true;

    [SerializeField] private bool mAllowRigidbodyMovingFast = false;

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
        if (mAllowCollisionPassingThrough && mAllowRigidbodyMovingFast)
        {
            com_Rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        }
        else
        if (mAllowCollisionPassingThrough)
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
        if (mAllowCollisionPassingThrough && mAllowRigidbodyMovingFast)
        {
            com_Rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        }
        else
        if (mAllowCollisionPassingThrough)
        {
            com_Rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        }
        else
        {
            com_Rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Discrete;
        }
    }
}