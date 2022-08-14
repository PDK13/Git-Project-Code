using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IsoBlock))]
public class IsoBlockMove : MonoBehaviour
{
    #region Move

    [Header("Move Primary")]

    [Tooltip("Move List")]
    private List<IsoDataMoveSingle> l_Move = new List<IsoDataMoveSingle>();

    [Tooltip("Index on Move List")]
    [SerializeField]
    private int i_Step_inActive = 0;

    [Header("Move Status")]

    [Tooltip("Status of Move is Active or Not-Active")]
    [SerializeField]
    private bool b_Status_isActive = false;

    [Header("Move Hop")]

    [Tooltip("Hop Between Move(s) when Status of Move is Active")]
    [SerializeField]
    private bool b_Hop_isAllow = false;

    [Tooltip("Hop Current will True when call and will False after end Move Step")]
    [SerializeField]
    private bool b_Hop_isActive = false;

    #endregion

    #region Pos

    [Header("Move Pos")]

    [Tooltip("Pos on Matrix Before Move")]
    [SerializeField]
    private Vector3Int v3_PosMatrix_Before = new Vector3Int();

    [Tooltip("Pos Own A will Chance when Pos on Matrix Single reach Pos Own B")]
    //[SerializeField]
    private Vector3Int v3_PosMatrix_Own_A = new Vector3Int();

    [Tooltip("Pos Own B will Chance when Pos on Matrix Single over come Pos Own A")]
    //[SerializeField]
    private Vector3Int v3_PosMatrix_Own_B = new Vector3Int();

    #endregion

    [Header("Move Component")]

    private IsoBlock cl_Block;

    private void Awake()
    {
        l_Move = new List<IsoDataMoveSingle>();
    }

    private void Start()
    {
        cl_Block = GetComponent<IsoBlock>();
    }

    private void Update()
    {
        if (!Get_Move_FineToWork())
        {
            return;
        }

        Set_Move_Active(
            Get_Count() > 0 &&
            Get_Status_isActive() &&
            Get_Hop_fixWorking()
        );
    }

    public void Set_Move(IsoBlockMove cl_Move)
    {
        v3_PosMatrix_Own_A = cl_Block.Get_PosOnMatrix_Primary();
        v3_PosMatrix_Own_B = cl_Block.Get_PosOnMatrix_Primary();

        v3_PosMatrix_Before = cl_Block.Get_PosOnMatrix_Primary();

        Set_List(cl_Move.Get_List());
        Set_Status_isActive(cl_Move.Get_Status_isActive_Numberic());
    }

    private bool Get_Move_FineToWork()
    {
        if (cl_Block.Get_World() != null)
        {
            if (!cl_Block.Get_World().Get_World_isGenerated())
            {
                return false;
            }

            if (!cl_Block.Get_World().Get_World_isActive())
            {
                return false;
            }
        }
        else
        {
            return false;
        }

        return true;
    }

    //============================================================================================================ Move Active

    #region Move Active 

    #region Move Active Main 

