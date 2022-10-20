using UnityEngine;

#if UNITY_EDITOR

[ExecuteAlways]

#endif

public class IsoBlock : MonoBehaviour
{
    [Header("World Manager")]

    [SerializeField] private IsoDepth m_Depth = IsoDepth.Fixed;

    [SerializeField] private IsoVector m_Pos = new IsoVector();

    [SerializeField] private IsoVector m_Scale = new IsoVector(1f, 1f, 1f);

    [SerializeField] private Vector3 m_Centre = new Vector3(0f, 0f, 0f);

    [Header("Matrix Manager")]

    [SerializeField] private IsoVector m_PosMatrix = new IsoVector();

    [SerializeField] [Range(0.5f, 0.6f)] private float m_RoundUpper = 0.55f;

    [SerializeField] [Range(0.4f, 0.5f)] private float m_RoundLower = 0.45f;

    private IsoVector m_PosPrimary = new IsoVector();

    //[Header("Block Manager")]

    //private IsoBlockImformation m_Imformation;

#if UNITY_EDITOR

    private void Update()
    {
        SetIsoTransform();
    }

#endif

    #region ================================================================== Iso

    public void SetDepth(IsoDepth m_Depth)
    {
        this.m_Depth = m_Depth;
    }

    private Vector3 GetIsoScene(IsoVector m_PosWorld)
    {
        switch (m_Depth)
        {
            case IsoDepth.Fixed:
                IsoVector m_PosWorldScale = new IsoVector(m_PosWorld);
                m_PosWorldScale.m_UDX *= m_Scale.m_UDX;
                m_PosWorldScale.m_LRY *= m_Scale.m_LRY;
                m_PosWorldScale.m_TBH *= m_Scale.m_TBH;

                float m_PosX = m_PosWorldScale.m_UDX + m_PosWorldScale.m_LRY;
                float m_PosY = 0.5f * (m_PosWorldScale.m_LRY - m_PosWorldScale.m_UDX) + m_PosWorldScale.m_TBH;
                float m_PosZ = (m_PosWorldScale.m_LRY - m_PosWorldScale.m_UDX) - m_PosWorldScale.m_TBH;

                return new Vector3(m_PosX, m_PosY, m_PosZ);
        }

        return new Vector3(0, 0, 0);
    }

    private void SetIsoTransform()
    {
        //if (m_World != null)
        //{
        //    m_Offset = m_World.GetFix_Offset();
        //    m_Square = m_World.GetFix_Square();
        //}

        Vector3 m_PosTransform = GetIsoScene(m_Pos);

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

    //public void SetImformation(IsoBlockImformation m_BlockImformation)
    //{
    //    this.m_Imformation = m_BlockImformation;
    //}

    //public IsoBlockImformation GetImformation()
    //{
    //    return this.m_Imformation;
    //}

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

public enum IsoDepth
{
    Fixed,
}

[System.Serializable]
public class IsoVector
{
    public float m_UDX = 0;

    public float m_LRY = 0;

    public float m_TBH = 0;

    public int m_UDXInt
    {
        get
        {
            return (int)m_UDX;
        }
    }

    public int m_LRYInt
    {
        get
        {
            return (int)m_LRY;
        }
    }

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

    public bool GetVectorEqual(IsoVector m_Vector)
    {
        if (this.m_UDX != m_Vector.m_UDX)
        {
            return false;
        }

        if (this.m_LRY != m_Vector.m_LRY)
        {
            return false;
        }

        if (this.m_TBH != m_Vector.m_TBH)
        {
            return false;
        }

        return true;
    }

    public IsoVector GetVectorAdd(IsoVector m_Vector)
    {
        IsoVector m_VectorNew = new IsoVector(this);
        m_VectorNew.SetVectorAdd(m_Vector);
        return m_VectorNew;
    }

    public IsoVector GetVectorAdd(float m_UDX, float m_LRY, float m_TBH)
    {
        IsoVector m_VectorNew = new IsoVector(this);
        m_VectorNew.SetVectorAdd(m_UDX, m_LRY, m_TBH);
        return m_VectorNew;
    }
}