using UnityEngine;

public class Iso_Move_Primary : MonoBehaviour
{
    private Iso_Object_Move cl_Move;

    private void Start()
    {
        cl_Move = GetComponent<Iso_Object_Move>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Get_Check(IsoClassDir.v3_Up_X);
        }

        if (Input.GetKey(KeyCode.S))
        {
            Get_Check(IsoClassDir.v3_Down_X);
        }

        if (Input.GetKey(KeyCode.A))
        {
            Get_Check(IsoClassDir.v3_Left_Y);
        }

        if (Input.GetKey(KeyCode.D))
        {
            Get_Check(IsoClassDir.v3_Right_Y);
        }
    }

    private void Get_Check(Vector3Int v3_Move_Dir)
    {
        cl_Move.Set_World_StandOn();

        if (cl_Move.Get_World_StandOn().GetComponent<IsoBlock>().Get_Block_Check_Stair())
        //If Stand-On Stair
        {
            if (v3_Move_Dir == IsoClassDir.v3_Up_X ||
                v3_Move_Dir == IsoClassDir.v3_Down_X)
            //If Move Up Down
            {
                if (cl_Move.Get_World_StandOn().GetComponent<IsoBlock>().Get_Block_Check_StairUD())
                //If Stand-On Stair Up Down
                {

                }
                else
                //If not Stand-On Stair Up Down
                {
                    return;
                }
            }
            else
                if (v3_Move_Dir == IsoClassDir.v3_Left_Y ||
                    v3_Move_Dir == IsoClassDir.v3_Right_Y)
            //If Move Left Right
            {
                if (cl_Move.Get_World_StandOn().GetComponent<IsoBlock>().Get_Block_Check_StairLR())
                //If Stand-On Stair Left Right
                {

                }
                else
                //If not Stand-On Stair Left Right
                {
                    return;
                }
            }
        }

        if (!cl_Move.Get_World_Emty(v3_Move_Dir, IsoClassDir.v3_Top_H))
        //If Move Top not Emty
        {
            if (cl_Move.Get_World_Current(v3_Move_Dir, IsoClassDir.v3_Top_H).GetComponent<IsoBlock>().Get_Block_Check_Stair())
            //If Move To Stair Top
            {
                if (v3_Move_Dir == IsoClassDir.v3_Up_X ||
                    v3_Move_Dir == IsoClassDir.v3_Down_X)
                //If Move Up Down
                {
                    if (cl_Move.Get_World_Current(v3_Move_Dir, IsoClassDir.v3_Top_H).GetComponent<IsoBlock>().Get_Block_Check_StairUD())
                    //If Move to Stair Top Up Down
                    {
                        if (cl_Move.Get_World_StandOn().GetComponent<IsoBlock>().Get_Block_Check_Ground())
                        //If Stand-On Ground
                        {
                            cl_Move.Set_Move(v3_Move_Dir, IsoClassDir.v3_Top_H, true);
                        }
                        else
                        //If not Stand-On Ground
                        {
                            cl_Move.Set_Move(v3_Move_Dir, IsoClassDir.v3_Top_H, false);
                        }
                    }
                    else
                    //If not Move to Stair Top Up Down
                    {
                        return;
                    }
                }
                else
                if (v3_Move_Dir == IsoClassDir.v3_Left_Y ||
                    v3_Move_Dir == IsoClassDir.v3_Right_Y)
                //If Move Left Right
                {
                    if (cl_Move.Get_World_Current(v3_Move_Dir, IsoClassDir.v3_Top_H).GetComponent<IsoBlock>().Get_Block_Check_StairLR())
                    //If Move to Stair Top Left Right
                    {
                        if (cl_Move.Get_World_StandOn().GetComponent<IsoBlock>().Get_Block_Check_Ground())
                        //If Stand-On Ground
                        {
                            cl_Move.Set_Move(v3_Move_Dir, IsoClassDir.v3_Top_H, true);
                        }
                        else
                        //If not Stand-On Ground
                        {
                            cl_Move.Set_Move(v3_Move_Dir, IsoClassDir.v3_Top_H, false);
                        }
                    }
                    else
                    //If not Move to Stair Top Left Right
                    {
                        return;
                    }
                }
            }
        }
        else
        if (!cl_Move.Get_World_Emty(v3_Move_Dir, IsoClassDir.v3_None))
        //If Move None not Emty
        {
            if (cl_Move.Get_World_Current(v3_Move_Dir, IsoClassDir.v3_None).GetComponent<IsoBlock>().Get_Block_Check_Stair())
            //If Move To Stair None
            {
                if (v3_Move_Dir == IsoClassDir.v3_Up_X ||
                    v3_Move_Dir == IsoClassDir.v3_Down_X)
                //If Move Up Down
                {
                    if (cl_Move.Get_World_Current(v3_Move_Dir, IsoClassDir.v3_None).GetComponent<IsoBlock>().Get_Block_Check_StairUD())
                    //If Move to Stair None Up Down
                    {
                        if (cl_Move.Get_World_StandOn().GetComponent<IsoBlock>().Get_Block_Check_Ground())
                        //If Stand-On Ground
                        {
                            cl_Move.Set_Move(v3_Move_Dir, IsoClassDir.v3_None, true);
                        }
                        else
                        //If not Stand-On Ground
                        {
                            cl_Move.Set_Move(v3_Move_Dir, IsoClassDir.v3_None, false);
                        }
                    }
                    else
                    //If not Move to Stair None Up Down
                    {
                        return;
                    }
                }
                else
                if (v3_Move_Dir == IsoClassDir.v3_Left_Y ||
                    v3_Move_Dir == IsoClassDir.v3_Right_Y)
                //If Move Left Right
                {
                    if (cl_Move.Get_World_Current(v3_Move_Dir, IsoClassDir.v3_None).GetComponent<IsoBlock>().Get_Block_Check_StairLR())
                    //If Move to Stair None Left Right
                    {
                        if (cl_Move.Get_World_StandOn().GetComponent<IsoBlock>().Get_Block_Check_Ground())
                        //If Stand-On Ground
                        {
                            cl_Move.Set_Move(v3_Move_Dir, IsoClassDir.v3_None, true);
                        }
                        else
                        //If not Stand-On Ground
                        {
                            cl_Move.Set_Move(v3_Move_Dir, IsoClassDir.v3_None, false);
                        }
                    }
                    else
                    //If not Move to Stair None Left Right
                    {
                        return;
                    }
                }
            }
            else
            if (cl_Move.Get_World_Current(v3_Move_Dir, IsoClassDir.v3_None).GetComponent<IsoBlock>().Get_Block_Check_Ground())
            //If Move To Ground None
            {
                cl_Move.Set_Move(v3_Move_Dir, IsoClassDir.v3_None, false);
            }
        }
        else
        if (!cl_Move.Get_World_Emty(v3_Move_Dir, IsoClassDir.v3_Bot_H))
        //If Move Bot not Emty
        {
            if (cl_Move.Get_World_Current(v3_Move_Dir, IsoClassDir.v3_Bot_H).GetComponent<IsoBlock>().Get_Block_Check_Stair())
            //If Move To Stair Bot (Mean Move to Stair Bot)
            {
                if (v3_Move_Dir == IsoClassDir.v3_Up_X ||
                    v3_Move_Dir == IsoClassDir.v3_Down_X)
                //If Move Up Down
                {
                    if (cl_Move.Get_World_Current(v3_Move_Dir, IsoClassDir.v3_Bot_H).GetComponent<IsoBlock>().Get_Block_Check_StairUD())
                    //If Move to Stair Bot Bot Up Down
                    {
                        cl_Move.Set_Move(v3_Move_Dir, IsoClassDir.v3_Bot_H, false);
                    }
                    else
                    //If not Move to Stair Bot Up Down
                    {
                        return;
                    }
                }
                else
                if (v3_Move_Dir == IsoClassDir.v3_Left_Y ||
                    v3_Move_Dir == IsoClassDir.v3_Right_Y)
                //If Move Left Right
                {
                    if (cl_Move.Get_World_Current(v3_Move_Dir, IsoClassDir.v3_Bot_H).GetComponent<IsoBlock>().Get_Block_Check_StairLR())
                    //If Move to Stair Bot Left Right
                    {
                        cl_Move.Set_Move(v3_Move_Dir, IsoClassDir.v3_Bot_H, false);
                    }
                    else
                    //If not Move to Stair Bot Left Right
                    {
                        return;
                    }
                }
            }
            else
            if (cl_Move.Get_World_Current(v3_Move_Dir, IsoClassDir.v3_Bot_H).GetComponent<IsoBlock>().Get_Block_Check_Ground())
            //If Move To Ground Bot
            {
                if (cl_Move.Get_World_StandOn().GetComponent<IsoBlock>().Get_Block_Check_Stair())
                //If Stand-On Stair
                {
                    cl_Move.Set_Move(v3_Move_Dir, IsoClassDir.v3_Bot_H, false);
                }
            }
        }
    }
}
