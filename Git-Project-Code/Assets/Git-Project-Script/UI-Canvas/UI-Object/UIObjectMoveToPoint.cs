using UnityEngine;

public class UIObjectMoveToPoint : MonoBehaviour
{
    [Header("Parent Canvas")]

    [Tooltip("Parent Canvas")]
    [SerializeField]
    private Canvas c_ParentCanvas;

    [Header("This RecTransform")]

    [SerializeField]
    private RectTransform com_MoveTo;

    [SerializeField]
    private float m_MoveTime = 1f;

    [SerializeField]
    private Vector2 v2_MoveTo_Offset = new Vector2();

    [Tooltip("After Finish m_ove, set Depth to Zero")]
    [SerializeField]
    private bool mAllowDepthAutoZero = false;

    [Header("Move To Point Debug")]

    [SerializeField]
    private Vector3 m_MoveTo = new Vector3();

    //[SerializeField]
    private float m_Distance = 0f;

    //[SerializeField]
    private float m_Speed = 0f;

    [SerializeField]
    private bool mAllowMoveDone = true;

    private RectTransform com_Transform;

    private void Awake()
    {
        if (c_ParentCanvas == null)
        {
            c_ParentCanvas = GetComponentInParent<Canvas>();
        }

        com_Transform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (com_MoveTo != null)
        {
            SetUI_MoveTo(com_MoveTo.anchoredPosition3D);
        }

        Vector2 v2_MoveTo = new Vector2(m_MoveTo.x, m_MoveTo.y);

        m_Distance = Vector2.Distance(com_Transform.anchoredPosition, v2_MoveTo + v2_MoveTo_Offset);

        if (mAllowMoveDone && m_Distance > 1f)
        {
            m_Speed = m_Distance / m_MoveTime;

            mAllowMoveDone = false;
        }
        else
        if (m_Distance <= 1f)
        {
            mAllowMoveDone = true;

            if (mAllowDepthAutoZero)
            {
                com_Transform.anchoredPosition3D = new Vector3(
                    com_Transform.anchoredPosition3D.x,
                    com_Transform.anchoredPosition3D.y,
                    0);
            }
        }

        if (m_Distance > 0f)
        {
            com_Transform.anchoredPosition = Vector2.MoveTowards(
                com_Transform.anchoredPosition,
                v2_MoveTo + v2_MoveTo_Offset,
                m_Speed * Time.deltaTime);

            com_Transform.anchoredPosition3D = new Vector3(
                com_Transform.anchoredPosition3D.x,
                com_Transform.anchoredPosition3D.y,
                m_MoveTo.z);
        }
    }

    public void SetUI_MoveTo(RectTransform com_MoveTo)
    {
        this.com_MoveTo = com_MoveTo;
    }

    public void SetUI_MoveTo(Vector3 m_MoveTo)
    {
        this.m_MoveTo = m_MoveTo;

        com_Transform.anchoredPosition3D = new Vector3(
            com_Transform.anchoredPosition3D.x,
            com_Transform.anchoredPosition3D.y,
            m_MoveTo.z);
    }

    public void SetUI_MoveToTime(float m_MoveTime)
    {
        this.m_MoveTime = m_MoveTime;
    }

    public void SetUI_MoveTo_Offset(Vector2 v2_MoveTo_Offset)
    {
        this.v2_MoveTo_Offset = v2_MoveTo_Offset;
    }

    public bool GetCheckUI_MoveToDone()
    {
        return mAllowMoveDone;
    }
}
