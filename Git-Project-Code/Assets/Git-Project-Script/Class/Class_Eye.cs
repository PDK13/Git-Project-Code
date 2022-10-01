using UnityEngine;

/// <summary>
/// Working on Cast
/// </summary>
public class Class_Eye
{
    public Class_Eye()
    {

    }

    //Cast

    //Linecast

    /// <summary>
    /// LineCast Vec Check
    /// </summary>
    /// <param name="v3_Start"></param>
    /// <param name="v3_End"></param>
    /// <param name="l_Tarket"></param>
    /// <returns></returns>
    public bool GetLineCast_Check(Vector3 v3_Start, Vector3 v3_End, LayerMask l_Tarket)
    {
        bool m_Hit = false;
        RaycastHit ray_Hit = new RaycastHit();

        m_Hit = Physics.Linecast(v3_Start, v3_End, out ray_Hit, l_Tarket);

        return m_Hit;
    }

    /// <summary>
    /// LineCast Vec Raycast
    /// </summary>
    /// <param name="v3_Start"></param>
    /// <param name="v3_End"></param>
    /// <param name="l_Tarket"></param>
    /// <returns></returns>
    public RaycastHit GetLineCast_RaycastHit(Vector3 v3_Start, Vector3 v3_End, LayerMask l_Tarket)
    {
        bool m_Hit = false;
        RaycastHit ray_Hit = new RaycastHit();

        m_Hit = Physics.Linecast(v3_Start, v3_End, out ray_Hit, l_Tarket);

        return ray_Hit;
    }

    /// <summary>
    /// LineCast Vec Check
    /// </summary>
    /// <param name="v3_Start"></param>
    /// <param name="v3_End"></param>
    /// <param name="l_Tarket"></param>
    /// <returns></returns>
    public bool GetLineCast_Check_LayerMask(Vector3 v3_Start, Vector3 v3_End, LayerMask l_Tarket)
    {
        if (GetLineCast_Check(v3_Start, v3_End, l_Tarket))
        {
            return true;
        }
        return false;
    }

    //Raycast

    /// <summary>
    /// Raycast Dir Check
    /// </summary>
    /// <param name="v3_Start"></param>
    /// <param name="v3_Forward"></param>
    /// <param name="f_Distance"></param>
    /// <param name="l_Tarket"></param>
    /// <returns></returns>
    public bool GetRayCast_Dir_Check(Vector3 v3_Start, Vector3 v3_Forward, float f_Distance, LayerMask l_Tarket)
    {
        bool m_Hit = false;
        RaycastHit ray_Hit = new RaycastHit();

        m_Hit = Physics.Raycast(v3_Start, v3_Forward, out ray_Hit, f_Distance, l_Tarket);

        return m_Hit;
    }

    /// <summary>
    /// Raycast Dir Raycast
    /// </summary>
    /// <param name="v3_Start"></param>
    /// <param name="v3_Forward"></param>
    /// <param name="f_Distance"></param>
    /// <param name="l_Tarket"></param>
    /// <returns></returns>
    public RaycastHit GetRayCast_Dir_RaycastHit(Vector3 v3_Start, Vector3 v3_Forward, float f_Distance, LayerMask l_Tarket)
    {
        bool m_Hit = false;
        RaycastHit ray_Hit = new RaycastHit();

        m_Hit = Physics.Raycast(v3_Start, v3_Forward, out ray_Hit, f_Distance, l_Tarket);

        return ray_Hit;
    }

    /// <summary>
    /// Raycast Vec Check
    /// </summary>
    /// <param name="v3_Start"></param>
    /// <param name="v3_End"></param>
    /// <param name="f_Distance"></param>
    /// <param name="l_Tarket"></param>
    /// <returns></returns>
    public bool GetRayCast_Vec_Check(Vector3 v3_Start, Vector3 v3_End, float f_Distance, LayerMask l_Tarket)
    {
        bool m_Hit = false;
        RaycastHit ray_Hit = new RaycastHit();

        Vector3 v3_Forward = (v3_End - v3_Start).normalized;
        m_Hit = Physics.Raycast(v3_Start, v3_Forward, out ray_Hit, f_Distance, l_Tarket);

        return m_Hit;
    }

