using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class DungeonSpawn
{
    [SerializeField] private      Transform  _playerSpawn;
    [SerializeField] private List<Transform> _wavesSpawn;

    public      Transform  PlayerSpawn => _playerSpawn;
    public List<Transform> WavesSpawn  => _wavesSpawn;
}

public class Dungeon
{
    private DungeonBehaviour   _behaviour;
    private DungeonSpawn       _spawns;
    private List<DungeonStage> _stages;

    public DungeonBehaviour   Behaviour => _behaviour;
    public DungeonSpawn       Spawns    => _spawns;
    public List<DungeonStage> Stages    => _stages;
    
    public Dungeon(DungeonBehaviour behaviour, DungeonSpawn spawn)
    {
        _behaviour = behaviour;
        _spawns    = spawn;
        _stages    = Enumerable.Range(0, _behaviour.StagesCount).Select(stage => new DungeonStage(this, stage)).ToList();
    }
}