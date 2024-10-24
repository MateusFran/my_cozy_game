using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item{
    public ItemData data;
    public int amount;

    public override string ToString()
    {
        return "Item: " + data.objectName + ", Amount: " + amount;
    }
}