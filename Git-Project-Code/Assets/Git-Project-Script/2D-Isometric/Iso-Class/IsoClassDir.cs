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
    public static readonly string m_None = "N";

    /// <summary>
    /// Dir(-1, 0, 0) on Isometric Block
    /// </summary>
    public static readonly Vector3Int v3_Up_X = new Vector3Int(-1, 0, 0);

    /// <summary>
    /// Dir(-1, 0, 0) on Isometric Block
    /// </summary>
    public static readonly string m_Up_X = "U";

    /// <summary>
    /// Dir(+1, 0, 0) on Isometric Block
    /// </summary>
    /// <returns></returns>
    public static readonly Vector3Int v3_Down_X = new Vector3Int(1, 0, 0);

    /// <summary>
    /// Dir(+1, 0, 0) on Isometric Block
    /// </summary>
    /// <returns></returns>
    public static readonly string m_Down_X = "D";

    /// <summary>
    /// Dir(+1, 0, 0) on Isometric Block
    /// </summary>
    /// <returns></returns>
    public static readonly Vector3Int v3_Left_Y = new Vector3Int(0, -1, 0);

    /// <summary>
    /// Dir(0, -1, 0) on Isometric Block
    /// </summary>
    public static readonly string m_Left_Y = "L";

    /// <summary>
    /// Dir(0, +1, 0) on Isometric Block
    /// </summary>
    public static readonly Vector3Int v3_Right_Y = new Vector3Int(0, 1, 0);

    /// <summary>
    /// Dir(0, +1, 0) on Isometric Block
    /// </summary>
    public static readonly string m_Right_Y = "R";

    /// <summary>
    /// Dir(0, 0, +1) on Isometric Block
    /// </summary>
    public static readonly Vector3Int v3_Top_H = new Vector3Int(0, 0, 1);

    /// <summary>
    /// Dir(0, 0, +1) on Isometric Block
    /// </summary>
    public static readonly string m_Top_H = "T";

    /// <summary>
    /// Dir(0, 0, -1) on Isometric Block
    /// </summary>
    public static readonly Vector3Int v3_Bot_H = new Vector3Int(0, 0, -1);

    /// <summary>
    /// Dir(0, 0, -1) on Isometric Block
    /// </summary>
    public static readonly string m_Bot_H = "B";

    public static string GetDirEncypt(Vector3Int v3_Dir)
    {
        if (v3_Dir == IsoClassDir.v3_Up_X)
        {
            return m_Up_X;
        }

        if (v3_Dir == IsoClassDir.v3_Down_X)
        {
            return m_Down_X;
        }

        if (v3_Dir == IsoClassDir.v3_Left_Y)
        {
            return m_Left_Y;
        }

        if (v3_Dir == IsoClassDir.v3_Right_Y)
        {
            return m_Right_Y;
        }

        if (v3_Dir == IsoClassDir.v3_Top_H)
        {
            return m_Top_H;
        }

        if (v3_Dir == IsoClassDir.v3_Bot_H)
        {
            return m_Bot_H;
        }

        return m_None;
    }

    public static Vector3Int GetDir_Dencyt(string m_Dir)
    {
        if (m_Dir == m_Up_X)
        {
            return IsoClassDir.v3_Up_X;
        }

        if (m_Dir == m_Down_X)
        {
            return IsoClassDir.v3_Down_X;
        }

        if (m_Dir == m_Left_Y)
        {
            return IsoClassDir.v3_Left_Y;
        }

        if (m_Dir == m_Right_Y)
        {
            return IsoClassDir.v3_Right_Y;
        }

        if (m_Dir == m_Bot_H)
        {
            return IsoClassDir.v3_Bot_H;
        }

        if (m_Dir == m_Top_H)
        {
            return IsoClassDir.v3_Top_H;
        }

        return new Vector3Int();
    }

    public static Vector3 GetVector_One(Vector3 v3Vector)
    {
        Vector3 v3Vector_Chance = v3Vector;

        if (v3Vector_Chance.x != 0)
        {
            v3Vector_Chance.x /= Mathf.Abs(v3Vector_Chance.x);
        }

        if (v3Vector_Chance.y != 0)
        {
            v3Vector_Chance.y /= Mathf.Abs(v3Vector_Chance.y);
        }

        if (v3Vector_Chance.z != 0)
        {
            v3Vector_Chance.z /= Mathf.Abs(v3Vector_Chance.z);
        }

        return v3Vector_Chance;
    }
}
