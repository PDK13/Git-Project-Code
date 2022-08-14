using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsoClassDir
{
    /// <summary>
    /// Dir(0, 0, 0) on Isometric Block
    /// </summary>
    public static readonly Vector3Int v3_None = new Vector3Int(0, 0, 0);

    /// <summary>
    /// Dir(0, 0, 0) on Isometric Block
    /// </summary>
    public readonly static string s_None = "N";

    /// <summary>
    /// Dir(-1, 0, 0) on Isometric Block
    /// </summary>
    public static readonly Vector3Int v3_Up_X = new Vector3Int(-1, 0, 0);

    /// <summary>
    /// Dir(-1, 0, 0) on Isometric Block
    /// </summary>
    public readonly static string s_Up_X = "U";

    /// <summary>
    /// Dir(+1, 0, 0) on Isometric Block
    /// </summary>
    /// <returns></returns>
    public static readonly Vector3Int v3_Down_X = new Vector3Int(1, 0, 0);

    /// <summary>
    /// Dir(+1, 0, 0) on Isometric Block
    /// </summary>
    /// <returns></returns>
    public readonly static string s_Down_X = "D";

    /// <summary>
    /// Dir(+1, 0, 0) on Isometric Block
    /// </summary>
    /// <returns></returns>
    public static readonly Vector3Int v3_Left_Y = new Vector3Int(0, -1, 0);

    /// <summary>
    /// Dir(0, -1, 0) on Isometric Block
    /// </summary>
    public readonly static string s_Left_Y = "L";

    /// <summary>
    /// Dir(0, +1, 0) on Isometric Block
    /// </summary>
    public static readonly Vector3Int v3_Right_Y = new Vector3Int(0, 1, 0);

    /// <summary>
    /// Dir(0, +1, 0) on Isometric Block
    /// </summary>
    public readonly static string s_Right_Y = "R";

    /// <summary>
    /// Dir(0, 0, +1) on Isometric Block
    /// </summary>
    public static readonly Vector3Int v3_Top_H = new Vector3Int(0, 0, 1);

    /// <summary>
    /// Dir(0, 0, +1) on Isometric Block
    /// </summary>
    public readonly static string s_Top_H = "T";

    /// <summary>
    /// Dir(0, 0, -1) on Isometric Block
    /// </summary>
    public static readonly Vector3Int v3_Bot_H = new Vector3Int(0, 0, -1);

    /// <summary>
    /// Dir(0, 0, -1) on Isometric Block
    /// </summary>
    public readonly static string s_Bot_H = "B";

    public static string Get_Dir_Encypt(Vector3Int v3_Dir)
    {
        if (v3_Dir == IsoClassDir.v3_Up_X)
        {
            return s_Up_X;
        }

        if (v3_Dir == IsoClassDir.v3_Down_X)
        {
            return s_Down_X;
        }

        if (v3_Dir == IsoClassDir.v3_Left_Y)
        {
            return s_Left_Y;
        }

        if (v3_Dir == IsoClassDir.v3_Right_Y)
        {
            return s_Right_Y;
        }

        if (v3_Dir == IsoClassDir.v3_Top_H)
        {
            return s_Top_H;
        }

        if (v3_Dir == IsoClassDir.v3_Bot_H)
        {
            return s_Bot_H;
        }

        return s_None;
    }

    public static Vector3Int Get_Dir_Dencyt(string s_Dir)
    {
        if (s_Dir == s_Up_X)
        {
            return IsoClassDir.v3_Up_X;
        }

        if (s_Dir == s_Down_X)
        {
            return IsoClassDir.v3_Down_X;
        }

        if (s_Dir == s_Left_Y)
        {
            return IsoClassDir.v3_Left_Y;
        }

        if (s_Dir == s_Right_Y)
        {
            return IsoClassDir.v3_Right_Y;
        }

        if (s_Dir == s_Bot_H)
        {
            return IsoClassDir.v3_Bot_H;
        }

        if (s_Dir == s_Top_H)
        {
            return IsoClassDir.v3_Top_H;
        }

        return new Vector3Int();
    }

    public static Vector3 Get_Vector_One(Vector3 v3_Vector)
    {
        Vector3 v3_Vector_Chance = v3_Vector;

        if (v3_Vector_Chance.x != 0)
            v3_Vector_Chance.x /= Mathf.Abs(v3_Vector_Chance.x);

        if (v3_Vector_Chance.y != 0)
            v3_Vector_Chance.y /= Mathf.Abs(v3_Vector_Chance.y);

        if (v3_Vector_Chance.z != 0)
            v3_Vector_Chance.z /= Mathf.Abs(v3_Vector_Chance.z);

        return v3_Vector_Chance;
    }
}
