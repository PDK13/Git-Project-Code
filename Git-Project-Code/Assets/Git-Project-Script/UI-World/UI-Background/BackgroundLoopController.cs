using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoopController : MonoBehaviour
{
    [Header("Camera")]

    //[SerializeField] private bool mAllowCameraLoopX = true;

    [SerializeField] private Transform com_Camera;

    private float m_CameraX;

    [Header("Background")]

    [SerializeField] private SpriteRenderer com_Background;

    private float m_BackgroundBoundX;

    private float m_BackgroundLocamX;

    [Header("Background Layer")]

    [SerializeField] private List<BackgroundLoopLayer> m_BackgroundLayer;

    private void Start()
    {
        if (com_Camera == null)
        {
            com_Camera = Camera.main.transform;
        }

        m_BackgroundBoundX = com_Background.GetComponent<SpriteRenderer>().bounds.size.x;
        m_BackgroundLocamX = com_Background.transform.localScale.x;

        m_CameraX = com_Camera.position.x;

        for (int i = 0; i < m_BackgroundLayer.Count; i++)
        {
            m_BackgroundLayer[i].SetLayerPosStartX(GetCameraX());
        }
    }

    private void Update()
    {
        for (int i = 0; i < m_BackgroundLayer.Count; i++)
        {
            float m_Temp = GetCameraX() * (1 - m_BackgroundLayer[i].GetLayerSpeedX());

            float m_Distance = GetCameraX() * m_BackgroundLayer[i].GetLayerSpeedX();

            m_BackgroundLayer[i].GetTransform().transform.position = new Vector2(m_BackgroundLayer[i].GetLayerPosStartX() + m_Distance + m_CameraX, GetCameraY(m_BackgroundLayer[i]));

            if (m_Temp > m_BackgroundLayer[i].GetLayerPosStartX() + m_BackgroundBoundX * m_BackgroundLocamX)
            {
                m_BackgroundLayer[i].SetLayerPosStartXChance(m_BackgroundBoundX * m_BackgroundLocamX);
            }
            else
            if (m_Temp < m_BackgroundLayer[i].GetLayerPosStartX() - m_BackgroundBoundX * m_BackgroundLocamX)
            {
                m_BackgroundLayer[i].SetLayerPosStartXChance(-m_BackgroundBoundX * m_BackgroundLocamX);
            }
        }
    }

    private float GetCameraX()
    {
        return com_Camera.position.x - m_CameraX;
    }

    private float GetCameraY(BackgroundLoopLayer m_Layer)
    {
        return (m_Layer.GetCheckLayerFollowY()) ? com_Camera.position.y : m_Layer.GetTransform().transform.position.y;
    }
}

[System.Serializable]
public class BackgroundLoopLayer
{
    [SerializeField] private GameObject m_Layer;

    [SerializeField] [Range(0, 1)] private float m_LayerSpeedX;

    private float m_LayerPosStartX;

    [SerializeField] private bool mAllowBackgroundLayerFollowY = true;

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

    public void SetLayerFollowY(bool mAllowBackgroundLayerFollowY)
    {
        this.mAllowBackgroundLayerFollowY = mAllowBackgroundLayerFollowY;
    }

    public bool GetCheckLayerFollowY()
    {
        return mAllowBackgroundLayerFollowY;
    }

    #endregion
}
