using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserDrivenChampionInstance : ChampionInstance
{
    public UserDrivenChampionInstance(Champion champion) : base(champion) {}

    public override IEnumerator TakeTurn(CrewInstance allies, CrewInstance enemies)
    {
        for(;;)
            yield return null;
    }
}
