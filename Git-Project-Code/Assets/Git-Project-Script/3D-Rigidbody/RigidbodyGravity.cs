using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidbodyGravity : MonoBehaviour
{
    [SerializeField] private float f_Gravity_Scale = 1.0f;

    [SerializeField] private float f_Gravity_Global = 9.81f;

    private Rigidbody com_Rigidbody;

    private void Awake()
    {
        try
        {
            if (GetComponent<Rigidbody2D>() != null) return;
            if (GetComponent<Rigidbody>() == null) gameObject.AddComponent<Rigidbody>();
            com_Rigidbody = GetComponent<Rigidbody>();
            com_Rigidbody.useGravity = false;
            com_Rigidbody.mass = 1;
        }
        catch
        {

        }
    }

    private void FixedUpdate()
    {
        if (com_Rigidbody == null)
        {
            return;
        }

        Vector3 v3_Gravity = Get_Gravity_Global_toVector() * Get_Gravity_Scale();

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

    public float Get_Gravity_Global_toFloat()
    {
        return this.f_Gravity_Global;
    }

    public Vector3 Get_Gravity_Global_toVector()
    {
        return Get_Gravity_Global_toFloat() * Vector3.down;
    }

    #endregion
}
