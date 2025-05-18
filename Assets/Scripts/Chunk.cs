using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
   private const int VISIBILITY_DISTANCE = 20;
   private Transform playerT;
   private bool isVisible;
   private Vector3 chunkPos;
    void Start()
    {
        playerT = GameObject.Find("Player").transform;
        chunkPos = transform.position;
        isVisible = true;
    }
    void SetActivity(bool isActive)
    {
        int childrenCount = transform.childCount;
        for(int i=0; i < childrenCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(isActive);
        }
        isVisible = isActive;
    }
    void Update()
    {
        float distance = Vector3.Distance(chunkPos, 
        new Vector3(playerT.position.x, 0, playerT.position.z));
        if (distance > VISIBILITY_DISTANCE && isVisible)
        {
            SetActivity(false);
        }
        if (distance < VISIBILITY_DISTANCE && !isVisible)
        {
            SetActivity(true);
        }
    }
}