    /// <summary>
    /// Primary Move Active
    /// </summary>
    /// <param name="b_Move_Active"></param>
    /// <remarks>
    /// The Move will take 1 or 2 Pos(s) on Matrix, called Own Pos(s), the other Move(s) not allow to Move in this Own Pos(s)
    /// </remarks>
    private void Set_Move_Active(bool b_Move_Active)
    {
        if (!b_Move_Active)
        {
            return;
        }

        //If Current Move at End Pos and Start Pos not Equa to End Pos
        if (Get_Move_Active_Index_End() && !Get_Move_Active_Index_Loop())
        {
            //Hop Stop Active if Hop Allow
            if (Get_Hop_isAllow())
            {
                Set_Hop_stopActive();
            }

            return;
        }

        //Move-On

        //Stop
        if (Get_Move_Active_Current().Get_Dir() == IsoClassDir.v3_None)
        {
            //Set_Status_isActive_Chance();

            Set_Move_Active_Index_Continue();
        }
        else
        //X
        //Up
        if (Get_Move_Active_Current().Get_Dir() == IsoClassDir.v3_Up_X)
        {
            //If Move above the Move Current
            if (cl_Block.Get_Pos_Current().x + (-Get_Move_Active_Current().Get_Speed()) < l_Move[i_Step_inActive].Get_PosMoveTo().x)
            {
                //Set Pos on True Move Current
                cl_Block.Set_Pos(l_Move[i_Step_inActive].Get_PosMoveTo());

                //Set Next Move
                Set_Move_Active_Index_Continue();

                //This Move take just 1 Pos on Matrix
                v3_PosMatrix_Own_A = v3_PosMatrix_Own_B;
            }
            //If Move on the way to Move Current
            else
            {
                //Moving
                cl_Block.Set_Pos_Add(new Vector3(-Get_Move_Active_Current().Get_Speed(), 0, 0));

                //If Moving to another Pos
                if (cl_Block.Get_Pos_Current().x <= Get_PosMatrix_Own_B().x)
                {
                    //This Move take 2 Pos(s) on Matrix
                    v3_PosMatrix_Own_A = v3_PosMatrix_Own_B;
                    v3_PosMatrix_Own_B = v3_PosMatrix_Own_A + Get_Move_Active_Current().Get_Dir();

                    //cl_Block.Set_Pos(v3_PosMatrix_Own_A);
                }
            }
        }
        else
        //Down
        if (Get_Move_Active_Current().Get_Dir() == IsoClassDir.v3_Down_X)
        {
            if (cl_Block.Get_Pos_Current().x + (Get_Move_Active_Current().Get_Speed()) > l_Move[i_Step_inActive].Get_PosMoveTo().x)
            {
                cl_Block.Set_Pos(l_Move[i_Step_inActive].Get_PosMoveTo());

                Set_Move_Active_Index_Continue();

                v3_PosMatrix_Own_A = v3_PosMatrix_Own_B;
            }
            else
            {
                cl_Block.Set_Pos_Add(new Vector3(Get_Move_Active_Current().Get_Speed(), 0, 0));

                if (cl_Block.Get_Pos_Current().x >= Get_PosMatrix_Own_B().x)
                {
                    v3_PosMatrix_Own_A = v3_PosMatrix_Own_B;
                    v3_PosMatrix_Own_B = v3_PosMatrix_Own_A + Get_Move_Active_Current().Get_Dir();

                    //cl_Block.Set_Pos(v3_PosMatrix_Own_A);
                }
            }
        }
        else
        //Y
        //Left
        if (Get_Move_Active_Current().Get_Dir() == IsoClassDir.v3_Left_Y)
        {
            if (cl_Block.Get_Pos_Current().y + (-Get_Move_Active_Current().Get_Speed()) < l_Move[i_Step_inActive].Get_PosMoveTo().y)
            {
                cl_Block.Set_Pos(l_Move[i_Step_inActive].Get_PosMoveTo());

                Set_Move_Active_Index_Continue();

                v3_PosMatrix_Own_A = v3_PosMatrix_Own_B;
            }
            else
            {
                cl_Block.Set_Pos_Add(new Vector3(0, -Get_Move_Active_Current().Get_Speed(), 0));

                if (cl_Block.Get_Pos_Current().y <= Get_PosMatrix_Own_B().y)
                {
                    v3_PosMatrix_Own_A = v3_PosMatrix_Own_B;
                    v3_PosMatrix_Own_B = v3_PosMatrix_Own_A + Get_Move_Active_Current().Get_Dir();

                    //cl_Block.Set_Pos(v3_PosMatrix_Own_A);
                }
            }
        }
        else
        //Right
        if (Get_Move_Active_Current().Get_Dir() == IsoClassDir.v3_Right_Y)
        {
            if (cl_Block.Get_Pos_Current().y + (Get_Move_Active_Current().Get_Speed()) > l_Move[i_Step_inActive].Get_PosMoveTo().y)
            {
                cl_Block.Set_Pos(l_Move[i_Step_inActive].Get_PosMoveTo());

                Set_Move_Active_Index_Continue();

                v3_PosMatrix_Own_A = v3_PosMatrix_Own_B;
            }
            else
            {
                cl_Block.Set_Pos_Add(new Vector3(0, Get_Move_Active_Current().Get_Speed(), 0));

                if (cl_Block.Get_Pos_Current().y >= Get_PosMatrix_Own_B().y)
                {
                    v3_PosMatrix_Own_A = v3_PosMatrix_Own_B;
                    v3_PosMatrix_Own_B = v3_PosMatrix_Own_A + Get_Move_Active_Current().Get_Dir();

                    //cl_Block.Set_Pos(v3_PosMatrix_Own_A);
                }
            }
        }
        else
        //Z
        //Top
        if (Get_Move_Active_Current().Get_Dir() == IsoClassDir.v3_Top_H)
        {
            if (cl_Block.Get_Pos_Current().z + (Get_Move_Active_Current().Get_Speed()) > l_Move[i_Step_inActive].Get_PosMoveTo().z)
            {
                cl_Block.Set_Pos(l_Move[i_Step_inActive].Get_PosMoveTo());

                Set_Move_Active_Index_Continue();

                v3_PosMatrix_Own_A = v3_PosMatrix_Own_B;
            }
            else
            {
                cl_Block.Set_Pos_Add(new Vector3(0, 0, Get_Move_Active_Current().Get_Speed()));

                if (cl_Block.Get_Pos_Current().z >= Get_PosMatrix_Own_B().z)
                {
                    v3_PosMatrix_Own_A = v3_PosMatrix_Own_B;
                    v3_PosMatrix_Own_B = v3_PosMatrix_Own_A + Get_Move_Active_Current().Get_Dir();

                    //cl_Block.Set_Pos(v3_PosMatrix_Own_A);
                }
            }
        }
        else
        //Bot
        if (Get_Move_Active_Current().Get_Dir() == IsoClassDir.v3_Bot_H)
        {
            if (cl_Block.Get_Pos_Current().z + (-Get_Move_Active_Current().Get_Speed()) < l_Move[i_Step_inActive].Get_PosMoveTo().z)
            {
                cl_Block.Set_Pos(l_Move[i_Step_inActive].Get_PosMoveTo());

                Set_Move_Active_Index_Continue();

                v3_PosMatrix_Own_A = v3_PosMatrix_Own_B;
            }
            else
            {
                cl_Block.Set_Pos_Add(new Vector3(0, 0, -Get_Move_Active_Current().Get_Speed()));

                if (cl_Block.Get_Pos_Current().z <= Get_PosMatrix_Own_B().z)
                {
                    v3_PosMatrix_Own_A = v3_PosMatrix_Own_B;
                    v3_PosMatrix_Own_B = v3_PosMatrix_Own_A + Get_Move_Active_Current().Get_Dir();

                    //cl_Block.Set_Pos(v3_PosMatrix_Own_A);
                }
            }
        }

        if (Get_Move_Active_PosOnMatrix_Chance())
        {
            //Update Pos
            Set_Move_Active_PosOnMatrix_Update();
        }
    }

