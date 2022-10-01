using UnityEngine;

[AddComponentMenu("Git-Project-Code/Rigidbody/Rigidbody Collision")]
public class RigidbodyCollision : MonoBehaviour
{
    [SerializeField] private bool b_Collision_PassingThrough = true;

    [SerializeField] private bool b_Rigidbody_MovingFast = false;

    private Rigidbody com_Rigidbody;

    private Rigidbody2D com_Rigidbody2D;

    private void Awake()
    {
        if (GetComponent<Rigidbody>() != null)
        {
            com_Rigidbody = GetComponent<Rigidbody>();

            Set_Rigidbody_Component_3D();
        }
        else
        if (GetComponent<Rigidbody2D>() != null)
        {
            com_Rigidbody2D = GetComponent<Rigidbody2D>();

            Set_Rigidbody_Component_2D();
        }
        else
        {
            Debug.LogErrorFormat("{0}: Require Componenet Rigidbody or Rigidbody2D.", name);
        }
    }

    private void Set_Rigidbody_Component_3D()
    {
        if (b_Collision_PassingThrough && b_Rigidbody_MovingFast)
        {
            com_Rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        }
        else
        if (b_Collision_PassingThrough)
        {
            com_Rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }
        else
        {
            com_Rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;
        }
    }

    private void Set_Rigidbody_Component_2D()
    {
        if (b_Collision_PassingThrough && b_Rigidbody_MovingFast)
        {
            com_Rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        }
        else
        if (b_Collision_PassingThrough)
        {
            com_Rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        }
        else
        {
            com_Rigidbody2D.collisionDetectionMode = CollisionDetectionMode2D.Discrete;
        }
    }
}