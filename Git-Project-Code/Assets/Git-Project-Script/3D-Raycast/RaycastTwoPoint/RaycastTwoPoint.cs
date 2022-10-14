using UnityEngine;

public class RaycastTwoPoint : MonoBehaviour
{
    [Header("Main")]

    public LayerMask m_Tarket;
    public LayerMask m_Barrier;

    public Transform m_TransformStart;
    public Transform m_TransformEnd;

    [Header("End Point is 'Pos' or 'Dir'?")]

    public bool m_AllowEndPos = true;
    public float m_Distance = 5f;

    [Header("None - Line - Ray - Box - Sphere Cast")]

    [Range(0, 4)]
    public int m_Cast = 0;

    [Header("BoxCast")]

    public Vector3 m_Square = new Vector3(0.1f, 0.1f, 0.1f);
    public Vector3 m_SquareRot = new Vector3();

    [Header("SphereCast")]

    public float m_Sphere = 0.1f;

    public float GetDistance_Transform()
    {
        return ClassVector.GetDistance(m_TransformStart, m_TransformEnd);
    }

    //Cast của Start và End

    public bool GetCheckLineCastLayerMask()
    {
        ClassEye m_Eye = new ClassEye();
        if (m_Eye.GetCheckLineCast(m_TransformStart.position, m_TransformEnd.position, m_Tarket))
        {
            //Always Hit if Tarket on LineCast
            return true;
        }
        return false;
    }

    public bool GetCheckRayCastLayerMask()
    {
        ClassEye m_Eye = new ClassEye();
        if (m_Eye.GetCheckRayCastVector(m_TransformStart.position, m_TransformEnd.position, GetDistance_Transform(), m_Barrier))
        {
            //Hit Barrier
            return false;
        }
        else
        if (m_Eye.GetCheckRayCastVector(m_TransformStart.position, m_TransformEnd.position, GetDistance_Transform(), m_Tarket))
        {
            //Hit Tarket
            return true;
        }
        return false;
    }

    public bool GetCheckBoxCastLayerMask()
    {
        ClassEye m_Eye = new ClassEye();
        if (m_Eye.GetCheckBoxCastVector(m_TransformStart.position, m_Square, m_TransformEnd.position, m_SquareRot, GetDistance_Transform(), m_Barrier))
        {
            //Hit Barrier
            return false;
        }
        else
            if (m_Eye.GetCheckBoxCastVector(m_TransformStart.position, m_Square, m_TransformEnd.position, m_SquareRot, GetDistance_Transform(), m_Tarket))
        {
            //Hit Tarket
            return true;
        }
        return false;
    }

    public bool GetCheckSphereCastLayerMask()
    {
        ClassEye m_Eye = new ClassEye();
        if (m_Eye.GetCheckSphereCastVector(m_TransformStart.position, m_Sphere, m_TransformEnd.position, GetDistance_Transform(), m_Barrier))
        {
            //Hit Barrier
            return false;
        }
        else
            if (m_Eye.GetCheckSphereCastVector(m_TransformStart.position, m_Sphere, m_TransformEnd.position, GetDistance_Transform(), m_Tarket))
        {
            //Hit Tarket
            return true;
        }
        return false;
    }

    //Gizmos

