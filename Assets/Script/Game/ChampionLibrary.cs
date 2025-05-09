using System.Linq;
using UnityEngine;

// Manages a collection of ChampionBehaviour objects, providing methods to access and iterate over them.
public class ChampionLibrary : Library<ChampionBehaviour>
{
    public ChampionLibrary()
    {
        AddRange(Resources.LoadAll<ChampionBehaviour>("Champion/Object").ToList());
    }
}
