using Unity.VisualScripting;
using UnityEngine;

public class ChampionEntity : MonoBehaviour
{
    [SerializeField] private ChampionInstance _instance;

    [SerializeField] private ProgressBar _healthBar;
    [SerializeField] private ProgressBar _turnMeterBar;
    [SerializeField] private GameObject  _turnCursor;
    [SerializeField] private GameObject  _targetCursor;
    [SerializeField] private GameObject  _spellCanvas;

    public ChampionInstance Instance
    {
        get => _instance;
        set => _instance = value;
    }

    void Awake()
    {
        if (_instance.GetDriver() == ChampionInstanceDriver.User)
            _spellCanvas.GetOrAddComponent<ChampionEntitySpellSelector>().Instance = _instance as UserDrivenChampionInstance;
    }

    void Update()
    {
        if (_instance != null)
        {
            _healthBar.Percent    = _instance.Health / _instance.Champion.Attributes.Health;
            _turnMeterBar.Percent = _instance.TurnMeter.BoundedValue / _instance.TurnMeter.Max;

            bool isTurn = _instance.State == ChampionInstanceState.TurnAndTarget || _instance.State == ChampionInstanceState.Turn;
            _spellCanvas .SetActive(isTurn && _instance.GetDriver() == ChampionInstanceDriver.User);
            _turnCursor  .SetActive(isTurn);
            _targetCursor.SetActive(_instance.State == ChampionInstanceState.TurnAndTarget || _instance.State == ChampionInstanceState.Target);
        }
    }
}
