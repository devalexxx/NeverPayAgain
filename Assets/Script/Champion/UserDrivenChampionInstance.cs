using System;
using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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

    public override ChampionInstanceDriver GetDriver()
    {
        return ChampionInstanceDriver.User;
    }

    public override IEnumerator TakeTurn(CrewInstance allies, CrewInstance enemies)
    {
        _state = ChampionInstanceState.Turn;

        _selectedSpell.Value = null;
        _selectedSpell.AcceptChanges();
        do
        {
            _selectedTarget = null;
            yield return new WaitUntil(() => _selectedSpell.Value != null);
            yield return new WaitUntil(() =>
            { 
                if (_selectedSpell.Changed)
                {
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
        }
        while (!_selectedSpell.Value.Trigger(this, _selectedTarget, allies, enemies));

        _turnMeter.Consume();
        _spells.ForEach(s => s.OnTurn());

        allies .ForEach(ResetChampionTrigger);
        enemies.ForEach(ResetChampionTrigger);

        _selectedSpell.Value.Spell.ResetTargetState(allies, enemies);
        _state = ChampionInstanceState.Idle;

        yield return true;
    }

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

    private void ResetChampionTrigger(ChampionInstance inst)
    {
        inst.Entity.transform.Find("EntityTrigger").gameObject.SetActive(false);
    }
}
