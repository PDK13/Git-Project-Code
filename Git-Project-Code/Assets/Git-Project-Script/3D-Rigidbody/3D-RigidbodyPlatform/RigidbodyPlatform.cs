using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(RigidbodyComponent))]

public class RigidbodyPlatform : MonoBehaviour
//Move Control Platform (X)
{
    private RigidbodyComponent cm_Rigid;
    //Use "Move" of this Script

    [Header("Keyboard")]
    public KeyCode k_MoveLeft = KeyCode.LeftArrow;
    //Control Move Left

    public KeyCode k_MoveRight = KeyCode.RightArrow;
    //Control Move Right

    public KeyCode k_SpeedChance = KeyCode.LeftShift;
    //Control Speed Chance

    [Header("Move")]
    public float m_SpeedNormal = 2f;
    //Normal Speed Move

    public float m_SpeedChance = 5f;
    //Chance Speed Move
    private float m_SpeedCur;
    //Current Speed Move

    public bool m_StopRightAway = false;
    //Control Stop without Speed Stop Velocity

    public float m_SpeedStop = 3f;
    //Speed Stop

    private void Awake()
    {
        cm_Rigid = GetComponent<RigidbodyComponent>();
    }

    private void Update()
    {
        SetSpeedChance();
        SetMoveButton();
    }

    public void SetMoveButton()
    //Control Move by Keyboard
    {
        if (Input.GetKey(k_MoveLeft) && Input.GetKey(k_MoveRight))
        {
            //Press "Left" & "Right" same time >> Not Move
            SetStop();
        }
        else
            if (Input.GetKey(k_MoveLeft))
        {
            //Left
            SetMove(-1);
        }
        else
            if (Input.GetKey(k_MoveRight))
        {
            //Right
            SetMove(1);
        }
        else
        {
            //Not Press
            SetStop();
        }
    }

    private void SetMove(int m_ButtonMove)
    //Control Move
    {
        cm_Rigid.SetMoveX_Velocity(m_ButtonMove, m_SpeedCur, m_SpeedCur);
    }

    private void SetStop()
    //Control Stop
    {
        if (m_StopRightAway)
        {
            cm_Rigid.SetStopX_Velocity();
        }
        else
        {
            cm_Rigid.SetStopX_Velocity(m_SpeedStop);
        }
    }

    public void SetSpeedChance()
    //Control Speed Chance
    {
        m_SpeedCur = (Input.GetKey(k_SpeedChance)) ? m_SpeedChance : m_SpeedNormal;
    }
}