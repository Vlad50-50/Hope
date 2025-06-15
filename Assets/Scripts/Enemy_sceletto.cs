using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_sceletto : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;
    public int HP_Scelletto = 2700;
    public Transform playerTransform;

    void Start()
    {
        GameObject player = GameObject.FindWithTag("Plaer");
        if (player != null)
        {
            target = player.transform;
            Debug.Log("Игрок найден: " + target.name);
        }
        else
        {
            Debug.LogWarning("Игрок с тегом 'Player' не найден!");
        }
    }

    void Update()
    {
        if (target == null) return;
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider obj) {
        if (obj.gameObject.CompareTag("mega_cube"))
        {
            HP_Scelletto = HP_Scelletto - 200;
            Debug.Log(HP_Scelletto);
            if (HP_Scelletto <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
