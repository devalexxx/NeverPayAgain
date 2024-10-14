using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChampionInventory : MonoBehaviour
{
    [SerializeField]
    private List<Champion> _champions;

    [SerializeField]
    private GameObject _uiContainer;

    private void Awake()
    {
        _champions = new();

        foreach (Champion champion in _champions)  
        {
            DisplayChampion(champion);
        }
    }

    private void Update()
    {
        foreach (Transform tf in _uiContainer.transform)
        {
            SetChampionLevel(tf.gameObject, _champions[tf.GetSiblingIndex()].Progress.Level);
        }
    }

    private void SetChampionLevel(GameObject go, uint level)
    {
        Transform tf;
        if ((tf = go.transform.GetChild(1)) != null)
        {
            if ((tf = tf.GetChild(1)) != null)
            {
                if (tf.gameObject.TryGetComponent(out TextMeshProUGUI tmp))
                {
                    tmp.SetText(level.ToString());
                }
            }
        }
    }

    private void DisplayChampion(Champion champion)
    {
        if (champion.Behaviour.Sheet != null)
        {
            GameObject go = Instantiate(champion.Behaviour.Sheet);
            go.transform.SetParent(_uiContainer.transform);
            go.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            SetChampionLevel(go, champion.Progress.Level);
        }
    }

    public void AddChampion(Champion champion)
    {
        _champions.Add(champion);
        DisplayChampion(_champions[^1]);
    }
}
