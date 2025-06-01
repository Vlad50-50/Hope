using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    void OnCollisionEnter(Collision obj)
    {
        if (obj.gameObject.CompareTag("Plaer"))
        {
            Debug.Log("Entered");
            
            Destroy(gameObject);
        }
    }
}
