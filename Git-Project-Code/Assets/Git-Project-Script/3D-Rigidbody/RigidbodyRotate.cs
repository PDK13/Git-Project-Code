using UnityEngine;

[AddComponentMenu("Git-Project-Code/Rigidbody/Rigidbody Rotate")]
public class RigidbodyRotate : MonoBehaviour
{
    [SerializeField] [Tooltip("Vector R - Red Axis")] private bool m_AllowFollowR = true;

    [SerializeField] [Tooltip("Vector Forward - Blue Axis")] private bool m_AllowFollowForward = false;

    [SerializeField] [Tooltip("Vector U - Green Axis")] private bool m_AllowFollowU = false;

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
            if (m_AllowFollowR)
            {
                transform.right = new Vector3(com_Rigidbody.velocity.x, com_Rigidbody.velocity.y, com_Rigidbody.velocity.z);
            }

            if (m_AllowFollowU)
            {
                transform.up = new Vector3(com_Rigidbody.velocity.x, com_Rigidbody.velocity.y, com_Rigidbody.velocity.z);
            }

            if (m_AllowFollowForward)
            {
                transform.forward = new Vector3(com_Rigidbody.velocity.x, com_Rigidbody.velocity.y, com_Rigidbody.velocity.z);
            }
        }
        else
        if (com_Rigidbody2D != null)
        {
            if (m_AllowFollowR)
            {
                transform.right = new Vector3(com_Rigidbody2D.velocity.x, com_Rigidbody2D.velocity.y);
            }

            if (m_AllowFollowU)
            {
                transform.up = new Vector3(com_Rigidbody2D.velocity.x, com_Rigidbody2D.velocity.y);
            }

            if (m_AllowFollowForward)
            {
                transform.forward = new Vector3(com_Rigidbody2D.velocity.x, com_Rigidbody2D.velocity.y);
            }
        }
    }

    public void SetFollowR(bool m_AllowFollowR)
    {
        this.m_AllowFollowR = m_AllowFollowR;
    }

    public bool GetCheckFollowR()
    {
        return m_AllowFollowR;
    }

    public void SetFollowU(bool m_AllowFollowU)
    {
        this.m_AllowFollowU = m_AllowFollowU ;
    }

    public bool GetCheckFollowU()
    {
        return m_AllowFollowU;
    }

    public void SetFollowForward(bool m_AllowFollowForward)
    {
        this.m_AllowFollowForward = m_AllowFollowForward;
    }

    public bool GetCheckFollowForward()
    {
        return m_AllowFollowForward;
    }
}