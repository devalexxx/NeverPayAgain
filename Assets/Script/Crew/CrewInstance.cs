using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;

// CrewInstance represents an instance of a crew in the game, consisting of a set of ChampionInstances.
[Serializable]
public class CrewInstance : IEqualityComparer<CrewInstance>
{
    // Unique identifier for the crew instance
    [SerializeField] private Guid _guid;

    // List of champion instances in the crew
    [SerializeReference] private List<ChampionInstance> _instances;

    // GameObject representing the crew in the game world
    private GameObject _entity;

    public GameObject Entity => _entity;

    public Guid Guid
    {
        get => _guid;
    }

    public CrewInstance(Crew crew, bool auto = true)
    {
        _guid      = Guid.NewGuid();
        if (auto)
        {
            // If auto, create AutoDrivenChampionInstances for the crew's champions
            _instances = crew.Champions.Select(ch => new AutoDrivenChampionInstance(ch) as ChampionInstance).ToList();
        }
        else
        {
            // If not auto, create UserDrivenChampionInstances for the crew's champions
            _instances = crew.Champions.Select(ch => new UserDrivenChampionInstance(ch) as ChampionInstance).ToList();
        }
    }

    // Method to apply a given action to each champion instance in the crew
    public void ForEach(Action<ChampionInstance> cb)
    {
        foreach (ChampionInstance inst in _instances)
        {
            cb(inst);
        }
    }

    // Method to summon the crew into the game world, at a specific location and with an offset
    public GameObject Summon(Transform transform, Vector3 offset)
    {
        _entity = new GameObject(_guid.ToString());
        _entity.transform.parent    = transform;
        _entity.transform.position += offset;

        // Arrange champions along the X-axis
        int n = -(_instances.Count / 2);
        ForEach(inst => inst.Summon(_entity.transform, new(n++ * 3.0f, 0, 0)));

        return _entity;
    }

    // Method to pick a random champion instance from the crew
    public ChampionInstance PickRandom()
    {
        return _instances[RandomNumberGenerator.GetInt32(0, _instances.Count)];
    }

    // Method to check if any of the champion instances in the crew are alive
    public bool IsAlive()
    {
        return _instances.Any(inst => inst.IsAlive());
    }

    // Method to check if a specific champion instance is part of the crew
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
