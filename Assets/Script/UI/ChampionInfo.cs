using TMPro;
using UnityEngine;

public class ChampionInfo : MonoBehaviour
{
    [SerializeField] private ProgressBar     _health;
    [SerializeField] private ProgressBar     _turnMeter;
    [SerializeField] private TextMeshProUGUI _level;

    public void UpdateHealthBar(ChampionInstance instance)
    {
        _health.Percent = instance.Health / instance.Champion.Attributes.Health;
    }

    public void UpdateTurnMeterBar(TurnMeter turnMeter)
    {
        _turnMeter.Percent = turnMeter.BoundedValue / turnMeter.Max;
    }

    public void UpdateLevel(ChampionProgress progress)
    {
        _level.text = progress.Level.ToString();
    }
}
