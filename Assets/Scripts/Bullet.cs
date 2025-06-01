using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
     void Start()
    {
        // Запускаем корутину, которая удалит пульку через 3 секунды
        StartCoroutine(DestroyBulletAfterTime(3f));
    }

    // Корутин, который удаляет пульку через заданное время
    IEnumerator DestroyBulletAfterTime(float time)
    {
        // Ждем указанное количество секунд
        yield return new WaitForSeconds(time);

        // Удаляем пульку
        Destroy(gameObject);
    }
}
