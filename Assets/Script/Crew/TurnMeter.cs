using System;
using UnityEngine;

[Serializable]
public class TurnMeter : IComparable<TurnMeter>
{
    [SerializeField] public readonly float Min = 0.0f;
    [SerializeField] public readonly float Max = 100.0f;

    [SerializeField] private float _value;

    public float Value 
    { 
        get => _value; 
    }

    public float BoundedValue 
    { 
        get => Math.Clamp(_value, Min, Max); 
    }

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

    public int CompareTo(TurnMeter other)
    {
        if (_value > other._value)
            return 1;
        else if (_value < other._value)
            return -1;
        else
            return 0;
    }
}
