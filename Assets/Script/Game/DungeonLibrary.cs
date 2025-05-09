using System.Linq;
using UnityEngine;

public class DungeonLibrary : Library<Dungeon>
{
    public DungeonLibrary()
    {
        AddRange(Resources.LoadAll<DungeonBehaviour>("Dungeon/Object").Select(t_behaviour => new Dungeon(t_behaviour)).ToList());
    }
}
