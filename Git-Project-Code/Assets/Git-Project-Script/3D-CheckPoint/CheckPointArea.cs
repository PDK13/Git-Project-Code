using UnityEngine;

public class CheckPointArea : MonoBehaviour
{
    [Header("Check Point(s)")]

    //Next Check-Point (If got) to attach to Tarket GameObject with CheckPoint3DGet.cs
    [SerializeField] private Transform m_PointNext;

    //Size of Check-Point Cube
    [SerializeField] private Vector3 m_Pointsize = new Vector3(1f, 1f, 1f);

    [Header("Check Tarket(s)")]

    //Layer-Mask to check Tarket GameObject with Cast
    [SerializeField] private LayerMask m_PointLayer;

    private void Update()
    {
        SetCheckPoint();
    }

    private void SetCheckPoint()
    {
        Collider[] m_Collide = Physics.OverlapBox(transform.position, m_Pointsize / 2f, GitVector.GetRotationEulerToQuaternion(0, 0, 0), m_PointLayer);

        for (int i = 0; i < m_Collide.Length; i++)
        {
            if (m_Collide[i].gameObject.GetComponent<CheckPointGet>() != null)
            {
                m_Collide[i].gameObject.GetComponent<CheckPointGet>().SetCheckPointNext(m_PointNext);
            }
        }
    }

    public Transform GetPointNext()
    {
        return m_PointNext;
    }

    public Vector3 GetPointSize()
    {
        return m_Pointsize;
    }

    public LayerMask GetPointLayer()
    {
        return m_PointLayer;
    }

    private void OnDrawGizmos()
    {
        if (Physics.OverlapBox(transform.position, m_Pointsize / 2f, GitVector.GetRotationEulerToQuaternion(0, 0, 0), m_PointLayer).Length > 0)
        {
            Gizmos.color = Color.red;
        }
        else
        {
            Gizmos.color = Color.green;
        }

        Gizmos.DrawWireCube(transform.position, m_Pointsize);

        Gizmos.color = Color.white;

        if (m_PointNext != null)
        {
            Gizmos.DrawLine(transform.position, m_PointNext.position);
        }
    }
}
