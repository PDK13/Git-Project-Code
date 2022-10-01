using UnityEngine;

public class ClassVector
{
    #region Dir & Vector

    public static Vector3 GetDir(Vector3 m_Point_A, Vector3 m_Point_B)
    {
        return (m_Point_B - m_Point_A).normalized;
    }

    public static Vector3 GetDir(Transform m_Point_A, Transform m_Point_B)
    {
        return (m_Point_B.position - m_Point_A.position).normalized;
    }

    #endregion

    #region Duration

    public static float GetDuration(Vector3 m_Vector)
    {
        return m_Vector.magnitude;
    }

    public static float GetDurationSqr(Vector3 m_Vector)
    {
        return m_Vector.sqrMagnitude;
    }

    #endregion

    #region Distance

    public static float GetDistance(Vector3 m_Point_A, Vector3 m_Point_B)
    {
        return Vector3.Distance(m_Point_A, m_Point_B);
    }

    public static float GetDistance(Transform m_Point_A, Transform m_Point_B)
    {
        return Vector3.Distance(m_Point_A.position, m_Point_B.position);
    }

    #endregion

    #region Deg

    public static float GetDegExchanceCircle(float m_Deg)
    {
        if (m_Deg >= 360)
        {
            return m_Deg - 360 * (m_Deg / 360);
        }
        else
        if (m_Deg <= 0)
        {
            return 360 * (Mathf.Abs(m_Deg) / 360 + 1) + m_Deg;
        }

        return m_Deg;
    }

    public static float GetDegExchanceUnity(float m_Deg_Euler)
    {
        if (m_Deg_Euler <= -180)
        {
            return 360 + m_Deg_Euler;
        }
        else
        if (m_Deg_Euler >= 180)
        {
            return (360 - m_Deg_Euler) * -1;
        }

        return m_Deg_Euler;
    }

    public static float GetDegTransformXY(Quaternion m_QuaternionRotation)
    {
        return GetRotationQuaternionToEuler(m_QuaternionRotation).z;
    }

    public static float GetDegTransformXZ(Quaternion m_QuaternionRotation)
    {
        return GetRotationQuaternionToEuler(m_QuaternionRotation).y;
    }

    #endregion

    #region Rotate & Quaternion

    public static Quaternion GetRotationEulerToQuaternion(float m_Deg_X, float m_Deg_Y, float m_Deg_Z)
    {
        return GetRotationEulerToQuaternion(new Vector3(m_Deg_X, m_Deg_Y, m_Deg_Z));
    }

    public static Quaternion GetRotationEulerToQuaternion(Vector3 m_Deg)
    {
        Quaternion q_Rotation = Quaternion.Euler(m_Deg);
        return q_Rotation;
    }

    public static Vector3 GetRotationQuaternionToEuler(Quaternion m_QuaternionRotation)
    {
        Vector3 m_EulerRotation = m_QuaternionRotation.eulerAngles;
        return m_EulerRotation;
    }

    #endregion

    #region Circle

    public static Vector3 GetPosOnCircleXY(float m_Deg, float m_Duration)
    {
        return new Vector3(Mathf.Cos(m_Deg * Mathf.Deg2Rad), Mathf.Sin(m_Deg * Mathf.Deg2Rad), 0) * m_Duration;
    }

    public static Vector3 GetPosOnCircleXZ(float m_Deg, float m_Duration)
    {
        return new Vector3(Mathf.Cos(m_Deg * Mathf.Deg2Rad), 0, Mathf.Sin(m_Deg * Mathf.Deg2Rad)) * m_Duration;
    }

    public static float GetDegOnRotationXZ(Transform m_TransformMain, Transform m_TransformTarket)
    {
        float m_Distance = Vector3.Distance(m_TransformMain.transform.position, m_TransformTarket.position);
        float m_Deg = GetRotationQuaternionToEuler(m_TransformMain.transform.rotation).y;

        Vector3 m_DirStart = GetDir(m_TransformMain.transform.position, m_TransformMain.transform.position + GetPosOnCircleXZ(-m_Deg, m_Distance));
        Vector3 m_DirEnd = GetDir(m_TransformMain.transform.position, m_TransformMain.transform.position + GetDir(m_TransformMain.transform.position, m_TransformTarket.position) * m_Distance);

        Vector2 m_Dir_A = new Vector2(m_DirStart.x, m_DirStart.z);
        Vector2 m_Dir_B = new Vector2(m_DirEnd.x, m_DirEnd.z);

        return Vector2.Angle(m_Dir_A, m_Dir_B);
    }

    #endregion

    #region Abs

    public static Vector2 GetAbs(Vector2 m_Vector)
    {
        return new Vector2(Mathf.Abs(m_Vector.x), Mathf.Abs(m_Vector.y));
    }

    public static Vector2Int GetAbs(Vector2Int m_Vector)
    {
        return new Vector2Int(Mathf.Abs(m_Vector.x), Mathf.Abs(m_Vector.y));
    }

    public static Vector3 GetAbs(Vector3 m_Vector)
    {
        return new Vector3(Mathf.Abs(m_Vector.x), Mathf.Abs(m_Vector.y), Mathf.Abs(m_Vector.z));
    }

    public static Vector3Int GetAbs(Vector3Int m_Vector)
    {
        return new Vector3Int(Mathf.Abs(m_Vector.x), Mathf.Abs(m_Vector.y), Mathf.Abs(m_Vector.z));
    }

    #endregion

    #region Vector

    public static Vector3 GetVector(float m_X, float m_Y, float m_Z)
    {
        return new Vector3(m_X, m_Y, m_Z);
    }

    public static Vector2 GetVector(float m_X, float m_Y)
    {
        return new Vector2(m_X, m_Y);
    }

    #endregion
}