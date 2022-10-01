using UnityEngine;

public class IsoDataMoveSingle
{
    [Tooltip("Move Dir")]
    private Vector3Int v3_Dir;

    [Tooltip("Move Length")]
    private int i_Length;

    [Tooltip("Move Pos-Move-To")]
    private Vector3Int v3_PosMoveTo;

    [Tooltip("Move Speed")]
    private float f_Speed;

    public IsoDataMoveSingle(Vector3Int v3_Dir, int i_Length, float f_Speed, Vector3Int v3_PosMoveTo)
    {
        Set(v3_Dir, i_Length, f_Speed, v3_PosMoveTo);
    }

    public IsoDataMoveSingle(Vector3Int v3_Dir, int i_Length, float f_Speed)
    {
        Set(v3_Dir, i_Length, f_Speed);
    }

    #region Move Single Set 

    public void Set(Vector3Int v3_Dir, int i_Length, float f_Speed, Vector3Int v3_PosMoveTo)
    {
        this.v3_Dir = v3_Dir;
        this.i_Length = i_Length;
        this.v3_PosMoveTo = v3_PosMoveTo;
        this.f_Speed = f_Speed;
    }

    public void Set(Vector3Int v3_Dir, int i_Length, float f_Speed)
    {
        this.v3_Dir = v3_Dir;
        this.i_Length = i_Length;
        this.f_Speed = f_Speed;
    }

    public void Set_Speed(float f_Speed)
    {
        this.f_Speed = f_Speed;
    }

    #endregion

    #region Move Single Get 

    public Vector3Int Get_Dir()
    {
        return v3_Dir;
    }

    public int Get_Length()
    {
        return i_Length;
    }

    public Vector3Int Get_PosMoveTo()
    {
        return v3_PosMoveTo;
    }

    public float Get_Speed()
    {
        return f_Speed;
    }

    #endregion
}
