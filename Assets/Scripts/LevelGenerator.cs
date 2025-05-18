using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] GameObject GroundPref;
    private const int PYRAMID_COUNT = 2;
    private const int PYRAMID_HEIGHT = 6;
    private const int PYRAMID_BASE = PYRAMID_HEIGHT*2 - 1;

    void CreatePyramide(Vector3 pos)
    {
        int offsetX = 0, offsetZ = 0;
        for (int y = 0; y < PYRAMID_HEIGHT; y++)
        {
            for (int x = (int)pos.x + offsetX; x < pos.x+PYRAMID_BASE-offsetX; x++)
            {
                for (int z = (int)pos.z + offsetZ; z < pos.z+PYRAMID_BASE-offsetZ; z++)
                {
                    GameObject tempGround = Instantiate(GroundPref, new Vector3(x+0.5f,y+0.5f,z+0.5f), Quaternion.identity);
                    if (y%2 == 0)
                    {
                        tempGround.GetComponent<MeshRenderer>().material.color = new Color(0,0,0.5f);
                    }
                    else
                    {
                        tempGround.GetComponent<MeshRenderer>().material.color = new Color(0,0.5f,0f);
                    
                    }
                }

            }
        
        offsetX++;
        offsetZ++;
        }
    }

    void Start()
    {
        for(int x=0; x < PYRAMID_COUNT; x++)
        {
            for(int z=0; z < PYRAMID_COUNT; z++)
            {
                CreatePyramide(new Vector3(x*PYRAMID_BASE,0,z*PYRAMID_BASE));
            }
        }
         
    }
}
