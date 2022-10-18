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

    [SerializeField] private ScrollRect com_ScrollRect;

    [SerializeField] private RectTransform com_Content;

    [SerializeField] private ContentSizeFitter com_ContentSizeFitter;

    [SerializeField] private GridLayoutGroup com_GridLayoutGroup;

    private void Awake()
    {
        if (this.GetComponent<ScrollRect>() == null)
        {
            Debug.LogWarningFormat("{0}: Script not attach to Scroll View GameObject?!", name);
        }
        else
        {
            //Scroll Rect

            com_ScrollRect = this.GetComponent<ScrollRect>();
        }

        if (this.transform.Find("Viewport/Content") == null)
        {
            Debug.LogWarningFormat("{0}: Something went wrong to get child Viewport/Content of Scroll View!", name);

            return;
        }
        else
        {
            com_Content = this.transform.Find("Viewport/Content").GetComponent<RectTransform>();

            //Grid Layout Group

            if (com_Content.gameObject.GetComponent<ContentSizeFitter>() == null)
            {
                com_ContentSizeFitter = com_Content.gameObject.AddComponent<ContentSizeFitter>();
            }

            if (com_ContentSizeFitter == null)
            {
                com_ContentSizeFitter = com_Content.gameObject.GetComponent<ContentSizeFitter>();
            }

            //Content Size Fitter

            if (com_Content.gameObject.GetComponent<GridLayoutGroup>() == null)
            {
                com_GridLayoutGroup = com_Content.gameObject.AddComponent<GridLayoutGroup>();
            }

            if (com_GridLayoutGroup == null)
            {
                com_GridLayoutGroup = com_Content.gameObject.GetComponent<GridLayoutGroup>();
            }
        }

        SetScrollViewFix();
    }

    private void Start()
    {
        if (Application.IsPlaying(this.gameObject))
        {
            SetCheckTestingOff();
        }
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
                if (com_ScrollRect != null)
                {
                    com_Content.anchoredPosition = new Vector2(0, com_Content.anchoredPosition.y);
                }
                //Scroll Rect
                if (com_ContentSizeFitter != null)
                {
                    com_ScrollRect.vertical = true;
                    com_ScrollRect.horizontal = false;
                }
                //Grid Layout Group
                if (com_GridLayoutGroup != null)
                {
                    com_GridLayoutGroup.startAxis = GridLayoutGroup.Axis.Horizontal;
                    com_GridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
                    com_GridLayoutGroup.constraintCount = 1;
                }
                //Content Size Fitter
                if (com_GridLayoutGroup == null)
                {
                    com_ContentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
                    com_ContentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
                }
                break;
            case ScrollViewType.Horizontal:
                if (com_ScrollRect != null)
                {
                    com_Content.anchoredPosition = new Vector2(com_Content.anchoredPosition.x, 0);
                }
                //Scroll Rect
                if (com_ContentSizeFitter != null)
                {
                    com_ScrollRect.vertical = false;
                    com_ScrollRect.horizontal = true;
                }
                //Grid Layout Group
                if (com_GridLayoutGroup != null)
                {
                    com_GridLayoutGroup.startAxis = GridLayoutGroup.Axis.Vertical;
                    com_GridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedRowCount;
                    com_GridLayoutGroup.constraintCount = 1;
                }
                //Content Size Fitter
                if (com_GridLayoutGroup != null)
                {
                    com_ContentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
                    com_ContentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
                }
                break;
        }

        if (com_GridLayoutGroup != null)
        {
            com_GridLayoutGroup.cellSize = m_ItemSize;
            com_GridLayoutGroup.spacing = m_ItemSpacing;
        }  
    }

    public void SetContent(float m_ItemInContent)
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
        return com_Content;
    }

    public RectTransform GetContentItem(int m_ItemInContent)
    {
        if (m_ItemInContent < 0)
        {
            return com_Content.GetChild(0).GetComponent<RectTransform>();
        }
        else
        if (m_ItemInContent > com_Content.childCount - 1)
        {
            return com_Content.GetChild(com_Content.childCount - 1).GetComponent<RectTransform>();
        }
        else
        {
            return com_Content.GetChild(m_ItemInContent).GetComponent<RectTransform>();
        }
    }

    public void SetCheckTestingOff()
    {
        m_CheckTesting = false;
    }
}

public enum ScrollViewType
{
    Vertical,
    Horizontal
}