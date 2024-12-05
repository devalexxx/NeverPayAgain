using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// Enum representing the possible target types for a spell in terms of its crew target (Ally or Enemy).
[Serializable]
public enum SpellCrewTarget
{
    Ally, Enemy
}

// Represents the behavior of a spell, including its effects, cooldown period, target type (ally or enemy),
[CreateAssetMenu]
public class SpellBehaviour : ScriptableObject
{
    [SerializeReference, SubclassPicker]
    [SerializeField] private List<SpellEffect> _effects;    // List of spell effects
    [SerializeField] private uint              _cooldown;   // Cooldown time for the spell
    [SerializeField] private SpellCrewTarget   _target;     // The target of the spell (either Ally or Enemy)

    public List<SpellEffect> Effects  
    { 
        get => _effects;  
    }

    public uint Cooldown 
    { 
        get => _cooldown; 
    }

    public SpellCrewTarget Target 
    { 
        get => _target; 
    }

    // Representation of the spell (Sheet is the card)
    [SerializeField] private GameObject _sheet;

    public GameObject Sheet  { get => _sheet;  }

    private void OnEnable()
    {
        // string name = ToString().Split()[0];
        // if (_sheet == null)
        // {
        //     string[] guids = AssetDatabase.FindAssets(name + " t:Prefab", new[] { "Assets/Prefab/Sheet/Spell" });
        //     if (guids.Length > 1)
        //     {
        //         throw new Exception("A spell can't have more than one sheet (" + name + ")");
        //     }
        //     else
        //     {
        //         if (guids.Length > 0)
        //         {
        //             _sheet = AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(guids[0]));
        //         }
        //     }
        // }
    }
}
