using TMPro;
using UnityEngine;

public class ChampionSheet : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _level;

    public Champion Champion;

    private void Start()
    {
        Champion.Progress.onLevelUp.AddListener(UpdateLevel);
        UpdateLevel(Champion.Progress.Level);
    }

    private void OnDestroy()
    {
        Champion.Progress.onLevelUp.RemoveListener(UpdateLevel);
    }

    private void UpdateLevel(uint level)
    {
        _level.text = level.ToString();
    }
}
