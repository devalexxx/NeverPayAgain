using System.Collections.Generic;

public class ChampionInstance
{
    private Champion _champion;

    private float               _health;
    private List<SpellInstance> _spells;

    public  float Health { get => _health; }

    public ChampionInstance(Champion champion)
    {
        _champion = champion;
        _health   = champion.Attributes.Health;
        _spells   = new();

        foreach (var spell in _champion.Spells)
        {  
            _spells.Add(new SpellInstance(spell));
        }
    }

    public void Heal(uint health)
    {
        // this.health = Math.Min(Champion.Behaviour.Attributes.Health, this.health + health);
    }

    public void Hit(uint damage)
    {
        // health = Math.Min(0, health - damage);
    }
}
