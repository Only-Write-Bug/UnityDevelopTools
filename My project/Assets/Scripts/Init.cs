using System;
using System.Collections;
using System.Collections.Generic;
using Test;
using Tools.ObjectPoolTool;
using Tools.ObjectPoolTool.Enum;
using UnityEngine;

public class Init : MonoBehaviour
{
    private ObjectPoolModel<cars> testGOPool = null;

    private void Start()
    {
        testGOPool = ObjectPoolTool.ApplyCreateObjectPool<cars>(8, OBJECT_POOL_MEMORY_TYPE.PREDICTABLE_CAPACITY);
    }

    private void FixedUpdate()
    {
        
    }
}