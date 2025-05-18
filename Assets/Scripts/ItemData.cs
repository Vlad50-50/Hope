using System;
using UnityEngine;
[Serializable]
public class ItemData
{
    public string name;
    public int id, count;
    [Multiline] public string description;
    public bool isUniq;
    public ItemData(string name, int id, int count,
    bool isUniq, string description)
    {
        this.name = name;
        this.id = id;
        this.count = count;
        this.isUniq = isUniq;
        this.description = description;
    }
}
