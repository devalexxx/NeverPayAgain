using System;
using UnityEngine;

[Serializable]
public abstract class DamageEffect : SpellEffect
{
    [Range(1.0f, 10.0f)]
    [SerializeField] protected float _percent;
}

public class DamageTarget : DamageEffect
{
    public override bool Apply(ChampionInstance self, ChampionInstance target, CrewInstance allies, CrewInstance enemies)
    {
        target.Health -= self.Champion.Attributes.Damage * _percent;
        return true;
    }
}

public class DamageAll : DamageEffect
{
    public override bool Apply(ChampionInstance self, ChampionInstance target, CrewInstance allies, CrewInstance enemies)
    {
        enemies.ForEach(ch => { ch.Health -= self.Champion.Attributes.Damage * _percent; });
        return true;
    }
}
