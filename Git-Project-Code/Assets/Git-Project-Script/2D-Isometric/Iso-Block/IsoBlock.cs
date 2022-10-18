using UnityEngine;

#if UNITY_EDITOR

[ExecuteAlways]

#endif

public class IsoBlock : MonoBehaviour
{
    [Header("World Manager")]

    [SerializeField] private IsoVector m_Pos = new IsoVector();

    [SerializeField] private Vector3 m_Centre = new Vector3(0f, 0f, 0f);

    [SerializeField] private IsoVector m_Scale = new IsoVector(1f, 1f, 1f);

    [Header("Matrix Manager")]

    [SerializeField] private IsoVector m_PosMatrix = new IsoVector();

    [SerializeField] [Range(0.5f, 0.6f)] private float m_RoundUpper = 0.55f;

    [SerializeField] [Range(0.4f, 0.5f)] private float m_RoundLower = 0.45f;

    private IsoVector m_PosPrimary = new IsoVector();

    [Header("Block Manager")]

    private IsoBlockImformation m_Imformation;

#if UNITY_EDITOR

    private void Update()
    {
        SetIsoTransform();
    }

#endif

    #region ================================================================== Iso

    private Vector3 GetIsoScene(IsoVector m_PosWorld)
    {
        float m_PosX = m_PosWorld.m_UDX + m_PosWorld.m_LRY;
        float m_PosY = 0.5f * (m_PosWorld.m_LRY - m_PosWorld.m_UDX) + m_PosWorld.m_TBH;
        float m_Z = (m_PosWorld.m_LRY - m_PosWorld.m_UDX) - m_PosWorld.m_TBH;

        return new Vector3(m_PosX, m_PosY, m_Z);
    }

    private void SetIsoTransform()
    {
        //if (m_World != null)
        //{
        //    m_Offset = m_World.GetFix_Offset();
        //    m_Square = m_World.GetFix_Square();
        //}

        Vector3 m_PosTransform = GetIsoScene(m_Pos);

        m_PosTransform.x *= m_Scale.m_UDX;
        m_PosTransform.y *= m_Scale.m_LRY;
        m_PosTransform.z *= m_Scale.m_TBH;

        m_PosTransform += m_Centre;

        transform.position = m_PosTransform;

        SetIsoMatrix();
    }

    private void SetIsoMatrix()
    {
        float m_PosX = (Mathf.Abs((int)m_Pos.m_UDX - m_Pos.m_UDX));

        if (m_PosX > m_RoundUpper)
        {
            m_PosMatrix.m_UDX = (int)m_Pos.m_UDX + 1;
        }
        else
        if (m_PosX < m_RoundLower)
        {
            m_PosMatrix.m_UDX = (int)m_Pos.m_UDX;
        }

        float m_PosY = (Mathf.Abs((int)m_Pos.m_LRY - m_Pos.m_LRY));

        if (m_PosY > m_RoundUpper)
        {
            m_PosMatrix.m_LRY = (int)m_Pos.m_LRY + 1;
        }
        else
        if (m_PosY < m_RoundLower)
        {
            m_PosMatrix.m_LRY = (int)m_Pos.m_LRY;
        }

        float m_PosH = (Mathf.Abs((int)m_Pos.m_TBH - m_Pos.m_TBH));

        if (m_PosH > m_RoundUpper)
        {
            m_PosMatrix.m_TBH = (int)m_Pos.m_TBH + 1;
        }
        else
        if (m_PosH < m_RoundLower)
        {
            m_PosMatrix.m_TBH = (int)m_Pos.m_TBH;
        }
    }

    public void SetScale(IsoVector m_Scale)
    {
        this.m_Scale = m_Scale;
    }

    public void SetFix(Vector3 m_Fix)
    {
        this.m_Centre = m_Fix;
    }

    #endregion

    #region ================================================================== Pos Set

    public void SetPos(IsoVector m_Pos)
    {
        this.m_Pos.SetVector(m_Pos);

        SetIsoTransform();
    }

