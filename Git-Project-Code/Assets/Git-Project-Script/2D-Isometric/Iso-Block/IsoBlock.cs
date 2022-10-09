using UnityEngine;

public class IsoBlock : MonoBehaviour
{
    #region Pos

    [Header("Pos Manager")]

    [Tooltip("Pos on World")]
    [SerializeField]
    private Vector3 m_Pos = new Vector3();

    [Header("Pos Matrix Manager")]

    [Tooltip("Round to Uper on Matrix")]
    [SerializeField]
    [Range(0.5f, 0.6f)]
    private float m_PosMatrixMax = 0.55f;

    [Tooltip("Round to Lower on Matrix")]
    [SerializeField]
    [Range(0.4f, 0.5f)]
    private float m_PosMatrixMin = 0.45f;

    [Tooltip("Pos Primary on Matrix")]
    private Vector3Int m_PosMatrixPrimary = new Vector3Int();

    [Tooltip("Pos on Matrix")]
    private Vector3Int m_PosMatrix = new Vector3Int();

    [Header("Pos Fix Manager")]

    [Tooltip("Fix Block Centre")]
    [SerializeField]
    private Vector3 m_Fix = new Vector3(0f, 0f, 0f);

    [Tooltip("Fix Block Distance")]
    [SerializeField]
    private Vector3 m_Square = new Vector3(1f, 1f, 1f);

    [Tooltip("Pos Block Offset")]
    [SerializeField]
    private Vector3 m_Offset = new Vector3(0f, 0f, 0f);

    #endregion

    private void FixedUpdate()
    {
        SetIsoTransform();
    }

    #region ================================================================== Iso

    private void SetIsoTransform()
    {
        //if (cs_World != null)
        //{
        //    m_Offset = cs_World.GetFix_Offset();
        //    m_Square = cs_World.GetFix_Square();
        //}

        Vector3 m_PosTransform = GetIsoScene(m_Pos);

        m_PosTransform.x *= m_Square.x;
        m_PosTransform.y *= m_Square.y;
        m_PosTransform.z *= m_Square.z;

        m_PosTransform += m_Offset;

        m_PosTransform += m_Fix;

        transform.position = m_PosTransform;

        SetIsoMatrix();
    }

    private void SetIsoMatrix()
    {
        float m_X = (Mathf.Abs((int)m_Pos.x - m_Pos.x));

        if (m_X > m_PosMatrixMax)
        {
            m_PosMatrix.x = (int)m_Pos.x + 1;
        }
        else
        if (m_X < m_PosMatrixMin)
        {
            m_PosMatrix.x = (int)m_Pos.x;
        }

        float m_Y = (Mathf.Abs((int)m_Pos.y - m_Pos.y));

        if (m_Y > m_PosMatrixMax)
        {
            m_PosMatrix.y = (int)m_Pos.y + 1;
        }
        else
        if (m_Y < m_PosMatrixMin)
        {
            m_PosMatrix.y = (int)m_Pos.y;
        }

        float m_H = (Mathf.Abs((int)m_Pos.z - m_Pos.z));

        if (m_H > m_PosMatrixMax)
        {
            m_PosMatrix.z = (int)m_Pos.z + 1;
        }
        else
        if (m_H < m_PosMatrixMin)
        {
            m_PosMatrix.z = (int)m_Pos.z;
        }
    }

    private Vector3 GetIsoScene(Vector3 m_PosWorld)
    {
        Vector3 m_FixTransform = new Vector3(
            m_PosWorld.x + m_PosWorld.y,
            0.5f * (m_PosWorld.y - m_PosWorld.x) + m_PosWorld.z,
            (m_PosWorld.y - m_PosWorld.x) - m_PosWorld.z);

        return m_FixTransform;
    }

    public void SetIsoFix(Vector3 m_Fix)
    {
        this.m_Fix = m_Fix;
    }

    #endregion

    #region ================================================================== Pos Set

    public void SetPos(Vector3 m_Pos)
    {
        this.m_Pos = m_Pos;

        SetIsoTransform();
    }

    public void SetPos(float m_X, float m_Y, float m_H)
    {
        SetPos(new Vector3(m_X, m_Y, m_H));
    }

    public void SetPosX(float m_X)
    {
        m_Pos.x = m_X;

        SetIsoTransform();
    }

