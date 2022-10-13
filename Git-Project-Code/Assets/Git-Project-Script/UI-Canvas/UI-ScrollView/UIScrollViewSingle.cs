using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class UIScrollViewSingle : MonoBehaviour
{
    public int m_Index = 0;

    [Header("Setting")]

    [SerializeField] private ScrollViewType m_ScrollViewType;

    [SerializeField] private Vector2 m_ItemSize = new Vector2(100f, 100f);

    [SerializeField] private Vector2 m_ItemSpacing = new Vector2(5f, 5f);

    [Header("Component")]

    [SerializeField] private ScrollRect com_ScrollRect;

    [SerializeField] private RectTransform com_Content;

    [SerializeField] private ContentSizeFitter com_ContentSizeFitter;

    [SerializeField] private GridLayoutGroup com_GridLayoutGroup;

    private void Awake()
    {
        com_ScrollRect = this.GetComponent<ScrollRect>();
        com_ContentSizeFitter = this.transform.Find("Viewport/Content").gameObject.AddComponent<ContentSizeFitter>();
        com_GridLayoutGroup = this.transform.Find("Viewport/Content").gameObject.AddComponent<GridLayoutGroup>();
        com_Content = this.transform.Find("Viewport/Content").GetComponent<RectTransform>();
        SetScrollViewFix();
    }

#if UNITY_EDITOR

    private void Update()
    {
        SetScrollViewFix();

        SetContent(m_Index);
    }

#endif

    private void SetScrollViewFix()
    {
        if (com_ScrollRect == null)
            return;

        if (com_ContentSizeFitter == null)
            return;

        if (com_GridLayoutGroup == null)
            return;

        switch (m_ScrollViewType)
        {
            case ScrollViewType.Vertical:
                //Content
                com_Content.anchoredPosition = new Vector2(0, com_Content.anchoredPosition.y);
                //Scroll Rect
                com_ScrollRect.vertical = true;
                com_ScrollRect.horizontal = false;
                //Grid Layout Group
                com_GridLayoutGroup.startAxis = GridLayoutGroup.Axis.Horizontal;
                com_GridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
                com_GridLayoutGroup.constraintCount = 1;
                //Content Size Fitter
                com_ContentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
                com_ContentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
                break;
            case ScrollViewType.Horizontal:
                //Content
                com_Content.anchoredPosition = new Vector2(com_Content.anchoredPosition.x, 0);
                //Scroll Rect
                com_ScrollRect.vertical = false;
                com_ScrollRect.horizontal = true;
                //Grid Layout Group
                com_GridLayoutGroup.startAxis = GridLayoutGroup.Axis.Vertical;
                com_GridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedRowCount;
                com_GridLayoutGroup.constraintCount = 1;
                //Content Size Fitter
                com_ContentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
                com_ContentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
                break;
        }

        com_GridLayoutGroup.cellSize = m_ItemSize;
        com_GridLayoutGroup.spacing = m_ItemSpacing;
    }

    public void SetContent(int m_ItemInContent)
    {
        if (m_ItemInContent < 0)
        {
            com_Content.anchoredPosition = GetContent(0);
        }
        else
        if (m_ItemInContent > com_Content.childCount - 1)
        {
            com_Content.anchoredPosition = GetContent(com_Content.childCount - 1);
        }
        else
        {
            com_Content.anchoredPosition = GetContent(m_ItemInContent);
        }
    }

    public Vector2 GetContent(int m_ItemInContent)
    {
        switch (m_ScrollViewType)
        {
            case ScrollViewType.Vertical:
                return (+1) * m_ItemInContent * new Vector2(0, m_ItemSize.y + m_ItemSpacing.y);
            case ScrollViewType.Horizontal:
                return (-1) * m_ItemInContent * new Vector2(m_ItemSize.x + m_ItemSpacing.x, 0);
        }
        return new Vector2();
    }

    public RectTransform GetContent()
    {
        return com_Content;
    }
}

public enum ScrollViewType
{
    Vertical,
    Horizontal
}