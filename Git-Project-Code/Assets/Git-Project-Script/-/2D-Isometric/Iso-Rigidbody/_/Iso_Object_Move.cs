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

    private bool b_Moved_X = true;

    private float f_Moved_X = 0f;

    private bool b_Moved_Y = true;

    private float f_Moved_Y = 0f;

    private bool b_Moved_High = true;

    private float f_Moved_High = 0f;

    private bool b_GroundToStair = false;

    #endregion

    private void Start()
    {
        cl_Block = GetComponent<IsoBlock>();
    }

    private void Update()
    {
        if (!cl_WorldManager.Get_World_isGenerated())
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
    public void Set_Move(Vector3Int v3_Move_Dir, Vector3Int v3_Dir_High, bool b_GroundToStair)
    {
        if (!cl_WorldManager.Get_World_isGenerated())
        {
            return;
        }

        if (g_World_StandOn == null)
        {
            return;
        }

        if (!b_Moved_X || !b_Moved_Y || !b_Moved_High)
        {
            return;
        }

        GameObject cl_World_Check = cl_WorldManager.Get_Current_GameObject(g_World_StandOn.GetComponent<IsoBlock>().Get_PosOnMatrix_Current() + v3_Move_Dir + v3_Dir_High);

        if (cl_World_Check == null)
        {
            return;
        }

        g_World_StandOn = cl_World_Check;

        this.b_GroundToStair = b_GroundToStair;

        b_Moved_X = false;
        b_Moved_Y = false;
        b_Moved_High = false;

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
    public GameObject Get_World_Current(Vector3Int v3_Move_Dir, Vector3Int v3_Dir_High)
    {
        return cl_WorldManager.Get_Current_GameObject(g_World_StandOn.GetComponent<IsoBlock>().Get_PosOnMatrix_Current() + v3_Move_Dir + v3_Dir_High);
    }

    /// <summary>
    /// Get World Check Emty (Block, Move and Join) from Stand-On, with add two Dir of Move and High to Get
    /// </summary>
    /// <param name="v3_Move_Dir"></param>
    /// <param name="v3_Dir_High"></param>
    /// <returns></returns>
    public bool Get_World_Emty(Vector3Int v3_Move_Dir, Vector3Int v3_Dir_High)
    {
        return Get_World_Current(v3_Move_Dir, v3_Dir_High) == null;
    }

    /// <summary>
    /// Get World (Block, Move and Join) from Stand-On
    /// </summary>
    /// <returns></returns>
    public GameObject Get_World_StandOn()
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

        if (b_GroundToStair)
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

            f_Speed = this.f_Speed + g_World_StandOn.GetComponent<IsoBlockMove>().Get_Move_Active_Current().Get_Speed();
        }

        //Move

        if (cl_Block.Get_Pos_Current().x < cl_World_MoveTo.GetComponent<IsoBlock>().Get_Pos_Current().x)
        {
            if (cl_Block.Get_Pos_Current().x + f_Speed > cl_World_MoveTo.GetComponent<IsoBlock>().Get_Pos_Current().x)
            {
                cl_Block.Set_Pos_X(cl_World_MoveTo.GetComponent<IsoBlock>().Get_Pos_Current().x);

                b_Moved_X = true;
            }
            else
            {
                cl_Block.Set_Pos_Add(f_Speed, 0, 0);
            }
        }
        else
        if (cl_Block.Get_Pos_Current().x > cl_World_MoveTo.GetComponent<IsoBlock>().Get_Pos_Current().x)
        {
            if (cl_Block.Get_Pos_Current().x - f_Speed < cl_World_MoveTo.GetComponent<IsoBlock>().Get_Pos_Current().x)
            {
                cl_Block.Set_Pos_X(cl_World_MoveTo.GetComponent<IsoBlock>().Get_Pos_Current().x);

                b_Moved_X = true;
            }
            else
            {
                cl_Block.Set_Pos_Add(-f_Speed, 0, 0);
            }
        }
        else
        {
            b_Moved_X = true;
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

            f_Speed = this.f_Speed + g_World_StandOn.GetComponent<IsoBlockMove>().Get_Move_Active_Current().Get_Speed();
        }

        //Move

        if (cl_Block.Get_Pos_Current().y < cl_World_MoveTo.GetComponent<IsoBlock>().Get_Pos_Current().y)
        {
            if (cl_Block.Get_Pos_Current().y + f_Speed > cl_World_MoveTo.GetComponent<IsoBlock>().Get_Pos_Current().y)
            {
                cl_Block.Set_Pos_Y(cl_World_MoveTo.GetComponent<IsoBlock>().Get_Pos_Current().y);

                b_Moved_Y = true;
            }
            else
            {
                cl_Block.Set_Pos_Add(0, f_Speed, 0);
            }
        }
        else
        if (cl_Block.Get_Pos_Current().y > cl_World_MoveTo.GetComponent<IsoBlock>().Get_Pos_Current().y)
        {
            if (cl_Block.Get_Pos_Current().y - f_Speed < cl_World_MoveTo.GetComponent<IsoBlock>().Get_Pos_Current().y)
            {
                cl_Block.Set_Pos_Y(cl_World_MoveTo.GetComponent<IsoBlock>().Get_Pos_Current().y);

                b_Moved_Y = true;
            }
            else
            {
                cl_Block.Set_Pos_Add(0, -f_Speed, 0);
            }
        }
        else
        {
            b_Moved_Y = true;
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

            f_Speed = this.f_Speed + g_World_StandOn.GetComponent<IsoBlockMove>().Get_Move_Active_Current().Get_Speed();
        }

        float f_High = 0;

        if (cl_World_MoveTo.GetComponent<IsoBlock>().Get_Block_Check_Stair())
        {
            f_High = 0.5f;

            //if (cl_World_MoveTo.GetComponent<Isometric2D_Block>().Get_Block_Check_StairD() || 
            //    cl_World_MoveTo.GetComponent<Isometric2D_Block>().Get_Block_Check_StairL())
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

        if (cl_Block.Get_Pos_Current().z < cl_World_MoveTo.GetComponent<IsoBlock>().Get_Pos_Current().z + f_High)
        {
            if (cl_Block.Get_Pos_Current().z + f_Speed > cl_World_MoveTo.GetComponent<IsoBlock>().Get_Pos_Current().z + f_High)
            {
                cl_Block.Set_Pos_High(cl_World_MoveTo.GetComponent<IsoBlock>().Get_Pos_Current().z + f_High);

                b_Moved_High = true;
            }
            else
            {
                cl_Block.Set_Pos_Add(0, 0, f_Speed);
            }
        }
        else
        if (cl_Block.Get_Pos_Current().z > cl_World_MoveTo.GetComponent<IsoBlock>().Get_Pos_Current().z + f_High)
        {
            if (cl_Block.Get_Pos_Current().z - f_Speed < cl_World_MoveTo.GetComponent<IsoBlock>().Get_Pos_Current().z + f_High)
            {
                cl_Block.Set_Pos_High(cl_World_MoveTo.GetComponent<IsoBlock>().Get_Pos_Current().z + f_High);

                b_Moved_High = true;
            }
            else
            {
                cl_Block.Set_Pos_Add(0, 0, -f_Speed);
            }
        }
        else
        {
            b_Moved_High = true;
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
            g_World_StandOn = cl_WorldManager.Get_Current_GameObject(cl_Block.Get_PosOnMatrix_Current() + IsoClassDir.v3_Bot_H);
        }
    }
}
