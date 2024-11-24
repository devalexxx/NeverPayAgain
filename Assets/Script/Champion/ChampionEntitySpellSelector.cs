using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChampionEntitySpellSelector : MonoBehaviour
{
    [SerializeReference] private UserDrivenChampionInstance _instance;
    [SerializeField]     private GameObject                 _spellContainer;
    [SerializeField]     private List<GameObject>           _spells;

    public UserDrivenChampionInstance Instance
    {
        get => _instance;
        set => _instance = value;
    }

    void OnEnable()
    {
        _spells.ForEach(spell => 
        {
            spell.transform.Find("Background").GetComponent<Image>().color = Color.black;
        });
    }

    void Start()
    {
        int cnt = 0;
        _instance?.Spells.AsEnumerable().Reverse().ToList().ForEach(inst => 
        {
            _spells.Add(Instantiate(inst.Spell.Behaviour.Sheet, _spellContainer.transform));
            _spells.Last().SetActive(false);
            _spells.Last().GetComponent<RectTransform>().sizeDelta *= 0.25f;
            int i = cnt;
            _spells.Last().AddComponent<Button>().onClick.AddListener(() =>  
            {
                _spells.ForEach(spell => 
                {
                    spell.transform.Find("Background").GetComponent<Image>().color = Color.black;
                });
                _instance.SelectedSpell = inst;
                _spells[i].transform.Find("Background").GetComponent<Image>().color = Color.white;
            });
            _spells.Last().AddComponent<SpellInstanceCooldownViewer>().Instance = inst;
            _spells.Last().SetActive(true);
            cnt += 1;
        });
    }    
}
