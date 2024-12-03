using System;
using UnityEngine;

[Serializable]
public struct TrackedValue<T>
{
    [SerializeReference] private T    _value;
    [SerializeReference] private T    _lastValue;
    [SerializeField]     private bool _changed;

    public T Value
    {
        readonly get => _value;
        set
        {
            if (!Equals(_value, value))
            {
                _lastValue = _value;
                _value     = value;
                _changed   = true;
            }
        }
    }

    public readonly T    LastValue => _lastValue;
    public readonly bool Changed   => _changed;

    public void AcceptChanges()
    {
        _changed = false;
    }
}
