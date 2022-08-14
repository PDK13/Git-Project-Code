using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sample_Toggle : MonoBehaviour
{
    public void Toggle_A(Toggle t_Toggle) //Gắn GameObject Toggle vào biến "t_Toggle" này trong Unity
    {
        if (!t_Toggle.isOn)
        {
            return;
        }

        Debug.Log("[Debug] Toggle A!");
    }

    public void Toggle_B(Toggle t_Toggle)
    {
        if (!t_Toggle.isOn)
        {
            return;
        }

        Debug.Log("[Debug] Toggle B!");
    }
}
