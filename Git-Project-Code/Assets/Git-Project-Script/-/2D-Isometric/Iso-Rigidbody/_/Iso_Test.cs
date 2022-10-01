using UnityEngine;

public class Iso_Test : MonoBehaviour
{
    private IsoBlock cl_Block;

    [SerializeField]
    private Vector3 v3_PointA = new Vector3(0, 0, 0);

    [SerializeField]
    private Vector3 v3_PointB = new Vector3(0, 5, 0);

    [SerializeField]
    private float f_Time = 0.01f;

    private bool b_PointA = false;

    private readonly float f_Velocity_X;
    private readonly float f_Velocity_Y;
    private readonly float f_Velocity_High;

    private void Start()
    {
        cl_Block = GetComponent<IsoBlock>();
    }

    private void Update()
    {
        if (b_PointA)
        {
            Vector3 v3_Pos = cl_Block.Get_Pos_Current();

            v3_Pos.x = 0;
            v3_Pos.y -= f_Time;
            v3_Pos.z = 0;

            cl_Block.Set_Pos(v3_Pos);

            if (cl_Block.Get_Pos_Current().y <= v3_PointA.y)
            {
                b_PointA = false;
            }
        }
        else
        {
            Vector3 v3_Pos = cl_Block.Get_Pos_Current();

            v3_Pos.x = 0;
            v3_Pos.y += f_Time;
            v3_Pos.z = 0;

            cl_Block.Set_Pos(v3_Pos);

            if (cl_Block.Get_Pos_Current().y >= v3_PointB.y)
            {
                b_PointA = true;
            }
        }
    }
}
