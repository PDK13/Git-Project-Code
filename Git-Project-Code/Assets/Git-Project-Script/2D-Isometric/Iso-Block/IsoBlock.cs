using UnityEngine;

public class oBlock : MonoBehaviour
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
    private float m_Pos_Matrix_Max = 0.55f;

    [Tooltip("Round to Lower on Matrix")]
    [SerializeField]
    [Range(0.4f, 0.5f)]
    private float m_Pos_Matrix_Min = 0.45f;

    [Tooltip("Pos Primary on Matrix")]
    private Vector3Int m_PosOnMatrix_Primary = new Vector3Int();

    [Tooltip("Pos on Matrix")]
    private Vector3Int m_PosOnMatrix = new Vector3Int();

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
        SetPos_Transform();
    }

    #region Pos 

    #region Pos main 

    private void SetPos_Transform()
    {
        //if (cs_World != null)
        //{
        //    m_Offset = cs_World.GetFix_Offset();
        //    m_Square = cs_World.GetFix_Square();
        //}

        Vector3 m_Pos_Transform = GetPos_OnScene(m_Pos);

        m_Pos_Transform.x *= m_Square.x;
        m_Pos_Transform.y *= m_Square.y;
        m_Pos_Transform.z *= m_Square.z;

        m_Pos_Transform += m_Offset;

        m_Pos_Transform += m_Fix;

        transform.position = m_Pos_Transform;

        SetPos_onMatrix();
    }

    private void SetPos_onMatrix()
    {
        float m_X = (Mathf.Abs((int)m_Pos.x - m_Pos.x));

        if (m_X > m_Pos_Matrix_Max)
        {
            m_PosOnMatrix.x = (int)m_Pos.x + 1;
        }
        else
        if (m_X < m_Pos_Matrix_Min)
        {
            m_PosOnMatrix.x = (int)m_Pos.x;
        }

        float m_Y = (Mathf.Abs((int)m_Pos.y - m_Pos.y));

        if (m_Y > m_Pos_Matrix_Max)
        {
            m_PosOnMatrix.y = (int)m_Pos.y + 1;
        }
        else
        if (m_Y < m_Pos_Matrix_Min)
        {
            m_PosOnMatrix.y = (int)m_Pos.y;
        }

        float m_H = (Mathf.Abs((int)m_Pos.z - m_Pos.z));

        if (m_H > m_Pos_Matrix_Max)
        {
            m_PosOnMatrix.z = (int)m_Pos.z + 1;
        }
        else
        if (m_H < m_Pos_Matrix_Min)
        {
            m_PosOnMatrix.z = (int)m_Pos.z;
        }
    }

    public static Vector3 GetPos_OnScene(Vector3 m_Pos_OnWorld)
    {
        Vector3 m_FixTransform = new Vector3(
            m_Pos_OnWorld.x + m_Pos_OnWorld.y,
            0.5f * (m_Pos_OnWorld.y - m_Pos_OnWorld.x) + m_Pos_OnWorld.z,
            (m_Pos_OnWorld.y - m_Pos_OnWorld.x) - m_Pos_OnWorld.z);

        return m_FixTransform;
    }

    public static Vector2 GetPos_OnWorld(Vector2 m_Pos_OnScene)
    {
        float m_Y_OnWorld = m_Pos_OnScene.y + m_Pos_OnScene.x / 2;

        float m_X_OnWorld = m_Pos_OnScene.x - m_Y_OnWorld;

        return new Vector3(m_X_OnWorld, m_Y_OnWorld, 0);
    }

    #endregion

    #region Pos on Matrix Float 

    #region Set Pos 

    public void SetPos(Vector3 m_Pos)
    {
        this.m_Pos = m_Pos;

        SetPos_Transform();
    }

    public void SetPos(float m_X, float m_Y, float m_H)
    {
        SetPos(new Vector3(m_X, m_Y, m_H));
    }

    public void SetPosX(float m_X)
    {
        m_Pos.x = m_X;

        SetPos_Transform();
    }

    public void SetPosY(float m_Y)
    {
        m_Pos.y = m_Y;

        SetPos_Transform();
    }

    public void SetPosH(float m_H)
    {
        m_Pos.z = m_H;

        SetPos_Transform();
    }

    #endregion

    #region Set Pos Add 

    public void SetPosAdd(Vector3 m_PosAdd)
    {
        SetPos(GetPosCurrentrent() + m_PosAdd);
    }

    public void SetPosAdd(float m_XAdd, float m_YAdd, float m_HAdd)
    {
        SetPosAdd(new Vector3(m_XAdd, m_YAdd, m_HAdd));
    }

    public void SetPosAddX(float m_XAdd)
    {
        m_Pos.x += m_XAdd;

        SetPos_Transform();
    }

    public void SetPosAddY(float m_YAdd)
    {
        m_Pos.y += m_YAdd;

        SetPos_Transform();
    }

    public void SetPosAddH(float m_HAdd)
    {
        m_Pos.z += m_HAdd;

        SetPos_Transform();
    }

    #endregion

    #region Get Pos 

    public Vector3 GetPosCurrentrent()
    {
        return m_Pos;
    }

    public float GetPosCurrentrentX()
    {
        return GetPosCurrentrent().x;
    }

    public float GetPosCurrentrentY()
    {
        return GetPosCurrentrent().y;
    }

    public float GetPosCurrentrentH()
    {
        return GetPosCurrentrent().z;
    }

    #endregion

    #endregion

    #region Pos on Matrix Int 

    public Vector3Int GetPosOnMatrixCurrent()
    {
        return m_PosOnMatrix;
    }

    #region Pos on Matrix Primary 

    public void SetPosOnMatrix_Primary(Vector3Int m_Pos)
    {
        m_PosOnMatrix_Primary = m_Pos;
    }

    public Vector3Int GetPosOnMatrixPrimary()
    {
        return m_PosOnMatrix_Primary;
    }

    /// <summary>
    /// Check Pos on Matrix Current same on Pos on Matrix Primary
    /// </summary>
    /// <returns></returns>
    public bool GetCheckPosOnMatrix_StayOnPrimary()
    {
        return GetPosOnMatrixPrimary() == GetPosOnMatrixCurrent();
    }

    /// <summary>
    /// Reset Pos Current to Pos on Matrix Primary
    /// </summary>
    public void SetPosOnMatrixResetToPrimary()
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
