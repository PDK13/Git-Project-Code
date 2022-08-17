using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyRotate : MonoBehaviour
{
    [SerializeField] private bool b_Rigidbody_isForward = true;

    [SerializeField] private bool b_Forward_XZ = true;

    private Rigidbody com_Rigidbody;

    private void Awake()
    {
        if (GetComponent<Rigidbody>() == null) gameObject.AddComponent<Rigidbody>();
        com_Rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (b_Rigidbody_isForward)
        {
            if (b_Forward_XZ)
            {
                this.transform.forward = new Vector3(com_Rigidbody.velocity.x, com_Rigidbody.velocity.y, com_Rigidbody.velocity.z);
            }
            else
            {
                this.transform.right = new Vector3(com_Rigidbody.velocity.x, com_Rigidbody.velocity.y, com_Rigidbody.velocity.z);
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