    private void OnDrawGizmos()
    {
        if (m_Cast == 0 || m_TransformStart == null || m_TransformEnd == null)
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
                    m_TransformEnd.position,
                    0.1f);
                //End Point is Black
                if (m_Eye.GetCheckLineCast(
                    m_TransformStart.position,
                    m_TransformEnd.position,
                    m_Tarket))
                {
                    //Hit Tarket
                    RaycastHit ray_Hit = m_Eye.GetLineCastRaycastHit(
                        m_TransformStart.position,
                        m_TransformEnd.position,
                        m_Tarket);
                    Gizmos.color = Color.red;
                    //Red is Hit
                    Gizmos.DrawLine(
                        m_TransformStart.position,
                        m_TransformEnd.position);
                }
                else
                {
                    Gizmos.color = Color.white;
                    //Start Point is White, if Hit is Red
                    Gizmos.DrawLine(
                        m_TransformStart.position,
                        m_TransformEnd.position);
                }
                Gizmos.DrawWireSphere(m_TransformStart.position, m_Sphere / 2);
                break;
            case 2:
                //RayCast
                Gizmos.color = Color.black;
                Gizmos.DrawWireSphere(
                    m_TransformEnd.position,
                    0.1f);
                if (m_Eye.GetCheckRayCastVector(
                    m_TransformStart.position,
                    m_TransformEnd.position,
                    (m_AllowEndPos) ? GetDistance_Transform() : m_Distance,
                    m_Barrier))
                {
                    //Hit Barrier
                    RaycastHit ray_Hit = m_Eye.GetRayCastVectorRaycastHit(
                        m_TransformStart.position,
                        m_TransformEnd.position,
                        (m_AllowEndPos) ? GetDistance_Transform() : m_Distance,
                        m_Barrier);
                    Gizmos.color = Color.white;
                    Gizmos.DrawRay(
                        m_TransformStart.position,
                        (m_TransformEnd.position - m_TransformStart.position).normalized * ray_Hit.distance);
                }
                else
                if (m_Eye.GetCheckRayCastVector(
                    m_TransformStart.position,
                    m_TransformEnd.position,
                    (m_AllowEndPos) ? GetDistance_Transform() : m_Distance,
                    m_Tarket))
                {
                    //Hit Tarket
                    RaycastHit ray_Hit = m_Eye.GetRayCastVectorRaycastHit(
                        m_TransformStart.position,
                        m_TransformEnd.position,
                        (m_AllowEndPos) ? GetDistance_Transform() : m_Distance,
                        m_Tarket);
                    Gizmos.color = Color.red;
                    Gizmos.DrawRay(
                        m_TransformStart.position,
                        (m_TransformEnd.position - m_TransformStart.position).normalized * ray_Hit.distance);
                }
                else
                {
                    Gizmos.color = Color.white;
                    Gizmos.DrawRay(
                        m_TransformStart.position,
                        (m_TransformEnd.position - m_TransformStart.position).normalized * ((m_AllowEndPos) ? GetDistance_Transform() : m_Distance));
                }
                Gizmos.DrawWireSphere(m_TransformStart.position, m_Sphere / 2);
                break;
            case 3:
                //BoxCast
                Gizmos.color = Color.black;
                Gizmos.DrawWireSphere(
                    m_TransformEnd.position,
                    m_Sphere / 2);
                if (m_Eye.GetCheckBoxCastVector(
                    m_TransformStart.position,
                    m_Square,
                    m_TransformEnd.position,
                    m_SquareRot,
                    (m_AllowEndPos) ? GetDistance_Transform() : m_Distance,
                    m_Barrier))
                {
                    //If Hit
                    RaycastHit ray_Hit = m_Eye.GetBoxCastVectorRaycastHit(
                        m_TransformStart.position,
                        m_Square,
                        m_TransformEnd.position,
                        m_SquareRot,
                        (m_AllowEndPos) ? GetDistance_Transform() : m_Distance,
                        m_Barrier);
                    Gizmos.color = Color.white;
                    Gizmos.DrawLine(
                        m_TransformStart.position,
                        m_TransformStart.position + (m_TransformEnd.position - m_TransformStart.position).normalized * ray_Hit.distance);
                    Gizmos.DrawWireCube(
                        m_TransformStart.position + (m_TransformEnd.position - m_TransformStart.position).normalized * ray_Hit.distance,
                        m_Square);
                }
                else
                if (m_Eye.GetCheckBoxCastVector(
                    m_TransformStart.position,
                    m_Square,
                    m_TransformEnd.position,
                    m_SquareRot,
                    (m_AllowEndPos) ? GetDistance_Transform() : m_Distance,
                    m_Tarket))
                {
                    //If Hit
                    RaycastHit ray_Hit = m_Eye.GetBoxCastVectorRaycastHit(
                        m_TransformStart.position,
                        m_Square,
                        m_TransformEnd.position,
                        m_SquareRot,
                        (m_AllowEndPos) ? GetDistance_Transform() : m_Distance,
                        m_Tarket);
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(
                        m_TransformStart.position,
                        m_TransformStart.position + (m_TransformEnd.position - m_TransformStart.position).normalized * ray_Hit.distance);
                    Gizmos.DrawWireCube(
                        m_TransformStart.position + (m_TransformEnd.position - m_TransformStart.position).normalized * ray_Hit.distance,
                        m_Square);
                }
                else
                {
                    Gizmos.color = Color.white;
                    Gizmos.DrawLine(
                        m_TransformStart.position,
                        m_TransformStart.position + (m_TransformEnd.position - m_TransformStart.position).normalized * ((m_AllowEndPos) ? GetDistance_Transform() : m_Distance));
                    Gizmos.DrawWireCube(
                        m_TransformStart.position + (m_TransformEnd.position - m_TransformStart.position).normalized * m_Distance,
                        m_Square);
                }
                Gizmos.DrawWireCube(
                    m_TransformStart.position,
                    m_Square);
                break;
            case 4:
                //SphereCast
                Gizmos.color = Color.black;
                Gizmos.DrawWireSphere(
                    m_TransformEnd.position,
                    m_Sphere / 2);
                if (m_Eye.GetCheckSphereCastVector(
                    m_TransformStart.position,
                    m_Sphere,
                    m_TransformEnd.position,
                    (m_AllowEndPos) ? GetDistance_Transform() : m_Distance,
                    m_Barrier))
                {
                    //If Hit
                    RaycastHit ray_Hit = m_Eye.GetSphereCastVectorRaycastHit(
                        m_TransformStart.position,
                        m_Sphere,
                        m_TransformEnd.position,
                        (m_AllowEndPos) ? GetDistance_Transform() : m_Distance,
                        m_Barrier);
                    Gizmos.color = Color.white;
                    Gizmos.DrawLine(
                        m_TransformStart.position,
                        m_TransformStart.position + (m_TransformEnd.position - m_TransformStart.position).normalized * ray_Hit.distance);
                    Gizmos.DrawWireSphere(
                        m_TransformStart.position + (m_TransformEnd.position - m_TransformStart.position).normalized * ray_Hit.distance,
                        m_Sphere / 2);
                }
                else
                if (m_Eye.GetCheckSphereCastVector(
                    m_TransformStart.position,
                    m_Sphere,
                    m_TransformEnd.position,
                    (m_AllowEndPos) ? GetDistance_Transform() : m_Distance,
                    m_Tarket))
                {
                    //If Hit
                    RaycastHit ray_Hit = m_Eye.GetSphereCastVectorRaycastHit(
                        m_TransformStart.position,
                        m_Sphere,
                        m_TransformEnd.position,
                        (m_AllowEndPos) ? GetDistance_Transform() : m_Distance,
                        m_Tarket);
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(
                        m_TransformStart.position,
                        m_TransformStart.position + (m_TransformEnd.position - m_TransformStart.position).normalized * ray_Hit.distance);
                    Gizmos.DrawWireSphere(
                        m_TransformStart.position + (m_TransformEnd.position - m_TransformStart.position).normalized * ray_Hit.distance,
                        m_Sphere / 2);
                }
                else
                {
                    Gizmos.color = Color.white;
                    Gizmos.DrawLine(
                        m_TransformStart.position,
                        m_TransformStart.position + (m_TransformEnd.position - m_TransformStart.position).normalized * ((m_AllowEndPos) ? GetDistance_Transform() : m_Distance));
                    Gizmos.DrawWireSphere(
                        m_TransformStart.position + (m_TransformEnd.position - m_TransformStart.position).normalized * ((m_AllowEndPos) ? GetDistance_Transform() : m_Distance),
                        m_Sphere / 2);
                }
                Gizmos.DrawWireSphere(
                    m_TransformStart.position,
                    m_Sphere / 2);
                break;
        }
    }
}
