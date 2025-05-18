using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveMent : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed;
    private Vector3 offset;

    void Start (){
        offset = transform.position - target.position;
    }

    void LateUpdate(){
        Vector3 camPos = new Vector3(target.position.x + offset.x, target.position.y + offset.y, target.position.z + offset.z);    
        transform.position = Vector3.Lerp(transform.position, camPos, Time.time * speed);   
    }
}