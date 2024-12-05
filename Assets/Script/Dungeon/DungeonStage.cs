using System.Collections.Generic;
using System.Linq;

// Represents a stage within a dungeon, containing waves of enemy crews (groups of champions).
public class DungeonStage
{
    private Dungeon        _dungeon;      // Reference to the dungeon this stage belongs to.
    private List<Champion> _champions;    // List of champions for the current stage.
    private List<Crew>     _waves;        // List of waves containing the enemy crews for the stage.

    public Dungeon    Dungeon => _dungeon;
    public List<Crew> Waves   => _waves;

    public DungeonStage(Dungeon dungeon, int stage)
    {
        _dungeon   = dungeon;
        _champions = new();

        // Generate waves based on the wave descriptors in the dungeon's behavior.
        _waves     = dungeon.Behaviour.WaveDescriptors
            .Select(
                wdesc => 
                {
                    // For each wave descriptor, create champions and adjust their levels based on the current stage.
                    foreach (var cdesc in wdesc.ChampionDescriptors)
                    {
                        _champions.Add(new Champion(cdesc.Behaviour, cdesc.DefaultLevel + wdesc.LevelIncrement * (uint)stage));
                    }
                    return new Crew(_champions[^3], _champions[^2], _champions[^1]);
                }
        ).ToList();
    }
}
