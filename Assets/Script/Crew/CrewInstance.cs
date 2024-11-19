using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class CrewInstance
{
    [SerializeField] private List<ChampionInstance> _instances;

    public CrewInstance(Crew crew)
    {
        _instances = crew.Champions.Select(ch => new ChampionInstance(ch)).ToList();
    }

    public void ForEach(Action<ChampionInstance> cb)
    {
        foreach (ChampionInstance inst in _instances)
        {
            cb(inst);
        }
    }

    public bool IsAlive()
    {
        return _instances.Sum(ch => ch.Health) > 0.0f;
    }
}
