using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;

    public float wait_time = 1;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!GameIsPaused)
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void Menu()
    {
        GameIsPaused = false;
        Time.timeScale = 1f;
        StartCoroutine(LoadTransition(SceneManager.GetActiveScene().buildIndex - 1));
    }
    IEnumerator LoadTransition(int ScreenIndex)
    {
        yield return new WaitForSeconds(wait_time);
        SceneManager.LoadScene(ScreenIndex);
    }
    public void QuitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}