    /// <summary>
    /// Get Move Current on Move Active
    /// </summary>
    /// <returns></returns>
    public IsoDataMoveSingle Get_Move_Active_Current()
    {
        return Get_List(i_Step_inActive);
    }

    #endregion

    #region Move Active Work 

    /// <summary>
    /// Update Index for Continue Move
    /// </summary>
    private void Set_Move_Active_Index_Continue()
    {
        i_Step_inActive++;

        if (Get_Move_Active_Index_End())
        {
            if (Get_Move_Active_Index_Loop())
            {
                i_Step_inActive = 0;
            }
        }

        //Hop Stop Active if Hop Allow
        if (Get_Hop_isAllow())
        {
            Set_Hop_stopActive();
        }
    }

    /// <summary>
    /// Check PosMoveTo of Lastest Move is the Start Pos
    /// </summary>
    /// <returns></returns>
    private bool Get_Move_Active_Index_Loop()
    {
        return cl_Block.Get_PosOnMatrix_Primary() == l_Move[l_Move.Count - 1].Get_PosMoveTo();
    }

    /// <summary>
    /// Check Move Active End at List
    /// </summary>
    /// <returns></returns>
    private bool Get_Move_Active_Index_End()
    {
        return i_Step_inActive > l_Move.Count - 1;
    }

    /// <summary>
    /// Get Pos on Matrix Single Equa to Pos on Matrix Before
    /// </summary>
    /// <returns></returns>
    private bool Get_Move_Active_PosOnMatrix_Chance()
    {
        return Get_PosOnMatrix_Before() != cl_Block.Get_PosOnMatrix_Current();
    }

