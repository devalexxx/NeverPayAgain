using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class CombatTest : MonoBehaviour
{
    public ChampionBehaviour cb1;
    public ChampionBehaviour cb2;
    public ChampionBehaviour cb3;
    public ChampionBehaviour cb4;
    public ChampionBehaviour cb5;
    public ChampionBehaviour cb6;

    public Champion c1;
    public Champion c2;
    public Champion c3;
    public Champion c4;
    public Champion c5;
    public Champion c6;

    public Crew clhs;
    public Crew crhs;

    public CrewInstance cilhs;
    public CrewInstance cirhs;

    public TurnBasedCombat combat;

    // Start is called before the first frame update
    void Start()
    {
        c1 = new(cb1);
        c2 = new(cb2);
        c3 = new(cb3);
        c4 = new(cb4);
        c5 = new(cb5);
        c6 = new(cb6);
    
        clhs = new(c1, c2, c3);
        crhs = new(c4, c5, c6);

        cilhs = new(clhs);
        cirhs = new(crhs, false);

        cilhs.Summon(transform, new(0, 0,   0));
        cirhs.Summon(transform, new(0, 0, -10));

        combat = new(cilhs, cirhs);

        StartCoroutine(combat.Start());
    }
}
