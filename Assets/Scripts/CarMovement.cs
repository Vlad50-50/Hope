using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CarMovement : MonoBehaviour
{
    [SerializeField] private Transform centerMass;
    [SerializeField] private float motorTorque;
    [SerializeField] private float maxSteer;
    private float horizontal, vertical;
    private Rigidbody rb;
    [SerializeField] private Wheel[] wheels;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerMass.localPosition;
        wheels = GetComponentsInChildren<Wheel>();
    }

    void Update()
    {
        Control();
    }

    private void Control()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        foreach (Wheel wheel in wheels)
        {
            wheel.ChangeMotorTorque(vertical * motorTorque);
            wheel.ChangeSteerAngle(horizontal * maxSteer);
        }
    }
}
