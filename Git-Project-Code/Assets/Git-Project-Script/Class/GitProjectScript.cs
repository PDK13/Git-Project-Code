using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

//=================================================== Vector

public class GitVector
{
    #region Dir & Vector

    public static Vector3 GetDir(Vector3 m_PointA, Vector3 m_PointB)
    {
        return (m_PointB - m_PointA).normalized;
    }

    public static Vector3 GetDir(Transform m_PointA, Transform m_PointB)
    {
        return (m_PointB.position - m_PointA.position).normalized;
    }

    public enum Axis { Up, Right, Forward, }

    public static Vector3 GetDir(Vector3 m_DirPrimary, float m_DegRotation, Axis m_VectorAxis)
    {
        Vector3 m_Axis = new Vector3();

        switch (m_VectorAxis)
        {
            case Axis.Up:
                m_Axis = Vector3.up;
                break;
            case Axis.Forward:
                m_Axis = Vector3.forward;
                break;
            case Axis.Right:
                m_Axis = Vector3.right;
                break;
        }

        return Quaternion.AngleAxis(m_DegRotation, m_Axis) * m_DirPrimary;
    }

    public static Vector3 GetPos(Vector3 m_PosPrimary, Vector3 m_DirPrimary, float m_DegRotation, Axis m_VectorAxis)
    {
        return m_PosPrimary + GetDir(m_DirPrimary, m_DegRotation, m_VectorAxis);
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

    public static float GetDistance(Vector3 m_PointA, Vector3 m_PointB)
    {
        return Vector3.Distance(m_PointA, m_PointB);
    }

    public static float GetDistance(Transform m_PointA, Transform m_PointB)
    {
        return Vector3.Distance(m_PointA.position, m_PointB.position);
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

    public static float GetDegExchanceUnity(float m_DegEuler)
    {
        if (m_DegEuler <= -180)
        {
            return 360 + m_DegEuler;
        }
        else
        if (m_DegEuler >= 180)
        {
            return (360 - m_DegEuler) * -1;
        }

        return m_DegEuler;
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

    #region Transform & Rotation & Quaternion

    //New Code (Should use)!!

    public static void SetRotate(Transform m_Transform, Vector3 m_RotateDeg, bool m_RotateWorld = true)
    {
        if (m_RotateWorld)
            m_Transform.eulerAngles = m_RotateDeg;
        else
            m_Transform.localEulerAngles = m_RotateDeg;
    }

    public static Vector3 GetRotate(Transform m_Transform, bool m_RotateWorld = true)
    {
        if (m_RotateWorld)
            return m_Transform.eulerAngles;
        else
            return m_Transform.localEulerAngles;
    }

    //New Code (Should use)!!

    //Old Code (Should delete)!!

    public static void SetRotationXY(Transform m_Transform, float m_Rotation)
    {
        m_Transform.rotation = GetRotationEulerToQuaternion(0, 0, m_Rotation);
    }

    public static void SetRotationXZ(Transform m_Transform, float m_Rotation)
    {
        m_Transform.rotation = GetRotationEulerToQuaternion(new Vector3(0, m_Rotation, 0));
    }

    public static void SetRotationChanceXY(Transform m_Transform, float m_RotationChance)
    {
        float m_Deg = GetDegExchanceUnity(GetRotationQuaternionToEuler(m_Transform.rotation).z);
        SetRotationXY(m_Transform, m_Deg + m_RotationChance);
    }

    public static void SetRotationChanceXZ(Transform m_Transform, float m_RotationChance)
    {
        float m_Deg = GetDegExchanceUnity(GetRotationQuaternionToEuler(m_Transform.rotation).y);
        SetRotationXZ(m_Transform, m_Deg + m_RotationChance);
    }

    public static Quaternion GetRotationEulerToQuaternion(float m_DegX, float m_DegY, float m_DegZ)
    {
        return GetRotationEulerToQuaternion(new Vector3(m_DegX, m_DegY, m_DegZ));
    }

    public static Quaternion GetRotationEulerToQuaternion(Vector3 m_Deg)
    {
        Quaternion m_Quaternion = Quaternion.Euler(m_Deg);
        return m_Quaternion;
    }

    public static Vector3 GetRotationQuaternionToEuler(Quaternion m_QuaternionRotation)
    {
        Vector3 m_EulerRotation = m_QuaternionRotation.eulerAngles;
        return m_EulerRotation;
    }

    //Old Code (Should delete)!!

    #endregion

    #region Circle

    public static Vector3 GetPosOnCircleXY(float m_Deg, float m_Radius)
    {
        return new Vector3(Mathf.Cos(m_Deg * Mathf.Deg2Rad), Mathf.Sin(m_Deg * Mathf.Deg2Rad), 0) * m_Radius;
    }

    public static Vector3 GetPosOnCircleXZ(float m_Deg, float m_Radius)
    {
        return new Vector3(Mathf.Cos(m_Deg * Mathf.Deg2Rad), 0, Mathf.Sin(m_Deg * Mathf.Deg2Rad)) * m_Radius;
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
}

//=================================================== Cast

public class GitCast
{
    #region 3D

    //None LayerMask

    public static GameObject GetLineCast(Vector3 m_PosStart, Vector3 m_PosEnd)
    {
        RaycastHit m_RaycastHit = new RaycastHit();
        Physics.Linecast(m_PosStart, m_PosEnd, out m_RaycastHit);

        if (m_RaycastHit.collider == null) return null;

        return m_RaycastHit.collider.gameObject;
    }

    public static GameObject GetRaycast(Vector3 m_PosStart, Vector3 m_PosEnd, float m_Distance)
    {
        Vector3 m_Forward = (m_PosEnd - m_PosStart).normalized;

        RaycastHit m_RaycastHit = new RaycastHit();
        Physics.Raycast(m_PosStart, m_Forward, out m_RaycastHit, m_Distance);

        if (m_RaycastHit.collider == null) return null;

        return m_RaycastHit.collider.gameObject;
    }

    public static GameObject GetBoxCast(Vector3 m_PosStart, Vector3 m_PosEnd, Vector3 m_Size, Vector3 m_Rotation, float m_Distance)
    {
        Vector3 m_Forward = (m_PosEnd - m_PosStart).normalized;
        Quaternion m_Quaternion = Quaternion.Euler(m_Rotation);

        RaycastHit m_RaycastHit = new RaycastHit();
        Physics.BoxCast(m_PosStart, m_Size, m_Forward, out m_RaycastHit, m_Quaternion, m_Distance);

        if (m_RaycastHit.collider == null) return null;

        return m_RaycastHit.collider.gameObject;
    }

    public static GameObject GetSphereCast(Vector3 m_PosStart, Vector3 m_PosEnd, float m_Radius, float m_Distance)
    {
        Vector3 m_Forward = (m_PosEnd - m_PosStart).normalized;

        RaycastHit m_RaycastHit = new RaycastHit();
        Physics.SphereCast(m_PosStart, m_Radius / 2, m_Forward, out m_RaycastHit, m_Distance);

        if (m_RaycastHit.collider == null) return null;

        return m_RaycastHit.collider.gameObject;
    }

    public static List<GameObject> GetBoxOverlap(Vector3 m_PosStart, Vector3 m_Size, Vector3 m_Rotation)
    {
        Quaternion m_Quaternion = Quaternion.Euler(m_Rotation);
        Collider[] m_ObjectsHit = Physics.OverlapBox(m_PosStart, m_Size, m_Quaternion);

        List<GameObject> m_ObjectsHitList = new List<GameObject>();
        foreach (Collider m_ObjectHit in m_ObjectsHit) m_ObjectsHitList.Add(m_ObjectHit.gameObject);

        return m_ObjectsHitList;
    }

    public static List<GameObject> GetCircleOverlap(Vector3 m_PosStart, float m_Size)
    {
        Collider[] m_ObjectsHit = Physics.OverlapSphere(m_PosStart, m_Size);

        List<GameObject> m_ObjectsHitList = new List<GameObject>();
        foreach (Collider m_ObjectHit in m_ObjectsHit) m_ObjectsHitList.Add(m_ObjectHit.gameObject);

        return m_ObjectsHitList;
    }

    //LayerMask

    public static GameObject GetLineCast(Vector3 m_PosStart, Vector3 m_PosEnd, LayerMask m_Tarket)
    {
        RaycastHit m_RaycastHit = new RaycastHit();

        Physics.Linecast(m_PosStart, m_PosEnd, out m_RaycastHit, m_Tarket);

        if (m_RaycastHit.collider == null) return null;

        return m_RaycastHit.collider.gameObject;
    }

    public static GameObject GetRaycast(Vector3 m_PosStart, Vector3 m_PosEnd, float m_Distance, LayerMask m_Tarket)
    {
        Vector3 m_Forward = (m_PosEnd - m_PosStart).normalized;

        RaycastHit m_RaycastHit = new RaycastHit();
        Physics.Raycast(m_PosStart, m_Forward, out m_RaycastHit, m_Distance, m_Tarket);

        if (m_RaycastHit.collider == null) return null;

        return m_RaycastHit.collider.gameObject;
    }

    public static GameObject GetBoxCast(Vector3 m_PosStart, Vector3 m_PosEnd, Vector3 m_Size, Vector3 m_Rotation, float m_Distance, LayerMask m_Tarket)
    {
        Vector3 m_Forward = (m_PosEnd - m_PosStart).normalized;
        Quaternion m_Quaternion = Quaternion.Euler(m_Rotation);

        RaycastHit m_RaycastHit = new RaycastHit();
        Physics.BoxCast(m_PosStart, m_Size, m_Forward, out m_RaycastHit, m_Quaternion, m_Distance, m_Tarket);

        if (m_RaycastHit.collider == null) return null;

        return m_RaycastHit.collider.gameObject;
    }

    public static GameObject GetSphereCast(Vector3 m_PosStart, Vector3 m_PosEnd, float m_Radius, float m_Distance, LayerMask m_Tarket)
    {
        Vector3 m_Forward = (m_PosEnd - m_PosStart).normalized;

        RaycastHit m_RaycastHit = new RaycastHit();
        Physics.SphereCast(m_PosStart, m_Radius / 2, m_Forward, out m_RaycastHit, m_Distance, m_Tarket);

        if (m_RaycastHit.collider == null) return null;

        return m_RaycastHit.collider.gameObject;
    }

    public static List<GameObject> GetBoxOverlap(Vector3 m_PosStart, Vector3 m_Size, Vector3 m_Rotation, LayerMask m_Tarket)
    {
        Quaternion m_Quaternion = Quaternion.Euler(m_Rotation);

        Collider[] m_ObjectsHit = Physics.OverlapBox(m_PosStart, m_Size, m_Quaternion, m_Tarket);

        List<GameObject> m_ObjectsHitList = new List<GameObject>();
        foreach (Collider m_ObjectHit in m_ObjectsHit) m_ObjectsHitList.Add(m_ObjectHit.gameObject);

        return m_ObjectsHitList;
    }

    public static List<GameObject> GetCircleOverlap(Vector3 m_PosStart, float m_Size, LayerMask m_Tarket)
    {
        Collider[] m_ObjectsHit = Physics.OverlapSphere(m_PosStart, m_Size, m_Tarket);

        List<GameObject> m_ObjectsHitList = new List<GameObject>();
        foreach (Collider m_ObjectHit in m_ObjectsHit) m_ObjectsHitList.Add(m_ObjectHit.gameObject);

        return m_ObjectsHitList;
    }

    #endregion

    #region 2D

    //None LayerMask

    public static GameObject GetLineCast2D(Vector3 m_PosStart, Vector3 m_PosEnd)
    {
        RaycastHit2D m_RaycastHit = Physics2D.Linecast(m_PosStart, m_PosEnd);

        if (m_RaycastHit.collider == null) return null;

        return m_RaycastHit.collider.gameObject;
    }

    public static GameObject GetRaycast2D(Vector3 m_PosStart, Vector3 m_PosEnd, float m_Distance)
    {
        Vector3 m_Forward = (m_PosEnd - m_PosStart).normalized;

        RaycastHit2D m_RaycastHit = Physics2D.Raycast(m_PosStart, m_Forward, m_Distance);

        if (m_RaycastHit.collider == null) return null;

        return m_RaycastHit.collider.gameObject;
    }

    public static GameObject GetBoxCast2D(Vector3 m_PosStart, Vector3 m_PosEnd, Vector3 m_Size, float m_Rotation, float m_Distance)
    {
        Vector3 m_Forward = (m_PosEnd - m_PosStart).normalized;

        RaycastHit2D m_RaycastHit = Physics2D.BoxCast(m_PosStart, m_Size, m_Rotation, m_Forward, m_Distance);

        if (m_RaycastHit.collider == null) return null;

        return m_RaycastHit.collider.gameObject;
    }

    public static GameObject GetCircleCast2D(Vector3 m_PosStart, Vector3 m_PosEnd, float m_Radius, float m_Distance)
    {
        Vector3 m_Forward = (m_PosEnd - m_PosStart).normalized;

        RaycastHit2D m_RaycastHit = Physics2D.CircleCast(m_PosStart, m_Radius / 2, m_Forward, m_Distance);

        if (m_RaycastHit.collider == null) return null;

        return m_RaycastHit.collider.gameObject;
    }

    public static GameObject GetOverlapCircle2D(Vector3 m_PosStart, float m_Radius)
    {
        Collider2D m_ColliderHit = Physics2D.OverlapCircle(m_PosStart, m_Radius);

        if (m_ColliderHit == null) return null;

        return m_ColliderHit.gameObject;
    }

    public static List<GameObject> GetOverlapCircleAll2D(Vector3 m_PosStart, float m_Radius)
    {
        Collider2D[] m_ColliderHit = Physics2D.OverlapCircleAll(m_PosStart, m_Radius);

        List<GameObject> m_ColliderHitList = new List<GameObject>();
        for (int i = 0; i < m_ColliderHit.Length; i++) m_ColliderHitList.Add(m_ColliderHit[i].gameObject);

        return m_ColliderHitList;
    }

    public static GameObject GetOverlapBox2D(Vector3 m_PosStart, Vector3 m_Size, float m_Rotation)
    {
        Collider2D m_ColliderHit = Physics2D.OverlapBox(m_PosStart, m_Size, m_Rotation);

        if (m_ColliderHit == null) return null;

        return m_ColliderHit.gameObject;
    }

    public static List<GameObject> GetOverlapBoxAll2D(Vector3 m_PosStart, Vector3 m_Size, float m_Rotation)
    {
        Collider2D[] m_ColliderHit = Physics2D.OverlapBoxAll(m_PosStart, m_Size, m_Rotation);

        List<GameObject> m_ColliderHitList = new List<GameObject>();
        for (int i = 0; i < m_ColliderHit.Length; i++) m_ColliderHitList.Add(m_ColliderHit[i].gameObject);

        return m_ColliderHitList;
    }

    //LayerMask

    public static GameObject GetLineCast2D(Vector3 m_PosStart, Vector3 m_PosEnd, LayerMask m_Tarket)
    {
        RaycastHit2D m_RaycastHit = Physics2D.Linecast(m_PosStart, m_PosEnd, m_Tarket);

        if (m_RaycastHit.collider == null) return null;

        return m_RaycastHit.collider.gameObject;
    }

    public static GameObject GetRaycast2D(Vector3 m_PosStart, Vector3 m_PosEnd, float m_Distance, LayerMask m_Tarket)
    {
        Vector3 m_Forward = (m_PosEnd - m_PosStart).normalized;

        RaycastHit2D m_RaycastHit = Physics2D.Raycast(m_PosStart, m_Forward, m_Distance, m_Tarket);

        if (m_RaycastHit.collider == null) return null;

        return m_RaycastHit.collider.gameObject;
    }

    public static GameObject GetBoxCast2D(Vector3 m_PosStart, Vector3 m_PosEnd, Vector3 m_Size, float m_Rotation, float m_Distance, LayerMask m_Tarket)
    {
        Vector3 m_Forward = (m_PosEnd - m_PosStart).normalized;

        RaycastHit2D m_RaycastHit = Physics2D.BoxCast(m_PosStart, m_Size, m_Rotation, m_Forward, m_Distance, m_Tarket);

        if (m_RaycastHit.collider == null) return null;

        return m_RaycastHit.collider.gameObject;
    }

    public static GameObject GetCircleCast2D(Vector3 m_PosStart, Vector3 m_PosEnd, float m_Radius, float m_Distance, LayerMask m_Tarket)
    {
        Vector3 m_Forward = (m_PosEnd - m_PosStart).normalized;

        RaycastHit2D m_RaycastHit = Physics2D.CircleCast(m_PosStart, m_Radius / 2, m_Forward, m_Distance, m_Tarket);

        if (m_RaycastHit.collider == null) return null;

        return m_RaycastHit.collider.gameObject;
    }

    public static GameObject GetOverlapCircle2D(Vector3 m_PosStart, float m_Radius, LayerMask m_Tarket)
    {
        Collider2D m_ColliderHit = Physics2D.OverlapCircle(m_PosStart, m_Radius, m_Tarket);

        if (m_ColliderHit == null) return null;

        return m_ColliderHit.gameObject;
    }

    public static List<GameObject> GetOverlapCircleAll2D(Vector3 m_PosStart, float m_Radius, LayerMask m_Tarket)
    {
        Collider2D[] m_ColliderHit = Physics2D.OverlapCircleAll(m_PosStart, m_Radius, m_Tarket);

        List<GameObject> m_ColliderHitList = new List<GameObject>();
        for (int i = 0; i < m_ColliderHit.Length; i++) m_ColliderHitList.Add(m_ColliderHit[i].gameObject);

        return m_ColliderHitList;
    }

    public static GameObject GetOverlapBox2D(Vector3 m_PosStart, Vector3 m_Size, float m_Rotation, LayerMask m_Tarket)
    {
        Collider2D m_ColliderHit = Physics2D.OverlapBox(m_PosStart, m_Size, m_Rotation, m_Tarket);

        if (m_ColliderHit == null) return null;

        return m_ColliderHit.gameObject;
    }

    public static List<GameObject> GetOverlapBoxAll2D(Vector3 m_PosStart, Vector3 m_Size, float m_Rotation, LayerMask m_Tarket)
    {
        Collider2D[] m_ColliderHit = Physics2D.OverlapBoxAll(m_PosStart, m_Size, m_Rotation, m_Tarket);

        List<GameObject> m_ColliderHitList = new List<GameObject>();
        for (int i = 0; i < m_ColliderHit.Length; i++) m_ColliderHitList.Add(m_ColliderHit[i].gameObject);

        return m_ColliderHitList;
    }

    #endregion
}

//=================================================== Scene

public class GitScene
{
    public static void SetSceneChance(string m_SceneName, LoadSceneMode enumLoadSceneMode = LoadSceneMode.Single)
    {
        SceneManager.LoadScene(m_SceneName, enumLoadSceneMode);
    }

    public static (int Index, string Name) GetSceneCurrent()
    {
        return (GetSceneCurrentBuildIndex(), GetSceneCurrentName());
    }

    public static string GetSceneCurrentName()
    {
        return SceneManager.GetActiveScene().name;
    }

    public static int GetSceneCurrentBuildIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
}

//=================================================== Resolution

public class GitResolution
{
    #region Convert

    public enum GitSizeUnitScaleType { Width, Height, Span, Primary, Tarket, }

    public static Vector2 GetSizeUnitScaled(Sprite m_SpritePrimary, Sprite m_SpriteTarket, GitSizeUnitScaleType m_SpriteScale)
    {
        return GetSizeUnitScaled(GitSprite.GetSpriteSizeUnit(m_SpritePrimary), GitSprite.GetSpriteSizeUnit(m_SpriteTarket), m_SpriteScale);
    }

    public static Vector2 GetSizeUnitScaled(Vector2 m_SizeUnitPrimary, Vector2 m_SizeUnitTarket, GitSizeUnitScaleType m_SpriteScale)
    {
        Vector2 m_SizeUnitFinal = new Vector2();

        switch (m_SpriteScale)
        {
            case GitSizeUnitScaleType.Width:
                {
                    float m_OffsetX = m_SizeUnitTarket.x / m_SizeUnitPrimary.x;
                    float m_SizeUnitFinalX = m_SizeUnitPrimary.x * m_OffsetX;
                    float m_SizeUnitFinalY = m_SizeUnitPrimary.y * m_OffsetX;
                    m_SizeUnitFinal = new Vector2(m_SizeUnitFinalX, m_SizeUnitFinalY);
                }
                break;
            case GitSizeUnitScaleType.Height:
                {
                    float m_OffsetY = m_SizeUnitTarket.y / m_SizeUnitPrimary.y;
                    float m_SizeUnitFinalX = m_SizeUnitPrimary.x * m_OffsetY;
                    float m_SizeUnitFinalY = m_SizeUnitPrimary.y * m_OffsetY;
                    m_SizeUnitFinal = new Vector2(m_SizeUnitFinalX, m_SizeUnitFinalY);
                }
                break;
            case GitSizeUnitScaleType.Span:
                {
                    float m_OffsetX = m_SizeUnitTarket.x / m_SizeUnitPrimary.x;
                    float m_OffsetY = m_SizeUnitTarket.y / m_SizeUnitPrimary.y;
                    if (m_OffsetX < m_OffsetY)
                    {
                        m_SizeUnitFinal = GetSizeUnitScaled(m_SizeUnitPrimary, m_SizeUnitTarket, GitSizeUnitScaleType.Height);
                    }
                    else
                    {
                        m_SizeUnitFinal = GetSizeUnitScaled(m_SizeUnitPrimary, m_SizeUnitTarket, GitSizeUnitScaleType.Width);
                    }
                }
                break;
            case GitSizeUnitScaleType.Primary:
                m_SizeUnitFinal = m_SizeUnitPrimary;
                break;
            case GitSizeUnitScaleType.Tarket:
                m_SizeUnitFinal = m_SizeUnitTarket;
                break;
        }

        return m_SizeUnitFinal;
    }

    #endregion

    #region Screen

    public static Vector2 GetScreenSizePixel()
    {
        return new Vector2(Screen.width, Screen.height);
    }

    #endregion

    #region Camera

    //CAMERA mode ORTHOGRAPHIC - SIZE is a HALF number of UNIT WORLD HEIGHT from Scene to Screen.
    //EX: If Camera orthographic Size is 1, mean need 2 Square 1x1 Unit world to fill full HEIGHT of screen.

    //MAIN CAMERA need tag MAIN CAMERA to find!

    public static Vector2 GetCameraSizePixel()
    {
        return GetCameraSizePixel(Camera.main);
    }

    public static Vector2 GetCameraSizeUnit()
    {
        return GetCameraSizeUnit(Camera.main);
    }

    public static Vector2 GetCameraSizePixel(Camera m_Camera)
    {
        return new Vector2(m_Camera.pixelWidth, m_Camera.pixelHeight);
    }

    public static Vector2 GetCameraSizeUnit(Camera m_Camera)
    {
        Vector2 m_SizePixel = GetCameraSizePixel(m_Camera);
        float m_HeightUnit = m_Camera.orthographicSize * 2;
        float m_WidthUnit = m_HeightUnit * (m_SizePixel.x / m_SizePixel.y);

        return new Vector2(m_WidthUnit, m_HeightUnit);
    }

    #endregion
}

//=================================================== Sprite & Sprite Renderer

public class GitSprite
{
    #region Sprite

    public static Vector2 GetSpriteSizePixel(Sprite m_Sprite)
    {
        return GetSpriteSizeUnit(m_Sprite) * GetSpritePixelPerUnit(m_Sprite) * 1.0f;
    }

    public static Vector2 GetSpriteSizeUnit(Sprite m_Sprite)
    {
        return m_Sprite.bounds.size * 1.0f;
    }

    public static float GetSpritePixelPerUnit(Sprite m_Sprite)
    {
        return m_Sprite.pixelsPerUnit * 1.0f;
    }

    #endregion

    #region Sprite-Renderer

    #endregion

    #region Texture

    //Texture can be used for Window Editor (Button)

    //Ex:
    //Window Editor:
    //Texture2D m_Texture = GitSprite.GetTextureConvert(m_Sprite);
    //GUIContent m_Content = new GUIContent("", (Texture)m_Texture);
    //GUILayout.Button(m_Content());

    public static Texture2D GetTextureConvert(Sprite m_Sprite)
    {
        if (m_Sprite.texture.isReadable == false)
        {
            return null;
        }

        Texture2D m_Texture = new Texture2D((int)m_Sprite.rect.width, (int)m_Sprite.rect.height);

        Color[] m_ColorPixel = m_Sprite.texture.GetPixels(
            (int)m_Sprite.textureRect.x,
            (int)m_Sprite.textureRect.y,
            (int)m_Sprite.textureRect.width,
            (int)m_Sprite.textureRect.height);
        m_Texture.SetPixels(m_ColorPixel);
        m_Texture.Apply();
        return m_Texture;
    }

    #endregion
}

//=================================================== GameObject

public class GitGameObject
{
    public static GameObject SetGameObjectCreate(GameObject m_Prepab, Transform m_Parent = null)
    {
        if (m_Parent == null)
        {
            return MonoBehaviour.Instantiate(m_Prepab);
        }
        else
        {
            return MonoBehaviour.Instantiate(m_Prepab, m_Parent);
        }
    }

    public static GameObject SetGameObjectCreate(string m_Prefab_Name, Transform m_Parent = null)
    {
        GameObject m_GameObject = new GameObject(m_Prefab_Name);

        if (m_Parent != null)
        {
            m_GameObject.transform.SetParent(m_Parent);
        }

        return m_GameObject;
    }

    public static void SetGameObjectDestroy(UnityEngine.Object m_Object)
    {
        if (m_Object != null)
        {
            MonoBehaviour.Destroy(m_Object);
        }
    }

    public static void SetComponentDestroy(UnityEngine.Object m_Object)
    {
        if (m_Object != null)
        {
            MonoBehaviour.Destroy(m_Object);
        }
    }

    public static string GetNameReplaceClone(string m_GameObjectName)
    {
        return m_GameObjectName.Replace(m_GameObjectName, "(Clone)");
    }
}

//=================================================== PlayerPref

public class GitPlayerPref
{
    #region Player Prefs Set

    public static void SetPlayerPrefsClearAll()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

    public static void SetPlayerPrefsClear(string m_ValueName)
    {
        if (!GetPlayerPrefsExist(m_ValueName))
        {
            Debug.LogError("SetPlayerPrefsClear: Not Exist" + "\"" + m_ValueName + "\"");
            return;
        }
        PlayerPrefs.DeleteKey(m_ValueName);
        PlayerPrefs.Save();
    }

    public static void SetPlayerPrefs(string m_ValueName, string m_Value)
    {
        PlayerPrefs.SetString(m_ValueName, m_Value);
        PlayerPrefs.Save();
    }

    public static void SetPlayerPrefs(string m_ValueName, int m_Value)
    {
        PlayerPrefs.SetInt(m_ValueName, m_Value);
        PlayerPrefs.Save();
    }

    public static void SetPlayerPrefs(string m_ValueName, float m_Value)
    {
        PlayerPrefs.SetFloat(m_ValueName, m_Value);
        PlayerPrefs.Save();
    }

    public static void SetPlayerPrefs(string m_ValueName, bool m_Value)
    {
        PlayerPrefs.SetInt(m_ValueName, (m_Value) ? 1 : 0);
        PlayerPrefs.Save();
    }

    public static void SetPlayerPrefs(string m_ValueName, Vector2 m_Value)
    {
        SetPlayerPrefs(m_ValueName + "X", m_Value.x);
        SetPlayerPrefs(m_ValueName + "Y", m_Value.y);
    }

    public static void SetPlayerPrefs(string m_ValueName, Vector3 m_Value)
    {
        SetPlayerPrefs(m_ValueName + "X", m_Value.x);
        SetPlayerPrefs(m_ValueName + "Y", m_Value.y);
        SetPlayerPrefs(m_ValueName + "Z", m_Value.z);
    }

    #endregion

    #region Player Prefs Get

    public static bool GetPlayerPrefsExist(string m_ValueName)
    {
        return PlayerPrefs.HasKey(m_ValueName);
    }

    public static string GetPlayerPrefsString(string m_ValueName, string m_ValueDefault = "")
    {
        return PlayerPrefs.GetString(m_ValueName, m_ValueDefault);
    }

    public static int GetPlayerPrefsInt(string m_ValueName, int m_ValueDefault = 0)
    {
        return PlayerPrefs.GetInt(m_ValueName, m_ValueDefault);
    }

    public static float GetPlayerPrefsFloat(string m_ValueName, float m_ValueDefault = 0.0f)
    {
        return PlayerPrefs.GetFloat(m_ValueName, m_ValueDefault);
    }

    public static bool GetPlayerPrefsBool(string m_ValueName, bool m_ValueDefault = false)
    {
        if (PlayerPrefs.GetInt(m_ValueName, 0) == 1)
        {
            return true;
        }
        return m_ValueDefault;
    }

    public static Vector2 SetPlayerPrefsVector2(string m_ValueName)
    {
        Vector2 m_Value = new Vector2();

        m_Value.x = GetPlayerPrefsFloat(m_ValueName + "X");
        m_Value.y = GetPlayerPrefsFloat(m_ValueName + "Y");

        return m_Value;
    }

    public static Vector3 SetPlayerPrefsVector3(string m_ValueName)
    {
        Vector3 m_Value = new Vector2();

        m_Value.x = GetPlayerPrefsFloat(m_ValueName + "X");
        m_Value.y = GetPlayerPrefsFloat(m_ValueName + "Y");
        m_Value.z = GetPlayerPrefsFloat(m_ValueName + "Z");

        return m_Value;
    }

    #endregion

    #region App First Run

    public static bool SetPlayerPrefFirstRun(string m_PlayerPref = "-First-Run")
    {
        if (GetPlayerPrefsExist(Application.productName + m_PlayerPref))
        {
            return false;
        }

        SetPlayerPrefs(Application.productName + m_PlayerPref, true);

        return true;
    }

    #endregion
}

//=================================================== Resources

public class GitResources
{
    //NOTE:
    //Folder(s) "Resources" can be created everywhere from root "Assests/*", that can be access by Unity or Application

    //BEWARD:
    //All content(s) in folder(s) "Resources" will be builded to Application, even they m_ightn't be used in Build-Game Application

    public static List<GameObject> GetResourcesPrefab(string m_PathInResources)
    {
        m_PathInResources.Replace(m_PathInResources, "Assets/resources/");
        GameObject[] m_PrefamArray = Resources.LoadAll<GameObject>(m_PathInResources);
        List<GameObject> m_PrefamList = new List<GameObject>();
        m_PrefamList.AddRange(m_PrefamArray);
        return m_PrefamList;
    }

    public static List<Sprite> GetResourcesSprite(string m_PathInResources)
    {
        m_PathInResources.Replace(m_PathInResources, "Assets/resources/");
        Sprite[] m_SpriteArray = Resources.LoadAll<Sprite>(m_PathInResources);
        List<Sprite> m_SpriteList = new List<Sprite>();
        m_SpriteList.AddRange(m_SpriteArray);
        return m_SpriteList;
    }

    public static List<TextAsset> GetResourcesTextAsset(string m_PathInResources)
    {
        m_PathInResources.Replace(m_PathInResources, "Assets/resources/");
        TextAsset[] m_TextAssetArray = Resources.LoadAll<TextAsset>(m_PathInResources);
        List<TextAsset> m_TextAssetList = new List<TextAsset>();
        m_TextAssetList.AddRange(m_TextAssetArray);
        return m_TextAssetList;
    }
}

//=================================================== File

public class GitFile
{
    public const string m_ExamplePath = @"D:/ClassFileIO.txt";

    public enum Path { None, Persistent, Resources, Document, Picture, Music, Video, Assets, }

    public static string GetPath(Path m_FileIOPathType, string m_FileIOName = "", params string[] m_FileIOPath)
    {
        string m_Path = "";

        for (int i = 0; i < m_FileIOPath.Length; i++)
        {
            m_Path += m_FileIOPath[i] + @"/";
        }

        if (m_FileIOName != "")
        {
            //m_Path += m_FileIOName + ".txt";
            m_Path += m_FileIOName;
        }

        switch (m_FileIOPathType)
        {
            case Path.Assets:
                m_Path = Application.dataPath + @"/" + m_Path;
                break;
            case Path.Persistent:
                m_Path = Application.persistentDataPath + @"/" + m_Path;
                break;
            case Path.Resources:
                m_Path = Application.dataPath + @"/resources/" + m_Path;
                break;
            case Path.Document:
                m_Path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"/" + m_Path;
                break;
            case Path.Picture:
                m_Path = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + @"/" + m_Path;
                break;
            case Path.Music:
                m_Path = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic) + @"/" + m_Path;
                break;
            case Path.Video:
                m_Path = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos) + @"/" + m_Path;
                break;
        }

        Debug.LogFormat("Get Path: {0}", m_Path);
        return m_Path;
    }

    public static bool GetPathFileExist(string m_PathFile)
    {
        return File.Exists(m_PathFile);
    }

    public static bool GetPathFolderExist(string m_PathFolder)
    {
        return Directory.Exists(m_PathFolder);
    }
}

//=================================================== Assets Database

#if UNITY_EDITOR

public class GitAssetsDatabase : GitFile
{
    //NOTE:
    //Folder "Assets" is the main root of all assets in project, that can find any assets from it.

    public const string m_ExamplePathAssets = "Assets/Scene";

    public static List<GameObject> GetAssetsPrefab(string m_PathFolderFromAssets)
    {
        if (!GetPathFolderExist(m_PathFolderFromAssets)) return new List<GameObject>();

        List<GameObject> m_ObjectsFound = new List<GameObject>();

        string[] m_GUIDPathUnityFound = AssetDatabase.FindAssets("t:prefab", new string[] { m_PathFolderFromAssets });

        foreach (string m_GUIDPath in m_GUIDPathUnityFound)
        {
            string m_AssetsSinglePath = AssetDatabase.GUIDToAssetPath(m_GUIDPath);
            GameObject m_ObjectFound = AssetDatabase.LoadAssetAtPath<GameObject>(m_AssetsSinglePath);
            m_ObjectsFound.Add(m_ObjectFound);
        }

        return m_ObjectsFound;
    }
}

#endif

//=================================================== FileIO

public class GitFileIO : GitFile
{
    public GitFileIO()
    {
        SetWriteDataClear();
        SetReadDataClear();
    }

    #region File IO Write 

    private string m_TextWrite = "";

    private void SetWriteDatatoFile(string m_Path, string m_Data)
    {
        using (FileStream m_yFile = File.Create(m_Path))
        {
            try
            {
                byte[] m_Info = new UTF8Encoding(true).GetBytes(m_Data);
                m_yFile.Write(m_Info, 0, m_Info.Length);
            }
            catch
            {
                Debug.LogErrorFormat("[Error] File Write Fail: {0}", m_Path);
            }
        }
    }

    public void SetWriteDataClear()
    {
        m_TextWrite = "";
    }

    public void SetWriteDataStart(string m_Path)
    {
        SetWriteDatatoFile(m_Path, GetWriteDataString());
    } //Call Last

    public void SetWriteDataAdd(string m_DataAdd)
    {
        if (m_TextWrite.Length != 0)
        {
            m_TextWrite += "\n";
        }

        m_TextWrite += m_DataAdd;
    }

    public void SetWriteDataAdd(int m_DataAdd)
    {
        if (m_TextWrite.Length != 0)
        {
            m_TextWrite += "\n";
        }

        m_TextWrite += m_DataAdd.ToString();
    }

    public void SetWriteDataAdd(float m_DataAdd)
    {
        if (m_TextWrite.Length != 0)
        {
            m_TextWrite += "\n";
        }

        m_TextWrite += m_DataAdd.ToString();
    }

    public void SetWriteDataAdd(double m_DataAdd)
    {
        if (m_TextWrite.Length != 0)
        {
            m_TextWrite += "\n";
        }

        m_TextWrite += m_DataAdd.ToString();
    }

    public void SetWriteDataAdd(bool m_DataAdd)
    {
        if (m_TextWrite.Length != 0)
        {
            m_TextWrite += "\n";
        }

        m_TextWrite += ((m_DataAdd) ? "True" : "False");
    }

    public void SetWriteDataAdd(Vector2 m_DataAdd, char m_Key)
    {
        SetWriteDataAdd(GitEncypt.GetDataVector2Encypt(m_DataAdd, m_Key));
    }

    public void SetWriteDataAdd(Vector2Int m_DataAdd, char m_Key)
    {
        SetWriteDataAdd(GitEncypt.GetDataVector2IntEncypt(m_DataAdd, m_Key));
    }

    public void SetWriteDataAdd(Vector3 m_DataAdd, char m_Key)
    {
        SetWriteDataAdd(GitEncypt.GetDataVector3Encypt(m_DataAdd, m_Key));
    }

    public void SetWriteDataAdd(Vector3Int m_DataAdd, char m_Key)
    {
        SetWriteDataAdd(GitEncypt.GetDataVector3IntEncypt(m_DataAdd, m_Key));
    }

    public string GetWriteDataString()
    {
        return m_TextWrite;
    }

    #endregion

    #region File IO Read 

    private List<string> m_TextRead = new List<string>();
    private int m_ReadRun = -1;

    private List<string> GetReadDatafromFile(string m_Path)
    {
        List<string> m_TextRead = new List<string>();

        try
        {
            using (StreamReader sr = File.OpenText(m_Path))
            {
                string m_ReadRun = "";
                while ((m_ReadRun = sr.ReadLine()) != null)
                {
                    m_TextRead.Add(m_ReadRun);
                }
            }

            return m_TextRead;
        }
        catch
        {
            Debug.LogErrorFormat("[Error] File Read Fail: {0}", m_Path);

            return null;
        }
    }

    public void SetReadDataClear()
    {
        m_TextRead = new List<string>();
        m_ReadRun = -1;
    }

    public void SetReadDataStart(string m_Path)
    {
        m_TextRead = GetReadDatafromFile(m_Path);
    } //Call First

    public string GetReadDataAutoString()
    {
        if (m_ReadRun >= m_TextRead.Count - 1) return "";
        m_ReadRun++;
        return m_TextRead[m_ReadRun];
    }

    public int GetReadDataAutoInt()
    {
        if (m_ReadRun >= m_TextRead.Count - 1) return 0;
        m_ReadRun++;
        return int.Parse(m_TextRead[m_ReadRun]);
    }

    public float GetReadDataAutoFloat()
    {
        if (m_ReadRun >= m_TextRead.Count - 1) return 0f;
        m_ReadRun++;
        return float.Parse(m_TextRead[m_ReadRun]);
    }

    public double GetReadDataAutoDouble()
    {
        if (m_ReadRun >= m_TextRead.Count - 1) return 0f;
        m_ReadRun++;
        return double.Parse(m_TextRead[m_ReadRun]);
    }

    public bool GetReadDataAutoBool()
    {
        if (m_ReadRun >= m_TextRead.Count - 1) return false;
        m_ReadRun++;
        return m_TextRead[m_ReadRun] == "True";
    }

    public Vector2 GetReadDataAutoVector2(char m_Key)
    {
        if (m_ReadRun >= m_TextRead.Count - 1) return new Vector2();
        m_ReadRun++;
        return GitEncypt.GetDataVector2Dencypt(m_TextRead[m_ReadRun], m_Key);
    }

    public Vector2Int GetReadDataAutoVector2Int(char m_Key)
    {
        if (m_ReadRun >= m_TextRead.Count - 1) return new Vector2Int();
        m_ReadRun++;
        return GitEncypt.GetDataVector2IntDencypt(m_TextRead[m_ReadRun], m_Key);
    }

    public Vector3 GetReadDataAutoVector3(char m_Key)
    {
        if (m_ReadRun >= m_TextRead.Count - 1) return new Vector3();
        m_ReadRun++;
        return GitEncypt.GetDataVector3Dencypt(m_TextRead[m_ReadRun], m_Key);
    }

    public Vector3Int GetReadDataAutoVector3Int(char m_Key)
    {
        if (m_ReadRun >= m_TextRead.Count - 1) return new Vector3Int();
        m_ReadRun++;
        return GitEncypt.GetDataVector3IntDencypt(m_TextRead[m_ReadRun], m_Key);
    }

    public int GetReadDataAutoCurrent()
    {
        return m_ReadRun;
    }

    public bool CheckGetReadDataAutoEnd()
    {
        return GetReadDataAutoCurrent() >= GetReadDataList().Count - 1;
    }

    public List<string> GetReadDataList()
    {
        return m_TextRead;
    }

    #endregion
}

//=================================================== JSON

public class GitJSON
{
    //NOTE:
    //Type "TextAsset" is a "Text Document" File or "*.txt" File

    //SAMPLE:
    //ClassData m_Data = ClassFileIO.GetDatafromJson<ClassData>(m_JsonDataTextDocument);

    public static ClassData GetDataJson<ClassData>(TextAsset m_JsonDataTextDocument)
    {
        return GetDataJson<ClassData>(m_JsonDataTextDocument.text);
    }

    public static ClassData GetDataJson<ClassData>(string m_JsonData)
    {
        return JsonUtility.FromJson<ClassData>(m_JsonData);
    }

    public static string GetDataJson(object m_JsonDataClass)
    {
        return JsonUtility.ToJson(m_JsonDataClass);
    }
}

//=================================================== Enum

public class GitEnum
{
    public static string[] GetEnumArray<EnumType>()
    {
        return Enum.GetNames(typeof(EnumType));
    }

    public static List<string> GetEnumList<EnumType>()
    {
        return new List<string>(GetEnumArray<EnumType>());
    }

    public static int[] GetEnumArrayIndex<EnumType>()
    {
        return Enum.GetValues(typeof(EnumType)).Cast<int>().ToArray();
    }

    public static List<int> GetEnumListIndex<EnumType>()
    {
        return Enum.GetValues(typeof(EnumType)).Cast<int>().ToList();
    }

    public static string GetEnumString<EnumType>(int m_Index)
    {
        return Enum.GetName(typeof(EnumType), m_Index);
    }
}

//=================================================== Color

public class GitColor
{
    private static string GetColorHex(Color m_ColorRGBA)
    {
        return ColorUtility.ToHtmlStringRGB(m_ColorRGBA);
    }

    public static string GetColorHexCode(Color m_ColorRGBA)
    {
        string m_ColorHex = GetColorHex(m_ColorRGBA);
        string m_ColorHexCode = string.Format("#{0}", m_ColorHex);
        return m_ColorHexCode;
    }

    public static string GetColorHexFormat(Color m_ColorRGBA, string m_Text)
    {
        string m_ColorHex = GetColorHex(m_ColorRGBA);
        string m_TextFormat = string.Format("<#{0}>{1}</color>", m_ColorHex, m_Text);
        return m_TextFormat;
    }
}

//=================================================== Encypt

public class GitEncypt
{
    #region String Split

    public static string[] GetStringSplitArray(string m_FatherString, char m_Key)
    {
        return m_FatherString.Split(m_Key);
    }

    public static List<string> GetStringSplitList(string m_FatherString, char m_Key)
    {
        string[] m_SplitArray = GetStringSplitArray(m_FatherString, m_Key);

        List<string> m_SplitString = new List<string>();

        m_SplitString.AddRange(m_SplitArray);

        return m_SplitString;
    }

    #endregion

    #region String Data

    #region String Data Main

    #region String Data Main Encypt

    public static string GetDataEncypt(List<string> m_DataList, char m_Key)
    {
        string m_Data = "";

        for (int i = 0; i < m_DataList.Count; i++)
        {
            m_Data = GetDataEncyptAdd(m_Data, m_DataList[i], m_Key);
        }

        return m_Data;
    }

    public static string GetDataEncypt(List<int> m_DataList, char m_Key)
    {
        string m_Data = "";

        for (int i = 0; i < m_DataList.Count; i++)
        {
            m_Data = GetDataEncyptAdd(m_Data, m_DataList[i], m_Key);
        }

        return m_Data;
    }

    public static string GetDataEncypt(List<float> m_DataList, char m_Key)
    {
        string m_Data = "";

        for (int i = 0; i < m_DataList.Count; i++)
        {
            m_Data = GetDataEncyptAdd(m_Data, m_DataList[i], m_Key);
        }

        return m_Data;
    }

    public static string GetDataEncypt(List<bool> m_DataList, char m_Key)
    {
        string m_Data = "";

        for (int i = 0; i < m_DataList.Count; i++)
        {
            m_Data = GetDataEncyptAdd(m_Data, m_DataList[i], m_Key);
        }

        return m_Data;
    }

    #endregion

    #region String Data Main Add Encypt

    public static string GetDataEncyptAdd(string m_Data, string m_DataAdd, char m_Key)
    {
        return m_Data + ((m_Data.Length != 0) ? m_Key.ToString() : "") + m_DataAdd;
    }

    public static string GetDataEncyptAdd(string m_Data, int m_DataAdd, char m_Key)
    {
        return m_Data + ((m_Data.Length != 0) ? m_Key.ToString() : "") + m_DataAdd.ToString();
    }

    public static string GetDataEncyptAdd(string m_Data, float m_DataAdd, char m_Key)
    {
        return m_Data + ((m_Data.Length != 0) ? m_Key.ToString() : "") + m_DataAdd.ToString();
    }

    public static string GetDataEncyptAdd(string m_Data, bool m_DataAdd, char m_Key)
    {
        return m_Data + ((m_Data.Length != 0) ? m_Key.ToString() : "") + ((m_DataAdd) ? "1" : "0");
    }

    #endregion

    #region String Data Main Dencypt

    public static List<string> GetDataDencyptString(string m_Data, char m_Key)
    {
        if (m_Data.Equals(""))
        {
            Debug.LogWarning("GetDataDencypt_String: Emty Data!");

            return new List<string>();
        }

        return GetStringSplitList(m_Data, m_Key);
    }

    public static List<int> GetDataDencyptInt(string m_Data, char m_Key)
    {
        if (m_Data.Equals(""))
        {
            Debug.LogWarning("GetDataDencypt_Int: Emty Data!");

            return new List<int>();
        }

        List<string> m_DataListString = GetDataDencyptString(m_Data, m_Key);

        List<int> m_DataListInt = new List<int>();

        for (int i = 0; i < m_DataListString.Count; i++)
        {
            m_DataListInt.Add(int.Parse(m_DataListString[i]));
        }

        return m_DataListInt;
    }

    public static List<float> GetDataDencyptFloat(string m_Data, char m_Key)
    {
        if (m_Data.Equals(""))
        {
            Debug.LogWarning("GetDataDencypt_Float: Emty Data!");

            return new List<float>();
        }

        List<string> m_DataListString = GetStringSplitList(m_Data, m_Key);

        List<float> m_DataListFloat = new List<float>();

        for (int i = 0; i < m_DataListString.Count; i++)
        {
            m_DataListFloat.Add(float.Parse(m_DataListString[i]));
        }

        return m_DataListFloat;
    }

    public static List<bool> GetDataDencyptBool(string m_Data, char m_Key)
    {
        if (m_Data.Equals(""))
        {
            Debug.LogWarning("GetDataDencypt_Bool: Emty Data!");

            return new List<bool>();
        }

        List<string> m_DataListString = GetStringSplitList(m_Data, m_Key);

        List<bool> m_DataListBool = new List<bool>();

        for (int i = 0; i < m_DataListString.Count; i++)
        {
            string m_Bool = m_DataListString[i];

            if (m_Bool == "1")
            {
                m_DataListBool.Add(true);
            }
            else
            if (m_Bool == "0")
            {
                m_DataListBool.Add(false);
            }
        }

        return m_DataListBool;
    }

    #endregion

    #endregion

    #region String Data Vector

    #region String Data Vector Encypt

    public static string GetDataVector2Encypt(Vector2 v2VectorData, char m_Key)
    {
        return v2VectorData.x.ToString() + m_Key + v2VectorData.y.ToString();
    }

    public static string GetDataVector3Encypt(Vector3 mVectorData, char m_Key)
    {
        return mVectorData.x.ToString() + m_Key + mVectorData.y.ToString() + m_Key + mVectorData.z.ToString();
    }

    public static string GetDataVector2IntEncypt(Vector2Int v2VectorData, char m_Key)
    {
        return v2VectorData.x.ToString() + m_Key + v2VectorData.y.ToString();
    }

    public static string GetDataVector3IntEncypt(Vector3Int mVectorData, char m_Key)
    {
        return mVectorData.x.ToString() + m_Key + mVectorData.y.ToString() + m_Key + mVectorData.z.ToString();
    }

    #endregion

    #region String Data Vector Dencypt

    public static Vector2 GetDataVector2Dencypt(string m_VectortorData, char m_Key)
    {
        List<float> m_Data = GetDataDencyptFloat(m_VectortorData, m_Key);

        return new Vector2(m_Data[0], m_Data[1]);
    }

    public static Vector3 GetDataVector3Dencypt(string m_VectortorData, char m_Key)
    {
        List<float> m_Data = GetDataDencyptFloat(m_VectortorData, m_Key);

        return new Vector3(m_Data[0], m_Data[1], m_Data[2]);
    }

    public static Vector2Int GetDataVector2IntDencypt(string m_VectortorData, char m_Key)
    {
        List<int> m_Data = GetDataDencyptInt(m_VectortorData, m_Key);

        return new Vector2Int(m_Data[0], m_Data[1]);
    }

    public static Vector3Int GetDataVector3IntDencypt(string m_VectortorData, char m_Key)
    {
        List<int> m_Data = GetDataDencyptInt(m_VectortorData, m_Key);

        return new Vector3Int(m_Data[0], m_Data[1], m_Data[2]);
    }

    #endregion

    #endregion

    #endregion
}


//=================================================== Keyboard

public class GitKeyboard
{
    public static void SetMouseVisible(bool m_MouseVisble)
    {
        Cursor.visible = m_MouseVisble;
    }

    public static string GetKeyCodeSimple(KeyCode m_KeyKey)
    {
        switch (m_KeyKey)
        {
            case KeyCode.Escape:
                return "Esc";
            case KeyCode.Return:
                return "Enter";
            case KeyCode.Delete:
                return "Del";
            case KeyCode.Backspace:
                return "B-Space";

            case KeyCode.Mouse0:
                return "L-Mouse";
            case KeyCode.Mouse1:
                return "R-Mouse";
            case KeyCode.Mouse2:
                return "M-Mouse";

            case KeyCode.LeftBracket:
                return "[";
            case KeyCode.RightBracket:
                return "]";

            case KeyCode.LeftCurlyBracket:
                return "{";
            case KeyCode.RightCurlyBracket:
                return "}";

            case KeyCode.LeftParen:
                return "(";
            case KeyCode.RightParen:
                return ")";

            case KeyCode.LeftShift:
                return "L-Shift";
            case KeyCode.RightShift:
                return "R-Shift";

            case KeyCode.LeftAlt:
                return "L-Alt";
            case KeyCode.RightAlt:
                return "R-Alt";

            case KeyCode.PageUp:
                return "Page-U";
            case KeyCode.PageDown:
                return "Page-D";
        }

        return m_KeyKey.ToString();
    }
}

//=================================================== Gizmos

public class GitGizmos
{
    //Camera

    public static void GizmosCamera(Color m_Color)
    {
        GizmosCamera(Camera.main, m_Color);
    }

    public static void GizmosCamera(Camera m_Camera, Color m_Color)
    {
        Gizmos.color = m_Color;

        Vector2 m_Resolution = GitResolution.GetCameraSizeUnit();
        Gizmos.DrawWireCube((Vector2)m_Camera.transform.position, m_Resolution);
    }

    //Collider

    public static void GizmosPolygonCollider2D(PolygonCollider2D m_PolygonCollider2D, Color m_Color)
    {
        Gizmos.color = m_Color;

        for (int i = 1; i < m_PolygonCollider2D.points.Length; i++)
        {
            Gizmos.DrawLine(m_PolygonCollider2D.points[i - 1], m_PolygonCollider2D.points[i]);
        }
        Gizmos.DrawLine(m_PolygonCollider2D.points[0], m_PolygonCollider2D.points[m_PolygonCollider2D.points.Length - 1]);
    }
}

//=================================================== Email

public class GitEmail
{
    public static bool GetEmail(string m_EmailCheck)
    {
        //Check Not Invalid
        if (!GetEmailNotInvalid(m_EmailCheck))
        {
            return false;
        }

        //Lower m_AIL
        m_EmailCheck = m_EmailCheck.ToLower();

        return
            GetEmailGmail(m_EmailCheck) &&
            GetEmailYahoo(m_EmailCheck);
    }

    private static bool GetEmailNotInvalid(string m_EmailCheck)
    {
        //Check SPACE
        if (m_EmailCheck.Contains(" "))
        {
            return false;
        }

        //Check @
        bool m_CheckAAExist = false;
        for (int i = 0; i < m_EmailCheck.Length; i++)
        {
            if (!m_CheckAAExist && m_EmailCheck[i] == '@')
            {
                m_CheckAAExist = true;
            }
            else
            if (m_CheckAAExist && m_EmailCheck[i] == '@')
            {
                return false;
            }
        }
        if (!m_CheckAAExist)
        {
            return false;
        }

        //All Check Done
        return true;
    }

    private static bool GetEmailGmail(string m_EmailCheck)
    {
        //Check if GMAIL
        if (m_EmailCheck.Contains("@gmail.com"))
        {
            //Get ASCII
            byte[] ba_Ascii = Encoding.ASCII.GetBytes(m_EmailCheck);

            //First Character (Just '0-9' and 'a-z')
            if (ba_Ascii[0] >= 48 && ba_Ascii[0] <= 57 ||
                ba_Ascii[0] >= 97 && ba_Ascii[0] <= 122)
            {
                //Next Character (Just '0-9' and 'a-z' and '.')
                for (int i = 1; i < m_EmailCheck.Length; i++)
                {
                    if (m_EmailCheck[i] == '@')
                    {
                        break;
                    }

                    if (ba_Ascii[i] >= 48 && ba_Ascii[i] <= 57 ||
                        ba_Ascii[i] >= 97 && ba_Ascii[i] <= 122 ||
                        m_EmailCheck[i] == '.')
                    {

                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }

        //All Check Done
        return true;
    }

    private static bool GetEmailYahoo(string m_EmailCheck)
    {
        //Check if GMAIL
        if (m_EmailCheck.Contains("@yahoo.com"))
        {
            //Get ASCII
            byte[] ba_Ascii = Encoding.ASCII.GetBytes(m_EmailCheck);

            //First Character (Just 'a-z')
            if (ba_Ascii[0] >= 97 && ba_Ascii[0] <= 122)
            {
                //Next Character (Just '0-9' and 'a-z' and '.' and '_')
                for (int i = 1; i < m_EmailCheck.Length; i++)
                {
                    if (m_EmailCheck[i] == '@')
                    {
                        break;
                    }

                    if (ba_Ascii[i] >= 48 && ba_Ascii[i] <= 57 ||
                        ba_Ascii[i] >= 97 && ba_Ascii[i] <= 122 ||
                        m_EmailCheck[i] == '.' ||
                        m_EmailCheck[i] == '_')
                    {

                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }

        //All Check Done
        return true;
    }
}

//=================================================== Enum

public enum GitOpption { Yes = 1, No = 0 }

public enum GitDirection { Up, Down, Left, Right }

public enum GitDirectionX { Left = -1, Right = 1 }

public enum GitDirectionY { Up = 1, Down = -1 }