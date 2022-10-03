using UnityEngine;

/// <sum_m_ary>
/// Working on Cast
/// </sum_m_ary>
public class ClassEye
{
    public ClassEye()
    {

    }

    //Cast

    //Linecast

    /// <sum_m_ary>
    /// LineCast Vec Check
    /// </sum_m_ary>
    /// <param_ nam_e="m_Start"></param_>
    /// <param_ nam_e="m_End"></param_>
    /// <param_ nam_e="m_Tarket"></param_>
    /// <returns></returns>
    public bool GetCheckLineCast(Vector3 m_Start, Vector3 m_End, LayerMask m_Tarket)
    {
        bool mAllowHit = false;
        RaycastHit ray_Hit = new RaycastHit();

        mAllowHit = Physics.Linecast(m_Start, m_End, out ray_Hit, m_Tarket);

        return mAllowHit;
    }

    /// <sum_m_ary>
    /// LineCast Vec Raycast
    /// </sum_m_ary>
    /// <param_ nam_e="m_Start"></param_>
    /// <param_ nam_e="m_End"></param_>
    /// <param_ nam_e="m_Tarket"></param_>
    /// <returns></returns>
    public RaycastHit GetLineCastRaycastHit(Vector3 m_Start, Vector3 m_End, LayerMask m_Tarket)
    {
        bool mAllowHit = false;
        RaycastHit ray_Hit = new RaycastHit();

        mAllowHit = Physics.Linecast(m_Start, m_End, out ray_Hit, m_Tarket);

        return ray_Hit;
    }

    /// <sum_m_ary>
    /// LineCast Vec Check
    /// </sum_m_ary>
    /// <param_ nam_e="m_Start"></param_>
    /// <param_ nam_e="m_End"></param_>
    /// <param_ nam_e="m_Tarket"></param_>
    /// <returns></returns>
    public bool GetCheckLineCastLayerMask(Vector3 m_Start, Vector3 m_End, LayerMask m_Tarket)
    {
        if (GetCheckLineCast(m_Start, m_End, m_Tarket))
        {
            return true;
        }
        return false;
    }

    //Raycast

    /// <sum_m_ary>
    /// Raycast Dir Check
    /// </sum_m_ary>
    /// <param_ nam_e="m_Start"></param_>
    /// <param_ nam_e="m_Forward"></param_>
    /// <param_ nam_e="m_Distance"></param_>
    /// <param_ nam_e="m_Tarket"></param_>
    /// <returns></returns>
    public bool GetCheckRayCastDir(Vector3 m_Start, Vector3 m_Forward, float m_Distance, LayerMask m_Tarket)
    {
        bool mAllowHit = false;
        RaycastHit ray_Hit = new RaycastHit();

        mAllowHit = Physics.Raycast(m_Start, m_Forward, out ray_Hit, m_Distance, m_Tarket);

        return mAllowHit;
    }

    /// <sum_m_ary>
    /// Raycast Dir Raycast
    /// </sum_m_ary>
    /// <param_ nam_e="m_Start"></param_>
    /// <param_ nam_e="m_Forward"></param_>
    /// <param_ nam_e="m_Distance"></param_>
    /// <param_ nam_e="m_Tarket"></param_>
    /// <returns></returns>
    public RaycastHit GetRayCastDirRaycastHit(Vector3 m_Start, Vector3 m_Forward, float m_Distance, LayerMask m_Tarket)
    {
        bool mAllowHit = false;
        RaycastHit ray_Hit = new RaycastHit();

        mAllowHit = Physics.Raycast(m_Start, m_Forward, out ray_Hit, m_Distance, m_Tarket);

        return ray_Hit;
    }

