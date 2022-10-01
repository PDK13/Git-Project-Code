using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(RigidbodyComponent))]

public class RaycastFan : MonoBehaviour
//Eye Fan Cast
//Distance 0 m_ean not Cast (Even "INSIDE" other GameObject)
{
    [Header("Fan in 'XZ' or 'XY'?")]
    public bool m_XZ = true;
    //World

    [Header("None - Line - Ray - Box - Sphere Cast")]
    [Range(0, 4)]
    public int m_Cast = 0;
    //Cast Type

    public LayerMask l_Barrier;
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
    public Vector3 v3_OffPos = new Vector3();
    //Offset Eye

    [Header("BoxCast")]
    public Vector3 v3_Square = new Vector3(0.1f, 0.1f, 0.1f);

    [Header("SphereCast")]
    public float m_Sphere = 0.1f;

    //Eye

    private RaycastHit GetEye(int m_CastIndex)
    //Get EyeCast (0: Centre; +1: Top; -1: Bot)
    {
        List<RaycastHit> l_RaycastHit = new List<RaycastHit>();

        Class_Eye cs_Eye = new Class_Eye();
        //Use this Script to use all  of Eye
        RigidbodyComponent cs_Rigid = GetComponent<RigidbodyComponent>();
        //Use this Script to get "Rotation" of this Object

        if (!m_XZ)
        {
            //LineCast
            if (m_Cast == 1)
            {
                if (cs_Eye.GetLineCast_Check(
                    transform.position + v3_OffPos,
                    transform.position + v3_OffPos + ClassVector.GetPosOnCircleXY(-cs_Rigid.GetRotation_XZ() + m_CastIndex * m_OffFan, m_Distance), l_Barrier))
                {
                    //Always Hit Tarket on Line
                    return cs_Eye.GetLineCast_RaycastHit(
                        transform.position + v3_OffPos,
                        transform.position + v3_OffPos + ClassVector.GetPosOnCircleXY(-cs_Rigid.GetRotation_XZ() + m_CastIndex * m_OffFan, m_Distance),
                        l_Barrier);
                }
            }
            //RayCast
            else
            if (m_Cast == 2)
            {
                if (cs_Eye.GetRayCast_Vec_Check(
                    transform.position + v3_OffPos,
                    transform.position + v3_OffPos + ClassVector.GetPosOnCircleXY(-cs_Rigid.GetRotation_XZ() + m_CastIndex * m_OffFan, m_Distance), m_Distance, l_Barrier))
                {
                    //Always Hit Tarket if not have Barrier
                    return cs_Eye.GetRayCast_Vec_RaycastHit(
                        transform.position + v3_OffPos,
                        transform.position + v3_OffPos + ClassVector.GetPosOnCircleXY(-cs_Rigid.GetRotation_XZ() + m_CastIndex * m_OffFan, m_Distance),
                        m_Distance,
                        l_Barrier);
                }
            }
            //BoxCast
            else
            if (m_Cast == 3)
            {
                if (cs_Eye.GetBoxCast_Vec_Check(
                    transform.position + v3_OffPos,
                    v3_Square,
                    transform.position + v3_OffPos + ClassVector.GetPosOnCircleXY(-cs_Rigid.GetRotation_XZ() + m_CastIndex * m_OffFan, m_Distance),
                    new Vector3(0, 0, 0),
                    m_Distance,
                    l_Barrier))
                {
                    //Always Hit Tarket if not have Barrier
                    return cs_Eye.GetBoxCast_Vec_RaycastHit(
                        transform.position + v3_OffPos,
                        v3_Square,
                        transform.position + v3_OffPos + ClassVector.GetPosOnCircleXY(-cs_Rigid.GetRotation_XZ() + m_CastIndex * m_OffFan, m_Distance),
                        new Vector3(0, 0, 0),
                        m_Distance,
                        l_Barrier);
                }
            }
            //SphereCast
            else
            if (m_Cast == 4)
            {
                if (cs_Eye.GetSphereCast_Vec_Check(
                    transform.position + v3_OffPos,
                    m_Sphere,
                    transform.position + v3_OffPos + ClassVector.GetPosOnCircleXY(-cs_Rigid.GetRotation_XZ() + m_CastIndex * m_OffFan, m_Distance),
                    m_Distance,
                    l_Barrier))
                {
                    //Always Hit Tarket if not have Barrier
                    return cs_Eye.GetSphereCast_Vec_RaycastHit(
                        transform.position + v3_OffPos,
                        m_Sphere,
                        transform.position + v3_OffPos + ClassVector.GetPosOnCircleXY(-cs_Rigid.GetRotation_XZ() + m_CastIndex * m_OffFan, m_Distance),
                        m_Distance,
                        l_Barrier);
                }
            }
        }
        else
    if (m_XZ)
        {
            //LineCast
            if (m_Cast == 1)
            {
                if (cs_Eye.GetLineCast_Check(
                    transform.position + v3_OffPos,
                    transform.position + v3_OffPos + ClassVector.GetPosOnCircleXZ(-cs_Rigid.GetRotation_XZ() + m_CastIndex * m_OffFan, m_Distance), l_Barrier))
                {
                    //Always Hit Tarket if not have Barrier
                    return cs_Eye.GetLineCast_RaycastHit(
                        transform.position + v3_OffPos, transform.position + v3_OffPos + ClassVector.GetPosOnCircleXZ(-cs_Rigid.GetRotation_XZ() + m_CastIndex * m_OffFan, m_Distance),
                        l_Barrier);
                }
            }
            //RayCast
            else
            if (m_Cast == 2)
            {
                if (cs_Eye.GetRayCast_Vec_Check(
                    transform.position + v3_OffPos,
                    transform.position + v3_OffPos + ClassVector.GetPosOnCircleXZ(-cs_Rigid.GetRotation_XZ() + m_CastIndex * m_OffFan, m_Distance), m_Distance, l_Barrier))
                {
                    //Always Hit Tarket if not have Barrier
                    return cs_Eye.GetRayCast_Vec_RaycastHit(
                        transform.position + v3_OffPos, transform.position + v3_OffPos + ClassVector.GetPosOnCircleXZ(-cs_Rigid.GetRotation_XZ() + m_CastIndex * m_OffFan, m_Distance),
                        m_Distance,
                        l_Barrier);
                }
            }
            //BoxCast
            else
            if (m_Cast == 3)
            {
                if (cs_Eye.GetBoxCast_Vec_Check(
                    transform.position + v3_OffPos,
                    v3_Square,
                    transform.position + v3_OffPos + ClassVector.GetPosOnCircleXZ(-cs_Rigid.GetRotation_XZ() + m_CastIndex * m_OffFan, m_Distance),
                    new Vector3(0, 0, 0),
                    m_Distance,
                    l_Barrier))
                {
                    //Always Hit Tarket if not have Barrier
                    return cs_Eye.GetBoxCast_Vec_RaycastHit(
                        transform.position + v3_OffPos,
                        v3_Square,
                        transform.position + v3_OffPos + ClassVector.GetPosOnCircleXZ(-cs_Rigid.GetRotation_XZ() + m_CastIndex * m_OffFan, m_Distance),
                        new Vector3(0, 0, 0),
                        m_Distance,
                        l_Barrier);
                }
            }
            //SphereCast
            else
            if (m_Cast == 4)
            {
                if (cs_Eye.GetSphereCast_Vec_Check(
                    transform.position + v3_OffPos,
                    m_Sphere,
                    transform.position + v3_OffPos + ClassVector.GetPosOnCircleXZ(-cs_Rigid.GetRotation_XZ() + m_CastIndex * m_OffFan, m_Distance),
                    m_Distance,
                    l_Barrier))
                {
                    //Always Hit Tarket if not have Barrier
                    return cs_Eye.GetSphereCast_Vec_RaycastHit(
                        transform.position + v3_OffPos,
                        m_Sphere,
                        transform.position + v3_OffPos + ClassVector.GetPosOnCircleXZ(-cs_Rigid.GetRotation_XZ() + m_CastIndex * m_OffFan, m_Distance),
                        m_Distance,
                        l_Barrier);
                }
            }
        }

        return new RaycastHit();
    }

    public List<RaycastHit> GetList()
    //Get List EyeCast (0:Centre; %2!=0: Top; %2==0: Bot)
    {
        List<RaycastHit> l_RaycastHit = new List<RaycastHit>
        {

            //Centre
            GetEye(0)
        };

        for (int i = 1; i <= m_Fan; i++)
        {
            //Top (%2!=0)
            l_RaycastHit.Add(GetEye(i));
            //Bot (%2==0)
            l_RaycastHit.Add(GetEye(-i));
        }

        return l_RaycastHit;
    }

    public List<float> GetList_Distance(int m_Minimize)
    //Get List Distance (Decrease by Minimize)
    {
        List<RaycastHit> l1_Ray = GetList();

        List<float> l1_Distance = new List<float>();

        for (int i = 0; i < l1_Ray.Count; i++)
        {
            if (l1_Ray[i].collider == null)
            {
                l1_Distance.Add(-1f);
            }
            else
            {
                l1_Distance.Add(l1_Ray[i].distance / m_Minimize);
            }
        }
        return l1_Distance;
    }

    public List<float> GetList_Distance()
    //Get List Distance
    {
        List<RaycastHit> l1_Ray = GetList();

        List<float> l1_Distance = new List<float>();

        for (int i = 0; i < l1_Ray.Count; i++)
        {
            if (l1_Ray[i].collider == null)
            {
                l1_Distance.Add(-1f);
            }
            else
            {
                l1_Distance.Add(l1_Ray[i].distance);
            }
        }
        return l1_Distance;
    }

    public RaycastHit GetEye_Top(int m_Index)
    //Get EyeCast Top
    {
        return GetEye(m_Index * 2 - 1);
    }

    public bool GetEye_Top_Check(int m_Index)
    //Get Check EyeCast Top
    {
        return GetEye(m_Index * 2 - 1).collider != null;
    }

    public RaycastHit GetEye_Bot(int m_Index)
    //Get EyeCast Bot
    {
        return GetEye(m_Index * 2);
    }

    public bool GetEye_Bot_Check(int m_Index)
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

    private void Set_Gizmos(int m_CastIndex)
    {
        Class_Eye cs_Eye = new Class_Eye();
        RigidbodyComponent cs_Rigid = GetComponent<RigidbodyComponent>();

        if (!m_XZ)
        {
            //LineCast
            if (m_Cast == 1)
            {
                if (cs_Eye.GetLineCast_Check(
                    transform.position + v3_OffPos,
                    transform.position + v3_OffPos + ClassVector.GetPosOnCircleXY(-cs_Rigid.GetRotation_XZ() + m_CastIndex * m_OffFan, m_Distance), l_Barrier))
                {
                    //Always Hit Tarket if not have Barrier
                    RaycastHit ray_Hit = cs_Eye.GetLineCast_RaycastHit(
                        transform.position + v3_OffPos, transform.position + v3_OffPos + ClassVector.GetPosOnCircleXY(-cs_Rigid.GetRotation_XZ() + m_CastIndex * m_OffFan, m_Distance),
                        l_Barrier);
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(
                        transform.position + v3_OffPos,
                        transform.position + v3_OffPos + ClassVector.GetPosOnCircleXY(-cs_Rigid.GetRotation_XZ() + m_CastIndex * m_OffFan, m_Distance));
                }
                else
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(
                        transform.position + v3_OffPos,
                        transform.position + v3_OffPos + ClassVector.GetPosOnCircleXY(-cs_Rigid.GetRotation_XZ() + m_CastIndex * m_OffFan, m_Distance));
                }
            }
            //RayCast
            else
            if (m_Cast == 2)
            {
                if (cs_Eye.GetRayCast_Vec_Check(
                    transform.position + v3_OffPos,
                    transform.position + v3_OffPos + ClassVector.GetPosOnCircleXY(-cs_Rigid.GetRotation_XZ() + m_CastIndex * m_OffFan, m_Distance),
                    m_Distance,
                    l_Barrier))
                {
                    //Always Hit Tarket if not have Barrier
                    RaycastHit ray_Hit = cs_Eye.GetRayCast_Vec_RaycastHit(
                        transform.position + v3_OffPos, transform.position + v3_OffPos + ClassVector.GetPosOnCircleXY(-cs_Rigid.GetRotation_XZ() + m_CastIndex * m_OffFan, m_Distance),
                        m_Distance,
                        l_Barrier);
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(
                        transform.position + v3_OffPos,
                        transform.position + v3_OffPos + ClassVector.GetPosOnCircleXY(-cs_Rigid.GetRotation_XZ() + m_CastIndex * m_OffFan, ray_Hit.distance));
                }
                else
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(
                        transform.position + v3_OffPos,
                        transform.position + v3_OffPos + ClassVector.GetPosOnCircleXY(-cs_Rigid.GetRotation_XZ() + m_CastIndex * m_OffFan, m_Distance));
                }
            }
            //BoxCast
            else
            if (m_Cast == 3)
            {
                if (cs_Eye.GetBoxCast_Vec_Check(
                    transform.position + v3_OffPos,
                    v3_Square,
                    transform.position + v3_OffPos + ClassVector.GetPosOnCircleXY(-cs_Rigid.GetRotation_XZ() + m_CastIndex * m_OffFan, m_Distance),
                    new Vector3(0, 0, 0),
                    m_Distance,
                    l_Barrier))
                {
                    //Always Hit Tarket if not have Barrier
                    RaycastHit ray_Hit = cs_Eye.GetBoxCast_Vec_RaycastHit(
                        transform.position + v3_OffPos,
                        v3_Square,
                        transform.position + v3_OffPos + ClassVector.GetPosOnCircleXY(-cs_Rigid.GetRotation_XZ() + m_CastIndex * m_OffFan, m_Distance),
                        new Vector3(0, 0, 0),
                        m_Distance,
                        l_Barrier);
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(
                        transform.position + v3_OffPos,
                        transform.position + v3_OffPos + ClassVector.GetPosOnCircleXY(-cs_Rigid.GetRotation_XZ() + m_CastIndex * m_OffFan, ray_Hit.distance));
                    Gizmos.DrawWireCube(
                        transform.position + v3_OffPos + ClassVector.GetPosOnCircleXY(-cs_Rigid.GetRotation_XZ() + m_CastIndex * m_OffFan, ray_Hit.distance),
                        v3_Square);
                }
                else
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(
                        transform.position + v3_OffPos,
                        transform.position + v3_OffPos + ClassVector.GetPosOnCircleXY(-cs_Rigid.GetRotation_XZ() + m_CastIndex * m_OffFan, m_Distance));
                    Gizmos.DrawWireCube(
                        transform.position + v3_OffPos + ClassVector.GetPosOnCircleXY(-cs_Rigid.GetRotation_XZ() + m_CastIndex * m_OffFan, m_Distance),
                        v3_Square);
                }
            }
            //SphereCast
            else
            if (m_Cast == 4)
            {
                if (cs_Eye.GetSphereCast_Vec_Check(
                    transform.position + v3_OffPos,
                    m_Sphere,
                    transform.position + v3_OffPos + ClassVector.GetPosOnCircleXY(-cs_Rigid.GetRotation_XZ() + m_CastIndex * m_OffFan, m_Distance),
                    m_Distance,
                    l_Barrier))
                {
                    //Always Hit Tarket if not have Barrier
                    RaycastHit ray_Hit = cs_Eye.GetSphereCast_Vec_RaycastHit(
                        transform.position + v3_OffPos,
                        m_Sphere,
                        transform.position + v3_OffPos + ClassVector.GetPosOnCircleXY(-cs_Rigid.GetRotation_XZ() + m_CastIndex * m_OffFan, m_Distance),
                        m_Distance,
                        l_Barrier);
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(
                        transform.position + v3_OffPos,
                        transform.position + v3_OffPos + ClassVector.GetPosOnCircleXY(-cs_Rigid.GetRotation_XZ() + m_CastIndex * m_OffFan, ray_Hit.distance));
                    Gizmos.DrawWireSphere(
                        transform.position + v3_OffPos + ClassVector.GetPosOnCircleXY(-cs_Rigid.GetRotation_XZ() + m_CastIndex * m_OffFan, ray_Hit.distance),
                        m_Sphere / 2);
                }
                else
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(
                        transform.position + v3_OffPos,
                        transform.position + v3_OffPos + ClassVector.GetPosOnCircleXY(-cs_Rigid.GetRotation_XZ() + m_CastIndex * m_OffFan, m_Distance));
                    Gizmos.DrawWireSphere(
                        transform.position + v3_OffPos + ClassVector.GetPosOnCircleXY(-cs_Rigid.GetRotation_XZ() + m_CastIndex * m_OffFan, m_Distance),
                        m_Sphere / 2);
                }
            }
        }
        else
        if (m_XZ)
        {
            //LineCast
            if (m_Cast == 1)
            {
                if (cs_Eye.GetLineCast_Check(
                    transform.position + v3_OffPos,
                    transform.position + v3_OffPos + ClassVector.GetPosOnCircleXZ(-cs_Rigid.GetRotation_XZ() + m_CastIndex * m_OffFan, m_Distance), l_Barrier))
                {
                    //Always Hit Tarket if not have Barrier
                    RaycastHit ray_Hit = cs_Eye.GetLineCast_RaycastHit(
                        transform.position + v3_OffPos, transform.position + v3_OffPos + ClassVector.GetPosOnCircleXZ(-cs_Rigid.GetRotation_XZ() + m_CastIndex * m_OffFan, m_Distance),
                        l_Barrier);
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(
                        transform.position + v3_OffPos,
                        transform.position + v3_OffPos + ClassVector.GetPosOnCircleXZ(-cs_Rigid.GetRotation_XZ() + m_CastIndex * m_OffFan, m_Distance));
                }
                else
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(
                        transform.position + v3_OffPos,
                        transform.position + v3_OffPos + ClassVector.GetPosOnCircleXZ(-cs_Rigid.GetRotation_XZ() + m_CastIndex * m_OffFan, m_Distance));
                }
            }
            //RayCast
            else
            if (m_Cast == 2)
            {
                if (cs_Eye.GetRayCast_Vec_Check(
                    transform.position + v3_OffPos,
                    transform.position + v3_OffPos + ClassVector.GetPosOnCircleXZ(-cs_Rigid.GetRotation_XZ() + m_CastIndex * m_OffFan, m_Distance), m_Distance, l_Barrier))
                {
                    //Always Hit Tarket if not have Barrier
                    RaycastHit ray_Hit = cs_Eye.GetRayCast_Vec_RaycastHit(
                        transform.position + v3_OffPos, transform.position + v3_OffPos + ClassVector.GetPosOnCircleXZ(-cs_Rigid.GetRotation_XZ() + m_CastIndex * m_OffFan, m_Distance),
                        m_Distance,
                        l_Barrier);
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(
                        transform.position + v3_OffPos,
                        transform.position + v3_OffPos + ClassVector.GetPosOnCircleXZ(-cs_Rigid.GetRotation_XZ() + m_CastIndex * m_OffFan, ray_Hit.distance));
                }
                else
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(
                        transform.position + v3_OffPos,
                        transform.position + v3_OffPos + ClassVector.GetPosOnCircleXZ(-cs_Rigid.GetRotation_XZ() + m_CastIndex * m_OffFan, m_Distance));
                }
            }
            //BoxCast
            else
            if (m_Cast == 3)
            {
                if (cs_Eye.GetBoxCast_Vec_Check(
                    transform.position + v3_OffPos,
                    v3_Square,
                    transform.position + v3_OffPos + ClassVector.GetPosOnCircleXZ(-cs_Rigid.GetRotation_XZ() + m_CastIndex * m_OffFan, m_Distance),
                    new Vector3(0, 0, 0),
                    m_Distance,
                    l_Barrier))
                {
                    //Always Hit Tarket if not have Barrier
                    RaycastHit ray_Hit = cs_Eye.GetBoxCast_Vec_RaycastHit(
                        transform.position + v3_OffPos,
                        v3_Square,
                        transform.position + v3_OffPos + ClassVector.GetPosOnCircleXZ(-cs_Rigid.GetRotation_XZ() + m_CastIndex * m_OffFan, m_Distance),
                        new Vector3(0, 0, 0),
                        m_Distance,
                        l_Barrier);
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(
                        transform.position + v3_OffPos,
                        transform.position + v3_OffPos + ClassVector.GetPosOnCircleXZ(-cs_Rigid.GetRotation_XZ() + m_CastIndex * m_OffFan, ray_Hit.distance));
                    Gizmos.DrawWireCube(
                        transform.position + v3_OffPos + ClassVector.GetPosOnCircleXZ(-cs_Rigid.GetRotation_XZ() + m_CastIndex * m_OffFan, ray_Hit.distance),
                        v3_Square);
                }
                else
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(
                        transform.position + v3_OffPos,
                        transform.position + v3_OffPos + ClassVector.GetPosOnCircleXZ(-cs_Rigid.GetRotation_XZ() + m_CastIndex * m_OffFan, m_Distance));
                    Gizmos.DrawWireCube(
                        transform.position + v3_OffPos + ClassVector.GetPosOnCircleXZ(-cs_Rigid.GetRotation_XZ() + m_CastIndex * m_OffFan, m_Distance),
                        v3_Square);
                }
            }
            //SphereCast
            else
            if (m_Cast == 4)
            {
                if (cs_Eye.GetSphereCast_Vec_Check(
                    transform.position + v3_OffPos,
                    m_Sphere,
                    transform.position + v3_OffPos + ClassVector.GetPosOnCircleXZ(-cs_Rigid.GetRotation_XZ() + m_CastIndex * m_OffFan, m_Distance),
                    m_Distance,
                    l_Barrier))
                {
                    //Always Hit Tarket if not have Barrier
                    RaycastHit ray_Hit = cs_Eye.GetSphereCast_Vec_RaycastHit(
                        transform.position + v3_OffPos,
                        m_Sphere,
                        transform.position + v3_OffPos + ClassVector.GetPosOnCircleXZ(-cs_Rigid.GetRotation_XZ() + m_CastIndex * m_OffFan, m_Distance),
                        m_Distance,
                        l_Barrier);
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(
                        transform.position + v3_OffPos,
                        transform.position + v3_OffPos + ClassVector.GetPosOnCircleXZ(-cs_Rigid.GetRotation_XZ() + m_CastIndex * m_OffFan, ray_Hit.distance));
                    Gizmos.DrawWireSphere(
                        transform.position + v3_OffPos + ClassVector.GetPosOnCircleXZ(-cs_Rigid.GetRotation_XZ() + m_CastIndex * m_OffFan, ray_Hit.distance),
                        m_Sphere / 2);
                }
                else
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(
                        transform.position + v3_OffPos,
                        transform.position + v3_OffPos + ClassVector.GetPosOnCircleXZ(-cs_Rigid.GetRotation_XZ() + m_CastIndex * m_OffFan, m_Distance));
                    Gizmos.DrawWireSphere(
                        transform.position + v3_OffPos + ClassVector.GetPosOnCircleXZ(-cs_Rigid.GetRotation_XZ() + m_CastIndex * m_OffFan, m_Distance),
                        m_Sphere / 2);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        //Centre
        Set_Gizmos(0);

        //Fan
        Gizmos.color = Color.red;

        for (int i = 1; i <= m_Fan; i++)
        {
            Set_Gizmos(i);
            Set_Gizmos(-i);
        }
    }
}