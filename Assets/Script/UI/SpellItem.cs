using TMPro;
using UnityEngine;

public class SpellItem : MonoBehaviour
{
    [SerializeField] private Transform       _iconHolder;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _desc;
    [SerializeField] private TextMeshProUGUI _cooldown;

    public void Setup(Spell p_spell)
    {
        var t_go = Instantiate(p_spell.Behaviour.Sheet, _iconHolder);
        var t_tr = t_go.GetComponent<RectTransform>();
        t_tr.anchorMax = Vector2.one;
        t_tr.anchorMin = Vector2.zero;
        t_tr.offsetMin = Vector2.zero;
        t_tr.offsetMax = Vector2.zero;
        
        _name.text     = p_spell.Behaviour.name;
        _desc.text     = p_spell.Behaviour.name;
        _cooldown.text = p_spell.Behaviour.Cooldown.ToString();
    }
}