    /// <summary>
    /// Update Pos on Matrix Before with Current Pos on Matrix
    /// </summary>
    private void Set_Move_Active_PosOnMatrix_Update()
    {
        if (cl_Block != null)
        {
            v3_PosMatrix_Before = cl_Block.Get_PosOnMatrix_Current();
        }
    }

    #endregion

    #endregion

    #region Move Pos 

    /// <summary>
    /// Reset Move to Start Pos
    /// </summary>
    public void Set_Active_Reset()
    {
        i_Step_inActive = 0;

        cl_Block.Set_PosOnMatrix_ResetToPrimary();
    }

    /// <summary>
    /// Get Pos Before Move on Matrix
    /// </summary>
    /// <returns></returns>
    public Vector3Int Get_PosOnMatrix_Before()
    {
        return v3_PosMatrix_Before;
    }

    #endregion

    //============================================================================================================ Move List

    #region Move List 

    #region Move List Add 

    public void Set_Add(Vector3Int v3_Move_Dir, int i_Move_Length, float f_Move_Speed)
    {
        if (i_Move_Length <= 0)
        {
            return;
        }

        if (f_Move_Speed <= 0)
        {
            return;
        }

        if (l_Move == null)
        {
            l_Move = new List<IsoDataMoveSingle>();
        }

        if (cl_Block == null)
        {
            cl_Block = GetComponent<IsoBlock>();
        }

        Vector3Int v3_Move_PosMoveTo = 
            (l_Move.Count == 0) ? 
            (cl_Block.Get_PosOnMatrix_Primary() + v3_Move_Dir * i_Move_Length) : 
            (l_Move[l_Move.Count - 1].Get_PosMoveTo() + v3_Move_Dir * i_Move_Length);

        l_Move.Add(new IsoDataMoveSingle(v3_Move_Dir, i_Move_Length, f_Move_Speed, v3_Move_PosMoveTo));
    }

    /// <summary>
    /// Add Reverse of All Current Move
    /// </summary>
    public void Set_Add_Res()
    {
        int i_Repeat = l_Move.Count - 1;

        for (int i = i_Repeat; i >= 0; i--)
        {
            if (IsoClassDir.Get_Vector_One(l_Move[i].Get_Dir()) == IsoClassDir.v3_Up_X)
            {
                Set_Add(IsoClassDir.v3_Down_X, l_Move[i].Get_Length(), l_Move[i].Get_Speed());
            }
            else
            if (IsoClassDir.Get_Vector_One(l_Move[i].Get_Dir()) == IsoClassDir.v3_Down_X)
            {
                Set_Add(IsoClassDir.v3_Up_X, l_Move[i].Get_Length(), l_Move[i].Get_Speed());
            }
            else
            if (IsoClassDir.Get_Vector_One(l_Move[i].Get_Dir()) == IsoClassDir.v3_Left_Y)
            {
                Set_Add(IsoClassDir.v3_Right_Y, l_Move[i].Get_Length(), l_Move[i].Get_Speed());
            }
            else
            if (IsoClassDir.Get_Vector_One(l_Move[i].Get_Dir()) == IsoClassDir.v3_Right_Y)
            {
                Set_Add(IsoClassDir.v3_Left_Y, l_Move[i].Get_Length(), l_Move[i].Get_Speed());
            }
            else
            if (IsoClassDir.Get_Vector_One(l_Move[i].Get_Dir()) == IsoClassDir.v3_Top_H)
            {
                Set_Add(IsoClassDir.v3_Bot_H, l_Move[i].Get_Length(), l_Move[i].Get_Speed());
            }
            else
            if (IsoClassDir.Get_Vector_One(l_Move[i].Get_Dir()) == IsoClassDir.v3_Bot_H)
            {
                Set_Add(IsoClassDir.v3_Top_H, l_Move[i].Get_Length(), l_Move[i].Get_Speed());
            }
            else
            if (IsoClassDir.Get_Vector_One(l_Move[i].Get_Dir()) == IsoClassDir.v3_None)
            {
                Set_Add(IsoClassDir.v3_None, l_Move[i].Get_Length(), l_Move[i].Get_Speed());
            }
        }
    }

