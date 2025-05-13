using System;
using UnityEngine;

// Singleton class responsible for managing the game's global state, including the champion library and player data.
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private PlayerInitialData _playerInitialData;

    private ChampionLibrary _championLibrary;
    private DungeonLibrary  _dungeonLibrary;
    [SerializeReference] public PlayerSave      Player;

    public ChampionLibrary ChampionLibrary => _championLibrary;
    public DungeonLibrary  DungeonLibrary  => _dungeonLibrary;
    public object          ScenePayload;

    private void Awake()
    {
        // Singleton pattern to ensure only one instance of GameManager exists.
        if (Instance == null)
        {
            Instance         = this;
            _championLibrary = new();
            _dungeonLibrary  = new();
            Player           = new();

            SetupPlayer();

            ScenePayload = null;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void NewPlayer()
    {
        SaveManager.Create(Player, _playerInitialData);
    }

    public void SetupPlayer()
    {
        var t_save = PlayerPrefs.GetString("player-save", "");
        if (t_save != "")
        {
            if (Guid.TryParse(t_save, out var t_guid))
            {
                if (SaveManager.AvailableSaves.Contains(t_guid))
                {
                    Load(t_guid);
                    return;
                }
            }
        }
            
        NewPlayer();
    }

    public void Load(Guid p_guid)
    {
        SaveManager.Load(p_guid, out Player);
    }

    public void Save()
    {
        SaveManager.Save(Player);
    }

    private void OnApplicationQuit()
    {
        Save();
        PlayerPrefs.SetString("player-save", Player.guid.ToString("", null));
    }

}
