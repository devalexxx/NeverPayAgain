using System;

[Serializable]
public abstract class SpellEffect
{
    public abstract bool Apply(ChampionInstance self, ChampionInstance target, CrewInstance allies, CrewInstance enemies);
}