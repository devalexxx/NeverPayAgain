using System;
using System.Collections;

[Serializable]
public abstract class SpellEffect
{
    public abstract IEnumerator Apply(ChampionInstance self, ChampionInstance target, CrewInstance allies, CrewInstance enemies);
}