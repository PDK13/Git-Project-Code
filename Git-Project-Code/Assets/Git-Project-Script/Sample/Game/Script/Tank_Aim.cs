using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using DG.Tweening;

public class Tank_Aim : MonoBehaviour
{
    [SerializeField] private Transform com_Environment;

    [Header("Gun")]

    [SerializeField] private Transform com_Gun;

    [SerializeField] private float f_Gun_Deg_Min = 0f;

    [SerializeField] private float f_Gun_Deg_Max = 30f;

    private float f_Gun_Deg = 0f;

    [Header("Bullet")]

    [SerializeField] private GameObject g_Bullet;

    private List<GameObject> lg_Bullet_Fire;

    [SerializeField] private Transform com_Bullet_Fire;

    [SerializeField] private float f_Bullet_Deg_ByGun = 0f;

    [SerializeField] private float f_Bullet_Fire_Power = 0.5f;

    [SerializeField] private float f_Bullet_Fire_Fly = 3f;

    private void Start()
    {
        f_Gun_Deg = f_Gun_Deg_Min;

        lg_Bullet_Fire = new List<GameObject>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (f_Gun_Deg < f_Gun_Deg_Max)
            {
                f_Gun_Deg += 0.1f;

                //com_Gun.DORotate(new Vector3(0, 0, f_Gun_Deg), 0.1f);
            }
        }
        else
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (f_Gun_Deg > f_Gun_Deg_Min)
            {
                f_Gun_Deg -= 0.1f;

                //com_Gun.DORotate(new Vector3(0, 0, f_Gun_Deg), 0.1f);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject g_Bullet_Clone = Class_Object.Set_Prepab_Create(g_Bullet, com_Environment);
            g_Bullet_Clone.transform.position = com_Bullet_Fire.position;


        }
    }
}
