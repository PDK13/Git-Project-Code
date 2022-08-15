using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(RigidbodyComponent))]

public class RigidbodySurface : MonoBehaviour
//Move Control Surface (X & Z)
{
    [Header("Keyboard")]

    [SerializeField] private bool b_LockControl = false;

    [SerializeField] private KeyCode k_MoveUp = KeyCode.UpArrow;

    [SerializeField] private KeyCode k_MoveDown = KeyCode.DownArrow;

    [SerializeField] private KeyCode k_MoveLeft = KeyCode.LeftArrow;

    [SerializeField] private KeyCode k_MoveRight = KeyCode.RightArrow;

    [SerializeField] private bool b_MutiButton = true;

    [SerializeField] private KeyCode k_SpeedChance = KeyCode.LeftShift;

    [Header("Move")]

    [SerializeField] private float f_SpeedNormal = 2f;

    [SerializeField] private float f_SpeedChance = 5f;

    private float f_SpeedCur;

    [SerializeField] private bool b_StopRightAway = false;

    [SerializeField] private float f_SpeedStop = 3f;

    private RigidbodyComponent cs_Rigid;

    private int i_ButtonMove_X_Right = 0;

    private int i_ButtonMove_Z_Forward = 0;

    private bool b_SpeedChance = false;

    private void Awake()
    {
        cs_Rigid = GetComponent<RigidbodyComponent>();
    }

    private void Update()
    {
        if (b_LockControl)
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

        if (!b_MutiButton)
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

    private void Set_Control_Move_X_Right(int i_ButtonMove_X_Right)
    {
        this.i_ButtonMove_X_Right = i_ButtonMove_X_Right;
    }

    private void Set_Control_Move_Z_Forward(int i_ButtonMove_Z_Forward)
    {
        this.i_ButtonMove_Z_Forward = i_ButtonMove_Z_Forward;
    }

    private void Set_Control_Move_SpeedChance(bool b_SpeedChance)
    {
        this.b_SpeedChance = b_SpeedChance;
    }

    #endregion

    #region Auto Control 

    private void Set_MoveControl()
    {
        f_SpeedCur = (b_SpeedChance) ? f_SpeedChance : f_SpeedNormal;

        if (i_ButtonMove_X_Right != 0)
        {
            cs_Rigid.Set_MoveX_Velocity(i_ButtonMove_X_Right, f_SpeedCur, f_SpeedCur);
        }
        else
        {
            if (b_StopRightAway)
                cs_Rigid.Set_StopX_Velocity();
            else
                cs_Rigid.Set_StopX_Velocity(f_SpeedStop);
        }

        if (i_ButtonMove_Z_Forward != 0)
        {
            cs_Rigid.Set_MoveZ_Velocity(i_ButtonMove_Z_Forward, f_SpeedCur, f_SpeedCur);
        }
        else
        {
            if (b_StopRightAway)
                cs_Rigid.Set_StopZ_Velocity();
            else
                cs_Rigid.Set_StopZ_Velocity(f_SpeedStop);
        }
    }

    #endregion

    #region Control is Lock

    public void Set_Control_isLock(bool b_LockControl)
    {
        this.b_LockControl = b_LockControl;
    }

    public bool Get_Control_isLock()
    {
        return this.b_LockControl;
    }

    #endregion
}
