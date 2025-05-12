using System;
using System.Collections.Generic;
using UnityEngine;

// Manages a collection of champions, providing methods to add, remove, and access champions.
[Serializable]
public class ChampionInventory
{
    [SerializeField]
    private List<Champion> _champions;

    public ChampionInventory()
    {
        _champions = new();
    }

    public Champion GetItem(int index)
    {
        if (_champions.Count > index)
        {
            return _champions[index];
        }
        else
        {
            return null;
        }
    }

    public void ForEach(Action<Champion> action)
    {
        _champions.ForEach(action);
    }

    public bool AddItem(Champion champion)
    {
        // @TODO: Add inventory size and check if there is enough space
        _champions.Add(champion);
        return true;
    }

    public void RemoveItem(Champion champion)
    {
        _champions.Remove(champion);
    }

    public void RemoveItemAt(int index)
    {
        _champions.RemoveAt(index);
    }
}
