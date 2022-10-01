using UnityEngine;

public class CheckPointGet : MonoBehaviour
{
    [Header("Check Point(s)")]

    [SerializeField] private Transform m_CheckPointFirst;

    [SerializeField] private float m_DegCheck = 90;

    [Header("Debug")]

    [SerializeField] private Transform m_CheckPointNext;

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
        return Class_Vector.Get_DirToDeg_XZ_MainToCheck(transform, m_CheckPointNext.transform);
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
