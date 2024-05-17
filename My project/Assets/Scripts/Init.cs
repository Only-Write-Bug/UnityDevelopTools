using System;
using System.Collections;
using System.Collections.Generic;
using Tools.ObjectPoolTool;
using Tools.ObjectPoolTool.Enum;
using UnityEngine;

public class Init : MonoBehaviour
{
    private void Start()
    {
        EventCenter.Instance.CreateEvent("aaa");
    }

    private void FixedUpdate()
    {
        
    }
}