    /// <summary>
    /// Raycast Vec Check
    /// </summary>
    /// <param name="v3_Start"></param>
    /// <param name="v3_End"></param>
    /// <param name="f_Distance"></param>
    /// <param name="l_Tarket"></param>
    /// <returns></returns>
    public RaycastHit GetRayCast_Vec_RaycastHit(Vector3 v3_Start, Vector3 v3_End, float f_Distance, LayerMask l_Tarket)
    {
        bool m_Hit = false;
        RaycastHit ray_Hit = new RaycastHit();

        Vector3 v3_Forward = (v3_End - v3_Start).normalized;
        m_Hit = Physics.Raycast(v3_Start, v3_Forward, out ray_Hit, f_Distance, l_Tarket);

        return ray_Hit;
    }

    /// <summary>
    /// Raycast Vec Check
    /// </summary>
    /// <param name="v3_Start"></param>
    /// <param name="v3_End"></param>
    /// <param name="l_Tarket"></param>
    /// <param name="l_Barrier"></param>
    /// <returns></returns>
    public bool GetRayCast_Vec_Check_LayerMask(Vector3 v3_Start, Vector3 v3_End, LayerMask l_Tarket, LayerMask l_Barrier)
    {
        if (GetRayCast_Vec_Check(v3_Start, v3_End, ClassVector.GetDistance(v3_Start, v3_End), l_Barrier))
        {
            //Hit Barrier
            return false;
        }
        else
        if (GetRayCast_Vec_Check(v3_Start, v3_End, ClassVector.GetDistance(v3_Start, v3_End), l_Tarket))
        {
            //Hit Tarket
            return true;
        }
        return false;
    }

    /// <summary>
    /// Raycast Vec Check
    /// </summary>
    /// <param name="v3_Start"></param>
    /// <param name="v3_End"></param>
    /// <param name="f_Distance"></param>
    /// <param name="l_Tarket"></param>
    /// <param name="l_Barrier"></param>
    /// <returns></returns>
    public bool GetRayCast_Vec_Check_LayerMask(Vector3 v3_Start, Vector3 v3_End, float f_Distance, LayerMask l_Tarket, LayerMask l_Barrier)
    {
        if (GetRayCast_Vec_Check(v3_Start, v3_End, f_Distance, l_Barrier))
        {
            //Hit Barrier
            return false;
        }
        else
        if (GetRayCast_Vec_Check(v3_Start, v3_End, f_Distance, l_Tarket))
        {
            //Hit Tarket
            return true;
        }
        return false;
    }

    //Boxcast

    /// <summary>
    /// Boxcast Dir Check
    /// </summary>
    /// <param name="v3_Start"></param>
    /// <param name="v3_Size"></param>
    /// <param name="v3_Forward"></param>
    /// <param name="v3_Rotation"></param>
    /// <param name="f_Distance"></param>
    /// <param name="l_Tarket"></param>
    /// <returns></returns>
    public bool GetBoxCast_Dir_Check(Vector3 v3_Start, Vector3 v3_Size, Vector3 v3_Forward, Vector3 v3_Rotation, float f_Distance, LayerMask l_Tarket)
    {
        bool m_Hit = false;
        RaycastHit ray_Hit = new RaycastHit();

        Quaternion q_Rotation = Quaternion.Euler(v3_Rotation);
        m_Hit = Physics.BoxCast(v3_Start, v3_Size, v3_Forward, out ray_Hit, q_Rotation, f_Distance, l_Tarket);

        return m_Hit;
    }

    /// <summary>
    /// Boxcast Dir Check
    /// </summary>
    /// <param name="v3_Start"></param>
    /// <param name="v3_Size"></param>
    /// <param name="v3_Forward"></param>
    /// <param name="v3_Rotation"></param>
    /// <param name="f_Distance"></param>
    /// <param name="l_Tarket"></param>
    /// <returns></returns>
    public RaycastHit GetBoxCast_Dir_RaycastHit(Vector3 v3_Start, Vector3 v3_Size, Vector3 v3_Forward, Vector3 v3_Rotation, float f_Distance, LayerMask l_Tarket)
    {
        bool m_Hit = false;
        RaycastHit ray_Hit = new RaycastHit();

        Quaternion q_Rotation = Quaternion.Euler(v3_Rotation);
        m_Hit = Physics.BoxCast(v3_Start, v3_Size, v3_Forward, out ray_Hit, q_Rotation, f_Distance, l_Tarket);

        return ray_Hit;
    }

