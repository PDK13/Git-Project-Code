using UnityEngine;

public class IsoBlock : MonoBehaviour
{
    #region Pos

    [Header("Pos Manager")]

    [Tooltip("Pos on World")]
    [SerializeField]
    private Vector3 v3_Pos = new Vector3();

    [Header("Pos Matrix Manager")]

    [Tooltip("Round to Upper on Matrix")]
    [SerializeField]
    [Range(0.5f, 0.6f)]
    private float m_Pom_Matrix_Max = 0.55f;

    [Tooltip("Round to Lower on Matrix")]
    [SerializeField]
    [Range(0.4f, 0.5f)]
    private float m_Pom_Matrix_Min = 0.45f;

    [Tooltip("Pos Primary on Matrix")]
    private Vector3Int v3_PosOnMatrix_Primary = new Vector3Int();

    [Tooltip("Pos on Matrix")]
    private Vector3Int v3_PosOnMatrix = new Vector3Int();

    [Header("Pos Fix Manager")]

    [Tooltip("Fix Block Centre")]
    [SerializeField]
    private Vector3 v3_Fix = new Vector3(0f, 0f, 0f);

    [Tooltip("Fix Block Distance")]
    [SerializeField]
    private Vector3 v3_Square = new Vector3(1f, 1f, 1f);

    [Tooltip("Pos Block Offset")]
    [SerializeField]
    private Vector3 v3_Offset = new Vector3(0f, 0f, 0f);

    #endregion

    #region Type and Dir

    [Header("Block Manager")]

    private IsoClassBlock cl_Block = new IsoClassBlock("...", "...");

    #endregion

    private void FixedUpdate()
    {
        SetPom_Transform();
    }

    #region Pos 

    #region Pos Main 

    private void SetPom_Transform()
    {
        //if (cl_World != null)
        //{
        //    v3_Offset = cl_World.GetFix_Offset();
        //    v3_Square = cl_World.GetFix_Square();
        //}

        Vector3 v3_Pom_Transform = GetPom_OnScene(v3_Pos);

        v3_Pom_Transform.x *= v3_Square.x;
        v3_Pom_Transform.y *= v3_Square.y;
        v3_Pom_Transform.z *= v3_Square.z;

        v3_Pom_Transform += v3_Offset;

        v3_Pom_Transform += v3_Fix;

        transform.position = v3_Pom_Transform;

        SetPom_onMatrix();
    }

    private void SetPom_onMatrix()
    {
        float m_Check_X = (Mathf.Abs((int)v3_Pos.x - v3_Pos.x));

        if (m_Check_X > m_Pom_Matrix_Max)
        {
            v3_PosOnMatrix.x = (int)v3_Pos.x + 1;
        }
        else
        if (m_Check_X < m_Pom_Matrix_Min)
        {
            v3_PosOnMatrix.x = (int)v3_Pos.x;
        }

        float m_Check_Y = (Mathf.Abs((int)v3_Pos.y - v3_Pos.y));

        if (m_Check_Y > m_Pom_Matrix_Max)
        {
            v3_PosOnMatrix.y = (int)v3_Pos.y + 1;
        }
        else
        if (m_Check_Y < m_Pom_Matrix_Min)
        {
            v3_PosOnMatrix.y = (int)v3_Pos.y;
        }

        float m_Check_High = (Mathf.Abs((int)v3_Pos.z - v3_Pos.z));

        if (m_Check_High > m_Pom_Matrix_Max)
        {
            v3_PosOnMatrix.z = (int)v3_Pos.z + 1;
        }
        else
        if (m_Check_High < m_Pom_Matrix_Min)
        {
            v3_PosOnMatrix.z = (int)v3_Pos.z;
        }
    }

    public static Vector3 GetPom_OnScene(Vector3 v3_Pom_OnWorld)
    {
        Vector3 v3_FixTransform = new Vector3(
            v3_Pom_OnWorld.x + v3_Pom_OnWorld.y,
            0.5f * (v3_Pom_OnWorld.y - v3_Pom_OnWorld.x) + v3_Pom_OnWorld.z,
            (v3_Pom_OnWorld.y - v3_Pom_OnWorld.x) - v3_Pom_OnWorld.z);

        return v3_FixTransform;
    }

    public static Vector2 GetPom_OnWorld(Vector2 v3_Pom_OnScene)
    {
        float m_Y_OnWorld = v3_Pom_OnScene.y + v3_Pom_OnScene.x / 2;

        float m_X_OnWorld = v3_Pom_OnScene.x - m_Y_OnWorld;

        return new Vector3(m_X_OnWorld, m_Y_OnWorld, 0);
    }

    #endregion

    #region Pos on Matrix Float 

    #region Set Pos 

    public void SetPos(Vector3 v3_Pos)
    {
        this.v3_Pos = v3_Pos;

        SetPom_Transform();
    }

    public void SetPos(float m_X, float m_Y, float m_High)
    {
        SetPos(new Vector3(m_X, m_Y, m_High));
    }

    public void SetPom_X(float m_X)
    {
        v3_Pos.x = m_X;

        SetPom_Transform();
    }

