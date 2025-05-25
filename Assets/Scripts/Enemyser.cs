using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyser : MonoBehaviour
{
    private float chanse = 25f;
    [SerializeField] private GameObject secelet;
    void Start()
    {
        foreach (Transform child in transform)
        {
            Debug.Log("Child: " + child.name + " Position: " + child.position);

            if (Random.Range(0f, 100f) >= chanse)
            {
                  Instantiate(secelet, child.position, child.rotation);
            }
        }
    }
}
