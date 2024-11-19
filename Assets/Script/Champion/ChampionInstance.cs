using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class ChampionInstance
{
    [SerializeField] private Champion _champion;

    [SerializeField] private float               _health;
    [SerializeField] private List<SpellInstance> _spells;
    [SerializeField] private TurnMeter           _turnMeter;

    public float Health 
    { 
        get => _health; 
        set => _health = Math.Min(_champion.Behaviour.Attributes.Health, _health + value);
    }

    public TurnMeter TurnMeter { get => _turnMeter; }
    public Champion  Champion  { get => _champion;  }

    public ChampionInstance(Champion champion)
    {
        _champion  = champion;
        _health    = champion.Attributes.Health;
        _spells    = champion.Spells.Select(s => new SpellInstance(s)).ToList();
        _turnMeter = new();
    }

    public bool CanTakeTurn()
    {
        return _turnMeter.IsFilled();
    }

    public bool TakeTurn()
    {
        // @Todo: Select spell and return if no errors
        _turnMeter.Consume();
        return true;
    }
}
