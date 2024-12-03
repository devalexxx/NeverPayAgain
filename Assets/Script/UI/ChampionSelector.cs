using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChampionSelector : MonoBehaviour
{
    [SerializeField] private GameObject _inventoryContainer;
    [SerializeField] private GameObject _selectionContainer;

    private List<Champion> _selection;
    public  List<Champion> Selection => _selection;

    private void Awake()
    {
        _selection = new();
    }

    private void Start()
    {
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

    private void OnSelect(Champion champion)
    {
        if (_selection.Count < 3)
        {
            _selection.Add(champion);
            GameObject go = Instantiate(champion.Behaviour.Sheet, _selectionContainer.transform);
            go.GetComponent<Button>().onClick.AddListener(() => OnUnSelect(champion, go));
        }
    }

    private void OnUnSelect(Champion champion, GameObject go)
    {
        _selection.Remove(champion);
        Destroy(go);
    }
}
