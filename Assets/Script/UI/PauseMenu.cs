using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
   
    public bool       gameIsPaused;
    public GameObject pauseMenuUI;

    void Awake()
    {
        gameIsPaused   = false;
        Time.timeScale = 1f;

        pauseMenuUI.transform.Find("ResumeButton").GetComponent<Button>().onClick.AddListener(Resume);
        pauseMenuUI.transform.Find("MenuButton")  .GetComponent<Button>().onClick.AddListener(LoadMenu);
        pauseMenuUI.transform.Find("QuitButton")  .GetComponent<Button>().onClick.AddListener(QuitGame);
    }
            
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
