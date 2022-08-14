using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Isometric Editor Vector with Pos and Size 
/// </summary>
public class IsoEditorUIPosAndSize : MonoBehaviour
{
    [Header("Pos and Size UI")]

    [SerializeField]
    [Tooltip("Show and Hide with Canvas Button")]
    private Text t_PosAndSize;

    private Vector3Int v3_Pos;

    private Vector3Int v3_WorldSize;

    public void Set_UI_Pos(Vector3Int v3_Pos)
    {
        this.v3_Pos = v3_Pos;

        Set_UI(v3_Pos, v3_WorldSize);
    }
    
    public void Set_UI_WorldSize(Vector3Int v3_WorldSize)
    {
        this.v3_WorldSize = v3_WorldSize;

        Set_UI(v3_Pos, v3_WorldSize);
    }

    public void Set_UI_PosAndSize(Vector3Int v3_Pos, Vector3Int v3_WorldSize)
    {
        this.v3_Pos = v3_Pos;
        this.v3_WorldSize = v3_WorldSize;

        Set_UI(v3_Pos, v3_WorldSize);
    }

    private void Set_UI(Vector3Int v3_Pos, Vector3Int v3_Size)
    {
        t_PosAndSize.text =
            "| X = " + (v3_Pos.x) + "/" + (v3_Size.x - 1) + " " +
            "| Y = " + (v3_Pos.y) + "/" + (v3_Size.y - 1) + " " +
            "| H = " + (v3_Pos.z) + "/" + (v3_Size.z - 1) + " |";
    }
}
