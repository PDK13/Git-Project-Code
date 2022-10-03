using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(RigidbodyComponent))]

public class RaycastFan : MonoBehaviour
//Eye Fan Cast
//Distance 0 m_ean not Cast (Even "INSIDE" other GameObject)
{
    [Header("Fan in 'XZ' or 'XY'?")]
    public bool mAllowXZ = true;
    //World

    [Header("None - Line - Ray - Box - Sphere Cast")]
    [Range(0, 4)]
    public int m_Cast = 0;
    //Cast Type

    public LayerMask m_Barrier;
    //Layer Cast

    [Header("Centre")]
    public float m_Distance = 2f;
    //Radius Cast

    [Header("Fan")]
    public int m_Fan = 1;
    //Half of Cast from Centre Cast
    [Range(0, 360)]
    public float m_OffFan = 15f;
    //Offset between Cast

    [Header("Chance")]
    public Vector3 m_OffPos = new Vector3();
    //Offset Eye

    [Header("BoxCast")]
    public Vector3 m_Square = new Vector3(0.1f, 0.1f, 0.1f);

    [Header("SphereCast")]
    public float m_Sphere = 0.1f;

    //Eye

    private RaycastHit GetEye(int m_CastIndex)
    //Get EyeCast (0: Centre; +1: Top; -1: Bot)
    {
        List<RaycastHit> m_RaycastHit = new List<RaycastHit>();

        ClassEye m_Eye = new ClassEye();
        //Use this Script to use all  of Eye
        RigidbodyComponent m_Rigid = GetComponent<RigidbodyComponent>();
        //Use this Script to get "Rotation" of this Object

        if (!mAllowXZ)
        {
            //LineCast
            if (m_Cast == 1)
            {
                if (m_Eye.GetCheckLineCast(
                    transform.position + m_OffPos,
                    transform.position + m_OffPos + ClassVector.GetPosOnCircleXY(-m_Rigid.GetRotationXZ() + m_CastIndex * m_OffFan, m_Distance), m_Barrier))
                {
                    //Always Hit Tarket on Line
                    return m_Eye.GetLineCastRaycastHit(
                        transform.position + m_OffPos,
                        transform.position + m_OffPos + ClassVector.GetPosOnCircleXY(-m_Rigid.GetRotationXZ() + m_CastIndex * m_OffFan, m_Distance),
                        m_Barrier);
                }
            }
            //RayCast
            else
            if (m_Cast == 2)
            {
                if (m_Eye.GetCheckRayCastVector(
                    transform.position + m_OffPos,
                    transform.position + m_OffPos + ClassVector.GetPosOnCircleXY(-m_Rigid.GetRotationXZ() + m_CastIndex * m_OffFan, m_Distance), m_Distance, m_Barrier))
                {
                    //Always Hit Tarket if not have Barrier
                    return m_Eye.GetRayCastVectorRaycastHit(
                        transform.position + m_OffPos,
                        transform.position + m_OffPos + ClassVector.GetPosOnCircleXY(-m_Rigid.GetRotationXZ() + m_CastIndex * m_OffFan, m_Distance),
                        m_Distance,
                        m_Barrier);
                }
            }
            //BoxCast
            else
            if (m_Cast == 3)
            {
                if (m_Eye.GetCheckBoxCastVector(
                    transform.position + m_OffPos,
                    m_Square,
                    transform.position + m_OffPos + ClassVector.GetPosOnCircleXY(-m_Rigid.GetRotationXZ() + m_CastIndex * m_OffFan, m_Distance),
                    new Vector3(0, 0, 0),
                    m_Distance,
                    m_Barrier))
                {
                    //Always Hit Tarket if not have Barrier
                    return m_Eye.GetBoxCastVectorRaycastHit(
                        transform.position + m_OffPos,
                        m_Square,
                        transform.position + m_OffPos + ClassVector.GetPosOnCircleXY(-m_Rigid.GetRotationXZ() + m_CastIndex * m_OffFan, m_Distance),
                        new Vector3(0, 0, 0),
                        m_Distance,
                        m_Barrier);
                }
            }
            //SphereCast
            else
            if (m_Cast == 4)
            {
                if (m_Eye.GetCheckSphereCastVector(
                    transform.position + m_OffPos,
                    m_Sphere,
                    transform.position + m_OffPos + ClassVector.GetPosOnCircleXY(-m_Rigid.GetRotationXZ() + m_CastIndex * m_OffFan, m_Distance),
                    m_Distance,
                    m_Barrier))
                {
                    //Always Hit Tarket if not have Barrier
                    return m_Eye.GetSphereCastVectorRaycastHit(
                        transform.position + m_OffPos,
                        m_Sphere,
                        transform.position + m_OffPos + ClassVector.GetPosOnCircleXY(-m_Rigid.GetRotationXZ() + m_CastIndex * m_OffFan, m_Distance),
                        m_Distance,
                        m_Barrier);
                }
            }
        }
        else
    if (mAllowXZ)
        {
            //LineCast
            if (m_Cast == 1)
            {
                if (m_Eye.GetCheckLineCast(
                    transform.position + m_OffPos,
                    transform.position + m_OffPos + ClassVector.GetPosOnCircleXZ(-m_Rigid.GetRotationXZ() + m_CastIndex * m_OffFan, m_Distance), m_Barrier))
                {
                    //Always Hit Tarket if not have Barrier
                    return m_Eye.GetLineCastRaycastHit(
                        transform.position + m_OffPos, transform.position + m_OffPos + ClassVector.GetPosOnCircleXZ(-m_Rigid.GetRotationXZ() + m_CastIndex * m_OffFan, m_Distance),
                        m_Barrier);
                }
            }
            //RayCast
            else
            if (m_Cast == 2)
            {
                if (m_Eye.GetCheckRayCastVector(
                    transform.position + m_OffPos,
                    transform.position + m_OffPos + ClassVector.GetPosOnCircleXZ(-m_Rigid.GetRotationXZ() + m_CastIndex * m_OffFan, m_Distance), m_Distance, m_Barrier))
                {
                    //Always Hit Tarket if not have Barrier
                    return m_Eye.GetRayCastVectorRaycastHit(
                        transform.position + m_OffPos, transform.position + m_OffPos + ClassVector.GetPosOnCircleXZ(-m_Rigid.GetRotationXZ() + m_CastIndex * m_OffFan, m_Distance),
                        m_Distance,
                        m_Barrier);
                }
            }
            //BoxCast
            else
            if (m_Cast == 3)
            {
                if (m_Eye.GetCheckBoxCastVector(
                    transform.position + m_OffPos,
                    m_Square,
                    transform.position + m_OffPos + ClassVector.GetPosOnCircleXZ(-m_Rigid.GetRotationXZ() + m_CastIndex * m_OffFan, m_Distance),
                    new Vector3(0, 0, 0),
                    m_Distance,
                    m_Barrier))
                {
                    //Always Hit Tarket if not have Barrier
                    return m_Eye.GetBoxCastVectorRaycastHit(
                        transform.position + m_OffPos,
                        m_Square,
                        transform.position + m_OffPos + ClassVector.GetPosOnCircleXZ(-m_Rigid.GetRotationXZ() + m_CastIndex * m_OffFan, m_Distance),
                        new Vector3(0, 0, 0),
                        m_Distance,
                        m_Barrier);
                }
            }
            //SphereCast
            else
            if (m_Cast == 4)
            {
                if (m_Eye.GetCheckSphereCastVector(
                    transform.position + m_OffPos,
                    m_Sphere,
                    transform.position + m_OffPos + ClassVector.GetPosOnCircleXZ(-m_Rigid.GetRotationXZ() + m_CastIndex * m_OffFan, m_Distance),
                    m_Distance,
                    m_Barrier))
                {
                    //Always Hit Tarket if not have Barrier
                    return m_Eye.GetSphereCastVectorRaycastHit(
                        transform.position + m_OffPos,
                        m_Sphere,
                        transform.position + m_OffPos + ClassVector.GetPosOnCircleXZ(-m_Rigid.GetRotationXZ() + m_CastIndex * m_OffFan, m_Distance),
                        m_Distance,
                        m_Barrier);
                }
            }
        }

        return new RaycastHit();
    }

    public List<RaycastHit> GetList()
    //Get List EyeCast (0:Centre; %2!=0: Top; %2==0: Bot)
    {
        List<RaycastHit> m_RaycastHit = new List<RaycastHit>
        {

            //Centre
            GetEye(0)
        };

        for (int i = 1; i <= m_Fan; i++)
        {
            //Top (%2!=0)
            m_RaycastHit.Add(GetEye(i));
            //Bot (%2==0)
            m_RaycastHit.Add(GetEye(-i));
        }

        return m_RaycastHit;
    }

    public List<float> GetListDistance(int mMinimize)
    //Get List Distance (Decrease by m_inimize)
    {
        List<RaycastHit> l1Ray = GetList();

        List<float> l1Distance = new List<float>();

        for (int i = 0; i < l1Ray.Count; i++)
        {
            if (l1Ray[i].collider == null)
            {
                l1Distance.Add(-1f);
            }
            else
            {
                l1Distance.Add(l1Ray[i].distance / mMinimize);
            }
        }
        return l1Distance;
    }

    public List<float> GetListDistance()
    //Get List Distance
    {
        List<RaycastHit> l1Ray = GetList();

        List<float> l1Distance = new List<float>();

        for (int i = 0; i < l1Ray.Count; i++)
        {
            if (l1Ray[i].collider == null)
            {
                l1Distance.Add(-1f);
            }
            else
            {
                l1Distance.Add(l1Ray[i].distance);
            }
        }
        return l1Distance;
    }

    public RaycastHit GetEyep(int m_Index)
    //Get EyeCast Top
    {
        return GetEye(m_Index * 2 - 1);
    }

    public bool GetCheckEyep(int m_Index)
    //Get Check EyeCast Top
    {
        return GetEye(m_Index * 2 - 1).collider != null;
    }

    public RaycastHit GetEye_Bot(int m_Index)
    //Get EyeCast Bot
    {
        return GetEye(m_Index * 2);
    }

    public bool GetCheckEye_Bot(int m_Index)
    //Get Check EyeCast Bot
    {
        return GetEye(m_Index * 2).collider != null;
    }

    public int GetFanCount()
    //Get Count Fan Eye (Always have 1 Fan Centre)
    {
        return 1 + m_Fan * 2;
    }

    //=== Gizmos

    private void SetGizmos(int m_CastIndex)
    {
        ClassEye m_Eye = new ClassEye();
        RigidbodyComponent m_Rigid = GetComponent<RigidbodyComponent>();

        if (mAllowXZ)
        {
            //LineCast
            if (m_Cast == 1)
            {
                if (m_Eye.GetCheckLineCast(
                    transform.position + m_OffPos,
                    transform.position + m_OffPos + ClassVector.GetPosOnCircleXY(-m_Rigid.GetRotationXZ() + m_CastIndex * m_OffFan, m_Distance), m_Barrier))
                {
                    //Always Hit Tarket if not have Barrier
                    RaycastHit ray_Hit = m_Eye.GetLineCastRaycastHit(
                        transform.position + m_OffPos, transform.position + m_OffPos + ClassVector.GetPosOnCircleXY(-m_Rigid.GetRotationXZ() + m_CastIndex * m_OffFan, m_Distance),
                        m_Barrier);
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(
                        transform.position + m_OffPos,
                        transform.position + m_OffPos + ClassVector.GetPosOnCircleXY(-m_Rigid.GetRotationXZ() + m_CastIndex * m_OffFan, m_Distance));
                }
                else
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(
                        transform.position + m_OffPos,
                        transform.position + m_OffPos + ClassVector.GetPosOnCircleXY(-m_Rigid.GetRotationXZ() + m_CastIndex * m_OffFan, m_Distance));
                }
            }
            //RayCast
            else
            if (m_Cast == 2)
            {
                if (m_Eye.GetCheckRayCastVector(
                    transform.position + m_OffPos,
                    transform.position + m_OffPos + ClassVector.GetPosOnCircleXY(-m_Rigid.GetRotationXZ() + m_CastIndex * m_OffFan, m_Distance),
                    m_Distance,
                    m_Barrier))
                {
                    //Always Hit Tarket if not have Barrier
                    RaycastHit ray_Hit = m_Eye.GetRayCastVectorRaycastHit(
                        transform.position + m_OffPos, transform.position + m_OffPos + ClassVector.GetPosOnCircleXY(-m_Rigid.GetRotationXZ() + m_CastIndex * m_OffFan, m_Distance),
                        m_Distance,
                        m_Barrier);
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(
                        transform.position + m_OffPos,
                        transform.position + m_OffPos + ClassVector.GetPosOnCircleXY(-m_Rigid.GetRotationXZ() + m_CastIndex * m_OffFan, ray_Hit.distance));
                }
                else
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(
                        transform.position + m_OffPos,
                        transform.position + m_OffPos + ClassVector.GetPosOnCircleXY(-m_Rigid.GetRotationXZ() + m_CastIndex * m_OffFan, m_Distance));
                }
            }
            //BoxCast
            else
            if (m_Cast == 3)
            {
                if (m_Eye.GetCheckBoxCastVector(
                    transform.position + m_OffPos,
                    m_Square,
                    transform.position + m_OffPos + ClassVector.GetPosOnCircleXY(-m_Rigid.GetRotationXZ() + m_CastIndex * m_OffFan, m_Distance),
                    new Vector3(0, 0, 0),
                    m_Distance,
                    m_Barrier))
                {
                    //Always Hit Tarket if not have Barrier
                    RaycastHit ray_Hit = m_Eye.GetBoxCastVectorRaycastHit(
                        transform.position + m_OffPos,
                        m_Square,
                        transform.position + m_OffPos + ClassVector.GetPosOnCircleXY(-m_Rigid.GetRotationXZ() + m_CastIndex * m_OffFan, m_Distance),
                        new Vector3(0, 0, 0),
                        m_Distance,
                        m_Barrier);
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(
                        transform.position + m_OffPos,
                        transform.position + m_OffPos + ClassVector.GetPosOnCircleXY(-m_Rigid.GetRotationXZ() + m_CastIndex * m_OffFan, ray_Hit.distance));
                    Gizmos.DrawWireCube(
                        transform.position + m_OffPos + ClassVector.GetPosOnCircleXY(-m_Rigid.GetRotationXZ() + m_CastIndex * m_OffFan, ray_Hit.distance),
                        m_Square);
                }
                else
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(
                        transform.position + m_OffPos,
                        transform.position + m_OffPos + ClassVector.GetPosOnCircleXY(-m_Rigid.GetRotationXZ() + m_CastIndex * m_OffFan, m_Distance));
                    Gizmos.DrawWireCube(
                        transform.position + m_OffPos + ClassVector.GetPosOnCircleXY(-m_Rigid.GetRotationXZ() + m_CastIndex * m_OffFan, m_Distance),
                        m_Square);
                }
            }
            //SphereCast
            else
            if (m_Cast == 4)
            {
                if (m_Eye.GetCheckSphereCastVector(
                    transform.position + m_OffPos,
                    m_Sphere,
                    transform.position + m_OffPos + ClassVector.GetPosOnCircleXY(-m_Rigid.GetRotationXZ() + m_CastIndex * m_OffFan, m_Distance),
                    m_Distance,
                    m_Barrier))
                {
                    //Always Hit Tarket if not have Barrier
                    RaycastHit ray_Hit = m_Eye.GetSphereCastVectorRaycastHit(
                        transform.position + m_OffPos,
                        m_Sphere,
                        transform.position + m_OffPos + ClassVector.GetPosOnCircleXY(-m_Rigid.GetRotationXZ() + m_CastIndex * m_OffFan, m_Distance),
                        m_Distance,
                        m_Barrier);
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(
                        transform.position + m_OffPos,
                        transform.position + m_OffPos + ClassVector.GetPosOnCircleXY(-m_Rigid.GetRotationXZ() + m_CastIndex * m_OffFan, ray_Hit.distance));
                    Gizmos.DrawWireSphere(
                        transform.position + m_OffPos + ClassVector.GetPosOnCircleXY(-m_Rigid.GetRotationXZ() + m_CastIndex * m_OffFan, ray_Hit.distance),
                        m_Sphere / 2);
                }
                else
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(
                        transform.position + m_OffPos,
                        transform.position + m_OffPos + ClassVector.GetPosOnCircleXY(-m_Rigid.GetRotationXZ() + m_CastIndex * m_OffFan, m_Distance));
                    Gizmos.DrawWireSphere(
                        transform.position + m_OffPos + ClassVector.GetPosOnCircleXY(-m_Rigid.GetRotationXZ() + m_CastIndex * m_OffFan, m_Distance),
                        m_Sphere / 2);
                }
            }
        }
        else
        if (mAllowXZ)
        {
            //LineCast
            if (m_Cast == 1)
            {
                if (m_Eye.GetCheckLineCast(
                    transform.position + m_OffPos,
                    transform.position + m_OffPos + ClassVector.GetPosOnCircleXZ(-m_Rigid.GetRotationXZ() + m_CastIndex * m_OffFan, m_Distance), m_Barrier))
                {
                    //Always Hit Tarket if not have Barrier
                    RaycastHit ray_Hit = m_Eye.GetLineCastRaycastHit(
                        transform.position + m_OffPos, transform.position + m_OffPos + ClassVector.GetPosOnCircleXZ(-m_Rigid.GetRotationXZ() + m_CastIndex * m_OffFan, m_Distance),
                        m_Barrier);
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(
                        transform.position + m_OffPos,
                        transform.position + m_OffPos + ClassVector.GetPosOnCircleXZ(-m_Rigid.GetRotationXZ() + m_CastIndex * m_OffFan, m_Distance));
                }
                else
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(
                        transform.position + m_OffPos,
                        transform.position + m_OffPos + ClassVector.GetPosOnCircleXZ(-m_Rigid.GetRotationXZ() + m_CastIndex * m_OffFan, m_Distance));
                }
            }
            //RayCast
            else
            if (m_Cast == 2)
            {
                if (m_Eye.GetCheckRayCastVector(
                    transform.position + m_OffPos,
                    transform.position + m_OffPos + ClassVector.GetPosOnCircleXZ(-m_Rigid.GetRotationXZ() + m_CastIndex * m_OffFan, m_Distance), m_Distance, m_Barrier))
                {
                    //Always Hit Tarket if not have Barrier
                    RaycastHit ray_Hit = m_Eye.GetRayCastVectorRaycastHit(
                        transform.position + m_OffPos, transform.position + m_OffPos + ClassVector.GetPosOnCircleXZ(-m_Rigid.GetRotationXZ() + m_CastIndex * m_OffFan, m_Distance),
                        m_Distance,
                        m_Barrier);
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(
                        transform.position + m_OffPos,
                        transform.position + m_OffPos + ClassVector.GetPosOnCircleXZ(-m_Rigid.GetRotationXZ() + m_CastIndex * m_OffFan, ray_Hit.distance));
                }
                else
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(
                        transform.position + m_OffPos,
                        transform.position + m_OffPos + ClassVector.GetPosOnCircleXZ(-m_Rigid.GetRotationXZ() + m_CastIndex * m_OffFan, m_Distance));
                }
            }
            //BoxCast
            else
            if (m_Cast == 3)
            {
                if (m_Eye.GetCheckBoxCastVector(
                    transform.position + m_OffPos,
                    m_Square,
                    transform.position + m_OffPos + ClassVector.GetPosOnCircleXZ(-m_Rigid.GetRotationXZ() + m_CastIndex * m_OffFan, m_Distance),
                    new Vector3(0, 0, 0),
                    m_Distance,
                    m_Barrier))
                {
                    //Always Hit Tarket if not have Barrier
                    RaycastHit ray_Hit = m_Eye.GetBoxCastVectorRaycastHit(
                        transform.position + m_OffPos,
                        m_Square,
                        transform.position + m_OffPos + ClassVector.GetPosOnCircleXZ(-m_Rigid.GetRotationXZ() + m_CastIndex * m_OffFan, m_Distance),
                        new Vector3(0, 0, 0),
                        m_Distance,
                        m_Barrier);
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(
                        transform.position + m_OffPos,
                        transform.position + m_OffPos + ClassVector.GetPosOnCircleXZ(-m_Rigid.GetRotationXZ() + m_CastIndex * m_OffFan, ray_Hit.distance));
                    Gizmos.DrawWireCube(
                        transform.position + m_OffPos + ClassVector.GetPosOnCircleXZ(-m_Rigid.GetRotationXZ() + m_CastIndex * m_OffFan, ray_Hit.distance),
                        m_Square);
                }
                else
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(
                        transform.position + m_OffPos,
                        transform.position + m_OffPos + ClassVector.GetPosOnCircleXZ(-m_Rigid.GetRotationXZ() + m_CastIndex * m_OffFan, m_Distance));
                    Gizmos.DrawWireCube(
                        transform.position + m_OffPos + ClassVector.GetPosOnCircleXZ(-m_Rigid.GetRotationXZ() + m_CastIndex * m_OffFan, m_Distance),
                        m_Square);
                }
            }
            //SphereCast
            else
            if (m_Cast == 4)
            {
                if (m_Eye.GetCheckSphereCastVector(
                    transform.position + m_OffPos,
                    m_Sphere,
                    transform.position + m_OffPos + ClassVector.GetPosOnCircleXZ(-m_Rigid.GetRotationXZ() + m_CastIndex * m_OffFan, m_Distance),
                    m_Distance,
                    m_Barrier))
                {
                    //Always Hit Tarket if not have Barrier
                    RaycastHit ray_Hit = m_Eye.GetSphereCastVectorRaycastHit(
                        transform.position + m_OffPos,
                        m_Sphere,
                        transform.position + m_OffPos + ClassVector.GetPosOnCircleXZ(-m_Rigid.GetRotationXZ() + m_CastIndex * m_OffFan, m_Distance),
                        m_Distance,
                        m_Barrier);
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(
                        transform.position + m_OffPos,
                        transform.position + m_OffPos + ClassVector.GetPosOnCircleXZ(-m_Rigid.GetRotationXZ() + m_CastIndex * m_OffFan, ray_Hit.distance));
                    Gizmos.DrawWireSphere(
                        transform.position + m_OffPos + ClassVector.GetPosOnCircleXZ(-m_Rigid.GetRotationXZ() + m_CastIndex * m_OffFan, ray_Hit.distance),
                        m_Sphere / 2);
                }
                else
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(
                        transform.position + m_OffPos,
                        transform.position + m_OffPos + ClassVector.GetPosOnCircleXZ(-m_Rigid.GetRotationXZ() + m_CastIndex * m_OffFan, m_Distance));
                    Gizmos.DrawWireSphere(
                        transform.position + m_OffPos + ClassVector.GetPosOnCircleXZ(-m_Rigid.GetRotationXZ() + m_CastIndex * m_OffFan, m_Distance),
                        m_Sphere / 2);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        //Centre
        SetGizmos(0);

        //Fan
        Gizmos.color = Color.red;

        for (int i = 1; i <= m_Fan; i++)
        {
            SetGizmos(i);
            SetGizmos(-i);
        }
    }
}