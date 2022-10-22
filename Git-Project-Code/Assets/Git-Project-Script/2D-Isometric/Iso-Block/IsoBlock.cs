using UnityEditor;
using UnityEngine;

[ExecuteAlways]
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

    private void Update()
    {
        SetIsoTransform();
    }

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
                m_PosWorldScale.X_UD *= m_Scale.X_UD * -1;
                m_PosWorldScale.Y_LR *= m_Scale.Y_LR;
                m_PosWorldScale.H_TB *= m_Scale.H_TB;

                float m_PosX = m_PosWorldScale.X_UD + m_PosWorldScale.Y_LR;
                float m_PosY = 0.5f * (m_PosWorldScale.Y_LR - m_PosWorldScale.X_UD) + m_PosWorldScale.H_TB;
                float m_PosZ = (m_PosWorldScale.Y_LR - m_PosWorldScale.X_UD) - m_PosWorldScale.H_TB;

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
        float m_PosX = (Mathf.Abs((int)m_Pos.X_UD - m_Pos.X_UD));

        if (m_PosX > m_RoundUpper)
        {
            m_PosMatrix.X_UD = (int)m_Pos.X_UD + 1;
        }
        else
        if (m_PosX < m_RoundLower)
        {
            m_PosMatrix.X_UD = (int)m_Pos.X_UD;
        }

        float m_PosY = (Mathf.Abs((int)m_Pos.Y_LR - m_Pos.Y_LR));

        if (m_PosY > m_RoundUpper)
        {
            m_PosMatrix.Y_LR = (int)m_Pos.Y_LR + 1;
        }
        else
        if (m_PosY < m_RoundLower)
        {
            m_PosMatrix.Y_LR = (int)m_Pos.Y_LR;
        }

        float m_PosH = (Mathf.Abs((int)m_Pos.H_TB - m_Pos.H_TB));

        if (m_PosH > m_RoundUpper)
        {
            m_PosMatrix.H_TB = (int)m_Pos.H_TB + 1;
        }
        else
        if (m_PosH < m_RoundLower)
        {
            m_PosMatrix.H_TB = (int)m_Pos.H_TB;
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

    public IsoVector Pos
    {
        get
        {
            return this.m_Pos;
        }
        set
        {
            this.m_Pos = value;
            SetIsoTransform();
        }
    }

    #endregion

    #region ================================================================== Pos Primary & Matrix

    public void SetPosPrimary(IsoVector m_Pos)
    {
        m_PosPrimary = m_Pos;
    }

    public IsoVector GetPosPrimary()
    {
        return m_PosPrimary;
    }

    public void SetPosPrimaryReset()
    {
        this.m_Pos = GetPosPrimary();
    }

    public bool GetPosPrimaryStay()
    {
        IsoVector m_PosPrimary = GetPosPrimary();
        IsoVector m_PosMatrixCurrent = GetPosMatrixCurrent();

        return m_PosPrimary.X_UD == m_PosMatrixCurrent.X_UD && m_PosPrimary.Y_LR == m_PosMatrixCurrent.Y_LR && m_PosPrimary.H_TB == m_PosMatrixCurrent.H_TB;
    }

    public IsoVector GetPosMatrixCurrent()
    {
        return m_PosMatrix;
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
    None = 0,
    //X
    Up = 1,
    Down = 2,
    //Y
    Left = 3,
    Right = 4,
    //H
    Top = 5,
    Bot = 6,
}

public enum IsoDepth
{
    Fixed,
}

[System.Serializable]
public struct IsoVector
{
    //Primary

    public float X_UD;

    public float Y_LR;

    public float H_TB;

    public IsoVector(float m_XUD, float m_YLR, float m_HTB)
    {
        X_UD = m_XUD;
        Y_LR = m_YLR;
        H_TB = m_HTB;
    }

    public IsoVector(IsoVector m_IsoVector)
    {
        X_UD = m_IsoVector.X_UD;
        Y_LR = m_IsoVector.Y_LR;
        H_TB = m_IsoVector.H_TB;
    }

    //Value Int

    public int X_UDInt
    {
        get
        {
            return (int)X_UD;
        }
    }

    public int Y_LRInt
    {
        get
        {
            return (int)Y_LR;
        }
    }

    public int H_TBInt
    {
        get
        {
            return (int)H_TB;
        }
    }

    //Primary Dir

    public IsoVector Up
    {
        get
        {
            return new IsoVector(1, 0, 0);
        }
    }

    public IsoVector Down
    {
        get
        {
            return new IsoVector(-1, 0, 0);
        }
    }

    public IsoVector Left
    {
        get
        {
            return new IsoVector(0, -1, 0);
        }
    }

    public IsoVector Right
    {
        get
        {
            return new IsoVector(0, 1, 0);
        }
    }

    public IsoVector Top
    {
        get
        {
            return new IsoVector(0, 0, 1);
        }
    }

    public IsoVector Bot
    {
        get
        {
            return new IsoVector(0, 0, -1);
        }
    }

    //If Move-To Dir

    public IsoVector IfUp
    {
        get
        {
            return this + Up;
        }
    }

    public IsoVector IfDown
    {
        get
        {
            return this + Down;
        }
    }

    public IsoVector IfLeft
    {
        get
        {
            return this + Left;
        }
    }

    public IsoVector IfRight
    {
        get
        {
            return this + Right;
        }
    }

    public IsoVector IfTop
    {
        get
        {
            return this + Top;
        }
    }

    public IsoVector IfBot
    {
        get
        {
            return this + Bot;
        }
    }


    //Move-To Dir

    public IsoVector ToUp
    {
        get
        {
            this = this + Up;
            return this;
        }
    }

    public IsoVector ToDown
    {
        get
        {
            this = this + Down;
            return this;
        }
    }

    public IsoVector ToLeft
    {
        get
        {
            this = this + Left;
            return this;
        }
    }

    public IsoVector ToRight
    {
        get
        {
            this = this + Right;
            return this;
        }
    }

    public IsoVector ToTop
    {
        get
        {
            this = this + Top;
            return this;
        }
    }

    public IsoVector ToBot
    {
        get
        {
            this = this + Bot;
            return this;
        }
    }

    //Operator

    public static IsoVector operator +(IsoVector m_IsoVector)
        => m_IsoVector;

    public static IsoVector operator -(IsoVector m_IsoVector)
        => new IsoVector(m_IsoVector.X_UD * -1, m_IsoVector.Y_LR * -1, m_IsoVector.H_TB * -1);

    public static IsoVector operator +(IsoVector m_IsoVectorA, IsoVector m_IsoVectorB)
        => new IsoVector(m_IsoVectorA.X_UD + m_IsoVectorB.X_UD, m_IsoVectorA.Y_LR + m_IsoVectorB.Y_LR, m_IsoVectorA.H_TB + m_IsoVectorB.H_TB);

    public static IsoVector operator -(IsoVector m_IsoVectorA, IsoVector m_IsoVectorB)
        => new IsoVector(m_IsoVectorA.X_UD - m_IsoVectorB.X_UD, m_IsoVectorA.Y_LR - m_IsoVectorB.Y_LR, m_IsoVectorA.H_TB - m_IsoVectorB.H_TB);

    public static IsoVector operator *(IsoVector m_IsoVectorA, float m_Number)
        => new IsoVector(m_IsoVectorA.X_UD * m_Number, m_IsoVectorA.Y_LR * m_Number, m_IsoVectorA.H_TB * m_Number);

    public static IsoVector operator /(IsoVector m_IsoVectorA, float m_Number)
        => new IsoVector(m_IsoVectorA.X_UD / m_Number, m_IsoVectorA.Y_LR / m_Number, m_IsoVectorA.H_TB / m_Number);

    public static bool operator ==(IsoVector m_IsoVectorA, IsoVector m_IsoVectorB)
    {
        return m_IsoVectorA.X_UD == m_IsoVectorB.X_UD && m_IsoVectorA.Y_LR == m_IsoVectorB.Y_LR && m_IsoVectorA.H_TB == m_IsoVectorB.H_TB;
    }

    public static bool operator !=(IsoVector m_IsoVectorA, IsoVector m_IsoVectorB)
    {
        return m_IsoVectorA.X_UD != m_IsoVectorB.X_UD && m_IsoVectorA.Y_LR != m_IsoVectorB.Y_LR && m_IsoVectorA.H_TB != m_IsoVectorB.H_TB;
    }

    //Overide

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString() => $"({X_UD}, {Y_LR}, {H_TB})";
}

//[CustomEditor(typeof(IsoBlock))]
//[CanEditMultipleObjects]
//public class CustomIsoVector : Editor
//{
//    SerializedProperty Pos;
//    SerializedProperty X;
//    SerializedProperty Y;
//    SerializedProperty H;

//    private void OnEnable()
//    {
//        Pos = serializedObject.FindProperty("Pos");
//    }

//    public override void OnInspectorGUI()
//    {
//        serializedObject.Update();
//        EditorGUILayout.PropertyField(Pos);
//        serializedObject.ApplyModifiedProperties();
//    }
//}