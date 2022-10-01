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
    private float f_Pos_Matrix_Max = 0.55f;

    [Tooltip("Round to Lower on Matrix")]
    [SerializeField]
    [Range(0.4f, 0.5f)]
    private float f_Pos_Matrix_Min = 0.45f;

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

    [SerializeField]
    private IsoWorld cl_World;

    private IsoClassBlock cl_Block = new IsoClassBlock("...", "...");

    #endregion

    public void Set_World(IsoWorld cl_World)
    {
        this.cl_World = cl_World;

        Set_Pos_Transform();
    }

    public IsoWorld Get_World()
    {
        return cl_World;
    }

    private void FixedUpdate()
    {
        Set_Pos_Transform();
    }

    #region Pos 

    #region Pos Main 

    private void Set_Pos_Transform()
    {
        if (cl_World != null)
        {
            v3_Offset = cl_World.Get_Fix_Offset();
            v3_Square = cl_World.Get_Fix_Square();
        }

        Vector3 v3_Pos_Transform = Get_Pos_OnScene(v3_Pos);

        v3_Pos_Transform.x *= v3_Square.x;
        v3_Pos_Transform.y *= v3_Square.y;
        v3_Pos_Transform.z *= v3_Square.z;

        v3_Pos_Transform += v3_Offset;

        v3_Pos_Transform += v3_Fix;

        transform.position = v3_Pos_Transform;

        Set_Pos_onMatrix();
    }

    private void Set_Pos_onMatrix()
    {
        float i_Check_X = (Mathf.Abs((int)v3_Pos.x - v3_Pos.x));

        if (i_Check_X > f_Pos_Matrix_Max)
        {
            v3_PosOnMatrix.x = (int)v3_Pos.x + 1;
        }
        else
        if (i_Check_X < f_Pos_Matrix_Min)
        {
            v3_PosOnMatrix.x = (int)v3_Pos.x;
        }

        float i_Check_Y = (Mathf.Abs((int)v3_Pos.y - v3_Pos.y));

        if (i_Check_Y > f_Pos_Matrix_Max)
        {
            v3_PosOnMatrix.y = (int)v3_Pos.y + 1;
        }
        else
        if (i_Check_Y < f_Pos_Matrix_Min)
        {
            v3_PosOnMatrix.y = (int)v3_Pos.y;
        }

        float i_Check_High = (Mathf.Abs((int)v3_Pos.z - v3_Pos.z));

        if (i_Check_High > f_Pos_Matrix_Max)
        {
            v3_PosOnMatrix.z = (int)v3_Pos.z + 1;
        }
        else
        if (i_Check_High < f_Pos_Matrix_Min)
        {
            v3_PosOnMatrix.z = (int)v3_Pos.z;
        }
    }

    /// <summary>
    /// Exchance Pos on World to Pos on Scene or Screen
    /// </summary>
    /// <param name="v3_Pos_OnWorld">XY which UDLR and Z which TB</param>
    /// <returns></returns>
    public static Vector3 Get_Pos_OnScene(Vector3 v3_Pos_OnWorld)
    {
        Vector3 v3_FixTransform = new Vector3(
            v3_Pos_OnWorld.x + v3_Pos_OnWorld.y,
            0.5f * (v3_Pos_OnWorld.y - v3_Pos_OnWorld.x) + v3_Pos_OnWorld.z,
            (v3_Pos_OnWorld.y - v3_Pos_OnWorld.x) - v3_Pos_OnWorld.z);

        return v3_FixTransform;
    }

    /// <summary>
    /// Exchance Pos on Scene or Screen to Pos on World
    /// </summary>
    /// <param name="v3_Pos_OnScene"></param>
    /// <returns></returns>
    public static Vector2 Get_Pos_OnWorld(Vector2 v3_Pos_OnScene)
    {
        //return new Vector2(0.5f * v_Pos_Transform.x - v_Pos_Transform.y, 0.5f * v_Pos_Transform.x + v_Pos_Transform.y);

        float f_Y_OnWorld = v3_Pos_OnScene.y + v3_Pos_OnScene.x / 2;

        float f_X_OnWorld = v3_Pos_OnScene.x - f_Y_OnWorld;

        return new Vector3(f_X_OnWorld, f_Y_OnWorld, 0);
    }

    #endregion

    #region Pos on Matrix Float 

    #region Set Pos 

    public void Set_Pos(Vector3 v3_Pos)
    {
        this.v3_Pos = v3_Pos;

        Set_Pos_Transform();
    }

    public void Set_Pos(float f_X, float f_Y, float f_High)
    {
        Set_Pos(new Vector3(f_X, f_Y, f_High));
    }

    public void Set_Pos_X(float f_X)
    {
        v3_Pos.x = f_X;

        Set_Pos_Transform();
    }

    public void Set_Pos_Y(float f_Y)
    {
        v3_Pos.y = f_Y;

        Set_Pos_Transform();
    }

    public void Set_Pos_High(float f_High)
    {
        v3_Pos.z = f_High;

        Set_Pos_Transform();
    }

    #endregion

    #region Set Pos Add 

    public void Set_Pos_Add(Vector3 v3_Pos_Add)
    {
        Set_Pos(Get_Pos_Current() + v3_Pos_Add);
    }

    public void Set_Pos_Add(float f_X_Add, float f_Y_Add, float f_High_Add)
    {
        Set_Pos_Add(new Vector3(f_X_Add, f_Y_Add, f_High_Add));
    }

    public void Set_Pos_Add_X(float f_X_Add)
    {
        v3_Pos.x += f_X_Add;

        Set_Pos_Transform();
    }

    public void Set_Pos_Add_Y(float f_Y_Add)
    {
        v3_Pos.y += f_Y_Add;

        Set_Pos_Transform();
    }

    public void Set_Pos_Add_High(float f_High_Add)
    {
        v3_Pos.z += f_High_Add;

        Set_Pos_Transform();
    }

    #endregion

    #region Get Pos 

    public Vector3 Get_Pos_Current()
    {
        return v3_Pos;
    }

    public float Get_Pos_Current_X()
    {
        return Get_Pos_Current().x;
    }

    public float Get_Pos_Current_Y()
    {
        return Get_Pos_Current().y;
    }

    public float Get_Pos_Current_High()
    {
        return Get_Pos_Current().z;
    }

    #endregion

    #endregion

    #region Pos on Matrix Int 

    public Vector3Int Get_PosOnMatrix_Current()
    {
        return v3_PosOnMatrix;
    }

    #region Pos on Matrix Primary 

    public void Set_PosOnMatrix_Primary(Vector3Int v3_Pos)
    {
        v3_PosOnMatrix_Primary = v3_Pos;
    }

    public Vector3Int Get_PosOnMatrix_Primary()
    {
        return v3_PosOnMatrix_Primary;
    }

    /// <summary>
    /// Check Pos on Matrix Current same on Pos on Matrix Primary
    /// </summary>
    /// <returns></returns>
    public bool Get_PosOnMatrix_StayOnPrimary()
    {
        return Get_PosOnMatrix_Primary() == Get_PosOnMatrix_Current();
    }

    /// <summary>
    /// Reset Pos Current to Pos on Matrix Primary
    /// </summary>
    public void Set_PosOnMatrix_ResetToPrimary()
    {
        Set_Pos(Get_PosOnMatrix_Primary());
    }

    #endregion

    #endregion

    public void Set_Fix(Vector3 v3_Fix)
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
    public string Get_Name()
    {
        return Class_String.Get_String_Replace_Clone(name);
    }

    #endregion

    #region Type and Dir Set 

    public void Set_Imformation(IsoClassBlock cl_Imformation_Block)
    {
        cl_Block = cl_Imformation_Block;
    }

    #region Type and Dir Check 

    public IsoClassBlock Get_Imformation()
    {
        return cl_Block;
    }

    [ContextMenu("Block Imformation")]
    public void Debug_Imformation()
    {
        Debug.Log(v3_PosOnMatrix + " : " + cl_Block.Get_Type());
    }

    #region Check Block 

    #region Check Block Type 

    public bool Get_Block_Check()
    {
        return cl_Block.Get_Type_Main().Equals(IsoClassBlock.s_Block);
    }

    public bool Get_Block_Check_Ground()
    {
        return cl_Block.Get_Type().Equals(IsoClassBlock.s_Block_Ground);
    }

    public bool Get_Block_Check_Object()
    {
        return cl_Block.Get_Type().Equals(IsoClassBlock.s_Block_Object);
    }

    public bool Get_Block_Check_Stair()
    {
        return cl_Block.Get_Type().Equals(IsoClassBlock.s_Block_Stair);
    }

    public bool Get_Block_Check_StairUD()
    {
        return cl_Block.Get_Type().Equals(IsoClassBlock.s_Block_StairUD);
    }

    public bool Get_Block_Check_StairLR()
    {
        return cl_Block.Get_Type().Equals(IsoClassBlock.s_Block_StairLR);
    }

    #endregion

    #endregion

    #region Check Character 

    public bool Get_Character_Check()
    {
        return cl_Block.Get_Type_Main().Equals(IsoClassBlock.s_Character);
    }

    public bool Get_Character_Check_Player()
    {
        return cl_Block.Get_Type().Equals(IsoClassBlock.s_Character_Player);
    }

    public bool Get_Character_Check_Good()
    {
        return cl_Block.Get_Type().Equals(IsoClassBlock.s_Character_Good);
    }

    public bool Get_Character_Check_Neutral()
    {
        return cl_Block.Get_Type().Equals(IsoClassBlock.s_Character_Neutral);
    }

    public bool Get_Character_Check_Bad()
    {
        return cl_Block.Get_Type().Equals(IsoClassBlock.s_Character_Bad);
    }

    #endregion

    #endregion

    #endregion
}
