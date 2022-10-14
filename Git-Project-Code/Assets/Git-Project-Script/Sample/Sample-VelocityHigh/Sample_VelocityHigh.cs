using UnityEngine;

public class Sample_VelocityHigh : MonoBehaviour
{
    [SerializeField] private Transform m_Tarket_Ground;

    private Rigidbody2D m_Rigidbody2D;

    private CircleCollider2D m_CircleCollider2D;

    private float m_Distance_Get;

    private Vector2? m_PosDrop;

    private void Start()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        m_CircleCollider2D = GetComponent<CircleCollider2D>();

        Physics2D.gravity = Vector2.down * 9.8f;

        m_Rigidbody2D.gravityScale = 15f; //If gravity too high, the m_omement will pass though the collider of tarket ground

        Time.timeScale = 1;
    }

    private void FixedUpdate()
    {
        m_Rigidbody2D.AddForce(Vector3.right * 50f);

        RaycastHit2D rayRaycast = Physics2D.CircleCast(
            (Vector2)transform.position + m_Rigidbody2D.velocity.normalized * (m_CircleCollider2D.radius * 2 + 0.2f),
            m_CircleCollider2D.radius,
            m_Rigidbody2D.velocity.normalized,
            m_Rigidbody2D.velocity.magnitude);

        if (m_PosDrop != null)
        {
            m_Rigidbody2D.bodyType = RigidbodyType2D.Static;

            transform.position = (Vector3)m_PosDrop;
        }
        else
        if (rayRaycast.collider != null)
        {
            if (rayRaycast.collider != null && m_Distance_Get == 0)
            {
                m_Distance_Get = rayRaycast.distance * 1.0f;
            }
            else
            if (rayRaycast.distance * 1.0f <= m_Distance_Get * Time.fixedDeltaTime && m_Distance_Get != 0)
            {
                if (m_Rigidbody2D.bodyType != RigidbodyType2D.Static)
                {
                    m_PosDrop = rayRaycast.collider.ClosestPoint((Vector2)transform.position + m_Rigidbody2D.velocity.normalized * (m_CircleCollider2D.radius * 2 + 0.2f) + (Vector2)ClassVector.GetVector(m_Rigidbody2D.velocity.x, 0, 0) * Time.fixedDeltaTime);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(transform.position, GetComponent<CircleCollider2D>().radius);

        if (m_Tarket_Ground != null)
        {
            Gizmos.DrawWireCube(m_Tarket_Ground.position, m_Tarket_Ground.GetComponent<BoxCollider2D>().size);

            Gizmos.color = Color.gray;

            Gizmos.DrawLine(m_Tarket_Ground.transform.position, transform.position);
        }

        Gizmos.color = Color.red;

        if (m_PosDrop != null)
        {
            Gizmos.DrawLine(transform.position, (Vector2)m_PosDrop);

            Gizmos.DrawWireSphere((Vector2)m_PosDrop, GetComponent<CircleCollider2D>().radius);
        }
    }
}