    public void Set_List(List<IsoDataMoveSingle> l_Move_List)
    {
        //this.l_Move = l_Move_List;

        //Set_List_Fix(0);

        for (int i = 0; i < l_Move_List.Count; i++) 
        {
            Set_Add(
                l_Move_List[i].Get_Dir(),
                l_Move_List[i].Get_Length(),
                l_Move_List[i].Get_Speed());
        }
    }

    #endregion

    #region Move List Chance 

    public void Set_Chance(int i_Move_Index, Vector3Int v3_Move_Dir, int i_Move_Length, float f_Move_Speed)
    {
        if (i_Move_Index > Get_Count() - 1)
        {
            return;
        }

        Vector3Int v3_Move_PosMoveTo = 
            (i_Move_Index == 0) ? 
            (cl_Block.Get_PosOnMatrix_Primary() + v3_Move_Dir * i_Move_Length) : 
            (l_Move[i_Move_Index - 1].Get_PosMoveTo() + v3_Move_Dir * i_Move_Length);

        l_Move[i_Move_Index] = new IsoDataMoveSingle(v3_Move_Dir, i_Move_Length, f_Move_Speed, v3_Move_PosMoveTo);

        Set_List_Fix(i_Move_Index);
    }

    public void Set_Speed(float f_Move_Speed)
    {
        for (int i = 0; i < l_Move.Count; i++)
        {
            l_Move[i].Set_Speed(f_Move_Speed);
        }
    }

    #endregion

    #region Move List Remove 

    public void Set_Remove(int i_Move_Index)
    {
        if (i_Move_Index < 0 || i_Move_Index > l_Move.Count - 1)
        {
            return;
        }

        //Remove at Index
        l_Move.RemoveAt(i_Move_Index);

        Set_List_Fix(i_Move_Index);
    }

    public void Set_Remove_Lastest()
    {
        Set_Remove(l_Move.Count - 1);
    }

    #endregion

    /// <summary>
    /// Fix Move List when Chance somewhere on List
    /// </summary>
    /// <param name="i_Move_Fix_Start"></param>
    private void Set_List_Fix(int i_Move_Fix_Start)
    {
        for (int i = i_Move_Fix_Start; i < l_Move.Count; i++)
        {
            Vector3Int v3_Move_PosMoveTo = 
                (i == 0) ? 
                (cl_Block.Get_PosOnMatrix_Primary() + l_Move[i].Get_Dir() * l_Move[i].Get_Length()) : 
                (l_Move[i - 1].Get_PosMoveTo() + l_Move[i].Get_Dir() * l_Move[i].Get_Length());

            l_Move[i] = new IsoDataMoveSingle(l_Move[i].Get_Dir(), l_Move[i].Get_Length(), l_Move[i].Get_Speed(), v3_Move_PosMoveTo);
        }
    }

    #endregion

    #region Move List Primary 

    public int Get_Count()
    {
        return l_Move.Count;
    }

    public IsoDataMoveSingle Get_List(int i_Move_Index)
    {
        return this.l_Move[i_Move_Index];
    }

    public IsoDataMoveSingle Get_List_Lastest()
    {
        return Get_List(Get_Count() - 1);
    }

    public List<IsoDataMoveSingle> Get_List()
    {
        return this.l_Move;
    }

    #endregion

    //============================================================================================================ Move Status

    #region Move Status 

    #region Move Status Active

    public void Set_Status_isActive_True()
    {
        b_Status_isActive = true;
    }

    public void Set_Status_isActive_False()
    {
        b_Status_isActive = false;
    }

    public void Set_Status_isActive_Chance()
    {
        b_Status_isActive = !b_Status_isActive;
    }

    public void Set_Status_isActive(bool b_Status_isActive)
    {
        if (b_Status_isActive)
        {
            Set_Status_isActive_True();
        }
        else
        {
            Set_Status_isActive_False();
        }
    }

    /// <summary>
    /// Set Move Status Active (1 or 0)
    /// </summary>
    /// <param name="i_Status_isActive_Numberic"></param>
    public void Set_Status_isActive(int i_Status_isActive_Numberic)
    {
        this.b_Status_isActive = Get_Status_isActive_Numberic_Complete(i_Status_isActive_Numberic);
    }

