using System;
using UnityEngine;

// TurnMeter class is used to track the progress of a turn or action in a turn-based system.
[Serializable]
public class TurnMeter : IComparable<TurnMeter>
{
    // Minimum and maximum values for the turn meter
    [SerializeField] public readonly float Min = 0.0f;
    [SerializeField] public readonly float Max = 100.0f;

    // Current value of the turn meter
    [SerializeField] private float _value;

    public float Value 
    { 
        get => _value; 
    }

    // Property that ensures the turn meter value is clamped between Min and Max
    public float BoundedValue 
    { 
        get => Math.Clamp(_value, Min, Max); 
    }

    public TurnMeter()
    {
        _value = Min;
    }

    // Method to advance the turn meter based on a speed value
    public void Advance(float speed)
    {
        _value += speed / Max;
    }

    // Method to consume the turn meter, effectively substract max to it
    public void Consume()
    {
        _value -= Max;
    }

    // Method to reset the turn meter back to its minimum value
    public void Reset()
    {
        _value = Min;
    }

    // Method to check if the turn meter is full (reached or exceeded the Max value)
    public bool IsFilled()
    {
        return _value >= Max;
    }

    // Implementation of IComparable<TurnMeter> to allow comparison of turn meters based on their value
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
