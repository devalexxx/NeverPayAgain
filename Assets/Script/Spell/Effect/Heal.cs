using System;
using System.Collections;
using UnityEngine;

// Abstract class that represents a healing effect applied to champions.
[Serializable]
public abstract class HealEffect : SpellEffect
{
    [Range(0.0f, 1.0f)]
    [SerializeField] protected float _percent;
}

// A healing effect that heals the caster (self).
[Serializable]
public class HealSelf : HealEffect
{
    //  Heals the caster by a percentage of their max health.
    public override IEnumerator Apply(ChampionInstance self, ChampionInstance target, CrewInstance allies, CrewInstance enemies)
    {
        self.Health += self.Champion.Attributes.Health * _percent;
        self.Entity.NotifyHealing();
        yield return true;
    }
}

// A healing effect that heals a target champion.
[Serializable]
public class HealTarget : HealEffect
{
    //  Heals the target if it's an ally. If not, heals a random ally.
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

// A healing effect that heals all allies in the crew.
public class HealAll : HealEffect
{
    // Heals all living champions in the crew by a percentage of their max health.
    public override IEnumerator Apply(ChampionInstance self, ChampionInstance target, CrewInstance allies, CrewInstance enemies)
    {
        allies.ForEach(ch => { if (ch.IsAlive()) { ch.Health += ch.Champion.Attributes.Health * _percent; ch.Entity.NotifyHealing(); } });
        yield return true;
    }
}
