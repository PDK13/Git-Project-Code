using UnityEngine;

[AddComponentMenu("Git-Project-Code/Rigidbody/Rigidbody Rotate")]
public class RigidbodyRotate : MonoBehaviour
{
    [SerializeField] [Tooltip("Vector Right - Red Axis")] private bool b_Follow_Right = true;

    [SerializeField] [Tooltip("Vector Forward - Blue Axis")] private bool b_Follow_Forward = false;

    [SerializeField] [Tooltip("Vector Up - Green Axis")] private bool b_Follow_Up = false;

    private Rigidbody com_Rigidbody;

    private Rigidbody2D com_Rigidbody2D;

    private void Awake()
    {
        if (GetComponent<Rigidbody>() != null)
        {
            com_Rigidbody = GetComponent<Rigidbody>();
        }
        else
        if (GetComponent<Rigidbody2D>() != null)
        {
            com_Rigidbody2D = GetComponent<Rigidbody2D>();
        }
        else
        {
            Debug.LogErrorFormat("{0}: Require Componenet Rigidbody or Rigidbody2D.", name);
        }
    }

    private void FixedUpdate()
    {
        if (com_Rigidbody != null)
        {
            if (b_Follow_Right)
            {
                transform.right = new Vector3(com_Rigidbody.velocity.x, com_Rigidbody.velocity.y, com_Rigidbody.velocity.z);
            }

            if (b_Follow_Up)
            {
                transform.up = new Vector3(com_Rigidbody.velocity.x, com_Rigidbody.velocity.y, com_Rigidbody.velocity.z);
            }

            if (b_Follow_Forward)
            {
                transform.forward = new Vector3(com_Rigidbody.velocity.x, com_Rigidbody.velocity.y, com_Rigidbody.velocity.z);
            }
        }
        else
        if (com_Rigidbody2D != null)
        {
            if (b_Follow_Right)
            {
                transform.right = new Vector3(com_Rigidbody2D.velocity.x, com_Rigidbody2D.velocity.y);
            }

            if (b_Follow_Up)
            {
                transform.up = new Vector3(com_Rigidbody2D.velocity.x, com_Rigidbody2D.velocity.y);
            }

            if (b_Follow_Forward)
            {
                transform.forward = new Vector3(com_Rigidbody2D.velocity.x, com_Rigidbody2D.velocity.y);
            }
        }
    }

    public void Set_Follow_Right(bool b_Follow_Right)
    {
        this.b_Follow_Right = b_Follow_Right;
    }

    public bool Get_Follow_Right()
    {
        return b_Follow_Right;
    }

    public void Set_Follow_Up(bool b_Follow_Up)
    {
        this.b_Follow_Up = b_Follow_Up;
    }

    public bool Get_Follow_Up()
    {
        return b_Follow_Up;
    }

    public void Set_Follow_Forward(bool b_Follow_Forward)
    {
        this.b_Follow_Forward = b_Follow_Forward;
    }

    public bool Get_Follow_Forward()
    {
        return b_Follow_Forward;
    }
}