    public bool Get_Status_isActive()
    {
        return this.b_Status_isActive;
    }

    #endregion

    #region Move Status Numberic 

    /// <summary>
    /// Get Move Status Active (1 or 0)
    /// </summary>
    /// <returns></returns>
    public int Get_Status_isActive_Numberic()
    {
        if (this.b_Status_isActive)
        {
            return 1;
        }
        return 0;
    }

    /// <summary>
    /// Complete Chance Move Status Active into '0' and '1'
    /// </summary>
    /// <param name="i_Status_Numberic">Move Number Active Current</param>
    /// <returns></returns>
    private bool Get_Status_isActive_Numberic_Complete(int i_Status_Numberic)
    {
        if (i_Status_Numberic == 1)
        {
            return true;
        }
        if (i_Status_Numberic == 0)
        {
            return false;
        }
        return false;
    }

    #endregion

    #endregion

    //============================================================================================================ Move Hop

    #region Move Hop 

    #region Move Hop Allow 

    public void Set_Hop_isAllow(bool b_Hop_isAllow)
    {
        this.b_Hop_isAllow = b_Hop_isAllow;
    }

    public bool Get_Hop_isAllow()
    {
        return this.b_Hop_isAllow;
    }

    #endregion

    #region Move Hop Active 

    /// <summary>
    /// Hop Active after Hop is Allow
    /// </summary>
    public void Set_Hop_isActive()
    {
        if (Get_Count() == 0 || !Get_Status_isActive())
        {
            this.b_Hop_isActive = false;
        }
        else
        {
            this.b_Hop_isActive = true;
        }
    }

    private void Set_Hop_stopActive()
    {
        this.b_Hop_isActive = false;
    }

    public bool Get_Hop_isActive()
    {
        return this.b_Hop_isActive;
    }

    #endregion

    #region Move Hop Fix 

    private bool Get_Hop_fixWorking()
    {
        if (b_Hop_isAllow)
        {
            return b_Hop_isActive;
        }
        return true;
    }

    #endregion

    #endregion

    //============================================================================================================ ...

    #region Pos Own 

    /// <summary>
    /// Reset Pos Own A and B
    /// </summary>
    public void Set_PosMatrix_Own_Reset()
    {
        v3_PosMatrix_Own_A = cl_Block.Get_PosOnMatrix_Primary();
        v3_PosMatrix_Own_B = cl_Block.Get_PosOnMatrix_Primary();
    }

    /// <summary>
    /// Get Pos Own A
    /// </summary>
    /// <remarks>
    /// Pos Own A will Chance when Pos on Matrix Single reach Pos Own B
    /// </remarks>
    /// <returns></returns>
    public Vector3Int Get_PosMatrix_Own_A()
    {
        //if (g_Join != null)
        //{
        //    return g_Join.GetComponent<Iso_Block_Move>().Get_PosMatrix_Own_A() + Get_Join_Pos();
        //}

        return v3_PosMatrix_Own_A;
    }

    /// <summary>
    /// Get Pos Own B
    /// </summary>
    /// <remarks>
    /// Pos Own B will Chance when Pos on Matrix Single over come Pos Own A
    /// </remarks>
    /// <returns></returns>
    public Vector3Int Get_PosMatrix_Own_B()
    {
        //if (g_Join != null)
        //{
        //    return g_Join.GetComponent<Iso_Block_Move>().Get_PosMatrix_Own_B() + Get_Join_Pos();
        //}

        return v3_PosMatrix_Own_B;
    }

    /// <summary>
    /// Get Remain of Current Pos to Pos Own B
    /// </summary>
    /// <returns></returns>
    public float Get_Remain_Own_B()
    {
        Vector3 v3_Remain = this.cl_Block.Get_Pos_Current() - Class_Vector.Get_Vector(Get_PosMatrix_Own_B());

        if (Get_Move_Active_Current().Get_Dir() == IsoClassDir.v3_Up_X || Get_Move_Active_Current().Get_Dir() == IsoClassDir.v3_Down_X)
        {
            return (Mathf.Abs(v3_Remain.x));
        }
        if (Get_Move_Active_Current().Get_Dir() == IsoClassDir.v3_Left_Y || Get_Move_Active_Current().Get_Dir() == IsoClassDir.v3_Right_Y)
        {
            return (Mathf.Abs(v3_Remain.y));
        }
        if (Get_Move_Active_Current().Get_Dir() == IsoClassDir.v3_Top_H || Get_Move_Active_Current().Get_Dir() == IsoClassDir.v3_Bot_H)
        {
            return (Mathf.Abs(v3_Remain.z));
        }
        return 0;
    }

