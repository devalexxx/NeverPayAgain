using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Represents the progress of a champion, including rank, level, and experience
[Serializable]
public class ChampionProgress
{
    private static uint _bound = 500;    // Base experience threshold for leveling up

    [SerializeField] private uint _rank; // The rank of the champion
    [SerializeField] private uint _lvl;  // The current level of the champion
    [SerializeField] private uint _exp;  // Current experience points

    public uint Rank  { get => _rank; }
    public uint Level { get => _lvl;  }
    public uint Xp    { get => _exp;  }

    // List of listeners triggered when the champion levels up
    public UnityEvent<uint> onLevelUp;

    public ChampionProgress(uint level) 
    {
        _rank = 1;
        _lvl  = level;
        _exp  = 0;

        onLevelUp = new();
    }

    public ChampionProgress() : this(1) {}

    // Method to add experience points and handle level-up logic
    public void Earn(uint amount)
    {
        _exp += amount;
        uint mod;
        // Check if experience exceeds the current level threshold
        if ((mod = _exp % (_lvl * _bound)) > 0)
        {
            _lvl += 1;
            _exp  = mod;
            onLevelUp.Invoke(_lvl);
        }
    }
}
