using UnityEngine;

public class Sample_VelocityHigh : MonoBehaviour
{
    [SerializeField] private Transform com_Tarket_Ground;

    private Rigidbody2D com_Rigidbody2D;

    private CircleCollider2D com_CircleCollider2D;

    private float f_Distance_Get;

    private Vector2? v2_Pos_Drop;

    private void Start()
    {
        com_Rigidbody2D = GetComponent<Rigidbody2D>();

        com_CircleCollider2D = GetComponent<CircleCollider2D>();

        Physics2D.gravity = Vector2.down * 9.8f;

        com_Rigidbody2D.gravityScale = 15f; //If gravity too high, the momement will pass though the collider of tarket ground

        Time.timeScale = 1;
    }

    private void FixedUpdate()
    {
        com_Rigidbody2D.AddForce(Vector3.right * 50f);

        RaycastHit2D ray_Raycast = Physics2D.CircleCast(
            (Vector2)transform.position + com_Rigidbody2D.velocity.normalized * (com_CircleCollider2D.radius * 2 + 0.2f),
            com_CircleCollider2D.radius,
            com_Rigidbody2D.velocity.normalized,
            com_Rigidbody2D.velocity.magnitude);

        if (v2_Pos_Drop != null)
        {
            com_Rigidbody2D.bodyType = RigidbodyType2D.Static;

            transform.position = (Vector3)v2_Pos_Drop;
        }
        else
        if (ray_Raycast.collider != null)
        {
            if (ray_Raycast.collider != null && f_Distance_Get == 0)
            {
                f_Distance_Get = ray_Raycast.distance * 1.0f;
            }
            else
            if (ray_Raycast.distance * 1.0f <= f_Distance_Get * Time.fixedDeltaTime && f_Distance_Get != 0)
            {
                if (com_Rigidbody2D.bodyType != RigidbodyType2D.Static)
                {
                    v2_Pos_Drop = ray_Raycast.collider.ClosestPoint((Vector2)transform.position + com_Rigidbody2D.velocity.normalized * (com_CircleCollider2D.radius * 2 + 0.2f) + (Vector2)Class_Vector.Get_Vector(com_Rigidbody2D.velocity.x, 0, 0) * Time.fixedDeltaTime);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(transform.position, GetComponent<CircleCollider2D>().radius);

        if (com_Tarket_Ground != null)
        {
            Gizmos.DrawWireCube(com_Tarket_Ground.position, com_Tarket_Ground.GetComponent<BoxCollider2D>().size);

            Gizmos.color = Color.gray;

            Gizmos.DrawLine(com_Tarket_Ground.transform.position, transform.position);
        }

        Gizmos.color = Color.red;

        if (v2_Pos_Drop != null)
        {
            Gizmos.DrawLine(transform.position, (Vector2)v2_Pos_Drop);

            Gizmos.DrawWireSphere((Vector2)v2_Pos_Drop, GetComponent<CircleCollider2D>().radius);
        }
    }
}
