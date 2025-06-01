using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_sceletto : MonoBehaviour
{
    public Transform target;   // Целевой игрок
    public float speed = 5f;   // Скорость движения
    void Update()
    {
        if (target == null) return;
        // Направление движения к цели
        Vector3 direction = (target.position - transform.position).normalized;
        // Перемещение к цели
        transform.position += direction * speed * Time.deltaTime;
    }
}
