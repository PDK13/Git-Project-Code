using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyVelocity : MonoBehaviour
{
    [SerializeField] private bool b_Rigidbody_isForward = true;

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
            Debug.LogError("RigidbodyRotate: Require Componenet Rigidbody or Rigidbody2D.");
        }
    }

    private void FixedUpdate()
    {
        if (com_Rigidbody != null)
        {
            if (b_Rigidbody_isForward)
            {
                this.transform.right = new Vector3(com_Rigidbody.velocity.x, com_Rigidbody.velocity.y, com_Rigidbody.velocity.z);
            }
        }
        else
        if (com_Rigidbody2D != null)
        {
            if (b_Rigidbody_isForward)
            {
                this.transform.right = new Vector3(com_Rigidbody2D.velocity.x, com_Rigidbody2D.velocity.y);
            }
        }
    }

    public void Set_Rigidbody_isForward(bool b_Rigidbody_isForward)
    {
        this.b_Rigidbody_isForward = b_Rigidbody_isForward;
    }

    public bool Get_Rigidbody_isForward()
    {
        return this.b_Rigidbody_isForward;
    }
}
