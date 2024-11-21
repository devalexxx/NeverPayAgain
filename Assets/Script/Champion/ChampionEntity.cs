using UnityEngine;

public class ChampionEntity : MonoBehaviour
{
    [SerializeField] private ChampionInstance _instance;

    [SerializeField] private ProgressBar      _healthBar;
    [SerializeField] private ProgressBar      _turnMeterBar;

    public ChampionInstance Instance
    {
        get => _instance;
        set => _instance = value;
    }

    void Update()
    {
        UpdateBars();
    }

    private void UpdateBars()
    {
        _healthBar.Percent    = _instance.Health / _instance.Champion.Attributes.Health;
        _turnMeterBar.Percent = _instance.TurnMeter.BoundedValue / _instance.TurnMeter.Max;
    }
}
