using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UIElements;

public class RandomLevelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject groundPref, grassPref;
    [SerializeField] private int baseHeight = 2, maxBlocksCountY = 10, chunkSize = 16,
                                 perlinNoizeSensetivity = 25, chunkCount = 4;
    private float seedX, seedY;
    [SerializeField] private GameObject chestPref, doubleChestPref;
    private int countChest = 0;
    void CreateChunk( int chunkNumX, int chunkNumZ)
    {
        GameObject chunk = new GameObject();
        float chunkX = chunkNumX*chunkSize + chunkSize/2;
        float chunkZ = chunkNumZ*chunkSize + chunkSize/2;
        
        chunk.transform.position = new Vector3(chunkX, 0f, chunkZ);
        chunk.name = "chunk" + chunkX + ", " + chunkZ;
        chunk.AddComponent<MeshFilter>();
        chunk.AddComponent<MeshRenderer>();
        chunk.AddComponent<Chunk>();

        for (int x = chunkNumX*chunkSize; x < chunkNumX*chunkSize+chunkSize; x++)
        {
            for (int z = chunkNumZ*chunkSize; z < chunkNumZ*chunkSize+chunkSize; z++)
        {
            float xSample = seedX + (float)x / perlinNoizeSensetivity;
            float ySample = seedY + (float)z / perlinNoizeSensetivity;
            float sample = Mathf.PerlinNoise(xSample, ySample);
            int height = baseHeight + (int)(sample*maxBlocksCountY);

            for (int y = 0; y < height; y++)
            {
                GameObject temp, tempC, tempDC;
                if (y == height-1)
                {
                    temp = Instantiate(grassPref, new Vector3(x,y,z), Quaternion.identity);
                    int createChestChance = Random.Range(0, 10000);
                    if (createChestChance > 9998)
                    {
                        int createDoubleChestChance = Random.Range(0, 100);
                        if (createDoubleChestChance > 50)
                        {
                            tempDC = Instantiate(doubleChestPref, new Vector3(x,height,z), Quaternion.identity);
                            tempDC.transform.SetParent(chunk.transform);
                        }
                        else{
                            tempC = Instantiate(chestPref, new Vector3(x,height,z), Quaternion.identity);
                        tempC.transform.SetParent(chunk.transform);
                        }
                        
                    }
                    
                }
                else 
                {
                    temp = Instantiate(groundPref, new Vector3(x,y,z), Quaternion.identity);
                }
                temp.transform.SetParent(chunk.transform);
            }

        }
        }
        chunk.transform.SetParent(transform);
    }

    void Start()
    {
        
    }

    [ContextMenu("CreateLevel")]
    void CreateLevel() {
        seedX = Random.Range(0,10);
        seedY = Random.Range(0,10);
        for ( int x = 0; x < chunkCount; x++)
        {
            for ( int z = 0; z < chunkCount; z++)
            {
                CreateChunk(x,z);
            }
        }
    }

    [ContextMenu("ClearLevel")]
    void ClearLevel(){
        for(int i = transform.childCount-1; i >= 0; i--){
             DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }

    private void CreateChest(int x, int y, int z)
    {
        int createChestChance = Random.Range(0, 100);
        
        if (createChestChance > 99)
        {
            countChest++;
            Debug.Log(countChest);
            Instantiate(chestPref, new UnityEngine.Vector3(x,y,z), 
            Quaternion.identity);
        }
    }
}


