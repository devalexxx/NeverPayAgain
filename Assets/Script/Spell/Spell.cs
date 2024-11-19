using System;
using UnityEngine;

[Serializable]
public class Spell
{
    [SerializeField] private SpellBehaviour _behaviour;

    public SpellBehaviour Behaviour { get => _behaviour; }

    public Spell(SpellBehaviour behaviour)
    {
        _behaviour = behaviour;
    }

    public bool Trigger(ChampionInstance self, ChampionInstance target, CrewInstance allies, CrewInstance enemies)
    {
        // @TODO: add effect revert in case of fail
        bool hasFailed = false;
        foreach (SpellEffect effect in _behaviour.Effects)
        {
            hasFailed |= effect.Apply(self, target, allies, enemies);
        }
        return hasFailed;
    }
}
