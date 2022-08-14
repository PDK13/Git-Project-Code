using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Renderer2DThown : MonoBehaviour
{
    public float power = 5f;

    public int i_Length = 500;

    Rigidbody2D rb;

    LineRenderer lr;

    Vector2 DragStartPos;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lr = GetComponent<LineRenderer>();

        //Physics2D.gravity = new Vector2(0, -9.8f);

        //Debug.Log(Physics2D.velocityIterations);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DragStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 DragEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 _velocity = (DragEndPos - DragStartPos) * power;
            Vector2[] trajectory = Plot(rb, (Vector2)transform.position, _velocity, i_Length);

            lr.positionCount = trajectory.Length;
            Vector3[] position = new Vector3[trajectory.Length];

            for (int i = 0; i < position.Length; i++)
            {
                position[i] = trajectory[i];
            }
            lr.SetPositions(position);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Vector2 DragEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 _velocity = (DragEndPos - DragStartPos) * power;

            rb.velocity = _velocity;
        }

    }

    public Vector2[] Plot(Rigidbody2D rigidbody, Vector2 pos, Vector2 velocity, int step)
    {
        Vector2[] result = new Vector2[step];

        float timeStep = Time.fixedDeltaTime / Physics2D.velocityIterations;
        Vector2 gravityAccel = Physics2D.gravity * rigidbody.gravityScale * timeStep * timeStep;

        float drag = 1f - timeStep * rigidbody.drag;
        Vector2 moveStep = velocity * timeStep;

        for (int i=0; i < step; i++)
        {
            moveStep += gravityAccel;
            moveStep *= drag;
            pos += moveStep;
            result[i] = pos;
        }

        return result;
    }
}