    /// <summary>
    /// Boxcast Vec Check
    /// </summary>
    /// <param name="v3_Start"></param>
    /// <param name="v3_Size"></param>
    /// <param name="v3_End"></param>
    /// <param name="v3_Rotation"></param>
    /// <param name="f_Distance"></param>
    /// <param name="l_Tarket"></param>
    /// <returns></returns>
    public bool GetBoxCast_Vec_Check(Vector3 v3_Start, Vector3 v3_Size, Vector3 v3_End, Vector3 v3_Rotation, float f_Distance, LayerMask l_Tarket)
    {
        bool m_Hit = false;
        RaycastHit ray_Hit = new RaycastHit();

        Vector3 v3_Forward = (v3_End - v3_Start).normalized;
        Quaternion q_Rotation = Quaternion.Euler(v3_Rotation);
        m_Hit = Physics.BoxCast(v3_Start, v3_Size, v3_Forward, out ray_Hit, q_Rotation, f_Distance, l_Tarket);

        return m_Hit;
    }

    /// <summary>
    /// Boxcast Vec Check
    /// </summary>
    /// <param name="v3_Start"></param>
    /// <param name="v3_Size"></param>
    /// <param name="v3_End"></param>
    /// <param name="v3_Rotation"></param>
    /// <param name="f_Distance"></param>
    /// <param name="l_Tarket"></param>
    /// <returns></returns>
    public RaycastHit GetBoxCast_Vec_RaycastHit(Vector3 v3_Start, Vector3 v3_Size, Vector3 v3_End, Vector3 v3_Rotation, float f_Distance, LayerMask l_Tarket)
    {
        bool m_Hit = false;
        RaycastHit ray_Hit = new RaycastHit();

        Vector3 v3_Forward = (v3_End - v3_Start).normalized;
        Quaternion q_Rotation = Quaternion.Euler(v3_Rotation);
        m_Hit = Physics.BoxCast(v3_Start, v3_Size, v3_Forward, out ray_Hit, q_Rotation, f_Distance, l_Tarket);

        return ray_Hit;
    }

    /// <summary>
    /// Boxcast Vec Check
    /// </summary>
    /// <param name="v3_Start"></param>
    /// <param name="v3_Size"></param>
    /// <param name="v3_End"></param>
    /// <param name="v3_Rotation"></param>
    /// <param name="f_Distance"></param>
    /// <param name="l_Tarket"></param>
    /// <param name="l_Barrier"></param>
    /// <returns></returns>
    public bool GetBoxCast_Vec_Check_LayerMask(Vector3 v3_Start, Vector3 v3_Size, Vector3 v3_End, Vector3 v3_Rotation, float f_Distance, LayerMask l_Tarket, LayerMask l_Barrier)
    {
        if (GetBoxCast_Vec_Check(v3_Start, v3_Size, v3_End, v3_Rotation, f_Distance, l_Barrier))
        {
            //Hit Barrier
            return false;
        }
        else
            if (GetBoxCast_Vec_Check(v3_Start, v3_Size, v3_End, v3_Rotation, f_Distance, l_Tarket))
        {
            //Hit Tarket
            return true;
        }
        return false;
    }

    /// <summary>
    /// Boxcast Vec Check
    /// </summary>
    /// <param name="v3_Start"></param>
    /// <param name="v3_Size"></param>
    /// <param name="v3_End"></param>
    /// <param name="v3_Rotation"></param>
    /// <param name="l_Tarket"></param>
    /// <param name="l_Barrier"></param>
    /// <returns></returns>
    public bool GetBoxCast_Vec_Check_LayerMask(Vector3 v3_Start, Vector3 v3_Size, Vector3 v3_End, Vector3 v3_Rotation, LayerMask l_Tarket, LayerMask l_Barrier)
    {
        if (GetBoxCast_Vec_Check(v3_Start, v3_Size, v3_End, v3_Rotation, ClassVector.GetDistance(v3_Start, v3_End), l_Barrier))
        {
            //Hit Barrier
            return false;
        }
        else
            if (GetBoxCast_Vec_Check(v3_Start, v3_Size, v3_End, v3_Rotation, ClassVector.GetDistance(v3_Start, v3_End), l_Tarket))
        {
            //Hit Tarket
            return true;
        }
        return false;
    }

    //Spherecast

    /// <summary>
    /// Spherecast Dir Check
    /// </summary>
    /// <param name="v3_Start"></param>
    /// <param name="f_Radius"></param>
    /// <param name="v3_Forward"></param>
    /// <param name="f_Distance"></param>
    /// <param name="l_Tarket"></param>
    /// <returns></returns>
    public bool GetSphereCast_Dir_Check(Vector3 v3_Start, float f_Radius, Vector3 v3_Forward, float f_Distance, LayerMask l_Tarket)
    {
        bool m_Hit = false;
        RaycastHit ray_Hit = new RaycastHit();

        m_Hit = Physics.SphereCast(v3_Start, f_Radius, v3_Forward, out ray_Hit, f_Distance, l_Tarket);

        return m_Hit;
    }

