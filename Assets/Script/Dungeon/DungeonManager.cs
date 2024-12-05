using System;
using UnityEngine;
using UnityEngine.UI;

//  Manages the overall flow of the dungeon, including initializing the dungeon, selecting champions, and starting the dungeon instance.
public class DungeonManager : MonoBehaviour
{
    // References to dungeon behaviour and spawn setup, which define the dungeon's configuration.
    [SerializeField] private DungeonBehaviour _behaviour;
    [SerializeField] private DungeonSpawn     _spawn;

    // References to the interface and champion selection system.
    private GameObject       _interface;
    private ChampionSelector _selector;

    // The player's crew and the dungeon and instance objects for managing the dungeon progress.
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

    // Called when the dungeon ends, shows the interface again.
    private void OnDungeonEnded()
    {
        _interface.SetActive(true);
    }

    // Handles the start button click event, checks if the player has selected 3 champions and starts the dungeon.
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
