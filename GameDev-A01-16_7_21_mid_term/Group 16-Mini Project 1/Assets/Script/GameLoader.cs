using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour
{
    public Animator transition;
    public float wait_time = 1f;

    public void LoadScreen()
    {
        StartCoroutine(LoadTransition(SceneManager.GetActiveScene().buildIndex + 1));
    }
    IEnumerator LoadTransition(int ScreenIndex)
    {
        transition.SetTrigger("ChangeScreen");
        yield return new WaitForSeconds(wait_time);
        SceneManager.LoadScene(ScreenIndex);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
