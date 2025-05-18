using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class miniBlock : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float freq;
    [SerializeField] private float amplitude;
    private float currentAngle, currentY;
    void Start()
    {
        currentY = transform.position.y;
    }

    void Update()
    {
        currentAngle = transform.rotation.eulerAngles.y;
        float nextAngle = currentAngle+rotationSpeed*Time.deltaTime*1000;
        transform.rotation = Quaternion.AngleAxis(nextAngle, new Vector3(0,1,0));
        double nextY = amplitude*Math.Cos(nextAngle*(float)Math.PI/180*freq);
        Vector3 posVec = 
                new Vector3(transform.position.x, currentY+(float)nextY, transform.position.z);
        transform.position = posVec;
    }
}
