using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerInitialData : ScriptableObject
{
    [field: SerializeField] public List<ChampionBehaviour> champions { get; private set; }
}
