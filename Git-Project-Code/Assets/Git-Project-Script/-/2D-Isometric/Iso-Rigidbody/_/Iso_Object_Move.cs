using UnityEngine;

/// <summary>
/// Move on World Manager
/// </summary>
[RequireComponent(typeof(IsoBlock))]
public class Iso_Object_Move : MonoBehaviour
{
    [Header("Iso World-Manager")]
    [SerializeField]
    private IsoWorld cl_WorldManager;

    [SerializeField]
    private float f_Speed = 0.01f;

    #region Private

    private IsoBlock cl_Block;

    private GameObject g_World_StandOn;

    private bool m_Moved_X = true;

    private float f_Moved_X = 0f;

    private bool m_Moved_Y = true;

    private float f_Moved_Y = 0f;

    private bool m_Moved_High = true;

    private float f_Moved_High = 0f;

    private bool m_GroundToStair = false;

    #endregion

    private void Start()
    {
        cl_Block = GetComponent<IsoBlock>();
    }

    private void Update()
    {
        if (!cl_WorldManager.GetWorldIsGenerated())
        {
            return;
        }

        Set_Move_Auto();
    }

    #region Move Control

    /// <summary>
    /// Set Move
    /// </summary>
    /// <param name="v3_Move_Dir"></param>
    public void Set_Move(Vector3Int v3_Move_Dir, Vector3Int v3_Dir_High, bool m_GroundToStair)
    {
        if (!cl_WorldManager.GetWorldIsGenerated())
        {
            return;
        }

        if (g_World_StandOn == null)
        {
            return;
        }

        if (!m_Moved_X || !m_Moved_Y || !m_Moved_High)
        {
            return;
        }

        GameObject cl_World_Check = cl_WorldManager.GetCurrent_GameObject(g_World_StandOn.GetComponent<IsoBlock>().GetPosOnMatrix_Current() + v3_Move_Dir + v3_Dir_High);

        if (cl_World_Check == null)
        {
            return;
        }

        g_World_StandOn = cl_World_Check;

        this.m_GroundToStair = m_GroundToStair;

        m_Moved_X = false;
        m_Moved_Y = false;
        m_Moved_High = false;

        f_Moved_X = 1f;
        f_Moved_Y = 1f;
        f_Moved_High = 1f;
    }

    #endregion

    #region Move Check

    /// <summary>
    /// Get World (Block, Move and Join) from Stand-On, with add two Dir of Move and High to Get
    /// </summary>
    /// <param name="v3_Move_Dir">Dir Move by UP, DOWN, LEFT or RIGHT</param>
    /// <param name="v3_Dir_High">Dir High by TOP or BOT</param>
    public GameObject GetWorld_Current(Vector3Int v3_Move_Dir, Vector3Int v3_Dir_High)
    {
        return cl_WorldManager.GetCurrent_GameObject(g_World_StandOn.GetComponent<IsoBlock>().GetPosOnMatrix_Current() + v3_Move_Dir + v3_Dir_High);
    }

    /// <summary>
    /// Get World Check Emty (Block, Move and Join) from Stand-On, with add two Dir of Move and High to Get
    /// </summary>
    /// <param name="v3_Move_Dir"></param>
    /// <param name="v3_Dir_High"></param>
    /// <returns></returns>
    public bool GetWorld_Emty(Vector3Int v3_Move_Dir, Vector3Int v3_Dir_High)
    {
        return GetWorld_Current(v3_Move_Dir, v3_Dir_High) == null;
    }

    /// <summary>
    /// Get World (Block, Move and Join) from Stand-On
    /// </summary>
    /// <returns></returns>
    public GameObject GetWorld_StandOn()
    {
        return g_World_StandOn;
    }

    #endregion

    #region Move Auto

    /// <summary>
    /// Move Auto
    /// </summary>
    private void Set_Move_Auto()
    {
        if (g_World_StandOn == null)
        {
            return;
        }

        if (m_GroundToStair)
        {
            Set_Move_Auto_X();
            Set_Move_Auto_Y();

            if (f_Moved_X < 0.5f && f_Moved_Y < 0.5f)
            {
                Set_Move_Auto_High();
            }
        }
        else
        {
            Set_Move_Auto_X();
            Set_Move_Auto_Y();
            Set_Move_Auto_High();
        }
    }

    /// <summary>
    /// Move Auto X
    /// </summary>
    private void Set_Move_Auto_X()
    {
        GameObject cl_World_MoveTo = null;

        float f_Speed = 0;

        if (g_World_StandOn == null)
        {
            if (g_World_StandOn == null)
            {
                return;
            }
            else
            {
                cl_World_MoveTo = g_World_StandOn;

                f_Speed = this.f_Speed;
            }
        }
        else
        {
            cl_World_MoveTo = g_World_StandOn;

            f_Speed = this.f_Speed + g_World_StandOn.GetComponent<IsoBlockMove>().GetMove_Active_Current().GetSpeed();
        }

        //Move

        if (cl_Block.GetPos_Current().x < cl_World_MoveTo.GetComponent<IsoBlock>().GetPos_Current().x)
        {
            if (cl_Block.GetPos_Current().x + f_Speed > cl_World_MoveTo.GetComponent<IsoBlock>().GetPos_Current().x)
            {
                cl_Block.Set_Pos_X(cl_World_MoveTo.GetComponent<IsoBlock>().GetPos_Current().x);

                m_Moved_X = true;
            }
            else
            {
                cl_Block.Set_Pos_Add(f_Speed, 0, 0);
            }
        }
        else
        if (cl_Block.GetPos_Current().x > cl_World_MoveTo.GetComponent<IsoBlock>().GetPos_Current().x)
        {
            if (cl_Block.GetPos_Current().x - f_Speed < cl_World_MoveTo.GetComponent<IsoBlock>().GetPos_Current().x)
            {
                cl_Block.Set_Pos_X(cl_World_MoveTo.GetComponent<IsoBlock>().GetPos_Current().x);

                m_Moved_X = true;
            }
            else
            {
                cl_Block.Set_Pos_Add(-f_Speed, 0, 0);
            }
        }
        else
        {
            m_Moved_X = true;
        }

        if (f_Moved_X > 0)
        {
            f_Moved_X -= f_Speed;
        }
    }

