using UnityEngine;
using UnityEngine.UI;

public class ChampionPane : MonoBehaviour
{
    [SerializeField] private GameObject _championInspectorItem;

    [SerializeField] private Transform _championHolder;
    [SerializeField] private Transform _championInspectorHolder;

    void Awake()
    {
        GameManager.Instance.Player.ChampionInventory.ForEach(t_champion => {
            var t_go = Instantiate(t_champion.Behaviour.Sheet, _championHolder);
            t_go.GetComponent<ChampionSheet>().Champion = t_champion;
            t_go.GetComponent<Button>().onClick.AddListener(() => {
                foreach (Transform t_tr in _championInspectorHolder)
                {
                    Destroy(t_tr.gameObject);
                }
                Instantiate(_championInspectorItem, _championInspectorHolder).GetComponent<ChampionInspectorItem>().Setup(t_champion);
            });
        });
    }
}
