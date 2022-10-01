using UnityEngine;

[AddComponentMenu("Git-Project-Code/Rigidbody/Rigidbody Rotate")]
public class RigidbodyRotate : MonoBehaviour
{
    [SerializeField] [Tooltip("Vector Right - Red Axis")] private bool m_Follow_Right = true;

    [SerializeField] [Tooltip("Vector Forward - Blue Axis")] private bool m_Follow_Forward = false;

    [SerializeField] [Tooltip("Vector Up - Green Axis")] private bool m_Follow_Up = false;

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
            if (m_Follow_Right)
            {
                transform.right = new Vector3(com_Rigidbody.velocity.x, com_Rigidbody.velocity.y, com_Rigidbody.velocity.z);
            }

            if (m_Follow_Up)
            {
                transform.up = new Vector3(com_Rigidbody.velocity.x, com_Rigidbody.velocity.y, com_Rigidbody.velocity.z);
            }

            if (m_Follow_Forward)
            {
                transform.forward = new Vector3(com_Rigidbody.velocity.x, com_Rigidbody.velocity.y, com_Rigidbody.velocity.z);
            }
        }
        else
        if (com_Rigidbody2D != null)
        {
            if (m_Follow_Right)
            {
                transform.right = new Vector3(com_Rigidbody2D.velocity.x, com_Rigidbody2D.velocity.y);
            }

            if (m_Follow_Up)
            {
                transform.up = new Vector3(com_Rigidbody2D.velocity.x, com_Rigidbody2D.velocity.y);
            }

            if (m_Follow_Forward)
            {
                transform.forward = new Vector3(com_Rigidbody2D.velocity.x, com_Rigidbody2D.velocity.y);
            }
        }
    }

    public void Set_Follow_Right(bool m_Follow_Right)
    {
        this.m_Follow_Right = m_Follow_Right;
    }

    public bool GetFollow_Right()
    {
        return m_Follow_Right;
    }

    public void Set_Follow_Up(bool m_Follow_Up)
    {
        this.m_Follow_Up = m_Follow_Up;
    }

    public bool GetFollow_Up()
    {
        return m_Follow_Up;
    }

    public void Set_Follow_Forward(bool m_Follow_Forward)
    {
        this.m_Follow_Forward = m_Follow_Forward;
    }

    public bool GetFollow_Forward()
    {
        return m_Follow_Forward;
    }
}