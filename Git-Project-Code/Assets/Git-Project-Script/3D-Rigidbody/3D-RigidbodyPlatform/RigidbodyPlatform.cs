using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(RigidbodyComponent))]

public class RigidbodyPlatform : MonoBehaviour
//Move Control Platform (X)
{
    private RigidbodyComponent cs_Rigid;
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
        cs_Rigid = GetComponent<RigidbodyComponent>();
    }

    private void Update()
    {
        Set_SpeedChance();
        Set_MoveButton();
    }

    public void Set_MoveButton()
    //Control Move by Keyboard
    {
        if (Input.GetKey(k_MoveLeft) && Input.GetKey(k_MoveRight))
        {
            //Press "Left" & "Right" same time >> Not Move
            Set_Stop();
        }
        else
            if (Input.GetKey(k_MoveLeft))
        {
            //Left
            Set_Move(-1);
        }
        else
            if (Input.GetKey(k_MoveRight))
        {
            //Right
            Set_Move(1);
        }
        else
        {
            //Not Press
            Set_Stop();
        }
    }

    private void Set_Move(int m_ButtonMove)
    //Control Move
    {
        cs_Rigid.Set_MoveX_Velocity(m_ButtonMove, m_SpeedCur, m_SpeedCur);
    }

    private void Set_Stop()
    //Control Stop
    {
        if (m_StopRightAway)
        {
            cs_Rigid.Set_StopX_Velocity();
        }
        else
        {
            cs_Rigid.Set_StopX_Velocity(m_SpeedStop);
        }
    }

    public void Set_SpeedChance()
    //Control Speed Chance
    {
        m_SpeedCur = (Input.GetKey(k_SpeedChance)) ? m_SpeedChance : m_SpeedNormal;
    }
}