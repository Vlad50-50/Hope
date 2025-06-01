using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaaner : MonoBehaviour
{
    public GameObject objectToSpawn;     // Префаб объекта
    public int numberOfObjects;     // Кол-во объектов
    public Vector3 areaSize; // Размер области (ширина, высота, глубина)
    public Vector3 centerPosition;     // Центр области

    void Start()
    {
        SpawnObjects();
    }

    void SpawnObjects()
    {
        centerPosition = transform.position;
        for (int i = 0; i < numberOfObjects; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(centerPosition.x - areaSize.x / 2, centerPosition.x + areaSize.x / 2),
                Random.Range(centerPosition.y - areaSize.y / 2, centerPosition.y + areaSize.y / 2),
                Random.Range(centerPosition.z - areaSize.z / 2, centerPosition.z + areaSize.z / 2)
            );
            Debug.Log(randomPosition);
            Instantiate(objectToSpawn, randomPosition, Quaternion.identity);
        }
    }
}
