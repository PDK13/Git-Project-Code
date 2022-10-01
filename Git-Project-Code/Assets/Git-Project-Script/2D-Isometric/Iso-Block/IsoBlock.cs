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
        SetPosTransform();
    }

    #region Pos 

    #region Pos main 

    private void SetPosTransform()
    {
        //if (cs_World != null)
        //{
        //    m_Offset = cs_World.GetFix_Offset();
        //    m_Square = cs_World.GetFix_Square();
        //}

        Vector3 m_PosTransform = GetPosScene(m_Pos);

        m_PosTransform.x *= m_Square.x;
        m_PosTransform.y *= m_Square.y;
        m_PosTransform.z *= m_Square.z;

        m_PosTransform += m_Offset;

        m_PosTransform += m_Fix;

        transform.position = m_PosTransform;

        SetPosMatrix();
    }

    private void SetPosMatrix()
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

    public static Vector3 GetPosScene(Vector3 m_PosWorld)
    {
        Vector3 m_FixTransform = new Vector3(
            m_PosWorld.x + m_PosWorld.y,
            0.5f * (m_PosWorld.y - m_PosWorld.x) + m_PosWorld.z,
            (m_PosWorld.y - m_PosWorld.x) - m_PosWorld.z);

        return m_FixTransform;
    }

    public static Vector2 GetPosWorld(Vector2 m_PosScene)
    {
        float m_YWorld = m_PosScene.y + m_PosScene.x / 2;

        float m_XWorld = m_PosScene.x - m_YWorld;

        return new Vector3(m_XWorld, m_YWorld, 0);
    }

    #endregion

    #region Pos on Matrix Float 

    #region Set Pos 

    public void SetPos(Vector3 m_Pos)
    {
        this.m_Pos = m_Pos;

        SetPosTransform();
    }

    public void SetPos(float m_X, float m_Y, float m_H)
    {
        SetPos(new Vector3(m_X, m_Y, m_H));
    }

    public void SetPosX(float m_X)
    {
        m_Pos.x = m_X;

        SetPosTransform();
    }

    public void SetPosY(float m_Y)
    {
        m_Pos.y = m_Y;

        SetPosTransform();
    }

    public void SetPosH(float m_H)
    {
        m_Pos.z = m_H;

        SetPosTransform();
    }

    #endregion

    #region Set Pos Add 

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

        SetPosTransform();
    }

    public void SetPosAddY(float m_AddY)
    {
        m_Pos.y += m_AddY;

        SetPosTransform();
    }

    public void SetPosAddH(float m_AddH)
    {
        m_Pos.z += m_AddH;

        SetPosTransform();
    }

    #endregion

    #region Get Pos 

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

    #endregion

    #region Pos on Matrix Int 

    public Vector3Int GetPosOnMatrixCurrent()
    {
        return m_PosMatrix;
    }

    #region Pos on Matrix Primary 

    public void SetPosOnMatrixPrimary(Vector3Int m_Pos)
    {
        m_PosMatrixPrimary = m_Pos;
    }

    public Vector3Int GetPosOnMatrixPrimary()
    {
        return m_PosMatrixPrimary;
    }

    /// <summary>
    /// Check Pos on Matrix Current same on Pos on Matrix Primary
    /// </summary>
    /// <returns></returns>
    public bool GetCheckPosMatrixStayPrimary()
    {
        return GetPosOnMatrixPrimary() == GetPosOnMatrixCurrent();
    }

    /// <summary>
    /// Reset Pos Current to Pos on Matrix Primary
    /// </summary>
    public void SetPosMatrixResetPrimary()
    {
        SetPos(GetPosOnMatrixPrimary());
    }

    #endregion

    #endregion

    public void SetFix(Vector3 m_Fix)
    {
        this.m_Fix = m_Fix;
    }

    #endregion
}

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