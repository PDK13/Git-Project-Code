using UnityEngine;

/// <summary>
/// Working on Vector
/// </summary>
public class Class_Vector
{
    #region Mouse Point on Canvas

    /// <summary>
    /// Mouse Pos on Canvas with Pivot is ( X= 0.5 ; Y = 0.5 )
    /// </summary>
    /// <param name="c_Canvas"></param>
    /// <returns></returns>
    public static Vector2 Get_Pos_MouseOnCanvas(Canvas c_Canvas)
    {

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            c_Canvas.transform as RectTransform,
            Input.mousePosition,
            c_Canvas.worldCamera,
            out Vector2 v3_MousePosOnCanvas);

        return v3_MousePosOnCanvas;
    }

    #endregion

    #region Duration

    public static float Get_Duration(Vector3 v3_Dir)
    {
        return v3_Dir.magnitude;
    }

    public static float Get_Duration_Sqr(Vector3 v3_Dir)
    {
        return v3_Dir.sqrMagnitude;
    }

    #endregion

    #region Distance

    public static float Get_Distance(Vector3 v3_Point_A, Vector3 v3_Point_B)
    {
        return Vector3.Distance(v3_Point_A, v3_Point_B);
    }

    public static float Get_Distance(Transform com_Point_A, Transform com_Point_B)
    {
        return Vector3.Distance(com_Point_A.position, com_Point_B.position);
    }

    #endregion

    #region Vector on Circle

    #region Exchance Deg

    public static float Get_Exchance_Rotate_Script(float f_DegNeedExchance)
    {
        if (f_DegNeedExchance >= 360)
        {
            return f_DegNeedExchance - 360 * (f_DegNeedExchance / 360);
        }
        else
        if (f_DegNeedExchance <= 0)
        {
            return 360 * (Mathf.Abs(f_DegNeedExchance) / 360 + 1) + f_DegNeedExchance;
        }

        return f_DegNeedExchance;
    }

    public static float Get_Exchance_Rotate_Unity(float f_DegNeedExchance)
    {
        if (f_DegNeedExchance <= -180)
        {
            return 360 + f_DegNeedExchance;
        }
        else
        if (f_DegNeedExchance >= 180)
        {
            return (360 - f_DegNeedExchance) * -1;
        }

        return f_DegNeedExchance;
    }

    #endregion

    #region XY

    public static Vector3 Get_DegToVector_XY(float f_Angle, float f_Duration)
    {
        return new Vector3(Mathf.Cos(f_Angle * Mathf.Deg2Rad), Mathf.Sin(f_Angle * Mathf.Deg2Rad), 0) * f_Duration;
    }

    public static Vector2 Get_Dir_XY(Vector2 v2_Point_A, Vector2 v2_Point_B)
    {
        return (v2_Point_B - v2_Point_A).normalized;
    }

    public static float Get_DirToDeg_XY(Vector2 v2_Dir_A, Vector2 v2_Dir_B)
    {
        return Vector2.Angle(v2_Dir_A, v2_Dir_B);
    }

    #endregion

    #region XZ

    public static Vector3 Get_DegToVector_XZ(float f_Angle, float f_Duration)
    {
        return new Vector3(Mathf.Cos(f_Angle * Mathf.Deg2Rad), 0, Mathf.Sin(f_Angle * Mathf.Deg2Rad)) * f_Duration;
    }

    public static Vector3 Get_Dir_XZ(Vector3 v3_Point_A, Vector3 v3_Point_B)
    {
        return (v3_Point_B - v3_Point_A).normalized;
    }

    public static float Get_DirToDeg_XZ(Vector3 v3_Dir_A, Vector3 v3_Dir_B)
    {
        Vector2 v2_Dir_A = new Vector2(v3_Dir_A.x, v3_Dir_A.z);
        Vector2 v2_Dir_B = new Vector2(v3_Dir_B.x, v3_Dir_B.z);
        return Vector2.Angle(v2_Dir_A, v2_Dir_B);
    }

    public static float Get_DirToDeg_XZ_MainToCheck(Transform com_Body_Main, Transform com_Body_Check)
    {
        float f_Distance = Vector3.Distance(com_Body_Main.transform.position, com_Body_Check.position);
        float f_Deg = Get_Rot_QuaternionToEuler(com_Body_Main.transform.rotation).y;

        Vector3 v3_DirStart = Get_Dir_XZ(com_Body_Main.transform.position, com_Body_Main.transform.position + Get_DegToVector_XZ(-f_Deg, f_Distance));
        Vector3 v3_DirEnd = Get_Dir_XZ(com_Body_Main.transform.position, com_Body_Main.transform.position + Get_Dir_XZ(com_Body_Main.transform.position, com_Body_Check.position) * f_Distance);

        return Get_DirToDeg_XZ(v3_DirStart, v3_DirEnd);
    }

    #endregion

    #endregion

    #region Rotate & Quaternion

    public static Quaternion Get_Rot_EulerToQuaternion(float f_Deg_X, float f_Deg_Y, float f_Deg_Z)
    {
        return Get_Rot_EulerToQuaternion(new Vector3(f_Deg_X, f_Deg_Y, f_Deg_Z));
    }

    public static Quaternion Get_Rot_EulerToQuaternion(Vector3 v3_Deg)
    {
        Quaternion q_Rotation = Quaternion.Euler(v3_Deg);
        return q_Rotation;
    }

    public static Vector3 Get_Rot_QuaternionToEuler(Quaternion com_Rotation)
    {
        Vector3 v3_Rotation = com_Rotation.eulerAngles;
        return v3_Rotation;
    }

    #endregion

    #region Abs

    public static Vector2 Get_Abs(Vector2 v2_Vector)
    {
        return new Vector2((v2_Vector.x > 0) ? v2_Vector.x : v2_Vector.x * -1, (v2_Vector.y > 0) ? v2_Vector.y : v2_Vector.y * -1);
    }

    public static Vector2Int Get_Abs(Vector2Int v2_Vector)
    {
        return new Vector2Int((v2_Vector.x > 0) ? v2_Vector.x : v2_Vector.x * -1, (v2_Vector.y > 0) ? v2_Vector.y : v2_Vector.y * -1);
    }

    public static Vector3 Get_Abs(Vector3 v3_Vector)
    {
        return new Vector3((v3_Vector.x > 0) ? v3_Vector.x : v3_Vector.x * -1, (v3_Vector.y > 0) ? v3_Vector.y : v3_Vector.y * -1, (v3_Vector.z > 0) ? v3_Vector.z : v3_Vector.z * -1);
    }

    public static Vector3Int Get_Abs(Vector3Int v3_Vector)
    {
        return new Vector3Int((v3_Vector.x > 0) ? v3_Vector.x : v3_Vector.x * -1, (v3_Vector.y > 0) ? v3_Vector.y : v3_Vector.y * -1, (v3_Vector.z > 0) ? v3_Vector.z : v3_Vector.z * -1);
    }

    #endregion

    #region Vector and VectorInt

    public static Vector2Int Get_VectorInt(Vector2 v2_Vector)
    {
        return new Vector2Int((int)v2_Vector.x, (int)v2_Vector.y);
    }

    public static Vector3Int Get_VectorInt(Vector3 v3_Vector)
    {
        return new Vector3Int((int)v3_Vector.x, (int)v3_Vector.y, (int)v3_Vector.z);
    }

    public static Vector2 Get_Vector(Vector2Int v2_Vector)
    {
        return new Vector2(v2_Vector.x, v2_Vector.y);
    }

    public static Vector3 Get_Vector(Vector3Int v3_Vector)
    {
        return new Vector3(v3_Vector.x, v3_Vector.y, v3_Vector.z);
    }

    #endregion

    #region Vector

    public static Vector3 Get_Vector(float f_X, float f_Y, float f_Z)
    {
        return new Vector3(f_X, f_Y, f_Z);
    }

    public static Vector2 Get_Vector(float f_X, float f_Y)
    {
        return new Vector2(f_X, f_Y);
    }

    #endregion
}