    /// <summary>
    /// Move Auto Y
    /// </summary>
    private void Set_Move_Auto_Y()
    {
        GameObject cl_World_MoveTo = null;

        float f_Speed = 0;

        if (g_World_StandOn == null)
        {
            if (g_World_StandOn == null)
            {
                return;
            }
            else
            {
                cl_World_MoveTo = g_World_StandOn;

                f_Speed = this.f_Speed;
            }
        }
        else
        {
            cl_World_MoveTo = g_World_StandOn;

            f_Speed = this.f_Speed + g_World_StandOn.GetComponent<IsoBlockMove>().GetMove_Active_Current().GetSpeed();
        }

        //Move

        if (cl_Block.GetPos_Current().y < cl_World_MoveTo.GetComponent<IsoBlock>().GetPos_Current().y)
        {
            if (cl_Block.GetPos_Current().y + f_Speed > cl_World_MoveTo.GetComponent<IsoBlock>().GetPos_Current().y)
            {
                cl_Block.Set_Pos_Y(cl_World_MoveTo.GetComponent<IsoBlock>().GetPos_Current().y);

                m_Moved_Y = true;
            }
            else
            {
                cl_Block.Set_Pos_Add(0, f_Speed, 0);
            }
        }
        else
        if (cl_Block.GetPos_Current().y > cl_World_MoveTo.GetComponent<IsoBlock>().GetPos_Current().y)
        {
            if (cl_Block.GetPos_Current().y - f_Speed < cl_World_MoveTo.GetComponent<IsoBlock>().GetPos_Current().y)
            {
                cl_Block.Set_Pos_Y(cl_World_MoveTo.GetComponent<IsoBlock>().GetPos_Current().y);

                m_Moved_Y = true;
            }
            else
            {
                cl_Block.Set_Pos_Add(0, -f_Speed, 0);
            }
        }
        else
        {
            m_Moved_Y = true;
        }

        if (f_Moved_Y > 0)
        {
            f_Moved_Y -= f_Speed;
        }
    }

    /// <summary>
    /// Move Auto High
    /// </summary>
    private void Set_Move_Auto_High()
    {
        GameObject cl_World_MoveTo = null;

        float f_Speed = 0;

        if (g_World_StandOn == null)
        {
            if (g_World_StandOn == null)
            {
                return;
            }
            else
            {
                cl_World_MoveTo = g_World_StandOn;

                f_Speed = this.f_Speed;
            }
        }
        else
        {
            cl_World_MoveTo = g_World_StandOn;

            f_Speed = this.f_Speed + g_World_StandOn.GetComponent<IsoBlockMove>().GetMove_Active_Current().GetSpeed();
        }

        float f_High = 0;

        if (cl_World_MoveTo.GetComponent<IsoBlock>().GetBlock_Check_Stair())
        {
            f_High = 0.5f;

            //if (cl_World_MoveTo.GetComponent<Isometric2D_Block>().GetBlock_Check_StairD() || 
            //    cl_World_MoveTo.GetComponent<Isometric2D_Block>().GetBlock_Check_StairL())
            //{
            //    cl_Block.Set_Fix(new Vector3(0f, 0f, 1f));
            //}
            //else
            {
                cl_Block.Set_Fix(new Vector3(0f, 0f, 0));
            }
        }
        else
        {
            f_High = 1f;

            cl_Block.Set_Fix(new Vector3(0f, 0f, 0));
        }

        //Move

        if (cl_Block.GetPos_Current().z < cl_World_MoveTo.GetComponent<IsoBlock>().GetPos_Current().z + f_High)
        {
            if (cl_Block.GetPos_Current().z + f_Speed > cl_World_MoveTo.GetComponent<IsoBlock>().GetPos_Current().z + f_High)
            {
                cl_Block.Set_Pos_High(cl_World_MoveTo.GetComponent<IsoBlock>().GetPos_Current().z + f_High);

                m_Moved_High = true;
            }
            else
            {
                cl_Block.Set_Pos_Add(0, 0, f_Speed);
            }
        }
        else
        if (cl_Block.GetPos_Current().z > cl_World_MoveTo.GetComponent<IsoBlock>().GetPos_Current().z + f_High)
        {
            if (cl_Block.GetPos_Current().z - f_Speed < cl_World_MoveTo.GetComponent<IsoBlock>().GetPos_Current().z + f_High)
            {
                cl_Block.Set_Pos_High(cl_World_MoveTo.GetComponent<IsoBlock>().GetPos_Current().z + f_High);

                m_Moved_High = true;
            }
            else
            {
                cl_Block.Set_Pos_Add(0, 0, -f_Speed);
            }
        }
        else
        {
            m_Moved_High = true;
        }

        if (f_Moved_High > 0)
        {
            f_Moved_High -= f_Speed;
        }
    }

    #endregion

    /// <summary>
    /// Update World (Block, Move and Join) Stand-On
    /// </summary>
    public void Set_World_StandOn()
    {
        if (g_World_StandOn == null)
        {
            g_World_StandOn = cl_WorldManager.GetCurrent_GameObject(cl_Block.GetPosOnMatrix_Current() + IsoClassDir.v3_Bot_H);
        }
    }
}
