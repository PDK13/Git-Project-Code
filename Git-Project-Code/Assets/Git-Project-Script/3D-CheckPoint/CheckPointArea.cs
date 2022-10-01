using UnityEngine;

public class CheckPointArea : MonoBehaviour
{
    [Header("Check Point(s)")]

    //Next Check-Point (If got) to attach to Tarket GameObject with CheckPoint3DGet.cs
    [SerializeField] private Transform m_CheckPointNext;

    //Size of Check-Point Cube
    [SerializeField] private Vector3 m_CheckPointSize = new Vector3(1f, 1f, 1f);

    [Header("Check Tarket(s)")]

    //Layer-Mask to check Tarket GameObject with Cast
    [SerializeField] private LayerMask m_CheckPointLayer;

    private void Update()
    {
        SetCheckPoint();
    }

    private void SetCheckPoint()
    {
        Collider[] m_Collide = Physics.OverlapBox(transform.position, m_CheckPointSize / 2f, Class_Vector.Get_Rot_EulerToQuaternion(0, 0, 0), m_CheckPointLayer);

        for (int i = 0; i < m_Collide.Length; i++)
        {
            if (m_Collide[i].gameObject.GetComponent<CheckPointGet>() != null)
            {
                m_Collide[i].gameObject.GetComponent<CheckPointGet>().SetCheckPointNext(m_CheckPointNext);
            }
        }
    }

    public Transform GetCheckPointNext()
    {
        return m_CheckPointNext;
    }

    public Vector3 GetCheckPointSize()
    {
        return m_CheckPointSize;
    }

    public LayerMask GetCheckPointLayer()
    {
        return m_CheckPointLayer;
    }

    private void OnDrawGizmos()
    {
        if (Physics.OverlapBox(transform.position, m_CheckPointSize / 2f, Class_Vector.Get_Rot_EulerToQuaternion(0, 0, 0), m_CheckPointLayer).Length > 0)
        {
            Gizmos.color = Color.red;
        }
        else
        {
            Gizmos.color = Color.green;
        }

        Gizmos.DrawWireCube(transform.position, m_CheckPointSize);

        Gizmos.color = Color.white;

        if (m_CheckPointNext != null)
        {
            Gizmos.DrawLine(transform.position, m_CheckPointNext.position);
        }
    }
}
