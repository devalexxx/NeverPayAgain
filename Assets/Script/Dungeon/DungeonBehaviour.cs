using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ChampionDescriptor
{
    [SerializeField] private ChampionBehaviour _behaviour;
    [SerializeField] private uint              _defaultLevel;

    public ChampionBehaviour Behaviour    => _behaviour;
    public uint              DefaultLevel => _defaultLevel;
}

[Serializable]
public class WaveDescriptor
{
    [SerializeField] private List<ChampionDescriptor> _championDescriptors;
    [SerializeField] private uint                     _levelIncrement;

    public List<ChampionDescriptor> ChampionDescriptors => _championDescriptors;
    public uint                     LevelIncrement      => _levelIncrement;
}

[CreateAssetMenu]
public class DungeonBehaviour : ScriptableObject
{
    [SerializeField] private List<WaveDescriptor> _waveDescriptors;
    [SerializeField] private int                  _stagesCount;
    [SerializeField] private int                  _expEarning;
    [SerializeField] private float                _expEarningFactor;

    public List<WaveDescriptor> WaveDescriptors => _waveDescriptors;
    public int                  StagesCount     => _stagesCount;
}
