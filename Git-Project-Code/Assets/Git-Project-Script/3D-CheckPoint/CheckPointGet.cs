﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointGet : MonoBehaviour
{
    [Header("Check Point(s)")]
    public Transform t_First;

    public float f_DegCheck = 90;

    [Header("Debug")]
    public Transform t_Next;

    private void Start()
    {
        t_Next = t_First;
    }

    public void Set_Next(Transform t_NewNext)
    {
        this.t_Next = t_NewNext;
    }

    public float Get_OffsetRotate()
    {
        return Class_Vector.Get_DirToDeg_XZ_MainToCheck(this.transform, t_Next.transform);
    }

    /// <summary>
    /// Check if this Object Turn Ron way
    /// </summary>
    /// <param name="f_AngleHigher">Require 30 Deg to check</param>
    /// <returns></returns>
    public bool Get_RonWay(float f_AngleHigher)
    {
        return Get_OffsetRotate() >= f_AngleHigher;
    }

    /// <summary>
    /// Check if this Object Turn Right way
    /// </summary>
    /// <param name="f_AngleLower"></param>
    /// <returns></returns>
    public bool Get_RightWay(float f_AngleLower)
    {
        return Get_OffsetRotate() <= f_AngleLower;
    }
}
