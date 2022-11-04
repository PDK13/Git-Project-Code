using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoopController : MonoBehaviour
{
    [Header("Camera")]

    [SerializeField] private BackgroundLoopType m_CameraLoop = BackgroundLoopType.Horizontal;

    [SerializeField] private Transform m_Camera;

    private float m_CameraX;

    [Header("Background")]

    [SerializeField] private BackgroundLoopMain m_BackgroundMain;

    private float m_BackgroundBoundX;

    private float m_BackgroundLocalX;

    private Vector2 m_SizeUnitBackground = new Vector2();

    [Header("Background Layer")]

    [SerializeField] private List<BackgroundLoopLayer> m_BackgroundLayer;

    private void Start()
    {
        if (m_Camera == null)
        {
            m_Camera = Camera.main.transform;
        }

        m_SizeUnitBackground = GitResolution.GetSpriteSizeUnit(m_BackgroundMain.Background.sprite);

        switch (m_CameraLoop)
        {
            case BackgroundLoopType.Horizontal:
                m_BackgroundBoundX = m_BackgroundMain.Background.GetComponent<SpriteRenderer>().bounds.size.x;
                m_BackgroundLocalX = m_BackgroundMain.Background.transform.localScale.x;
                m_CameraX = m_Camera.position.x;
                for (int i = 0; i < m_BackgroundLayer.Count; i++)
                {
                    m_BackgroundLayer[i].PosPrimaryX = GetCameraX();
                }
                break;
            case BackgroundLoopType.Vertical:
                m_BackgroundBoundX = m_BackgroundMain.Background.GetComponent<SpriteRenderer>().bounds.size.y;
                m_BackgroundLocalX = m_BackgroundMain.Background.transform.localScale.y;
                m_CameraX = m_Camera.position.y;
                for (int i = 0; i < m_BackgroundLayer.Count; i++)
                {
                    m_BackgroundLayer[i].PosPrimaryX = GetCameraX();
                }
                break;
        }

        if (m_BackgroundMain.FixedCamera)
        {
            m_BackgroundMain.Background.drawMode = SpriteDrawMode.Sliced;

            m_BackgroundMain.Background.size = GitResolution.GetSizeUnitScaled(
                m_SizeUnitBackground,
                GitResolution.GetCameraSizeUnit(),
                GitSizeUnitScaleType.Span);
        }
    }

    private void LateUpdate()
    {
        if (m_BackgroundMain.FixedCamera)
        {
            m_BackgroundMain.Background.size = GitResolution.GetSizeUnitScaled(
                m_SizeUnitBackground,
                GitResolution.GetCameraSizeUnit(),
                GitSizeUnitScaleType.Span);
        }

        switch (m_CameraLoop)
        {
            case BackgroundLoopType.Horizontal:
                for (int i = 0; i < m_BackgroundLayer.Count; i++)
                {
                    float m_Temp = GetCameraX() * (1 - m_BackgroundLayer[i].LayerSpeedX);

                    float m_Distance = GetCameraX() * m_BackgroundLayer[i].LayerSpeedX;

                    m_BackgroundLayer[i].Transform.transform.position = new Vector2(m_BackgroundLayer[i].PosPrimaryX + m_Distance + m_CameraX, GetCameraY(m_BackgroundLayer[i]));

                    if (m_Temp > m_BackgroundLayer[i].PosPrimaryX + m_BackgroundBoundX * m_BackgroundLocalX)
                    {
                        m_BackgroundLayer[i].PosPrimaryX += (m_BackgroundBoundX * m_BackgroundLocalX);
                    }
                    else
                    if (m_Temp < m_BackgroundLayer[i].PosPrimaryX - m_BackgroundBoundX * m_BackgroundLocalX)
                    {
                        m_BackgroundLayer[i].PosPrimaryX += (-m_BackgroundBoundX * m_BackgroundLocalX);
                    }
                }
                break;
            case BackgroundLoopType.Vertical:
                for (int i = 0; i < m_BackgroundLayer.Count; i++)
                {
                    float m_Temp = GetCameraX() * (1 - m_BackgroundLayer[i].LayerSpeedX);

                    float m_Distance = GetCameraX() * m_BackgroundLayer[i].LayerSpeedX;

                    m_BackgroundLayer[i].Transform.transform.position = new Vector2(GetCameraY(m_BackgroundLayer[i]), m_BackgroundLayer[i].PosPrimaryX + m_Distance + m_CameraX);

                    if (m_Temp > m_BackgroundLayer[i].PosPrimaryX + m_BackgroundBoundX * m_BackgroundLocalX)
                    {
                        m_BackgroundLayer[i].PosPrimaryX += (m_BackgroundBoundX * m_BackgroundLocalX);
                    }
                    else
                    if (m_Temp < m_BackgroundLayer[i].PosPrimaryX - m_BackgroundBoundX * m_BackgroundLocalX)
                    {
                        m_BackgroundLayer[i].PosPrimaryX += (-m_BackgroundBoundX * m_BackgroundLocalX);
                    }
                }
                break;
        }
    }

    private float GetCameraX()
    {
        switch (m_CameraLoop)
        {
            case BackgroundLoopType.Horizontal:
                return m_Camera.position.x - m_CameraX;
            case BackgroundLoopType.Vertical:
                return m_Camera.position.y - m_CameraX;
        }

        return 0;
    }

    private float GetCameraY(BackgroundLoopLayer m_Layer)
    {
        switch (m_CameraLoop)
        {
            case BackgroundLoopType.Horizontal:
                return (m_Layer.FollowY) ? m_Camera.position.y : m_Layer.Transform.transform.position.y;
            case BackgroundLoopType.Vertical:
                return (m_Layer.FollowY) ? m_Camera.position.x : m_Layer.Transform.transform.position.x;
        }
        return 0;
    }
}

[System.Serializable]
public class BackgroundLoopLayer
{
    [SerializeField] private SpriteRenderer m_Layer;

    [SerializeField] [Range(0, 1)] private float m_SpeedX = 0.5f;

    private float m_PosPrimaryX;

    [SerializeField] private bool m_FollowY = true;

    public SpriteRenderer Layer
    {
        get => m_Layer;
    }

    public Transform Transform
    {
        get => m_Layer.transform;
    }

    public float LayerSpeedX
    {
        get => m_SpeedX;
        set
        {
            m_SpeedX = value;
        }
    }

    public float PosPrimaryX
    {
        get => m_PosPrimaryX;
        set
        {
            m_PosPrimaryX = value;
        }
    }

    public bool FollowY
    {
        get => m_FollowY;
        set
        {
            m_FollowY = value;
        }
    }
}

public enum BackgroundLoopType { Horizontal, Vertical }

[System.Serializable]
public class BackgroundLoopMain
{
    [SerializeField] private SpriteRenderer m_Background;

    [SerializeField] private bool m_FixedCamera = true;

    public SpriteRenderer Background
    {
        get => m_Background;
    }

    public bool FixedCamera
    {
        get => m_FixedCamera;
    }
}