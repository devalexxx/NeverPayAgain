using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoToDungeon : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => {
            SceneManager.LoadScene("DungeonTest");
        });
    }
}
