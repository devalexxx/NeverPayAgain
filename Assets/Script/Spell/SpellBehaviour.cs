using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Serializable]
public enum SpellCrewTarget
{
    Ally, Enemy
}

[CreateAssetMenu]
public class SpellBehaviour : ScriptableObject
{
    [SerializeReference, SubclassPicker]
    [SerializeField] private List<SpellEffect> _effects;
    [SerializeField] private uint              _cooldown;
    [SerializeField] private SpellCrewTarget   _target;

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

    // Representation of the spell (Sheet is the card and Entity is the 3D representation)
    [SerializeField] private GameObject _sheet;
    // [SerializeField] private GameObject _entity;

    public GameObject Sheet  { get => _sheet;  }
    // public GameObject Entity { get => _entity; }

    private void OnEnable()
    {
        string name = ToString().Split()[0];
        if (_sheet == null)
        {
            string[] guids = AssetDatabase.FindAssets(name + " t:Prefab", new[] { "Assets/Prefab/Sheet/Spell" });
            if (guids.Length > 1)
            {
                throw new Exception("A spell can't have more than one sheet (" + name + ")");
            }
            else
            {
                if (guids.Length > 0)
                {
                    _sheet = AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(guids[0]));
                }
            }
        }
    }
}
