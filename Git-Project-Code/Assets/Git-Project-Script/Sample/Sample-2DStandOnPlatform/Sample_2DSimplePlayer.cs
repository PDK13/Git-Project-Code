using UnityEngine;

public class Sample_2DSimplePlayer : MonoBehaviour
{
    [SerializeField] private float f_Speed = 10f;
    [SerializeField] private float f_Jump = 1500f;

    [SerializeField] private float f_Mass = 4f;
    [SerializeField] private float f_LinearDrag = 2f;
    [SerializeField] private float f_Angular = 2f;
    [SerializeField] private float f_Gravity = 3f;

    private Transform t_Parent;

    private Rigidbody2D r_Rigidbody2D;

    private void Awake()
    {
        r_Rigidbody2D = GetComponent<Rigidbody2D>();

        t_Parent = transform.parent;

        r_Rigidbody2D.gravityScale = f_Gravity;
        r_Rigidbody2D.angularVelocity = f_Angular;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            r_Rigidbody2D.AddForce(Vector2.right * f_Speed);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            r_Rigidbody2D.AddForce(Vector2.left * f_Speed);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            r_Rigidbody2D.AddForce(Vector2.up * f_Jump);
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
