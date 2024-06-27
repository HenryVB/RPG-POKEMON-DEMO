using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Inventario del player
public class Inventory : MonoBehaviour
{
    [SerializeField] private List<ItemSlot> slots;

    public List<ItemSlot> Slots { get => slots; set => slots = value; }

    public static Inventory GetInventory()
    {
        return FindObjectOfType<PlayerController>().GetComponent<Inventory>();
    }
}

[Serializable]
public class ItemSlot
{
    [SerializeField] ItemBase item;
    [SerializeField] int count;

    public ItemBase Item => item;
    public int Count => count;
}
