using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    [SerializeField] private ToolTypes type;
    [SerializeField] private ToolMaterials material;
    
    public int damageToEnemy;
    public int damageToBlock;

    void Awake()
    {
        damageToBlock = (int)type * (int)material;
        switch (type)
        {
            case ToolTypes.PICKAXE:
                damageToBlock = 4 * (int)material;
                break;
            case ToolTypes.SWORD:
                damageToBlock = 2 * (int)material;
                break;
        }
    }

}
