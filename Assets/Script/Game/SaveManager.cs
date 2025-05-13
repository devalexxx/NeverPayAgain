using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public static class SaveManager
{
#if UNITY_EDITOR
    private static readonly string PATH = Path.Combine(Application.dataPath, "Saves");
#else
    private static readonly string PATH = Path.Combine(Application.persistentDataPath, "Saves")
#endif

    public static event Action onSaveLoaded;
    public static event Action onSaveCreated;
    public static event Action onSaveSaved;

    public static List<Guid> AvailableSaves => Directory.GetFiles(PATH).Where(t_path => !t_path.Contains(".meta")).Select(t_path => Guid.Parse(t_path.Split("/")[^1].Split(".")[0])).ToList();

    public static void Create(PlayerSave p_save, PlayerInitialData p_data)
    {
        p_save.New(p_data);
        Save(p_save);
        onSaveCreated?.Invoke();
    }

    public static void Save(PlayerSave p_data)
    {
        if (!Directory.Exists(PATH))
        {
            Directory.CreateDirectory(PATH);
        }

        string t_path = Path.Combine(PATH, $"{p_data.guid}.json");
        string t_json = JsonSerializer.ToJson(p_data);
        File.WriteAllText(t_path, t_json);

        onSaveSaved?.Invoke();
    }

    public static bool Load(Guid p_guid, out PlayerSave p_data)
    {
        if (!Directory.Exists(PATH))
        {
            Directory.CreateDirectory(PATH);
        }

        string t_path = Path.Combine(PATH, $"{p_guid}.json");
        if (File.Exists(t_path) && p_guid != Guid.Empty)
        {
            string t_json = File.ReadAllText(t_path);
            p_data = JsonSerializer.FromJson<PlayerSave>(t_json);
            onSaveLoaded?.Invoke();
            return true;
        }

        p_data = null;
        return false;
    }
}
