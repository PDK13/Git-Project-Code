using UnityEngine;

[RequireComponent(typeof(IsoBlock))]
public class IsoBlockTeleport : MonoBehaviour
{
    [Header("Teleport to other World")]

    [SerializeField]
    private bool m_TeleportIsAlow = true;

    [Tooltip("World Name for Teleport")]
    private string s_WorldName = "";

    [Tooltip("Pos for Teleport")]
    private Vector3Int v3_Pos_Teleport;

    #region Teleport Set

    public void Set_Add(string s_WorldName, Vector3Int v3_Pos_Teleport)
    {
        this.s_WorldName = s_WorldName;
        this.v3_Pos_Teleport = v3_Pos_Teleport;
    }

    public void Set_Add(GameObject g_Teleport)
    {
        Set_Add(
            g_Teleport.GetComponent<IsoBlockTeleport>().GetWorld_Name(),
            g_Teleport.GetComponent<IsoBlockTeleport>().GetSpawm_Pos());
    }

    public void Set_Add(IsoDataTeleport cl_Data_Teleport)
    {
        Set_Add(cl_Data_Teleport.GetWorld_Name(), cl_Data_Teleport.GetPos());
    }

    public void Set_Remove()
    {
        s_WorldName = "";
        v3_Pos_Teleport = new Vector3Int();
    }

    #endregion

    #region Teleport is Allow

    public void Set_TeleportIsAllow(bool m_TeleportIsAlow)
    {
        this.m_TeleportIsAlow = m_TeleportIsAlow;
    }

    public bool GetTeleportIsAllow()
    {
        return m_TeleportIsAlow;
    }

    #endregion

    #region Teleport Get

    public bool GetisExist()
    {
        return GetWorld_Name() != "";
    }

    public string GetWorld_Name()
    {
        if (GetTeleportIsAllow())
        {
            return s_WorldName;
        }

        return "";
    }

    public Vector3Int GetSpawm_Pos()
    {
        if (GetTeleportIsAllow())
        {
            return v3_Pos_Teleport;
        }

        return new Vector3Int();
    }

    #endregion
}
