using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

// Enum representing the rarity levels of a champion
public enum ChampionRarity
{
    Common, Rare, Legendary
}

// Struct defining the attributes of a champion
[Serializable]
public struct ChampionAttributes
{
    [SerializeField] public float Health;  // Champion's health points
    [SerializeField] public float Damage;  // Champion's damage output
    [SerializeField] public float Speed;   // Champion's speed (for turnmeter)

    public ChampionAttributes(float health, float damage, float speed)
    {
        Health = health;
        Damage = damage;
        Speed  = speed;
    }
}

// ScriptableObject defining a champion's behaviour and data
[CreateAssetMenu]
public class ChampionBehaviour : ScriptableObject, IReferencableAsset
{
    [SerializeField] private ChampionRarity       _rarity;      // Champion's rarity
    [SerializeField] private ChampionAttributes   _attributes;  // Champion's base attributes
    [SerializeField] private List<SpellBehaviour> _spells;      // List of spells associated with the champion

    // Public properties to access private fields
    public ChampionRarity       Rarity     { get => _rarity;     }
    public ChampionAttributes   Attributes { get => _attributes; }
    public List<SpellBehaviour> Spells     { get => _spells;     }

    // Visual representation of the champion:
    // _sheet is used for UI card representation, _entity for the 3D in-game model
    [SerializeField] private GameObject _sheet;
    [SerializeField] private GameObject _entity;

    // Public properties for the visual representations
    public GameObject Sheet  { get => _sheet;  }
    public GameObject Entity { get => _entity; }

    // Method called in the editor to validate the champion's configuration
    private void OnValidate()
    {
        Debug.Assert(_spells.Count == (int)_rarity + 1, "Champion " + name + " should have " + ((int)_rarity + 1) + " spells" );

        // string _name = ToString().Split()[0];
        // if (_sheet == null)
        // {
        //     string[] guids = AssetDatabase.FindAssets(_name + " t:Prefab", new[] { "Assets/Prefab/Sheet/Champion" });
        //     if (guids.Length > 1)
        //     {
        //         throw new Exception("A champion can't have more than one sheet (" + _name + ")");
        //     }
        //     else
        //     {
        //         if (guids.Length > 0)
        //         {
        //             _sheet = AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(guids[0]));
        //         }
        //     }
        // }

        // if (_entity == null)
        // {
        //     string[] guids = AssetDatabase.FindAssets(_name + " t:Prefab", new[] { "Assets/Prefab/Entity" });
        //     if (guids.Length > 1)
        //     {
        //         throw new Exception("A champion can't have more than one sheet (" + _name + ")");
        //     }
        //     else
        //     {
        //         if (guids.Length > 0)
        //         {
        //             _entity = AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(guids[0]));
        //         }
        //     }
        // }
    }

    public string GetAssetPath()
    {
        return $"Champion/Object/{name}";
    }
}
