using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Represents a user-driven champion instance in the game.
[Serializable]
public class UserDrivenChampionInstance : ChampionInstance
{
    [SerializeField]     private TrackedValue<SpellInstance> _selectedSpell;
    [SerializeReference] private ChampionInstance            _selectedTarget;

    public SpellInstance SelectedSpell
    {
        get => _selectedSpell.Value;
        set => _selectedSpell.Value = value;
    }

    public ChampionInstance SelectedTarget
    {
        get => _selectedTarget;
        set => _selectedTarget = value;
    }

    public UserDrivenChampionInstance(Champion champion) : base(champion) {}

    // Returns the driver type, indicating that this instance is controlled by the user.
    public override ChampionInstanceDriver GetDriver()
    {
        return ChampionInstanceDriver.User;
    }

    // Defines the behavior for taking a turn in combat.
    public override IEnumerator TakeTurn(CrewInstance allies, CrewInstance enemies)
    {
        _state = ChampionInstanceState.Turn;

        // Reset selected spell and accept change.
        _selectedSpell.Value = null;
        _selectedSpell.AcceptChanges();

        bool hasSpellTriggerSucceed = false;
        do
        {
            // Reset selected target if trigger failed.
            _selectedTarget = null;

            // Wait until the user selects a spell.
            yield return new WaitUntil(() => _selectedSpell.Value != null);

            // Wait until a target is selected for the chosen spell.
            yield return new WaitUntil(() =>
            { 
                // Continue update spell target while wait for target selsection
                if (_selectedSpell.Changed)
                {
                    // Reset and set the targeting states for allies and enemies.
                    _selectedSpell.LastValue?.Spell.ResetTargetState(allies, enemies);
                    _selectedSpell.Value     .Spell.SetTargetState  (allies, enemies);

                    if (_selectedSpell.Value.Spell.Behaviour.Target == SpellCrewTarget.Ally)
                    {
                        allies .ForEach(SetChampionTrigger);
                        enemies.ForEach(ResetChampionTrigger);
                    }
                    else
                    {
                        enemies.ForEach(SetChampionTrigger);
                        allies .ForEach(ResetChampionTrigger);
                    }
                    _selectedSpell.AcceptChanges();
                }

                return _selectedTarget != null;
            });

            // Attempt to trigger the spell on the selected target.
            yield return CoroutineUtils.Run<bool>(_selectedSpell.Value.Trigger(this, _selectedTarget, allies, enemies), res => hasSpellTriggerSucceed = res);
        }
        while (!hasSpellTriggerSucceed);

        // Consume the turn meter and update spell cooldowns.
        _turnMeter.Consume();
        _spells.ForEach(s => s.OnTurn());

        // Reset targeting states for all champions.
        allies .ForEach(ResetChampionTrigger);
        enemies.ForEach(ResetChampionTrigger);
        _selectedSpell.Value.Spell.ResetTargetState(allies, enemies);

        // Set the champion's state back to idle after completing the turn.
        _state = ChampionInstanceState.Idle;

        yield return true;
    }

    // Enables targeting triggers for a champion instance.
    private void SetChampionTrigger(ChampionInstance inst)
    {
        Transform t;
        t = inst.Entity.transform.Find("EntityTrigger");
        t.gameObject.SetActive(true);
        Button b;
        b = t.Find("Trigger").GetComponent<Button>();
        b.onClick.RemoveAllListeners();
        b.onClick.AddListener(() => _selectedTarget = inst);
    }

    // Disables targeting triggers for a champion instance.
    private void ResetChampionTrigger(ChampionInstance inst)
    {
        inst.Entity.transform.Find("EntityTrigger").gameObject.SetActive(false);
    }
}
