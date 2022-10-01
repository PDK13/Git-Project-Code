using UnityEngine;

public class IsoClassDir
{
    /// <summary>
    /// Dir(0, 0, 0) on ometric Block
    /// </summary>
    public static readonly Vector3Int m_DirNone = new Vector3Int(0, 0, 0);

    /// <summary>
    /// Dir(0, 0, 0) on ometric Block
    /// </summary>
    public static readonly string m_None = "N";

    /// <summary>
    /// Dir(-1, 0, 0) on ometric Block
    /// </summary>
    public static readonly Vector3Int m_DirUX = new Vector3Int(-1, 0, 0);

    /// <summary>
    /// Dir(-1, 0, 0) on ometric Block
    /// </summary>
    public static readonly string m_UX = "U";

    /// <summary>
    /// Dir(+1, 0, 0) on ometric Block
    /// </summary>
    /// <returns></returns>
    public static readonly Vector3Int m_DirDX = new Vector3Int(1, 0, 0);

    /// <summary>
    /// Dir(+1, 0, 0) on ometric Block
    /// </summary>
    /// <returns></returns>
    public static readonly string m_DX = "D";

    /// <summary>
    /// Dir(+1, 0, 0) on ometric Block
    /// </summary>
    /// <returns></returns>
    public static readonly Vector3Int m_DirLY = new Vector3Int(0, -1, 0);

    /// <summary>
    /// Dir(0, -1, 0) on ometric Block
    /// </summary>
    public static readonly string m_LY = "L";

    /// <summary>
    /// Dir(0, +1, 0) on ometric Block
    /// </summary>
    public static readonly Vector3Int m_DirRY = new Vector3Int(0, 1, 0);

    /// <summary>
    /// Dir(0, +1, 0) on ometric Block
    /// </summary>
    public static readonly string m_RY = "R";

    /// <summary>
    /// Dir(0, 0, +1) on ometric Block
    /// </summary>
    public static readonly Vector3Int m_DirTH = new Vector3Int(0, 0, 1);

    /// <summary>
    /// Dir(0, 0, +1) on ometric Block
    /// </summary>
    public static readonly string m_TH = "T";

    /// <summary>
    /// Dir(0, 0, -1) on ometric Block
    /// </summary>
    public static readonly Vector3Int m_DirBH = new Vector3Int(0, 0, -1);

    /// <summary>
    /// Dir(0, 0, -1) on ometric Block
    /// </summary>
    public static readonly string m_BH = "B";

    public static string GetDirEncypt(Vector3Int m_Dir)
    {
        if (m_Dir == IsoClassDir.m_DirUX)
        {
            return m_UX;
        }

        if (m_Dir == IsoClassDir.m_DirDX)
        {
            return m_DX;
        }

        if (m_Dir == IsoClassDir.m_DirLY)
        {
            return m_LY;
        }

        if (m_Dir == IsoClassDir.m_DirRY)
        {
            return m_RY;
        }

        if (m_Dir == IsoClassDir.m_DirTH)
        {
            return m_TH;
        }

        if (m_Dir == IsoClassDir.m_DirBH)
        {
            return m_BH;
        }

        return m_None;
    }

    public static Vector3Int GetDirDencyt(string m_Dir)
    {
        if (m_Dir == m_UX)
        {
            return IsoClassDir.m_DirUX;
        }

        if (m_Dir == m_DX)
        {
            return IsoClassDir.m_DirDX;
        }

        if (m_Dir == m_LY)
        {
            return IsoClassDir.m_DirLY;
        }

        if (m_Dir == m_RY)
        {
            return IsoClassDir.m_DirRY;
        }

        if (m_Dir == m_BH)
        {
            return IsoClassDir.m_DirBH;
        }

        if (m_Dir == m_TH)
        {
            return IsoClassDir.m_DirTH;
        }

        return new Vector3Int();
    }

    public static Vector3 GetVectorOne(Vector3 m_Vector)
    {
        Vector3 m_VectorChance = m_Vector;

        if (m_VectorChance.x != 0)
        {
            m_VectorChance.x /= Mathf.Abs(m_VectorChance.x);
        }

        if (m_VectorChance.y != 0)
        {
            m_VectorChance.y /= Mathf.Abs(m_VectorChance.y);
        }

        if (m_VectorChance.z != 0)
        {
            m_VectorChance.z /= Mathf.Abs(m_VectorChance.z);
        }

        return m_VectorChance;
    }
}