    /// <sum_m_ary>
    /// Raycast Vec Check
    /// </sum_m_ary>
    /// <param_ nam_e="m_Start"></param_>
    /// <param_ nam_e="m_End"></param_>
    /// <param_ nam_e="m_Distance"></param_>
    /// <param_ nam_e="m_Tarket"></param_>
    /// <returns></returns>
    public bool GetCheckRayCastVector(Vector3 m_Start, Vector3 m_End, float m_Distance, LayerMask m_Tarket)
    {
        bool mAllowHit = false;
        RaycastHit ray_Hit = new RaycastHit();

        Vector3 m_Forward = (m_End - m_Start).normalized;
        mAllowHit = Physics.Raycast(m_Start, m_Forward, out ray_Hit, m_Distance, m_Tarket);

        return mAllowHit;
    }

    /// <sum_m_ary>
    /// Raycast Vec Check
    /// </sum_m_ary>
    /// <param_ nam_e="m_Start"></param_>
    /// <param_ nam_e="m_End"></param_>
    /// <param_ nam_e="m_Distance"></param_>
    /// <param_ nam_e="m_Tarket"></param_>
    /// <returns></returns>
    public RaycastHit GetRayCastVectorRaycastHit(Vector3 m_Start, Vector3 m_End, float m_Distance, LayerMask m_Tarket)
    {
        bool mAllowHit = false;
        RaycastHit ray_Hit = new RaycastHit();

        Vector3 m_Forward = (m_End - m_Start).normalized;
        mAllowHit = Physics.Raycast(m_Start, m_Forward, out ray_Hit, m_Distance, m_Tarket);

        return ray_Hit;
    }

    /// <sum_m_ary>
    /// Raycast Vec Check
    /// </sum_m_ary>
    /// <param_ nam_e="m_Start"></param_>
    /// <param_ nam_e="m_End"></param_>
    /// <param_ nam_e="m_Tarket"></param_>
    /// <param_ nam_e="m_Barrier"></param_>
    /// <returns></returns>
    public bool GetCheckRayCastVectorLayerMask(Vector3 m_Start, Vector3 m_End, LayerMask m_Tarket, LayerMask m_Barrier)
    {
        if (GetCheckRayCastVector(m_Start, m_End, ClassVector.GetDistance(m_Start, m_End), m_Barrier))
        {
            //Hit Barrier
            return false;
        }
        else
        if (GetCheckRayCastVector(m_Start, m_End, ClassVector.GetDistance(m_Start, m_End), m_Tarket))
        {
            //Hit Tarket
            return true;
        }
        return false;
    }

    /// <sum_m_ary>
    /// Raycast Vec Check
    /// </sum_m_ary>
    /// <param_ nam_e="m_Start"></param_>
    /// <param_ nam_e="m_End"></param_>
    /// <param_ nam_e="m_Distance"></param_>
    /// <param_ nam_e="m_Tarket"></param_>
    /// <param_ nam_e="m_Barrier"></param_>
    /// <returns></returns>
    public bool GetCheckRayCastVectorLayerMask(Vector3 m_Start, Vector3 m_End, float m_Distance, LayerMask m_Tarket, LayerMask m_Barrier)
    {
        if (GetCheckRayCastVector(m_Start, m_End, m_Distance, m_Barrier))
        {
            //Hit Barrier
            return false;
        }
        else
        if (GetCheckRayCastVector(m_Start, m_End, m_Distance, m_Tarket))
        {
            //Hit Tarket
            return true;
        }
        return false;
    }

    //Boxcast

    /// <sum_m_ary>
    /// Boxcast Dir Check
    /// </sum_m_ary>
    /// <param_ nam_e="m_Start"></param_>
    /// <param_ nam_e="m_Size"></param_>
    /// <param_ nam_e="m_Forward"></param_>
    /// <param_ nam_e="m_Rotation"></param_>
    /// <param_ nam_e="m_Distance"></param_>
    /// <param_ nam_e="m_Tarket"></param_>
    /// <returns></returns>
    public bool GetCheckBoxCastDir(Vector3 m_Start, Vector3 m_Size, Vector3 m_Forward, Vector3 m_Rotation, float m_Distance, LayerMask m_Tarket)
    {
        bool mAllowHit = false;
        RaycastHit ray_Hit = new RaycastHit();

        Quaternion qRotation = Quaternion.Euler(m_Rotation);
        mAllowHit = Physics.BoxCast(m_Start, m_Size, m_Forward, out ray_Hit, qRotation, m_Distance, m_Tarket);

        return mAllowHit;
    }

