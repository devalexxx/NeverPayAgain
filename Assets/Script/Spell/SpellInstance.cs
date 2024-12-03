using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class SpellInstance
{
    [SerializeReference] private Spell _spell;
    [SerializeField]     private uint  _turnSinceEnable;

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

    public IEnumerator Trigger(ChampionInstance self, ChampionInstance target, CrewInstance allies, CrewInstance enemies)
    {
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

    public void OnTurn()
    {
        if (_turnSinceEnable > 0)
            _turnSinceEnable -= 1;
    }
}
