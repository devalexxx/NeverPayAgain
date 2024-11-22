using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public abstract class ChampionInstance : IEqualityComparer<ChampionInstance>
{
    [SerializeField] private Guid _guid;

    [SerializeField] private Champion _champion;

    [SerializeField] protected float               _health;
    [SerializeField] protected List<SpellInstance> _spells;
    [SerializeField] protected TurnMeter           _turnMeter;

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

    public abstract IEnumerator TakeTurn(CrewInstance allies, CrewInstance enemies);

    public bool IsAlive()
    {
        return _health > 0;
    }

    public bool CanTakeTurn()
    {
        return _turnMeter.IsFilled();
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
