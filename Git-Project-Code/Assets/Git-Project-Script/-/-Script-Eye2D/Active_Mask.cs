using UnityEngine;

public class Active_Mask : MonoBehaviour
{
    //Script điều khiển gán Mask lên chính mục tiêu được gắn Script này

    public GameObject g_Mask;
    private GameObject g_MaskClone;

    public Vector3 v_PosMask = new Vector3(0f, 0f, -1f);

    private bool m_MaskActive = false;

    private void Update()
    {
        Active_MaskControl();
    }

    private void Active_MaskControl()
    {
        if (m_MaskActive)
        //Nếu cho phép kích hoạt hiển thị
        {
            if (g_MaskClone == null)
            {
                //Nếu chưa có thì tạo mới và gán vị trí cho nó
                g_MaskClone = Instantiate(g_Mask, transform.position + v_PosMask, transform.rotation);
            }
            else
            {
                //Thay đổi vị trí cho nó nếu đã có
                g_MaskClone.transform.position = transform.position + v_PosMask;
            }
        }
        else
        //Nếu không cho phép kích hoạt hiển thị
        {
            if (g_MaskClone != null)
            //Nếu nó vẫn còn tồn tại thì phá huỷ nó đi
            {
                Destroy(g_MaskClone);
                g_MaskClone = null;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position + v_PosMask, 0.1f);
    }

    //--------------------------------------------------------------------------------
    public void Active_SetMaskControl(bool m_MaskActive)
    //Kích hoạt Mask
    {
        this.m_MaskActive = m_MaskActive;
    }
}
