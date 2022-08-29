using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyVelocity : MonoBehaviour
{
    [SerializeField] [Tooltip("Vector Right - Red Axis")] private bool b_Rigidbody_X = true;

    [SerializeField] [Tooltip("Vector Forward - Blue Axis")] private bool b_Rigidbody_Z = false;

    [SerializeField] [Tooltip("Vector Up - Green Axis")] private bool b_Rigidbody_Y = false;

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
            if (b_Rigidbody_X)
            {
                this.transform.right = new Vector3(com_Rigidbody.velocity.x, com_Rigidbody.velocity.y, com_Rigidbody.velocity.z);
            }

            if (b_Rigidbody_Y)
            {
                this.transform.up = new Vector3(com_Rigidbody.velocity.x, com_Rigidbody.velocity.y, com_Rigidbody.velocity.z);
            }

            if (b_Rigidbody_Z)
            {
                this.transform.forward = new Vector3(com_Rigidbody.velocity.x, com_Rigidbody.velocity.y, com_Rigidbody.velocity.z);
            }
        }
        else
        if (com_Rigidbody2D != null)
        {
            if (b_Rigidbody_X)
            {
                this.transform.right = new Vector3(com_Rigidbody2D.velocity.x, com_Rigidbody2D.velocity.y);
            }

            if (b_Rigidbody_Y)
            {
                this.transform.up = new Vector3(com_Rigidbody2D.velocity.x, com_Rigidbody2D.velocity.y);
            }

            if (b_Rigidbody_Z)
            {
                this.transform.forward = new Vector3(com_Rigidbody2D.velocity.x, com_Rigidbody2D.velocity.y);
            }
        }
    }

    public void Set_Rigidbody_X(bool b_Rigidbody_X)
    {
        this.b_Rigidbody_X = b_Rigidbody_X;
    }

    public bool Get_Rigidbody_X()
    {
        return this.b_Rigidbody_X;
    }

    public void Set_Rigidbody_Y(bool b_Rigidbody_Y)
    {
        this.b_Rigidbody_Y = b_Rigidbody_Y;
    }

    public bool Get_Rigidbody_Y()
    {
        return this.b_Rigidbody_Y;
    }

    public void Set_Rigidbody_Z(bool b_Rigidbody_Z)
    {
        this.b_Rigidbody_Z = b_Rigidbody_Z;
    }

    public bool Get_Rigidbody_Z()
    {
        return this.b_Rigidbody_Z;
    }
}
