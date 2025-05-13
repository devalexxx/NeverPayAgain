using System;
using System.Linq;
using UnityEngine;
using UnityToolkit;

// Class to store and manage the player's data, including their inventory of champions.
[Serializable]
public class PlayerSave : IJsonSerializable
{
    [field: SerializeField] public GUID                guid      { get; private set; }
    [field: SerializeField] public InventoryCollection inventory { get; private set; }

    public PlayerSave()
    {
        guid      = Guid.Empty;
        inventory = new();
    }

    public void New(PlayerInitialData p_data)
    {
        guid      = Guid.NewGuid();
        inventory = new();
        p_data.champions.ForEach(p_behaviour => inventory.champion.Add(new(p_behaviour)));
    }
}
