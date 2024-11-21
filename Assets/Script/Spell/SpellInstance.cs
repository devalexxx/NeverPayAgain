using System;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class SpellInstance
{
    [SerializeField] private Spell _spell;
    [SerializeField] private uint  _turnSinceEnable;

    public Spell Spell
    {
        get => _spell;
    }

    public uint TurnSinceEnable
    {
        get => _turnSinceEnable;
    }

    public SpellInstance(Spell spell)
    {
        _spell = spell;
        _turnSinceEnable = 0;
    }

    public bool Trigger(ChampionInstance self, ChampionInstance target, CrewInstance allies, CrewInstance enemies)
    {
        if (_turnSinceEnable == 0)
        {
            _turnSinceEnable = _spell.Behaviour.Cooldown;
            return _spell.Trigger(self, target, allies, enemies);
        }
        else
        {
            return false;
        }
    }

    public void OnTurn()
    {
        if (_turnSinceEnable > 0)
            _turnSinceEnable -= 1   ;
    }
}
