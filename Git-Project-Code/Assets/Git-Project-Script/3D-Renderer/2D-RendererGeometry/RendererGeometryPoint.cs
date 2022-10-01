using System.Collections.Generic;
using UnityEngine;

//Script này dùng để nhận toạ độ các điểm để vẽ trên cung tròn, ứng dụng công thức lượng giác trong việc xác định điểm
public class RendererGeometryPoint : MonoBehaviour
{
    public bool m_Debug = true;

    public float m_Duration = 2;

    [Range(0, 360)]
    public float m_DegStart = 0;

    //Số lượng điểm quyết định hình dạng của hình vẽ (Tam giác đều, Tứ giác đều, Lục giác đều,...)
    //Khi số lượng điểm đạt tối đa, sẽ vẽ được Hình tròn (Không được vượt quá 36 điểm để tránh vị lỗi)
    [Range(3, 36)]
    public int m_PointCount = 3;

    #region Renderer

    //Hàm này khi được gọi sẽ trả về danh sách các điểm để vẽ trên cung tròn với tâm O có toạ độ gốc là O(0;0)
    //Khi vẽ các điểm này với tâm có góc toạ độ không là O(0;0), cần cộng cho chúng 1 Vector tâm A(x;y)
    public List<Vector2> Active_CreatePoint(float m_Duration, float m_DegStart, int m_PointCount)
    {
        List<Vector2> l_Point = new List<Vector2>();

        float m_RadSpace = (360 / m_PointCount) * (Mathf.PI / 180);
        float m_RadStart = (m_DegStart) * (Mathf.PI / 180);
        float m_RadCur = m_RadStart;

        Vector2 v_StartPoint = new Vector2(Mathf.Cos(m_RadStart) * m_Duration, Mathf.Sin(m_RadStart) * m_Duration);
        Vector2 v_OldPoint = v_StartPoint;

        l_Point.Add(v_StartPoint);

        for (int i = 1; i < m_PointCount; i++)
        {
            m_RadCur += m_RadSpace;
            Vector2 v_NewPoint = new Vector2(Mathf.Cos(m_RadCur) * m_Duration, Mathf.Sin(m_RadCur) * m_Duration);

            l_Point.Add(v_NewPoint);

            v_OldPoint = v_NewPoint;
        }

        return l_Point;
    }

    //Hàm này trả nhanh về danh sách các điểm dựa theo thông số hiện có
    public List<Vector2> Receive_PointList()
    {
        return Active_CreatePoint(m_Duration, m_DegStart, m_PointCount);
    }

    #endregion

    private void OnDrawGizmosSelected()
    {
        //Thể hiện hình vẽ mẫu trên chính GameObject. Và đây cũng là gợi ý cho cách sử dụng danh sách điểm.
        //Đường vẽ màu "Vàng" là giữa điểm Kết thúc và Bắt đầu

        if (!m_Debug)
        {
            return;
        }

        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere((Vector2)transform.position, m_Duration);

        //Sử dụng danh sách điểm từ hàm phía dưới
        List<Vector2> l_PointDebug = Active_CreatePoint(m_Duration, m_DegStart, m_PointCount);

        for (int i = 1; i < l_PointDebug.Count; i++)
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
                (Vector2)transform.position + l_PointDebug[i - 1],
                (Vector2)transform.position + l_PointDebug[i]);
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(
            (Vector2)transform.position + l_PointDebug[l_PointDebug.Count - 1],
            (Vector2)transform.position + l_PointDebug[0]);
    }
}