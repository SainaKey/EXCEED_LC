using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalTimeManager : MonoBehaviour
{
    [SerializeField] private float globalTime;
    private int propertyID;

    private void Start()
    {
        propertyID = Shader.PropertyToID("_GlobalTime");
    }

    void Update()
    {
        globalTime += Time.deltaTime;
        Shader.SetGlobalFloat(propertyID,globalTime);
    }
}
