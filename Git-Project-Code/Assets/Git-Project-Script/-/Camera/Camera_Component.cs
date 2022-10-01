using UnityEngine;

[RequireComponent(typeof(Camera))]

//Điều khiển Camera
public class Camera_Component : MonoBehaviour
{
    public Transform t_Follow;
    //Object cần di chuyển theo

    //public Transform t_Renderer_Rotation;
    //public bool m_Renderer_Rotation = false;
    //Tự động xoay Object cần di chuyển theo để hiển thị tốt hơn

    public Vector3 v3_Follow = new Vector3(0f, 6f, -10f);
    //Khoá vị trí của Camera so với Object cần di chuyển theo (Mặc định là phía trên cao)
    public bool m_FollowX = true;
    public bool m_FollowY = true;
    public bool m_FollowZ = false;
    //Cho phép Camera di chuyển teo Object theo trục X, Y và Z?

    public Vector3 v3_Smooth = new Vector3(0.5f, 0.5f, 0.5f);
    //Độ trễ của Camera khi di chuyển theo Object (Đặt về 0 để di chuyển tức thời)
    private Vector3 v3_SmoothVelocity;
    //Quán tính Camera khi di chuyển theo m_ục tiêu

    public Vector3 v3_Rotation = new Vector3(35f, 0f, 0f);
    //Góc quay của Camera (Mặc định là hướng xuống)

    private float m_PosX, m_PosY, m_PosZ;

    private void LateUpdate()
    {
        if (t_Follow != null)
        {
            Control_Follow();
        }
    }

    //Điều khiển Camera
    private void Control_Follow()
    {
        if (m_FollowX)
        {
            if (v3_Smooth.x == 0)
            {
                m_PosX = t_Follow.position.x;
            }
            else
            {
                m_PosX = Mathf.SmoothDamp(transform.position.x, t_Follow.position.x + v3_Follow.x, ref v3_SmoothVelocity.x, v3_Smooth.x);
            }
        }
        else
        {
            m_PosX = transform.position.x;
        }

        if (m_FollowY)
        {
            if (v3_Smooth.y == 0)
            {
                m_PosY = t_Follow.position.y;
            }
            else
            {
                m_PosY = Mathf.SmoothDamp(transform.position.y, t_Follow.position.y + v3_Follow.y, ref v3_SmoothVelocity.y, v3_Smooth.y);
            }
        }
        else
        {
            m_PosY = transform.position.y;
        }

        if (m_FollowZ)
        {
            if (v3_Smooth.y == 0)
            {
                m_PosZ = t_Follow.position.z;
            }
            else
            {
                m_PosZ = Mathf.SmoothDamp(transform.position.z, t_Follow.position.z + v3_Follow.z, ref v3_SmoothVelocity.z, v3_Smooth.z);
            }
        }
        else
        {
            m_PosZ = transform.position.z;
        }

        //Vector.SmoothDamp càng gia tăng tốc độ khi m_ục tiêu càng ở xa hơn

        Set_CameraPosition(m_PosX, m_PosY, m_PosZ);
        //Chú ý: Không rút gọn đoạn code này vì sẽ gây ra lỗi hoạt động
    }


    //private void Set_Renderer_Rotation(float m_DegX_Chance, float m_DegY_Chance, float m_DegZ_Chance)
    //{
    //    Vector3 v3_RotationVector = new Vector3(m_DegX_Chance, m_DegY_Chance, m_DegZ_Chance);
    //    Quaternion q_Rotation = Quaternion.Euler(v3_RotationVector);
    //    t_Renderer_Rotation.rotation = q_Rotation;
    //}

    private void OnDrawGizmos()
    {
        if (Application.isEditor && t_Follow != null)
        {
            //Nếu đang trong Edit Mode của Unity >> Thực hiện Debug

            Set_CameraRotation(v3_Rotation.x, v3_Rotation.y, v3_Rotation.z);
            //Xoay Camera
        }
    }

    //-------------------------------------------------------

    //Thiết đặt m_ục tiêu di chuyển theo
    public void Set_Follow(Transform t_FollowNew)
    {
        t_Follow = t_FollowNew;
    }

    public void Set_Follow_InstanceFollow(Transform t_FollowNew)
    {
        t_Follow = t_FollowNew;

        Set_CameraPosition(t_Follow.position.x + v3_Follow.x, t_Follow.position.y + v3_Follow.y, t_Follow.position.z + v3_Follow.z);
    }

    //Thiết đặt Camera kiểu 2D và 2.5D (tức không có chiều sâu)
    public void Set_CameraNonDepth(float m_CameraSize)
    {
        if (GetComponent<Camera>() != null)
        {
            GetComponent<Camera>().orthographicSize = m_CameraSize;
        }
    }

    //Thiết đặt Camera kiểu 3D (tức có chiều sâu)
    public void Set_CameraWithDepth(float m_CameraView)
    {
        if (GetComponent<Camera>() != null)
        {
            GetComponent<Camera>().fieldOfView = m_CameraView;
        }
    }

    //Di chuyển Camera
    public void Set_CameraPosition(float m_PosX_Chance, float m_PosY_Chance, float m_PosZ_Chance)
    {
        transform.position = new Vector3(m_PosX_Chance, m_PosY_Chance, m_PosZ_Chance);
    }

    //Di chuyển Camera
    public void Set_CameraPosition(Vector3 v3_Pos_Chance)
    {
        transform.position = v3_Pos_Chance;
    }

    //Xoay Camera
    public void Set_CameraRotation(float m_DegX_Chance, float m_DegY_Chance, float m_DegZ_Chance)
    {
        Vector3 v3_RotationVector = new Vector3(m_DegX_Chance, m_DegY_Chance, m_DegZ_Chance);
        Quaternion q_Rotation = Quaternion.Euler(v3_RotationVector);
        transform.rotation = q_Rotation;
    }

    //Xoay Camera hướng Lên theo trục X
    public void Set_CameraRotation_Up(float m_Deg_Up_Chance)
    {
        Set_CameraRotation(-Mathf.Abs(m_Deg_Up_Chance), transform.rotation.y, transform.rotation.z);
    }

    //Xoay Camera hướng Xuống theo trục X
    public void Set_CameraRotation_Down(float m_Deg_Down_Chance)
    {
        Set_CameraRotation(Mathf.Abs(m_Deg_Down_Chance), transform.rotation.y, transform.rotation.z);
    }

    //Xoay Camera hướng Xuống theo trục Y
    public void Set_CameraRotation_Left(float m_Deg_Left_Chance)
    {
        Set_CameraRotation(transform.rotation.x, -Mathf.Abs(m_Deg_Left_Chance), transform.rotation.z);
    }

    //Xoay Camera hướng Xuống theo trục Y
    public void Set_CameraRotation_Right(float m_Deg_Right_Chance)
    {
        Set_CameraRotation(transform.rotation.x, Mathf.Abs(m_Deg_Right_Chance), transform.rotation.z);
    }

    //Xoay Camera hướng vòng Trái theo trục Z
    public void Set_CameraRotation_ClockLeft(float m_Deg_ClockLeft_Chance)
    {
        Set_CameraRotation(transform.rotation.x, transform.rotation.y, -Mathf.Abs(m_Deg_ClockLeft_Chance));
    }

    //Xoay Camera hướng vòng Trái theo trục Z
    public void Set_CameraRotation_ClockRight(float m_Deg_ClockRight_Chance)
    {
        Set_CameraRotation(transform.rotation.x, transform.rotation.y, Mathf.Abs(m_Deg_ClockRight_Chance));
    }
}
