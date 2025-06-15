using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniCamera : MonoBehaviour
{
    public Transform player;

    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 newPos = player.position;
            newPos.y = transform.position.y; // оставляем высоту камеры
            transform.position = newPos;
        }
    }
}
