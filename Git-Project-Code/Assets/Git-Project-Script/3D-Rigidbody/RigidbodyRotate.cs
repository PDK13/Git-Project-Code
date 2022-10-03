using UnityEngine;

[AddComponentMenu("Git-Project-Code/Rigidbody/Rigidbody Rotate")]
public class RigidbodyRotate : MonoBehaviour
{
    [SerializeField] [Tooltip("Vector R - Red Axis")] private bool mAllowFollowR = true;

    [SerializeField] [Tooltip("Vector Forward - Blue Axis")] private bool mAllowFollowForward = false;

    [SerializeField] [Tooltip("Vector U - Green Axis")] private bool mAllowFollowU = false;

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
            if (mAllowFollowR)
            {
                transform.right = new Vector3(com_Rigidbody.velocity.x, com_Rigidbody.velocity.y, com_Rigidbody.velocity.z);
            }

            if (mAllowFollowU)
            {
                transform.up = new Vector3(com_Rigidbody.velocity.x, com_Rigidbody.velocity.y, com_Rigidbody.velocity.z);
            }

            if (mAllowFollowForward)
            {
                transform.forward = new Vector3(com_Rigidbody.velocity.x, com_Rigidbody.velocity.y, com_Rigidbody.velocity.z);
            }
        }
        else
        if (com_Rigidbody2D != null)
        {
            if (mAllowFollowR)
            {
                transform.right = new Vector3(com_Rigidbody2D.velocity.x, com_Rigidbody2D.velocity.y);
            }

            if (mAllowFollowU)
            {
                transform.up = new Vector3(com_Rigidbody2D.velocity.x, com_Rigidbody2D.velocity.y);
            }

            if (mAllowFollowForward)
            {
                transform.forward = new Vector3(com_Rigidbody2D.velocity.x, com_Rigidbody2D.velocity.y);
            }
        }
    }

    public void SetFollowR(bool mAllowFollowR)
    {
        this.mAllowFollowR = mAllowFollowR;
    }

    public bool GetCheckFollowR()
    {
        return mAllowFollowR;
    }

    public void SetFollowU(bool mAllowFollowU)
    {
        this.mAllowFollowU = mAllowFollowU ;
    }

    public bool GetCheckFollowU()
    {
        return mAllowFollowU;
    }

    public void SetFollowForward(bool mAllowFollowForward)
    {
        this.mAllowFollowForward = mAllowFollowForward;
    }

    public bool GetCheckFollowForward()
    {
        return mAllowFollowForward;
    }
}