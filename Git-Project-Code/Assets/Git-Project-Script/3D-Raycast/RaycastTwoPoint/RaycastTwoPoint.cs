using UnityEngine;

public class RaycastTwoPoint : MonoBehaviour
{
    [Header("Main")]
    public LayerMask l_Tarket;
    public LayerMask l_Barrier;
    //LayerMask Debug

    public Transform t_Start;
    public Transform t_End;
    //Point

    [Header("End Point is 'Pos' or 'Dir'?")]
    public bool m_EndIsPos = true;
    public float m_Distance = 5f;

    [Header("None - Line - Ray - Box - Sphere Cast")]
    [Range(0, 4)]
    public int m_Cast = 0;
    //Type of Cast

    [Header("BoxCast")]
    public Vector3 v3_Square = new Vector3(0.1f, 0.1f, 0.1f);
    public Vector3 v3_Square_Rot = new Vector3();

    [Header("SphereCast")]
    public float m_Sphere = 0.1f;

    public float GetDistance_Transform()
    //Distance from Start to End Point
    {
        return ClassVector.GetDistance(t_Start, t_End);
    }

    //Cast của Start và End

    public bool GetLineCast_Check_LayerMask()
    //LineCast from Start to End
    {
        Clasm_Eye cm_Eye = new Clasm_Eye();
        if (cm_Eye.GetLineCast_Check(t_Start.position, t_End.position, l_Tarket))
        {
            //Always Hit if Tarket on LineCast
            return true;
        }
        return false;
    }

    public bool GetRayCast_Check_LayerMask()
    //Raycast from Start to End
    {
        Clasm_Eye cm_Eye = new Clasm_Eye();
        if (cm_Eye.GetRayCast_Vec_Check(t_Start.position, t_End.position, GetDistance_Transform(), l_Barrier))
        {
            //Hit Barrier
            return false;
        }
        else
        if (cm_Eye.GetRayCast_Vec_Check(t_Start.position, t_End.position, GetDistance_Transform(), l_Tarket))
        {
            //Hit Tarket
            return true;
        }
        return false;
    }

    public bool GetBoxCast_Check_LayerMask()
    //BoxCast from Start to End
    {
        Clasm_Eye cm_Eye = new Clasm_Eye();
        if (cm_Eye.GetBoxCast_Vec_Check(t_Start.position, v3_Square, t_End.position, v3_Square_Rot, GetDistance_Transform(), l_Barrier))
        {
            //Hit Barrier
            return false;
        }
        else
            if (cm_Eye.GetBoxCast_Vec_Check(t_Start.position, v3_Square, t_End.position, v3_Square_Rot, GetDistance_Transform(), l_Tarket))
        {
            //Hit Tarket
            return true;
        }
        return false;
    }

    public bool GetSphereCast_Check_LayerMask()
    //SphereCast from Start to End
    {
        Clasm_Eye cm_Eye = new Clasm_Eye();
        if (cm_Eye.GetSphereCast_Vec_Check(t_Start.position, m_Sphere, t_End.position, GetDistance_Transform(), l_Barrier))
        {
            //Hit Barrier
            return false;
        }
        else
            if (cm_Eye.GetSphereCast_Vec_Check(t_Start.position, m_Sphere, t_End.position, GetDistance_Transform(), l_Tarket))
        {
            //Hit Tarket
            return true;
        }
        return false;
    }

    //Gizmos

