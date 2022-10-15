using System.Collections.Generic;
using UnityEngine;

public class RendererGeometryPoint : MonoBehaviour
{
    public bool m_Debug = true;

    public float m_Duration = 2;

    [Range(0, 360)]
    public float m_DegStart = 0;

    [Range(3, 36)]
    public int m_PointCount = 3;

    #region Renderer

    public List<Vector2> ActiveCreatePoint(float m_Duration, float m_DegStart, int m_PointCount)
    {
        List<Vector2> m_Point = new List<Vector2>();

        float m_RadSpace = (360 / m_PointCount) * (Mathf.PI / 180);
        float m_RadStart = (m_DegStart) * (Mathf.PI / 180);
        float m_RadCur = m_RadStart;

        Vector2 vStartPoint = new Vector2(Mathf.Cos(m_RadStart) * m_Duration, Mathf.Sin(m_RadStart) * m_Duration);
        Vector2 v_OldPoint = vStartPoint;

        m_Point.Add(vStartPoint);

        for (int i = 1; i < m_PointCount; i++)
        {
            m_RadCur += m_RadSpace;
            Vector2 v_NewPoint = new Vector2(Mathf.Cos(m_RadCur) * m_Duration, Mathf.Sin(m_RadCur) * m_Duration);

            m_Point.Add(v_NewPoint);

            v_OldPoint = v_NewPoint;
        }

        return m_Point;
    }

    public List<Vector2> Receive_PointList()
    {
        return ActiveCreatePoint(m_Duration, m_DegStart, m_PointCount);
    }

    #endregion

    private void OnDrawGizmosSelected()
    {
        //Thể hiện hình vẽ m_ẫu trên chính GameObject. Và đây cũng là gợi ý cho cách sử dụng danh sách điểm.
        //Đường vẽ m_àu "Vàng" là giữa điểm Kết thúc và Bắt đầu

        if (!m_Debug)
        {
            return;
        }

        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere((Vector2)transform.position, m_Duration);

        //Sử dụng danh sách điểm từ hàm phía dưới
        List<Vector2> m_PointDebug = ActiveCreatePoint(m_Duration, m_DegStart, m_PointCount);

        for (int i = 1; i < m_PointDebug.Count; i++)
        {
            if (i % 2 == 0)
            {
                Gizmos.color = Color.white;
            }
            else
            {
                Gizmos.color = Color.black;
            }

            Gizmos.DrawLine(
                (Vector2)transform.position + m_PointDebug[i - 1],
                (Vector2)transform.position + m_PointDebug[i]);
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(
            (Vector2)transform.position + m_PointDebug[m_PointDebug.Count - 1],
            (Vector2)transform.position + m_PointDebug[0]);
    }
}