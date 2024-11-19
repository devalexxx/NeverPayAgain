using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[Serializable]
public class TurnMeter
{
    [SerializeField] private readonly float Min = 0.0f;
    [SerializeField] private readonly float Max = 100.0f;
    [SerializeField] private          float _value;

    public float Value { get => _value; }

    public TurnMeter()
    {
        _value = Min;
    }

    public void Advance(float speed)
    {
        _value += speed / Max;
    }

    public void Consume()
    {
        _value -= Max;
    }

    public void Reset()
    {
        _value = Min;
    }

    public bool IsFilled()
    {
        return _value >= Max;
    }

    public static bool operator <=(TurnMeter lhs, TurnMeter rhs)
    {
        return lhs._value < rhs._value;
    }

    public static bool operator >=(TurnMeter lhs, TurnMeter rhs)
    {
        return !(lhs <= rhs);
    }
}
