using UnityEngine;
using UnityEngine.UI;

public class IsoEditorUIJoinToClone : MonoBehaviour
{
    [Header("Text for Join-To Clone")]

    [Tooltip("Move Dir")]
    [SerializeField]
    private Text t_Dir;

    [Tooltip("Move Length")]
    [SerializeField]
    private Text t_Length;

    [Tooltip("Move Speed")]
    [SerializeField]
    private Text t_Speed;

    [Tooltip("Move Pos-Move-To")]
    [SerializeField]
    private Text t_PosMoveTo;

    #region Set Clone

    public void SetClone(Vector3Int v3_Dir, int i_Length, float f_Speed, Vector3Int v3_PosMoveTo)
    {
        if (v3_Dir == IsoClassDir.v3_None)
        {
            t_Dir.text = "STA";
            t_Dir.fontSize = 8;
            t_Length.text = "";
            t_Speed.text = "";

            return;
        }

        if (v3_Dir == IsoClassDir.v3_Up_X)
        {
            t_Dir.text = "U";
        }
        else
        if (v3_Dir == IsoClassDir.v3_Down_X)
        {
            t_Dir.text = "D";
        }
        else
        if (v3_Dir == IsoClassDir.v3_Left_Y)
        {
            t_Dir.text = "L";
        }
        else
        if (v3_Dir == IsoClassDir.v3_Right_Y)
        {
            t_Dir.text = "R";
        }
        else
        if (v3_Dir == IsoClassDir.v3_Top_H)
        {
            t_Dir.text = "T";
        }
        else
        if (v3_Dir == IsoClassDir.v3_Bot_H)
        {
            t_Dir.text = "B";
        }
        t_Dir.fontSize = 10;
        t_Length.text = i_Length.ToString();
        t_Speed.text = f_Speed.ToString();
        t_PosMoveTo.text = v3_PosMoveTo.ToString();
    }

    #endregion
}
