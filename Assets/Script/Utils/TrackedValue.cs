using UnityEngine;

public struct TrackedValue<T>
{
    private T    _value;
    private T    _lastValue;
    private bool _changed;

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
