using UnityEngine;
using UnityEngine.UI;

//  Manages the overall flow of the dungeon, including initializing the dungeon, selecting champions, and starting the dungeon instance.
public class DungeonManager : MonoBehaviour
{
    // References to dungeon stage and spawn setup, which define the dungeon's configuration.
    [SerializeField] private DungeonStage _stage;
    [SerializeField] private DungeonSpawn _spawn;

    // The player's crew.
    private Crew _crew;

    // The dungeon instance
    private DungeonInstance _instance;

    // References to the interface and champion selection system.
    private GameObject       _interface;
    private ChampionSelector _selector;

    private void Awake()
    {
        _interface = GameObject.FindWithTag("MainInterface");
        _selector  = _interface.GetComponent<ChampionSelector>();
        
        var t_startButton = _selector.transform.Find("StartButton").GetComponent<Button>();
        if (GameManager.Instance.ScenePayload is DungeonStage t_stage)
        {
            _stage = t_stage;
            t_startButton.onClick.AddListener(OnStartButtonClicked);
        }
        else
        {
            t_startButton.interactable = false;
        }
    }

    // Called when the dungeon ends, shows the interface again.
    private void OnDungeonEnded()
    {
        _interface.SetActive(true);

        var t_rewards = _stage.Dungeon.Behaviour.Rewards;
        if (_instance.IsWon())
        {
            uint stage = 0;
            uint exp = t_rewards.ExperienceDefault * t_rewards.ExperienceMultiplier * (stage + 1) / (uint)_crew.Champions.Count;
            _crew.Champions.ForEach(champion => champion.Progress.Earn(exp));
        }
    }

    // Handles the start button click event, checks if the player has selected 3 champions and starts the dungeon.
    private void OnStartButtonClicked()
    {
        if (_stage != null && _selector.Selection.Count == 3)
        {   
            _interface.SetActive(false);

            _crew     = new(_selector.Selection);
            _instance = new(_stage, _spawn, _crew);
            _instance.onEnded.AddListener(OnDungeonEnded);

            StartCoroutine(_instance.Start());
        }
        else
        {
            Debug.Log(_selector.Selection.Count);
        }
    }
}
