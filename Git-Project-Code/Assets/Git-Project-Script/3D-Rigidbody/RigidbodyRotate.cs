using UnityEngine;

[AddComponentMenu("Git-Project-Code/Rigidbody/Rigidbody Rotate")]
public class RigidbodyRotate : MonoBehaviour
{
    [SerializeField] [Tooltip("Vector Right - Red Axis")] private bool m_AllowFollowRight = true;

    [SerializeField] [Tooltip("Vector Forward - Blue Axis")] private bool m_AllowFollowForward = false;

    [SerializeField] [Tooltip("Vector Up - Green Axis")] private bool m_AllowFollowUp = false;

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
            if (m_AllowFollowRight)
            {
                transform.right = new Vector3(com_Rigidbody.velocity.x, com_Rigidbody.velocity.y, com_Rigidbody.velocity.z);
            }

            if (m_AllowFollowUp)
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
            if (m_AllowFollowRight)
            {
                transform.right = new Vector3(com_Rigidbody2D.velocity.x, com_Rigidbody2D.velocity.y);
            }

            if (m_AllowFollowUp)
            {
                transform.up = new Vector3(com_Rigidbody2D.velocity.x, com_Rigidbody2D.velocity.y);
            }

            if (m_AllowFollowForward)
            {
                transform.forward = new Vector3(com_Rigidbody2D.velocity.x, com_Rigidbody2D.velocity.y);
            }
        }
    }

    public void SetFollowR(bool mAllowFollowR)
    {
        this.m_AllowFollowRight = mAllowFollowR;
    }

    public bool GetCheckFollowR()
    {
        return m_AllowFollowRight;
    }

    public void SetFollowU(bool mAllowFollowU)
    {
        this.m_AllowFollowUp = mAllowFollowU ;
    }

    public bool GetCheckFollowU()
    {
        return m_AllowFollowUp;
    }

    public void SetFollowForward(bool mAllowFollowForward)
    {
        this.m_AllowFollowForward = mAllowFollowForward;
    }

    public bool GetCheckFollowForward()
    {
        return m_AllowFollowForward;
    }
}