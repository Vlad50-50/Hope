using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private BlockTypes blockTypes;
    private int health;

    public int Health
    {
        get
        {
            return health;
        }
        set
        {
           health = value;
        }
    }


    void Start()
    {
        Health = (int)blockTypes;
    }

    public void DestroyBehaviour()
    {
        GameObject miniBlock = Resources.Load<GameObject>("mini"+blockTypes.ToString());

        GameObject temp = Instantiate(miniBlock, transform.position, Quaternion.identity);
        temp.transform.SetParent(transform.parent.transform);
        Destroy(gameObject);
    }
}
