using UnityEngine;
using UnityEngine.SceneManagement;

// Singleton class responsible for managing the game's global state, including the champion library and player data.
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private ChampionLibrary _championLibrary;
    private DungeonLibrary  _dungeonLibrary;
    private PlayerData      _player;

    public ChampionLibrary ChampionLibrary => _championLibrary;
    public DungeonLibrary  DungeonLibrary  => _dungeonLibrary;
    public PlayerData      Player          => _player;
    public object          ScenePayload;

    private void Awake()
    {
        // Singleton pattern to ensure only one instance of GameManager exists.
        if (Instance == null)
        {
            Instance         = this;
            _championLibrary = new();
            _dungeonLibrary  = new();
            _player          = new();

            ScenePayload = null;

            // @TODO: Remove this line later. It's currently adding all champions in the library to the player's inventory.
            _championLibrary.ForEach(behaviour => _player.ChampionInventory.AddItem(new(behaviour)));
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public static void FOo()
    {
        var t_dungeon = Instance._dungeonLibrary[0];
        Instance.ScenePayload = t_dungeon.Stages[0];
        SceneManager.LoadScene(t_dungeon.Behaviour.RelatedScene);
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

}
