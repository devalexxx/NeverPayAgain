using System;

// [Serializable]
public class PlayerData
{
    private ChampionInventory _championInventory;

    public ChampionInventory ChampionInventory => _championInventory;

    public PlayerData()
    {
        _championInventory = new();
    }
}
