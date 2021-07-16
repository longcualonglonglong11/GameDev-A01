using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
