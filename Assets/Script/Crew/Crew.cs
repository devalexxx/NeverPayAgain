using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Crew
{
    [SerializeReference] private List<Champion> _champions;

    public List<Champion> Champions { get => _champions; }

    public Crew(Champion a, Champion b, Champion c)
    {
        _champions = new() { a, b, c };
    }

    public Crew(List<Champion> champions) 
    {
        if (champions.Count < 3)
        {
            throw new ArgumentOutOfRangeException(nameof(champions), $"The list must contain at least 3 elements.");
        }
        _champions = champions.GetRange(0, 3);
    }
}
