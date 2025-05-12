using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Slot<T>
{
    [SerializeReference] public T    element;
    [SerializeField]     public bool locked;
    
    public bool empty => EqualityComparer<T>.Default.Equals(element, default);
}

[Serializable]
public class Inventory<T> : IEnumerable<Slot<T>>
{
    [SerializeField] private List<Slot<T>> _data;
    [SerializeField] private int           _size;

    public Inventory(int p_size)
    {
        _data = Enumerable.Range(0, p_size).Select(_ => new Slot<T> { element = default, locked = false }).ToList();   
    }

    public bool Add(T p_item)
    {
        var t_slot = _data.Find(p_slot => p_slot.empty);
        if (t_slot != null)
        {
            t_slot.element = p_item;
            return true;
        }

        return false;
    }

    public bool TryGet(int p_index, out T p_item)
    {
        if (p_index < _size)
        {
            var t_slot = _data[p_index];
            if (!t_slot.locked && !t_slot.empty)
            {
                p_item = t_slot.element;
                return true;
            }
        }
        p_item = default;
        return false;
    }

    public IEnumerator<Slot<T>> GetEnumerator() => _data.GetEnumerator();
    IEnumerator     IEnumerable.GetEnumerator() => GetEnumerator();
}

public static class InventoryExtension
{
    public static void ForEach<T>(this Inventory<T> p_self, Action<Slot<T>> p_action)
    {
        foreach (Slot<T> t_slot in p_self)
        {
            p_action.Invoke(t_slot);
        }
    }

    public static void ForEach<T>(this Inventory<T> p_self, Action<T> p_action)
    {
        foreach (Slot<T> t_slot in p_self)
        {
            if (!t_slot.empty)
                p_action.Invoke(t_slot.element);
        }
    }
}
