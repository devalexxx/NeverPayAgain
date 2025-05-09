using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// DungeonSpawn represents the spawn points within a dungeon.
[Serializable]
public class DungeonSpawn
{
    [SerializeField] private      Transform  _playerSpawn;  // Transform representing the spawn point of the player in the dungeon
    [SerializeField] private List<Transform> _wavesSpawn;   // List of transforms representing the spawn points of enemy waves in the dungeon

    public      Transform  PlayerSpawn => _playerSpawn;
    public List<Transform> WavesSpawn  => _wavesSpawn;
}

// Dungeon represents the overall structure of a dungeon in the game.
public class Dungeon
{
    private DungeonBehaviour   _behaviour;  // The behaviour of the dungeon, containing wave information, stage count, and other gameplay data
    private List<DungeonStage> _stages;     // A list of dungeon stages that the player will progress through

    public DungeonBehaviour   Behaviour => _behaviour;
    public List<DungeonStage> Stages    => _stages;
    
    public Dungeon(DungeonBehaviour behaviour)
    {
        _behaviour = behaviour;

        // Initialize the stages based on the stage count in the behaviour
        _stages    = Enumerable.Range(0, _behaviour.StagesCount).Select(stage => new DungeonStage(this, stage)).ToList();
    }
}