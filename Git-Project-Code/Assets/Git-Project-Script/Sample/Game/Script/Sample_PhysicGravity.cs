using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample_PhysicGravity : MonoBehaviour
{
    enum GravityDirection { Down, Left, Up, Right };
    GravityDirection m_GravityDirection;

    void Start()
    {
        m_GravityDirection = GravityDirection.Down;
    }

    void FixedUpdate()
    {
        switch (m_GravityDirection)
        {
            case GravityDirection.Down:
                //Change the gravity to be in a downward direction (default)
                Physics2D.gravity = new Vector2(0, -9.8f);
                //Press the space key to switch to the left direction
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    m_GravityDirection = GravityDirection.Left;
                    Debug.Log("Left");
                }
                break;

            case GravityDirection.Left:
                //Change the gravity to go to the left
                Physics2D.gravity = new Vector2(-9.8f, 0);
                //Press the space key to change the direction of gravity
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    m_GravityDirection = GravityDirection.Up;
                    Debug.Log("Up");
                }
                break;

            case GravityDirection.Up:
                //Change the gravity to be in a upward direction
                Physics2D.gravity = new Vector2(0, 9.8f);
                //Press the space key to change the direction
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    m_GravityDirection = GravityDirection.Right;
                    Debug.Log("Right");
                }
                break;

            case GravityDirection.Right:
                //Change the gravity to go in the right direction
                Physics2D.gravity = new Vector2(9.8f, 0);
                //Press the space key to change the direction
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    m_GravityDirection = GravityDirection.Down;
                    Debug.Log("Down");
                }

                break;
        }
    }
}
