using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public enum DungeonInstanceState
{
    Starting, InProgress, Ended
}

public class DungeonInstance
{
    private DungeonStage       _stage;
    private CrewInstance       _player;
    private List<CrewInstance> _waves;

    private TurnBasedCombat _currentCombat;

    private GameObject _playerEntity;
    private GameObject _entity;

    public UnityEvent onEnded = new();
    public GameObject PlayerEntity => _playerEntity;

    public DungeonInstance(DungeonStage stage, Crew player)
    {
        _stage  = stage;
        _player = new(player, false);
        _waves  = stage.Waves.Select(wave => new CrewInstance(wave)).ToList();
    }

    public IEnumerator Start()
    {
        Summon();
        while (_player.IsAlive() && _waves.Any(wave => wave.IsAlive()))
        {
            if (_currentCombat != null)
            {
                yield return _currentCombat.Start();
                _currentCombat.Looser.Entity.SetActive(false);
                _currentCombat = null;
            }
            else
            {
                _playerEntity.transform.Translate(5.0f * Time.deltaTime * Vector3.forward);
            }
            yield return null;
        }
        Destroy();
        onEnded.Invoke();
    }

    private void OnCrewCollide(Collider collider)
    {
        if (_currentCombat == null || _currentCombat.State == CombatState.Ended)
        {
            var crew = _waves.Find(wave => wave.Guid == Guid.Parse(collider.gameObject.name));
            if (crew != null)
            {
                _currentCombat = new(_player, crew);
            }
        }
    }

    private void Summon()
    {
        _entity = new("DungeonInstance");

        // Summon player
        _playerEntity = _player.Summon(_entity.transform, _stage.Dungeon.Spawns.PlayerSpawn.position);
        _playerEntity.AddComponent<PlayerEntity>();
        _playerEntity.AddComponent<OnTriggerEnterEvent>().onTriggerEnter.AddListener(OnCrewCollide);

        // Summon waves
        for (int i = 0; i < _waves.Count; i++)
        {
            GameObject go = _waves[i].Summon(_entity.transform, _stage.Dungeon.Spawns.WavesSpawn[i].position);
            go.AddComponent<WaveEntity>();
            go.transform.Rotate(new Vector3(0, 180, 0));
        }

        _entity.SetActive(false);
        _entity.AddComponent<DungeonEntity>().Instance = this;
        _entity.SetActive(true);
    }

    private void Destroy()
    {
        GameObject.Destroy(_entity);
    }
}
