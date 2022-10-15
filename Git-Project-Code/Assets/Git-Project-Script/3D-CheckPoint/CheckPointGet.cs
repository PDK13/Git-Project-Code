using UnityEngine;

public class CheckPointGet : MonoBehaviour
{
    [Header("Check Point(s)")]

    //First Check-Point head to go
    [SerializeField] private Transform m_CheckPointFirst;

    //Offset Direction in Angle (Deg)
    [SerializeField] private float m_CheckPointOffsetDirection = 90f;

    private Transform m_PointNext;

    private void Start()
    {
        m_PointNext = m_CheckPointFirst;
    }

    public void SetCheckPointNext(Transform m_PointNext)
    {
        this.m_PointNext = m_PointNext;
    }

    public float GetPointNextOffsetRotate()
    {
        return GitVector.GetDegOnRotationXZ(transform, m_PointNext.transform);
    }

    public void SetCheckPointOffsetDirection(float m_PointOffsetDirectionDeg)
    {
        this.m_CheckPointOffsetDirection = m_PointOffsetDirectionDeg;
    }


    public float GetPointOffsetDirection()
    {
        return m_CheckPointOffsetDirection;
    }

    public bool GetPointNextRonDirection(float m_PointOffsetAngleHigher)
    {
        return GetPointNextOffsetRotate() >= m_PointOffsetAngleHigher;
    }

    public bool GetPointNextRDirection(float m_PointOffsetAngleLower)
    {
        return GetPointNextOffsetRotate() <= m_PointOffsetAngleLower;
    }
}
