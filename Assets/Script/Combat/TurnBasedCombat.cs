using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

// Enum representing the state of the combat.
[Serializable]
public enum CombatState
{
    Starting, InProgress, Ended
}


// Class managing turn-based combat between two crew.
[Serializable]
public class TurnBasedCombat
{
    [SerializeReference] private CrewInstance _lhs;   // Left-hand side (team 1)
    [SerializeReference] private CrewInstance _rhs;   // Right-hand side (team 2)
    [SerializeField]     private CombatState  _state; // Current state of the combat (Starting, InProgress, Ended)
    [SerializeField]     private float        _speed; // Speed of turn meter progression

    public CombatState State
    {
        get => _state;
    }

    public TurnBasedCombat(CrewInstance lhs, CrewInstance rhs, float speed = 500.0f)
    {
        _lhs   = lhs;
        _rhs   = rhs;
        _state = CombatState.Starting;
        _speed = speed;
    }

    // Starts the combat and progresses through turns until one team wins.
    public IEnumerator Start()
    {
        ResetTurnMeters();
        _state = CombatState.InProgress;
        
        // While the combat is in progress, keep progressing through turns.
        while (_state == CombatState.InProgress)
            yield return Progress();
    }

    // Progresses the combat by taking turns for the champions in each team.
    public IEnumerator Progress()
    {
        // If both teams are alive, proceed with the combat round.
        if (_lhs.IsAlive() && _rhs.IsAlive())
        {
            Stack<ChampionInstance> stack = new();

            // Push champions that can take a turn to the stack.
            ForEachChampion(inst => { if (inst.CanTakeTurn()) { stack.Push(inst); } });
            stack = new(stack.OrderBy(inst => inst.TurnMeter));

            // Pop and take turns for the champion with the highest turn meter.
            if (stack.TryPop(out var inst))
            {
                bool hasSucceed = false;
                do
                {
                    // Execute the champion's turn and check if it succeeded.
                    if (_lhs.Contains(inst))
                        yield return CoroutineUtils.Run<bool>(inst.TakeTurn(_lhs, _rhs), res => hasSucceed = res);
                    else
                        yield return CoroutineUtils.Run<bool>(inst.TakeTurn(_rhs, _lhs), res => hasSucceed = res);
                }
                while(!hasSucceed); // Repeat until the champion's turn is successfully executed.
            }

            ForEachChampion(inst => inst.Advance(Time.deltaTime * _speed)); // Progress the turn meter for all champions.
        }
        else
        {
            // End the combat when one team is defeated.
            _state = CombatState.Ended;
        }
    }

    public bool HasWon(CrewInstance inst, out CrewInstance looser)
    {
        if (_state == CombatState.Ended && inst.IsAlive())
        {
            looser = Equals(inst, _lhs) ? _rhs : _lhs;
            return true;
        }
        looser = null;
        return false;
    }

    // Resets the turn meters for all champions.
    private void ResetTurnMeters()
    {
        ForEachChampion(inst => inst.TurnMeter.Reset());
    }

    // Executes the given action on all champions in both teams.
    private void ForEachChampion(Action<ChampionInstance> action)
    {
        _lhs.ForEach(action);
        _rhs.ForEach(action);
    }

}
