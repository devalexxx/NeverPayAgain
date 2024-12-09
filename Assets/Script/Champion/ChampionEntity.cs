using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

// Represents the in-game entity for a ChampionInstance
public class ChampionEntity : MonoBehaviour
{
    [SerializeField] private ChampionInstance _instance;    // Associated ChampionInstance

    [SerializeField] private ChampionInfo _info;
    [SerializeField] private GameObject   _turnCursor;      // Indicator for active turn
    [SerializeField] private GameObject   _targetCursor;    // Indicator for target selection
    [SerializeField] private GameObject   _spellCanvas;     // UI canvas for spell selection

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
        // Ensure the instance is not null
        if (_instance == null)
        {
            throw new NullReferenceException("[ChampionEntity] _instance must be non-null");
        }

        // Attach the spell selector if the instance is user-controlled
        if (_instance.GetDriver() == ChampionInstanceDriver.User)
        {
            _spellCanvas.GetOrAddComponent<ChampionEntitySpellSelector>().Instance = _instance as UserDrivenChampionInstance;
        }

        // Retrieve or add the ChampionAnimationProxy to handle animation events
        AnimationProxy = GetComponentInChildren<Animator>().gameObject.GetOrAddComponent<ChampionAnimationProxy>();
    }

    void Start()
    {
        _info.UpdateHealthBar   (_instance);
        _info.UpdateTurnMeterBar(_instance.TurnMeter);
        _info.UpdateLevel       (_instance.Champion.Progress);
    }

    void Update()
    {
        _info.UpdateTurnMeterBar(_instance.TurnMeter);

        // Determine if it is the champion's turn
        bool isTurn = _instance.State == ChampionInstanceState.TurnAndTarget || _instance.State == ChampionInstanceState.Turn;

        // Update UI elements based on the current state
        _spellCanvas .SetActive(isTurn && _instance.GetDriver() == ChampionInstanceDriver.User);
        _turnCursor  .SetActive(isTurn);
        _targetCursor.SetActive(_instance.State == ChampionInstanceState.TurnAndTarget || _instance.State == ChampionInstanceState.Target);
    }

    public void NotifyTakeDamage()
    {
        AnimationProxy.Animator.SetTrigger("TakeDamage");
        _info.UpdateHealthBar(_instance);

        // Disable the game object if health reaches zero (Trigger death animation in future)
        if (_instance.Health <= 0)
            gameObject.SetActive(false);
    }

    // Notify that the champion has been healed
    public void NotifyHealing()
    {
        _info.UpdateHealthBar(_instance);
    }
}
