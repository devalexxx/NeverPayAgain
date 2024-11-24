using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

public enum ChampionRarity
{
    Common, Rare, Legendary
}

[Serializable]
public struct ChampionAttributes
{
    [SerializeField] public float Health;
    [SerializeField] public float Damage;
    [SerializeField] public float Speed;

    public ChampionAttributes(float health, float damage, float speed)
    {
        Health = health;
        Damage = damage;
        Speed  = speed;
    }
}

[CreateAssetMenu]
public class ChampionBehaviour : ScriptableObject
{
    [SerializeField] private ChampionRarity       _rarity;
    [SerializeField] private ChampionAttributes   _attributes;
    [SerializeField] private List<SpellBehaviour> _spells;  

    public ChampionRarity       Rarity     { get => _rarity;     }
    public ChampionAttributes   Attributes { get => _attributes; }
    public List<SpellBehaviour> Spells     { get => _spells;     }

    // Representation of the champion (Sheet is the card and Entity is the 3D representation)
    [SerializeField] private GameObject _sheet;
    [SerializeField] private GameObject _entity;

    public GameObject Sheet  { get => _sheet;  }
    public GameObject Entity { get => _entity; }

    private void OnValidate()
    {
        Debug.Assert(_spells.Count == (int)_rarity + 1, "Champion " + name + " should have " + ((int)_rarity + 1) + " spells" );
    }

    private void OnEnable()
    {
        string name = ToString().Split()[0];
        if (_sheet == null)
        {
            string[] guids = AssetDatabase.FindAssets(name + " t:Prefab", new[] { "Assets/Prefab/Sheet/Champion" });
            if (guids.Length > 1)
            {
                throw new Exception("A champion can't have more than one sheet (" + name + ")");
            }
            else
            {
                if (guids.Length > 0)
                {
                    _sheet = AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(guids[0]));
                }
            }
        }

        if (_entity == null)
        {
            string[] guids = AssetDatabase.FindAssets(name + " t:Prefab", new[] { "Assets/Prefab/Entity" });
            if (guids.Length > 1)
            {
                throw new Exception("A champion can't have more than one sheet (" + name + ")");
            }
            else
            {
                if (guids.Length > 0)
                {
                    _entity = AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(guids[0]));
                }
            }
        }
    }
}
