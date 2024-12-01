using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private ChampionLibrary _championLibrary;
    private PlayerData      _player;

    public ChampionLibrary ChampionLibrary => _championLibrary;
    public PlayerData      Player          => _player;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance         = this;
            _championLibrary = new();
            _player          = new();

            // @TODO: Remove that
            _championLibrary.ForEach(behaviour => _player.ChampionInventory.AddItem(new(behaviour)));

            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

}
