using UnityEngine;

public class Sample_2DSimplePlatform : MonoBehaviour
{
    [SerializeField] private float m_Speed = 0.001f;

    private int m_Move_Dir = 1;

    private readonly float m_Time_Chance = 5f;

    private float m_Time_Chance_Cur = 0f;

    private void Awake()
    {
        m_Time_Chance_Cur = m_Time_Chance;
    }

    private void Update()
    {
        if (m_Time_Chance_Cur > 0)
        {
            m_Time_Chance_Cur -= Time.deltaTime;
        }
        else
        {
            m_Move_Dir *= -1;

            m_Time_Chance_Cur = m_Time_Chance;
        }

        Vector2 v2_Pos = Vector2.MoveTowards(transform.position, transform.position + Vector3.right * m_Move_Dir, m_Speed);

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
        Sample_2DSimplePlayer cl_Player = collider.GetComponent<Sample_2DSimplePlayer>();

        if (cl_Player == null)
        {
            return;
        }

        if (collider.transform.parent != cl_Player.GetPrimary_Parent())
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
        Sample_2DSimplePlayer cl_Player = collider.GetComponent<Sample_2DSimplePlayer>();

        if (cl_Player == null)
        {
            return;
        }

        if (collider.transform.parent != transform)
        {
            return;
        }

        cl_Player.Reset();
    }
}
