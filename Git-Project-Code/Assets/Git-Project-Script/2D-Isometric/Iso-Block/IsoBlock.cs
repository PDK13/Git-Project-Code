using UnityEngine;

#if UNITY_EDITOR

[ExecuteAlways]

#endif

public class IsoBlock : MonoBehaviour
{
    [Header("Pos Manager")]

    [Tooltip("Pos on World")]

    [SerializeField] private IsoPos m_Pos = new IsoPos();

    [SerializeField] private Vector3 m_Centre = new Vector3(0f, 0f, 0f);

    [Header("Pos on Matrix")]

    [SerializeField] private IsoPos m_PosMatrix = new IsoPos();

    [SerializeField] [Range(0.5f, 0.6f)] private float m_RoundUpper = 0.55f;

    [SerializeField] [Range(0.4f, 0.5f)] private float m_RoundLower = 0.45f;

    [SerializeField] private Vector3 m_Scale = new Vector3(1f, 1f, 1f);

    private IsoPos m_PosPrimary = new IsoPos();

#if UNITY_EDITOR

    private void FixedUpdate()
    {
        SetIsoTransform();
    }

#endif

    #region ================================================================== Iso

    private Vector3 GetIsoScene(IsoPos m_PosWorld)
    {
        float m_PosX = m_PosWorld.m_PosUDX + m_PosWorld.m_PosLRY;
        float m_PosY = 0.5f * (m_PosWorld.m_PosLRY - m_PosWorld.m_PosUDX) + m_PosWorld.m_PosTBH;
        float m_Z = (m_PosWorld.m_PosLRY - m_PosWorld.m_PosUDX) - m_PosWorld.m_PosTBH;

        return new Vector3(m_PosX, m_PosY, m_Z);
    }

    private void SetIsoTransform()
    {
        //if (cs_World != null)
        //{
        //    m_Offset = cs_World.GetFix_Offset();
        //    m_Square = cs_World.GetFix_Square();
        //}

        Vector3 m_PosTransform = GetIsoScene(m_Pos);

        m_PosTransform.x *= m_Scale.x;
        m_PosTransform.y *= m_Scale.y;
        m_PosTransform.z *= m_Scale.z;

        m_PosTransform += m_Centre;

        transform.position = m_PosTransform;

        SetIsoMatrix();
    }

    private void SetIsoMatrix()
    {
        float m_PosX = (Mathf.Abs((int)m_Pos.m_PosUDX - m_Pos.m_PosUDX));

        if (m_PosX > m_RoundUpper)
        {
            m_PosMatrix.m_PosUDX = (int)m_Pos.m_PosUDX + 1;
        }
        else
        if (m_PosX < m_RoundLower)
        {
            m_PosMatrix.m_PosUDX = (int)m_Pos.m_PosUDX;
        }

        float m_PosY = (Mathf.Abs((int)m_Pos.m_PosLRY - m_Pos.m_PosLRY));

        if (m_PosY > m_RoundUpper)
        {
            m_PosMatrix.m_PosLRY = (int)m_Pos.m_PosLRY + 1;
        }
        else
        if (m_PosY < m_RoundLower)
        {
            m_PosMatrix.m_PosLRY = (int)m_Pos.m_PosLRY;
        }

        float m_PosH = (Mathf.Abs((int)m_Pos.m_PosTBH - m_Pos.m_PosTBH));

        if (m_PosH > m_RoundUpper)
        {
            m_PosMatrix.m_PosTBH = (int)m_Pos.m_PosTBH + 1;
        }
        else
        if (m_PosH < m_RoundLower)
        {
            m_PosMatrix.m_PosTBH = (int)m_Pos.m_PosTBH;
        }
    }

    public void SetIsoFix(Vector3 m_Fix)
    {
        this.m_Centre = m_Fix;
    }

    #endregion

    #region ================================================================== Pos Set

    public void SetPos(IsoPos m_Pos)
    {
        this.m_Pos.SetPos(m_Pos);

        SetIsoTransform();
    }

    public void SetPos(float m_PosX, float m_PosY, float m_PosH)
    {
        this.m_Pos.SetPos(m_PosX, m_PosY, m_PosH);

        SetIsoTransform();
    }

    #endregion

    #region ================================================================== Pos Add 

    public void SetPosAdd(IsoPos m_PosAdd)
    {
        m_Pos.SetPosAdd(m_PosAdd);

        SetIsoTransform();
    }

    public void SetPosAdd(float m_PosAddX, float m_PosAddY, float m_PosAddH)
    {
        m_Pos.SetPosAdd(m_PosAddX, m_PosAddY, m_PosAddH);

        SetIsoTransform();
    }

    #endregion

    #region ================================================================== Pos Primary

    public void SetPosPrimary(IsoPos m_Pos)
    {
        m_PosPrimary = m_Pos;
    }

    public void SetPosPrimaryReset()
    {
        SetPos(GetPosPrimary());
    }

    #endregion

    #region ================================================================== Get Pos 

    public IsoPos GetPosCurrent()
    {
        return m_Pos;
    }

    public IsoPos GetPosMatrixCurrent()
    {
        return m_PosMatrix;
    }

    public IsoPos GetPosPrimary()
    {
        return m_PosPrimary;
    }

    public bool GetCheckPosPrimaryStay()
    {
        IsoPos m_PosPrimary = GetPosPrimary();
        IsoPos m_PosMatrixCurrent = GetPosMatrixCurrent();

        return m_PosPrimary.m_PosUDX == m_PosMatrixCurrent.m_PosUDX && m_PosPrimary.m_PosLRY == m_PosMatrixCurrent.m_PosLRY && m_PosPrimary.m_PosTBH == m_PosMatrixCurrent.m_PosTBH;
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

    public IsoPos()
    {
        this.m_PosUDX = 0;
        this.m_PosLRY = 0;
        this.m_PosTBH = 0;
    }

    public IsoPos(IsoPos m_Pos)
    {
        SetPos(m_Pos);
    }

    public IsoPos(float m_PosUDX, float m_PosLRY, float m_PosTBH)
    {
        SetPos(m_PosUDX, m_PosLRY, m_PosTBH);
    }

    public void SetPos(IsoPos m_Pos)
    {
        this.m_PosUDX = m_Pos.m_PosUDX;
        this.m_PosLRY = m_Pos.m_PosLRY;
        this.m_PosTBH = m_Pos.m_PosTBH;
    }

    public void SetPos(float m_PosUDX, float m_PosLRY, float m_PosTBH)
    {
        this.m_PosUDX = m_PosUDX;
        this.m_PosLRY = m_PosLRY;
        this.m_PosTBH = m_PosTBH;
    }

    public void SetPosAdd(IsoPos m_Pos)
    {
        this.m_PosUDX += m_Pos.m_PosUDX;
        this.m_PosLRY += m_Pos.m_PosLRY;
        this.m_PosTBH += m_Pos.m_PosTBH;
    }

    public void SetPosAdd(float m_PosUDX, float m_PosLRY, float m_PosTBH)
    {
        this.m_PosUDX += m_PosUDX;
        this.m_PosLRY += m_PosLRY;
        this.m_PosTBH += m_PosTBH;
    }

    public IsoPos GetPos()
    {
        return this;
    }
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