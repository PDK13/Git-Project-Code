using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteAlways]
public class IsoBlock : MonoBehaviour
{
    [Header("World Manager")]

    [SerializeField] private IsoType m_Type = IsoType.Block;

    [SerializeField] private string m_Tag = "";

    [SerializeField] private IsoVector m_Pos = new IsoVector();

    [SerializeField] private IsoVector m_Scale = new IsoVector(1f, 1f, 1f);

    [SerializeField] private Vector3 m_Centre = new Vector3(0f, 0f, 0f);

    [Header("Matrix Manager")]

    [SerializeField] private IsoVector m_PosMatrix = new IsoVector();

    [SerializeField] [Range(0.5f, 0.6f)] private float m_RoundUpper = 0.55f;

    [SerializeField] [Range(0.4f, 0.5f)] private float m_RoundLower = 0.45f;

    private IsoVector m_PosPrimary = new IsoVector();

    [Header("Code Manager")]

    private List<IsoCode> m_Code = new List<IsoCode>();

#if UNITY_EDITOR

    private void Update()
    {
        SetIsoTransform();
    }

#endif

    #region ================================================================== Type

    public IsoType Type
    {
        get
        {
            return m_Type;
        }
        set
        {
            m_Type = value;
        }
    }

    public bool TypeIsBlock
    {
        get => m_Type == IsoType.Block;
    }

    public bool TypeIsPlayer
    {
        get => m_Type == IsoType.Player;
    }

    public bool TypeIsFriend
    {
        get => m_Type == IsoType.Friend;
    }

    public bool TypeIsNeutral
    {
        get => m_Type == IsoType.Neutral;
    }

    public bool TypeIsEnermy
    {
        get => m_Type == IsoType.Enermy;
    }

    public bool TypeIsCharacter
    {
        get => TypeIsPlayer || TypeIsFriend || TypeIsNeutral || TypeIsEnermy;
    }

    public bool TypeIsObject
    {
        get => m_Type == IsoType.Object;
    }

    #region ================================================================== Iso

    public string Tag
    {
        get => m_Tag;
        set
        {
            m_Tag = value;
        }
    }

    public IsoVector Scale
    {
        get => m_Scale;
        set
        {
            m_Scale = value;
        }
    }

    public Vector3 Centre
    {
        get => m_Centre;
        set
        {
            m_Centre = value;
        }
    }

    private Vector3 GetIsoScene(IsoVector m_PosWorld)
    {
        IsoVector m_PosWorldScale = new IsoVector(m_PosWorld);
        m_PosWorldScale.X_UD *= m_Scale.X_UD * -1;
        m_PosWorldScale.Y_LR *= m_Scale.Y_LR;
        m_PosWorldScale.H_TB *= m_Scale.H_TB;

        float m_PosX = m_PosWorldScale.X_UD + m_PosWorldScale.Y_LR;
        float m_PosY = 0.5f * (m_PosWorldScale.Y_LR - m_PosWorldScale.X_UD) + m_PosWorldScale.H_TB;
        float m_PosZ = (m_PosWorldScale.Y_LR - m_PosWorldScale.X_UD) - m_PosWorldScale.H_TB;

        return new Vector3(m_PosX, m_PosY, m_PosZ);
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

    #region ================================================================== Pos Set

    public IsoVector Pos
    {
        get => m_Pos;
        set
        {
            m_Pos = value;
            SetIsoTransform();
        }
    }

    public IsoVector PosPrimary
    {
        get => m_PosPrimary;
        set
        {
            m_PosPrimary = value;
        }
    }

    public IsoVector PosMatrix
    {
        get => m_PosMatrix;
        private set
        {
            m_PosMatrix = value;
        }
    }

    #endregion

    #region ================================================================== Code

    public List<IsoCode> Code
    {
        get => m_Code;
    }

    #endregion
}

public enum IsoType { Block, Player, Friend, Neutral, Enermy, Object }

public enum IsoDir { None, Up, Down, Left, Right, Top, Bot }

public enum IsoDepth { Fixed }

[System.Serializable]
public struct IsoCode
{
    private string m_Type;
    private string m_Command;

    public string Type
    {
        get => m_Type;
        set
        {
            m_Type = value;
        }
    }

    public string Command
    {
        get => m_Command;
        set
        {
            m_Command = value;
        }
    }

    public IsoCode(string m_Type, string m_Command)
    {
        this.m_Type = m_Type;
        this.m_Command = m_Command;
    }
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
        get => (int)X_UD;
    }

    public int Y_LRInt
    {
        get => (int)Y_LR;
    }

    public int H_TBInt
    {
        get => (int)H_TB;
    }

    //Primary Dir

    public IsoVector Up
    {
        get => new IsoVector(1, 0, 0);
    }

    public IsoVector Down
    {
        get => new IsoVector(-1, 0, 0);
    }

    public IsoVector Left
    {
        get => new IsoVector(0, -1, 0);
    }

    public IsoVector Right
    {
        get => new IsoVector(0, 1, 0);
    }

    public IsoVector Top
    {
        get => new IsoVector(0, 0, 1);
    }

    public IsoVector Bot
    {
        get => new IsoVector(0, 0, -1);
    }

    //If Move-To Dir

    public IsoVector IfUp
    {
        get => this + Up;
    }

    public IsoVector IfDown
    {
        get => this + Down;
    }

    public IsoVector IfLeft
    {
        get => this + Left;
    }

    public IsoVector IfRight
    {
        get => this + Right;
    }

    public IsoVector IfTop
    {
        get => this + Top;
    }

    public IsoVector IfBot
    {
        get => this + Bot;
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