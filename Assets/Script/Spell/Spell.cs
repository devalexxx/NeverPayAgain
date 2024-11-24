using System;
using UnityEngine;

[Serializable]
public class Spell
{
    [SerializeReference] private SpellBehaviour _behaviour;

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

    public void SetTargetState(CrewInstance allies, CrewInstance enemies)
    {
        switch (_behaviour.Target)
        {
            case SpellCrewTarget.Ally:
                allies.ForEach(SetChampionState);
                break;
            case SpellCrewTarget.Enemy:
                enemies.ForEach(SetChampionState);
                break;
        }
    }

    public void ResetTargetState(CrewInstance allies, CrewInstance enemies)
    {
        switch (_behaviour.Target)
        {
            case SpellCrewTarget.Ally:
                allies.ForEach(ResetChampionState);
                break;
            case SpellCrewTarget.Enemy:
                enemies.ForEach(ResetChampionState);
                break;
        }
    }

    private void SetChampionState(ChampionInstance inst)
    {
        inst.State = ChampionInstanceState.Target;
    }

    private void ResetChampionState(ChampionInstance inst)
    {
        if (inst.State == ChampionInstanceState.TurnAndTarget || inst.State == ChampionInstanceState.Turn)
            inst.State = ChampionInstanceState.Turn;
        else
            inst.State = ChampionInstanceState.Idle;   
    }
}
