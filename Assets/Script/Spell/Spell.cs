using System;
using System.Collections;
using UnityEngine;

// Represents a spell with a specific behavior and effects applied to targets.
[Serializable]
public class Spell : IJsonSerializable
{
    // The behavior of the spell, which defines its effects.
    [SerializeField] private AssetReference<SpellBehaviour> _behaviourReference;

    public SpellBehaviour Behaviour => _behaviourReference.handle;

    public Spell(SpellBehaviour behaviour)
    {
        _behaviourReference = new(behaviour);
    }

    // Triggers the spell, applying its effects to the given self and target with specified allies and enemies.
    public IEnumerator Trigger(ChampionInstance self, ChampionInstance target, CrewInstance allies, CrewInstance enemies)
    {
        // @TODO: add effect revert in case of fail
        bool hasFailed = false;
        foreach (SpellEffect effect in Behaviour.Effects)
        {
            yield return CoroutineUtils.Run<bool>(effect.Apply(self, target, allies, enemies), res => hasFailed |= res);
        }

        yield return hasFailed;
    }

    // Sets the target state for the affected crew (either allies or enemies based on the spell's target).
    public void SetTargetState(CrewInstance allies, CrewInstance enemies)
    {
        switch (Behaviour.Target)
        {
            case SpellCrewTarget.Ally:
                allies.ForEach(SetChampionState);
                break;
            case SpellCrewTarget.Enemy:
                enemies.ForEach(SetChampionState);
                break;
        }
    }

    // Resets the target state for the affected crew (either allies or enemies).
    public void ResetTargetState(CrewInstance allies, CrewInstance enemies)
    {
        switch (Behaviour.Target)
        {
            case SpellCrewTarget.Ally:
                allies.ForEach(ResetChampionState);
                break;
            case SpellCrewTarget.Enemy:
                enemies.ForEach(ResetChampionState);
                break;
        }
    }

    // Sets the champion state to 'Target', typically when the spell is targeting them.
    private void SetChampionState(ChampionInstance inst)
    {
        inst.State = ChampionInstanceState.Target;
    }

    // Resets the champion state, either back to 'Turn' or 'Idle' based on their previous state.
    private void ResetChampionState(ChampionInstance inst)
    {
        if (inst.State == ChampionInstanceState.TurnAndTarget || inst.State == ChampionInstanceState.Turn)
            inst.State = ChampionInstanceState.Turn;
        else
            inst.State = ChampionInstanceState.Idle;   
    }
}
