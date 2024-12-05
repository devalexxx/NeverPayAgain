using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

// Handles the user interface for selecting spells for a user-driven champion instance.
public class ChampionEntitySpellSelector : MonoBehaviour
{
    [SerializeReference] private UserDrivenChampionInstance _instance;          // Reference to the champion instance controlled by the user.
    [SerializeField]     private GameObject                 _spellContainer;    // Container for displaying spell UI elements.
    [SerializeField]     private List<GameObject>           _spells;            // List of instantiated spell UI elements.

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
        // Reverse the spell list to display them in the desired order and create UI for each spell.
        _instance?.Spells.AsEnumerable().Reverse().ToList().ForEach(inst => 
        {
            // Instantiate the spell's UI sheet and add it to the container.
            _spells.Add(Instantiate(inst.Spell.Behaviour.Sheet, _spellContainer.transform));

            // Disable the spell UI until fully initialized.
            _spells.Last().SetActive(false);

            // Adjust the size of the spell UI.
            _spells.Last().GetComponent<RectTransform>().sizeDelta *= 0.25f;
            int i = cnt;

            // Add a button component to the spell UI and configure its behavior when clicked.
            _spells.Last().AddComponent<Button>().onClick.AddListener(() =>  
            {
                // Reset all spell backgrounds to black.
                _spells.ForEach(spell => 
                {
                    spell.transform.Find("Background").GetComponent<Image>().color = Color.black;
                });
                _instance.SelectedSpell = inst;

                // Highlight the selected spell's background.
                _spells[i].transform.Find("Background").GetComponent<Image>().color = Color.white;
            });

            // Add a cooldown viewer component to display cooldown information for the spell.
            _spells.Last().AddComponent<SpellInstanceCooldownViewer>().Instance = inst;

            // Activate the spell UI.
            _spells.Last().SetActive(true);
            cnt += 1;
        });
    }    
}
