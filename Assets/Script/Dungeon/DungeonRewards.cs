using System;
using UnityEngine;

[Serializable]
public class DungeonRewards
{
    [SerializeField] private uint _experienceDefault;       // The amount of experience earned for a stage 1 dungeon win
    [SerializeField] private uint _experienceMultiplier;    // The experience multiplier between stages

    public uint ExperienceDefault    => _experienceDefault;
    public uint ExperienceMultiplier => _experienceMultiplier;
}
