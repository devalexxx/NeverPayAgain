using System;
using System.Collections.Generic;

[Serializable]
public class Crew
{
    private List<Champion> _champions;

    public List<Champion> Champions { get => _champions; }

    public Crew(Champion a, Champion b, Champion c)
    {
        _champions.Add(a);
        _champions.Add(b);
        _champions.Add(c);
    }
}
