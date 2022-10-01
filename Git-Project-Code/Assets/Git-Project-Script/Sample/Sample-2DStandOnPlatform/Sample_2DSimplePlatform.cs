using UnityEngine;

public class Sample_2DSimplePlatform : MonoBehaviour
{
    [SerializeField] private float f_Speed = 0.001f;

    private int i_Move_Dir = 1;

    private readonly float f_Time_Chance = 5f;

    private float f_Time_Chance_Cur = 0f;

    private void Awake()
    {
        f_Time_Chance_Cur = f_Time_Chance;
    }

    private void Update()
    {
        if (f_Time_Chance_Cur > 0)
        {
            f_Time_Chance_Cur -= Time.deltaTime;
        }
        else
        {
            i_Move_Dir *= -1;

            f_Time_Chance_Cur = f_Time_Chance;
        }

        Vector2 v2_Pos = Vector2.MoveTowards(transform.position, transform.position + Vector3.right * i_Move_Dir, f_Speed);

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

        if (collider.transform.parent != cl_Player.Get_Primary_Parent())
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
