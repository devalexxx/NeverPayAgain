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
}
