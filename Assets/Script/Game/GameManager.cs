using System;
using UnityEngine;

// Singleton class responsible for managing the game's global state, including the champion library and player data.
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

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

            var t_saves = SaveManager.AvailableSaves;
            Load(t_saves.Count > 0 ? t_saves[0] : Guid.Empty);

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
    }

}
