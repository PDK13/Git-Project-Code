using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoopController : MonoBehaviour
{
    [Header("Camera")]

    [SerializeField] private bool b_Camera_Loop_X = true;

    [SerializeField] private Transform com_Camera;

    private float f_Camera_X;

    [Header("Background")]

    [SerializeField] private SpriteRenderer com_Background;

    private float f_Background_Bound_X;

    private float f_Background_Local_X;

    [Header("Background Layer")]

    [SerializeField] private List<BackgroundLoopLayer> l_Background_Layer;

    private void Start()
    {
        if (com_Camera == null)
        {
            com_Camera = Camera.main.transform;
        }

        f_Background_Bound_X = com_Background.GetComponent<SpriteRenderer>().bounds.size.x;
        f_Background_Local_X = com_Background.transform.localScale.x;

        f_Camera_X = com_Camera.position.x;

        for (int i = 0; i < l_Background_Layer.Count; i++)
        {
            l_Background_Layer[i].Set_Layer_PosStart_X(Get_Camera_X());
        }
    }

    private void Update()
    {
        for (int i = 0; i < l_Background_Layer.Count; i++)
        {
            float f_Temp = Get_Camera_X() * (1 - l_Background_Layer[i].Get_Layer_Speed_X());

            float f_Distance = Get_Camera_X() * l_Background_Layer[i].Get_Layer_Speed_X();

            l_Background_Layer[i].Get_Transform().transform.position = new Vector2(l_Background_Layer[i].Get_Layer_PosStart_X() + f_Distance + f_Camera_X, Get_Camera_Y(l_Background_Layer[i]));

            if (f_Temp > l_Background_Layer[i].Get_Layer_PosStart_X() + f_Background_Bound_X * f_Background_Local_X)
            {
                l_Background_Layer[i].Set_Layer_PosStart_X_Chance(f_Background_Bound_X * f_Background_Local_X);
            }
            else
            if (f_Temp < l_Background_Layer[i].Get_Layer_PosStart_X() - f_Background_Bound_X * f_Background_Local_X)
            {
                l_Background_Layer[i].Set_Layer_PosStart_X_Chance(-f_Background_Bound_X * f_Background_Local_X);
            }
        }
    }

    private float Get_Camera_X()
    {
        return com_Camera.position.x - f_Camera_X;
    }

    private float Get_Camera_Y(BackgroundLoopLayer cs_Layer)
    {
        return (cs_Layer.Get_Layer_Follow_Y()) ? com_Camera.position.y : cs_Layer.Get_Transform().transform.position.y;
    }
}

[System.Serializable]
public class BackgroundLoopLayer
{
    [SerializeField] private GameObject g_Layer;

    [SerializeField] [Range(0, 1)] private float f_Layer_Speed_X;

    private float f_Layer_PosStart_X;

    [SerializeField] private bool b_Layer_Follow_Y = true;

    public Transform Get_Transform()
    {
        return g_Layer.transform;
    }

    public SpriteRenderer Get_SpriteRenderer()
    {
        return g_Layer.GetComponent<SpriteRenderer>();
    }

    #region Loop X

    public void Set_Layer_Speed_X(float f_Background_Layer_Speed_X)
    {
        f_Layer_Speed_X = f_Background_Layer_Speed_X;
    }

    public float Get_Layer_Speed_X()
    {
        return f_Layer_Speed_X;
    }

    public void Set_Layer_PosStart_X(float f_Background_Layer_PosStart_X)
    {
        f_Layer_PosStart_X = f_Background_Layer_PosStart_X;
    }

    public void Set_Layer_PosStart_X_Chance(float f_Background_Layer_PosStart_X_Chance)
    {
        f_Layer_PosStart_X += f_Background_Layer_PosStart_X_Chance;
    }

    public float Get_Layer_PosStart_X()
    {
        return f_Layer_PosStart_X;
    }

    public void Set_Layer_Follow_Y(bool b_Background_Layer_Follow_Y)
    {
        b_Layer_Follow_Y = b_Background_Layer_Follow_Y;
    }

    public bool Get_Layer_Follow_Y()
    {
        return b_Layer_Follow_Y;
    }

    #endregion
}
