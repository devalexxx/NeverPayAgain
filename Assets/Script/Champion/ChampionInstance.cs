using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

// Enum representing the possible states of a ChampionInstance
[Serializable]
public enum ChampionInstanceState
{
    Idle, Turn, Target, TurnAndTarget
}

// Enum representing the driver controlling the ChampionInstance
[Serializable]
public enum ChampionInstanceDriver
{
    Auto, User
}

// Abstract class representing an instance of a Champion in the game (@see AutoDrivenChampionInstance, @see UserDrivenChampionInstance)
[Serializable]
public abstract class ChampionInstance : IEqualityComparer<ChampionInstance>
{
    // Unique identifier to ease comparison
    [SerializeField] private Guid _guid;

    // The Champion this instance represents
    [SerializeReference] protected Champion _champion;

    [SerializeField] protected float                 _health;       // Current health of the instance
    [SerializeField] protected List<SpellInstance>   _spells;       // Instances of spells available to the champion
    [SerializeField] protected TurnMeter             _turnMeter;    // Tracks turn readiness
    [SerializeField] protected ChampionInstanceState _state;        // Current state of the champion
    [SerializeField] protected ChampionEntity        _entity;       // Associated in-game entity

    public Champion Champion
    { 
        get => _champion;  
    }

    public float Health 
    { 
        get => _health; 
        set 
        {
            // Clamp health between 0 and the champion's max health
            _health = Math.Clamp(value, 0.0f, _champion.Attributes.Health);
        }
    }

    public List<SpellInstance> Spells
    {
        get => _spells;
    }

    public TurnMeter TurnMeter 
    { 
        get => _turnMeter; 
    }

    public ChampionInstanceState State
    {
        get => _state;
        set
        {
            // Combine states if transitioning between Turn and Target
            if      (_state == ChampionInstanceState.Turn && value == ChampionInstanceState.Target)
                _state = ChampionInstanceState.TurnAndTarget;
            else if (_state == ChampionInstanceState.Target && value == ChampionInstanceState.Turn)
                _state = ChampionInstanceState.TurnAndTarget;
            else
                _state = value;
        }
    }

    public ChampionEntity Entity
    {
        get => _entity;
    }

    public ChampionInstance(Champion champion)
    {
        _guid      = Guid.NewGuid();
        _champion  = champion;
        _health    = champion.Attributes.Health;
        _spells    = champion.Spells.Select(s => new SpellInstance(s)).ToList();
        _turnMeter = new();
        _state     = ChampionInstanceState.Idle;
    }

    // Abstract method to determine the driver (AI or user) for this instance
    public abstract ChampionInstanceDriver GetDriver();

    // Abstract method to execute the champion's turn
    public abstract IEnumerator TakeTurn (CrewInstance allies, CrewInstance enemies);

    // Summons the champion into the game by creating its in-game entity
    public void Summon(Transform parent, Vector3 offset)
    {
        parent.gameObject.SetActive(false);
        GameObject go = GameObject.Instantiate(Champion.Behaviour.Entity, parent);
        go.transform.position += offset;
        _entity = go.GetOrAddComponent<ChampionEntity>();
        _entity.Instance = this;
        parent.gameObject.SetActive(true);
    }

    public bool IsAlive()
    {
        return _health > 0;
    }

    public bool CanTakeTurn()
    {
        return IsAlive() && _turnMeter.IsFilled();
    }

    // Advances the turn meter based on the champion's speed and frame time
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