    public void SetPom_Y(float m_Y)
    {
        v3_Pos.y = m_Y;

        SetPom_Transform();
    }

    public void SetPom_High(float m_High)
    {
        v3_Pos.z = m_High;

        SetPom_Transform();
    }

    #endregion

    #region Set Pos Add 

    public void SetPom_Add(Vector3 v3_Pom_Add)
    {
        SetPos(GetPom_Current() + v3_Pom_Add);
    }

    public void SetPom_Add(float m_X_Add, float m_Y_Add, float m_High_Add)
    {
        SetPom_Add(new Vector3(m_X_Add, m_Y_Add, m_High_Add));
    }

    public void SetPom_Add_X(float m_X_Add)
    {
        v3_Pos.x += m_X_Add;

        SetPom_Transform();
    }

    public void SetPom_Add_Y(float m_Y_Add)
    {
        v3_Pos.y += m_Y_Add;

        SetPom_Transform();
    }

    public void SetPom_Add_High(float m_High_Add)
    {
        v3_Pos.z += m_High_Add;

        SetPom_Transform();
    }

    #endregion

    #region Get Pos 

    public Vector3 GetPom_Current()
    {
        return v3_Pos;
    }

    public float GetPom_Current_X()
    {
        return GetPom_Current().x;
    }

    public float GetPom_Current_Y()
    {
        return GetPom_Current().y;
    }

    public float GetPom_Current_High()
    {
        return GetPom_Current().z;
    }

    #endregion

    #endregion

    #region Pos on Matrix Int 

    public Vector3Int GetPosOnMatrix_Current()
    {
        return v3_PosOnMatrix;
    }

    #region Pos on Matrix Primary 

    public void SetPosOnMatrix_Primary(Vector3Int v3_Pos)
    {
        v3_PosOnMatrix_Primary = v3_Pos;
    }

    public Vector3Int GetPosOnMatrix_Primary()
    {
        return v3_PosOnMatrix_Primary;
    }

    /// <summary>
    /// Check Pos on Matrix Current same on Pos on Matrix Primary
    /// </summary>
    /// <returns></returns>
    public bool GetPosOnMatrix_StayOnPrimary()
    {
        return GetPosOnMatrix_Primary() == GetPosOnMatrix_Current();
    }

    /// <summary>
    /// Reset Pos Current to Pos on Matrix Primary
    /// </summary>
    public void SetPosOnMatrix_ResetToPrimary()
    {
        SetPos(GetPosOnMatrix_Primary());
    }

    #endregion

    #endregion

    public void SetFix(Vector3 v3_Fix)
    {
        this.v3_Fix = v3_Fix;
    }

    #endregion

    //============================================================================================================ Name, Type & Dir

    #region Name 

    /// <summary>
    /// Get Origin Name of GameObject
    /// </summary>
    /// <returns></returns>
    public string GetName()
    {
        return ClassString.GetStringReplaceClone(name);
    }

    #endregion

    #region Type and Dir Set 

    public void SetImformation(IsoClassBlock cl_Imformation_Block)
    {
        cl_Block = cl_Imformation_Block;
    }

    #region Type and Dir Check 

    public IsoClassBlock GetImformation()
    {
        return cl_Block;
    }

    [ContextMenu("Block Imformation")]
    public void Debug_Imformation()
    {
        Debug.Log(v3_PosOnMatrix + " : " + cl_Block.GetType());
    }

    #region Check Block 

    #region Check Block Type 

    public bool GetBlock_Check()
    {
        return cl_Block.GetType_Main().Equals(IsoClassBlock.m_Block);
    }

    public bool GetBlock_Check_Ground()
    {
        return cl_Block.GetType().Equals(IsoClassBlock.m_Block_Ground);
    }

    public bool GetBlock_Check_Object()
    {
        return cl_Block.GetType().Equals(IsoClassBlock.m_Block_Object);
    }

    public bool GetBlock_Check_Stair()
    {
        return cl_Block.GetType().Equals(IsoClassBlock.m_Block_Stair);
    }

    public bool GetBlock_Check_StairUD()
    {
        return cl_Block.GetType().Equals(IsoClassBlock.m_Block_StairUD);
    }

    public bool GetBlock_Check_StairLR()
    {
        return cl_Block.GetType().Equals(IsoClassBlock.m_Block_StairLR);
    }

    #endregion

    #endregion

    #region Check Character 

    public bool GetCharacter_Check()
    {
        return cl_Block.GetType_Main().Equals(IsoClassBlock.m_Character);
    }

    public bool GetCharacter_Check_Player()
    {
        return cl_Block.GetType().Equals(IsoClassBlock.m_Character_Player);
    }

    public bool GetCharacter_Check_Good()
    {
        return cl_Block.GetType().Equals(IsoClassBlock.m_Character_Good);
    }

    public bool GetCharacter_Check_Neutral()
    {
        return cl_Block.GetType().Equals(IsoClassBlock.m_Character_Neutral);
    }

    public bool GetCharacter_Check_Bad()
    {
        return cl_Block.GetType().Equals(IsoClassBlock.m_Character_Bad);
    }

    #endregion

    #endregion

    #endregion
}
