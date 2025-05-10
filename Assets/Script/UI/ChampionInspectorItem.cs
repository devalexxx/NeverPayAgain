using System;
using TMPro;
using UnityEngine;

public class ChampionInspectorItem : MonoBehaviour
{
    [SerializeField] private GameObject _spellItem;

    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private Transform       _sheetHolder;
    [SerializeField] private Transform       _spellHolder;

    [SerializeField] private TextMeshProUGUI _health;
    [SerializeField] private TextMeshProUGUI _attack;
    [SerializeField] private TextMeshProUGUI _speed;

    public void Setup(Champion p_champion)
    {
        _name.text = p_champion.Behaviour.name;
        var t_go = Instantiate(p_champion.Behaviour.Sheet, _sheetHolder);
        t_go.GetComponent<ChampionSheet>().Champion = p_champion;
        var t_tr = t_go.GetComponent<RectTransform>();
        t_tr.anchorMax = Vector2.one;
        t_tr.anchorMin = Vector2.zero;
        t_tr.offsetMin = Vector2.zero;
        t_tr.offsetMax = Vector2.zero;
        p_champion.Spells.ForEach(t_spell => {
            Instantiate(_spellItem, _spellHolder).GetComponent<SpellItem>().Setup(t_spell);
        });

        _health.text = ((int)p_champion.Attributes.Health).ToString();
        _attack.text = ((int)p_champion.Attributes.Damage).ToString();
        _speed .text = ((int)p_champion.Attributes.Speed ).ToString();
    }
}