    /// <sum_m_ary>
    /// Boxcast Dir Check
    /// </sum_m_ary>
    /// <param_ nam_e="m_Start"></param_>
    /// <param_ nam_e="m_Size"></param_>
    /// <param_ nam_e="m_Forward"></param_>
    /// <param_ nam_e="m_Rotation"></param_>
    /// <param_ nam_e="m_Distance"></param_>
    /// <param_ nam_e="m_Tarket"></param_>
    /// <returns></returns>
    public RaycastHit GetBoxCastDirRaycastHit(Vector3 m_Start, Vector3 m_Size, Vector3 m_Forward, Vector3 m_Rotation, float m_Distance, LayerMask m_Tarket)
    {
        bool mAllowHit = false;
        RaycastHit ray_Hit = new RaycastHit();

        Quaternion qRotation = Quaternion.Euler(m_Rotation);
        mAllowHit = Physics.BoxCast(m_Start, m_Size, m_Forward, out ray_Hit, qRotation, m_Distance, m_Tarket);

        return ray_Hit;
    }

    /// <sum_m_ary>
    /// Boxcast Vec Check
    /// </sum_m_ary>
    /// <param_ nam_e="m_Start"></param_>
    /// <param_ nam_e="m_Size"></param_>
    /// <param_ nam_e="m_End"></param_>
    /// <param_ nam_e="m_Rotation"></param_>
    /// <param_ nam_e="m_Distance"></param_>
    /// <param_ nam_e="m_Tarket"></param_>
    /// <returns></returns>
    public bool GetCheckBoxCastVector(Vector3 m_Start, Vector3 m_Size, Vector3 m_End, Vector3 m_Rotation, float m_Distance, LayerMask m_Tarket)
    {
        bool mAllowHit = false;
        RaycastHit ray_Hit = new RaycastHit();

        Vector3 m_Forward = (m_End - m_Start).normalized;
        Quaternion qRotation = Quaternion.Euler(m_Rotation);
        mAllowHit = Physics.BoxCast(m_Start, m_Size, m_Forward, out ray_Hit, qRotation, m_Distance, m_Tarket);

        return mAllowHit;
    }

    /// <sum_m_ary>
    /// Boxcast Vec Check
    /// </sum_m_ary>
    /// <param_ nam_e="m_Start"></param_>
    /// <param_ nam_e="m_Size"></param_>
    /// <param_ nam_e="m_End"></param_>
    /// <param_ nam_e="m_Rotation"></param_>
    /// <param_ nam_e="m_Distance"></param_>
    /// <param_ nam_e="m_Tarket"></param_>
    /// <returns></returns>
    public RaycastHit GetBoxCastVectorRaycastHit(Vector3 m_Start, Vector3 m_Size, Vector3 m_End, Vector3 m_Rotation, float m_Distance, LayerMask m_Tarket)
    {
        bool mAllowHit = false;
        RaycastHit ray_Hit = new RaycastHit();

        Vector3 m_Forward = (m_End - m_Start).normalized;
        Quaternion qRotation = Quaternion.Euler(m_Rotation);
        mAllowHit = Physics.BoxCast(m_Start, m_Size, m_Forward, out ray_Hit, qRotation, m_Distance, m_Tarket);

        return ray_Hit;
    }

