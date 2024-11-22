using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

[Serializable]
public enum CombatState
{
    Starting, InProgress, Ended
}

[Serializable]
public class TurnBasedCombat
{
    [SerializeField] private CrewInstance _lhs;
    [SerializeField] private CrewInstance _rhs;
    [SerializeField] private CombatState  _state;

    [SerializeField] private Stack<ChampionInstance> stack = new();

    public CombatState State
    {
        get => _state;
    }

    public TurnBasedCombat(CrewInstance lhs, CrewInstance rhs)
    {
        _lhs   = lhs;
        _rhs   = rhs;
        _state = CombatState.Starting;
    }

    public IEnumerator Start()
    {
        ResetTurnMeters();
        _state = CombatState.InProgress;
        
        while (_state == CombatState.InProgress)
            yield return Progress();
    }

    public IEnumerator Progress()
    {
        Assert.AreEqual(_state, CombatState.InProgress);
        if (_lhs.IsAlive() && _rhs.IsAlive())
        {
            stack = new();
            ForEachChampion(inst => { if (inst.CanTakeTurn()) { stack.Push(inst); } });
            stack = new(stack.OrderBy(inst => inst.TurnMeter));

            if (stack.TryPop(out var inst))
            {
                bool hasSucceed = false;
                do
                {
                    if (_lhs.Contains(inst))
                        yield return CoroutineUtils.Run<bool>(inst.TakeTurn(_lhs, _rhs), res => hasSucceed = res);
                    else
                        yield return CoroutineUtils.Run<bool>(inst.TakeTurn(_rhs, _lhs), res => hasSucceed = res);
                }
                while(!hasSucceed);
            }

            ForEachChampion(inst => inst.Advance(Time.deltaTime * 100.0f));
        }
        else
        {
            _state = CombatState.Ended;
        }
    }

    private void ResetTurnMeters()
    {
        ForEachChampion(inst => inst.TurnMeter.Reset());
    }

    private void ForEachChampion(Action<ChampionInstance> action)
    {
        _lhs.ForEach(action);
        _rhs.ForEach(action);
    }

}
