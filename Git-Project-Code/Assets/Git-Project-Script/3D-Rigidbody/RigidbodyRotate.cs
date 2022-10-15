using UnityEngine;

[AddComponentMenu("Git-Project-Code/Rigidbody/Rigidbody Rotate")]
public class RigidbodyRotate : MonoBehaviour
{
    [SerializeField] [Tooltip("Vector Right - Red Axis")] private bool m_FollowRight = true;

    [SerializeField] [Tooltip("Vector Forward - Blue Axis")] private bool m_FollowForward = false;

    [SerializeField] [Tooltip("Vector Up - Green Axis")] private bool m_FollowUp = false;

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
            if (m_FollowRight)
            {
                transform.right = new Vector3(m_Rigidbody.velocity.x, m_Rigidbody.velocity.y, m_Rigidbody.velocity.z);
            }

            if (m_FollowUp)
            {
                transform.up = new Vector3(m_Rigidbody.velocity.x, m_Rigidbody.velocity.y, m_Rigidbody.velocity.z);
            }

            if (m_FollowForward)
            {
                transform.forward = new Vector3(m_Rigidbody.velocity.x, m_Rigidbody.velocity.y, m_Rigidbody.velocity.z);
            }
        }
        else
        if (m_Rigidbody2D != null)
        {
            if (m_FollowRight)
            {
                transform.right = new Vector3(m_Rigidbody2D.velocity.x, m_Rigidbody2D.velocity.y);
            }

            if (m_FollowUp)
            {
                transform.up = new Vector3(m_Rigidbody2D.velocity.x, m_Rigidbody2D.velocity.y);
            }

            if (m_FollowForward)
            {
                transform.forward = new Vector3(m_Rigidbody2D.velocity.x, m_Rigidbody2D.velocity.y);
            }
        }
    }

    public void SetFollowR(bool m_FollowR)
    {
        this.m_FollowRight = m_FollowR;
    }

    public bool GetFollowR()
    {
        return m_FollowRight;
    }

    public void SetFollowU(bool m_FollowU)
    {
        this.m_FollowUp = m_FollowU ;
    }

    public bool GetFollowU()
    {
        return m_FollowUp;
    }

    public void SetFollowForward(bool m_FollowForward)
    {
        this.m_FollowForward = m_FollowForward;
    }

    public bool GetFollowForward()
    {
        return m_FollowForward;
    }
}