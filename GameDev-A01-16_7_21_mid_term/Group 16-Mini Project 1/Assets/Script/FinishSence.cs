using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishSence : MonoBehaviour
{
    public float wait_time = 2f;
    public GameObject mainCharacter; 
    public SpriteRenderer sprite;
    private AudioSource winSound;
    private bool isPlayedWinSound;
    public void Start()
    {
        mainCharacter = GameObject.Find("Jump Queen");
        sprite = GetComponent<SpriteRenderer>();
        winSound = GetComponent<AudioSource>();
        isPlayedWinSound = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isPlayedWinSound)
        {
            winSound.Play();
            isPlayedWinSound = true;
        }
        StartCoroutine(LoadTransition(0));
    }
    public void Update()
    {
        if (
            mainCharacter.transform.position.x > sprite.transform.position.x - 1.5 &&
            mainCharacter.transform.position.x < sprite.transform.position.x + 1.5 &&
            mainCharacter.transform.position.y > sprite.transform.position.y - 3 &&
            mainCharacter.transform.position.y < sprite.transform.position.y + 3
            )
        {
            if (!isPlayedWinSound)
            {
                winSound.Play();
                isPlayedWinSound = true;
            }
            StartCoroutine(LoadTransition(0));
        }
    }

    public void LoadScreen()
    {
        StartCoroutine(LoadTransition(0));
    }
    IEnumerator LoadTransition(int ScreenIndex)
    {
        yield return new WaitForSeconds(wait_time);
        SceneManager.LoadScene(ScreenIndex);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
