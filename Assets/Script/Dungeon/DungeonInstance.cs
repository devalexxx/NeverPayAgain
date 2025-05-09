using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

// Enum representing the possible states of a dungeon instance.
public enum DungeonInstanceState
{
    Starting, InProgress, Ended
}

// Class responsible for managing the state and progression of a dungeon encounter.
public class DungeonInstance
{
    private DungeonInstanceState _state;
    private DungeonStage         _stage;  // Current dungeon stage.
    private DungeonSpawn         _spawns; // The spawn points for the player and enemy waves in the dungeon
    private CrewInstance         _player; // Player crew instance.
    private List<CrewInstance>   _waves;  // Enemy waves in the current stage.

    // The current combat instance between the player and enemy.
    private TurnBasedCombat _currentCombat;

    private PlayerEntity _playerEntity; // Entity representing the player crew in the game.
    private GameObject   _entity;       // Game object representing the entire dungeon instance.

    // Unity event triggered when the dungeon instance ends.
    public UnityEvent onEnded = new();

    public DungeonInstance(DungeonStage stage, DungeonSpawn spawn, Crew player)
    {
        _state = DungeonInstanceState.Starting;
        _stage  = stage;
        _spawns = spawn;
        _player = new(player, false);
        _waves  = stage.Waves.Select(wave => new CrewInstance(wave)).ToList();
    }

    // Starts the dungeon instance, summoning the player and waves, and progressing through the combat phases.
    public IEnumerator Start()
    {
        Summon();

        // Continue the dungeon while the player and at least one enemy wave are still alive.
        _state = DungeonInstanceState.InProgress;
        while (_player.IsAlive() && _waves.Any(wave => wave.IsAlive()))
        {
            // If there is an ongoing combat, progress the combat.
            if (_currentCombat != null)
            {
                yield return _currentCombat.Start();            // Start the combat
                if (_currentCombat.HasWon(_player, out var looser))
                {
                    looser.Entity.SetActive(false);             // Deactivate the loser entity after combat if enemy.
                }
                _currentCombat = null;                          // Reset current combat.
            }
            else
            {
                _playerEntity.IsMoving = true;                  // If no combat is ongoing, allow player movement to reach a wave.
            }
            yield return null;
        }
        _state = DungeonInstanceState.Ended;

        Destroy();
        onEnded.Invoke();
    }

    public bool IsWon()
    {
        return _state == DungeonInstanceState.Ended && _player.IsAlive();
    }

    // Handles collision events when the player's crew collides with an enemy crew.
    private void OnCrewCollide(Collider collider)
    {
        if (_currentCombat == null || _currentCombat.State == CombatState.Ended)
        {
            var crew = _waves.Find(wave => wave.Guid == Guid.Parse(collider.gameObject.name));
            if (crew != null)
            {
                _currentCombat = new(_player, crew);
                _playerEntity.IsMoving = false;
            }
        }
    }

    // Summons the player and waves, placing them in the dungeon at the appropriate locations.
    private void Summon()
    {
        _entity = new("DungeonInstance");

        GameObject go;

        // Summon player and add necessary components.
        go = _player.Summon(_entity.transform, _spawns.PlayerSpawn.position);
        _playerEntity = go.AddComponent<PlayerEntity>();
        go.AddComponent<OnTriggerEnterEvent>().onTriggerEnter.AddListener(OnCrewCollide);

        // Summon enemy waves and add necessary components.
        for (int i = 0; i < _waves.Count; i++)
        {
            go = _waves[i].Summon(_entity.transform, _spawns.WavesSpawn[i].position);
            go.AddComponent<WaveEntity>();
            go.transform.Rotate(new Vector3(0, 180, 0));
        }

        _entity.SetActive(false);
        _entity.AddComponent<DungeonEntity>().Instance = this;
        _entity.SetActive(true);
    }

    // Destroys the dungeon instance's game object and any associated components.
    private void Destroy()
    {
        if (_entity)
        {
            GameObject.Destroy(_entity);
        }
    }
}
