using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample_Singleton
{
    //Xác nhận chỉ 1 lớp này tồn tại trong hệ thống
    private static Sample_Singleton instance = new Sample_Singleton();

    private int i_Number = 2;

    public int i_Number_Public = 5;

    public static Sample_Singleton Get_Instance()
    {
        return instance;
    }

    public static void Set_Number(int i_Number)
    {
        Get_Instance().i_Number = i_Number;
    }

    public static int Get_Number()
    {
        return Get_Instance().i_Number;
    }
}