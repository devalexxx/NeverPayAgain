using System;
using UnityEngine;

[Serializable]
public class InventoryCollection
{
    [field: SerializeField] public Inventory<Champion> champion { get; private set; }

    public InventoryCollection()
    {
        champion = new(25);
    }
}
