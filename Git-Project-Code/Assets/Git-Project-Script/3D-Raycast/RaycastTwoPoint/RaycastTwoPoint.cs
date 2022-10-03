using UnityEngine;

public class RaycastTwoPoint : MonoBehaviour
{
    [Header("Main")]
    public LayerMask m_Tarket;
    public LayerMask m_Barrier;
    //LayerMask Debug

    public Transform tStart;
    public Transform t_End;
    //Point

    [Header("End Point is 'Pos' or 'Dir'?")]
    public bool mAllowEndPos = true;
    public float m_Distance = 5f;

    [Header("None - Line - Ray - Box - Sphere Cast")]
    [Range(0, 4)]
    public int m_Cast = 0;
    //Type of Cast

    [Header("BoxCast")]
    public Vector3 m_Square = new Vector3(0.1f, 0.1f, 0.1f);
    public Vector3 m_SquareRot = new Vector3();

    [Header("SphereCast")]
    public float m_Sphere = 0.1f;

    public float GetDistance_Transform()
    //Distance from Start to End Point
    {
        return ClassVector.GetDistance(tStart, t_End);
    }

    //Cast của Start và End

    public bool GetCheckLineCastLayerMask()
    //LineCast from Start to End
    {
        ClassEye m_Eye = new ClassEye();
        if (m_Eye.GetCheckLineCast(tStart.position, t_End.position, m_Tarket))
        {
            //Always Hit if Tarket on LineCast
            return true;
        }
        return false;
    }

    public bool GetCheckRayCastLayerMask()
    //Raycast from Start to End
    {
        ClassEye m_Eye = new ClassEye();
        if (m_Eye.GetCheckRayCastVector(tStart.position, t_End.position, GetDistance_Transform(), m_Barrier))
        {
            //Hit Barrier
            return false;
        }
        else
        if (m_Eye.GetCheckRayCastVector(tStart.position, t_End.position, GetDistance_Transform(), m_Tarket))
        {
            //Hit Tarket
            return true;
        }
        return false;
    }

    public bool GetCheckBoxCastLayerMask()
    //BoxCast from Start to End
    {
        ClassEye m_Eye = new ClassEye();
        if (m_Eye.GetCheckBoxCastVector(tStart.position, m_Square, t_End.position, m_SquareRot, GetDistance_Transform(), m_Barrier))
        {
            //Hit Barrier
            return false;
        }
        else
            if (m_Eye.GetCheckBoxCastVector(tStart.position, m_Square, t_End.position, m_SquareRot, GetDistance_Transform(), m_Tarket))
        {
            //Hit Tarket
            return true;
        }
        return false;
    }

    public bool GetCheckSphereCastLayerMask()
    //SphereCast from Start to End
    {
        ClassEye m_Eye = new ClassEye();
        if (m_Eye.GetCheckSphereCastVector(tStart.position, m_Sphere, t_End.position, GetDistance_Transform(), m_Barrier))
        {
            //Hit Barrier
            return false;
        }
        else
            if (m_Eye.GetCheckSphereCastVector(tStart.position, m_Sphere, t_End.position, GetDistance_Transform(), m_Tarket))
        {
            //Hit Tarket
            return true;
        }
        return false;
    }

    //Gizmos

