using UnityEngine;

[AddComponentMenu("Git-Project-Code/Rigidbody/Rigidbody Rotate")]
public class RigidbodyRotate : MonoBehaviour
{
    public GitVector.Axis m_Axis = GitVector.Axis.Right;

    public enum RigidbodyType { Rigidbody2D, Rigidbody3D, }

    [SerializeField] private RigidbodyType m_RigidbodyType;

    private Rigidbody m_Rigidbody;

    private Rigidbody2D m_Rigidbody2D;

    private void Awake()
    {
        switch (m_RigidbodyType)
        {
            case RigidbodyType.Rigidbody2D:
                m_Rigidbody2D = GetComponent<Rigidbody2D>();
                break;
            case RigidbodyType.Rigidbody3D:
                m_Rigidbody = GetComponent<Rigidbody>();
                break;
        }

        if (m_Rigidbody2D  == null && m_Rigidbody == null)
        {
            Debug.LogErrorFormat("{0}: Require Componenet Rigidbody or Rigidbody2D.", name);
        }
    }

    private void FixedUpdate()
    {
        switch (m_RigidbodyType)
        {
            case RigidbodyType.Rigidbody2D:
                { 
                    switch (m_Axis)
                    {
                        case GitVector.Axis.Right:
                            transform.right = new Vector3(m_Rigidbody.velocity.x, m_Rigidbody.velocity.y, m_Rigidbody.velocity.z);
                            break;
                        case GitVector.Axis.Up:
                            transform.up = new Vector3(m_Rigidbody.velocity.x, m_Rigidbody.velocity.y, m_Rigidbody.velocity.z);
                            break;
                        case GitVector.Axis.Forward:
                            transform.forward = new Vector3(m_Rigidbody.velocity.x, m_Rigidbody.velocity.y, m_Rigidbody.velocity.z);
                            break;
                    }
                }
                break;
            case RigidbodyType.Rigidbody3D:
                {
                    switch (m_Axis)
                    {
                        case GitVector.Axis.Right:
                            transform.right = new Vector3(m_Rigidbody2D.velocity.x, m_Rigidbody2D.velocity.y);
                            break;
                        case GitVector.Axis.Up:
                            transform.up = new Vector3(m_Rigidbody2D.velocity.x, m_Rigidbody2D.velocity.y);
                            break;
                        case GitVector.Axis.Forward:
                            transform.forward = new Vector3(m_Rigidbody2D.velocity.x, m_Rigidbody2D.velocity.y);
                            break;
                    }
                }
                break;
        }
    }

    public void SetFollow(GitVector.Axis m_Axis)
    {
        this.m_Axis = m_Axis;
    }

    public GitVector.Axis GetFollow()
    {
        return m_Axis;
    }
}