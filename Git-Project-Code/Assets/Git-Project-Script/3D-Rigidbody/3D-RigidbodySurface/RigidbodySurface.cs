using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(RigidbodyComponent))]

public class RigidbodySurface : MonoBehaviour
//Move Control Surface (X & Z)
{
    [Header("Keyboard")]

    [SerializeField] private bool mAllowLockControl = false;

    [SerializeField] private KeyCode m_KeyMoveU = KeyCode.UpArrow;

    [SerializeField] private KeyCode m_KeyMoveD = KeyCode.DownArrow;

    [SerializeField] private KeyCode m_KeyMoveL = KeyCode.LeftArrow;

    [SerializeField] private KeyCode m_KeyMoveR = KeyCode.RightArrow;

    [SerializeField] private bool mAllowMutiButton = true;

    [SerializeField] private KeyCode m_KeySpeedChance = KeyCode.LeftShift;

    [Header("Move")]

    [SerializeField] private float m_SpeedNormal = 2f;

    [SerializeField] private float m_SpeedChance = 5f;

    private float m_SpeedCur;

    [SerializeField] private bool mAllowStopRAway = false;

    [SerializeField] private float m_SpeedStop = 3f;

    private RigidbodyComponent m_Rigid;

    private int m_ButtonMoveXR = 0;

    private int m_ButtonMoveZForward = 0;

    private bool mAllowSpeedChance = false;

    private void Awake()
    {
        m_Rigid = GetComponent<RigidbodyComponent>();
    }

    private void Update()
    {
        if (mAllowLockControl)
        {
            return;
        }

        SetControlMain_Keyboard();
    }

    private void FixedUpdate()
    {
        SetMoveControl();
    }

    #region Keyboard Control 

    private void SetControlMain_Keyboard()
    {
        SetControlMoveSpeedChance(Input.GetKey(m_KeySpeedChance) ? true : false);

        if (!mAllowMutiButton)
        {
            if ((Input.GetKey(m_KeyMoveL) || Input.GetKey(m_KeyMoveR)) &&
                (Input.GetKey(m_KeyMoveU) || Input.GetKey(m_KeyMoveD)))
            {
                SetControlMoveXR(0);
                SetControlMoveZForward(0);
            }
        }
        else
        {
            if (Input.GetKey(m_KeyMoveL) && Input.GetKey(m_KeyMoveR))
            {
                SetControlMoveXR(0);
            }
            else
            {
                if (Input.GetKey(m_KeyMoveL))
                {
                    SetControlMoveXR(-1);
                }
                else
                if (Input.GetKey(m_KeyMoveR))
                {
                    SetControlMoveXR(1);
                }
                else
                {
                    SetControlMoveXR(0);
                }
            }

            if (Input.GetKey(m_KeyMoveD) && Input.GetKey(m_KeyMoveU))
            {
                SetControlMoveZForward(0);
            }
            else
            {
                if (Input.GetKey(m_KeyMoveD))
                {
                    SetControlMoveZForward(-1);
                }
                else
                if (Input.GetKey(m_KeyMoveU))
                {
                    SetControlMoveZForward(1);
                }
                else
                {
                    SetControlMoveZForward(0);
                }
            }
        }
    }

    private void SetControlMoveXR(int m_ButtonMoveXR)
    {
        this.m_ButtonMoveXR = m_ButtonMoveXR;
    }

    private void SetControlMoveZForward(int m_ButtonMoveZForward)
    {
        this.m_ButtonMoveZForward = m_ButtonMoveZForward;
    }

    private void SetControlMoveSpeedChance(bool mAllowSpeedChance)
    {
        this.mAllowSpeedChance = mAllowSpeedChance;
    }

    #endregion

    #region Auto Control 

    private void SetMoveControl()
    {
        m_SpeedCur = (mAllowSpeedChance) ? m_SpeedChance : m_SpeedNormal;

        if (m_ButtonMoveXR != 0)
        {
            m_Rigid.SetMoveX_Velocity(m_ButtonMoveXR, m_SpeedCur, m_SpeedCur);
        }
        else
        {
            if (mAllowStopRAway)
            {
                m_Rigid.SetStopX_Velocity();
            }
            else
            {
                m_Rigid.SetStopX_Velocity(m_SpeedStop);
            }
        }

        if (m_ButtonMoveZForward != 0)
        {
            m_Rigid.SetMoveZ_Velocity(m_ButtonMoveZForward, m_SpeedCur, m_SpeedCur);
        }
        else
        {
            if (mAllowStopRAway)
            {
                m_Rigid.SetStopZ_Velocity();
            }
            else
            {
                m_Rigid.SetStopZ_Velocity(m_SpeedStop);
            }
        }
    }

    #endregion

    #region Control is Lock

    public void SetControlLock(bool mAllowLockControl)
    {
        this.mAllowLockControl = mAllowLockControl;
    }

    public bool GetCheckControlLock()
    {
        return mAllowLockControl;
    }

    #endregion
}
