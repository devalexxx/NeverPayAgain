using System.Collections.Generic;
using System.Linq;

public class DungeonStage
{
    private Dungeon        _dungeon;
    private List<Champion> _champions;
    private List<Crew>     _waves;

    public Dungeon    Dungeon => _dungeon;
    public List<Crew> Waves   => _waves;

    public DungeonStage(Dungeon dungeon, int stage)
    {
        _dungeon   = dungeon;
        _champions = new();
        _waves     = dungeon.Behaviour.WaveDescriptors
            .Select(
                wdesc => 
                {
                    foreach (var cdesc in wdesc.ChampionDescriptors)
                    {
                        _champions.Add(new Champion(cdesc.Behaviour, cdesc.DefaultLevel + wdesc.LevelIncrement * (uint)stage));
                    }
                    return new Crew(_champions[0], _champions[1], _champions[2]);
                }
        ).ToList();
    }
}
