using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class queenHp : MonoBehaviour
{
    // Start is called before the first frame update
    public int hp;
    int maxHp = 2000;
    Animator animator;
    public queenControl control;
    int preHp;
    public bool hpIsDecreasing = false;
    public healthbarUI healthbar;
    private AudioSource[] soundEffects;
    private AudioSource deathSound;
    private bool isDeathSoundPlayed = false;
    public float wait_time = 3f;
    public int trapDmg = 20;
    void Start()
    {

        hp = maxHp;
        preHp = hp;
        animator = GetComponent<Animator>();
        healthbar.SetMaxHealth(maxHp);
        soundEffects = GetComponents<AudioSource>();
        deathSound = soundEffects[9];
    }

    void Update(){
        if (hp <= 0){
            animator.Play("Death");
            if(!isDeathSoundPlayed)
            {
                deathSound.Play();
                isDeathSoundPlayed = true;
                StartCoroutine(LoadTransition(SceneManager.GetActiveScene().buildIndex));
            }
        }
        if (hp < preHp && hp > 0)
        {
            if (!control.hrt && !control.atk)
            {
                animator.Play("Hurt");
                soundEffects[Random.Range(5, 9)].Play();
            }
            preHp = hp;
            hpIsDecreasing = true;
        }
        else if (hp > 0) hpIsDecreasing = false;
        healthbar.SetHealth(hp);
    }

    void OnCollisionEnter2D(Collision2D other){
        if (other.collider.tag == "DeadZone")
            hp = 0;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.collider.tag == "Trap")
            hp -= trapDmg;
    }

    public void LoadScreen()
    {
        StartCoroutine(LoadTransition(SceneManager.GetActiveScene().buildIndex));
    }

    IEnumerator LoadTransition(int ScreenIndex)
    {
        yield return new WaitForSeconds(wait_time);
        SceneManager.LoadScene(ScreenIndex);
    }

}
