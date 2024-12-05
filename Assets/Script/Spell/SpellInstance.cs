using System;
using System.Collections;
using UnityEngine;

// Represents an instance of a spell with its cooldown and trigger functionality.
[Serializable]
public class SpellInstance
{
    [SerializeReference] private Spell _spell;              // The spell associated with this instance.
    [SerializeField]     private uint  _turnSinceEnable;    // Tracks how many turns since the spell will be enabled.

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

    // Triggers the spell, checking cooldown and applying the spell effect.
    public IEnumerator Trigger(ChampionInstance self, ChampionInstance target, CrewInstance allies, CrewInstance enemies)
    {
        // If the spell is ready to be triggered (cooldown is 0), apply it.
        if (_turnSinceEnable == 0)
        {
            _turnSinceEnable = _spell.Behaviour.Cooldown;
            bool hasSucceed = false;
            yield return CoroutineUtils.Run<bool>(_spell.Trigger(self, target, allies, enemies), res => hasSucceed = res);
            yield return hasSucceed;
        }
        else
        {
            yield return false;
        }
    }

     // Called every turn to decrease the cooldown timer for the spell.
    public void OnTurn()
    {
        if (_turnSinceEnable > 0)
            _turnSinceEnable -= 1;
    }
}
