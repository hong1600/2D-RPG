using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Equip,
    Expendables,
    Etc
}

[System.Serializable]
public class Item
{
    public ItemType itemType;
    public string itemName;
    public Sprite itemImage;
}
