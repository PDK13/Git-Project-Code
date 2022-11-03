using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoopController : MonoBehaviour
{
    [Header("Camera")]

    [SerializeField] private BackgroundLoopType m_CameraLoop = BackgroundLoopType.Horizontal;

    [SerializeField] private Transform m_Camera;

    private float m_CameraX;

    [Header("Background")]

    [SerializeField] private SpriteRenderer m_BackgroundMain;

    private float m_BackgroundBoundX;

    private float m_BackgroundLocalX;

    [Header("Background Layer")]

    [SerializeField] private List<BackgroundLoopLayer> m_BackgroundLayer;

    private void Start()
    {
        if (m_Camera == null)
        {
            m_Camera = Camera.main.transform;
        }

        switch (m_CameraLoop)
        {
            case BackgroundLoopType.Horizontal:
                m_BackgroundBoundX = m_BackgroundMain.GetComponent<SpriteRenderer>().bounds.size.x;
                //m_BackgroundBoundX = GitSprite.GetSizeUnit(m_BackgroundMain.GetComponent<SpriteRenderer>().sprite).x;
                //m_BackgroundBoundX = GitCamera.GetSizeUnit(m_Camera.GetComponent<Camera>()).x;
                m_BackgroundLocalX = m_BackgroundMain.transform.localScale.x;

                m_CameraX = m_Camera.position.x;

                for (int i = 0; i < m_BackgroundLayer.Count; i++)
                {
                    m_BackgroundLayer[i].SetLayerPosStartX(GetCameraX());
                }
                break;
            case BackgroundLoopType.Vertical:
                m_BackgroundBoundX = m_BackgroundMain.GetComponent<SpriteRenderer>().bounds.size.y;
                m_BackgroundLocalX = m_BackgroundMain.transform.localScale.y;

                m_CameraX = m_Camera.position.y;

                for (int i = 0; i < m_BackgroundLayer.Count; i++)
                {
                    m_BackgroundLayer[i].SetLayerPosStartX(GetCameraX());
                }
                break;
        }
    }

    private void Update()
    {
        switch (m_CameraLoop)
        {
            case BackgroundLoopType.Horizontal:
                for (int i = 0; i < m_BackgroundLayer.Count; i++)
                {
                    float m_Temp = GetCameraX() * (1 - m_BackgroundLayer[i].GetLayerSpeedX());

                    float m_Distance = GetCameraX() * m_BackgroundLayer[i].GetLayerSpeedX();

                    m_BackgroundLayer[i].GetTransform().transform.position = new Vector2(m_BackgroundLayer[i].GetLayerPosStartX() + m_Distance + m_CameraX, GetCameraY(m_BackgroundLayer[i]));

                    if (m_Temp > m_BackgroundLayer[i].GetLayerPosStartX() + m_BackgroundBoundX * m_BackgroundLocalX)
                    {
                        m_BackgroundLayer[i].SetLayerPosStartXChance(m_BackgroundBoundX * m_BackgroundLocalX);
                    }
                    else
                    if (m_Temp < m_BackgroundLayer[i].GetLayerPosStartX() - m_BackgroundBoundX * m_BackgroundLocalX)
                    {
                        m_BackgroundLayer[i].SetLayerPosStartXChance(-m_BackgroundBoundX * m_BackgroundLocalX);
                    }
                }
                break;
            case BackgroundLoopType.Vertical:
                for (int i = 0; i < m_BackgroundLayer.Count; i++)
                {
                    float m_Temp = GetCameraX() * (1 - m_BackgroundLayer[i].GetLayerSpeedX());

                    float m_Distance = GetCameraX() * m_BackgroundLayer[i].GetLayerSpeedX();

                    m_BackgroundLayer[i].GetTransform().transform.position = new Vector2(GetCameraY(m_BackgroundLayer[i]), m_BackgroundLayer[i].GetLayerPosStartX() + m_Distance + m_CameraX);

                    if (m_Temp > m_BackgroundLayer[i].GetLayerPosStartX() + m_BackgroundBoundX * m_BackgroundLocalX)
                    {
                        m_BackgroundLayer[i].SetLayerPosStartXChance(m_BackgroundBoundX * m_BackgroundLocalX);
                    }
                    else
                    if (m_Temp < m_BackgroundLayer[i].GetLayerPosStartX() - m_BackgroundBoundX * m_BackgroundLocalX)
                    {
                        m_BackgroundLayer[i].SetLayerPosStartXChance(-m_BackgroundBoundX * m_BackgroundLocalX);
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
                return (m_Layer.GetLayerFollowY()) ? m_Camera.position.y : m_Layer.GetTransform().transform.position.y;
            case BackgroundLoopType.Vertical:
                return (m_Layer.GetLayerFollowY()) ? m_Camera.position.x : m_Layer.GetTransform().transform.position.x;

        }

        return 0;
    }
}

[System.Serializable]
public class BackgroundLoopLayer
{
    [SerializeField] private GameObject m_Layer;

    [SerializeField] [Range(0, 1)] private float m_LayerSpeedX;

    private float m_LayerPosStartX;

    [SerializeField] private bool m_BackgroundLayerFollowY = true;

    public Transform GetTransform()
    {
        return m_Layer.transform;
    }

    public SpriteRenderer GetSpriteRenderer()
    {
        return m_Layer.GetComponent<SpriteRenderer>();
    }

    #region Loop X

    public void SetLayerSpeedX(float m_BackgroundLayerSpeedX)
    {
        m_LayerSpeedX = m_BackgroundLayerSpeedX;
    }

    public float GetLayerSpeedX()
    {
        return m_LayerSpeedX;
    }

    public void SetLayerPosStartX(float m_BackgroundLayerPosStartX)
    {
        m_LayerPosStartX = m_BackgroundLayerPosStartX;
    }

    public void SetLayerPosStartXChance(float m_BackgroundLayerPosStartXChance)
    {
        m_LayerPosStartX += m_BackgroundLayerPosStartXChance;
    }

    public float GetLayerPosStartX()
    {
        return m_LayerPosStartX;
    }

    public void SetLayerFollowY(bool m_BackgroundLayerFollowY)
    {
        this.m_BackgroundLayerFollowY = m_BackgroundLayerFollowY;
    }

    public bool GetLayerFollowY()
    {
        return m_BackgroundLayerFollowY;
    }

    #endregion
}

public enum BackgroundLoopType { Horizontal, Vertical }