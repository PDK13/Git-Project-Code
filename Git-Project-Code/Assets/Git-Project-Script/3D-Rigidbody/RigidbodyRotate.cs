using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyRotate : MonoBehaviour
{
    [SerializeField] private bool b_Rigidbody_isForward = true;

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
            this.transform.forward = com_Rigidbody.velocity;
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