    private void OnDrawGizmos()
    {
        if (m_Cast == 0 || t_Start == null || t_End == null)
        {
            return;
        }

        Clasm_Eye cm_Eye = new Clasm_Eye();

        switch (m_Cast)
        {
            case 1:
                //LineCast
                Gizmos.color = Color.black;
                Gizmos.DrawWireSphere(
                    t_End.position,
                    0.1f);
                //End Point is Black
                if (cm_Eye.GetLineCast_Check(
                    t_Start.position,
                    t_End.position,
                    l_Tarket))
                {
                    //Hit Tarket
                    RaycastHit ray_Hit = cm_Eye.GetLineCast_RaycastHit(
                        t_Start.position,
                        t_End.position,
                        l_Tarket);
                    Gizmos.color = Color.red;
                    //Red is Hit
                    Gizmos.DrawLine(
                        t_Start.position,
                        t_End.position);
                }
                else
                {
                    Gizmos.color = Color.white;
                    //Start Point is White, if Hit is Red
                    Gizmos.DrawLine(
                        t_Start.position,
                        t_End.position);
                }
                Gizmos.DrawWireSphere(t_Start.position, m_Sphere / 2);
                break;
            case 2:
                //RayCast
                Gizmos.color = Color.black;
                Gizmos.DrawWireSphere(
                    t_End.position,
                    0.1f);
                if (cm_Eye.GetRayCast_Vec_Check(
                    t_Start.position,
                    t_End.position,
                    (m_EndIsPos) ? GetDistance_Transform() : m_Distance,
                    l_Barrier))
                {
                    //Hit Barrier
                    RaycastHit ray_Hit = cm_Eye.GetRayCast_Vec_RaycastHit(
                        t_Start.position,
                        t_End.position,
                        (m_EndIsPos) ? GetDistance_Transform() : m_Distance,
                        l_Barrier);
                    Gizmos.color = Color.white;
                    Gizmos.DrawRay(
                        t_Start.position,
                        (t_End.position - t_Start.position).normalized * ray_Hit.distance);
                }
                else
                if (cm_Eye.GetRayCast_Vec_Check(
                    t_Start.position,
                    t_End.position,
                    (m_EndIsPos) ? GetDistance_Transform() : m_Distance,
                    l_Tarket))
                {
                    //Hit Tarket
                    RaycastHit ray_Hit = cm_Eye.GetRayCast_Vec_RaycastHit(
                        t_Start.position,
                        t_End.position,
                        (m_EndIsPos) ? GetDistance_Transform() : m_Distance,
                        l_Tarket);
                    Gizmos.color = Color.red;
                    Gizmos.DrawRay(
                        t_Start.position,
                        (t_End.position - t_Start.position).normalized * ray_Hit.distance);
                }
                else
                {
                    Gizmos.color = Color.white;
                    Gizmos.DrawRay(
                        t_Start.position,
                        (t_End.position - t_Start.position).normalized * ((m_EndIsPos) ? GetDistance_Transform() : m_Distance));
                }
                Gizmos.DrawWireSphere(t_Start.position, m_Sphere / 2);
                break;
            case 3:
                //BoxCast
                Gizmos.color = Color.black;
                Gizmos.DrawWireSphere(
                    t_End.position,
                    m_Sphere / 2);
                if (cm_Eye.GetBoxCast_Vec_Check(
                    t_Start.position,
                    v3_Square,
                    t_End.position,
                    v3_Square_Rot,
                    (m_EndIsPos) ? GetDistance_Transform() : m_Distance,
                    l_Barrier))
                {
                    //If Hit
                    RaycastHit ray_Hit = cm_Eye.GetBoxCast_Vec_RaycastHit(
                        t_Start.position,
                        v3_Square,
                        t_End.position,
                        v3_Square_Rot,
                        (m_EndIsPos) ? GetDistance_Transform() : m_Distance,
                        l_Barrier);
                    Gizmos.color = Color.white;
                    Gizmos.DrawLine(
                        t_Start.position,
                        t_Start.position + (t_End.position - t_Start.position).normalized * ray_Hit.distance);
                    Gizmos.DrawWireCube(
                        t_Start.position + (t_End.position - t_Start.position).normalized * ray_Hit.distance,
                        v3_Square);
                }
                else
                if (cm_Eye.GetBoxCast_Vec_Check(
                    t_Start.position,
                    v3_Square,
                    t_End.position,
                    v3_Square_Rot,
                    (m_EndIsPos) ? GetDistance_Transform() : m_Distance,
                    l_Tarket))
                {
                    //If Hit
                    RaycastHit ray_Hit = cm_Eye.GetBoxCast_Vec_RaycastHit(
                        t_Start.position,
                        v3_Square,
                        t_End.position,
                        v3_Square_Rot,
                        (m_EndIsPos) ? GetDistance_Transform() : m_Distance,
                        l_Tarket);
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(
                        t_Start.position,
                        t_Start.position + (t_End.position - t_Start.position).normalized * ray_Hit.distance);
                    Gizmos.DrawWireCube(
                        t_Start.position + (t_End.position - t_Start.position).normalized * ray_Hit.distance,
                        v3_Square);
                }
                else
                {
                    Gizmos.color = Color.white;
                    Gizmos.DrawLine(
                        t_Start.position,
                        t_Start.position + (t_End.position - t_Start.position).normalized * ((m_EndIsPos) ? GetDistance_Transform() : m_Distance));
                    Gizmos.DrawWireCube(
                        t_Start.position + (t_End.position - t_Start.position).normalized * m_Distance,
                        v3_Square);
                }
                Gizmos.DrawWireCube(
                    t_Start.position,
                    v3_Square);
                break;
            case 4:
                //SphereCast
                Gizmos.color = Color.black;
                Gizmos.DrawWireSphere(
                    t_End.position,
                    m_Sphere / 2);
                if (cm_Eye.GetSphereCast_Vec_Check(
                    t_Start.position,
                    m_Sphere,
                    t_End.position,
                    (m_EndIsPos) ? GetDistance_Transform() : m_Distance,
                    l_Barrier))
                {
                    //If Hit
                    RaycastHit ray_Hit = cm_Eye.GetSphereCast_Vec_RaycastHit(
                        t_Start.position,
                        m_Sphere,
                        t_End.position,
                        (m_EndIsPos) ? GetDistance_Transform() : m_Distance,
                        l_Barrier);
                    Gizmos.color = Color.white;
                    Gizmos.DrawLine(
                        t_Start.position,
                        t_Start.position + (t_End.position - t_Start.position).normalized * ray_Hit.distance);
                    Gizmos.DrawWireSphere(
                        t_Start.position + (t_End.position - t_Start.position).normalized * ray_Hit.distance,
                        m_Sphere / 2);
                }
                else
                if (cm_Eye.GetSphereCast_Vec_Check(
                    t_Start.position,
                    m_Sphere,
                    t_End.position,
                    (m_EndIsPos) ? GetDistance_Transform() : m_Distance,
                    l_Tarket))
                {
                    //If Hit
                    RaycastHit ray_Hit = cm_Eye.GetSphereCast_Vec_RaycastHit(
                        t_Start.position,
                        m_Sphere,
                        t_End.position,
                        (m_EndIsPos) ? GetDistance_Transform() : m_Distance,
                        l_Tarket);
                    Gizmos.color = Color.red;
                    Gizmos.DrawLine(
                        t_Start.position,
                        t_Start.position + (t_End.position - t_Start.position).normalized * ray_Hit.distance);
                    Gizmos.DrawWireSphere(
                        t_Start.position + (t_End.position - t_Start.position).normalized * ray_Hit.distance,
                        m_Sphere / 2);
                }
                else
                {
                    Gizmos.color = Color.white;
                    Gizmos.DrawLine(
                        t_Start.position,
                        t_Start.position + (t_End.position - t_Start.position).normalized * ((m_EndIsPos) ? GetDistance_Transform() : m_Distance));
                    Gizmos.DrawWireSphere(
                        t_Start.position + (t_End.position - t_Start.position).normalized * ((m_EndIsPos) ? GetDistance_Transform() : m_Distance),
                        m_Sphere / 2);
                }
                Gizmos.DrawWireSphere(
                    t_Start.position,
                    m_Sphere / 2);
                break;
        }
    }
}
