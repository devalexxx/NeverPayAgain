using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Progress
{
    private static uint _bound = 500;

    [SerializeField] private uint _rank;
    [SerializeField] private uint _lvl;
    [SerializeField] private uint _exp;

    public uint Rank  { get => _rank; }
    public uint Level { get => _lvl;  }
    public uint Xp    { get => _exp;  }

    private readonly Action<uint> _onLevelUp;

    public Progress()
    {
        _rank = 1;
        _lvl  = 1;
        _exp  = 0;
    }

    public Progress(Action<uint> onLevelUp) : this() 
    {
        _onLevelUp = onLevelUp;
    }

    public void Earn(uint amount)
    {
        _exp += amount;
        uint mod;
        if ((mod = _exp % (_lvl * _bound)) > 0)
        {
            _lvl++;
            _exp = mod;
            _onLevelUp(_lvl);
        }
    }
}

[Serializable]
public class Champion
{
    [SerializeField] private ChampionBehaviour  _behaviour;
    [SerializeField] private Progress           _progress;
    [SerializeField] private ChampionAttributes _attributes;
    [SerializeField] private List<Spell>        _spells;

    public ChampionBehaviour  Behaviour  { get => _behaviour;  }
    public Progress           Progress   { get => _progress;   }
    public ChampionAttributes Attributes { get => _attributes; }
    public List<Spell>        Spells     { get => _spells;     }

    public Champion(ChampionBehaviour behaviour)
    {
        _behaviour  = behaviour;
        _progress   = new(OnLevelUp);
        _attributes = _behaviour.Attributes;
        _spells     = new();

        foreach (var sBehaviour in _behaviour.Spells)
        {
            _spells.Add(new Spell(sBehaviour));
        }
    }

    private void OnLevelUp(uint lvl)
    {
        _attributes.Health = _behaviour.Attributes.Health * (0.1f * (lvl - 1));
        _attributes.Damage = _behaviour.Attributes.Damage * (0.1f * (lvl - 1));
        _attributes.Speed  = _behaviour.Attributes.Speed  * (0.1f * (lvl - 1));
    }
}
