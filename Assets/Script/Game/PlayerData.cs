using System;

// Class to store and manage the player's data, including their inventory of champions.
public class PlayerData
{
    private ChampionInventory _championInventory;

    public ChampionInventory ChampionInventory => _championInventory;

    public PlayerData()
    {
        _championInventory = new();
    }
}
