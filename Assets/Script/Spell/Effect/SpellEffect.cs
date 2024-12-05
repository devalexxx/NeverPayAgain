using System;
using System.Collections;

// Abstract base class representing a spell effect that can be applied in combat.
[Serializable]
public abstract class SpellEffect
{
    public abstract IEnumerator Apply(ChampionInstance self, ChampionInstance target, CrewInstance allies, CrewInstance enemies);
}