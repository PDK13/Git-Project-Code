using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(RigidbodyComponent))]

public class RigidbodySurface : MonoBehaviour
//Move Control Surface (X & Z)
{
    [Header("Keyboard")]

    [SerializeField] private bool m_LockControl = false;

    [SerializeField] private KeyCode k_MoveUp = KeyCode.UpArrow;

    [SerializeField] private KeyCode k_MoveDown = KeyCode.DownArrow;

    [SerializeField] private KeyCode k_MoveLeft = KeyCode.LeftArrow;

    [SerializeField] private KeyCode k_MoveRight = KeyCode.RightArrow;

    [SerializeField] private bool m_MutiButton = true;

    [SerializeField] private KeyCode k_SpeedChance = KeyCode.LeftShift;

    [Header("Move")]

    [SerializeField] private float m_SpeedNormal = 2f;

    [SerializeField] private float m_SpeedChance = 5f;

    private float m_SpeedCur;

    [SerializeField] private bool m_StopRightAway = false;

    [SerializeField] private float m_SpeedStop = 3f;

    private RigidbodyComponent cs_Rigid;

    private int m_ButtonMove_X_Right = 0;

    private int m_ButtonMove_Z_Forward = 0;

    private bool m_SpeedIsChance = false;

    private void Awake()
    {
        cs_Rigid = GetComponent<RigidbodyComponent>();
    }

    private void Update()
    {
        if (m_LockControl)
        {
            return;
        }

        Set_Control_Main_Keyboard();
    }

    private void FixedUpdate()
    {
        Set_MoveControl();
    }

    #region Keyboard Control 

    private void Set_Control_Main_Keyboard()
    {
        Set_Control_Move_SpeedChance(Input.GetKey(k_SpeedChance) ? true : false);

        if (!m_MutiButton)
        {
            if ((Input.GetKey(k_MoveLeft) || Input.GetKey(k_MoveRight)) &&
                (Input.GetKey(k_MoveUp) || Input.GetKey(k_MoveDown)))
            {
                Set_Control_Move_X_Right(0);
                Set_Control_Move_Z_Forward(0);
            }
        }
        else
        {
            if (Input.GetKey(k_MoveLeft) && Input.GetKey(k_MoveRight))
            {
                Set_Control_Move_X_Right(0);
            }
            else
            {
                if (Input.GetKey(k_MoveLeft))
                {
                    Set_Control_Move_X_Right(-1);
                }
                else
                if (Input.GetKey(k_MoveRight))
                {
                    Set_Control_Move_X_Right(1);
                }
                else
                {
                    Set_Control_Move_X_Right(0);
                }
            }

            if (Input.GetKey(k_MoveDown) && Input.GetKey(k_MoveUp))
            {
                Set_Control_Move_Z_Forward(0);
            }
            else
            {
                if (Input.GetKey(k_MoveDown))
                {
                    Set_Control_Move_Z_Forward(-1);
                }
                else
                if (Input.GetKey(k_MoveUp))
                {
                    Set_Control_Move_Z_Forward(1);
                }
                else
                {
                    Set_Control_Move_Z_Forward(0);
                }
            }
        }
    }

    private void Set_Control_Move_X_Right(int m_ButtonMove_X_Right)
    {
        this.m_ButtonMove_X_Right = m_ButtonMove_X_Right;
    }

    private void Set_Control_Move_Z_Forward(int m_ButtonMove_Z_Forward)
    {
        this.m_ButtonMove_Z_Forward = m_ButtonMove_Z_Forward;
    }

    private void Set_Control_Move_SpeedChance(bool m_SpeedIsChance)
    {
        this.m_SpeedIsChance = m_SpeedIsChance;
    }

    #endregion

    #region Auto Control 

    private void Set_MoveControl()
    {
        m_SpeedCur = (m_SpeedIsChance) ? m_SpeedChance : m_SpeedNormal;

        if (m_ButtonMove_X_Right != 0)
        {
            cs_Rigid.Set_MoveX_Velocity(m_ButtonMove_X_Right, m_SpeedCur, m_SpeedCur);
        }
        else
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

        if (m_ButtonMove_Z_Forward != 0)
        {
            cs_Rigid.Set_MoveZ_Velocity(m_ButtonMove_Z_Forward, m_SpeedCur, m_SpeedCur);
        }
        else
        {
            if (m_StopRightAway)
            {
                cs_Rigid.Set_StopZ_Velocity();
            }
            else
            {
                cs_Rigid.Set_StopZ_Velocity(m_SpeedStop);
            }
        }
    }

    #endregion

    #region Control is Lock

    public void Set_ControlIsLock(bool m_LockControl)
    {
        this.m_LockControl = m_LockControl;
    }

    public bool GetControlIsLock()
    {
        return m_LockControl;
    }

    #endregion
}
