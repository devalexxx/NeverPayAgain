using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ChampionLibrary : MonoBehaviour
{
    [SerializeField]
    private List<ChampionBehaviour> _champions;

    [SerializeField]
    private GameObject _uiContainer;

    [SerializeReference]
    private ChampionInventory _inventory;

    private void Awake()    
    {
        _champions = new();

        string[] guids = AssetDatabase.FindAssets("t:" + typeof(ChampionBehaviour).FullName);
        foreach (var guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            ChampionBehaviour asset = AssetDatabase.LoadAssetAtPath<ChampionBehaviour>(path);
            if (asset)
            {
                _champions.Add(asset);
            }
        }

        foreach (ChampionBehaviour champion in _champions)  
        {
            if (champion.Sheet != null)
            {
                GameObject go = Instantiate(champion.Sheet);
                go.GetComponent<Button>().onClick.AddListener(delegate () { OnSheetClicked(go); });
                go.transform.SetParent(_uiContainer.transform);
            }
        }
    }

    public void OnSheetClicked(GameObject go)
    {
        _inventory.AddChampion(new Champion(_champions[go.transform.GetSiblingIndex()]));
    }
}
