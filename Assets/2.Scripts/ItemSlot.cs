using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot : MonoBehaviour
{
    public List<GameObject> slots = new List<GameObject>();
    [SerializeField] GameObject slotPrefabs;
    int maxSlot = 16;

    private void Start()
    {
        for (int i = 0; i < maxSlot; i++) 
        {
            GameObject go = Instantiate(slotPrefabs, transform, false);
            go.name = "Slot" + i;

            ItemSlotData slot = new ItemSlotData();
            slot.isEmpty = true;
            slot.slotObj = go;
            slots.Add(go);
        }
    }
}

