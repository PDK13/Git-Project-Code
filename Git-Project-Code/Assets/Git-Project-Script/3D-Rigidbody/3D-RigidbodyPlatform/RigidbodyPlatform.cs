using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(RigidbodyComponent))]

public class RigidbodyPlatform : MonoBehaviour
//Move Control Platform (X)
{
    private RigidbodyComponent m_Rigid;
    //Use "Move" of this Script

    [Header("Keyboard")]
    public KeyCode m_KeyMoveL = KeyCode.LeftArrow;
    //Control m_ove L

    public KeyCode m_KeyMoveR = KeyCode.RightArrow;
    //Control m_ove R

    public KeyCode m_KeySpeedChance = KeyCode.LeftShift;
    //Control Speed Chance

    [Header("Move")]
    public float m_SpeedNormal = 2f;
    //Normal Speed m_ove

    public float m_SpeedChance = 5f;
    //Chance Speed m_ove
    private float m_SpeedCur;
    //Current Speed m_ove

    public bool m_AllowStopRAway = false;
    //Control Stop without Speed Stop Velocity

    public float m_SpeedStop = 3f;
    //Speed Stop

    private void Awake()
    {
        m_Rigid = GetComponent<RigidbodyComponent>();
    }

    private void Update()
    {
        SetSpeedChance();
        SetMoveButton();
    }

    public void SetMoveButton()
    //Control m_ove by Keyboard
    {
        if (Input.GetKey(m_KeyMoveL) && Input.GetKey(m_KeyMoveR))
        {
            //Press "L" & "R" same time >> Not m_ove
            SetStop();
        }
        else
            if (Input.GetKey(m_KeyMoveL))
        {
            //L
            SetMove(-1);
        }
        else
            if (Input.GetKey(m_KeyMoveR))
        {
            //R
            SetMove(1);
        }
        else
        {
            //Not Press
            SetStop();
        }
    }

    private void SetMove(int m_ButtonMove)
    //Control m_ove
    {
        m_Rigid.SetMoveX_Velocity(m_ButtonMove, m_SpeedCur, m_SpeedCur);
    }

    private void SetStop()
    //Control Stop
    {
        if (m_AllowStopRAway)
        {
            m_Rigid.SetStopX_Velocity();
        }
        else
        {
            m_Rigid.SetStopX_Velocity(m_SpeedStop);
        }
    }

    public void SetSpeedChance()
    //Control Speed Chance
    {
        m_SpeedCur = (Input.GetKey(m_KeySpeedChance)) ? m_SpeedChance : m_SpeedNormal;
    }
}