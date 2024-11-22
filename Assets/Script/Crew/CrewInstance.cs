using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine;

[Serializable]
public class CrewInstance : IEqualityComparer<CrewInstance>
{
    [SerializeField] private Guid _guid;

    [SerializeReference]
    [SerializeField] private List<ChampionInstance> _instances;

    public CrewInstance(Crew crew, bool auto = true)
    {
        _guid      = Guid.NewGuid();
        if (auto)
        {
            _instances = crew.Champions.Select(ch => new AutoDrivenChampionInstance(ch) as ChampionInstance).ToList();
        }
        else
        {
            _instances = crew.Champions.Select(ch => new UserDrivenChampionInstance(ch) as ChampionInstance).ToList();
        }
    }

    public void ForEach(Action<ChampionInstance> cb)
    {
        foreach (ChampionInstance inst in _instances)
        {
            cb(inst);
        }
    }

    public ChampionInstance PickRandom()
    {
        return _instances[RandomNumberGenerator.GetInt32(0, _instances.Count)];
    }

    public bool IsAlive()
    {
        return _instances.Any(inst => inst.IsAlive());
    }

    public bool Contains(ChampionInstance inst)
    {
        return _instances.Contains(inst);
    }

    public bool Equals(CrewInstance x, CrewInstance y)
    {
        return x._guid == y._guid;
    }

    public int GetHashCode(CrewInstance obj)
    {
        return obj._guid.GetHashCode();
    }
}