    private void OnDrawGizmos()
    {
        if (m_Cast == 0 || tStart == null || t_End == null)
        {
            return;
        }

        ClassEye m_Eye = new ClassEye();

        switch (m_Cast)
        {
            case 1:
                //LineCast
                Gizmos.color = Color.black;
                Gizmos.DrawWireSphere(
                    t_End.position,
                    0.1f);
                //End Point is Black
                if (m_Eye.GetCheckLineCast(
                    tStart.position,
                    t_End.position,
                    m_Tarket))
                {
                    //Hit Tarket
                    RaycastHit ray_Hit = m_Eye.GetLineCastRaycastHit(
                        tStart.position,
                        t_End.position,
                        m_Tarket);
                    Gizmos.color = Color.red;
                    //Red is Hit
                    Gizmos.DrawLine(
                        tStart.position,
                        t_End.position);
                }
                else
                {
                    Gizmos.color = Color.white;
                    //Start Point is White, if Hit is Red
                    Gizmos.DrawLine(
                        tStart.position,
                        t_End.position);
                }
                Gizmos.DrawWireSphere(tStart.position, m_Sphere / 2);
                break;
            case 2:
                //RayCast
                Gizmos.color = Color.black;
                Gizmos.DrawWireSphere(
                    t_End.position,
                    0.1f);
                if (m_Eye.GetCheckRayCastVector(
                    tStart.position,
                    t_End.position,
                    (mAllowEndPos) ? GetDistance_Transform() : m_Distance,
                    m_Barrier))
                {
                    //Hit Barrier
                    RaycastHit ray_Hit = m_Eye.GetRayCastVectorRaycastHit(
                        tStart.position,
                        t_End.position,
                        (mAllowEndPos) ? GetDistance_Transform() : m_Distance,
                        m_Barrier);
                    Gizmos.color = Color.white;
                    Gizmos.DrawRay(
                        tStart.position,
                        (t_End.position - tStart.position).normalized * ray_Hit.distance);
                }
                else
                if (m_Eye.GetCheckRayCastVector(
                    tStart.position,
                    t_End.position,
                    (mAllowEndPos) ? GetDistance_Transform() : m_Distance,
                    m_Tarket))
                {
                    //Hit Tarket
                    RaycastHit ray_Hit = m_Eye.GetRayCastVectorRaycastHit(
                        tStart.position,
                        t_End.position,
                        (mAllowEndPos) ? GetDistance_Transform() : m_Distance,
                        m_Tarket);
                    Gizmos.color = Color.red;
                    Gizmos.DrawRay(
                        tStart.position,
                        (t_End.position - tStart.position).normalized * ray_Hit.distance);
                }
                else
                {
                    Gizmos.color = Color.white;
                    Gizmos.DrawRay(
                        tStart.position,
                        (t_End.position - tStart.position).normalized * ((mAllowEndPos) ? GetDistance_Transform() : m_Distance));
                }
                Gizmos.DrawWireSphere(tStart.position, m_Sphere / 2);
                break;
            case 3:
                //BoxCast
                Gizmos.color = Color.black;
                Gizmos.DrawWireSphere(
                    t_End.position,
                    m_Sphere / 2);
                if (m_Eye.GetCheckBoxCastVector(
                    tStart.position,
                    m_Square,
                    t_End.position,
                    m_SquareRot,
                    (mAllowEndPos) ? GetDistance_Transform() : m_Distance,
                    m_Barrier))
                {
                    //If Hit
                    RaycastHit ray_Hit = m_Eye.GetBoxCastVectorRaycastHit(
                        tStart.position,
                        m_Square,
                        t_End.position,
                        m_SquareRot,
                        (mAllowEndPos) ? GetDistance_Transform() : m_Distance,
                        m_Barrier);
                    Gizmos.color = Color.white;
                    Gizmos.DrawLine(
                        tStart.position,
                        tStart.position + (t_End.position - tStart.position).normalized * ray_Hit.distance);
                    Gizmos.DrawWireCube(
                        tStart.position + (t_End.position - tStart.position).normalized * ray_Hit.distance,
                        m_Square);
                }
                else
                if (m_Eye.GetCheckBoxCastVector(
                    tStart.position,
                    m_Square,
                    t_End.position,
                    m_SquareRot,
                    (mAllowEndPos) ? GetDistance_Transform() : m_Distance,
                    m_Tarket))
                {
                    //If Hit
                    RaycastHit ray_Hit = m_Eye.GetBoxCastVectorRaycastHit(
                        tStart.position,
                        m_Square,
                        t_End.position,
                        m_SquareRot,
                        (mAllowEndPos) ? GetDistance_Transform() : m_Distance,
                        m_Tarket);
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(
                        tStart.position,
                        tStart.position + (t_End.position - tStart.position).normalized * ray_Hit.distance);
                    Gizmos.DrawWireCube(
                        tStart.position + (t_End.position - tStart.position).normalized * ray_Hit.distance,
                        m_Square);
                }
                else
                {
                    Gizmos.color = Color.white;
                    Gizmos.DrawLine(
                        tStart.position,
                        tStart.position + (t_End.position - tStart.position).normalized * ((mAllowEndPos) ? GetDistance_Transform() : m_Distance));
                    Gizmos.DrawWireCube(
                        tStart.position + (t_End.position - tStart.position).normalized * m_Distance,
                        m_Square);
                }
                Gizmos.DrawWireCube(
                    tStart.position,
                    m_Square);
                break;
            case 4:
                //SphereCast
                Gizmos.color = Color.black;
                Gizmos.DrawWireSphere(
                    t_End.position,
                    m_Sphere / 2);
                if (m_Eye.GetCheckSphereCastVector(
                    tStart.position,
                    m_Sphere,
                    t_End.position,
                    (mAllowEndPos) ? GetDistance_Transform() : m_Distance,
                    m_Barrier))
                {
                    //If Hit
                    RaycastHit ray_Hit = m_Eye.GetSphereCastVectorRaycastHit(
                        tStart.position,
                        m_Sphere,
                        t_End.position,
                        (mAllowEndPos) ? GetDistance_Transform() : m_Distance,
                        m_Barrier);
                    Gizmos.color = Color.white;
                    Gizmos.DrawLine(
                        tStart.position,
                        tStart.position + (t_End.position - tStart.position).normalized * ray_Hit.distance);
                    Gizmos.DrawWireSphere(
                        tStart.position + (t_End.position - tStart.position).normalized * ray_Hit.distance,
                        m_Sphere / 2);
                }
                else
                if (m_Eye.GetCheckSphereCastVector(
                    tStart.position,
                    m_Sphere,
                    t_End.position,
                    (mAllowEndPos) ? GetDistance_Transform() : m_Distance,
                    m_Tarket))
                {
                    //If Hit
                    RaycastHit ray_Hit = m_Eye.GetSphereCastVectorRaycastHit(
                        tStart.position,
                        m_Sphere,
                        t_End.position,
                        (mAllowEndPos) ? GetDistance_Transform() : m_Distance,
                        m_Tarket);
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(
                        tStart.position,
                        tStart.position + (t_End.position - tStart.position).normalized * ray_Hit.distance);
                    Gizmos.DrawWireSphere(
                        tStart.position + (t_End.position - tStart.position).normalized * ray_Hit.distance,
                        m_Sphere / 2);
                }
                else
                {
                    Gizmos.color = Color.white;
                    Gizmos.DrawLine(
                        tStart.position,
                        tStart.position + (t_End.position - tStart.position).normalized * ((mAllowEndPos) ? GetDistance_Transform() : m_Distance));
                    Gizmos.DrawWireSphere(
                        tStart.position + (t_End.position - tStart.position).normalized * ((mAllowEndPos) ? GetDistance_Transform() : m_Distance),
                        m_Sphere / 2);
                }
                Gizmos.DrawWireSphere(
                    tStart.position,
                    m_Sphere / 2);
                break;
        }
    }
}
