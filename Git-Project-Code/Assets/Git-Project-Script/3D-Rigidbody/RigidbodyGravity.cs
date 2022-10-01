using UnityEngine;

[AddComponentMenu("Git-Project-Code/Rigidbody/Rigidbody Gravity")]
public class RigidbodyGravity : MonoBehaviour
{
    [SerializeField] private float m_Gravity_Scale = 1.0f;

    [SerializeField] private float m_Gravity_Global = 9.81f;

    private Rigidbody com_Rigidbody;

    private void Awake()
    {
        if (GetComponent<Rigidbody2D>() == null)
        {
            if (GetComponent<Rigidbody>() == null)
            {
                gameObject.AddComponent<Rigidbody>();
            }
        }
        else
        {
            Debug.LogErrorFormat("{0}: Detect Rigidbody2D Component, please remove it!", name);
        }

        com_Rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        com_Rigidbody.useGravity = false;
    }

    private void FixedUpdate()
    {
        if (com_Rigidbody == null)
        {
            return;
        }

        Vector3 v3_Gravity = GetGravity_Global_toVector() * GetGravity_Scale();

        com_Rigidbody.AddForce(v3_Gravity, ForceMode.Acceleration);
    }

    #region Set

    public void Set_Gravity_Scale(float m_Gravity_Scale)
    {
        this.m_Gravity_Scale = m_Gravity_Scale;
    }

    public void Set_Gravity_Global(float m_Gravity_Global)
    {
        this.m_Gravity_Global = m_Gravity_Global;
    }

    public void Set_Rigidbody_Drag(float m_Gravity_Drag)
    {
        com_Rigidbody.drag = m_Gravity_Drag;
    }

    #endregion

    #region Get

    public float GetGravity_Scale()
    {
        return m_Gravity_Scale;
    }

    public float GetGravity_Global_toFloat()
    {
        return m_Gravity_Global;
    }

    public Vector3 GetGravity_Global_toVector()
    {
        return GetGravity_Global_toFloat() * Vector3.down;
    }

    public float GetRigidbody_Drag()
    {
        return com_Rigidbody.drag;
    }

    #endregion
}
