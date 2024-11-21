using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class ChampionInstance : IEqualityComparer<ChampionInstance>
{
    [SerializeField] private Guid _guid;

    [SerializeField] private Champion _champion;

    [SerializeField] private float               _health;
    [SerializeField] private List<SpellInstance> _spells;
    [SerializeField] private TurnMeter           _turnMeter;

    public float Health 
    { 
        get => _health; 
        set => _health = Math.Clamp(value, 0.0f, _champion.Behaviour.Attributes.Health);
    }

    public TurnMeter TurnMeter 
    { 
        get => _turnMeter; 
    }

    public Champion Champion
    { 
        get => _champion;  
    }

    public ChampionInstance(Champion champion)
    {
        _guid      = Guid.NewGuid();
        _champion  = champion;
        _health    = champion.Attributes.Health;
        _spells    = champion.Spells.Select(s => new SpellInstance(s)).ToList();
        _turnMeter = new();
    }

    public bool CanTakeTurn()
    {
        return _turnMeter.IsFilled();
    }

    public bool TakeTurn(ChampionInstance target, CrewInstance allies, CrewInstance enemies)
    {
        // @Todo: Select spell
        if (_spells[0].Trigger(this, target, allies, enemies))
        {
            _turnMeter.Consume();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Advance(float delta)
    {
        _turnMeter.Advance(_champion.Attributes.Speed * delta);
    }

    public bool Equals(ChampionInstance x, ChampionInstance y)
    {
        return x._guid == y._guid;
    }

    public int GetHashCode(ChampionInstance obj)
    {
        return obj._guid.GetHashCode();
    }
}
