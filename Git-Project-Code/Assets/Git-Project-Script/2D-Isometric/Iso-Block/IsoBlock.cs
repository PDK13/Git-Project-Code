using UnityEngine;

public class oBlock : MonoBehaviour
{
    #region Pos

    [Header("Pos m_anager")]

    [Tooltip("Pos on World")]
    [SerializeField]
    private Vector3 m_Pos = new Vector3();

    [Header("Pos m_atrix m_anager")]

    [Tooltip("Round to Uper on m_atrix")]
    [SerializeField]
    [Range(0.5f, 0.6f)]
    private float m_Pom_Matrix_Max = 0.55f;

    [Tooltip("Round to Lower on m_atrix")]
    [SerializeField]
    [Range(0.4f, 0.5f)]
    private float m_Pom_Matrix_Min = 0.45f;

    [Tooltip("Pos Primary on m_atrix")]
    private Vector3Int m_PosOnMatrix_Primary = new Vector3Int();

    [Tooltip("Pos on m_atrix")]
    private Vector3Int m_PosOnMatrix = new Vector3Int();

    [Header("Pos Fix m_anager")]

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

    #region Type and Dir

    [Header("Block m_anager")]

    private oClassBlock cm_Block = new oClassBlock("...", "...");

    #endregion

    private void FixedUpdate()
    {
        SetPom_Transform();
    }

    #region Pos 

    #region Pos main 

    private void SetPom_Transform()
    {
        //if (cm_World != null)
        //{
        //    m_Offset = cm_World.GetFix_Offset();
        //    m_Square = cm_World.GetFix_Square();
        //}

        Vector3 m_Pom_Transform = GetPom_OnScene(m_Pos);

        m_Pom_Transform.x *= m_Square.x;
        m_Pom_Transform.y *= m_Square.y;
        m_Pom_Transform.z *= m_Square.z;

        m_Pom_Transform += m_Offset;

        m_Pom_Transform += m_Fix;

        transform.position = m_Pom_Transform;

        SetPom_onMatrix();
    }

    private void SetPom_onMatrix()
    {
        float m_X = (Mathf.Abs((int)m_Pos.x - m_Pos.x));

        if (m_X > m_Pom_Matrix_Max)
        {
            m_PosOnMatrix.x = (int)m_Pos.x + 1;
        }
        else
        if (m_X < m_Pom_Matrix_Min)
        {
            m_PosOnMatrix.x = (int)m_Pos.x;
        }

        float m_Y = (Mathf.Abs((int)m_Pos.y - m_Pos.y));

        if (m_Y > m_Pom_Matrix_Max)
        {
            m_PosOnMatrix.y = (int)m_Pos.y + 1;
        }
        else
        if (m_Y < m_Pom_Matrix_Min)
        {
            m_PosOnMatrix.y = (int)m_Pos.y;
        }

        float m_High = (Mathf.Abs((int)m_Pos.z - m_Pos.z));

        if (m_High > m_Pom_Matrix_Max)
        {
            m_PosOnMatrix.z = (int)m_Pos.z + 1;
        }
        else
        if (m_High < m_Pom_Matrix_Min)
        {
            m_PosOnMatrix.z = (int)m_Pos.z;
        }
    }

    public static Vector3 GetPom_OnScene(Vector3 m_Pom_OnWorld)
    {
        Vector3 m_FixTransform = new Vector3(
            m_Pom_OnWorld.x + m_Pom_OnWorld.y,
            0.5f * (m_Pom_OnWorld.y - m_Pom_OnWorld.x) + m_Pom_OnWorld.z,
            (m_Pom_OnWorld.y - m_Pom_OnWorld.x) - m_Pom_OnWorld.z);

        return m_FixTransform;
    }

    public static Vector2 GetPom_OnWorld(Vector2 m_Pom_OnScene)
    {
        float m_Y_OnWorld = m_Pom_OnScene.y + m_Pom_OnScene.x / 2;

        float m_X_OnWorld = m_Pom_OnScene.x - m_Y_OnWorld;

        return new Vector3(m_X_OnWorld, m_Y_OnWorld, 0);
    }

    #endregion

    #region Pos on m_atrix Float 

    #region Set Pos 

    public void SetPos(Vector3 m_Pos)
    {
        this.m_Pos = m_Pos;

        SetPom_Transform();
    }

    public void SetPos(float m_X, float m_Y, float m_High)
    {
        SetPos(new Vector3(m_X, m_Y, m_High));
    }

    public void SetPomX(float m_X)
    {
        m_Pos.x = m_X;

        SetPom_Transform();
    }

    public void SetPomY(float m_Y)
    {
        m_Pos.y = m_Y;

        SetPom_Transform();
    }

    public void SetPom_High(float m_High)
    {
        m_Pos.z = m_High;

        SetPom_Transform();
    }

    #endregion

    #region Set Pos Add 

    public void SetPom_Add(Vector3 m_Pom_Add)
    {
        SetPos(GetPomCurrentrent() + m_Pom_Add);
    }

    public void SetPom_Add(float m_X_Add, float m_Y_Add, float m_High_Add)
    {
        SetPom_Add(new Vector3(m_X_Add, m_Y_Add, m_High_Add));
    }

    public void SetPom_AddX(float m_X_Add)
    {
        m_Pos.x += m_X_Add;

        SetPom_Transform();
    }

    public void SetPom_AddY(float m_Y_Add)
    {
        m_Pos.y += m_Y_Add;

        SetPom_Transform();
    }

    public void SetPom_Add_High(float m_High_Add)
    {
        m_Pos.z += m_High_Add;

        SetPom_Transform();
    }

    #endregion

    #region Get Pos 

    public Vector3 GetPomCurrentrent()
    {
        return m_Pos;
    }

    public float GetPomCurrentrentX()
    {
        return GetPomCurrentrent().x;
    }

    public float GetPomCurrentrentY()
    {
        return GetPomCurrentrent().y;
    }

    public float GetPomCurrentrent_High()
    {
        return GetPomCurrentrent().z;
    }

    #endregion

    #endregion

    #region Pos on m_atrix Int 

    public Vector3Int GetPosOnMatrixCurrentrent()
    {
        return m_PosOnMatrix;
    }

    #region Pos on m_atrix Primary 

    public void SetPosOnMatrix_Primary(Vector3Int m_Pos)
    {
        m_PosOnMatrix_Primary = m_Pos;
    }

    public Vector3Int GetPosOnMatrix_Primary()
    {
        return m_PosOnMatrix_Primary;
    }

    /// <summary>
    /// Check Pos on m_atrix Current same on Pos on m_atrix Primary
    /// </summary>
    /// <returns></returns>
    public bool GetCheckPosOnMatrix_StayOnPrimary()
    {
        return GetPosOnMatrix_Primary() == GetPosOnMatrixCurrentrent();
    }

    /// <summary>
    /// Reset Pos Current to Pos on m_atrix Primary
    /// </summary>
    public void SetPosOnMatrixResetToPrimary()
    {
        SetPos(GetPosOnMatrix_Primary());
    }

    #endregion

    #endregion

    public void SetFix(Vector3 m_Fix)
    {
        this.m_Fix = m_Fix;
    }

    #endregion
}
