using UnityEngine;

[RequireComponent(typeof(IsoBlock))]
public class IsoBlockTeleport : MonoBehaviour
{
    [Header("Teleport to other World")]

    [SerializeField]
    private bool b_Teleport_isAlow = true;

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
            g_Teleport.GetComponent<IsoBlockTeleport>().Get_World_Name(),
            g_Teleport.GetComponent<IsoBlockTeleport>().Get_Spawm_Pos());
    }

    public void Set_Add(IsoDataTeleport cl_Data_Teleport)
    {
        Set_Add(cl_Data_Teleport.Get_World_Name(), cl_Data_Teleport.Get_Pos());
    }

    public void Set_Remove()
    {
        s_WorldName = "";
        v3_Pos_Teleport = new Vector3Int();
    }

    #endregion

    #region Teleport is Allow

    public void Set_Teleport_isAllow(bool b_Teleport_isAlow)
    {
        this.b_Teleport_isAlow = b_Teleport_isAlow;
    }

    public bool Get_Teleport_isAllow()
    {
        return this.b_Teleport_isAlow;
    }

    #endregion

    #region Teleport Get

    public bool Get_isExist()
    {
        return Get_World_Name() != "";
    }
    
    public string Get_World_Name()
    {
        if (Get_Teleport_isAllow())
        {
            return s_WorldName;
        }

        return "";
    }

    public Vector3Int Get_Spawm_Pos()
    {
        if (Get_Teleport_isAllow())
        {
            return v3_Pos_Teleport;
        }

        return new Vector3Int();
    }

    #endregion
}
