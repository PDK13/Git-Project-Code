using UnityEngine;

public class Sample_2DSimplePlayer : MonoBehaviour
{
    [SerializeField] private float m_Speed = 10f;
    [SerializeField] private float m_Jump = 1500f;

    [SerializeField] private float m_Mass = 4f;
    //[SerializeField] private float m_LinearDrag = 2f;
    [SerializeField] private float m_Angular = 2f;
    [SerializeField] private float m_Gravity = 3f;

    private Transform t_Parent;

    private Rigidbody2D m_Rigidbody2D;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        t_Parent = transform.parent;

        m_Rigidbody2D.mass = m_Mass;
        m_Rigidbody2D.gravityScale = m_Gravity;
        m_Rigidbody2D.angularVelocity = m_Angular;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            m_Rigidbody2D.AddForce(Vector2.right * m_Speed);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            m_Rigidbody2D.AddForce(Vector2.left * m_Speed);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            m_Rigidbody2D.AddForce(Vector2.up * m_Jump);
        }
    }

    public void Reset()
    {
        transform.SetParent(GetPrimary_Parent());
        transform.localScale = GetPrimary_Parent().localScale;
        GetComponent<Rigidbody2D>().interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    public Transform GetPrimary_Parent()
    {
        return t_Parent;
    }
}
