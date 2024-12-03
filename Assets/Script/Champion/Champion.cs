using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[Serializable]
public class Champion : IEquatable<Champion>
{
    private Guid _guid;

    [SerializeReference] private ChampionBehaviour  _behaviour;
    [SerializeField]     private ChampionProgress   _progress;
    [SerializeField]     private ChampionAttributes _attributes;
    [SerializeField]     private List<Spell>        _spells;

    public ChampionBehaviour  Behaviour  { get => _behaviour;  }
    public ChampionProgress   Progress   { get => _progress;   }
    public ChampionAttributes Attributes { get => _attributes; }
    public List<Spell>        Spells     { get => _spells;     }

    public Champion(ChampionBehaviour behaviour, uint level)
    {
        _guid       = Guid.NewGuid();
        _behaviour  = behaviour;
        _attributes = _behaviour.Attributes;
        _progress   = new(level, ComputeAttributes);
        _spells     = _behaviour.Spells.Select(spell => new Spell(spell)).ToList();
        ComputeAttributes();
    }

    public Champion(ChampionBehaviour behaviour) : this(behaviour, 1) {}

    private void ComputeAttributes()
    {
        _attributes.Health = _behaviour.Attributes.Health * (1 + (0.1f * (Progress.Level - 1)));
        _attributes.Damage = _behaviour.Attributes.Damage * (1 + (0.1f * (Progress.Level - 1)));
        _attributes.Speed  = _behaviour.Attributes.Speed  * (1 + (0.1f * (Progress.Level - 1)));
    }

    public bool Equals(Champion other)
    {
        return _guid == other._guid;
    }
}