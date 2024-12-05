using System;
using System.Collections.Generic;
using UnityEngine;

// Crew class represents a team of exactly three champions in the game.
[Serializable]
public class Crew
{
    // List to hold ref of the champions that make up this crew
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
