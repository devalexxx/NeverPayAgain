using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ChampionDescriptor represents a descriptor for a champion, which includes the champion's behaviour and default level.
[Serializable]
public class ChampionDescriptor
{
    [SerializeField] private ChampionBehaviour _behaviour;      // Behaviour associated with the champion
    [SerializeField] private uint              _defaultLevel;   // The default level of the champion

    public ChampionBehaviour Behaviour    => _behaviour;
    public uint              DefaultLevel => _defaultLevel;
}

// WaveDescriptor represents the description of a wave in a dungeon encounter.
[Serializable]
public class WaveDescriptor
{
    [SerializeField] private List<ChampionDescriptor> _championDescriptors; // List of champion descriptors that define the champions in the wave
    [SerializeField] private uint                     _levelIncrement;      // The level increment applied to the champions in this wave

    public List<ChampionDescriptor> ChampionDescriptors => _championDescriptors;
    public uint                     LevelIncrement      => _levelIncrement;
}

// DungeonBehaviour represents the overall behaviour of a dungeon, which consists of multiple waves, stage count and further more.
[CreateAssetMenu]
public class DungeonBehaviour : ScriptableObject
{
    [SerializeField] private string _displayName;        // The display name
    [SerializeField] private string _displayDescription; // The display description
    [SerializeField] private Sprite _displayIcon;        // The display icon

    [SerializeField] private List<WaveDescriptor> _waveDescriptors; // List of wave descriptors, defining the waves in the dungeon
    [SerializeField] private int                  _stagesCount;     // The number of stages in the dungeon
    [SerializeField] private DungeonRewards       _rewards;         // The rewards gives by dungeon win
    [SerializeField] private string               _relatedScene;    // The scene to load to start this dungeon

    public string DisplayName        => _displayName;
    public string DisplayDescription => _displayDescription;
    public Sprite DisplayIcon        => _displayIcon;

    public List<WaveDescriptor> WaveDescriptors => _waveDescriptors;
    public int                  StagesCount     => _stagesCount;
    public DungeonRewards       Rewards         => _rewards;
    public string               RelatedScene    => _relatedScene;
}
