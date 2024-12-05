using System;
using System.Collections;
using UnityEngine;

[Serializable]
public abstract class HealEffect : SpellEffect
{
    [Range(0.0f, 1.0f)]
    [SerializeField] protected float _percent;
}

[Serializable]
public class HealSelf : HealEffect
{
    public override IEnumerator Apply(ChampionInstance self, ChampionInstance target, CrewInstance allies, CrewInstance enemies)
    {
        self.Health += self.Champion.Attributes.Health * _percent;
        self.Entity.NotifyHealing();
        yield return true;
    }
}

[Serializable]
public class HealTarget : HealEffect
{
    public override IEnumerator Apply(ChampionInstance self, ChampionInstance target, CrewInstance allies, CrewInstance enemies)
    {
        // @TODO: Check if target is in fromCrew, else do nothing and return false
        if (allies.Contains(target))
        {
            target.Health += target.Champion.Attributes.Health * _percent;
            target.Entity.NotifyHealing();
        }
        else
        {
            ChampionInstance ally = allies.PickRandom();
            while (ally == self)
            {
                ally = allies.PickRandom();
            }
            ally.Health += ally.Champion.Attributes.Health * _percent;
            ally.Entity.NotifyHealing();
        }
        yield return true;
    }
}

public class HealAll : HealEffect
{
    public override IEnumerator Apply(ChampionInstance self, ChampionInstance target, CrewInstance allies, CrewInstance enemies)
    {
        allies.ForEach(ch => { if (ch.IsAlive()) { ch.Health += ch.Champion.Attributes.Health * _percent; ch.Entity.NotifyHealing(); } });
        yield return true;
    }
}
