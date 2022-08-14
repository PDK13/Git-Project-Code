using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample_ClassController
{
    public static Action<string> s_ActionString; //Cách khai báo

    public static void Set_ActionString(string s_ActionString_This) //Nên viết như thế này
    {
        s_ActionString?.Invoke("Set_ActionString: " + s_ActionString_This); //Truyền vào đúng tham số đã khai báo
    }

    public static void Set_ActionString() //Không nên viết như thế này
    {
        s_ActionString?.Invoke("Set_ActionString: " + s_ActionString);
    }
}
