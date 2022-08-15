using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyGravity : MonoBehaviour
{
    [SerializeField] private float f_Gravity_Scale = 1.0f;

    [SerializeField] private float f_Gravity_Global = -9.81f;

    private Rigidbody com_Rigidbody;

    private void Awake()
    {
        if (GetComponent<Rigidbody>() == null) gameObject.AddComponent<Rigidbody>();
        com_Rigidbody = GetComponent<Rigidbody>();
        com_Rigidbody.useGravity = false;
    }

    private void FixedUpdate()
    {
        Vector3 v3_Gravity = f_Gravity_Global * f_Gravity_Scale * Vector3.up;
        com_Rigidbody.AddForce(v3_Gravity, ForceMode.Acceleration);
    }

    #region Set

    public void Set_Gravity_Scale(float f_Gravity_Scale)
    {
        this.f_Gravity_Scale = f_Gravity_Scale;
    }

    public void Set_Gravity_Global(float f_Gravity_Global)
    {
        this.f_Gravity_Global = f_Gravity_Global;
    }

    #endregion

    #region Get

    public float Get_Gravity_Scale()
    {
        return this.f_Gravity_Scale;
    }

    public float Get_Gravity_Global()
    {
        return this.f_Gravity_Global;
    }

    #endregion
}
