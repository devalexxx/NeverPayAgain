using UnityEngine;

public class SpellBehaviour : MonoBehaviour
{
    // Representation of the champion (Sheet is the card and Entity is the 3D representation)
    [SerializeField] private GameObject _sheet;
    [SerializeField] private GameObject _entity;

    public GameObject Sheet  { get => _sheet;  }
    public GameObject Entity { get => _entity; }
}