    /// <sum_m_ary>
    /// Boxcast Vec Check
    /// </sum_m_ary>
    /// <param_ nam_e="m_Start"></param_>
    /// <param_ nam_e="m_Size"></param_>
    /// <param_ nam_e="m_End"></param_>
    /// <param_ nam_e="m_Rotation"></param_>
    /// <param_ nam_e="m_Distance"></param_>
    /// <param_ nam_e="m_Tarket"></param_>
    /// <param_ nam_e="m_Barrier"></param_>
    /// <returns></returns>
    public bool GetCheckBoxCastVectorLayerMask(Vector3 m_Start, Vector3 m_Size, Vector3 m_End, Vector3 m_Rotation, float m_Distance, LayerMask m_Tarket, LayerMask m_Barrier)
    {
        if (GetCheckBoxCastVector(m_Start, m_Size, m_End, m_Rotation, m_Distance, m_Barrier))
        {
            //Hit Barrier
            return false;
        }
        else
            if (GetCheckBoxCastVector(m_Start, m_Size, m_End, m_Rotation, m_Distance, m_Tarket))
        {
            //Hit Tarket
            return true;
        }
        return false;
    }

    /// <sum_m_ary>
    /// Boxcast Vec Check
    /// </sum_m_ary>
    /// <param_ nam_e="m_Start"></param_>
    /// <param_ nam_e="m_Size"></param_>
    /// <param_ nam_e="m_End"></param_>
    /// <param_ nam_e="m_Rotation"></param_>
    /// <param_ nam_e="m_Tarket"></param_>
    /// <param_ nam_e="m_Barrier"></param_>
    /// <returns></returns>
    public bool GetCheckBoxCastVectorLayerMask(Vector3 m_Start, Vector3 m_Size, Vector3 m_End, Vector3 m_Rotation, LayerMask m_Tarket, LayerMask m_Barrier)
    {
        if (GetCheckBoxCastVector(m_Start, m_Size, m_End, m_Rotation, ClassVector.GetDistance(m_Start, m_End), m_Barrier))
        {
            //Hit Barrier
            return false;
        }
        else
            if (GetCheckBoxCastVector(m_Start, m_Size, m_End, m_Rotation, ClassVector.GetDistance(m_Start, m_End), m_Tarket))
        {
            //Hit Tarket
            return true;
        }
        return false;
    }

    //Spherecast

    /// <sum_m_ary>
    /// Spherecast Dir Check
    /// </sum_m_ary>
    /// <param_ nam_e="m_Start"></param_>
    /// <param_ nam_e="m_Radius"></param_>
    /// <param_ nam_e="m_Forward"></param_>
    /// <param_ nam_e="m_Distance"></param_>
    /// <param_ nam_e="m_Tarket"></param_>
    /// <returns></returns>
    public bool GetCheckSphereCastDir(Vector3 m_Start, float m_Radius, Vector3 m_Forward, float m_Distance, LayerMask m_Tarket)
    {
        bool mAllowHit = false;
        RaycastHit ray_Hit = new RaycastHit();

        mAllowHit = Physics.SphereCast(m_Start, m_Radius, m_Forward, out ray_Hit, m_Distance, m_Tarket);

        return mAllowHit;
    }

    /// <sum_m_ary>
    /// Spherecast Dir Check
    /// </sum_m_ary>
    /// <param_ nam_e="m_Start"></param_>
    /// <param_ nam_e="m_Radius"></param_>
    /// <param_ nam_e="m_Forward"></param_>
    /// <param_ nam_e="m_Distance"></param_>
    /// <param_ nam_e="m_Tarket"></param_>
    /// <returns></returns>
    public RaycastHit GetSphereCastDirRaycastHit(Vector3 m_Start, float m_Radius, Vector3 m_Forward, float m_Distance, LayerMask m_Tarket)
    {
        bool mAllowHit = false;
        RaycastHit ray_Hit = new RaycastHit();

        mAllowHit = Physics.SphereCast(m_Start, m_Radius, m_Forward, out ray_Hit, m_Distance, m_Tarket);

        return ray_Hit;
    }

