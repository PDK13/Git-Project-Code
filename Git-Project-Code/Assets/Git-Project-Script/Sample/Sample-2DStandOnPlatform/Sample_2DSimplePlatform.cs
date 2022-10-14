using UnityEngine;

public class Sample_2DSimplePlatform : MonoBehaviour
{
    [SerializeField] private float m_Speed = 0.001f;

    private int m_MoveDir = 1;

    private readonly float m_TimeChance = 5f;

    private float m_TimeChanceCurrent = 0f;

    private void Awake()
    {
        m_TimeChanceCurrent = m_TimeChance;
    }

    private void Update()
    {
        if (m_TimeChanceCurrent > 0)
        {
            m_TimeChanceCurrent -= Time.deltaTime;
        }
        else
        {
            m_MoveDir *= -1;

            m_TimeChanceCurrent = m_TimeChance;
        }

        Vector2 v2_Pos = Vector2.MoveTowards(transform.position, transform.position + Vector3.right * m_MoveDir, m_Speed);

        transform.position = v2_Pos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            DoPlayerEnter(collision);
        }
    }

    private void DoPlayerEnter(Collider2D collider)
    {
        Sample_2DSimplePlayer m_Player = collider.GetComponent<Sample_2DSimplePlayer>();

        if (m_Player == null)
        {
            return;
        }

        if (collider.transform.parent != m_Player.GetPrimary_Parent())
        {
            return;
        }

        collider.transform.SetParent(transform, true);

        collider.GetComponent<Rigidbody2D>().interpolation = RigidbodyInterpolation2D.None;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            DoPlayerExit(collision);
        }
    }

    private void DoPlayerExit(Collider2D collider)
    {
        Sample_2DSimplePlayer m_Player = collider.GetComponent<Sample_2DSimplePlayer>();

        if (m_Player == null)
        {
            return;
        }

        if (collider.transform.parent != transform)
        {
            return;
        }

        m_Player.Reset();
    }
}
