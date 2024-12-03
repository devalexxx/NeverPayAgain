using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
   
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    void Awake()
    {
        pauseMenuUI.transform.Find("ResumeButton").GetComponent<Button>().onClick.AddListener(Resume);
        pauseMenuUI.transform.Find("MenuButton").GetComponent<Button>().onClick.AddListener(LoadMenu);
        pauseMenuUI.transform.Find("QuitButton").GetComponent<Button>().onClick.AddListener(QuitGame);
    }
            
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Pause();
            }
            else
            {
                Resume();
                
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
