using System;
using System.Linq;
using UnityEngine;
using UnityToolkit;

// Class to store and manage the player's data, including their inventory of champions.
[Serializable]
public class PlayerSave
{
    [field: SerializeField] public GUID                guid      { get; private set; }
    [field: SerializeField] public InventoryCollection inventory { get; private set; }

    public PlayerSave()
    {
        guid      = Guid.NewGuid();
        inventory = new();

        Resources.LoadAll<ChampionBehaviour>("Champion/Object")
            .OrderBy(_ => UnityEngine.Random.value)
            .Take(3)
            .ToList()
            .ForEach(t_behaviour => inventory.champion.Add(new(t_behaviour)));
    }
}