    public void SetPosY(float m_Y)
    {
        m_Pos.y = m_Y;

        SetIsoTransform();
    }

    public void SetPosH(float m_H)
    {
        m_Pos.z = m_H;

        SetIsoTransform();
    }

    #endregion

    #region ================================================================== Pos Add 

    public void SetPosAdd(Vector3 m_PosAdd)
    {
        SetPos(GetPosCurrent() + m_PosAdd);
    }

    public void SetPosAdd(float m_AddX, float m_AddY, float m_AddH)
    {
        SetPosAdd(new Vector3(m_AddX, m_AddY, m_AddH));
    }

    public void SetPosAddX(float m_AddX)
    {
        m_Pos.x += m_AddX;

        SetIsoTransform();
    }

    public void SetPosAddY(float m_AddY)
    {
        m_Pos.y += m_AddY;

        SetIsoTransform();
    }

    public void SetPosAddH(float m_AddH)
    {
        m_Pos.z += m_AddH;

        SetIsoTransform();
    }

    #endregion

    #region ================================================================== Get Pos 

    public Vector3Int GetPosOnMatrixCurrent()
    {
        return m_PosMatrix;
    }

    public Vector3 GetPosCurrent()
    {
        return m_Pos;
    }

    public float GetPosCurrentX()
    {
        return GetPosCurrent().x;
    }

    public float GetPosCurrentY()
    {
        return GetPosCurrent().y;
    }

    public float GetPosCurrentH()
    {
        return GetPosCurrent().z;
    }

    #endregion

    #region ================================================================== Pos Primary

    public void SetPrimary(Vector3Int m_Pos)
    {
        m_PosMatrixPrimary = m_Pos;
    }

    public void SetPrimaryReset()
    {
        SetPos(GetPrimary());
    }

    public Vector3Int GetPrimary()
    {
        return m_PosMatrixPrimary;
    }

    public bool GetCheckPrimaryStay()
    {
        return GetPrimary() == GetPosOnMatrixCurrent();
    }

    #endregion
}

public enum IsoDir
{
    None    = 0,
    Up      = 1,
    Down    = 2,
    Left    = 3,
    Right   = 4,
    Top     = 5,
    Bot     = 6,
}

[System.Serializable]
public class IsoPos
{
    public float m_PosUDX = 0;
    public float m_PosLRY = 0;
    public float m_PosTBH = 0;
}

public class IsoDirVector
{
    public static readonly Vector3Int m_DirN  = new Vector3Int( 0,  0,  0);
    public static readonly Vector3Int m_DirUX = new Vector3Int(-1,  0,  0);
    public static readonly Vector3Int m_DirDX = new Vector3Int(+1,  0,  0);
    public static readonly Vector3Int m_DirLY = new Vector3Int( 0, -1,  0);
    public static readonly Vector3Int m_DirRY = new Vector3Int( 0, +1,  0);
    public static readonly Vector3Int m_DirTH = new Vector3Int( 0,  0, +1);
    public static readonly Vector3Int m_DirBH = new Vector3Int( 0,  0, -1);

    public static IsoDir GetDir(Vector3Int m_Dir)
    {
        if (m_Dir == m_DirUX)
        {
            return IsoDir.Up;
        }

        if (m_Dir == m_DirDX)
        {
            return IsoDir.Down;
        }

        if (m_Dir == m_DirLY)
        {
            return IsoDir.Left;
        }

        if (m_Dir == m_DirRY)
        {
            return IsoDir.Right;
        }

        if (m_Dir == m_DirTH)
        {
            return IsoDir.Top;
        }

        if (m_Dir == m_DirBH)
        {
            return IsoDir.Bot;
        }

        return IsoDir.None;
    }

    public static Vector3Int GetDir(IsoDir m_Dir)
    {
        if (m_Dir == IsoDir.Up)
        {
            return m_DirUX;
        }

        if (m_Dir == IsoDir.Down)
        {
            return m_DirDX;
        }

        if (m_Dir == IsoDir.Left)
        {
            return m_DirLY;
        }

        if (m_Dir == IsoDir.Right)
        {
            return m_DirRY;
        }

        if (m_Dir == IsoDir.Top)
        {
            return m_DirTH;
        }

        if (m_Dir == IsoDir.Bot)
        {
            return m_DirBH;
        }

        return m_DirN;
    }

}