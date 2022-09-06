using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample_StaticScript : MonoBehaviour
{
    private static Sample_StaticScript cl_This; 
    //Dùng để khẳng định chỉ được xuất hiện duy nhất 1 Script này trong Scene và có thể dùng Script này cho toàn bộ hệ thống mà không cần khai báo

    [SerializeField] private string s_MyString = "Sample_StaticScript";

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (cl_This == null)
        {
            cl_This = this;
        }
    }

    public static void Set_StaticScript()
    {
        Debug.Log("Set_StaticScript: " + cl_This.s_MyString);
    }

    public static void Set_StaticScript(string s_MyString_Chance)
    {
        Debug.Log("Set_StaticScript Before: " + cl_This.s_MyString);

        cl_This.s_MyString = s_MyString_Chance;

        Debug.Log("Set_StaticScript After: " + cl_This.s_MyString);
    }
}
