using UnityEngine;

public class DungeonSelectionPane : MonoBehaviour
{
    private class Selection
    {
        public DungeonListItem item;
        public Dungeon         dungeon;
    }

    [SerializeField] private GameObject _dungeonContent;
    [SerializeField] private GameObject _dungeonItem;

    [SerializeField] private GameObject _stageContent;
    [SerializeField] private GameObject _stageItem;

    private Selection _selection;

    private void Awake()
    {
        _selection = null;

        GameManager.Instance.DungeonLibrary.ForEach(t_dungeon => {
            var t_go  = GameObject.Instantiate(_dungeonItem, _dungeonContent.transform);
            var t_dli = t_go.GetComponent<DungeonListItem>();
            t_dli.Setup(t_dungeon);
            t_dli.onClick += () => {
                if (_selection != null)
                {
                    _selection.item.SetSelected(false);
                    if (t_dli == _selection.item)
                    {
                        _selection = null;
                        OnSelectionChanged();
                        return;
                    }
                }
                else
                {
                    _selection = new();
                }

                _selection.item    = t_dli;
                _selection.dungeon = t_dungeon;
                t_dli.SetSelected(true);

                OnSelectionChanged();
            };
        });
    }

    private void OnSelectionChanged()
    {
        foreach (Transform t_child in _stageContent.transform)
        {
            GameObject.Destroy(t_child.gameObject);
        }

        if (_selection != null)
        {
            for (int i = 0; i < _selection.dungeon.Behaviour.StagesCount; i++)
            {
                GameObject.Instantiate(_stageItem, _stageContent.transform).GetComponent<StageListItem>().Setup(_selection.dungeon.Stages[i]);
            }
        }
    }
}
