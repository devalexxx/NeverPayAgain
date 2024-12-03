using System;
using System.Collections;
using System.Linq;
using UnityEngine;

[Serializable]
public class AutoDrivenChampionInstance : ChampionInstance
{

    public AutoDrivenChampionInstance(Champion champion) : base(champion) {}

    public override ChampionInstanceDriver GetDriver()
    {
        return ChampionInstanceDriver.Auto;
    }

    public override IEnumerator TakeTurn(CrewInstance allies, CrewInstance enemies)
    {
        _state = ChampionInstanceState.Turn;
        SpellInstance inst = _spells.OrderByDescending(s => s.Spell.Behaviour.Cooldown).FirstOrDefault(s => s.TurnSinceEnable == 0);
        if (inst != null)
        {
            yield return new WaitForSeconds(0.3f);
            // @TODO: add max iteration
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
            while(!target.IsAlive());

            bool hasSpellTriggerSucceed = false;
            yield return CoroutineUtils.Run<bool>(inst.Trigger(this, target, allies, enemies), res => hasSpellTriggerSucceed = res);

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
        _state = ChampionInstanceState.Idle;
    }
}
