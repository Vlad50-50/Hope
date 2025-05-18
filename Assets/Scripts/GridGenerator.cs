using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] GameObject GroundPref;
    void Start()
    {
        for (int x = 0; x < 10; x++)
        {
            for (int z = 0; z < 10; z++)
            {
                Instantiate(GroundPref, new Vector3(x,0,z), Quaternion.identity);
            }
        }
    }
}
