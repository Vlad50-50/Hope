using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    [SerializeField]
    private Transform wheelTransform;
    [SerializeField]
    private bool isSteer, isInvertSteer, isPower;
    private float steerAngle;
    private float motorTorque;
    public WheelCollider wheelCollider;
    private void Start()
    {
        wheelCollider = GetComponent<WheelCollider>();
    }

    private void Update()
    {       
        wheelCollider.GetWorldPose(out Vector3 pos, out Quaternion rot);
        wheelTransform.position = pos;
        wheelTransform.rotation = rot * Quaternion.Euler(0, 0, -90f);
    }

    private void FixedUpdate()
    {
        if(isSteer)
        {
            wheelCollider.steerAngle = steerAngle * (isInvertSteer ? -1 : 1);
        }
        if (isPower)
        {
            wheelCollider.motorTorque = motorTorque;
        }
    }

    public void ChangeMotorTorque(float torque)
    {
        motorTorque = torque;
    }
    public void ChangeSteerAngle(float angle)
    {
        steerAngle = angle;
    }
}
