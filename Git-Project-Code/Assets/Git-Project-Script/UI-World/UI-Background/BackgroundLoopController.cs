using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoopController : MonoBehaviour
{
    [Header("Camera")]

    [SerializeField] private bool m_Camera_Loop_X = true;

    [SerializeField] private Transform com_Camera;

    private float m_Camera_X;

    [Header("Background")]

    [SerializeField] private SpriteRenderer com_Background;

    private float m_Background_Bound_X;

    private float m_Background_Local_X;

    [Header("Background Layer")]

    [SerializeField] private List<BackgroundLoopLayer> l_Background_Layer;

    private void Start()
    {
        if (com_Camera == null)
        {
            com_Camera = Camera.main.transform;
        }

        m_Background_Bound_X = com_Background.GetComponent<SpriteRenderer>().bounds.size.x;
        m_Background_Local_X = com_Background.transform.localScale.x;

        m_Camera_X = com_Camera.position.x;

        for (int i = 0; i < l_Background_Layer.Count; i++)
        {
            l_Background_Layer[i].Set_Layer_PosStart_X(GetCamera_X());
        }
    }

    private void Update()
    {
        for (int i = 0; i < l_Background_Layer.Count; i++)
        {
            float m_Temp = GetCamera_X() * (1 - l_Background_Layer[i].GetLayer_Speed_X());

            float m_Distance = GetCamera_X() * l_Background_Layer[i].GetLayer_Speed_X();

            l_Background_Layer[i].GetTransform().transform.position = new Vector2(l_Background_Layer[i].GetLayer_PosStart_X() + m_Distance + m_Camera_X, GetCamera_Y(l_Background_Layer[i]));

            if (m_Temp > l_Background_Layer[i].GetLayer_PosStart_X() + m_Background_Bound_X * m_Background_Local_X)
            {
                l_Background_Layer[i].Set_Layer_PosStart_X_Chance(m_Background_Bound_X * m_Background_Local_X);
            }
            else
            if (m_Temp < l_Background_Layer[i].GetLayer_PosStart_X() - m_Background_Bound_X * m_Background_Local_X)
            {
                l_Background_Layer[i].Set_Layer_PosStart_X_Chance(-m_Background_Bound_X * m_Background_Local_X);
            }
        }
    }

    private float GetCamera_X()
    {
        return com_Camera.position.x - m_Camera_X;
    }

    private float GetCamera_Y(BackgroundLoopLayer cs_Layer)
    {
        return (cs_Layer.GetLayer_Follow_Y()) ? com_Camera.position.y : cs_Layer.GetTransform().transform.position.y;
    }
}

[System.Serializable]
public class BackgroundLoopLayer
{
    [SerializeField] private GameObject g_Layer;

    [SerializeField] [Range(0, 1)] private float m_Layer_Speed_X;

    private float m_Layer_PosStart_X;

    [SerializeField] private bool m_Layer_Follow_Y = true;

    public Transform GetTransform()
    {
        return g_Layer.transform;
    }

    public SpriteRenderer GetSpriteRenderer()
    {
        return g_Layer.GetComponent<SpriteRenderer>();
    }

    #region Loop X

    public void Set_Layer_Speed_X(float m_Background_Layer_Speed_X)
    {
        m_Layer_Speed_X = m_Background_Layer_Speed_X;
    }

    public float GetLayer_Speed_X()
    {
        return m_Layer_Speed_X;
    }

    public void Set_Layer_PosStart_X(float m_Background_Layer_PosStart_X)
    {
        m_Layer_PosStart_X = m_Background_Layer_PosStart_X;
    }

    public void Set_Layer_PosStart_X_Chance(float m_Background_Layer_PosStart_X_Chance)
    {
        m_Layer_PosStart_X += m_Background_Layer_PosStart_X_Chance;
    }

    public float GetLayer_PosStart_X()
    {
        return m_Layer_PosStart_X;
    }

    public void Set_Layer_Follow_Y(bool m_Background_Layer_Follow_Y)
    {
        m_Layer_Follow_Y = m_Background_Layer_Follow_Y;
    }

    public bool GetLayer_Follow_Y()
    {
        return m_Layer_Follow_Y;
    }

    #endregion
}
