using System;
using System.Collections;
using System.Linq;

[Serializable]
public class AutoDrivenChampionInstance : ChampionInstance
{

    public AutoDrivenChampionInstance(Champion champion) : base(champion) {}

    public override IEnumerator TakeTurn(CrewInstance allies, CrewInstance enemies)
    {
        SpellInstance inst = _spells.OrderByDescending(s => s.Spell.Behaviour.Cooldown).FirstOrDefault(s => s.TurnSinceEnable == 0);
        if (inst != null)
        {
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

            if (inst.Trigger(this, target, allies, enemies))
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
    }
}
