using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SpellBehaviour : ScriptableObject
{
    [SerializeReference, SubclassPicker]
    [SerializeField] private List<SpellEffect> _effects;
    [SerializeField] private uint              _cooldown;

    public List<SpellEffect> Effects  { get => _effects;  }
    public uint              Cooldown { get => _cooldown; }

    // Representation of the spell (Sheet is the card and Entity is the 3D representation)
    [SerializeField] private GameObject _sheet;
    [SerializeField] private GameObject _entity;

    public GameObject Sheet  { get => _sheet;  }
    public GameObject Entity { get => _entity; }
}
