using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum CombatState
{
    Starting, InProgress, Ended
}

public struct CombatResult
{

}

[Serializable]
public class TurnBasedCombat
{
    [SerializeField] private CrewInstance _lhs;
    [SerializeField] private CrewInstance _rhs;
    [SerializeField] private CombatState  _state;

    [SerializeField] private Stack<ChampionInstance> stack = new();

    public TurnBasedCombat(CrewInstance lhs, CrewInstance rhs)
    {
        _lhs   = lhs;
        _rhs   = rhs;
        _state = CombatState.Starting;
    }

    public void Start()
    {
        ResetTurnMeters();
        _state = CombatState.InProgress;
    }

    public void Progress(float delta)
    {
        if (_state == CombatState.InProgress)
        {
            if (_lhs.IsAlive() && _rhs.IsAlive())
            {
                stack = new();
                ForEachChampion(inst => { if (inst.CanTakeTurn()) { stack.Push(inst); } });
                stack = new(stack.OrderBy(inst => inst.TurnMeter));

                if (stack.TryPop(out var inst))
                {
                    // @TODO: Check if turn has failed
                    if (_lhs.Contains(inst))
                    {
                        ChampionInstance target = inst;
                        while (target == inst)
                        {
                            target = _lhs.PickRandom();
                        }
                        inst.TakeTurn(target, _lhs, _rhs);
                    }
                    else
                    {
                        ChampionInstance target = inst;
                        while (target == inst)
                        {
                            target = _rhs.PickRandom();
                        }
                        inst.TakeTurn(target, _rhs, _lhs);
                    }
                }

                ForEachChampion(inst => inst.Advance(delta));
            }
            else
            {
                _state = CombatState.Ended;
            }
        }
    }

    private void ForEachChampion(Action<ChampionInstance> action)
    {
        _lhs.ForEach(action);
        _rhs.ForEach(action);
    }

    private void ResetTurnMeters()
    {
        _lhs.ForEach(inst => inst.TurnMeter.Reset());
        _rhs.ForEach(inst => inst.TurnMeter.Reset());
    }

}
