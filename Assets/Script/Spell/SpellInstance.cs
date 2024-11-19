

public class SpellInstance
{
    private Spell _spell;

    public SpellInstance(Spell spell)
    {
        _spell = spell;
    }

    public bool Trigger(ChampionInstance self, ChampionInstance target, CrewInstance allies, CrewInstance enemies)
    {
        // @TODO: Add turn counter check
        return _spell.Trigger(self, target, allies, enemies);
    }
}
