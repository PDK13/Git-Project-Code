using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample_MainClass : MonoBehaviour
{
    [SerializeField] private string s_Scene_Next = "Sample_StaticAction02";

    [SerializeField] private string s_MyString = "Sample_MainClass";

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject); //Không cho phép GameObject này Destroy khi chuyển Scene

        Sample_ClassController.s_ActionString += Set_Event; //Thêm sự kiện
    }

    private void OnDestroy()
    {
        Sample_ClassController.s_ActionString -= Set_Event; //Hủy sự kiện
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            Class_Scene.Set_ChanceScene(s_Scene_Next); //Chuyển Scene
        }

        //Action

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Sample_ClassController.Set_ActionString(s_MyString); //Nên sử dụng để thêm dữ liệu cho sự kiện
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            Sample_ClassController.Set_ActionString(); //Không nên sử dụng
        }

        //Static

        if (Input.GetKeyDown(KeyCode.A))
        {
            Sample_StaticScript.Set_StaticScript(); //Dữ liệu hiện tại của Static Script và có thể sử dụng trực tiếp mà không cần khai báo, vì đã nằm trong hệ thống
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Sample_StaticScript.Set_StaticScript(s_MyString); //Đổi dữ liệu của Static Script và có thể sử dụng trực tiếp mà không cần khai báo, vì đã nằm trong hệ thống
        }
    }

    private void Set_Event(string s_EventString) //Sự kiện sẽ thêm
    {
        Debug.Log("Set_Event: " + s_EventString);

        StartCoroutine(Set_Wait()); //Bắt đầu Đa luồng
    }

    IEnumerator Set_Wait() //Đa luồng
    {
        yield return new WaitForSeconds(3f); //Đặt điều kiện sau Return

        Debug.Log("Set_Wait: " + s_MyString);
    }
}
