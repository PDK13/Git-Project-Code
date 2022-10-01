using UnityEngine;

public class UIObjectMoveToPoint : MonoBehaviour
{
    [Header("Parent Canvas")]

    [Tooltip("Parent Canvas")]
    [SerializeField]
    private Canvas c_ParentCanvas;

    [Header("This RecTransform")]

    [SerializeField]
    private RectTransform rec_MoveTo;

    [SerializeField]
    private float f_Move_Time = 1f;

    [SerializeField]
    private Vector2 v2_MoveTo_Offset = new Vector2();

    [Tooltip("After Finish Move, set Depth to Zero")]
    [SerializeField]
    private bool m_DepthAutoZero = false;

    [Header("Move To Point Debug")]

    [SerializeField]
    private Vector3 v3_MoveTo = new Vector3();

    //[SerializeField]
    private float f_Distance = 0f;

    //[SerializeField]
    private float f_Speed = 0f;

    [SerializeField]
    private bool m_Move_Done = true;

    private RectTransform rec_Transform;

    private void Awake()
    {
        if (c_ParentCanvas == null)
        {
            c_ParentCanvas = GetComponentInParent<Canvas>();
        }

        rec_Transform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (rec_MoveTo != null)
        {
            Set_UI_MoveTo(rec_MoveTo.anchoredPosition3D);
        }

        Vector2 v2_MoveTo = new Vector2(v3_MoveTo.x, v3_MoveTo.y);

        f_Distance = Vector2.Distance(rec_Transform.anchoredPosition, v2_MoveTo + v2_MoveTo_Offset);

        if (m_Move_Done && f_Distance > 1f)
        {
            f_Speed = f_Distance / f_Move_Time;

            m_Move_Done = false;
        }
        else
        if (f_Distance <= 1f)
        {
            m_Move_Done = true;

            if (m_DepthAutoZero)
            {
                rec_Transform.anchoredPosition3D = new Vector3(
                    rec_Transform.anchoredPosition3D.x,
                    rec_Transform.anchoredPosition3D.y,
                    0);
            }
        }

        if (f_Distance > 0f)
        {
            rec_Transform.anchoredPosition = Vector2.MoveTowards(
                rec_Transform.anchoredPosition,
                v2_MoveTo + v2_MoveTo_Offset,
                f_Speed * Time.deltaTime);

            rec_Transform.anchoredPosition3D = new Vector3(
                rec_Transform.anchoredPosition3D.x,
                rec_Transform.anchoredPosition3D.y,
                v3_MoveTo.z);
        }
    }

    public void Set_UI_MoveTo(RectTransform rec_MoveTo)
    {
        this.rec_MoveTo = rec_MoveTo;
    }

    public void Set_UI_MoveTo(Vector3 v3_MoveTo)
    {
        this.v3_MoveTo = v3_MoveTo;

        rec_Transform.anchoredPosition3D = new Vector3(
            rec_Transform.anchoredPosition3D.x,
            rec_Transform.anchoredPosition3D.y,
            v3_MoveTo.z);
    }

    public void Set_UI_MoveTo_Time(float f_Move_Time)
    {
        this.f_Move_Time = f_Move_Time;
    }

    public void Set_UI_MoveTo_Offset(Vector2 v2_MoveTo_Offset)
    {
        this.v2_MoveTo_Offset = v2_MoveTo_Offset;
    }

    public bool GetUI_MoveTo_Done()
    {
        return m_Move_Done;
    }
}
