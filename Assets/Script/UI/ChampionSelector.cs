using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Manages the selection of champions for a team, showing available champions and handling selection/unselection.
public class ChampionSelector : MonoBehaviour
{
    [SerializeField] private GameObject _inventoryContainer; // The container where the available champions are displayed.
    [SerializeField] private GameObject _selectionContainer; // The container where selected champions are displayed.

    private List<Champion> _selection;  // List to store the currently selected champions.
    public  List<Champion> Selection => _selection;

    private void Awake()
    {
        _selection = new();
    }

    private void Start()
    {
        // Add all player's inventory champions
        GameManager.Instance.Player.ChampionInventory.ForEach(champion => {
            if (champion.Behaviour.Sheet != null)
            {
                GameObject go = Instantiate(champion.Behaviour.Sheet);
                go.GetComponent<Button>().onClick.AddListener(() => OnSelect(champion));
                go.transform.Find("Inner").transform.Find("Level").GetComponent<TextMeshProUGUI>().text = champion.Progress.Level.ToString();
                go.transform.SetParent(_inventoryContainer.transform);
            }
        });
    }

    // Handles selecting a champion when clicked from the inventory.
    private void OnSelect(Champion champion)
    {
        if (_selection.Count < 3)
        {
            _selection.Add(champion);
            GameObject go = Instantiate(champion.Behaviour.Sheet, _selectionContainer.transform);
            go.GetComponent<Button>().onClick.AddListener(() => OnUnSelect(champion, go));
        }
    }

    // Handles unselecting a champion, removing it from the selection.
    private void OnUnSelect(Champion champion, GameObject go)
    {
        _selection.Remove(champion);
        Destroy(go);
    }
}
