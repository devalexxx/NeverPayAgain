using System;
using UnityEngine;
using UnityEngine.UI;

public class DungeonManager : MonoBehaviour
{
    [SerializeField] private DungeonBehaviour _behaviour;
    [SerializeField] private DungeonSpawn     _spawn;

    private GameObject       _interface;
    private ChampionSelector _selector;

    private Crew            _playerCrew;
    private Dungeon         _dungeon;
    private DungeonInstance _instance;

    private void Awake()
    {
        _dungeon   = new(_behaviour, _spawn);
        _interface = GameObject.FindWithTag("MainInterface");
        _selector  = _interface.GetComponent<ChampionSelector>();
        _selector.transform.Find("StartButton").GetComponent<Button>().onClick.AddListener(OnStartButtonClicked);
    }

    private void OnDungeonEnded()
    {
        _interface.SetActive(true);
    }

    private void OnStartButtonClicked()
    {
        if (_selector.Selection.Count == 3)
        {   
            _interface.SetActive(false);

            _playerCrew = new(_selector.Selection);
            _instance   = new(_dungeon.Stages[0], _playerCrew);
            _instance.onEnded.AddListener(OnDungeonEnded);

            StartCoroutine(_instance.Start());
        }
        else
        {
            Debug.Log(_selector.Selection.Count);
        }
    }
}