    public void SetPos(float m_PosX, float m_PosY, float m_PosH)
    {
        this.m_Pos.SetVector(m_PosX, m_PosY, m_PosH);

        SetIsoTransform();
    }

    #endregion

    #region ================================================================== Pos Add 

    public void SetPosAdd(IsoVector m_PosAdd)
    {
        m_Pos.SetVectorAdd(m_PosAdd);

        SetIsoTransform();
    }

    public void SetPosAdd(float m_PosAddX, float m_PosAddY, float m_PosAddH)
    {
        m_Pos.SetVectorAdd(m_PosAddX, m_PosAddY, m_PosAddH);

        SetIsoTransform();
    }

    #endregion

    #region ================================================================== Pos Primary

    public void SetPosPrimary(IsoVector m_Pos)
    {
        m_PosPrimary = m_Pos;
    }

    public void SetPosPrimaryReset()
    {
        SetPos(GetPosPrimary());
    }

    #endregion

    #region ================================================================== Get Pos 

    public IsoVector GetPosCurrent()
    {
        return m_Pos;
    }

    public IsoVector GetPosMatrixCurrent()
    {
        return m_PosMatrix;
    }

    public IsoVector GetPosPrimary()
    {
        return m_PosPrimary;
    }

    public bool GetPosPrimaryStay()
    {
        IsoVector m_PosPrimary = GetPosPrimary();
        IsoVector m_PosMatrixCurrent = GetPosMatrixCurrent();

        return m_PosPrimary.m_UDX == m_PosMatrixCurrent.m_UDX && m_PosPrimary.m_LRY == m_PosMatrixCurrent.m_LRY && m_PosPrimary.m_TBH == m_PosMatrixCurrent.m_TBH;
    }

    #endregion

    #region ================================================================== Imformation

    public void SetImformation(IsoBlockImformation m_BlockImformation)
    {
        this.m_Imformation = m_BlockImformation;
    }

    public IsoBlockImformation GetImformation()
    {
        return this.m_Imformation;
    }

    #endregion
}

public enum IsoDir
{
    None    = 0,
    //X
    Up      = 1,
    Down    = 2,
    //Y
    Left    = 3,
    Right   = 4,
    //H
    Top     = 5,
    Bot     = 6,
}

[System.Serializable]
public class IsoVector
{
    public float m_UDX = 0;

    public int m_UDXInt
    {
        get
        {
            return (int)m_UDX;
        }
    }

    public float m_LRY = 0;

    public int m_LRYInt
    {
        get
        {
            return (int)m_LRY;
        }
    }

    public float m_TBH = 0;

    public int m_TBHInt
    {
        get
        {
            return (int)m_TBH;
        }
    }

    public IsoVector()
    {
        this.m_UDX = 0;
        this.m_LRY = 0;
        this.m_TBH = 0;
    }

    public IsoVector(IsoVector m_Vector)
    {
        SetVector(m_Vector);
    }

    public IsoVector(float m_UDX, float m_LRY, float m_TBH)
    {
        SetVector(m_UDX, m_LRY, m_TBH);
    }

    public void SetVector(IsoVector m_Vector)
    {
        this.m_UDX = m_Vector.m_UDX;
        this.m_LRY = m_Vector.m_LRY;
        this.m_TBH = m_Vector.m_TBH;
    }

    public void SetVector(float m_UDX, float m_LRY, float m_TBH)
    {
        this.m_UDX = m_UDX;
        this.m_LRY = m_LRY;
        this.m_TBH = m_TBH;
    }

    public void SetVectorAdd(IsoVector m_Vector)
    {
        this.m_UDX += m_Vector.m_UDX;
        this.m_LRY += m_Vector.m_LRY;
        this.m_TBH += m_Vector.m_TBH;
    }

    public void SetVectorAdd(float m_UDX, float m_LRY, float m_TBH)
    {
        this.m_UDX += m_UDX;
        this.m_LRY += m_LRY;
        this.m_TBH += m_TBH;
    }

    public IsoVector GetVector()
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