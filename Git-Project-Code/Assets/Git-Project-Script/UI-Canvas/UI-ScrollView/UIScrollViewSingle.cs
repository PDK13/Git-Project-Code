using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class UIScrollViewSingle : MonoBehaviour
{
    [Header("Setting")]

    [SerializeField] private ScrollViewType m_ScrollViewType;

    [SerializeField] private Vector2 m_ItemSize = new Vector2(100f, 100f);

    [SerializeField] private Vector2 m_ItemSpacing = new Vector2(5f, 5f);

    [Header("Testing")]

    [SerializeField] [Range(0, 10)] float m_IndexTesting = 0;

    [SerializeField] bool m_CheckTesting = false;

    [Header("Component")]

    [SerializeField] private ScrollRect m_ScrollRect;

    [SerializeField] private RectTransform m_Content;

    [SerializeField] private ContentSizeFitter m_ContentSizeFitter;

    [SerializeField] private GridLayoutGroup m_GridLayoutGroup;

    private void Awake()
    {
        if (this.GetComponent<ScrollRect>() == null)
        {
            Debug.LogWarningFormat("{0}: Script not attach to Scroll View GameObject?!", name);
        }
        else
        {
            //Scroll Rect

            m_ScrollRect = this.GetComponent<ScrollRect>();
        }

        if (this.transform.Find("Viewport/Content") == null)
        {
            Debug.LogWarningFormat("{0}: Something went wrong to get child Viewport/Content of Scroll View!", name);

            return;
        }
        else
        {
            m_Content = this.transform.Find("Viewport/Content").GetComponent<RectTransform>();

            //Grid Layout Group

            if (m_Content.gameObject.GetComponent<ContentSizeFitter>() == null)
            {
                m_ContentSizeFitter = m_Content.gameObject.AddComponent<ContentSizeFitter>();
            }

            if (m_ContentSizeFitter == null)
            {
                m_ContentSizeFitter = m_Content.gameObject.GetComponent<ContentSizeFitter>();
            }

            //Content Size Fitter

            if (m_Content.gameObject.GetComponent<GridLayoutGroup>() == null)
            {
                m_GridLayoutGroup = m_Content.gameObject.AddComponent<GridLayoutGroup>();
            }

            if (m_GridLayoutGroup == null)
            {
                m_GridLayoutGroup = m_Content.gameObject.GetComponent<GridLayoutGroup>();
            }
        }

        SetScrollViewFix();
    }

#if UNITY_EDITOR

    private void Update()
    {
        SetScrollViewFix();

        if (m_CheckTesting)
        {
            SetContent(m_IndexTesting);
        }
    }

#endif

    private void SetScrollViewFix()
    {
        switch (m_ScrollViewType)
        {
            case ScrollViewType.Vertical:
                //Content
                if (m_ScrollRect != null)
                {
                    m_Content.anchoredPosition = new Vector2(0, m_Content.anchoredPosition.y);
                }
                //Scroll Rect
                if (m_ContentSizeFitter != null)
                {
                    m_ScrollRect.vertical = true;
                    m_ScrollRect.horizontal = false;
                }
                //Grid Layout Group
                if (m_GridLayoutGroup != null)
                {
                    m_GridLayoutGroup.startAxis = GridLayoutGroup.Axis.Horizontal;
                    m_GridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
                    m_GridLayoutGroup.constraintCount = 1;
                }
                //Content Size Fitter
                if (m_GridLayoutGroup == null)
                {
                    m_ContentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
                    m_ContentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
                }
                break;
            case ScrollViewType.Horizontal:
                if (m_ScrollRect != null)
                {
                    m_Content.anchoredPosition = new Vector2(m_Content.anchoredPosition.x, 0);
                }
                //Scroll Rect
                if (m_ContentSizeFitter != null)
                {
                    m_ScrollRect.vertical = false;
                    m_ScrollRect.horizontal = true;
                }
                //Grid Layout Group
                if (m_GridLayoutGroup != null)
                {
                    m_GridLayoutGroup.startAxis = GridLayoutGroup.Axis.Vertical;
                    m_GridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedRowCount;
                    m_GridLayoutGroup.constraintCount = 1;
                }
                //Content Size Fitter
                if (m_GridLayoutGroup != null)
                {
                    m_ContentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
                    m_ContentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
                }
                break;
        }

        if (m_GridLayoutGroup != null)
        {
            m_GridLayoutGroup.cellSize = m_ItemSize;
            m_GridLayoutGroup.spacing = m_ItemSpacing;
        }  
    }

    public void SetContent(float m_ItemInContent)
    {
        if (m_ItemInContent < 0)
        {
            m_Content.anchoredPosition = GetContent(0);
        }
        else
        if (m_ItemInContent > m_Content.childCount - 1)
        {
            m_Content.anchoredPosition = GetContent(m_Content.childCount - 1);
        }
        else
        {
            m_Content.anchoredPosition = GetContent(m_ItemInContent);
        }
    }

    public Vector2 GetContent(float m_ItemInContent)
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
        return m_Content;
    }

    public RectTransform GetContentItem(int m_ItemInContent)
    {
        if (m_ItemInContent < 0)
        {
            return m_Content.GetChild(0).GetComponent<RectTransform>();
        }
        else
        if (m_ItemInContent > m_Content.childCount - 1)
        {
            return m_Content.GetChild(m_Content.childCount - 1).GetComponent<RectTransform>();
        }
        else
        {
            return m_Content.GetChild(m_ItemInContent).GetComponent<RectTransform>();
        }
    }
}

public enum ScrollViewType
{
    Vertical,
    Horizontal
}