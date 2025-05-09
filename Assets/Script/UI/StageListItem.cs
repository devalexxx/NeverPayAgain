using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageListItem : MonoBehaviour
{
    [SerializeField] private Button _playButton;

    public void Setup(DungeonStage p_stage)
    {
        _playButton.onClick.AddListener(() => {
            GameManager.Instance.ScenePayload = p_stage;
            SceneManager.LoadScene(p_stage.Dungeon.Behaviour.RelatedScene);
        });
    }
}
