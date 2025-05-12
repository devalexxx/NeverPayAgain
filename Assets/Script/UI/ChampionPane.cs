using UnityEngine;
using UnityEngine.UI;

public class ChampionPane : MonoBehaviour
{
    [SerializeField] private GameObject _championInspectorItem;

    [SerializeField] private Transform _championHolder;
    [SerializeField] private Transform _championInspectorHolder;

    private void OnEnable()
    {
        Fill();    
        SaveManager.onSaveLoaded += Fill;   
    }

    private void OnDisable()
    {
        SaveManager.onSaveLoaded -= Fill;
    }

    private void Fill()
    {
        if (gameObject.activeSelf)
        {
            Clear();

            GameManager.Instance.Player.inventory.champion.ForEach(t_champion => {
                var t_go = Instantiate(t_champion.Behaviour.Sheet, _championHolder);
                t_go.GetComponent<ChampionSheet>().Champion = t_champion;
                t_go.GetComponent<Button>().onClick.AddListener(() => {
                    ClearInspector();
                    Instantiate(_championInspectorItem, _championInspectorHolder).GetComponent<ChampionInspectorItem>().Setup(t_champion);
                });
            });   
        }
    }

    private void Clear()
    {
        foreach (Transform t_champion in _championHolder)
        {
            Destroy(t_champion.gameObject);
        }

        ClearInspector();
    }

    private void ClearInspector()
    {
        foreach (Transform t_champion in _championInspectorHolder)
        {
            Destroy(t_champion.gameObject);
        }
    }
}
