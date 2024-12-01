using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ChampionProgress
{
    private static uint _bound = 500;

    [SerializeField] private uint _rank;
    [SerializeField] private uint _lvl;
    [SerializeField] private uint _exp;

    public uint Rank  { get => _rank; }
    public uint Level { get => _lvl;  }
    public uint Xp    { get => _exp;  }

    private readonly List<Action> _levelUpListeners;

    public ChampionProgress(uint level, Action levelUpListener) 
    {
        _rank = 1;
        _lvl  = level;
        _exp  = 0;
        _levelUpListeners = new() { levelUpListener };
    }

    public ChampionProgress(Action levelUpListener) : this(1, levelUpListener) {}

    public void Earn(uint amount)
    {
        _exp += amount;
        uint mod;
        if ((mod = _exp % (_lvl * _bound)) > 0)
        {
            _lvl += 1;
            _exp  = mod;
            _levelUpListeners.ForEach(listener => listener());

        }
    }
}
