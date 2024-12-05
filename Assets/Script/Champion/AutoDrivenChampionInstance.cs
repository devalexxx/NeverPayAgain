using System;
using System.Collections;
using System.Linq;
using UnityEngine;

// Represents a champion instance controlled by the AI.
[Serializable]
public class AutoDrivenChampionInstance : ChampionInstance
{

    public AutoDrivenChampionInstance(Champion champion) : base(champion) {}

    // Returns the driver type, indicating that this instance is AI-controlled.
    public override ChampionInstanceDriver GetDriver()
    {
        return ChampionInstanceDriver.Auto;
    }

    // Defines the behavior for taking a turn in combat.
    public override IEnumerator TakeTurn(CrewInstance allies, CrewInstance enemies)
    {
        // Set the champion's state to indicate it's their turn.
        _state = ChampionInstanceState.Turn;

        // Select the highest priority spell that is ready to use (cooldown is 0).
        SpellInstance inst = _spells.OrderByDescending(s => s.Spell.Behaviour.Cooldown).FirstOrDefault(s => s.TurnSinceEnable == 0);


        if (inst != null)
        {
            // Introduce a slight delay before executing actions for a more natural feel.
            yield return new WaitForSeconds(0.3f);

            // @TODO: add max iteration
            // Select a valid target for the spell based on its targeting behavior.
            ChampionInstance target;
            do 
            {
                target = inst.Spell.Behaviour.Target switch
                {
                    SpellCrewTarget.Ally  => allies .PickRandom(),
                    SpellCrewTarget.Enemy => enemies.PickRandom(),
                    _ => throw new ArgumentOutOfRangeException("Non valid SpellCrewTarget"),
                };
            } 
            while(!target.IsAlive());   // Ensure the selected target is alive.

            // Attempt to trigger the spell and capture whether it succeeded.
            bool hasSpellTriggerSucceed = false;
            yield return CoroutineUtils.Run<bool>(inst.Trigger(this, target, allies, enemies), res => hasSpellTriggerSucceed = res);

            // If the spell successfully triggers, consume the turn meter and advance all spell states (e.g. decrease cooldown)
            if (hasSpellTriggerSucceed)
            {
                _turnMeter.Consume();
                _spells.ForEach(s => s.OnTurn());
                yield return true;
            }
            else
            {
                yield return false;
            }
        }
        else
        {
            yield return false;
        }

        // Set the champion's state back to idle after completing the turn.
        _state = ChampionInstanceState.Idle;
    }
}
