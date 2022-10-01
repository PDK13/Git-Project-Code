using UnityEngine;

public class CheckPointGet : MonoBehaviour
{
    [Header("Check Point(s)")]

    //First Check-Point head to go
    [SerializeField] private Transform m_CheckPointFirst;

    //Offset Direction in Angle (Deg)
    [SerializeField] private float m_CheckPointOffsetDirection = 90f;

    private Transform m_CheckPointNext;

    private void Start()
    {
        m_CheckPointNext = m_CheckPointFirst;
    }

    public void SetCheckPointNext(Transform m_CheckPointNext)
    {
        this.m_CheckPointNext = m_CheckPointNext;
    }

    public float GetCheckPointNextOffsetRotate()
    {
        return ClassVector.GetDegOnRotationXZ(transform, m_CheckPointNext.transform);
    }

    public bool GetCheckPointNextRonDirection(float m_CheckPointOffsetAngleHigher)
    {
        return GetCheckPointNextOffsetRotate() >= m_CheckPointOffsetAngleHigher;
    }

    public bool GetCheckPointNextRightDirection(float m_CheckPointOffsetAngleLower)
    {
        return GetCheckPointNextOffsetRotate() <= m_CheckPointOffsetAngleLower;
    }
}
