using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityToolkit;

// Class representing a Champion, with computed attributes, progress, and behavior (e.g. A champion that belongs to a player or dungeon)
[Serializable]
public class Champion : IEquatable<Champion>, IJsonSerializable
{
    [SerializeField] private AssetReference<ChampionBehaviour> _behaviourReference;    // References the champion's behavior and base configuration

    // Unique identifier to ease comparison
    [SerializeField] private GUID               _guid;
    [SerializeField] private ChampionProgress   _progress;      // Tracks the champion's progression (e.g., level, rank)
    [SerializeField] private ChampionAttributes _attributes;    // Stores the current attributes of the champion (adjusted by level or other factors)
    [SerializeField] private List<Spell>        _spells;        // List of spells available to the champion

    // Public accessors for private fields
    public ChampionBehaviour  Behaviour  { get => _behaviourReference.handle;  }
    public ChampionProgress   Progress   { get => _progress;   }
    public ChampionAttributes Attributes { get => _attributes; }
    public List<Spell>        Spells     { get => _spells;     }

    public Champion(ChampionBehaviour behaviour, uint level)
    {
        _behaviourReference = new(behaviour);

        _guid       = Guid.NewGuid();
        _attributes = Behaviour.Attributes;
        _progress   = new(level);
        _spells     = Behaviour.Spells.Select(spell => new Spell(spell)).ToList();

        _progress.onLevelUp.AddListener(_ => ComputeAttributes());
        ComputeAttributes();
    }

    public Champion(ChampionBehaviour behaviour) : this(behaviour, 1) {}

    // Computes the champion's attributes based on its level and base values
    private void ComputeAttributes()
    {
        // Increase attributes by 10% per level above 1 (it's a wildcard, not definitive %)
        _attributes.Health = Behaviour.Attributes.Health * (1 + (0.1f * (Progress.Level - 1)));
        _attributes.Damage = Behaviour.Attributes.Damage * (1 + (0.1f * (Progress.Level - 1)));
        _attributes.Speed  = Behaviour.Attributes.Speed  * (1 + (0.1f * (Progress.Level - 1)));
    }

    public bool Equals(Champion other)
    {
        return _guid == other._guid;
    }
}