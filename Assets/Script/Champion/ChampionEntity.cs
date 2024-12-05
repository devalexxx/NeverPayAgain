using System;
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

    public ChampionAnimationProxy AnimationProxy 
    { 
        get; 
        private set; 
    }

    void Awake()
    {
        if (_instance == null)
        {
            throw new NullReferenceException("[ChampionEntity] _instance must be non-null");
        }

        if (_instance.GetDriver() == ChampionInstanceDriver.User)
        {
            _spellCanvas.GetOrAddComponent<ChampionEntitySpellSelector>().Instance = _instance as UserDrivenChampionInstance;
        }

        AnimationProxy = GetComponentInChildren<Animator>().gameObject.GetOrAddComponent<ChampionAnimationProxy>();
    }

    void Start()
    {
        UpdateHealthBar();
        UpdateTurnMeterBar();
    }

    void Update()
    {
        UpdateTurnMeterBar();

        bool isTurn = _instance.State == ChampionInstanceState.TurnAndTarget || _instance.State == ChampionInstanceState.Turn;
        _spellCanvas .SetActive(isTurn && _instance.GetDriver() == ChampionInstanceDriver.User);
        _turnCursor  .SetActive(isTurn);
        _targetCursor.SetActive(_instance.State == ChampionInstanceState.TurnAndTarget || _instance.State == ChampionInstanceState.Target);
    }

    public void NotifyTakeDamage()
    {
        AnimationProxy.Animator.SetTrigger("TakeDamage");
        UpdateHealthBar();

        if (_instance.Health <= 0)
            gameObject.SetActive(false);
    }

    public void NotifyHealing()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        _healthBar.Percent = _instance.Health / _instance.Champion.Attributes.Health;
    }

    private void UpdateTurnMeterBar()
    {
        _turnMeterBar.Percent = _instance.TurnMeter.BoundedValue / _instance.TurnMeter.Max;
    }
}