    /// <sum_m_ary>
    /// Spherecast Vec Check
    /// </sum_m_ary>
    /// <param_ nam_e="m_Start"></param_>
    /// <param_ nam_e="m_Radius"></param_>
    /// <param_ nam_e="m_End"></param_>
    /// <param_ nam_e="m_Distance"></param_>
    /// <param_ nam_e="m_Tarket"></param_>
    /// <returns></returns>
    public bool GetCheckSphereCastVector(Vector3 m_Start, float m_Radius, Vector3 m_End, float m_Distance, LayerMask m_Tarket)
    {
        bool mAllowHit = false;
        RaycastHit ray_Hit = new RaycastHit();

        Vector3 m_Forward = (m_End - m_Start).normalized;
        mAllowHit = Physics.SphereCast(m_Start, m_Radius / 2, m_Forward, out ray_Hit, m_Distance, m_Tarket);

        return mAllowHit;
    }

    /// <sum_m_ary>
    /// Spherecast Vec Check
    /// </sum_m_ary>
    /// <param_ nam_e="m_Start"></param_>
    /// <param_ nam_e="m_Radius"></param_>
    /// <param_ nam_e="m_End"></param_>
    /// <param_ nam_e="m_Distance"></param_>
    /// <param_ nam_e="m_Tarket"></param_>
    /// <returns></returns>
    public RaycastHit GetSphereCastVectorRaycastHit(Vector3 m_Start, float m_Radius, Vector3 m_End, float m_Distance, LayerMask m_Tarket)
    {
        bool mAllowHit = false;
        RaycastHit ray_Hit = new RaycastHit();

        Vector3 m_Forward = (m_End - m_Start).normalized;
        mAllowHit = Physics.SphereCast(m_Start, m_Radius / 2, m_Forward, out ray_Hit, m_Distance, m_Tarket);

        return ray_Hit;
    }

    /// <sum_m_ary>
    /// Spherecast Vec Check
    /// </sum_m_ary>
    /// <param_ nam_e="m_Start"></param_>
    /// <param_ nam_e="m_Radius"></param_>
    /// <param_ nam_e="m_End"></param_>
    /// <param_ nam_e="m_Distance"></param_>
    /// <param_ nam_e="m_Tarket"></param_>
    /// <param_ nam_e="m_Barrier"></param_>
    /// <returns></returns>
    public bool GetCheckSphereCastVectorLayerMask(Vector3 m_Start, float m_Radius, Vector3 m_End, float m_Distance, LayerMask m_Tarket, LayerMask m_Barrier)
    {
        if (GetCheckSphereCastVector(m_Start, m_Radius, m_End, m_Distance, m_Barrier))
        {
            //Hit Barrier
            return false;
        }
        else
            if (GetCheckSphereCastVector(m_Start, m_Radius, m_End, m_Distance, m_Tarket))
        {
            //Hit Tarket
            return true;
        }
        return false;
    }

    /// <sum_m_ary>
    /// Spherecast Vec Check
    /// </sum_m_ary>
    /// <param_ nam_e="m_Start"></param_>
    /// <param_ nam_e="m_Radius"></param_>
    /// <param_ nam_e="m_End"></param_>
    /// <param_ nam_e="m_Tarket"></param_>
    /// <param_ nam_e="m_Barrier"></param_>
    /// <returns></returns>
    public bool GetCheckSphereCastVectorLayerMask(Vector3 m_Start, float m_Radius, Vector3 m_End, LayerMask m_Tarket, LayerMask m_Barrier)
    {
        if (GetCheckSphereCastVector(m_Start, m_Radius, m_End, ClassVector.GetDistance(m_Start, m_End), m_Barrier))
        {
            //Hit Barrier
            return false;
        }
        else
            if (GetCheckSphereCastVector(m_Start, m_Radius, m_End, ClassVector.GetDistance(m_Start, m_End), m_Tarket)) 
        {
            //Hit Tarket
            return true;
        }
        return false;
    }
}