    #endregion

    #region Check with other Move(s) and World 

    private bool Get_Move_Stop_Check_World()
    {
        //If Not Emty at Pos Own B
        if (!cl_Block.Get_World().Get_Primary_isEmty(Get_PosMatrix_Own_B()))
        {
            return true;
        }

        //Non-Stop Moving
        return false;
    }

    //private bool Get_Move_Stop_Check_List(int i_Move_Index_Current)
    //{
    //    if (!cl_World.Get_Move_Check())
    //    {
    //        //Non-Stop Moving
    //        return false;
    //    }

    //    for (int i = 0; i < cl_World.Get_Move_List().Count; i++)
    //    {
    //        if (i_Move_Index_Current != i)
    //        {
    //            if (Get_PosMatrix_Own_B() == cl_World.Get_Move_List()[i][0].GetComponent<Iso_Block_Move>().Get_PosMatrix_Own_A())
    //            //Own B = A
    //            {
    //                if (Get_Move_Current().Get_Dir() == cl_World.Get_Move_List()[i][0].GetComponent<Iso_Block_Move>().Get_Move_Current().Get_Dir())
    //                //Dir Equa
    //                {
    //                    if (Get_Remain_Own_B() <= cl_World.Get_Move_List()[i][0].GetComponent<Iso_Block_Move>().Get_Remain_Own_B())
    //                    //Remain Lower or Equa
    //                    {
    //                        //Stop Moving
    //                        return true;
    //                    }
    //                }
    //                else
    //                {
    //                    if (cl_World.Get_Move_List()[i][0].GetComponent<Iso_Block_Move>().Get_Remain_Own_B() > cl_World.Get_Move_Fix())
    //                    //Remain Check
    //                    {
    //                        //Stop Moving
    //                        return true;
    //                    }
    //                }
    //            }
    //            else
    //            if (Get_PosMatrix_Own_B() == cl_World.Get_Move_List()[i][0].GetComponent<Iso_Block_Move>().Get_PosMatrix_Own_B())
    //            //Own B = B
    //            {
    //                if (Get_Remain_Own_B() > cl_World.Get_Move_List()[i][0].GetComponent<Iso_Block_Move>().Get_Remain_Own_B())
    //                //Remain Higher
    //                {
    //                    //Stop Moving
    //                    return true;
    //                }
    //                else
    //                if (Get_Remain_Own_B() == cl_World.Get_Move_List()[i][0].GetComponent<Iso_Block_Move>().Get_Remain_Own_B())
    //                {
    //                    if (Get_Move_Current().Get_Speed() < cl_World.Get_Move_List()[i][0].GetComponent<Iso_Block_Move>().Get_Move_Current().Get_Speed())
    //                    //Speed Lower
    //                    {
    //                        //Stop Moving
    //                        return true;
    //                    }
    //                    else
    //                    if (Get_Move_Current().Get_Speed() == cl_World.Get_Move_List()[i][0].GetComponent<Iso_Block_Move>().Get_Move_Current().Get_Speed())
    //                    {
    //                        if (Get_Move_Current().Get_Length() < cl_World.Get_Move_List()[i][0].GetComponent<Iso_Block_Move>().Get_Move_Current().Get_Length())
    //                        //Length Lower
    //                        {
    //                            //Stop Moving
    //                            return true;
    //                        }
    //                        else
    //                        if (Get_Move_Current().Get_Length() == cl_World.Get_Move_List()[i][0].GetComponent<Iso_Block_Move>().Get_Move_Current().Get_Length())
    //                        {
    //                            if (i_Move_Index_Current < i)
    //                            {
    //                                //Stop Moving
    //                                return true;
    //                            }
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //    }

    //    //Non-Stop Moving
    //    return false;
    //}

    #endregion
}
