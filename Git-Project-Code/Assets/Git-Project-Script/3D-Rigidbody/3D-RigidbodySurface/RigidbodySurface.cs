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

    private RigidbodyComponent cm_Rigid;

    private int m_ButtonMove_X_Right = 0;

    private int m_ButtonMove_Z_Forward = 0;

    private bool m_SpeedIsChance = false;

    private void Awake()
    {
        cm_Rigid = GetComponent<RigidbodyComponent>();
    }

    private void Update()
    {
        if (m_LockControl)
        {
            return;
        }

        SetControl_Main_Keyboard();
    }

    private void FixedUpdate()
    {
        SetMoveControl();
    }

    #region Keyboard Control 

    private void SetControl_Main_Keyboard()
    {
        SetControl_Move_SpeedChance(Input.GetKey(k_SpeedChance) ? true : false);

        if (!m_MutiButton)
        {
            if ((Input.GetKey(k_MoveLeft) || Input.GetKey(k_MoveRight)) &&
                (Input.GetKey(k_MoveUp) || Input.GetKey(k_MoveDown)))
            {
                SetControl_Move_X_Right(0);
                SetControl_Move_Z_Forward(0);
            }
        }
        else
        {
            if (Input.GetKey(k_MoveLeft) && Input.GetKey(k_MoveRight))
            {
                SetControl_Move_X_Right(0);
            }
            else
            {
                if (Input.GetKey(k_MoveLeft))
                {
                    SetControl_Move_X_Right(-1);
                }
                else
                if (Input.GetKey(k_MoveRight))
                {
                    SetControl_Move_X_Right(1);
                }
                else
                {
                    SetControl_Move_X_Right(0);
                }
            }

            if (Input.GetKey(k_MoveDown) && Input.GetKey(k_MoveUp))
            {
                SetControl_Move_Z_Forward(0);
            }
            else
            {
                if (Input.GetKey(k_MoveDown))
                {
                    SetControl_Move_Z_Forward(-1);
                }
                else
                if (Input.GetKey(k_MoveUp))
                {
                    SetControl_Move_Z_Forward(1);
                }
                else
                {
                    SetControl_Move_Z_Forward(0);
                }
            }
        }
    }

    private void SetControl_Move_X_Right(int m_ButtonMove_X_Right)
    {
        this.m_ButtonMove_X_Right = m_ButtonMove_X_Right;
    }

    private void SetControl_Move_Z_Forward(int m_ButtonMove_Z_Forward)
    {
        this.m_ButtonMove_Z_Forward = m_ButtonMove_Z_Forward;
    }

    private void SetControl_Move_SpeedChance(bool m_SpeedIsChance)
    {
        this.m_SpeedIsChance = m_SpeedIsChance;
    }

    #endregion

    #region Auto Control 

    private void SetMoveControl()
    {
        m_SpeedCur = (m_SpeedIsChance) ? m_SpeedChance : m_SpeedNormal;

        if (m_ButtonMove_X_Right != 0)
        {
            cm_Rigid.SetMoveX_Velocity(m_ButtonMove_X_Right, m_SpeedCur, m_SpeedCur);
        }
        else
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

        if (m_ButtonMove_Z_Forward != 0)
        {
            cm_Rigid.SetMoveZ_Velocity(m_ButtonMove_Z_Forward, m_SpeedCur, m_SpeedCur);
        }
        else
        {
            if (m_StopRightAway)
            {
                cm_Rigid.SetStopZ_Velocity();
            }
            else
            {
                cm_Rigid.SetStopZ_Velocity(m_SpeedStop);
            }
        }
    }

    #endregion

    #region Control is Lock

    public void SetControlIsLock(bool m_LockControl)
    {
        this.m_LockControl = m_LockControl;
    }

    public bool GetControlIsLock()
    {
        return m_LockControl;
    }

    #endregion
}