    /// <summary>
    /// Spherecast Dir Check
    /// </summary>
    /// <param name="v3_Start"></param>
    /// <param name="f_Radius"></param>
    /// <param name="v3_Forward"></param>
    /// <param name="f_Distance"></param>
    /// <param name="l_Tarket"></param>
    /// <returns></returns>
    public RaycastHit GetSphereCast_Dir_RaycastHit(Vector3 v3_Start, float f_Radius, Vector3 v3_Forward, float f_Distance, LayerMask l_Tarket)
    {
        bool m_Hit = false;
        RaycastHit ray_Hit = new RaycastHit();

        m_Hit = Physics.SphereCast(v3_Start, f_Radius, v3_Forward, out ray_Hit, f_Distance, l_Tarket);

        return ray_Hit;
    }

    /// <summary>
    /// Spherecast Vec Check
    /// </summary>
    /// <param name="v3_Start"></param>
    /// <param name="f_Radius"></param>
    /// <param name="v3_End"></param>
    /// <param name="f_Distance"></param>
    /// <param name="l_Tarket"></param>
    /// <returns></returns>
    public bool GetSphereCast_Vec_Check(Vector3 v3_Start, float f_Radius, Vector3 v3_End, float f_Distance, LayerMask l_Tarket)
    {
        bool m_Hit = false;
        RaycastHit ray_Hit = new RaycastHit();

        Vector3 v3_Forward = (v3_End - v3_Start).normalized;
        m_Hit = Physics.SphereCast(v3_Start, f_Radius / 2, v3_Forward, out ray_Hit, f_Distance, l_Tarket);

        return m_Hit;
    }

    /// <summary>
    /// Spherecast Vec Check
    /// </summary>
    /// <param name="v3_Start"></param>
    /// <param name="f_Radius"></param>
    /// <param name="v3_End"></param>
    /// <param name="f_Distance"></param>
    /// <param name="l_Tarket"></param>
    /// <returns></returns>
    public RaycastHit GetSphereCast_Vec_RaycastHit(Vector3 v3_Start, float f_Radius, Vector3 v3_End, float f_Distance, LayerMask l_Tarket)
    {
        bool m_Hit = false;
        RaycastHit ray_Hit = new RaycastHit();

        Vector3 v3_Forward = (v3_End - v3_Start).normalized;
        m_Hit = Physics.SphereCast(v3_Start, f_Radius / 2, v3_Forward, out ray_Hit, f_Distance, l_Tarket);

        return ray_Hit;
    }

    /// <summary>
    /// Spherecast Vec Check
    /// </summary>
    /// <param name="v3_Start"></param>
    /// <param name="f_Radius"></param>
    /// <param name="v3_End"></param>
    /// <param name="f_Distance"></param>
    /// <param name="l_Tarket"></param>
    /// <param name="l_Barrier"></param>
    /// <returns></returns>
    public bool GetSphereCast_Vec_Check_LayerMask(Vector3 v3_Start, float f_Radius, Vector3 v3_End, float f_Distance, LayerMask l_Tarket, LayerMask l_Barrier)
    {
        if (GetSphereCast_Vec_Check(v3_Start, f_Radius, v3_End, f_Distance, l_Barrier))
        {
            //Hit Barrier
            return false;
        }
        else
            if (GetSphereCast_Vec_Check(v3_Start, f_Radius, v3_End, f_Distance, l_Tarket))
        {
            //Hit Tarket
            return true;
        }
        return false;
    }

    /// <summary>
    /// Spherecast Vec Check
    /// </summary>
    /// <param name="v3_Start"></param>
    /// <param name="f_Radius"></param>
    /// <param name="v3_End"></param>
    /// <param name="l_Tarket"></param>
    /// <param name="l_Barrier"></param>
    /// <returns></returns>
    public bool GetSphereCast_Vec_Check_LayerMask(Vector3 v3_Start, float f_Radius, Vector3 v3_End, LayerMask l_Tarket, LayerMask l_Barrier)
    {
        if (GetSphereCast_Vec_Check(v3_Start, f_Radius, v3_End, ClassVector.GetDistance(v3_Start, v3_End), l_Barrier))
        {
            //Hit Barrier
            return false;
        }
        else
            if (GetSphereCast_Vec_Check(v3_Start, f_Radius, v3_End, ClassVector.GetDistance(v3_Start, v3_End), l_Tarket))
        {
            //Hit Tarket
            return true;
        }
        return false;
    }
}
