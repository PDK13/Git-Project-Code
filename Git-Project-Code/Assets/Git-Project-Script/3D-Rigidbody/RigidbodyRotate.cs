using UnityEngine;

[AddComponentMenu("Git-Project-Code/Rigidbody/Rigidbody Rotate")]
public class RigidbodyRotate : MonoBehaviour
{
    [SerializeField] [Tooltip("Vector Right - Red Axis")] private bool m_AllowFollowRight = true;

    [SerializeField] [Tooltip("Vector Forward - Blue Axis")] private bool m_AllowFollowForward = false;

    [SerializeField] [Tooltip("Vector Up - Green Axis")] private bool m_AllowFollowUp = false;

    private Rigidbody m_Rigidbody;

    private Rigidbody2D m_Rigidbody2D;

    private void Awake()
    {
        if (GetComponent<Rigidbody>() != null)
        {
            m_Rigidbody = GetComponent<Rigidbody>();
        }
        else
        if (GetComponent<Rigidbody2D>() != null)
        {
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
        }
        else
        {
            Debug.LogErrorFormat("{0}: Require Componenet Rigidbody or Rigidbody2D.", name);
        }
    }

    private void FixedUpdate()
    {
        if (m_Rigidbody != null)
        {
            if (m_AllowFollowRight)
            {
                transform.right = new Vector3(m_Rigidbody.velocity.x, m_Rigidbody.velocity.y, m_Rigidbody.velocity.z);
            }

            if (m_AllowFollowUp)
            {
                transform.up = new Vector3(m_Rigidbody.velocity.x, m_Rigidbody.velocity.y, m_Rigidbody.velocity.z);
            }

            if (m_AllowFollowForward)
            {
                transform.forward = new Vector3(m_Rigidbody.velocity.x, m_Rigidbody.velocity.y, m_Rigidbody.velocity.z);
            }
        }
        else
        if (m_Rigidbody2D != null)
        {
            if (m_AllowFollowRight)
            {
                transform.right = new Vector3(m_Rigidbody2D.velocity.x, m_Rigidbody2D.velocity.y);
            }

            if (m_AllowFollowUp)
            {
                transform.up = new Vector3(m_Rigidbody2D.velocity.x, m_Rigidbody2D.velocity.y);
            }

            if (m_AllowFollowForward)
            {
                transform.forward = new Vector3(m_Rigidbody2D.velocity.x, m_Rigidbody2D.velocity.y);
            }
        }
    }

    public void SetFollowR(bool m_AllowFollowR)
    {
        this.m_AllowFollowRight = m_AllowFollowR;
    }

    public bool GetCheckFollowR()
    {
        return m_AllowFollowRight;
    }

    public void SetFollowU(bool m_AllowFollowU)
    {
        this.m_AllowFollowUp = m_AllowFollowU ;
    }

    public bool GetCheckFollowU()
    {
        return m_AllowFollowUp;
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