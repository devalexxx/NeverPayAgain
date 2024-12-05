using UnityEngine;

// Singleton class responsible for managing the game's global state, including the champion library and player data.
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private ChampionLibrary _championLibrary;
    private PlayerData      _player;

    public ChampionLibrary ChampionLibrary => _championLibrary;
    public PlayerData      Player          => _player;

    private void Awake()
    {
        // Singleton pattern to ensure only one instance of GameManager exists.
        if (Instance == null)
        {
            Instance         = this;
            _championLibrary = new();
            _player          = new();

            // @TODO: Remove this line later. It's currently adding all champions in the library to the player's inventory.
            _championLibrary.ForEach(behaviour => _player.ChampionInventory.AddItem(new(behaviour)));

            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

}
