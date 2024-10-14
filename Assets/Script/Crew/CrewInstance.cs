using System;
using System.Collections.Generic;

public class CrewInstance
{
    private List<ChampionInstance> _champions;

    public CrewInstance(Crew crew)
    {
        _champions = new();

        foreach (var champion in crew.Champions)
        {
            _champions.Add(new ChampionInstance(champion));
        }
    }

    // public void Heal(float hFactor)
    // {
    //     foreach (var inst in championsInstance)
    //     {
    //         inst.Heal((uint)Math.Round(inst.Health * hFactor));
    //     }
    // }

    // public void Hit(float dFactor, uint baseDamage)
    // {
    //     foreach (var inst in championsInstance)
    //     {
    //         inst.Hit((uint)Math.Round(dFactor * baseDamage));
    //     }
    // }
}
