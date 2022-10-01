using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(RendererGeometryPoint))]

//Script dùng để điều khiển việc vẽ trên Renderer
public class RendererGeometry : MonoBehaviour
{
    private RendererGeometryPoint cs_Point;
    private LineRenderer l_LineRenderer;

    public float f_LineWidth = 0.1f;

    private void Start()
    {
        cs_Point = GetComponent<RendererGeometryPoint>();
        l_LineRenderer = GetComponent<LineRenderer>();
    }

    [System.Obsolete]
    private void Update()
    {
        Active_LineRenderer();
    }

    [System.Obsolete]
    private void Active_LineRenderer()
    {
        l_LineRenderer.SetWidth(f_LineWidth, f_LineWidth);

        int i_PointCount = cs_Point.Receive_PointList().Count;

        l_LineRenderer.positionCount = i_PointCount + 1;

        Vector2 vVector2;
        Vector3 vVector3;

        for (int i = 0; i < i_PointCount; i++)
        {
            vVector2 = cs_Point.Receive_PointList()[i];
            vVector3 = new Vector3(vVector2.x, vVector2.y, 0) + transform.position;
            l_LineRenderer.SetPosition(i, vVector3);
        }

        vVector2 = cs_Point.Receive_PointList()[0];
        vVector3 = new Vector3(vVector2.x, vVector2.y, 0) + transform.position;
        l_LineRenderer.SetPosition(i_PointCount, vVector3);
    }
}
