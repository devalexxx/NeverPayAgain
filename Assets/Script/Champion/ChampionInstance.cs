using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public enum ChampionInstanceState
{
    Idle, Turn, Target, TurnAndTarget
}

[Serializable]
public enum ChampionInstanceDriver
{
    Auto, User
}

[Serializable]
public abstract class ChampionInstance : IEqualityComparer<ChampionInstance>
{
    [SerializeField] private Guid _guid;

    [SerializeReference] protected Champion _champion;

    [SerializeField] protected float                 _health;
    [SerializeField] protected List<SpellInstance>   _spells;
    [SerializeField] protected TurnMeter             _turnMeter;
    [SerializeField] protected ChampionInstanceState _state;
    [SerializeField] protected GameObject            _entity;

    public Champion Champion
    { 
        get => _champion;  
    }

    public float Health 
    { 
        get => _health; 
        set 
        {
            _health = Math.Clamp(value, 0.0f, _champion.Behaviour.Attributes.Health);
            if (!IsAlive())
            {
                _entity.SetActive(false);
            }
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
            if      (_state == ChampionInstanceState.Turn && value == ChampionInstanceState.Target)
                _state = ChampionInstanceState.TurnAndTarget;
            else if (_state == ChampionInstanceState.Target && value == ChampionInstanceState.Turn)
                _state = ChampionInstanceState.TurnAndTarget;
            else
                _state = value;
        }
    }

    public GameObject Entity
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

    public abstract ChampionInstanceDriver GetDriver();
    public abstract IEnumerator            TakeTurn (CrewInstance allies, CrewInstance enemies);

    public void Summon(Transform parent, Vector3 offset)
    {
        parent.gameObject.SetActive(false);
        _entity = GameObject.Instantiate(Champion.Behaviour.Entity, parent);
        if (_entity.transform.Find("Entity").TryGetComponent<Renderer>(out var renderer))
        {
            _entity.transform.position += new Vector3(0, renderer.bounds.extents.y, 0);
        }
        _entity.transform.position += offset;
        _entity.GetOrAddComponent<ChampionEntity>().Instance = this;
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
